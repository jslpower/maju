cordova.define("com.enow.cordova.plugin.denglu.QQDengLu", function(require, exports, module) {
    var cordova = require('cordova');
    var exec = require('cordova/exec');

    var denglu = function() { };

    denglu.prototype.denglu = function(successCallback, errorCallback,options) {
        exec(successCallback, errorCallback, 'QQDengLu','denglu',[options]);
    }

    var _denglu = new denglu();

    module.exports = _denglu;
});
