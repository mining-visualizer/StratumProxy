
const net = require('net');
const EventEmitter = require('events');

var LogS = global.logger.LogS;
var LogD = global.logger.LogD;
var LogB = global.logger.LogB;


function sleep(ms) {
  return new Promise(resolve => setTimeout(resolve, ms));
}

module.exports = class StratumClient extends EventEmitter {

   constructor (config) {
      super();
      this.config = config;
      this.minerAccount = config.Miner.EthAddress.toLowerCase();
      this.connectToPool();
   }

   connectToPool() {

      this.subscribed = false;
      this.submitShareResponse = null;
      this.dataBuffer = '';

      this.client = new net.Socket();
      this.client.setEncoding('utf8');
      this.client.setKeepAlive(true);

      this.client.on('connect', () => {
         LogB('Stratum proxy connected to ' + this.config.Stratum.Host + ':' + this.config.Stratum.Port);
         // subscribe to work notifications
         var msg = {
            id : 1,
            method : 'mining.subscribe',
            params : [this.minerAccount, 'Stratum Proxy v' + global.VERSION]
         };
         LogD('subscribing to pool with account ', this.minerAccount);
         this.client.write(JSON.stringify(msg) + '\n');

      }).on('data', (data) => {
         this.handleData(data);

      }).on('close', () => {
         this.subscribed = false;
         this.emit('disconnect');

      }).on('error', (e) => {
         if (e.code == 'ECONNREFUSED') {
            LogB('Socket error: unable to connect to mining pool at ', e.address + ':' + e.port, ' [ECONNREFUSED]');
         } else {
            LogB('Socket error: ', e.message);
         }

      });

      this.client.connect(this.config.Stratum.Port, this.config.Stratum.Host);
   }

   handleData(data) {
      this.dataBuffer += data;
      if (this.dataBuffer.indexOf('\n') !== -1) {
         var messages = this.dataBuffer.split('\n');
         var incomplete = this.dataBuffer.slice(-1) === '\n' ? '' : messages.pop();
         messages.forEach((message) => {
            if (message === '') return;
            this.handleMessage(message);
         });
         this.dataBuffer = incomplete;
      }
   }

   handleMessage(message) {
      try {
         var jsonData = JSON.parse(message);
         if (jsonData.id !== undefined) {
            switch (jsonData.id) {
               case 1:
                  // miner.subscribe response
                  this.subscribed = jsonData.result;
                  if (jsonData.error) {
                     LogB('Pool login rejected. Reason given: ', jsonData.error[1]);
                  }
                  this.emit('login', jsonData.error);
                  break;
               case 4:
                  // miner.submit response  
                  this.submitShareResponse = jsonData;
                  break;
            }
         } else {
            if (jsonData.method == 'mining.notify') {
               this.emit('workPackage', jsonData.params);
            } else {
               LogB('Unexpected data from mining pool: ' + jsonData);
            }
         }
      }
      catch (err) {
         LogB('Error in handleMessage:', err);
         LogB('Data received from pool:', message);
      }
   }

   async submitShare(args) {
      if (!this.subscribed) {
         LogB('Share received from miner, but not logged into pool');
         return false;
      }
      var msg = {
         id : 4,
         method : 'mining.submit',
         params : args
      };
      this.client.write(JSON.stringify(msg) + '\n');

      // wait for the pool response
      this.submitShareResponse = null;
      var timeNow = Date.now();
      const TIMEOUT = 20 * 1000;

      while (!this.submitShareResponse && Date.now() - timeNow < TIMEOUT) {
         await sleep(200);
      }
      if (this.submitShareResponse) {
         if (this.submitShareResponse.error) {
            LogB('Share rejected by pool. Reason: ', this.submitShareResponse.error[1]);
            return false;
         }
         return true;
      } else {
         LogB('Timeout waiting for mining.submit response from pool');
         return false;
      }

   }

   async disconnect() {
      this.subscribed = false;
      this.client.destroy();
   }

}




