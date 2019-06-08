

var RPCServer = require('./lib/rpc')
var StratumClient = require('./lib/stratum');

var path = require('path');
var fs = require('fs');
var ini = require('ini');

var iniFile = path.join(process.cwd(), 'config.ini');

if (!fs.existsSync(iniFile)) {
	console.log('Error : unable to locate ', iniFile);
	process.exit();
}

var config = ini.parse(fs.readFileSync(iniFile, 'utf-8'));

var logger = require('./lib/log');
logger.init(process.cwd(), 'log.txt');

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