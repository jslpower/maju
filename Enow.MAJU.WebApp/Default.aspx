<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Enow.MAJU.WebApp.Default" %>

<!DOCTYPE html>
<!--HTML5 doctype-->
<html>
<head>
    <meta http-equiv="Content-type" content="application/xhtml+xml; charset=utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta content="yes" name="apple-mobile-web-app-capable" />
    <meta content="black" name="apple-mobile-web-app-status-bar-style" />
    <meta content="telephone=no" name="format-detection" />
    <link rel="stylesheet" href="/css/basic.css" type="text/css" media="screen">
    <link rel="stylesheet" href="/css/user.css" type="text/css" media="screen">
    <link rel="stylesheet" href="/js/jquery.mobile-1.4.5.css" type="text/css" media="screen">
    <link rel="stylesheet" href="/css/ISrcoll/scrollbar.css" type="text/css" media="screen">
    <script src="/js/jquery_cm.js" type="text/javascript"></script>
    <script src="/js/jquery.mobile-1.4.5.min.js" type="text/javascript"></script>
    <script src="/js/IScroll/IScroll4.2.5.js" type="text/javascript"></script>
    <script type="text/javascript" src="cordova.js"></script>
    <script type="text/javascript" src="js/enow.core.js"></script>
    <title>马驹保险</title>
</head>
<body>
    <form id="form1" runat="server">
    <div data-role="page" id="pagelogin">
        <div data-role="header">
            <header class="log_head">
               
                <h1>
                </h1>
                <a href="index.aspx" data-transition="slideup"><b class="icon_home"></b></a>
            </header>
        </div>
        <div role="main" class="warp">
            <div class="login_form">
                <ul>
                    <li><s class="ico_user"></s>
                        <input id="m" name="m" type="tel" class="u-input" placeholder="输入手机号码" data-role="none">
                    </li>
                    <li><s class="ico_mima"></s>
                        <input id="p" name="p" type="password" class="u-input" placeholder="登录密码" data-role="none">
                    </li>
                </ul>
            </div>
            <div class="padd cent paddB mt20">
                <input id="login" type="button" class="y_btn radius4" value="登录" data-role="none"></div>
            <div class="mt10 right_txt" style="padding-right: 60px;">
                <a href="#pagefind1" class="font_blue" data-transition="slide" data-role="none">忘记密码？</a></div>
            <div class="login_nav mt20">
                <h2 class="padd paddB">
                    快捷方式登录/注册</h2>
                <ul>
                    <li id="qq">
                        <div class="radius ico">
                        </div>
                        <div class="title">
                            QQ登录</div>
                    </li>

                    <li id="weibo">
                        <div class="radius ico">
                        </div>
                        <div class="title">
                            微博登录</div>
                    </li>
                    <li id="mobile"><a href="#pageregister1" data-transition="slide">
                        <div class="radius ico">
                        </div>
                    </a>
                        <div class="title">
                            手机注册</div>
                    </li>
                </ul>
            </div>
        </div>
        <div data-role="none">
            <script type="text/javascript">
                var ipage = {
                    diff: 60,
                    clearid: null,
                    timer: function (_diff, _mobile, _o) {
                        ipage.clearid = setInterval(function () {
                            if (_diff <= 0) {
                                clearInterval(ipage.clearid);
                                $(_o).closest("a").on("tap", function () {
                                    ipage.getcode(ipage.diff, _mobile, _o);
                                })
                                $(_o).html("重新发送");
                                return false;
                            }
                            $(_o).closest("a").unbind();
                            $(_o).html(_diff + " 秒后重新发送");
                            _diff--;
                        }, 1000);
                    },
                    getcode: function (_diff, _mobile, _o) {
                        $.ajax({ type: "post", url: '/ashx/GetVerCode.ashx?MobilePhone=' + _mobile, data: $("#form1").serialize(), cache: false, dataType: "json", async: false,
                            success: function (response) {
                                if (response.result != "1") {
                                    ipage.ShowMsg(response.msg);
                                }
                                ipage.timer(_diff, _mobile, _o);
                                $(_o).closest("a").unbind();
                            },
                            error: function () {
                                ipage.ShowMsg("请求异常");
                            }
                        });
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
                    },
                    shareoption: { webpageUrl: window.location.href, title: '<%=System.Configuration.ConfigurationManager.AppSettings["ShareTitle"] %>', description: '<%=System.Configuration.ConfigurationManager.AppSettings["ShareDescription"] %>', imgUrl: "http://www.51maju.cn/images/xx_head1.png" },
                    shareonsuccess: function (result) {
                        var _s = JSON.stringify(result);
                        console.log(_s);
                        //                        ipage.ShowMsg(result.xiaoxi);
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
                    weibodengluonsuccess: function (result) {
                        var _s = JSON.stringify(result);
                        console.log(_s);
                        //alert(_s);
                        if (result.success == "1") {
                            $.ajax({ type: "post", url: "Weibo.aspx?dotype=login&id=" + result.uid + "&screen_name=" + encodeURI(result.screen_name) + "&gender=" + result.gender + "&avatar_large=" + result.avatar_large + "", cache: false, dataType: "json", async: false,
                                success: function (response) {
                                    if (response.result != "1") {
                                        ipage.ShowMsg(response.msg);
                                    } else {
                                        $.mobile.changePage(response.obj, { transition: "slideup" });
                                    }
                                },
                                error: function () {
                                    ipage.ShowMsg("请求异常");
                                }
                            });
                        } else {
                            ipage.ShowMsg("登录失败");
                        }
                    },
                    weibodengluonerror: function (message) {
                        console.log('微博登录错误信息：' + message);
                    },
                    qqdengluonsuccess: function (result) {
                        var _s = JSON.stringify(result);
                        console.log(_s);
                        //alert(_s);
                        if (result.success == "1") {
                            $.ajax({ type: "post", url: "qqlogin.aspx?openid=" + result.openid + "&access_token=" + result.access_token + "&nickname=" + encodeURI(result.nickname) + "&gender=" + encodeURI(result.gender) + "&figureurl_qq_1=" + result.figureurl_qq_1 + "&figureurl_qq_2=" + result.figureurl_qq_2 + "&figureurl_1=" + result.figureurl_1 + "&figureurl_2=" + result.figureurl_2 + "&figureurl=" + result.figureurl, cache: false, dataType: "json", async: false,
                                success: function (response) {
                                    if (response.result != "1") {
                                        ipage.ShowMsg(response.msg);
                                    } else {
                                        $.mobile.changePage(response.obj, { transition: "slideup" });
                                    }
                                },
                                error: function () {
                                    ipage.ShowMsg("请求异常");
                                }
                            });
                        } else {
                            ipage.ShowMsg("登录失败");
                        }
                    },
                    qqdeengluonerror: function (message) {
                        console.log('QQ登录错误信息：' + message);
                    },
                    weibodenglu: function () {
                        window.eNow.denglu.weibo.denglu(ipage.weibodengluonsuccess, ipage.weibodengluonerror, {});
                    },
                    qqdenglu: function () {
                        window.eNow.denglu.qq.denglu(ipage.qqdengluonsuccess, ipage.qqdeengluonerror, {});
                    }
                };

                var pagelogin = function () {
                    $("body").css("background", "#fff");
                    $(".warp").css("margin-top", "80px");

                    $("#login").on("tap", function () {
                        if ($("#m").val() == "") {
                            ipage.ShowMsg("请输入手机号码");
                            $("#m").focus();
                            return false;
                        };
                        if ($("#p").val() == "") {
                            ipage.ShowMsg("请输入登陆密码");
                            $("#p").focus();
                            return false;
                        };
                        $.ajax({ type: "post", url: 'default.aspx?doajax=login', data: $("#form1").serialize(), cache: false, dataType: "json", async: false,
                            success: function (response) {
                                if (response.result == "1") {
                                    $.mobile.changePage("index.aspx", { transition: "slideup" });
                                } else {
                                    ipage.ShowMsg(response.msg);
                                    return false;
                                }
                            },
                            error: function () {
                                ipage.ShowMsg("请求异常");
                            }
                        });
                    });

                    $("#qq").on("tap", function () {
                        ipage.qqdenglu();
                    });

                    $("#weibo").on("tap", function () {
                        ipage.weibodenglu();
                    });
                }

                $(document).on("pageshow", "#pagelogin", pagelogin);
            </script>
        </div>
    </div>
    <div data-role="page" id="pagefind1">
        <div data-role="header">
            <header>
                <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
                <h1>
                    找回密码</h1>
            </header>
        </div>
        <div role="main" class="warp">
            <h2 class="padd" style="padding-top: 40px;">
                请输入注册手机号码</h2>
            <div class="user_form mt20">
                <ul>
                    <li class="u_bg u_phone radius4"><s class="ico_phone"></s>
                        <input id="Tel1" type="tel" class="u-input" placeholder="请输入手机号" data-role="none" />
                        <span class="del_icon" style="display: block;"></span></li>
                </ul>
            </div>
            <div class="padd cent mt20 paddB">
                <input id="next" type="button" class="y_btn radius4" value="下一步" data-role="none"></div>
        </div>
        <div data-role="none">
            <script>
                var pagefind1 = function () {
                    $("#Tel1").val($("#m").val());
                    $(".del_icon").on("tap", function () {
                        $("#Tel1").val("").focus();
                    });
                    $("#next").on("tap", function () {
                        var _m = $("#Tel1").val();
                        if (_m == "") {
                            ipage.ShowMsg("请输入手机号");
                            $("#Tel1").focus();
                            return false;
                        }
                        var isMobile = /^(?:13\d|15\d|18\d)\d{5}(\d{3}|\*{3})$/; //手机号码验证规则
                        if (!isMobile.test(_m)) {
                            ipage.ShowMsg("请输入正确的手机号");
                            $("#Tel1").focus();
                            return false;
                        }
                        $.mobile.changePage("#pagefind2", "slide");
                    });
                }
                $(document).on("pageshow", "#pagefind1", pagefind1);
            </script>
        </div>
    </div>
    <div data-role="page" id="pagefind2">
        <div data-role="header">
            <header>
                <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
                <h1>
                    找回密码</h1>
            </header>
        </div>
        <div role="main" class="warp">
            <h2 class="padd mt20">
                已向手机号码：<span id="send"></span>发送验证码，请在输入框填入验证码，30分钟内有效</h2>
            <div class="user_form mt20">
                <ul class="clearfix">
                    <li class="u-code">
                        <div class="u_bg radius4">
                            <input id="code" name="code" type="text" class="u-input" placeholder="请输入收到的验证码"
                                data-role="none"></div>
                        <a class="code_btn radius4" data-role="none"><span id="timer"></span></a></li>
                    <li class="u_bg u_phone radius4 list"><span class="label_name right_txt">新密码：</span>
                        <input id="pwd" name="pwd" type="password" class="u-input" placeholder="6-20位数字字母组合"
                            data-role="none">
                    </li>
                   
                </ul>
            </div>
            <div class="padd cent paddB">
                <input id="register" type="button" class="y_btn radius4" value="确认" data-role="none"></div>
        </div>
        <div data-role="none">
            <script>
                var pagefind2 = function () {
                    $("#send").text($("#Tel1").val());
                    $("#timer").closest("a").on("tap", function () {
                        ipage.getcode(ipage.diff, $("#send").text(), $("#timer"));
                    });
                    $("#register").on("tap", function () {
                        if ($("#code").val() == "") {
                            ipage.ShowMsg("请输入收到的验证码");
                            $("#code").focus();
                            return false;
                        }
                        if ($("#pwd").val() == "") {
                            ipage.ShowMsg("请输入6-20位数字字母组合新密码");
                            $("#pwd").focus();
                            return false;
                        }
                        $.ajax({ type: "post", url: 'default.aspx?doajax=findpwd&mobile=' + $("#send").text(), data: $("#form1").serialize(), cache: false, dataType: "json", async: false,
                            success: function (response) {
                                ipage.ShowMsg(response.msg);
                                if (response.result == "1") {
                                    $.mobile.changePage("index.aspx", "slideup");
                                }
                            },
                            error: function () {
                                ipage.ShowMsg("请求异常");
                            }
                        });
                    });
                    ipage.getcode(ipage.diff, $("#send").text(), $("#timer"));
                }
                $(document).on("pageshow", "#pagefind2", pagefind2);
            </script>
        </div>
    </div>
    <div data-role="page" id="pageregister1">
        <div data-role="header">
            <header>
                <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
                <h1>
                    注册</h1>
            </header>
        </div>
        <div role="main" class="warp">
            <h2 class="padd" style="padding-top: 40px;">
                请输入手机号码注册</h2>
            <div class="user_form mt20">
                <ul>
                    <li class="u_bg u_phone radius4"><s class="ico_phone"></s>
                        <input id="Tel2" type="tel" class="u-input" placeholder="请输入手机号" data-role="none">
                        <span class="del_icon" style="display: block;"></span></li>
                </ul>
            </div>
            <div class="padd cent mt20 paddB">
                <input id="next1" type="button" class="y_btn radius4" value="下一步" data-role="none"></div>
        </div>
        <div data-role="none">
            <script>
                var pageregister1 = function () {
                    $(".del_icon").on("tap", function () {
                        $("#Tel2").val("").focus();
                    })
                    $("#next1").on("tap", function () {
                        if ($("#Tel2").val() == "") {
                            ipage.ShowMsg("请输入手机号");
                            $("#Tel2").focus();
                            return false;
                        }
                        var isMobile = /^(?:13\d|15\d|18\d)\d{5}(\d{3}|\*{3})$/; //手机号码验证规则
                        if (!isMobile.test($("#Tel2").val())) {
                            ipage.ShowMsg("请输入正确的手机号");
                            $("#Tel2").focus();
                            return false;
                        }
                        $.mobile.changePage("#pageregister2", "slide");
                    })
                }
                $(document).on("pageshow", "#pageregister1", pageregister1);
            </script>
        </div>
    </div>
    <div data-role="page" id="pageregister2">
        <div data-role="header">
            <header>
                <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
                <h1>
                    注册</h1>
            </header>
        </div>
        <div role="main" class="warp">
            <h2 class="padd mt20">
                已向手机号码：<span id="send1"></span>发送验证码，请在输入框填入验证码，30分钟内有效</h2>
            <div class="user_form mt20">
                <ul class="clearfix">
                    <li class="u-code">
                        <div class="u_bg radius4">
                            <input id="code1" name="code1" type="text" class="u-input" placeholder="请输入收到的验证码"
                                data-role="none"></div>
                        <a class="code_btn radius4" data-role="none"><span id="timer1"></span></a></li>
                    <li class="u_bg u_phone radius4 list"><span class="label_name">登录密码：</span>
                        <input id="pwd2" name="pwd2" type="password" class="u-input" placeholder="6-20位数字字母组合"
                            data-role="none">
                    </li>
                    <li>
                        <label class="font_blue">
                            <input id="agree" type="checkbox" value="" style="vertical-align: -1px;" data-role="none">
                            同意马驹服务条款</label></li>
                </ul>
            </div>
            <div class="padd cent paddB">
                <input id="register1" type="button" class="y_btn radius4" value="注册" data-role="none"></div>
        </div>
        <div data-role="none">
            <script>
                var pageregister2 = function () {
                    $("#send1").text($("#Tel2").val());
                    $("#timer1").closest("a").on("tap", function () {
                        ipage.getcode(ipage.diff, $("#send1").text(), $("#timer1"));
                    });
                    $("#register1").on("tap", function () {
                        if ($("#code1").val() == "") {
                            ipage.ShowMsg("请输入收到的验证码");
                            $("#code1").focus();
                            return false;
                        }
                        if ($("#pwd2").val() == "") {
                            ipage.ShowMsg("请输入6-20位数字字母组合密码");
                            $("#pwd2").focus();
                            return false;
                        }
                        if (!$("#agree").is(":checked")) {
                            ipage.ShowMsg("请阅读马驹服务条款");
                            $("#agree").focus();
                            return false;
                        }
                        $.ajax({ type: "post", url: 'default.aspx?doajax=register&mobile=' + $("#send1").text(), data: $("#form1").serialize(), cache: false, dataType: "json", async: false,
                            success: function (response) {
                                ipage.ShowMsg(response.msg);
                                if (response.result == "1") {
                                    $.mobile.changePage("index.aspx", "slide");
                                }
                            },
                            error: function () {
                                ipage.ShowMsg("请求异常");
                            }
                        });
                    });
                    ipage.getcode(ipage.diff, $("#send1").text(), $("#timer1"));
                }
                $(document).on("pageshow", "#pageregister2", pageregister2);
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
