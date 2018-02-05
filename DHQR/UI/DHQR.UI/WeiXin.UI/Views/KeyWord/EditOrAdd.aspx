<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<WeiXinKeyWordModel>" %>

<%@ Import Namespace="DHQR.UI.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EditOrAdd
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="iframe_page">
        <%=Html.DropDownList("sourceSelect", ViewData["WeiXinSourceSelect"] as IEnumerable<SelectListItem>, new {style = "display:none" })%>
        <%=Html.DropDownList("triggerInfo", ViewData["TriggerInfo"] as IEnumerable<SelectListItem>, new { style = "display:none" })%>
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
					<li class=""><a href="../WeiXinApp/Index">微信授权配置</a></li>
					<li class=""><a href="../WeiXinApp/InterfaceCfg">微信接口配置</a></li>
					<li class=""><a href="../FirstIn/Index">首次关注设置</a></li>
					<li class=""><a href="../WeiXinMenu/Index">自定义菜单设置</a></li>
					<li class="cur"><a href="../KeyWord/Index">关键词回复</a></li>
					<li class=""><a href="./?m=wechat&a=spread">零售户信息维护</a></li>
                </ul>
            </div>
            <div id="reply_keyword" class="r_con_wrap">
                <form id="keyword_reply_form" class="r_con_form">
                <%if (Model != null)
                  {%>
                <%= Html.Hidden("Id", Model.Id)%>
                <%= Html.Hidden("PicMsgOrTirggerInfoId", Model.PicMsgOrTirggerInfoId)%>
                <%= Html.Hidden("Type", Model.Type)%>
                <%= Html.Hidden("WeiXinAppId", Model.WeiXinAppId)%>
                <% }
                  else
                  {%>
                <%= Html.Hidden("Id", "")%>
                <%= Html.Hidden("PicMsgOrTirggerInfoId","")%>
                <%= Html.Hidden("Type","")%>
                <%= Html.Hidden("WeiXinAppId","")%>
                <%} %>
                <div class="rows">
                    <label>
                        关键词</label>
                    <span class="input">
                        <%if (Model != null)
                          {%>
                        <textarea name="Keywords" class="keywords" notnull="" value="<%=Model.KeyWord %>"><%=Model.KeyWord %></textarea>
                        <% }
                          else
                          {%>
                        <textarea name="Keywords" class="keywords" notnull="" value=" "></textarea>
                        <%} %>
                        <div class="tips">
                            多个关键词，请每行填写一个</div>
                    </span>
                    <div class="clear">
                    </div>
                </div>
                <div class="rows">
                    <label>
                        匹配模式</label>
                    <span class="input">
                        <%if (Model != null)
                          {%>
                        <%=Html.RadioButton("PatternMethod", KeyWordPatternMethod.Exact, Model.PatternMethod == KeyWordPatternMethod.Exact)%>
                        精确匹配<span class="tips">（用户输入的文字和此关键词一样才会触发）</span><br />
                        <%=Html.RadioButton("PatternMethod", KeyWordPatternMethod.Fuzzy, Model.PatternMethod == KeyWordPatternMethod.Fuzzy)%>
                        模糊匹配<span class="tips">（只要用户输入的文字包含此关键词就触）</span><br />
                    </span>
                    <% }
                          else
                          {%>
                    <input type="radio" name="PatternMethod" value="<%=KeyWordPatternMethod.Exact %>"
                        checked="" />精确匹配<span class="tips">（用户输入的文字和此关键词一样才会触发）</span><br />
                    <input type="radio" name="PatternMethod" value="<%=KeyWordPatternMethod.Fuzzy %>" />模糊匹配<span
                        class="tips">（只要用户输入的文字包含此关键词就触）</span><br />
                    </span>
                    <%} %>
                    <div class="clear">
                    </div>
                </div>
                <div class="rows">
                    <label>
                        回复类型</label>
                    <span class="input">
                        <select name="ReplyMsgType">
                            <option value="<%=KeyWordType.Text %>" selected="selected">文字消息</option>
                            <option value="<%=KeyWordType.Image %>">图文消息</option>
                        </select></span>
                    <div class="clear">
                    </div>
                </div>
                <div class="rows" id="text_msg_row">
                    <label>
                        回复内容</label>
                    <span class="input">
                        <%if (Model != null)
                          {%>
                        <textarea name="TextContents" value="<%=Model.ContenInfo %>"><%=Model.ContenInfo%></textarea>
                        <% }
                          else
                          {%>
                        <textarea name="TextContents" value=""></textarea>
                        <%} %>
                    </span>
                    <div class="clear">
                    </div>
                </div>
                <div class="rows" id="img_msg_row" style="display: none;">
                    <label>
                        回复内容</label>
                    <span class="input">
                        <select name="MaterialId">
                        </select><a href="./?m=material" class="material">图文消息管理</a></span>
                    <div class="clear">
                    </div>
                </div>
                <div class="rows">
                    <label>
                    </label>
                    <span class="input">
                        <input type="submit" class="btn_green" name="submit_button" value="提交保存"/><a href="Index"
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
    <script src="../../DHQRJs/global.js" type="text/javascript"></script>
    <script src="../../DHQRJs/wechat.js" type="text/javascript"></script>
    <style type="text/css">
        body, html
        {
            background: url(../../DHQRImages/main/main-bg.jpg) left top fixed no-repeat;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
    <script type="text/javascript">
        //$(document).ready(wechat_obj.reply_keyword_init);

        //初始化资源select
        $(function () {
            var materialSelect = $("#img_msg_row select");
            var sourceSelect = $("#sourceSelect");
            var triggerInfoSelect = $("#triggerInfo");
            materialSelect.empty();
            materialSelect.append(' <option value="">--请选择--</option>');
            materialSelect.append('<optgroup label="---------------系统业务模块---------------"></optgroup>');
            materialSelect.append(triggerInfoSelect.html());
            materialSelect.append('<optgroup label="---------------自定义图文消息---------------"></optgroup>');
            materialSelect.append(sourceSelect.html());
            var PicMsgOrTirggerInfoId = $("#PicMsgOrTirggerInfoId").val();
            //            if (PicMsgOrTirggerInfoId != "") {
            for (var i = 0; i < materialSelect[0].length; i++) {
                if (materialSelect[0][i].value == PicMsgOrTirggerInfoId) {
                    materialSelect[0][i].selected = true;
                }
            }
            //            }

            var type = $("#Type").val(), typeSelect = $('select[name=ReplyMsgType]');
            for (var i = 0; i < typeSelect[0].length; i++) {
                if (typeSelect[0][i].value == type) {
                    typeSelect[0][i].selected = true;
                }
            }
            $("#iframe_page").remove("#sourceSelect");
            $("#iframe_page").remove("#triggerInfo");

            wechat_obj.reply_keyword_init();
        });
    </script>
</asp:Content>
