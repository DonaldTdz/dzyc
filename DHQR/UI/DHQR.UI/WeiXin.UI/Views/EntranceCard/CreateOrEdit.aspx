<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<DHQR.UI.Models.EntranceCardModel>" %>

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
                   <input type="hidden" id="CreateTime" value="<%=Model.CreateTime.ToString("yyyy-MM-dd HH:mm:ss")%>"/>
                   <%=Html.HiddenFor(f=>f.DptName) %>
                    <div class="rows">
                        <label>全球唯一码</label><span class="input"> <%=Html.TextBoxFor(f => f.GlobalCode, new { @Class="form_input",@Size="35",@notnull="notnull"})%><font class="fc_red">*</font></span>
                        <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label>卡号</label><span class="input"><%=Html.TextBoxFor(f=>f.CardNum, new { @Class="form_input",@Size="35"}) %></span>
                        <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label>送货部</label><span class="input"> <%=Html.DropDownListFor(f => f.DptId, (IList<SelectListItem>)ViewData["dptList"], new  {@Style="width:20   0px;" })%></span>
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
                    Id: $("#Id").val(), CardNum: $("#CardNum").val(), DptName: $("#DptName").val(), GlobalCode: $("#GlobalCode").val(), DptId: $("#DptId").val(),
                    IsValid: $("#IsValid")[0].checked, DptName: $("#DptId").find("option:selected").text(),CreateTime:$("#CreateTime").val()
                }
                var url = postData.Id == emptyGuid ? "../EntranceCard/Add" : "../EntranceCard/Edit";
                var btn = $(this);
                btn.attr('disabled', true);
                $.post(url, postData, function (result) {
                    if (result.IsSuccessful == true) {
                        window.location = '../EntranceCard/Index';
                    } else {
                        alert(result.OperateMsg);
                        btn.attr('disabled', false);
                    }
                });
            });
        });

    </script>
</asp:Content>
