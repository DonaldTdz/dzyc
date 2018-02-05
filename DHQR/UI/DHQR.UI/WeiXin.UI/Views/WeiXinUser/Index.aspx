<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    微信用户
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
					<li class="cur"><a href="../WeiXinUser/Index">零售户</a></li>
					<li class=""><a href="../WeiXinUser/InnerUser">内部员工</a></li>
					<%--<li class=""><a href="../WeiXinUserType/TypeToModule">用户权限管理</a></li>
					<li class=""><a href="../WeiXinUser/Bind">账号绑定管理</a></li>--%>
                </ul>
            </div>
            <div id="reply_keyword" class="r_con_wrap">
                <%--<div class="control_btn">
                    <a href="../WeiXinUser/EditOrAdd" class="btn_green btn_w_120">添加用户</a></div>--%>
                <form class="r_con_search_form" method="get">
                    <label>关键字：</label>
                    <input type="text" name="Keyword" id="Keyword" value="" class="form_input" size="15" />
                    <label>线路：</label>
                    <%=Html.DropDownList("RutList",ViewData["RutList"] as IList<SelectListItem>,new { @class = "ui-text-select" ,@onchange="getPageData(1);" }) %>
                    <input type="button" class="search_btn" value="搜索"  onclick="getPageData(1);"/>
                    <input type="button" class="search_btn" value="导出"  onclick="downLoad();"/>
                    <label>共：【</label><label id="userCount" style="color:red;"></label> <label>】人</label> 
                </form>
                <table border="0" cellpadding="5" cellspacing="0" class="r_con_table">
                    <thead>
                        <tr>
                           <%-- <td style="width:100px;">
                                用户类型
                            </td>--%>
                            <td style="width:100px;">
                                头像
                            </td>
                            <td style="width:100px;">
                                专卖证号
                            </td>
                            <td style="width:100px;">
                                姓名
                            </td>
                            <td style="width:100px;">
                                电话
                            </td>
                            <td style="width:100px;">
                                线路
                            </td>
                              <td style="width:100px;">
                                地址
                            </td>
                           <%-- <td style="width:100px;">
                                微信OpenID
                            </td>--%>
                            <td style="width:100px;">
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
    <link href="../../DHQRCss/frame.css" rel="stylesheet" type="text/css" />
    <link href="../../DHQRCss/main.css" rel="stylesheet" type="text/css" />
    <link href="../../DHQRCss/wechat.css" rel="stylesheet" type="text/css" />
    <script src="../../DHQRJs/global.js" type="text/javascript"></script>
    <script src="../../DHQRJs/wechat.js" type="text/javascript"></script>
    <script src="../../Scripts/Common.js"></script>
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
                $.post("QueryData", { Page: pageIndex, KeyWord: $("#Keyword").val(), WeiXinUserTypeId: "4B51C42E-BE15-436F-97A2-6BF48DEDDA6E", RUT_ID: $("#RutList").val() }, function (response) {
                    var dataRows = response.rows;
                    $("#userCount").text(response.records);
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
                        tr.append('<td style="display:none">' + dataRows[i].WxUserName + '</td>');

                        //td = $('<td></td>');
                        //td.append(dataRows[i].UserTypeDsc);
                        //tr.append(td);
                       var  td = $('<td></td>');
                        var img = $(' <a href="' + dataRows[i].headimgurl + '" target="_blank"><img src="' + dataRows[i].headimgurl + '" align="absmiddle" width="48px"; height="48px" alt=""></a>');
                        td.append(img);
                        tr.append(td);

                        td = $('<td></td>');
                        td.append(dataRows[i].SysName);
                        tr.append(td);

                        td = $('<td></td>');
                        td.append(dataRows[i].Name);
                        tr.append(td);

                        td = $('<td></td>');
                        td.append(dataRows[i].Tel);
                        tr.append(td);

                        td = $('<td></td>');
                        td.append(dataRows[i].RUT_NAME);
                        tr.append(td);

                        td = $('<td></td>');
                        td.append(dataRows[i].Address);
                        tr.append(td);

                        //td = $('<td></td>');
                        //td.append(dataRows[i].WxUserName);
                        //tr.append(td);

                        //操作
                        td = $('<td class="last"></td>');
                        //var modifyATag = $('<a href="EditOrAdd?Id=' + dataRows[i].Id + '"><img src="../../DHQRImages/ico/mod.gif" align="absmiddle" alt="修改"></a>');
                        //td.append(modifyATag);
                        //td.append('<sapn style="display:inline-block; width:5px;"></span>');
                        var delATag = $('<a href="javascript:void(0)" onclick="delKeyword(this)" style="cursor:pointer;"><img src="../../DHQRImages/ico/del.gif" align="absmiddle" alt="删除"></a>');
                        td.append(delATag);

                        tr.append(td);

                        tbody.append(tr);
                    });
                    //设置分页数据
                    setPageDiv("turn_page", pageIndex, response.total, "getPageData");

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

            //导出数据
            function downLoad()
            {
                if ($("#RutList").val() == "")
                {
                    alert("请选择线路进行导出！");
                    return;
                }
                var data = {
                    rutId: $("#RutList").val(),
                    fileName: "线路-"+$("#RutList  option:selected").text()
                };
                KTree.Common.Post("../PublicData/ExportWeiXinUsers", data, function (result) {
                    if (result.IsSuccessful) {
                        top.location.href = result.OperateMsg;
                    } else {
                        $.messager.alert('友情提示', result.OperateMsg, 'warning', function () {
                        });
                    }
                }, function (result) { });
            }
    </script>

</asp:Content>
