<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/WebApp.Master" AutoEventWireup="true"
    CodeBehind="NewsList.aspx.cs" Inherits="Enow.MAJU.WebApp.NewsList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <link rel="Stylesheet" href="css/basic.css" type="text/css" media="screen" />
    <link rel="Stylesheet" href="css/zixun.css" type="text/css" media="screen" />
    <link rel="Stylesheet" href="css/ISrcoll/scrollbar.css" type="text/css" media="screen" />
    <script type="text/javascript" src="js/IScroll/IScroll4.2.5.js"></script>
    <script type="text/javascript" src="js/foucs.js"></script>
    <style type="text/css" media="all">
         /**
         *
         * 下拉样式 Pull down styles
         *
         */
        #pullDown, #pullUp {
	        background:#fff;
	        height:40px;
	        line-height:40px;
	        padding:5px 10px;
	        border-bottom:1px solid #ccc;
	        font-weight:bold;
	        font-size:14px;
	        color:#888;
        }
        #pullDown .pullDownIcon, #pullUp .pullUpIcon  {
	        display:block; float:left;
	        width:40px; height:40px;
	        background:url(images/pull-icon@2x.png) 0 0 no-repeat;
	        -webkit-background-size:40px 80px; background-size:40px 80px;
	        -webkit-transition-property:-webkit-transform;
	        -webkit-transition-duration:250ms;	
        }
        #pullDown .pullDownIcon {
	        -webkit-transform:rotate(0deg) translateZ(0);
        }
        #pullUp .pullUpIcon  {
	        -webkit-transform:rotate(-180deg) translateZ(0);
        }

        #pullDown.flip .pullDownIcon {
	        -webkit-transform:rotate(-180deg) translateZ(0);
        }

        #pullUp.flip .pullUpIcon {
	        -webkit-transform:rotate(0deg) translateZ(0);
        }

        #pullDown.loading .pullDownIcon, #pullUp.loading .pullUpIcon {
	        background-position:0 100%;
	        -webkit-transform:rotate(0deg) translateZ(0);
	        -webkit-transition-duration:0ms;

	        -webkit-animation-name:loading;
	        -webkit-animation-duration:2s;
	        -webkit-animation-iteration-count:infinite;
	        -webkit-animation-timing-function:linear;
        }

        @-webkit-keyframes loading {
	        from { -webkit-transform:rotate(0deg) translateZ(0); }
	        to { -webkit-transform:rotate(360deg) translateZ(0); }
        }
   </style>
    <header>
        <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
        <h1>
            资讯</h1>
    </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
    <!--baner------------start-->
    <div class="banner clearfix">
        <div id="slidePhoto" class="swipe-slide slides_wrap" style="width: 100%; overflow: hidden;">
        </div>
        <div id="slideNum" class="banner-num">
        </div>
        <div id="slideTitle" class="banner_txt">
        </div>
    </div>
    <!--baner------------end-->
    <div id="wrapper">
        <div class="zixun_list mt20">
            <ul id="thelist">
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <li onclick="javascript:$.mobile.changePage('NewsDetail.aspx?Id=<%#Eval("NewsId") %>', { transition: 'slide' });">
                            <dl>
                                <dt>
                                    <%#Eval("Title") %></dt>
                                <dd class="cont">
                                    <%#Eval("Context") %>
                                </dd>
                                <dd>
                                    <%#Eval("IssueTime","{0:yyyy-MM-dd HH:mm}")%></dd>
                            </dl>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <div id="pullUp">
                <span class="pullUpIcon"></span><span class="pullUpLabel">上拉加载更多...</span>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bd_ft" runat="server">
    <script type="text/javascript">
        /**
        图片幻灯片
        **/
        $(function () {
            $.getJSON("NewsList.aspx",
    { dotype: "getphoto" },
    function (result) {
        var strPhoto = "", strNum = "", strTitle = "";
        $(result).each(function (k, v) {
            if (k == 0) {
                strPhoto = '<div class="slide slide-transition active"><a href="#" target="_blank"><img src="' + v.PhotoPath + '" alt="" /></a></div>';
                strNum = '<a class="dot-select"></a>';
                strTitle = '<span class="txt_select">' + v.Title + '</span>';
                $("#slidePhoto").append(strPhoto);
                $("#slideNum").append(strNum);
                $("#slideTitle").append(strTitle);
            }
            else {
                strPhoto = '<div class="slide slide-transition"><a href="#" target="_blank"><img src="' + v.PhotoPath + '" alt="" /></a></div>';
                strNum = '<a class="dot"></a>';
                strTitle = '<span class="txt">' + v.Title + '</span>';
                $("#slidePhoto").append(strPhoto);
                $("#slideNum").append(strNum);
                $("#slideTitle").append(strTitle);
            }
        })
    })
});

var myScroll,
	    pullDownEl, pullDownOffset,
	    pullUpEl, pullUpOffset,
	    pagedown = 2, pageup = 2;

/**
* 滚动翻页 （自定义实现此方法）
* myScroll.refresh();		// 数据加载完成后，调用界面更新方法
*/
function pullUpAction() {
    setTimeout(function () {	// <-- Simulate network congestion, remove setTimeout from production!
        var el, li, l = "";
        el = document.getElementById('thelist');

        //上拉获取翻页
        $.getJSON(
                "NewsList.aspx"
                , { dotype: "pageup", page: pageup }
                , function (result) {
                    $(result).each(function (i, item) {
                        li = document.createElement('li');
                        li.innerHTML = '<li onclick="$.mobile.changePage("NewsDetail.aspx?Id=' + item.NewsId + '", { transition: "slide" });"><dl><dt>' + item.Title + '</dt><dd class="cont">' + item.Context + '</dd><dd>' + item.IssueTime + '</dd></dl></li>';
                        el.appendChild(li, el.childNodes[0]);
                    });
                });
        pageup += 1;
        myScroll.refresh(); 	// 数据加载完成后，调用界面更新方法 Remember to refresh when contents are loaded (ie: on ajax completion)
    }, 1000);     // <-- Simulate network congestion, remove setTimeout from production!
}

/**
* 初始化iScroll控件
*/
function loaded() {
    pullUpEl = document.getElementById('pullUp');
    pullUpOffset = pullUpEl.offsetHeight;

    myScroll = new iScroll('wrapper', {
        scrollbarClass: 'myScrollbar', /* 重要样式 */
        useTransition: false, /* 此属性不知用意，本人从true改为false */
        topOffset: pullDownOffset,
        onRefresh: function () {
            if (pullUpEl.className.match('loading')) {
                pullUpEl.className = '';
                pullUpEl.querySelector('.pullUpLabel').innerHTML = '上拉加载更多...';
            }
        },
        onScrollMove: function () {
            if (this.y < (this.maxScrollY - 5) && !pullUpEl.className.match('flip')) {
                pullUpEl.className = 'flip';
                pullUpEl.querySelector('.pullUpLabel').innerHTML = '松手开始更新...';
                this.maxScrollY = this.maxScrollY;
            } else if (this.y > (this.maxScrollY + 5) && pullUpEl.className.match('flip')) {
                pullUpEl.className = '';
                pullUpEl.querySelector('.pullUpLabel').innerHTML = '上拉加载更多...';
                this.maxScrollY = pullUpOffset;
            }
        },
        onScrollEnd: function () {
            if (pullUpEl.className.match('flip')) {
                pullUpEl.className = 'loading';
                pullUpEl.querySelector('.pullUpLabel').innerHTML = '加载中...';
                pullUpAction(); // Execute custom function (ajax call?)
            }
        }
    });

    setTimeout(function () { document.getElementById('wrapper').style.left = '0'; }, 800);
}

//初始化绑定iScroll控件 
document.addEventListener('touchmove', function (e) { e.preventDefault(); }, false);
document.addEventListener('DOMContentLoaded', loaded, false);
    </script>
</asp:Content>
