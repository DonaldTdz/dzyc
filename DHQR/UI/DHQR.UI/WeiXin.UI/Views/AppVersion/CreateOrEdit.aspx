<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<DHQR.UI.Models.AppVersionModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    编辑
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
                </ul>
            </div>
            <div id="reply_keyword" class="r_con_wrap">
                <form id="material_form" class="r_con_form" action="../AppVersion/UpLoadImage">
                    <%=Html.HiddenFor(f=>f.Id) %>
                   
                    <div class="rows">
                        <label>包名</label><span class="input"> <%=Html.TextBoxFor(f => f.ApkPacket, new { @Class="form_input",@Size="35",@notnull="notnull"})%><font class="fc_red">*</font></span>
                        <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label>APK名称</label><span class="input"><%=Html.TextBoxFor(f=>f.ApkName, new { @Class="form_input",@Size="35",@notnull="notnull"}) %><font class="fc_red">*</font></span>
                        <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label>版本号</label><span class="input"> <%=Html.TextBoxFor(f=>f.VersionCode, new { @Class="form_input",@Size="35",@notnull="notnull"}) %><font class="fc_red">*</font></span>
                        <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label>版本描述</label><span class="input"> <%=Html.TextBoxFor(f=>f.VersionName, new { @Class="form_input",@Size="35"}) %></span>
                        <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label>APK地址</label><span class="input"> <%=Html.TextBoxFor(f=>f.Url, new { @Class="form_input",@Size="35",@notnull="notnull",@readonly="readonly"}) %><font class="fc_red">*</font></span>
                        <div class="clear"></div>
                    </div>
                     <div class="rows">
                      <label>APK上传</label>
                        <div>
                          <span class="input"><input name="FileUpload" id="MsgFileUpload" type="file" />
                           <input class="btn_ok" type="submit" id="upload" value="" style="cursor: pointer; background-position: 0px 0px; height: 27px; width: 118px; background-image: url(../../DHQRImages/uploadFile.jpg)" />
                              </span>
                       </div>
                         <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label>是否有效</label><span class="input"> <%=Html.CheckBoxFor(f => f.IsValid)%></span>
                        <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label>
                        </label>
                        <span class="input">
                            <input type="button" class="btn_green" id="save" name="submit_button" style="cursor:pointer;" value="提交保存" /><a href="Index"
                                class="btn_gray">返回</a></span>
                        <div class="clear">
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
    <link href="../../DHQRCss/global.css" rel="stylesheet" type="text/css" />
    <link href="../../DHQRCss/main.css" rel="stylesheet" type="text/css" />
    <link href="../../DHQRCss/wechat.css" rel="stylesheet" type="text/css" />
    <link href="../../DHQRCss/operamasks-ui.css" rel="stylesheet" />
    <script src="../../DHQRJs/operamasks-ui.js" type="text/javascript"></script>
    <script src="../../DHQRJs/frame.js" type="text/javascript"></script>
    <script src="../../DHQRJs/global.js" type="text/javascript"></script>
    <script src="../../DHQRJs/wechat.js" type="text/javascript"></script>

    <link href="../../DHQRCss/frame.css" rel='stylesheet' type='text/css' />
    <script type='text/javascript' src="../../DHQRJs/jquery-1.7.2.min.js"></script>
    <link href="../../DHQRCss/material.css" rel='stylesheet' type='text/css' />
    <script type='text/javascript' src="../../DHQRJs/material.js"></script>
    <link href="../../DHQRCss/operamasks-ui.css" rel='stylesheet' type='text/css' />
    <script type='text/javascript' src="../../DHQRJs/operamasks-ui.min.js"></script>
    <script type="text/javascript" src="../../DHQRJs/jquery.form.js"></script>
    <style type="text/css">
        body, html {
            background: url(../../DHQRImages/main/main-bg.jpg) left top fixed no-repeat;
        }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">

    <script type="text/javascript">
        var emptyGuid = '<%=Guid.Empty%>';
        $(document).ready(function () {
            //frame_obj.search_form_init();
            wechat_obj.wechat_set();
            $("#save").click(function () {
                if (system_obj.check_form($('*[notnull]'))) {
                    return false
                };
                var postData = {
                    Id: $("#Id").val(), ApkPacket: $("#ApkPacket").val(), ApkName: $("#ApkName").val(), Url: $("#Url").val(),
                    IsValid: $("#IsValid")[0].checked, VersionCode: $("#VersionCode").val(), VersionName: $("#VersionName").val()
                }
                var url = postData.Id == emptyGuid ? "../AppVersion/Add" : "../AppVersion/Edit";
                var btn = $(this);
                btn.attr('disabled', true);
                $.post(url, postData, function (result) {
                    if (result.IsSuccessful == true) {
                        window.location = '../AppVersion/Index';
                    } else {
                        alert(result.OperateMsg);
                        btn.attr('disabled', false);
                    }
                });
            });
        });


        $(function () {

            $("#material_form").submit(function () {
                $(this).ajaxSubmit({
                    target: '#outputdiv',
                    type: "post",  //提交方式  
                    dataType: "json", //数据类型 
                    url: "../AppVersion/UpLoadImage",
                    beforeSubmit: function (formData, jqForm, options) {
                        if (!$("#MsgFileUpload").val()) {
                            //alert("提示", "请选择上传图片！", "error");
                            return false;
                        }
                        return true;
                    },
                    error: function (result) {
                        //BLUE.MsgAlert("提示", result.Message, "error");
                    },
                    success: function (responseText, statusText) {
                        $("#Url").val(responseText);
                    }
                });
                return false;
            });
        });
    </script>
</asp:Content>
