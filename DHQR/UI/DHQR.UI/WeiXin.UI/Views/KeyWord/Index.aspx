<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
					<li class=""><a href="../WeiXinApp/Index">微信授权配置</a></li>
					<li class=""><a href="../WeiXinApp/InterfaceCfg">微信接口配置</a></li>
					<li class=""><a href="../FirstIn/Index">首次关注设置</a></li>
					<li class=""><a href="../WeiXinMenu/Index">自定义菜单设置</a></li>
					<li class="cur"><a href="../KeyWord/Index">关键词回复</a></li>
                </ul>
            </div>
            <div id="reply_keyword" class="r_con_wrap">
                <div class="control_btn">
                    <a href="../KeyWord/EditOrAdd" class="btn_green btn_w_120">添加关键字</a></div>
                <table border="0" cellpadding="5" cellspacing="0" class="r_con_table">
                    <thead>
                        <tr>
                            <td width="10%" nowrap="nowrap">
                                序号
                            </td>
                            <td width="25%" nowrap="nowrap">
                                触发关键词
                            </td>
                            <td width="15%" nowrap="nowrap">
                                匹配模式
                            </td>
                            <td width="35%" nowrap="nowrap">
                                回复内容
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
                    <font class="page_noclick">&lt;&lt;上一页</font>&nbsp;<font class="page_item_current">1</font>&nbsp;<font
                        class="page_noclick">下一页&gt;&gt;</font></div>
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

                    var td = $('<td nowrap="nowrap"></td>');
                    td.append(dataRows[i].Index);
                    tr.append(td);

                    td = $('<td nowrap="nowrap"></td>');
                    td.append(dataRows[i].KeyWord);
                    tr.append(td);

                    td = $('<td nowrap="nowrap"></td>');
                    td.append(dataRows[i].PatternMethodStr);
                    tr.append(td);

                    td = $('<td nowrap="nowrap"></td>');
                    td.append(dataRows[i].ContenInfo);
                    tr.append(td);
                    //操作
                    td = $('<td nowrap="nowrap" class="last"></td>');
                    var modifyATag = $('<a href="EditOrAdd?Id=' + dataRows[i].Id + '"><img src="../../DHQRImages/ico/mod.gif" align="absmiddle" alt="修改"></a>');
                    td.append(modifyATag);
                    td.append('<sapn style="display:inline-block; width:5px;"></span>');
                    var delATag = $('<a href="javascript:void(0)" onclick="delKeyword(this)" style="cursor:pointer;"><img src="../../DHQRImages/ico/del.gif" align="absmiddle" alt="删除"></a>');
                    td.append(delATag);

                    tr.append(td);

                    tbody.append(tr);
                });
            });
        }

        function delKeyword(obj) {
            if (!confirm("删除后不可恢复，继续吗？")) { return false };
            var Id = $(obj).parents("tr")[0].children[0].innerText;
            $.post("Delete", { id: Id }, function (data) {
                if (data.IsSuccessful == true) {
                    $(obj).parents("tr").remove();
                } else {
                    alert(data.OperateMsg);
                }
            }, 'json');
        }


    </script>
</asp:Content>
