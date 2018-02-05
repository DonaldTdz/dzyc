<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<DHQR.UI.Models.GlobalConfigurationModel>" %>

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
                <form id="keyword_reply_form" class="r_con_form" action="">
                    <%=Html.HiddenFor(f=>f.Id) %>
                    <div class="rows">
                        <label>用途</label><span class="input"> <%=Html.TextBoxFor(f => f.Name, new { @Class="form_input",@Size="35",@notnull="notnull"})%><font class="fc_red">*</font></span>
                        <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label>键</label><span class="input"><%=Html.TextBoxFor(f=>f.Key, new { @Class="form_input",@Size="35"}) %></span>
                        <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label>值</label><span class="input"><%=Html.TextBoxFor(f=>f.Value, new { @Class="form_input",@Size="35"}) %></span>
                        <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label>类型</label><span class="input"> <%=Html.DropDownListFor(f => f.Type, (IList<SelectListItem>)ViewData["DataTypes"], new  {@Style="width:20   0px;" })%></span>
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
                    Id: $("#Id").val(), Name: $("#Name").val(), Type: $("#Type").val(), Key: $("#Key").val(), Value: $("#Value").val()
                }
                var url = postData.Id == emptyGuid ? "../GlobalConfiguration/Add" : "../GlobalConfiguration/Edit";
                var btn = $(this);
                btn.attr('disabled', true);
                $.post(url, postData, function (result) {
                    if (result.IsSuccessful == true) {
                        window.location = '../GlobalConfiguration/Index';
                    } else {
                        alert(result.OperateMsg);
                        btn.attr('disabled', false);
                    }
                });
            });
        });

    </script>
</asp:Content>
