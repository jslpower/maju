cordova.define("com.enow.cordova.plugin.denglu.WeiboDengLu", function(require, exports, module) {
    var cordova = require('cordova');
    var exec = require('cordova/exec');

    var denglu = function() { };

    denglu.prototype.denglu = function(successCallback, errorCallback,options) {
        exec(successCallback, errorCallback, 'WeiboDengLu','denglu',[options]);
    }

    var _denglu = new denglu();

    module.exports = _denglu;
});
