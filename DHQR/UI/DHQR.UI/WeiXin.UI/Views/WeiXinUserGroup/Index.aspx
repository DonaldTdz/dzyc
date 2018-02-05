<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    微信用户
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
                <div class="control_btn">
                    <a href="#" class="btn_green btn_w_120" onclick="SynUserGroup();">同步分组</a>
                    <a href="../WeiXinUserGroup/EditOrAdd" class="btn_green btn_w_120">新增分组</a>
                    <a href="../WeiXinUserGroup/EditUserGroup" class="btn_green btn_w_120">管理分组用户</a>
                </div>
                <table border="0" cellpadding="5" cellspacing="0" class="r_con_table">
                    <thead>
                        <tr>
                            <td width="25%" nowrap="nowrap">
                                分组ID
                            </td>
                            <td width="25%" nowrap="nowrap">
                                分组名称
                            </td>
                            <td width="25%" nowrap="nowrap">
                                用户数
                            </td>
                            <td width="10%" nowrap="nowrap" class="last">
                                操作
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
            //var tabModel = {"Index",};
            $(function () {
                getPageData(1);
            });

            function getPageData(pageIndex) {
                $.post("GetPageData", { Page: pageIndex }, function (response) {
                    var dataRows = response.rows;
                    var tbody = $(".r_con_table").find("tbody");
                    tbody.empty();
                    $(dataRows).each(function (i) {
                        var tr = $("<tr></tr>");
                        tr.append('<td style="display:none">' + dataRows[i].Id + '</td>');

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].groupid);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].name);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].count);
                        tr.append(td);

                        //操作
                        td = $('<td nowrap="nowrap" class="last"></td>');
                        var modifyATag = $('<a href="EditOrAdd?Id=' + dataRows[i].Id + '"><img src="../../DHQRImages/ico/mod.gif" align="absmiddle" alt="修改"></a>');
                        td.append(modifyATag);
                        td.append('<sapn style="display:inline-block; width:5px;"></span>');
                        var viewATag = $('<a href="ViewUser?groupid=' + dataRows[i].groupid + '"><img src="../../DHQRImages/ico/view.gif" align="absmiddle" alt="查看用户"></a>');
                        td.append(viewATag);

                        tr.append(td);

                        tbody.append(tr);
                    });
                });
            }

            function SynUserGroup() {
                $.post("SynUserGroup", {}, function (response) {
                    if (response.IsSuccessful) {
                        getPageData(1);
                    }
                    else {
                        alert(response.OperateMsg);
                    }
                });

            }

    </script>

</asp:Content>
