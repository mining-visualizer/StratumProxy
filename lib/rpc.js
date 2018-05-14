
var jayson = require('jayson');
const EventEmitter = require('events');

var LogS = null;
var LogD = null;
var LogB = null

module.exports = class RPCServer extends EventEmitter {

	constructor (config, logger) {
      super();
		var _this = this;

		this.minerEthAddress = config.Miner.EthAddress.toLowerCase();
		this.mintingAccount = "";
		this.minerVarDiff = 0;
		this.minimumShareTarget = "";
		this.challengeNumber = "";

      LogS = logger.LogS;
      LogD = logger.LogD;
      LogB = logger.LogB;

		LogB('JSON-RPC server listening on localhost:' + config.RPC.Port);

		// create a server
		var server = jayson.server({

			getPoolEthAddress: function(args, callback) {
				callback(null, _this.mintingAccount);
			},

			getMinimumShareDifficulty: async function(args, callback) {
				if (args[0].toLowerCase() != _this.minerEthAddress) {
					callback({code: -1, message: 'Invalid minerEthAddresss ' + args[0] + '. Expecting ' + _this.minerEthAddress}); 
				}
				callback(null, _this.minerVarDiff);
			},

			getMinimumShareTarget: async function(args, callback) {
				if (args[0].toLowerCase() != _this.minerEthAddress) {
					callback({code: -1, message: 'Invalid minerEthAddresss ' + args[0] + '. Expecting ' + _this.minerEthAddress}); 
				}
				callback(null, _this.minimumShareTarget);
			},

			getChallengeNumber: async function(args, callback) {
				callback(null, _this.challengeNumber);
			},

			submitShare: async function(args, callback) {
         	_this.emit('submitShare', args, callback);
			},

		});

		server.http().listen(config.RPC.Port);
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

