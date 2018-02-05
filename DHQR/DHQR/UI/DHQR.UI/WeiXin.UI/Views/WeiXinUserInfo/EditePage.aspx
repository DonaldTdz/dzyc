<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<WeiXinUserInfoModel>" %>

<%@ Import Namespace="DHQR.UI.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EditOrAdd
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
					<li class=""><a href="../WeiXinUserGroup/Index">用户分组管理</a></li>
					<li class="cur"><a href="../WeiXinUserInfo/Index">微信用户管理</a></li>
                </ul>
            </div>
            <div id="reply_keyword" class="r_con_wrap">
                <form id="keyword_reply_form" class="r_con_form">
                <%= Html.Hidden("Id", Model.Id)%>
                <%= Html.Hidden("openid", Model.openid)%>
                <div class="rows">
                    <label>
                         头像</label>
                    <span class="input">
                        <a href="<%=Model.headimgurl %>" target="_blank"><img src="<%=Model.headimgurl%>" align="absmiddle" width="48px"; height="48px" alt=""></a>
                    </span>
                    <div class="clear">
                    </div>
                </div>
                <div class="rows">
                    <label>
                         昵称</label>
                    <span class="input">
                        <%=Model.nickname %>
                    </span>
                    <div class="clear">
                    </div>
                </div>
                <div class="rows">
                    <label>
                         性别</label>
                    <span class="input">
                        <%if (Model.sex==1)
                          {%>
                            男
                        <% }
                          else
                          {%>
                             女
                        <%} %>
                    </span>
                    <div class="clear">
                    </div>
                </div>
                <div class="rows">
                    <label>
                         国家</label>
                    <span class="input">
                       <%=Model.country %>
                    </span>
                    <div class="clear">
                    </div>
                </div>
                <div class="rows">
                    <label>
                         省份</label>
                    <span class="input">
                        <%=Model.province %>
                    </span>
                    <div class="clear">
                    </div>
                </div>
                <div class="rows">
                    <label>
                         城市</label>
                    <span class="input">
                        <%=Model.city %>
                    </span>
                    <div class="clear">
                    </div>
                </div>

                <div class="rows">
                    <label>
                         备注</label>
                    <span class="input">                 
                        <input name="remark" id="remark"  value="<%=Model.remark %>"/>                  
                    </span>
                    <div class="clear">
                    </div>
                </div>

                <div class="rows">
                    <label>
                         分组</label>
                    <span class="input">                      
                        <%=Html.DropDownListFor(f=>f.groupid,(IEnumerable<SelectListItem>)ViewData["groups"])%>                 
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
                var action = "UpdateUser";
                
                var openid = $("#openid").attr("value"), remark = $("#remark").attr("value"),
                    groupid = $('select[id=groupid]').find("option:selected").val();
                var params = {
                    openid: openid, remark: remark, groupid: groupid
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
