cordova.define("com.enow.cordova.plugin.fenxiang.WeiboFenXiang", function(require, exports, module) {
    var cordova = require('cordova');
    var exec = require('cordova/exec');

    var fenXiang = function() { };

    //peng you quan
    fenXiang.prototype.fenXiang = function(successCallback, errorCallback, options) {
    	if(typeof options=="undefined") options={};
    	if(typeof options.webpageUrl=="undefinde") options.webpageUrl="";
    	if(typeof options.title=="undefinde") options.title="";
    	if(typeof options.description=="undefinde") options.description="";
    	if(typeof options.imgUrl=="undefinde") options.imgUrl="";
    	
        exec(successCallback, errorCallback, 'WeiboFenXiang', 'fenXiang', [options]);
    }

    var _fenXiang = new fenXiang();

    module.exports = _fenXiang;
});
