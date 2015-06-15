cordova.define("com.enow.cordova.plugin.baidu.PushNotificationListener", function (require, exports, module) {
    var cordova = require('cordova');
    var exec = require('cordova/exec');

    function handlers() {
        return baidupushnotificationlistener.channels.xiaoxi.numHandlers;
    }

    var BaiDuPushNotificationListener = function () {
        this.channels = {
            xiaoxi: cordova.addWindowEventHandler("baidupushnotification")
        };

        for (var key in this.channels) {
            this.channels[key].onHasSubscribersChange = BaiDuPushNotificationListener.onHasSubscribersChange;
        }
    }

    BaiDuPushNotificationListener.onHasSubscribersChange = function () {        
        if (this.numHandlers === 1 && handlers() === 1) {
            exec(baidupushnotificationlistener.callbackSuccess, baidupushnotificationlistener.callbackError, "BaiDuPushNotificationListener", "start", []);
        }
    };

    BaiDuPushNotificationListener.prototype.callbackSuccess = function (info) {
        cordova.fireWindowEvent("baidupushnotification", info);
        console.log("baidupushnotification listener success");
    };

    BaiDuPushNotificationListener.prototype.callbackError = function (e) {
        console.log("error initializing baidupushnotification");
    };

    var baidupushnotificationlistener = new BaiDuPushNotificationListener();

    module.exports = baidupushnotificationlistener;
});
