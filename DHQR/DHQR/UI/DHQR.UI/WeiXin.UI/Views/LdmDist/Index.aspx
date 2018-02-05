<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    配送单信息查询
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
                    <li class="cur"><a href="#">配送单信息</a></li>
                </ul>
            </div>
            <div id="stores" class="r_con_wrap">
                
                <form class="r_con_search_form" method="get">
                    <label>关键字：</label>
                    <input type="text" name="Keyword" value="" class="form_input" size="15" />
                    <label>配送日期：</label>
                    <input type="text" name="distDate" id="distDate" class="form_input" style="margin: 0px; padding: 0px; width: 150px;" value="<%=DateTime.Now.ToString("yyyy-MM-dd") %>" />
     
                    <input type="button" class="search_btn" value="搜索"  onclick="searchStore()"/>
                </form>
                <table border="0" cellpadding="5" cellspacing="0" class="r_con_table">
                    <thead>
                        <tr>
                            <td width="12%" nowrap="nowrap">配送单号</td>
                            <td width="12%" nowrap="nowrap">线路</td>
                            <td width="10%" nowrap="nowrap">送货员</td>
                            <td width="10%" nowrap="nowrap">驾驶员</td>
                            <td width="10%" nowrap="nowrap">车牌号</td>
                            <td width="10%" nowrap="nowrap">客户数</td>
                            <td width="10%" nowrap="nowrap">送货量</td>
                            <td width="10%" nowrap="nowrap">应收金额</td>
                            <td width="6%" nowrap="nowrap" class="last">操作</td>
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
    <link href="../../Content/JqueryUi/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" />
    <script src="../../Scripts/jqueryUi/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="../../Scripts/jqueryUi/timepicker.js" type="text/javascript"></script>
    <script src="../../Scripts/jqueryUi/ui.datepicker-zh-CN.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
        <script type="text/javascript">
            $(function () {
                initDate();
                getPageData(1, "");
            });
            function initDate() {
                $('#distDate').datepicker({
                    format: 'yyyy-MM-dd',
                    //weekStart: 1,
                    autoclose: true,
                    todayBtn: 'linked',
                    onClose: function (dateText, inst) {
                        searchStore();
                    }
                });

            }

            function searchStore() {
                getPageData(1, $("input[name=Keyword]").val());
            }

          

            function getPageData(pageIndex, keyWord) {
                     
                $.post("QueryLdmDist", { Page: pageIndex, Keyword: keyWord,DistDate:$("#distDate").val(), Rows: 10, Sord: "asc", Sidx: "DLVMAN_ID", StartIndex: 1 }, function (response) {
                    var dataRows = response.rows;
                    var tbody = $(".r_con_table").find("tbody");
                    tbody.empty();
                    $(dataRows).each(function (i) {
                        var tr = $("<tr></tr>");
                        tr.append('<td style="display:none">' + dataRows[i].Id + '</td>');

                        var td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].DIST_NUM);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].RUT_NAME);
                        tr.append(td);

                       

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].DLVMAN_NAME);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].DRIVER_NAME);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].CAR_LICENSE);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].DIST_CUST_SUM);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].QTY_BAR);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].AMT_SUM);
                        tr.append(td);


                        //操作
                        td = $('<td nowrap="nowrap" class="last"></td>');
                        var modifyATag = $('<a href="OrderInfo?DIST_NUM=' + dataRows[i].DIST_NUM + '"><img src="../../DHQRImages/ico/view.gif" align="absmiddle" alt="查看"></a>');
                        td.append(modifyATag);
                        td.append('<sapn style="display:inline-block; width:5px;"></span>');
                        //var resttTag = $('<a href="javascript:void(0)" onclick="resetPsw(this)" style="cursor:pointer;"><img src="../../DHQRImages/ico/add.gif" align="absmiddle" alt="重置密码"></a>');
                        //td.append(resttTag);

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
