
MVIS Stratum Proxy

If you want to mine at MVIS Mining Pool using the Stratum protocol, but your 
mining software does not support this protocol, you can use this proxy 
program to bridge the gap. See the FAQ page at http://mvis.ca/faq.html 
for general information regarding the Stratum protocol.

INSTRUCTIONS

1. Double-click StratumProxy.exe to start the program.  
2. Enter an ETH address in the text box provided. This 
   is the ETH address you will receive payouts to from the 
   pool. This address needs to match the ETH address set 
   in your mining software.
3. Once you have done that the proxy should start automatically, and you
   should see some output in the console log.
4. Start your mining software and point it to http://localhost:8080


MULTIPLE MINERS

If you have multiple mining rigs, you can either run a separate 
instance of the proxy program on each mining rig, each one configured 
with a separate ETH address, or you can point all miners to the same 
proxy via your local area network. In this case all miners need to 
be configured with the same ETH address, which needs to match the 
EthAddress setting in config.ini.

Keep in mind if you do it this way, the mining pool will see all 
miners as one high-hashrate miner, and assign a difficulty according 
to the cumulative hash rate.

RUN FROM SOURCE

These instructions are for the Node process only, not the C# GUI.
This procedure should work either under Windows or Linux:

1. Make sure you have Node 10 installed. Node 8 may work as well, 
but I haven't tested it.
2. Clone the repo to a folder of your choice.
3. cd into that folder and type "npm install"
4. Edit config.ini to input your ETH address.
5. To run the program, type 'node index.js'