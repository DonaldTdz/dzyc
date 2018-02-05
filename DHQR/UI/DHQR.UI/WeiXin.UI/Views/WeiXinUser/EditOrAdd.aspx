<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<WeiXinUserModel>" %>

<%@ Import Namespace="DHQR.UI.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EditOrAdd
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
					<li class="cur"><a href="../WeiXinUser/Index">微信用户管理</a></li>
					<li class=""><a href="../WeiXinUserType/Index">用户类型管理</a></li>
					<li class=""><a href="../WeiXinUserType/TypeToModule">用户权限管理</a></li>
					<li class=""><a href="../WeiXinUser/WeiXinUser">账号绑定管理</a></li>
                </ul>
            </div>
            <div id="reply_keyword" class="r_con_wrap">
                <form id="keyword_reply_form" class="r_con_form">
                <%if (Model != null)
                  {%>
                <%= Html.Hidden("Id", Model.Id)%>
                <%= Html.Hidden("LocationX", Model.LocationX)%>
                <%= Html.Hidden("LocationY", Model.LocationY)%>
                <%= Html.Hidden("WeiXinUserTypeId", Model.WeiXinUserTypeId)%>
                <%= Html.Hidden("WeiXinAppId", Model.WeiXinAppId)%>
                <% }
                  else
                  {%>
                <%= Html.Hidden("Id", "")%>
                <%= Html.Hidden("LocationX","")%>
                <%= Html.Hidden("LocationY","")%>
                <%= Html.Hidden("WeiXinUserTypeId","")%>
                <%= Html.Hidden("WeiXinAppId","")%>
                <%} %>
                <div class="rows">
                    <label>
                         登录名</label>
                    <span class="input">
                        <%if (Model != null)
                          {%>
                        <input name="SysName" id="SysName" notnull="" value="<%=Model.SysName %>"/>
                        <% }
                          else
                          {%>
                        <input name="SysName" id="SysName" notnull="" value=""/>
                        <%} %>
                    </span>
                    <div class="clear">
                    </div>
                </div>

                <div class="rows">
                    <label>
                         密码</label>
                    <span class="input">
                        <%if (Model != null)
                          {%>
                        <input type="password" name="SysPsw" id="SysPsw" notnull="" value="<%=Model.SysPsw %>"/>
                        <% }
                          else
                          {%>
                        <input type="password" name="SysPsw" id="SysPsw" notnull="" value=""/>
                        <%} %>
                    </span>
                    <div class="clear">
                    </div>
                </div>

                <div class="rows">
                    <label>
                         姓名</label>
                    <span class="input">
                        <%if (Model != null)
                          {%>
                        <input name="Name" id="Name" notnull="" value="<%=Model.Name %>"/>
                        <% }
                          else
                          {%>
                        <input name="Name" id="Name" notnull="" value=""/>
                        <%} %>
                    </span>
                    <div class="clear">
                    </div>
                </div>

                <div class="rows">
                    <label>
                         电话</label>
                    <span class="input">
                        <%if (Model != null)
                          {%>
                        <input name="Tel" id="Tel" notnull="" value="<%=Model.Tel %>"/>
                        <% }
                          else
                          {%>
                        <input name="Tel" id="Tel" notnull="" value=""/>
                        <%} %>
                    </span>
                    <div class="clear">
                    </div>
                </div>

                <div class="rows">
                    <label>
                         地址</label>
                    <span class="input">
                        <%if (Model != null)
                          {%>
                        <input name="Address" id="Address" notnull="" value="<%=Model.Address %>"/>
                        <% }
                          else
                          {%>
                        <input name="Address" id="Address" notnull="" value=""/>
                        <%} %>
                    </span>
                    <div class="clear">
                    </div>
                </div>

                <div class="rows">
                    <label>
                         用户OpenId</label>
                    <span class="input">
                        <%if (Model != null)
                          {%>
                        <input name="WxUserName" id="WxUserName" value="<%=Model.WxUserName %>"/>
                        <% }
                          else
                          {%>
                        <input name="WxUserName" id="WxUserName" value=""/>
                        <%} %>
                    </span>
                    <div class="clear">
                    </div>
                </div>

                
                <div class="rows">
                    <label>
                        用户类型</label>
                    <span class="input">
                        <select name="UserType">
                            <% if(Model==null || (Model!=null && Model.UserType==0)) { %>
                            <option value="0" selected="selected">零售户</option>
                            <option value="1">内部员工</option>
                            <%} else{ %>
                            <option value="0">零售户</option>
                            <option value="1"  selected="selected">内部员工</option>                        
                            <%} %>
                        </select></span>
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
                var Id = $("#Id").attr("value"), Name = $("#Name").attr("value"), SysName = $("#SysName").attr("value"),
                                SysPsw = $("#SysPsw").attr("value"), UserType = $('select[name=UserType]').find("option:selected").val(),
                                Tel = $("#Tel").attr("value"), Address = $("#Address").attr("value"), WxUserName = $("#WxUserName").attr("value"),
                                LocationX = $("#LocationX").attr("value"), LocationY = $("#LocationY").attr("value"),
                                WeiXinUserTypeId = $("#WeiXinUserTypeId").attr("value"), WeiXinAppId = $("#WeiXinAppId").attr("value");
                var params = {
                    Id: Id, UserType: UserType, SysName: SysName, SysPsw: SysPsw, Name: Name, Tel: Tel, Address: Address, WxUserName: WxUserName,
                    LocationX: LocationX, LocationY: LocationY, WeiXinUserTypeId: WeiXinUserTypeId, WeiXinAppId: WeiXinAppId
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
