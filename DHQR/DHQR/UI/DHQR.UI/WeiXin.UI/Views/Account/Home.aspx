<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    首页
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="uiCenter" class="ui-layout-center">
      <%--<div id="bgimg" style="display:block;">
       <img src="../../Content/img/bg.jpg" alt=""/></div>--%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
    <link href="../../Content/Common/fromCommon.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/font-awesome/css/font-awesome.css" rel="Stylesheet" />
    <link href="../../Content/font-awesome/css/font.css" rel="Stylesheet" />
    <style type="text/css">
        #uiCenter
        {
            background-image:url(../../Content/img/bg.jpg);
            background-position:50% 50%;
            background-repeat:no-repeat;
            }
        div.widget-title
        {
            background-color: rgb(215, 235, 249);
            background-image: url(../../Content/JqueryUi/images/ui-bg_glass_80_d7ebf9_1x400.png);
            background-position:50% 50%;
            color: #fff;
            border-color: #e78f08;
        }
        div.widget-title h5
        {
            color: #fff;
        }
        div.widget-title span
        {
            color: #fff;
        }
        .content-box-date
        {
            position:absolute;
            top:10px;
            right:15px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('body').layout();
            $('#container').layout();
            var height = $("#container").height();
            $.post("GetRolesByUser", null, function (data) {
                var hasContent = false;
                $.each(data, function (key, val) {
                    switch (val) {
                        case "SuperAdmin":
                            $.post("GetUnDoUserApply", null, function (result) {
                                CreateBox(result, "用户申请", height, "../../User/Apply", "个人信息/用户申请").appendTo($("#uiCenter"));
                            });
                            $.post("GetUnDoResetPassWordApply", null, function (result) {
                                CreateBox(result, "重置密码申请", height, "../../User/ResetPswAppove", "个人信息/重置密码申请").appendTo($("#uiCenter"));
                            });
                            hasContent = true;
                            break;
                        case "Admin":
                            $.post("GetUnDoUserApply", null, function (result) {
                                CreateBox(result, "用户申请", height, "../../User/Apply", "个人信息/用户申请").appendTo($("#uiCenter"));
                            });
                            $.post("GetUnDoResetPassWordApply", null, function (result) {
                                CreateBox(result, "重置密码申请", height, "../../User/ResetPswAppove", "个人信息/重置密码申请").appendTo($("#uiCenter"));
                            });
                            $.post("GetUnDoWebKeyApply", null, function (result) {
                                CreateBox(result, "微博关键字申请", height, "../../WeiboKeyWord/Apply", "个人信息/微博关键字申请").appendTo($("#uiCenter"));
                            });
                            hasContent = true;
                            break;
                        case "Leader":
                            $.post("../ProjectMasters/GetMasterInfo", null, function (result) {
                                CreateBox(result, "项目", height).appendTo($("#uiCenter"));
                            });
                            hasContent = true;
                            break;
//                        case "XMJL":
//                            $.post("../ProjectMasters/GetMasterInfo", null, function (result) {
//                                CreateBox(result, "项目", height).appendTo($("#uiCenter"));
//                            });
//                            $.post("GetUnDoWebKeyApply", null, function (result) {
//                                CreateBox(result, "微博关键字申请", height, "../../WeiboKeyWord/Apply", "个人信息/微博关键字申请").appendTo($("#uiCenter"));
//                            });
//                            hasContent = true;
//                            break;
                        case "GCCY":
                            $.post("../ProjectMasters/GetMasterInfo", null, function (result) {
                                CreateBox(result, "项目", height).appendTo($("#uiCenter"));
                            });
                            hasContent = true;
                            break;

                    }
                });
                if (!hasContent) {
                    var width = $("#uiCenter").width();
                    $('#bgimg').css({ "display": "block", height: height, width: width });
                    $("#bgimg>img").css({ height: height, width: width });
                }
            });




        });


        var timeout = null;
        $(window).bind("resize", function () {
            if (timeout) {
                clearTimeout(timeout);
            }
            timeout = setTimeout(resizeGrid, 300);
        });

        function resizeGrid() {
            var width = $("#uiCenter").width();
            var height = $("#uiCenter").height();
            $('#bgimg').css({  height: height, width: width });
            $("#bgimg>img").css({ height: height, width: width });
        }


        function CreateBox(data, title, height, url, urlName) {
            var divBox = $('<div class="widget-box" style="width:47%;height:' + (height / 2 - 12) + 'px;margin-left:20px;margin-top:0px;margin-bottom:5px; clear:none;float:left;"></div>');
            var divTitle = $('<div class="widget-title"></div>');
            var divTitleSpan = $('<span class="icon"><i class="icon-picture" style="color:#333;display:inline-block;margin-top:3px;"></i></span>');
            var divTitleH5 = $('<h5 style="color:#333">' + title + '</h5>');
            divTitleSpan.appendTo(divTitle);
            divTitleH5.appendTo(divTitle);
            divTitle.appendTo(divBox);

            var divContent = $('<div class="widget-content" style="padding:0px 0px 15px 0px;height:' + (height / 2 - 75) + 'px"></div>');
            divContent.appendTo(divBox);
            var ulContent = $('<ul style="padding-left:20px;width:92%;height:' + (height / 2 - 75)+ 'px;overflow:auto;"></ul>');
            ulContent.appendTo(divContent);
            $.each(data, function (key, val) {
                var liContent = $('<li class="content-box" style="width:95%;position:relative;"></li>');
                var divLiContent = undefined;
                var divLiContentDate = $('<div class="content-box-date"><span>' + val.date + '</span></div>');
                //divLiContent.appendTo(liContent);
                if (url) {
                    divLiContent = $('<div style="margin-left:10px;"><span>' + val.name + '：</span><span>' + val.text + '</span></div>');
                    divLiContent.appendTo(liContent);
                    var link = $('<div style="display:inline-block;cursor:pointer;margin-left:20px;"><i class="icon icon-share-alt"></i></div>');
                    link.appendTo(divLiContent);
                    divLiContent.click(function () {
                        window.parent.setHome(url, urlName);
                    }).css("cursor", "pointer");
                    divLiContentDate.appendTo(liContent);
                } else {

                    divLiContent = $('<div><span>' + val.name + '</span></div>');
                    divLiContent.appendTo(liContent);
                    divLiContentDate.appendTo(liContent);
                    var divPoint = $('<div style="border-top:1px solid #dadada;"><span>' + val.text + '</span></div>');
                    divPoint.appendTo(liContent);
                    var divManager = $('<div><span>' + val.manager + '</span></div>');
                    divManager.appendTo(liContent);
                }




                if (!isNaN(Number(val.progress))) {
                    var progressDiv = $('<div style="padding-top:5px; width:100%;"><div style="float:left;"><b>进度：</b></div><div class="progress"><div class="bar" style="width:' + val.progress + '%;"><span>' + val.progress + '%</span></div></div></div>');
                    progressDiv.appendTo(liContent);
                    liContent.addClass("content-box-progress");
                }
                liContent.appendTo(ulContent);
            });
            return divBox;
        }
    </script>
</asp:Content>
