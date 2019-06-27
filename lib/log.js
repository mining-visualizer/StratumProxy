
const fs = require('fs');
var dateFormat = require('dateformat');
const os = require('os');
var path = require('path');
const util = require('util');

const MAX_SIZE =   2 * 1000 * 1000;       // trim log file when it exceeds MAX_SIZE bytes
const KEEP = 1 * 1000 * 1000;             // # of bytes to keep when trimming
const TRIM_EVERY = 6 * 60 * 60;           // how often to trim (seconds)

var logfile = '';
var fileStream;

module.exports =  {

   init(logfolder, filename) {
      // check that the folder exists
      if (!fs.existsSync(logfolder)) {
         fs.mkdirSync(logfolder);
      }
      logfile = path.join(logfolder, filename);
      fileStream = fs.createWriteStream(logfile, {flags: 'a'});
      // setTimeout(trimLogfile, 0);
   },


   // log to the console
   LogS() {
      console.log(concat(arguments, false));
   },


   // log to disk file
   LogD() {
      logToDisk(concat(arguments, true));
   },


   // log to both console and disk file
   LogB() {
      var logText = concat(arguments, false);
      console.log(logText);
      logText = dateFormat("'d'd ") + logText
      logToDisk(logText);
   }
}


function logToDisk(text) {
   fileStream.write(text + os.EOL);
}

function concat(args, includeDate) {
   var output = dateFormat((includeDate ? "'d'd " : "") + "HH:MM:ss.l> ");
   for (var i = 0; i < args.length; i++) {
      if (typeof args[i] == 'string') {
         output += args[i];
      } else {
         output += util.inspect(args[i]);
      }
   }
   return output;
}

function trimLogfile() {

   setTimeout(trimLogfile, TRIM_EVERY * 1000);

   if (fs.existsSync(logfile)) {
      var stats = fs.statSync(logfile);
      if (stats.size > MAX_SIZE) {
         var fileContents = fs.readFileSync(logfile, 'utf8');
         var cleanLineBreakPos = fileContents.indexOf(os.EOL, fileContents.length - KEEP);
         fileContents = fileContents.substr(cleanLineBreakPos);
         fs.writeFileSync(logfile, fileContents);
      }
   }
}