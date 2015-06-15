cordova.define("com.enow.cordova.plugin.tencent.PushNotificationListener", function (require, exports, module) {
    var cordova = require('cordova');
    var exec = require('cordova/exec');

    function handlers() {
        return tencentpushnotificationlistener.channels.xiaoxi.numHandlers;
    }

    var TencentPushNotificationListener = function () {
        this.channels = {
            xiaoxi: cordova.addWindowEventHandler("tencentpushnotification")
        };

        for (var key in this.channels) {
            this.channels[key].onHasSubscribersChange = TencentPushNotificationListener.onHasSubscribersChange;
        }
    }

    TencentPushNotificationListener.onHasSubscribersChange = function () {        
        if (this.numHandlers === 1 && handlers() === 1) {
            exec(tencentpushnotificationlistener.callbackSuccess, tencentpushnotificationlistener.callbackError, "TencentPushNotificationListener", "start", []);
        }
    };

    TencentPushNotificationListener.prototype.callbackSuccess = function (info) {
        cordova.fireWindowEvent("tencentpushnotification", info);
        console.log("tencentpushnotification listener success");
    };

    TencentPushNotificationListener.prototype.callbackError = function (e) {
        console.log("Error initializing tencentpushnotification");
    };

    var tencentpushnotificationlistener = new TencentPushNotificationListener();

    module.exports = tencentpushnotificationlistener;
});
