
MVIS Stratum Proxy

If you want to mine at MVIS Mining Pool using the Stratum protocol, but your 
mining software does not support this protocol, you can use this proxy 
program to bridge the gap. See the FAQ page at http://mvis.ca/faq.html 
for general information regarding the Stratum protocol.

INSTRUCTIONS

1. Open up config.ini in your preferred text editor. The only thing 
you need to change is the 'EthAddress' setting. This is the ETH address 
you will receive payouts to from the pool. This address needs to 
match the ETH address set in your mining software.

2. Run the program. You can simply double-click on the .exe and it will 
open up in a console window, however it is preferable to open a 
command prompt first, 'cd' into the correct directory, and then 
run it from there by typing stratumproxy.exe, the reason being, if 
the program crashes this will allow you to see the error message 
that caused the crash.

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

This procedure should work either under Windows or Linux:

1. Make sure you have Node 10 installed. Node 8 may work as well, 
but I haven't tested it.
2. Clone the repo to a folder of your choice.
3. cd into that folder and type npm install
4. Edit config.ini as explained above.
5. To run the program, type 'node index.js'