<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Enow.MAJU.WebApp.index" %>

<!DOCTYPE html>
<!--HTML5 doctype-->
<html>
<head>
    <meta http-equiv="Content-type" content="application/xhtml+xml; charset=utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />   
    <script src="/js/IScroll/IScroll4.2.5.js" type="text/javascript"></script>
   <link rel="Stylesheet" href="/js/jquery.mobile-1.4.5.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="/css/ISrcoll/scrollbar.css" type="text/css" media="screen">
    <script src="/js/jquery_cm.js" type="text/javascript"></script>
    <script src="/js/jquery.mobile-1.4.5.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/css/basic.css" type="text/css" media="screen">
    <link rel="stylesheet" href="/css/user.css" type="text/css" media="screen">
    <script type="text/javascript" src="cordova.js"></script>
    <script type="text/javascript" src="js/enow.core.js"></script>
<title>马驹保险</title>
    <script>
        var ipage = {
            shareoption: { webpageUrl: window.location.href, title: '<%=System.Configuration.ConfigurationManager.AppSettings["ShareTitle"] %>', description: '<%=System.Configuration.ConfigurationManager.AppSettings["ShareDescription"] %>', imgUrl: "http://www.51maju.cn/images/xx_head1.png" },
            shareonsuccess: function (result) {
                var _s = JSON.stringify(result);
                console.log(_s);
//                                ipage.ShowMsg(result.xiaoxi);
            },
            shareonerror: function (message) {
                console.log('分享错误信息：' + message);
            },
            weiboshare: function () {

                window.eNow.fenXiang.weibo.fenXiang(ipage.shareonsuccess, ipage.shareonerror, ipage.shareoption);
            },
            weixinhaoyoushare: function () {

                //注：options.imgUrl指定的图片大小不能超过32KB，否则分享将会不成功
                //var _options = {webpageUrl:"http://www.enowinfo.com",title:"我是分享的标题2",description:"我是分享的内容2",imgUrl:"http://a.hiphotos.baidu.com/baike/s%3D220/sign=670fbb9e7e1ed21b7dc929e79d6fddae/8326cffc1e178a82599a55ccf503738da977e83c.jpg"};

                window.eNow.fenXiang.weiXin.haoYou(ipage.shareonsuccess, ipage.shareonerror, ipage.shareoption);
            },
            weixinpengyoushare: function () {

                //注：options.imgUrl指定的图片大小不能超过32KB，否则分享将会不成功
                //var _options = {webpageUrl:"http://www.baidu.com",title:"我是分享的标题1",description:"我是分享的内容1",imgUrl:"http://www.baidu.com/img/baidu_jgylogo3.gif"};

                window.eNow.fenXiang.weiXin.pengYouQuan(ipage.shareonsuccess, ipage.shareonerror, ipage.shareoption);
            },
            ShowMsg: function (msg) {
                var id = $("#popMsg");
                id.find("span").text(msg);
                id.show();
                $("#popMsg").find(".btn_yellow").click(function () {
                    $("#popMsg").hide();
                });
            },
            sharecancel: function () {
                $("#quxiao").click(function () {
                    $("#fenxiang").hide();
                })
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div data-role="page">
        <div data-role="none">
            <link rel="stylesheet" href="/css/home.css" type="text/css" media="screen">
            <style>
            #wrapper {
	        top:215px; bottom:65px; left:0;
        }#pullDown, #pullUp {
	        border-bottom:1px solid #ccc;
        }
            </style>
            <header>
                <a href="MyCollect.aspx" data-transition="slide"><i class="icon_star"></i></a>
                <h1>首页</h1>
                <a href="messagecenter.aspx" data-transition="slide"><b class="icon_email">
                <%if (!(new Enow.MAJU.WebApp.MessageCenter().IsMemberTopicReplyAllRead && new Enow.MAJU.WebApp.MessageCenter().IsSysMessageAllRead && new Enow.MAJU.WebApp.MessageCenter().IsProductReplyAllRead)) %>
                <%{ %>
                <em class="radius"></em>
                <%} %>
                </b></a>
            </header>
        </div>
        <div role="main" class="warp">
             <div class="home_nav">
     
                  <ul>
                     <li onclick='javascript:$.mobile.changePage("huatilist.aspx", { transition: "slide" });'>
                        <div class="radius ico"></div>
                        <div class="title">话题</div>
                     </li>
             
                     <li onclick='javascript:$.mobile.changePage("zixun.aspx", { transition: "slide" });'>
                        <div class="radius ico"></div>
                        <div class="title">资讯</div>
                     </li>

                     <li onclick='javascript:$.mobile.changePage("baoxian.aspx", { transition: "slide" });'>
                        <div class="radius ico"></div>
                        <div class="title">保险</div>
                     </li>

                     <li onclick='javascript:$.mobile.changePage("myfangan.aspx", { transition: "slide" });'>
                        <div class="radius ico"></div>
                        <div class="title">方案</div>
                     </li>

                  </ul>
             </div>

            <div id="wrapper1">
                <div class="home_item" id="scroller1">
		            <div id="pullDown" style="display:none;">
			            <span class="pullDownIcon"></span><span class="pullDownLabel">下拉刷新...</span>
		            </div>
                    <ul id="thelist">
                    <asp:Repeater ID="rpt" runat="server">
                    <ItemTemplate>
                        <li><div class="h_touxian radius"><img src='<%#string.IsNullOrEmpty((string)Eval("HeadPhoto"))?((int)Eval("T")==0?"/images/touxian48.png":"/images/touxian0.png"):Eval("HeadPhoto") %>'/></div><a href='<%#((int)Eval("T")==0?"huatidetail.aspx?Id=":"NewsDetail.aspx?Id=")+Eval("TopicId") %>' data-transition="slide"><%#Eval("Title") %></a></li>
                    </ItemTemplate>
                    </asp:Repeater>
                    </ul>
		            <%--<div id="pullUp" style="display:none;">
			            <span class="pullUpIcon"></span><span class="pullUpLabel">上拉加载更多...</span>
		            </div>--%>
                </div>
            </div>
        </div>
        <div data-role="none">
          <div class="bot_nav">
             <div class="bot_box">
                 <ul class="clearfix">
                     <li class="on">
                        <div class="ico"></div>
                        <p>首页</p>
                     </li>

                     <li>
                        <a href="more.aspx" data-transition="slide"><div class="ico"></div></a>
                        <p>更多</p>
                     </li>
                 </ul>
             </div>
          </div>
          <script type="text/javascript">
              var myScroll1,
	        pullDownEl1, pullDownOffset1,
	        pullUpEl1, pullUpOffset1,
	        pagedown1 = 2, pageup1 = 1;

              /**
              * 初始化iScroll控件
              */
              function loaded1() {
                  if (myScroll1 != null) {
                      //        alert(myScroll);
                      myScroll1.destroy();
                  }
                  pullDownEl1 = document.getElementById('pullDown');
                  pullDownOffset1 = pullDownEl1.offsetHeight;
                  //          pullUpEl1 = document.getElementById('pullUp');
                  //          pullUpOffset1 = pullUpEl1.offsetHeight;
                  //          pullUpEl1.style.display = 'none';
                  pullDownEl1.style.display = 'none';
                  setTimeout(function () {
                      myScroll1 = new iScroll('wrapper1', {
                          scrollbarClass: 'myScrollbar', /* 重要样式 */
                          useTransition: false, /* 此属性不知用意，本人从true改为false */
                          checkDOMChange: false,
                          topOffset: pullDownOffset1,
                          zoom: true,
                          onRefresh: function () {
                              if (pullDownEl1.className.match('loading')) {
                                  pullDownEl1.className = '';
                                  pullDownEl1.querySelector('.pullDownLabel').innerHTML = '下拉刷新...';
                                  pullDownEl1.style.display = 'none';
                              }
                              //                      else if (pullUpEl1.className.match('loading')) {
                              //                          pullUpEl1.className = '';
                              //                          pullUpEl1.querySelector('.pullUpLabel').innerHTML = '上拉加载更多...';
                              //                          pullUpEl1.style.display = 'none';
                              //                      }
                          },
                          onScrollMove: function () {
                              if (this.y > 5 && !pullDownEl1.className.match('flip')) {
                                  pullDownEl1.style.display = 'block';
                                  pullDownEl1.className = 'flip';
                                  pullDownEl1.querySelector('.pullDownLabel').innerHTML = '松手开始更新...';
                                  this.minScrollY = 0;
                              } else if (this.y < 5 && pullDownEl1.className.match('flip')) {
                                  pullDownEl1.style.display = 'block';
                                  pullDownEl1.className = '';
                                  pullDownEl1.querySelector('.pullDownLabel').innerHTML = '下拉刷新...';
                                  this.minScrollY = -pullDownOffset1;
                              }
                              //                      else if (this.y < (this.maxScrollY - 5) && !pullUpEl1.className.match('flip')) {
                              //                          pullUpEl1.style.display = 'block';
                              //                          pullUpEl1.className = 'flip';
                              //                          pullUpEl1.querySelector('.pullUpLabel').innerHTML = '松手开始更新...';
                              //                          this.maxScrollY = this.maxScrollY;
                              //                      } else if (this.y > (this.maxScrollY + 5) && pullUpEl1.className.match('flip')) {
                              //                          pullUpEl1.style.display = 'block';
                              //                          pullUpEl1.className = '';
                              //                          pullUpEl1.querySelector('.pullUpLabel').innerHTML = '上拉加载更多...';
                              //                          this.maxScrollY = this.maxScrollY;
                              //                      }
                          },
                          onScrollEnd: function () {
                              if (pullDownEl1.className.match('flip')) {
                                  pullDownEl1.className = 'loading';
                                  pullDownEl1.querySelector('.pullDownLabel').innerHTML = '加载中...';
                                  pullDownAction1(); // Execute custom function (ajax call?)
                              }
                              //                      else if (pullUpEl1.className.match('flip')) {
                              //                          pullUpEl1.className = 'loading';
                              //                          pullUpEl1.querySelector('.pullUpLabel').innerHTML = '加载中...';
                              //                          pullUpAction1(); // Execute custom function (ajax call?)
                              //                      }
                          }
                      });
                  }, 1000);

                  setTimeout(function () { document.getElementById('wrapper1').style.left = '0'; }, 800);
              }
              var pullDownAction1 = function () {
                  setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
                      var el, li, l = "", h = "/images/touxian0.png";
                      el = document.getElementById('thelist');

                      //下拉获取热门话题和咨询
                      $.getJSON(
                        "index.aspx"
                        , { dotype: "gethot", page: pagedown1 }
                        , function (result) {
                            if ($(result).length > 0) {
                                pagedown1 += 1;
                            }
                            $(result).each(function (i, item) {
                                switch (item.T) {
                                    case 0:
                                        l = "huatidetail.aspx?Id=" + item.TopicId;
                                        h = "/images/touxian48.png";
                                        break;
                                    case 1:
                                        l = "NewsDetail.aspx?Id=" + item.TopicId;
                                        h = "/images/touxian0.png";
                                        break;
                                    default:
                                        break;
                                }
                                if (item.HeadPhoto != null && $.trim(item.HeadPhoto).length > 0) {
                                    h = item.HeadPhoto;
                                }
                                li = document.createElement('li');
                                li.innerHTML = '<div class="h_touxian radius"><img src="' + h + '"/></div><a href="' + l + '" data-transition="slide">' + item.Title + '</a>';
                                $(el).empty();
                                el.insertBefore(li, el.childNodes[0]);
                            });
                        });
                      myScroll1.refresh(); 	//数据加载完成后，调用界面更新方法   Remember to refresh when contents are loaded (ie: on ajax completion)
                  }, 1000);               // <-- Simulate network congestion, remove setTimeout from production!
              }
              var pullUpAction1 = function () {
                  setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
                      var el, li, l = "", h = "/images/touxian0.png";
                      el = document.getElementById('thelist');

                      //上拉获取精选话题和咨询
                      $.getJSON(
                        "index.aspx"
                        , { dotype: "getessence", page: pageup1 }
                        , function (result) {
                            if ($(result).length > 0) {
                                pageup1 += 1;
                            }
                            $(result).each(function (i, item) {
                                switch (item.T) {
                                    case 0:
                                        l = "huatidetail.aspx?Id=" + item.TopicId;
                                        h = "/images/touxian48.png";
                                        break;
                                    case 1:
                                        l = "NewsDetail.aspx?Id=" + item.TopicId;
                                        h = "/images/touxian0.png";
                                        break;
                                    default:
                                        break;
                                }
                                if (item.HeadPhoto != null && $.trim(item.HeadPhoto).length > 0) {
                                    h = item.HeadPhoto;
                                }
                                li = document.createElement('li');
                                li.innerHTML = '<div class="h_touxian radius"><img src="' + h + '"/></div><a href="' + l + '" data-transition="slide">' + item.Title + '</a>';
                                $(el).empty();
                                el.appendChild(li, el.childNodes[0]);
                            });
                        });
                      myScroll1.refresh(); 	// 数据加载完成后，调用界面更新方法 Remember to refresh when contents are loaded (ie: on ajax completion)
                  }, 1000);   // <-- Simulate network congestion, remove setTimeout from production!
              }
              function initindex() {
                  setTimeout(loaded1, 100);
                  $(document).off("pageinit", initindex);
              }
              $(document).on("pageinit", initindex);

              //百度消息推送注册
              var _pushservice1 = function () {
                  console.log("start baidu pushnotification");
                  var _onSuccess = function (result) {
                      var _s = JSON.stringify(result);
                      console.log(_s);
                  };

                  var _onFail = function (message) {
                      console.log('register baidu push notification error.message:' + message);
                  };

                  if ("<%=IsPush %>" == "0") {
                      baidu.PushNotification.unregisterPush(_onSuccess, _onFail, {});
                  } else {
                      baidu.PushNotification.registerPush(_onSuccess, _onFail, {});
                  }
              };
              //百度消息推送监听事件
              var _onBaidupushnotification = function (info) {
                  var _s = JSON.stringify(info);
                  switch (info.leixing) {
                      case "1": //绑定
                          //保存
                          $.ajax({ type: "post", url: 'index.aspx?dotype=save&userid=' + info.userid + '&channelid=' + info.channelid, data: $("#form1").serialize(), cache: false, dataType: "json", async: false,
                              success: function (response) {
                                  ipage.ShowMsg(response.result);
                              },
                              error: function () {
                                  ipage.ShowMsg("请求异常");
                              }
                          });
                          break;
                      case "2": //解绑
                          break;
                      case "3": //透传消息
                          break;
                      case "4": //点击消息
                          break;
                      case "5": //自定义广播
                          break;
                      default: //注册监听
                          break;
                  }
                  console.log(_s);
              };
              //极光消息推送
              var _OnJiGuangPushNotification = function () {
                  var _onSuccess = function (result) {
                      var _s = JSON.stringify(result);
                      console.log(_s);
                  };

                  var _onFail = function (message) {
                      console.log('jpush notification error.message:' + message);
                  };
                  if ("<%=IsPush %>" == "0") {
                      window.eNow.push.jpush.stop(_onSuccess, _onFail, {});
                  } else {
                      window.eNow.push.jpush.resume(_onSuccess, _onFail, {});
                  }
              }
              //是否消息推送
              window.eNow.ready({ fn: function () { _pushservice1(); _OnJiGuangPushNotification(); }, onBaidupushnotification: _onBaidupushnotification });
          </script>
        </div>
    </div>
    <div class="user-mask" data-role="page" data-rel="dialog" id="popMsg">
        <div class="user-mask-box radius4" style="margin-top: -95px;">
            <div class="font18 cent">
                <span></span>
            </div>
            <div class="caozuo mt10">
                <ul>
                    <li style="width: 100%;"><a href="" class="btn_yellow radius4">确定</a></li>
                </ul>
            </div>
        </div>
    </div>
    <!-----分享--------->
    <div class="user-mask" data-role="page" data-rel="dialog" id="fenxiang">

       <div class="user-mask-tx">
       
          <div class="fx_menu radius4">
              <ul>
                   <li id="wb" onclick="ipage.weiboshare()"><img src="/images/skyblue_logo_sinaweibo_checked.png"><p>新浪微博</p></li>
                   <li id="hy" onclick="ipage.weixinhaoyoushare()"><img src="/images/skyblue_logo_wechat_checked.png"><p>微信好友</p></li>
                   <li id="py" onclick="ipage.weixinpengyoushare()"><img src="/images/skyblue_logo_wechatmoments_checked.png"><p>朋友圈</p></li>
              </ul>
          </div>
      
          <div class="item mt10 radius4" id="quxiao" onclick="ipage.sharecancel()">取消</div>
          
       </div>
    </div>
    </form>
</body>
</html>
