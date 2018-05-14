
console.log('===================');
console.log('__dirname', __dirname);
console.log('process.cwd()', process.cwd());
console.log('process.execPath', process.execPath);
console.log('===================');

var RPCServer = require('./lib/rpc')
var StratumClient = require('./lib/stratum');

var fs = require('fs');
var ini = require('ini');
var config = ini.parse(fs.readFileSync('./config.ini', 'utf-8'));

var path = require('path');
var logger = require('./lib/log');
logger.init(path.resolve(__dirname), 'log.txt');

logger.LogB('======== starting stratum proxy ========');
var rpcServer = null;

init();

async function init() {

   var stratumClient = new StratumClient(config, logger);

   if (!rpcServer) {
		rpcServer = new RPCServer(config, logger);
   }

   // wait for shares from the miner and send them up to the pool
	rpcServer.on('submitShare', async function(args, callback) {
		var res = await stratumClient.submitShare(args);
		callback(null, res);
	});

	// if we get disconnected from the pool, try to reconnect
   stratumClient.on('disconnect', function() {
   	rpcServer.removeAllListeners('submitShare');
      logger.LogB('Connection closed. Reconnecting in 5 seconds ...');
      setTimeout(init, 5000);

   }).on('workPackage', function(params) {
		// listen for work packages from the stratum pool and make them available to the rpc server
		rpcServer.setWork(params);
   });

}