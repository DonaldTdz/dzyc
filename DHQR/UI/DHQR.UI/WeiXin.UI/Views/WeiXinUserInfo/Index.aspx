<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    微信用户
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
                <div class="control_btn">
                    <a href="#" class="btn_green btn_w_120" onclick="SynUserGroup();">同步微信用户</a></div>
                <table border="0" cellpadding="5" cellspacing="0" class="r_con_table">
                    <thead>
                        <tr>
                            <td width="15%" nowrap="nowrap">
                                头像
                            </td>
                            <td width="15%" nowrap="nowrap">
                                昵称
                            </td>
                            <td width="10%" nowrap="nowrap">
                                性别
                            </td>
                            <td width="10%" nowrap="nowrap">
                                备注名
                            </td>
                            <td width="10%" nowrap="nowrap">
                                国家
                            </td>
                            <td width="10%" nowrap="nowrap">
                                省份
                            </td>
                            <td width="10%" nowrap="nowrap">
                                城市
                            </td>
                            <td width="10%" nowrap="nowrap">
                                分组
                            </td>

                            <td width="15%" nowrap="nowrap" class="last">
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
                $.post("GetPageData", { Page: pageIndex,Sidx:"groupid" }, function (response) {
                    var dataRows = response.rows;
                    var tbody = $(".r_con_table").find("tbody");
                    tbody.empty();
                    $(dataRows).each(function (i) {
                        var tr = $("<tr></tr>");
                        tr.append('<td style="display:none">' + dataRows[i].Id + '</td>');
                        tr.append('<td style="display:none">' + dataRows[i].openid + '</td>');

                        td = $('<td nowrap="nowrap"></td>');
                        var img = $(' <a href="' + dataRows[i].headimgurl + '" target="_blank"><img src="' + dataRows[i].headimgurl + '" align="absmiddle" width="48px"; height="48px" alt=""></a>');
                      //  td.append(dataRows[i].headimgurl);
                        td.append(img);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].nickname);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        if (dataRows[i].sex == "1") {
                            td.append("男");
                        }
                        else {
                            td.append("女");
                        }
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].remark);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].country);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].province);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].city);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].groupname);
                        tr.append(td);

                        //操作
                        td = $('<td nowrap="nowrap" class="last"></td>');
                        var modifyATag = $('<a href="EditePage?openid=' + dataRows[i].openid + '"><img src="../../DHQRImages/ico/mod.gif" align="absmiddle" alt="修改"></a>');
                        td.append(modifyATag);
                        td.append('<sapn style="display:inline-block; width:5px;"></span>');
                        var viewATag = $('<a href="ViewUser?groupid=' + dataRows[i].groupid + '"><img src="../../DHQRImages/ico/view.gif" align="absmiddle" alt="查看用户"></a>');
                        td.append(viewATag);

                        tr.append(td);

                        tbody.append(tr);
                    });
                    //设置分页数据
                    setPageDiv("turn_page", pageIndex, response.total, "getPageData");

                });
            }

            function SynUserGroup() {
                $.post("SynWeiXinUserInfo", {}, function (response) {
                    if (response.IsSuccessful) {
                        getPageData(1);
                    }
                    else {
                        alert(response.OperateMsg);
                    }
                });

            }

            //设置翻页
            function setPageDiv(pageDivId, pageIndex, totalPage, refashFn) {
                var pageDiv = $("#" + pageDivId);
                var pageHtml = "";
                var prevPage, nextPage;
                var keyWd = $("input[name=Keyword]").val();
                if (pageIndex == 1 || totalPage == 0) {
                    prevPage = '<font class="page_noclick">&lt;&lt;上一页</font>';
                } else {
                    prevPage = "<a style=\"cursor:pointer;\" onclick=\"getPageData(" + (pageIndex - 1) + ",'" + keyWd + "')\"><font>&lt;&lt;上一页</font></a>";
                }
                if (pageIndex == totalPage) {
                    nextPage = '<font class="page_noclick">下一页&gt;&gt;</font>';
                } else {
                    nextPage = "<a style=\"cursor:pointer;\" onclick=\"" + refashFn + "(" + (pageIndex + 1) + ",'" + keyWd + "')\"><font class=\"page_button\">下一页&gt;&gt;</font></a>";
                }
                pageHtml = prevPage;
                for (var i = 1; i <= totalPage; i++) {
                    if (i == pageIndex) {
                        pageHtml += '&nbsp;<font class="page_item_current">' + i + '</font>';
                    }
                    if ((i >= parseInt(pageIndex) - parseInt(4)) && (i <= parseInt(pageIndex) + parseInt(4)) && i != pageIndex) {
                        pageHtml += "&nbsp;<a style=\"cursor:pointer;\" onclick=\"" + refashFn + "(" + i + ",'" + keyWd + "')\"><font>" + i + "</font></a>";
                    }
                }
                if (totalPage == 0) {
                    pageHtml += '&nbsp;<font class="page_item_current">' + 0 + '</font>';
                }
                pageHtml += '&nbsp' + nextPage;
                pageDiv.html(pageHtml);
            }


    </script>

</asp:Content>
