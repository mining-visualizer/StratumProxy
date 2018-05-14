
var jayson = require('jayson');


// create a client
var client = jayson.client.http({
	host: 'localhost',
	port: 8500 
});

console.log(' ');

client.request('getChallengeNumber', [], function(err, response) {
	if(err) throw err;
	if (response.error) {
		console.log('getChallengeNumber returned error : ', response.error);
	}
	console.log('getChallengeNumber', response.result); 
});

client.request('getMinimumShareTarget', ["0x1b7bfB694eE51913c347971c7090a74AEFbd41f6"], function(err, response) {
	if(err) throw err;
	if (response.error) {
		console.log('getMinimumShareTarget returned error : ', response.error);
	}
	console.log('getMinimumShareTarget', response.result); 
});

client.request('getMinimumShareDifficulty', ["0x1b7bfB694eE51913c347971c7090a74AEFbd41f6"], function(err, response) {
	if(err) throw err;
	if (response.error) {
		console.log('getMinimumShareDifficulty returned error : ', response.error);
	}
	console.log('getMinimumShareDifficulty', response.result); 
});

client.request('getPoolEthAddress', [], function(err, response) {
	if(err) throw err;
	if (response.error) {
		console.log('getPoolEthAddress returned error : ', response.error);
	}
	console.log('getPoolEthAddress', response.result); 
});
