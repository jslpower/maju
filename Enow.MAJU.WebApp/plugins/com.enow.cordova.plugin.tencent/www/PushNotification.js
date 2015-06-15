cordova.define("com.enow.cordova.plugin.tencent.PushNotification", function (require, exports, module) {
    var cordova = require('cordova');
    var exec = require('cordova/exec');

    var TencentPushNotification = function () { };

    //register push
    TencentPushNotification.prototype.registerPush = function (successCallback, errorCallback, options) {
        var _username = "";        
        exec(successCallback, errorCallback, 'TencentPushNotification', 'registerPush', [_username]);
    }

    //unregister push
    TencentPushNotification.prototype.unregisterPush = function (successCallback, errorCallback, options) {
        exec(successCallback, errorCallback, 'TencentPushNotification', 'unregisterPush', []);
    }

    //send broadcast
    TencentPushNotification.prototype.sendBroadcast = function (successCallback, errorCallback, options) {
        exec(successCallback, errorCallback, 'TencentPushNotification', 'sendBroadcast', []);
    }

    var tencentpushnotification = new TencentPushNotification();

    module.exports = tencentpushnotification;
});
