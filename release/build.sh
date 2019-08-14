#!/bin/bash

pkg -t node10-linux -o ./stratumproxy ../index.js

rm -r ./stage/*

cp stratumproxy stage/
cp config.ini stage/
cp README.txt stage/

rm stratum-proxy-ver-linux.tar.gz

tar -czf stratum-proxy-ver-linux.tar.gz  --directory=stage .

echo "."
echo "."
echo "."

if [ $? -ge 1 ] ; then
   echo "ERRORS WERE ENCOUNTERED!!!"
   echo "."
   echo "."
   echo "."
   exit 1
fi

echo "script completed successfully"
echo "."
echo "."
echo "."
