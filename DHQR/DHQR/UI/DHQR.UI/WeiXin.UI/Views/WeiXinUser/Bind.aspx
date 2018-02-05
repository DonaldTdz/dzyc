<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WeiXin.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    微信用户
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
					<li class=""><a href="../WeiXinUser/Index">微信用户管理</a></li>
					<li class=""><a href="../WeiXinUserType/Index">用户类型管理</a></li>
					<li class=""><a href="../WeiXinUserType/TypeToModule">用户权限管理</a></li>
					<li class="cur"><a href="../WeiXinUser/Bind">账号绑定管理</a></li>
                </ul>
            </div>
            <div id="reply_keyword" class="r_con_wrap">
                <table border="0" cellpadding="5" cellspacing="0" class="r_con_table">
                    <thead>
                        <tr>
                            <td width="25%" nowrap="nowrap">
                                用户类型
                            </td>
                            <td width="25%" nowrap="nowrap">
                                登录名
                            </td>
                            <td width="25%" nowrap="nowrap">
                                姓名
                            </td>
                            <td width="25%" nowrap="nowrap">
                                电话
                            </td>
                            <td width="25%" nowrap="nowrap">
                                地址
                            </td>
                            <td width="15%" nowrap="nowrap">
                                微信OpenID
                            </td>
                              <td width="10%" nowrap="nowrap" class="last">
                                绑定
                            </td>
                          
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                <div class="blank20">
                </div>
                <div id="turn_page">
                    <font class="page_noclick">&lt;&lt;上一页</font>&nbsp;<font class="page_item_current">0</font>&nbsp;<font
                        class="page_noclick">下一页&gt;&gt;</font>
                </div>
            </div>
        </div>
    </div>

    <%--绑定微信账号--%>
    <div id="BindDialog" title="绑定微信账号" class="rows" style="margin-left:50px;margin-top:20px;">
        <label>用户微信OpenId:</label>
       <input type="text" name="OpenId" id="OpenId" notnull="" "/>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">

    <link href="../../Content/themes/base/jquery-ui.css" rel="stylesheet" />
    <link href="../../Css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/Box/Content.css" rel="stylesheet" type="text/css" />
    <link href="../../WeiXinCss/global.css" rel="stylesheet" type="text/css" />
    <link href="../../WeiXinCss/main.css" rel="stylesheet" type="text/css" />
    <link href="../../WeiXinCss/wechat.css" rel="stylesheet" type="text/css" />
    <script src="../../WeiXinJs/global.js" type="text/javascript"></script>
    <script src="../../WeiXinJs/wechat.js" type="text/javascript"></script>
    <script src="../../Scripts/jqueryUi/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Box/jquery.Box.min.js" type="text/javascript"></script>
    <script src="../../Scripts/JQueryValidate/jquery.validate.min.js" type="text/javascript"></script>
    <script src="../../Scripts/JqGrid/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script src="../../Scripts/JqGrid/grid.locale-cn.js" type="text/javascript"></script>
    <script src="../../Scripts/Shop/shop.js" type="text/javascript"></script>


    <style type="text/css">
        body, html
        {
            background: url(../../WeiXinImages/main/main-bg.jpg) left top fixed no-repeat;
        }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
        <script type="text/javascript">
            var currentId = "", currentName = "";

            $(function () {
                getPageData(1);
                initDialog();
            });

            function getPageData(pageIndex) {
                $.post("GetPageData", { Page: pageIndex }, function (response) {
                    var dataRows = response.rows;
                    var tbody = $(".r_con_table").find("tbody");
                    tbody.empty();
                    $(dataRows).each(function (i) {
                        var tr = $("<tr></tr>");
                        tr.append('<td style="display:none">' + dataRows[i].Id + '</td>');
                        tr.append('<td style="display:none">' + dataRows[i].SysPsw + '</td>');
                        tr.append('<td style="display:none">' + dataRows[i].UserType + '</td>');
                        tr.append('<td style="display:none">' + dataRows[i].LocationX + '</td>');
                        tr.append('<td style="display:none">' + dataRows[i].LocationY + '</td>');
                        tr.append('<td style="display:none">' + dataRows[i].WeiXinUserTypeId + '</td>');
                        tr.append('<td style="display:none">' + dataRows[i].WeiXinAppId + '</td>');

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].UserTypeDsc);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].SysName);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].Name);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].Tel);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].Address);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].WxUserName);
                        tr.append(td);

                        //操作
                        td = $('<td nowrap="nowrap" class="last"></td>');
                        var p = "'" + dataRows[i].Id + "','" + dataRows[i].Name+"'";
                        var modifyATag = $('<a onclick="showBindDialog(' + p + ');' + '"><img src="../../WeiXinImages/ico/mod.gif" align="absmiddle" alt="修改"></a>');
                        td.append(modifyATag);

                        tr.append(td);

                        tbody.append(tr);
                    });
                });
            }

         
            function showBindDialog(id,name) {
                currentId = id;
                currentName = name;
                var title = "绑定微信账号-当前用户：" + name;
                $("#BindDialog").dialog("option", "title", title);
                $("#BindDialog").dialog("open");
            }
            //初始化添加产品dialog
            function initDialog() {
              
                $("#BindDialog").dialog({
                    autoOpen: false,
                    modal: true,
                    width: 400,
                    height: 200,
                    resizable: true,
                    position: ['center', 'center'],
                    buttons: {
                        "确定": function () {
                            Bind();
                        },
                        "关闭": function () {
                            $(this).dialog("close");
                        }
                    }
                });

                $("#BindDialog").dialog();
            }

            //账号绑定
            function Bind()
            {
                var wxUserName = $("#OpenId").val();
                if (wxUserName == "") {
                    MsgAlert("警告", "请填写要绑定的微信用户OpenId！");
                }
                else {
                    var postData = { Id: currentId, WxUserName: wxUserName };
                    $.ajax({
                        url: "BindUser",
                        data: postData,
                        //contentType: "application/json;charset=utf-8",
                        type: "POST",
                        catche: false,
                        success: function (dohandle) {
                            if (dohandle.IsSuccessful) {
                                MsgAlert("提示", dohandle.OperateMsg);
                                $("#BindDialog").dialog();
                            }
                            else {
                                MsgAlert("警告", dohandle.OperateMsg);
                                
                            }

                        },
                        error: function (e) {
                            MsgAlert("提示", e.statusText);
                        }

                    });
                }

            }

    </script>

</asp:Content>
