

global.VERSION = '1.0.7';

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
var web3utils =  require('web3-utils');

if (process.argv[2] == '-v' || process.argv[2] == '--version') {
   console.log(VERSION);
   process.exit();
}

var iniPath = inPackage() ? path.dirname(process.execPath) : __dirname;
var iniFile = path.join(iniPath, 'config.ini');

if (!fs.existsSync(iniFile)) {
   console.log('Error : unable to locate ', iniFile);
   process.exit();
}

var config = ini.parse(fs.readFileSync(iniFile, 'utf-8'));

if (!web3utils.isAddress(config.Miner.EthAddress)) {
   console.log('\nError: invalid ETH address. Please set a valid ETH address in config.ini\n');
   process.exit();
}

LogB('======== Stratum Proxy v', VERSION, ' ========');
LogB('ETH address: ', config.Miner.EthAddress);

var shareCount = 1;
var rejected = 0;

var stratumClient = new StratumClient(config);
var rpcServer = new RPCServer(config);

init();

async function init() {

   // wait for shares from the miner and send them up to the pool. use the 
   // callback to notify the miner if share was accepted.
   rpcServer.on('submitShare', async (args, callback) => {
      var res = await stratumClient.submitShare(args);
      callback(res);
      rejected = res ? rejected : rejected + 1;
      process.stdout.write('Total shares: ' + shareCount++ + ',  Rejected: ' + rejected + "\r");

   }).on('activeMinerChanged', (activeMiner) => {
      if (activeMiner) {
         LogB('Mining activity resumed. Reconnecting to pool.');
         stratumClient.connectToPool();
      } else {
         LogB('Miner has gone inactive. Disconnecting from pool.');
         stratumClient.disconnect();
      }
   });

   // if we get disconnected from the pool and we have an active miner, try to reconnect.
   stratumClient.on('disconnect', () => {
      if (rpcServer.activeMiner) {
         LogB('Pool connection was closed. Reconnecting in 5 seconds ...');
         setTimeout(() => {
            stratumClient.connectToPool();
         }, 5000);
      }

   }).on('workPackage', (params) => {
      // listen for work packages from the stratum pool and make them available to the rpc server
      rpcServer.setWork(params);

   }).on('login', (error) => {
      rpcServer.poolLoginNotify(error);
   });

}

function inPackage() {
   // are we running in a packaged node environment, ie. npm pkg
   return process.pkg !== undefined;
}