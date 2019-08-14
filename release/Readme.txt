
MVIS Stratum Proxy

If you want to mine at MVIS Mining Pool using the Stratum protocol, but your 
mining software does not support this protocol, you can use this proxy 
program to bridge the gap. See the FAQ page at http://mvis.ca/faq.html 
for general information regarding the Stratum protocol.


WINDOWS INSTRUCTIONS

1. Double-click StratumProxy.exe to start the program.  
2. Enter an ETH address in the text box provided. This 
   is the ETH address you will receive payouts to from the 
   pool. This address needs to match the ETH address set 
   in your mining software.
3. Once you have done that the proxy should start automatically, and you
   should see some output in the log window at the bottom.
4. Start your mining software and point it to http://localhost:8080


LINUX INSTRUCTIONS

1. Open up config.ini in your preferred text editor.  The only thing you need to 
   change is the `EthAddress` setting.  This is the ETH address you will receive 
   payouts to from the pool.  This address needs to match the ETH address set in 
   your mining software.
2. Run the program by typing "./stratumproxy" without the quotes.
3. Start your mining software and point it to http://localhost:8080


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

