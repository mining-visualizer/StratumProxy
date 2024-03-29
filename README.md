## MVIS Stratum Proxy ##

If you want to mine at [MVIS Mining Pool](http://mvis.ca) using the Stratum protocol, but your mining software does not support this protocol, you can use this proxy program to bridge the gap. You can think of it as an interface or adapter between your mining software and the mining pool.  See the FAQ page at http://mvis.ca/faq.html for general information regarding the Stratum protocol.


### Windows Instuctions

1. Download the zip package from the [Releases](https://github.com/mining-visualizer/StratumProxy/releases) page and extract it to a folder of your choice.
1. Double-click `StratumProxy.exe` to start the program.  
1. Enter an ETH address in the text box provided. This is the ETH address you will receive payouts to from the pool. This address needs to match the ETH address set in your mining software.
1. Once you have done that the proxy should start automatically, and you should see some output in the log window at the bottom.
1. Start your mining software and configure it to connect to the proxy, ie. `http://192.168.xx.xx:8080`, or if the miner and proxy are on the same machine, `http://localhost:8080`

### Linux Instructions

1. Download the .tar.gz package from the [Releases](https://github.com/mining-visualizer/StratumProxy/releases) page and extract it to a folder of your choice. 
2. Open up `config.ini` in your preferred text editor.  The only thing you need to change is the `EthAddress` setting.  This is the ETH address you will receive payouts to from the pool.  This address needs to match the ETH address set in your mining software.
3. Run the program by typing `./stratumproxy`.
4. Start your mining software and configure it to connect to the proxy, ie. `http://192.168.xx.xx:8080`, or if the miner and proxy are on the same machine, `http://localhost:8080`

### Run From Source

If the supplied binaries are not suitable, or you prefer to run from source, these instructions should help:

1. `git clone https://github.com/mining-visualizer/StratumProxy.git`
2. `cd StratumProxy`
3. `npm install`
4. Make sure you have at least version 10 of node installed.
5. Open up `config.ini` in your preferred text editor.  The only thing you need to change is the `EthAddress` setting.  This is the ETH address you will receive payouts to from the pool.  This address needs to match the ETH address set in your mining software.
3. Run the program by typing `node index.js`.
4. Start your mining software and configure it to connect to the proxy, ie. `http://192.168.xx.xx:8080`, or if the miner and proxy are on the same machine, `http://localhost:8080`



### Multiple Miners

If you have multiple mining rigs, you can either run a separate instance of the proxy program on each mining rig, or you can point all miners to the same proxy via your local area network. 

If you use the second option, all miners need to be configured with the same ETH address, which needs to match the `EthAddress` setting in `config.ini`.  Another consequence of this option is that the mining pool will see all miners as one high-hashrate miner, and assign a difficulty according to the cumulative hash rate.

If you use the first option, you can choose to run all miners with the same ETH address, or you can use different addresses, it doesn't matter. The pool will assign a difficulty level appropriate for each individual miner.
