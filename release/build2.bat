@ECHO OFF

cd %~dp0

REM copy in the most recent c# StratumProxy.exe

copy ..\gui\bin\Release\StratumProxy.exe .

REM copy to staging

del /Q .\stage\*.*

copy mvsp.exe stage
copy README.txt stage
copy config.ini stage
copy StratumProxy.exe stage

REM create zip file

del "stratum-proxy-ver-win64.zip"

powershell.exe -nologo -noprofile -command "& { Add-Type -A 'System.IO.Compression.FileSystem'; [IO.Compression.ZipFile]::CreateFromDirectory('stage', 'stratum-proxy-ver-win64.zip'); }"

