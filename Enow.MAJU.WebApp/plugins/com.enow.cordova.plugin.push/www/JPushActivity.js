cordova.define("com.enow.cordova.plugin.push.JPushActivity", function(require, exports, module) {
    var cordova = require('cordova');
    var exec = require('cordova/exec');

    var jpush = function() { };

    //stop
    jpush.prototype.stop = function(successCallback, errorCallback, options) {
        exec(successCallback, errorCallback, 'JPushActivity', 'stop', [options]);
    }

    //resume
    jpush.prototype.resume = function(successCallback, errorCallback, options) {
        exec(successCallback, errorCallback, 'JPushActivity', 'resume', [options]);
    }

    var _jpush = new jpush();

    module.exports = _jpush;
});
