
var jayson = require('jayson');
const EventEmitter = require('events');

var LogS = global.logger.LogS;
var LogD = global.logger.LogD;
var LogB = global.logger.LogB;


function sleep(ms) {
   return new Promise(resolve => setTimeout(resolve, ms));
}
 
module.exports = class RPCServer extends EventEmitter {

   constructor (config) {
      super();

      this.minerEthAddress = config.Miner.EthAddress.toLowerCase();
      this.mintingAccount = "";
      this.minerVarDiff = 0;
      this.minimumShareTarget = "";
      this.challengeNumber = "";
      this.lastRPCRequest = Date.now();
      this.activeMiner = true;
      this.poolConnection = false;
      this.poolError = null;

      // create an rpc server
      var server = jayson.server({

         getPoolEthAddress: (args, callback) => {
            this.serviceRPCRequest('getPoolEthAddress', () => {
               if (this.poolError) {
                  callback(this.poolError);
               } else {
                  callback(null, this.mintingAccount);
               }
            });
         },

         getMinimumShareDifficulty: async (args, callback) => {
            this.serviceRPCRequest('getMinimumShareDifficulty', () => {
               if (this.poolError) {
                  callback(this.poolError);
               } else if (args[0].toLowerCase() != this.minerEthAddress) {
                  callback({code: -1, message: 'Invalid minerEthAddresss ' + args[0] + '. Expecting ' + this.minerEthAddress}); 
               } else {
                  callback(null, this.minerVarDiff);
               }
            });
         },

         getMinimumShareTarget: async (args, callback) => {
            this.serviceRPCRequest('getMinimumShareTarget', () => {
               if (this.poolError) {
                  callback(this.poolError);
               } else if (args[0].toLowerCase() != this.minerEthAddress) {
                  callback({code: -1, message: 'Invalid minerEthAddresss ' + args[0] + '. Expecting ' + this.minerEthAddress}); 
               } else {
                  callback(null, this.minimumShareTarget);
               }
            });
         },

         getChallengeNumber: async (args, callback) => {
            this.serviceRPCRequest('getChallengeNumber', () => {
               if (this.poolError) {
                  callback(this.poolError);
               } else {
                  callback(null, this.challengeNumber);
               }
            });
         },

         submitShare: async (args, callback) => {
            this.serviceRPCRequest('submitShare', () => {
               if (this.poolError) {
                  callback(this.poolError);
               } else {
                  // notify interested listeners of a share submit. provide a callback so we can be 
                  // notified if share was accepted (true/false).
                  this.emit('submitShare', args, (res) => {
                     callback(null, res);
                  })
               }
            });
         },

      });

      server.http().listen(config.RPC.Port, config.RPC.Host, () => { 
         LogB(`JSON-RPC server listening on ${config.RPC.Host}:${config.RPC.Port}`);

      }).on('error', (err) => {
         if (err.code === 'EADDRINUSE') {
            LogB(`Error: unable to open port ${config.RPC.Port} ... port is already in use.`);
         } else {
            LogB(err.message);
         }
         process.exit();
      });


      // notify listeners if miner activity ceases. 
      setInterval(() => {
         const TIMEOUT = 5 * 60 * 1000;
         if (this.activeMiner) {
            if (Date.now() - this.lastRPCRequest > TIMEOUT) {
               this.activeMiner = false;
               // we're expecting that the listener will close the pool connection.
               this.poolConnection = false;
               this.emit('activeMinerChanged', false);
            }
         }
      }, 2 * 1000);
   
   }


   async serviceRPCRequest(method, callback) {
      this.lastRPCRequest = Date.now();
      if (!this.activeMiner) {
         this.activeMiner = true;
         // notify listeners that mining activity has resumed, and we need a pool connection.
         this.emit('activeMinerChanged', true);
      }

      // check for pool connection.  if not there, wait for it.
      while (!this.poolConnection) {
         await sleep(200);
      }
      callback();
   }


   setWork(params) {
      this.poolConnection = true;
      try {
         this.challengeNumber = params[0];
         this.minimumShareTarget = params[1];
         this.minerVarDiff = params[2];
         this.mintingAccount = params[3];
      }
      catch (err) {
         LogB('Error in setWork: unable to process mining pool work parameters');
         LogB('params=', params);
      }
   }
   

   poolLoginNotify(error) {
      if (error) {
         this.poolError = {code: error[0], message: error[1]};
      } else {
         this.poolError = null;
      }
   }

}

