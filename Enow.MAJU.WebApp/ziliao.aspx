<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ziliao.aspx.cs" Inherits="Enow.MAJU.WebApp.ziliao"
    MasterPageFile="~/MasterPage/WebApp.Master" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bd_hd" runat="server">
    <script type="text/javascript" src="js/CitySelect.js"></script>
   <header>
        <a data-rel="back" data-direction="reverse"><i class="returnico"></i></a>
        <h1>
            编辑资料</h1>
        <a href=""><b class="icon_ok" id="edit"></b></a>
    </header>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="bd_bd" runat="server">
    <div class="list_item form_list mt10">
        <ul>
            <li class="R_jiantou"><span class="label_name">头像</span>
                <div class="h_touxian radius" style="right: 35px;">
                    <img src="images/touxian48.png" id="imgHeadPhoto" runat="server" />
                    <input type="hidden" id="hidHeadPhoto" runat="server" />
                </div>
            </li>
            <li class="R_jiantou"><span class="label_name">昵称</span>
                <input type="text" id="txtNickName" name="txtNickName" runat="server" class="u-input"
                    data-role="none" placeholder="请填写昵称" readonly />
            </li>
            <li class="R_jiantou"><span class="label_name">性别</span>
                <select id="selGender" runat="server" class="u-input" data-role="none">
                </select>
            </li>
            <li class="R_jiantou"><span class="label_name">年龄</span>
                <select id="selAge" runat="server" class="u-input" data-role="none">
                </select>
            </li>
        </ul>
    </div>
    <div class="list_item form_list mt10">
        <ul>
            <li class="R_jiantou"><span class="label_name">家庭年收入</span>
                <select id="selRevenue" runat="server" class="u-input" data-role="none">
                </select>
            </li>
            <li class="R_jiantou"><span class="label_name">保险需求</span>
                <select id="selRequirement" runat="server" class="u-input" data-role="none">
                </select>
            </li>
        </ul>
    </div>
    <div class="list_item form_list mt10">
        <ul>
            <li class="R_jiantou"><span class="label_name">邮箱地址</span>
                <input id="txtEmail" runat="server" type="text" class="u-input" data-role="none"
                    placeholder="请填写邮箱" readonly>
            </li>
            <li class="R_jiantou"><span class="label_name">所在省份</span>
                <select id="selProvince" runat="server" class="u-input" data-role="none">
                </select>
            </li>
            <li class="R_jiantou"><span class="label_name">所在城市</span>
                <select id="selCity" runat="server" class="u-input" data-role="none">
                </select>
            </li>
        </ul>
    </div>

    <!-----姓名输入--------->
    <div class="user-mask" data-role="none" style="display: none;" id="pop">
        <div class="user-mask-box radius4" style="margin-top: -95px;">
            <div class="font18 cent">
                请输入您的<span></span></div>
            <div class="cent">
                <input name="" type="text" class="input_bk radius4" data-role="none"></div>
            <div class="caozuo">
                <ul>
                    <li class="paddR10"><a href="" class="btn_gray radius4" data-role="none">取消</a></li>
                    <li style="padding-left: 10px;"><a href="" class="btn_yellow radius4" data-role="none">
                        确定</a></li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="bd_ft" runat="server">
    <!-----上传头像--------->
    <div class="user-mask" style="display: none;" data-role="none" id="poptouxiang">
        <div class="user-mask-tx">
            <div class="tx_menu radius4">
                <ul>
                    <li id="btnUpload">上传头像</li>
                    <li id="btnCamera">拍照</li>
                    <li id="btnPhoto">相册</li>
                </ul>
            </div>
            <div class="item mt10 radius4" id="cancel">
                取消</div>
        </div>
    </div>
<!--消息提示框-->
     <div class="user-mask" data-role="none" style="display: none;" id="popMsg">
        <div class="user-mask-box radius4" style="margin-top: -95px;">
            <div class="font18 cent">
                <input type="hidden" id="hidStatus" value="0" />
                <span></span>
            </div>
            <div class="caozuo mt10">
                <ul>
                    <li style="width: 100%;"><a href="" class="btn_yellow radius4" data-role="none">确定</a></li>
                </ul>
            </div>
        </div>
    </div>
    <script type="text/javascript">

        function ShowMsg(msg) {
            var id = $("#popMsg");
            id.find("span").text(msg);
            id.show();
        }

        var iPage = {
            _headphoto: "",
            //上传
            _photoUrl: "",
            _upload: function () {

                if ($.trim(iPage._headphoto).length == 0) {
                    ShowMsg("请选择头像");
                }
                else {
                    var __options = {
                        fileURL: iPage._headphoto, serverURL: '<%=System.Configuration.ConfigurationManager.AppSettings["UploadFilePath"] %>/ashx/Handler.ashx?dotype=shangchuan',
                        onsuccess: function (response) {
                            var info = JSON.parse(response);

                            $("#<%=hidHeadPhoto.ClientID %>").val(info.Filepath);
                            $("#<%=imgHeadPhoto.ClientID %>").attr("src", response.Filepath);

                        }
                    };


                    window.eNow.file.uploadFile(__options);



                    $("#poptouxiang").hide();
                }
            },
            //拍照
            _camera: function () {
                function _onSuccess(imageURI) {
                    iPage._headphoto = imageURI;
                    $("#<%=imgHeadPhoto.ClientID %>").attr("src", imageURI);
                }

                function _onError(message) {
                    ShowMsg('Failed because: ' + message);
                }

                var _options = { quality: 75,
                    destinationType: Camera.DestinationType.FILE_URI,
                    sourceType: Camera.PictureSourceType.CAMERA,
                    allowEdit: true,
                    encodingType: Camera.EncodingType.JPEG,
                    targetWidth: 100,
                    targetHeight: 100,
                    popoverOptions: CameraPopoverOptions,
                    saveToPhotoAlbum: false
                };

                eNow.camera.getPicture(_onSuccess, _onError, _options);
            },
            //相册
            _photo: function () {
                function _onSuccess(imageURI) {
                    iPage._headphoto = imageURI;
                    $("#<%=imgHeadPhoto.ClientID %>").attr("src", imageURI);
                }

                function _onError(message) {
                    ShowMsg('Failed because: ' + message);
                }

                var _options = {
                    quality: 75,
                    destinationType: Camera.DestinationType.FILE_URI,
                    sourceType: Camera.PictureSourceType.PHOTOLIBRARY,
                    popoverOptions: CameraPopoverOptions,
                    saveToPhotoAlbum: false,
                    mediaType: Camera.MediaType.ALLMEDIA
                };

                eNow.camera.getPicture(_onSuccess, _onError, _options);
            },
            //页面加载
            init: function () {
                $("#btnUpload").click(function () { iPage._upload(); });
                $("#btnCamera").click(function () { iPage._camera(); });
                $("#btnPhoto").click(function () { iPage._photo(); });
            }
        };

        //百度消息推送监听事件
        var _onBaidupushnotification = function (info) {
            var _s = JSON.stringify(info);
            console.log(_s);

            //            alert(_s);
        }

        //腾讯消息推送监听事件
        var _onTencentpushnotification = function (info) {
            var _s = JSON.stringify(info);
            console.log(_s);

            //            alert(_s);
        }

        //ready
        //window.eNow.ready({ fn: function () { iPage.init();}, onTencentpushnotification: _onTencentpushnotification, onBaidupushnotification: _onBaidupushnotification });
        
        ipop = {
            pop: $("#pop"),
            that: null
        };
        var initziliao = function () {
            $("#popMsg").find(".btn_yellow").click(function () {
                $("#popMsg").hide();
            });
            pcToobar.init({

                pID: "#<%=selProvince.ClientID%>",
                cID: "#<%=selCity.ClientID %>",
                comId: '',
                pSelect: "<%=ProvinceId %>",
                cSelect: "<%=CityId %>"

            });
            $("#<%=txtNickName.ClientID %>,#<%=txtEmail.ClientID %>").on("tap", function () {
                ipop.that = $(this);
                ipop.pop.find("span").text(ipop.that.closest("li").find("span").text());
                ipop.pop.find("input").val(ipop.that.val());
                ipop.pop.show();
            });
            ipop.pop.find(".btn_gray").on("tap", function () {
                ipop.pop.hide();
            });
            ipop.pop.find(".btn_yellow").on("tap", function () {
                ipop.that.val(ipop.pop.find("input").val());
                ipop.pop.hide();
            });
            $(".h_touxian").on("tap", function () {
                $("#poptouxiang").show();
            });
            $("#cancel").on("tap", function () {
                $("#poptouxiang").hide();
            });

            $("#edit").on("tap", function () {

                if ($.trim($("#<%=txtNickName.ClientID %>").val()) == "") {
                    ShowMsg("请填写用户昵称");
                    return false;
                }
                if ($.trim($("#<%=txtEmail.ClientID %>").val()) == "" || !$("#<%=txtEmail.ClientID %>").val().match(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/)) {
                    ShowMsg("请填写正确的邮箱地址");
                    return false;
                }
                $.ajax({
                    type: "post",
                    url: 'ziliao.aspx?dotype=save',
                    data: $("#form1").serialize(),
                    cache: false,
                    dataType: "json",
                    async: false,
                    success: function (response) {
                        ShowMsg(response.msg);
                    },
                    error: function () {
                        ShowMsg("请求异常");
                    }
                });
            });
            iPage.init();

            $(document).off("pageinit", initziliao);
        }
        $(document).on("pageinit", initziliao);
    </script>
</asp:Content>
