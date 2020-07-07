
var jayson = require('jayson');
const EventEmitter = require('events');

var LogS = global.logger.LogS;
var LogD = global.logger.LogD;
var LogB = global.logger.LogB;


module.exports = class RPCServer extends EventEmitter {

   constructor (config) {
      super();

      this.minerEthAddress = config.Miner.EthAddress.toLowerCase();
      this.mintingAccount = "";
      this.minerVarDiff = 0;
      this.minimumShareTarget = "";
      this.challengeNumber = "";

      // create an rpc server
      var server = jayson.server({

         getPoolEthAddress: (args, callback) => {
            // LogB('getPoolEthAddress: ', args);
            callback(null, this.mintingAccount);
         },

         getMinimumShareDifficulty: async (args, callback) => {
            // LogB('getMinimumShareDifficulty: ', args);
            if (args[0].toLowerCase() != this.minerEthAddress) {
               callback({code: -1, message: 'Invalid minerEthAddresss ' + args[0] + '. Expecting ' + this.minerEthAddress}); 
            }
            callback(null, this.minerVarDiff);
         },

         getMinimumShareTarget: async (args, callback) => {
            // LogB('getMinimumShareTarget: ', args);
            if (args[0].toLowerCase() != this.minerEthAddress) {
               callback({code: -1, message: 'Invalid minerEthAddresss ' + args[0] + '. Expecting ' + this.minerEthAddress}); 
            }
            callback(null, this.minimumShareTarget);
         },

         getChallengeNumber: async (args, callback) => {
            // LogB('getChallengeNumber: ', args);
            callback(null, this.challengeNumber);
         },

         submitShare: async (args, callback) => {
            // LogB('submitShare: ', args);

            // notify interested listeners of a share submit. provide a callback so we can be 
            // notified if share was accepted (true/false).
            this.emit('submitShare', args, (res) => {
               callback(null, res);
            })
         },

      });

      server.http().listen(config.RPC.Port, () => { 
         LogB('JSON-RPC server listening on localhost:' + config.RPC.Port);

      }).on('error', (err) => {
         if (err.code === 'EADDRINUSE') {
            LogB(`Error: unable to open port ${config.RPC.Port} ... port is already in use.`);
         } else {
            LogB(err.message);
         }
         process.exit();
      });

   }


   setWork(params) {
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
   
}

