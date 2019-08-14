
cd %~dp0

REM For some reason this has to be in a separate batch file.  in my experience, any
REM commands after pkg will not execute.  it's like the batch file abruptly terminates!

pkg -t node10-win -o mvsp.exe ..\index.js --options title="Stratum Proxy"

