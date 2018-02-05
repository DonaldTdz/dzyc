<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    零售户信息管理
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
                    <li class="cur"><a href="#">零售户管理</a></li>
                </ul>
            </div>
            <div id="stores" class="r_con_wrap">
                <div class="control_btn">
                    <%--<a href="../WeiXinRetailStore/Add" class="btn_ok btn_ok_w_120">新增零售户</a>--%>
                    <div class="tips_info"><strong>提示：</strong>零售户信息从管控平台每日同步，在到货确认系统扩展增加到货确认密码和位置信息</div>
                </div>
                <form class="r_con_search_form" method="get">
                    <label>关键字：</label>
                    <input type="text" name="Keyword" value="" class="form_input" size="15" />
                    <label>收货方式：</label>
                    <%=Html.DropDownList("RecieveTypeName",ViewData["RecieveTypeList"] as IList<SelectListItem>,new { @class = "ui-text-select" ,@onchange="searchStore();" }) %>
                     <label>状态：</label>
                    <%=Html.DropDownList("Status",ViewData["RetailerStatusList"] as IList<SelectListItem>,new { @class = "ui-text-select" ,@onchange="searchStore();" }) %>
                    <label>是否采点：</label>
                    <%=Html.CheckBox("IsCollect")%>
                    <input type="button" class="search_btn" value="搜索"  onclick="searchStore()"/>
                </form>
                <table border="0" cellpadding="5" cellspacing="0" class="r_con_table">
                    <thead>
                        <tr>
                            <td width="20%" nowrap="nowrap">客户内码</td>
                            <td width="20%" nowrap="nowrap">客户名称</td>
                            <td width="25%" nowrap="nowrap">专卖证件号</td>
                            <td width="20%" nowrap="nowrap">客户状态</td>
                            <td width="20%" nowrap="nowrap">收货方式</td>
                            <td width="20%" nowrap="nowrap">是否采点</td>
                            <td width="15%" nowrap="nowrap" class="last">操作</td>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                <div class="blank20"></div>
                <div id="turn_page"><font class="page_noclick">&lt;&lt;上一页</font>&nbsp;<font class="page_item_current">1</font>&nbsp;<font class="page_noclick">下一页&gt;&gt;</font></div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
    <link href="../../DHQRCss/global.css" rel="stylesheet" type="text/css" />
    <link href="../../DHQRCss/frame.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
        <script type="text/javascript">
            $(function () {
                getPageData(1, "");
            });


            function searchStore() {
                getPageData(1, $("input[name=Keyword]").val());
            }

            
            function getPageData(pageIndex, keyWord) {
                var STATUS = $("#Status").val();
                var RecieveType = $("#RecieveTypeName").val();
                var iscolect = $('#IsCollect').is(':checked');
                $.post("GetPageData", { Page: pageIndex, Keyword: keyWord, IsCollect: iscolect, STATUS: STATUS, RecieveType: RecieveType, Rows: 10, Sord: "asc", Sidx: "CUST_ID", StartIndex: 1 }, function (response) {
                    var dataRows = response.rows;
                    var tbody = $(".r_con_table").find("tbody");
                    tbody.empty();
                    $(dataRows).each(function (i) {
                        var tr = $("<tr></tr>");
                        tr.append('<td style="display:none">' + dataRows[i].Id + '</td>');

                        var td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].CUST_ID);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].CUST_NAME);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].LICENSE_CODE);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].StatusStr);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].RecieveTypeName);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        var checkStr ;
                        if (dataRows[i].HasPoint) {
                            checkStr = '<input type="checkbox" checked="checked" disabled="disabled"/>';
                        }
                        else {
                            checkStr = '<input type="checkbox" disabled="disabled"/>';
                        }
                        var checkBtn = $(checkStr);
                        td.append(checkBtn);
                        tr.append(td);

                        //操作
                        td = $('<td nowrap="nowrap" class="last"></td>');
                        var modifyATag = $('<a href="Edit?Id=' + dataRows[i].Id + '"><img src="../../DHQRImages/ico/mod.gif" align="absmiddle" alt="修改"></a>');
                        td.append(modifyATag);
                        td.append('<sapn style="display:inline-block; width:5px;"></span>');
                        var resttTag = $('<a href="javascript:void(0)" onclick="resetPsw(this)" style="cursor:pointer;"><img src="../../DHQRImages/ico/add.gif" align="absmiddle" alt="重置密码"></a>');
                        td.append(resttTag);

                        tr.append(td);

                        tbody.append(tr);
                    });

                    //设置分页数据
                   setPageDiv("turn_page", pageIndex, response.total, "getPageData");

                });
            }

            function delStore(obj) {
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

            function resetPsw(obj) {
                if (!confirm("是否进行密码重置？")) { return false };
                var Id = $(obj).parents("tr")[0].children[0].innerText;
                $.post("ResetPsw", { Id: Id }, function (data) {
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
                var keyWd=$("input[name=Keyword]").val();
                if (pageIndex == 1 || totalPage == 0) {
                    prevPage = '<font class="page_noclick">&lt;&lt;上一页</font>';
                } else {
                    prevPage = "<a style=\"cursor:pointer;\" onclick=\"getPageData(" + (pageIndex - 1) + ",'" + keyWd + "')\"><font>&lt;&lt;上一页</font></a>";
                }
                if (pageIndex == totalPage) {
                    nextPage = '<font class="page_noclick">下一页&gt;&gt;</font>';
                } else {
                    nextPage = "<a style=\"cursor:pointer;\" onclick=\"" + refashFn + "(" + (pageIndex + 1) + ",'" + keyWd  + "')\"><font class=\"page_button\">下一页&gt;&gt;</font></a>";
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
