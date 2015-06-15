/**
* @fileOverview:enow.core.js
* @author:汪奇志 2014-12-03
* @requires:cordova.js,cordova_plugins.js
* @version:0.0.0.1
*/

(function () {

    if (!window.eNow) { window.eNow = {}; }

    //default url
    var __defaulturl = "http://www.baidu.com";
    //server url
    var __serverurl = "http://www.baidu.com";

    /**
    *@description:alert警告消息框 
    *@options:{message:'消息内容',title:'消息标题',callback:function(){var s='返回事件';},btnName:'按钮名称'}
    */
    window.eNow.alert = function (options) {
        if (typeof (options) === 'undefined') return;
        if (typeof (options.btnName) === 'undefined') options.btnName = '确定';
        if (typeof (options.callback) !== 'function') options.callback = function () { };
        if (typeof (options.title) === 'undefined') options.title = '消息';
        if (typeof (options.message) === 'undefined') options.message = '消息内容';

        navigator.notification.alert(options.message, options.callback, options.title, options.btnName);
    };

    /**
    *@description:confirm确认消息框 
    *@options:{message:'消息内容',title:'消息标题',callback:function(button){var s='返回事件';},btnOk:'OK按钮名称',btnCancel:'Cancel按钮名称'},callback.button:按下按钮索引
    */
    window.eNow.confirm = function (options) {
        if (typeof (options) === 'undefined') return;
        if (typeof (options.btnOk) === 'undefined') options.btnName = '确定';
        if (typeof (options.btnCancel) === 'undefined') options.btnName = '取消';
        if (typeof (options.callback) !== 'function') options.callback = function (button) { };
        if (typeof (options.title) === 'undefined') options.title = '消息';
        if (typeof (options.message) === 'undefined') options.message = '消息内容?';

        navigator.notification.confirm(options.message, options.callback, options.title, options.btnOk + ',' + options.btnCancel);
    };

    /**
    *@description:有网络连接时触发事件
    */
    window.eNow._onOnline = function () {
        //window.eNow.alert({ message: '网络已连接' });
    };

    /**
    *@description:无网络连接时触发事件
    */
    window.eNow._onOffline = function () {
        //window.eNow.alert({ message: '暂时无网络连接，请连接后重试' });
    };

    /**
    *@description:设备就绪(PhoneGap被完全加载)时触发事件 详细文档：http://docs.phonegap.com/en/4.0.0/cordova_events_events.md.html#Events
    */
    window.eNow._onDeviceReady = function (options) {
        //navigator.splashscreen.hide();
        setTimeout(function () { navigator.splashscreen.hide(); }, 1000);

        if (options.onPause != null) document.addEventListener("pause", options.onPause, false);
        if (options.onResume != null) document.addEventListener("resume", options.onResume, false);
        if (options.onBackbutton != null) document.addEventListener("backbutton", options.onBackbutton, false);
        if (options.onMenubutton != null) document.addEventListener("menubutton", options.onMenubutton, false);
        if (options.onSearchbutton != null) document.addEventListener("searchbutton", options.onSearchbutton, false);
        if (options.onStartcallbutton != null) document.addEventListener("startcallbutton", options.onStartcallbutton, false);
        if (options.onEndcallbutton != null) document.addEventListener("endcallbutton", options.onEndcallbutton, false);
        if (options.onVolumedownbutton != null) document.addEventListener("volumedownbutton", options.onVolumedownbutton, false);
        if (options.onVolumeupbutton != null) document.addEventListener("volumeupbutton", options.onVolumeupbutton, false);
        if (options.onBatterycritical != null) window.addEventListener("batterycritical", options.onBatterycritical, false);
        if (options.onBatterylow != null) window.addEventListener("batterylow", options.onBatterylow, false);
        if (options.onBatterystatus != null) window.addEventListener("batterystatus", options.onBatterystatus, false);
        if (options.onOnline != null) document.addEventListener("online", options.onOnline, false);
        if (options.onOffline != null) document.addEventListener("offline", options.onOffline, false);
        if (options.onBaidupushnotification != null) window.addEventListener("baidupushnotification", options.onBaidupushnotification, false);
        if (options.onTencentpushnotification != null) window.addEventListener("tencentpushnotification", options.onTencentpushnotification, false);
        
        if (options.fn != null) options.fn();
    };

    /**
    *@description:刷新 
    *@url:url
    */
    window.eNow.reload = function (url) {
        if (typeof (url) === 'undefined') url = __defaulturl;

        if (window.eNow.getConnectionType() == 'NONE') {
            window.eNow.alert({ message: '暂时无网络连接，请连接后重试' });
            return;
        }

        window.location.href = url;
        return false;
    };


    /**
    *@description:eNow初始化
    *@options:见_onDeviceReady
    */
    window.eNow.ready = function (options) {
        var _options = {
            onPause: null
            , onResume: null
            , onBackbutton: null
            , onMenubutton: null
            , onSearchbutton: null
            , onStartcallbutton: null
            , onEndcallbutton: null
            , onVolumedownbutton: null
            , onVolumeupbutton: null
            , onBatterycritical: null
            , onBatterylow: null
            , onBatterystatus: null
            , onOnline: null
            , onOffline: null
            , onBaidupushnotification: null
            , onTencentpushnotification:null
            , fn: null
        };

        if (typeof (options) === 'undefined') options = _options;

        if (typeof (options.onPause) !== 'function') options.onPause = null;
        if (typeof (options.onResume) !== 'function') options.onResume = null;
        if (typeof (options.onBackbutton) !== 'function') options.onBackbutton = null;
        if (typeof (options.onMenubutton) !== 'function') options.onMenubutton = null;
        if (typeof (options.onSearchbutton) !== 'function') options.onSearchbutton = null;
        if (typeof (options.onStartcallbutton) !== 'function') options.onStartcallbutton = null;
        if (typeof (options.onEndcallbutton) !== 'function') options.onEndcallbutton = null;
        if (typeof (options.onVolumedownbutton) !== 'function') options.onVolumedownbutton = null;
        if (typeof (options.onVolumeupbutton) !== 'function') options.onVolumeupbutton = null;
        if (typeof (options.onBatterycritical) !== 'function') options.onBatterycritical = null;
        if (typeof (options.onBatterylow) !== 'function') options.onBatterylow = null;
        if (typeof (options.onBatterystatus) !== 'function') options.onBatterystatus = null;
        if (typeof (options.onOnline) !== 'function') options.onOnline = null;
        if (typeof (options.onOffline) !== 'function') options.onOffline = function () {
            var networkstate = navigator.network.connection.type;
            if (networkstate == Connection.NONE) {
                navigator.notification.confirm('请确认网络连接已经开启', function (buttom) { if (buttom == 1) { navigator.app.exitApp(); } return false; }, '提示', '确定');
                return false;
            }
        };
        if (typeof (options.onBaidupushnotification) !== 'function') options.onBaidupushnotification = null;
        if (typeof (options.onTencentpushnotification) !== 'function') options.onTencentpushnotification = null;
        if (typeof (options.fn) !== 'function') options.fn = null;

        document.addEventListener("deviceready", function () { window.eNow._onDeviceReady(options); }, false);
    };

    //camera
    window.eNow.camera = {};

    /**
    *@description:navigator.camera.getPicture
    *@cameraSuccess：
    *@cameraError：
    *@cameraOptions：
    */
    window.eNow.camera.getPicture = function (cameraSuccess, cameraError, cameraOptions) {
        navigator.camera.getPicture(cameraSuccess, cameraError, cameraOptions);
    };

    //file
    window.eNow.file = {};

    //options{s:"内容",directory:"目录",fileName:"文件名",onwrite:function(){}}
    window.eNow.file.writeFile = function (options) {
        if (typeof options.onwrite !== 'function') options.onwrite = null;

        function _onFileSystemSuccess(fileSystem) {
            fileSystem.root.getDirectory(options.directory, { create: true, exclusive: false }, _onDirectorySuccess, _onFileSystemFail);
        }

        function _onDirectorySuccess(dirEntry) {
            dirEntry.getFile(options.fileName, { create: true, exclusive: false }, _onFileSuccess, _onFileSystemFail);
        }

        function _onFileSuccess(fileEntry) {
            fileEntry.createWriter(_onFileWriterSuccess, _onFileSystemFail);
        }

        function _onFileSystemFail(error) {
            console.log("文件写入失败：" + error.code);
        }

        function _onFileWriterSuccess(writer) {

            writer.onwrite = function (evt) {
                console.log("文件写入成功");
                if (options.onwrite != null) options.onwrite(evt)
            };

            writer.seek(writer.length);
            writer.write(options.s);
        }

        console.log("开始文件写入");
        window.requestFileSystem(LocalFileSystem.PERSISTENT, 0, _onFileSystemSuccess, _onFileSystemFail);
    };

    //options{directory:"目录",fileName:"文件名",onloadend:function(){}}
    window.eNow.file.readFile = function (options) {
        if (typeof options.onloadend !== 'function') options.onloadend = null;

        function _onFileSystemSuccess(fileSystem) {
            fileSystem.root.getDirectory(options.directory, { create: true, exclusive: false }, _onDirectorySuccess, _onFileSystemFail);
        }
        function _onDirectorySuccess(dirEntry) {
            dirEntry.getFile(options.fileName, { create: true, exclusive: false }, _onFileSuccess, _onFileSystemFail);
        }
        function _onFileSuccess(fileEntry) {
            fileEntry.file(_onFileRead, _onFileSystemFail);
        }
        function _onFileRead(file) {
            var reader = new FileReader();
            reader.onloadend = function (evt) {
                console.log("文件读取成功");
                if (options.onloadend != null) options.onloadend(evt.target.result);
            };
            reader.readAsText(file);
        }
        function _onFileSystemFail(error) {
            console.log("文件读取失败：" + error.code);
        }

        console.log("开始文件读取");
        window.requestFileSystem(LocalFileSystem.PERSISTENT, 0, _onFileSystemSuccess, _onFileSystemFail);
    };

    //options{directory:"目录",fileName:"文件名",onsuccess:function(){}}
    window.eNow.file.deleteFile = function (options) {
        if (typeof options.onsuccess !== 'function') options.onsuccess = null;

        function _onFileSystemSuccess(fileSystem) {
            fileSystem.root.getDirectory(options.directory, { create: true, exclusive: false }, _onDirectorySuccess, _onFileSystemFail);
        }
        function _onDirectorySuccess(dirEntry) {
            dirEntry.getFile(options.fileName, { create: true, exclusive: false }, _onFileSuccess, _onFileSystemFail);
        }
        function _onFileSuccess(fileEntry) {
            fileEntry.remove(function (entry) {
                console.log("文件删除成功");
                if (options.onsuccess != null) options.onsuccess(entry);
            }, function (error) {
                console.log("文件删除失败：" + error.code);
            });
        }
        function _onFileSystemFail(error) {
            console.log("文件删除失败："+error.code);
        }

        console.log("开始文件删除");
        window.requestFileSystem(LocalFileSystem.PERSISTENT, 0, _onFileSystemSuccess, _onFileSystemFail);
    };

    //options;{fileURL:"",serverURL:"",onsuccess:function(response){}}
    window.eNow.file.uploadFile = function (options) {
        if (typeof options.onsuccess !== 'function') options.onsuccess = null;

        function _onSuccess(r) {
            console.log("Code = " + r.responseCode);
            console.log("Response = " + r.response);
            console.log("Sent = " + r.bytesSent);
            navigator.notification.progressStop();
            if (options.onsuccess != null) options.onsuccess(r.response);
        }

        function _onFail(error) {
            alert("An error has occurred: Code = " + error.code);
            console.log("upload error source " + error.source);
            console.log("upload error target " + error.target);
        }

        var _serveruri = encodeURI(options.serverURL);

        var _options = new FileUploadOptions();
        _options.fileKey = "file";
        _options.fileName = options.fileURL.substr(options.fileURL.lastIndexOf('/') + 1);
        _options.mimeType = "image/jpeg";

        var _headers = { 'Authorization': 'Basic dGVzdHVzZXJuYW1lOnRlc3RwYXNzd29yZA=='};
        var _params = { 'fileentry_fullpath': '', 'fileentry_url': '' };

        _options.headers = _headers;
        _options.params = _params;

        var _ft = new FileTransfer();
        _ft.onprogress = function (progressEvent) {
            if (progressEvent.lengthComputable) {
                var _p = Math.round((progressEvent.loaded / progressEvent.total) * 100);
                //console.log(_p);
                if (_p >= 100) navigator.notification.progressStop();
                else navigator.notification.progressValue(_p);                
            } else {
                //console.log('loadingStatus.increment();');
            }
        };

        window.resolveLocalFileSystemURI(options.fileURL, _onFileSuccess, _onFileError);

        function _onFileError(message) {
            console.log(message)
        }

        function _onFileSuccess(entry) {
            _params.fileentry_fullpath = entry.fullPath;
            _params.fileentry_url = entry.toURL();

            navigator.notification.progressStart("", "当前上传进度");
            _ft.upload(options.fileURL, _serveruri, _onSuccess, _onFail, _options);            
        }        
    };

    //options:{sourceURL:"",onsuccess:function(response){}}
    window.eNow.file.downloadFile = function (options) {
        if (typeof options == 'undefined') { console.log('参数错误'); return; }
        if (typeof options.sourceURL == 'undefined') { console.log('参数错误'); return; }
        if (options.sourceURL.length < 1) { console.log('没有需要下载的文件'); return; }

        var _fileTransfer = new FileTransfer();
        var _sourceUri = encodeURI(options.sourceURL);
        var _reg = /[^\\\/]*[\\\/]+/g;
        var _fileName = options.sourceURL.replace(_reg, '');

        if (_fileName.length < 1) { console.log('没有需要下载的文件'); return; }

        function _onSuccess(entry) {
            console.log("download complete: " + entry.toURL());

            options.onsuccess(entry);
        }

        function _onError(error) {
            console.log("download error source " + error.source);
            console.log("download error target " + error.target);
            console.log("upload error code" + error.code);
        }

        var _options = {};
        var _headers = { "Authorization": "Basic dGVzdHVzZXJuYW1lOnRlc3RwYXNzd29yZA==" };
        _options["headers"] = _headers;

        var _fileURL = "";

        function _onFileSystemSuccess(fileSystem) {
            fileSystem.root.getDirectory('EnowApp', { create: true, exclusive: false }, _onDirectorySuccess, _onFileSystemFail);
        }

        function _onDirectorySuccess(dirEntry) {            
            _fileURL = dirEntry.toURL();
            _fileURL += '/' + _fileName;
            console.log(_fileURL);
            _fileTransfer.download(_sourceUri, _fileURL, _onSuccess, _onError, false, _options);
        }

        function _onFileSystemFail(error) {
            console.log("文件下载失败：" + error.code);
        }

        window.requestFileSystem(LocalFileSystem.PERSISTENT, 0, _onFileSystemSuccess, _onFileSystemFail);
    };
    
    window.eNow.fenXiang = {};
    window.eNow.fenXiang.weiXin = {};

    window.eNow.fenXiang.weiXin.pengYouQuan = function(successCallback, errorCallback, options) {
        console.log("weixin fenxiang pengyouquan start");
        fenxiang.WeiXinFenXiang.pengYouQuan(successCallback, errorCallback, options);
    }

    window.eNow.fenXiang.weiXin.haoYou = function(successCallback, errorCallback, options) {
        console.log("weixin fenxiang pengyouquan start");
        fenxiang.WeiXinFenXiang.haoYou(successCallback, errorCallback, options);
    }
    
    window.eNow.fenXiang.weibo = {};

    window.eNow.fenXiang.weibo.fenXiang = function(successCallback, errorCallback, options) {
        console.log("weibo fenxiang start");
        fenxiang.WeiboFenXiang.fenXiang(successCallback, errorCallback, options);
    }

	window.eNow.denglu={};
	window.eNow.denglu.weibo = {};

    window.eNow.denglu.weibo.denglu = function(successCallback, errorCallback,options) {
        console.log("weibo denglu start");
        denglu.WeiboDengLu.denglu(successCallback, errorCallback,options);
    }

	window.eNow.denglu.qq = {};

    window.eNow.denglu.qq.denglu = function(successCallback, errorCallback,options) {
        console.log("qq denglu start");
        denglu.QQDengLu.denglu(successCallback, errorCallback,options);
    }

	window.eNow.push = {};
    window.eNow.push.jpush = {};

    window.eNow.push.jpush.stop = function(successCallback, errorCallback, options) {
        console.log("push jpush stop start");
        push.JPushActivity.stop(successCallback, errorCallback, options);
    }

    window.eNow.push.jpush.resume = function(successCallback, errorCallback, options) {
        console.log("push jpush resume start");
        push.JPushActivity.resume(successCallback, errorCallback, options);
    }
    

})();
