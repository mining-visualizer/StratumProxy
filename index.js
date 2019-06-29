

const VERSION = '1.0.0';

global.logger = logger = require('./lib/log');
logger.init(process.cwd(), 'log.txt');

var LogS = logger.LogS;
var LogD = logger.LogD;
var LogB = logger.LogB;

var RPCServer = require('./lib/rpc')
var StratumClient = require('./lib/stratum');

var path = require('path');
var fs = require('fs');
var ini = require('ini');

if (process.argv[2] == '-v' || process.argv[2] == '--version') {
   console.log(VERSION);
   process.exit();
}

var iniFile = path.join(process.cwd(), 'config.ini');

if (!fs.existsSync(iniFile)) {
	console.log('Error : unable to locate ', iniFile);
	process.exit();
}

var config = ini.parse(fs.readFileSync(iniFile, 'utf-8'));


LogB('======== starting stratum proxy ========');
var rpcServer = null;

init();

async function init() {

   var stratumClient = new StratumClient(config);

   if (!rpcServer) {
		rpcServer = new RPCServer(config);
   }

   // wait for shares from the miner and send them up to the pool. use the 
   // callback to notify the miner if share was accepted.
	rpcServer.on('submitShare', async function(args, callback) {
		var res = await stratumClient.submitShare(args);
		callback(res);
	});

	// if we get disconnected from the pool, try to reconnect
   stratumClient.on('disconnect', function() {
   	rpcServer.removeAllListeners('submitShare');
      LogB('Connection closed. Reconnecting in 5 seconds ...');
      setTimeout(init, 5000);

   }).on('workPackage', function(params) {
		// listen for work packages from the stratum pool and make them available to the rpc server
		rpcServer.setWork(params);
   });

}