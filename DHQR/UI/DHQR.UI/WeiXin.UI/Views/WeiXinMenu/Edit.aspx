<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<List<WeiXinMenuModel>>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    自定义菜单编辑
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% var curMenu = (WeiXinMenuModel)ViewData["CurMenu"]; %>
        <div id="iframe_page">
        <%=Html.DropDownList("sourceSelect", ViewData["WeiXinSourceSelect"] as IEnumerable<SelectListItem>, new {style = "display:none" })%>
        <%=Html.DropDownList("triggerInfo", ViewData["TriggerInfo"] as IEnumerable<SelectListItem>, new { style = "display:none" })%>
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
					<li class=""><a href="../WeiXinApp/Index">微信授权配置</a></li>
					<li class=""><a href="../WeiXinApp/InterfaceCfg">微信接口配置</a></li>
					<li class=""><a href="../FirstIn/Index">首次关注设置</a></li>
					<li class="cur"><a href="../WeiXinMenu/Index">自定义菜单设置</a></li>
					<li class=""><a href="../KeyWord/Index">关键词回复</a></li>
                </ul>
            </div>
            <div id="wechat_menu" class="r_con_wrap">
                <div class="m_menu">
                    <div class="tips_info">
                        1. 您的公众平台帐号类型必须为<span>服务号</span>。<br />
                        2. 在微信公众平台申请接口使用的<span>AppId</span>和<span>AppSecret</span>，然后在【<a href="./?m=wechat&a=auth">微信授权配置</a>】中设置。<br />
                        3. 最多创建<span>3</span>个一级菜单，每个一级菜单下最多可以创建<span>5</span>个二级菜单，菜单最多支持两层。<br />
                        4. 拖动树形菜单可以对菜单重排序，但最终只有“<span>发布菜单</span>”后才会生效，公众平台限制了每天的发布次数，请勿频繁操作。<br />
                        5. 微信公众平台规定，<span>菜单发布24小时后生效</span>。您也可先取消关注，再重新关注即可马上看到菜单。<br />
                        6. 点击“<span>删除菜单</span>”操作只删除微信公众平台上的菜单，并不是删除本系统已经设置好的菜单。
                    </div>
                    <div class="form">
                        <div class="m_lefter">
                            <dl>
                                <% var topMenu = Model.Where(f => f.ParentId == null);
                                   foreach (var item in topMenu)
                                   {
                                       var childMenu = Model.Where(f => f.ParentId == item.Id);
                                %>
                                <dd mid="915">
                                    <div class="list ">
                                        <a title="修改" href="Edit?Id=<%:item.Id %>">
                                            <img src="../../DHQRImages/ico/mod.gif" align="absmiddle" alt="编辑" /></a> 
                                        <a title="删除" onclick="DeleteMenu('<%:item.Id %>');">
                                                <img src="../../DHQRImages/ico/del.gif" align="absmiddle" alt="删除" /></a>
                                        <%:item.Name%>
                                    </div>
                                    <ul>
                                        <% foreach (var weiXinMenuModel in childMenu)
                                           { %>
                                        <li mid="1059">
                                            <div class="title">
                                                <img src="../../DHQRImages/ico/jt.gif" />
                                                <%:weiXinMenuModel.Name%></div>
                                            <div class="opt">
                                                <a href="Edit?Id=<%:weiXinMenuModel.Id %>">
                                                    <img src="../../DHQRImages/ico/mod.gif" /></a><a onclick="DeleteMenu('<%:weiXinMenuModel.Id %>');">
                                                        <img src="../../DHQRImages/ico/del.gif" /></a>
                                            </div>
                                        </li>
                                        <% } %>
                                    </ul>
                                    <div class="blank9">
                                    </div>
                                </dd>
                                <%
                                   }
                                %>
                            </dl>
                            <div class="publish">
                                <input type="button" class="btn_green" name="publish_btn" value="发布菜单" onclick="PublishMenu();" /><input type="button"
                                    class="btn_gray" name="del_btn" value="删除菜单" /></div>
                        </div>
                        <div class="m_righter">
                            <form id="menu_form" method="POST">
                            <input type="hidden" name="Id" id="Id" value="<%=curMenu.Id %>" />
                            <h1>
                                添加菜单</h1>
                            <div class="opt_item">
                                <label>
                                    菜单名称：</label>
                                <span class="input">
                                    <input type="text" name="Name" value="<%:curMenu.Name%>" class="form_input" size="15" maxlength="15"
                                        notnull />
                                    <font class="fc_red">*</font></span>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="opt_item">
                                <label>
                                    添加到：</label>
                                <span class="input">
                                    <%=Html.DropDownListExtend("ParentId","ParentId",ViewData["TopTree"] as IEnumerable<SelectListItem>, "一级菜单", null)%>
                                </span>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="opt_item">
                                <label>
                                    序列：</label>
                                <span class="input">
                                    <input type="text" name="Sequence" value="<%=curMenu.Sequence%>" class="form_input" size="15" maxlength="15"
                                        notnull />
                                    <font class="fc_red">*</font></span>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="opt_item">
                                <label>
                                    消息类型：</label>
                                <span class="input">
                                    <%=Html.DropDownListExtend("MenuType", "MenuType", ViewData["SelType"] as IEnumerable<SelectListItem>, null, null)%>
                                </span>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="opt_item" id="text_msg_row">
                                <label>
                                    文字内容：</label>
                                <span class="input">
                                    <textarea name="ContenInfo"><%:curMenu.ContenInfo%></textarea></span>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="opt_item" id="img_msg_row">
                                <label>
                                    图文内容：</label>
                                <span class="input">
                                    <select name='MaterialId' id="MaterialId">                                      
                                    </select><a href="./?m=material">素材管理</a></span>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="opt_item" id="url_msg_row">
                                <label>
                                    链接网址：</label>
                                <span class="input">
                                    <input type="text" id="Url" name="Url" value="<%:curMenu.Url%>" class="form_input"
                                        size="35" maxlength="200" /></span>
                                <div class="clear">
                                </div>
                            </div>
                            <div class="opt_item">
                                <label>
                                </label>
                                <span class="input">
                                    <input type="button" onclick="EditMenu();" class="btn_green btn_w_120" name="submit_button"
                                        value="修改菜单" /></span>
                                <div class="clear">
                                </div>
                            </div>
                            <input type="hidden" id="Type" name="Type" value="<%:curMenu.Type%>"/>
                            <input type="hidden" id="Key" name="Key" value="<%:curMenu.Key%>"/>
                            <input type="hidden" id="WeiXinSourceId" name="WeiXinSourceId" value="<%:curMenu.WeiXinSourceId%>"/>
                            <input type="hidden" id="WeiXinAppId" name="WeiXinAppId" value="<%:curMenu.WeiXinAppId%>"/>
                            <input type="hidden" id="PicMsgOrTirggerInfoId" name="PicMsgOrTirggerInfoId" value="<%:curMenu.PicMsgOrTirggerInfoId%>"/>
                            </form>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
  <link href='../../DHQRCss/wechat.css' rel='stylesheet' type='text/css' />
  <script type='text/javascript' src='../../DHQRJs/wechat.js'></script>
  <script type='text/javascript' src='../../DHQRJs/dragsort-0.5.1.min.js'></script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
        <script type="text/javascript">
            $(document).ready(wechat_obj.menu_init);
            function DeleteMenu(id) {
                if (!confirm('删除后不可恢复，继续吗？')) {
                    return false;
                };
                $.post('DeleteMenuId', { id: id }, function (data) {
                    if (data.IsSuccessful) {
                        location.reload();
                    } else {
                        alert(data.OperateMsg);
                    }
                });
                return false;
            }
            function EditMenu() {
                var tp = $("#MenuType").val();
                $("#Type").val(tp);
                var pid = $("#MaterialId").find("option:selected").val();
                $("#PicMsgOrTirggerInfoId").val(pid);
                var data = $('#menu_form').serialize();

                $.post('../../WeiXinMenu/AdMenu', data, function (info) {
                    if (info.IsSuccessful) {
                        location.reload();
                    } else {
                        alert(info.OperateMsg);
                    }
                });
            }
            //发布菜单
            function PublishMenu() {
                if (!confirm('确认发布菜单')) {
                    return false;
                };
                $.post('../../WeiXinMenu/PublishMenu', "", function (info) {
                    if (info.IsSuccessful) {
                        alert(info.OperateMsg);
                        location.reload();
                    } else {
                        alert(info.OperateMsg);
                    }
                });

            }
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
                for (var i = 0; i < materialSelect[0].length; i++) {
                    if (materialSelect[0][i].value == PicMsgOrTirggerInfoId) {
                        materialSelect[0][i].selected = true;
                        break;
                    }
                }

                var type = $("#Type").val(), typeSelect = $('select[name=MenuType]');
                for (var i = 0; i < typeSelect[0].length; i++) {
                    if (typeSelect[0][i].value == type) {
                        typeSelect[0][i].selected = true;
                        break;
                    }
                }
                $("#iframe_page").remove("#sourceSelect");
                $("#iframe_page").remove("#triggerInfo");
                wechat_obj.menu_init();
            });
    </script>
</asp:Content>
