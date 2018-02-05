<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<WeiXinUserGroupModel>" %>

<%@ Import Namespace="DHQR.UI.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EditOrAdd
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
					<li class="cur"><a href="../WeiXinUserGroup/Index">用户分组管理</a></li>
					<li class=""><a href="../WeiXinUserInfo/Index">微信用户管理</a></li>
                </ul>
            </div>
            <div id="reply_keyword" class="r_con_wrap">
                <form id="keyword_reply_form" class="r_con_form">
                <%if (Model != null)
                  {%>
                <%= Html.Hidden("Id", Model.Id)%>
                <% }
                  else
                  {%>
                <%= Html.Hidden("Id", "")%>
                <%} %>
                <%if (Model != null)
                  {%>
                <%= Html.Hidden("count", Model.count)%>
                <% }
                  else
                  {%>
                <%= Html.Hidden("count", 0)%>
                <%} %>
                <div class="rows">
                    <label>
                         分组ID</label>
                    <span class="input">
                        <%if (Model != null)
                          {%>
                        <input type="text" name="groupid" id="groupid" value="<%=Model.groupid %>"/>
                        <% }
                          else
                          {%>
                        <input type="text" name="groupid" id="groupid"  value=""/>
                        <%} %>
                    </span>
                    <div class="clear">
                    </div>
                </div>

                <div class="rows">
                    <label>
                         分组名称</label>
                    <span class="input">
                        <%if (Model != null)
                          {%>
                        <input  name="name" id="name" notnull="" value="<%=Model.name %>"/>
                        <% }
                          else
                          {%>
                        <input  name="name" id="name" notnull="" value=""/>
                        <%} %>
                    </span>
                    <div class="clear">
                    </div>
                </div>

                <div class="rows">
                    <label>
                         人数</label>
                    <span class="input">
                        <%if (Model != null)
                          {%>
                        <%=Model.count %>
                        <% }
                          else
                          {%>
                         0
                        <%} %>
                    </span>
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
        //初始化资源select
        $(function () {
            $('#keyword_reply_form').submit(function () { return false; });
            $('#keyword_reply_form input:submit').click(function () {

                if (global_obj.check_form($('*[notnull]'))) { return false };
                var action = "Create";
                if ($("#Id").val() != "") {
                    action = "Edit";
                }
                var Id = $("#Id").attr("value"), groupid = $("#groupid").attr("value"), name = $("#name").attr("value"),
                                count = $("#count").attr("value");
                var params = {
                    Id: Id, groupid: groupid, name: name, count: count
                };

                $(this).attr('disabled', true);
                $.post(action, params, function (data) {
                    if (data.IsSuccessful == true) {
                        window.location = 'Index';
                    } else {
                        alert(data.OperateMsg);
                        $('#keyword_reply_form input:submit').attr('disabled', false);
                    }
                }, 'json');
            })

        });
    </script>
</asp:Content>
