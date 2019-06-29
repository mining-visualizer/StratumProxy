
var jayson = require('jayson');
const EventEmitter = require('events');

var LogS = global.logger.LogS;
var LogD = global.logger.LogD;
var LogB = global.logger.LogB;


module.exports = class RPCServer extends EventEmitter {

	constructor (config) {
      super();
		var _this = this;

		this.minerEthAddress = config.Miner.EthAddress.toLowerCase();
		this.mintingAccount = "";
		this.minerVarDiff = 0;
		this.minimumShareTarget = "";
		this.challengeNumber = "";

		LogB('JSON-RPC server listening on localhost:' + config.RPC.Port);

		// create an rpc server
		var server = jayson.server({

			getPoolEthAddress: function(args, callback) {
				// LogB('getPoolEthAddress: ', args);
				callback(null, _this.mintingAccount);
			},

			getMinimumShareDifficulty: async function(args, callback) {
				// LogB('getMinimumShareDifficulty: ', args);
				if (args[0].toLowerCase() != _this.minerEthAddress) {
					callback({code: -1, message: 'Invalid minerEthAddresss ' + args[0] + '. Expecting ' + _this.minerEthAddress}); 
				}
				callback(null, _this.minerVarDiff);
			},

			getMinimumShareTarget: async function(args, callback) {
				// LogB('getMinimumShareTarget: ', args);
				if (args[0].toLowerCase() != _this.minerEthAddress) {
					callback({code: -1, message: 'Invalid minerEthAddresss ' + args[0] + '. Expecting ' + _this.minerEthAddress}); 
				}
				callback(null, _this.minimumShareTarget);
			},

			getChallengeNumber: async function(args, callback) {
				// LogB('getChallengeNumber: ', args);
				callback(null, _this.challengeNumber);
			},

			submitShare: async function(args, callback) {
				// LogB('submitShare: ', args);

				// notify interested listeners of a share submit. provide a callback so we can be 
				// notified if share was accepted (true/false).
         	_this.emit('submitShare', args, (res) => {
         		callback(null, res);
         	})
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

