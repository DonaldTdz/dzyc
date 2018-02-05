<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    订单信息
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
               <%-- <ul>
                    <li class="cur"><a href="#">订单信息</a></li>
                </ul>--%>
            </div>
            <div id="stores" class="r_con_wrap">
                <form class="r_con_search_form" method="get">
                    <label>关键字：</label>
                    <input type="text" name="Keyword" id="Keyword" value="" class="form_input" size="15" />     
                    <input type="button" class="search_btn" value="搜索"  onclick="searchStore()"/>
                </form>           
                <table border="0" cellpadding="5" cellspacing="0" class="r_con_table">
                    <thead>
                        <tr>
                            <td style="width:100px;">订单号</td>
                            <td style="width:100px;">客户代码</td>
                            <td style="width:100px;">客户名称</td>
                            <td style="width:200px;">地址</td>
                            <td style="width:100px;">电话</td>
                            <td style="width:100px;">数量</td>
                            <td style="width:100px;">金额</td>
                            <td style="width:100px;">订单类型</td>
                            <td style="width:100px;">收货状态</td>
                            <td style="width:100px;">刷卡时间</td>
                            <td style="width:100px;" class="last">操作</td>
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
    <%=Html.Hidden("DIST_NUM")%>
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

                $.post("QueryLdmdistLine", { Page: pageIndex, DIST_NUM: $("#DIST_NUM").val(),KeyData:$("#Keyword").val(), Rows: 10, Sord: "asc", Sidx: "SEQ", StartIndex: 1 }, function (response) {
                    var dataRows = response.rows;
                    var tbody = $(".r_con_table").find("tbody");
                    tbody.empty();
                    $(dataRows).each(function (i) {
                        var tr = $("<tr></tr>");
                        tr.append('<td style="display:none">' + dataRows[i].Id + '</td>');

                        var td = $('<td></td>');
                        td.append(dataRows[i].CO_NUM);
                        tr.append(td);

                        td = $('<td></td>');
                        td.append(dataRows[i].CUST_CODE);
                        tr.append(td);



                        td = $('<td></td>');
                        td.append(dataRows[i].MANAGER);
                        tr.append(td);

                        td = $('<td></td>');
                        td.append(dataRows[i].ADDR);
                        tr.append(td);

                        td = $('<td></td>');
                        td.append(dataRows[i].TEL);
                        tr.append(td);

                        td = $('<td></td>');
                        td.append(dataRows[i].QTY_BAR);
                        tr.append(td);

                        td = $('<td></td>');
                        td.append(dataRows[i].AMT_AR);
                        tr.append(td);

                        td = $('<td></td>');
                        if (dataRows[i].IsTemp) {
                            td.append("暂存订单");
                        }
                        else {
                            td.append("正常订单");
                        }
                        tr.append(td);

                        td = $('<td></td>');
                        td.append(dataRows[i].REC_STATE);
                        tr.append(td);

                        td = $('<td></td>');
                        td.append(dataRows[i].REC_CIG_TIME);
                        tr.append(td);


                        //操作
                        td = $('<td class="last"></td>');
                        var modifyATag = $('<a href="OrderInfo?DIST_NUM=' + dataRows[i].DIST_NUM + '"><img src="../../DHQRImages/ico/view.gif" align="absmiddle" alt="查看"></a>');
                        td.append(modifyATag);
                        td.append('<sapn style="display:inline-block; width:5px;"></span>');
                        if (dataRows[i].REC_STATE == "未送达") {
                            //var send = $('<div><a onclick="confirmDel(\'' + dataRows[i].CO_NUM + '\')"  style="cursor:pointer;"><img src="../../DHQRImages/ico/send.png" align="absmiddle" alt="到货确认"/></a></div>')
                            //td.append(send);

                            var send = $('<a href="javascript:void(0)" onclick="confirmDel(\'' + dataRows[i].CO_NUM + '\')" style="cursor:pointer;"><img src="../../DHQRImages/ico/send.png" align="absmiddle" alt="到货确认"></a>');
                            td.append(send);

                        }

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

            //到货确认
            function confirmDel(CO_NUM)
            {
                if (!confirm("是否进行到货确认？")) { return false };
                $.post("../../LdmDist/HandleConfirm", { CO_NUM: CO_NUM, DIST_NUM: $("#DIST_NUM").val() }, function (data) {
                    if (data.IsSuccessful == true) {
                        alert("到货确认成功");
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
