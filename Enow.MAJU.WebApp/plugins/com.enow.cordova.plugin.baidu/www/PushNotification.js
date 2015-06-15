cordova.define("com.enow.cordova.plugin.baidu.PushNotification", function (require, exports, module) {
    var cordova = require('cordova');
    var exec = require('cordova/exec');

    var BaiDuPushNotification = function () { };

    //register push
    BaiDuPushNotification.prototype.registerPush = function (successCallback, errorCallback, options) {
        var _username = "";        
        exec(successCallback, errorCallback, 'BaiDuPushNotification', 'registerPush', [_username]);
    }

    //unregister push
    BaiDuPushNotification.prototype.unregisterPush = function (successCallback, errorCallback, options) {
        exec(successCallback, errorCallback, 'BaiDuPushNotification', 'unregisterPush', []);
    }

    //send broadcast
    BaiDuPushNotification.prototype.sendBroadcast = function (successCallback, errorCallback, options) {
        exec(successCallback, errorCallback, 'BaiDuPushNotification', 'sendBroadcast', []);
    }

    var baidupushnotification = new BaiDuPushNotification();

    module.exports = baidupushnotification;
});
