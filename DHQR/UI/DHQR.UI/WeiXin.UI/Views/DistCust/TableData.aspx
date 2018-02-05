<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    满意度信息
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
                    <li class="cur" ><a href="../DistCust/TableData">满意度数据</a></li>
                    <li ><a href="../DistCust/SatisfyByDay">按单日任务分析</a></li>
                    <li><a href="../DistCust/SatisfyOfRete">按评价项分析</a></li>
                    <li><a href="../DistCust/NotSatisReason">不满意原因分析</a></li>
                </ul>
            </div>
            <div id="reply_keyword" class="r_con_wrap">
                <div class="control_btn">
                <form class="r_con_search_form" method="get">
                    <label>开始时间：</label>
                    <input type="text" name="startTime" id="startTime" class="form_input" style="margin: 0px; padding: 0px; width: 150px;" value="<%=DateTime.Now.AddDays(0).ToString("yyyy-MM-dd")  %>" />
                    <label>结束时间：</label>
                    <input type="text" name="endTime" id="endTime" class="form_input" style="margin: 0px; padding: 0px; width: 150px;" value="<%=DateTime.Now.AddDays(0).ToString("yyyy-MM-dd")  %>" />
                    <input type="button" class="search_btn" value="搜索"  onclick="getPageData()"/>
                </form>
                      </div>
                <table border="0" cellpadding="5" cellspacing="0" class="r_con_table">
                    <thead>
                        <tr>                         
                            <td width="10%" nowrap="nowrap">
                                送货员
                            </td>
                            <td width="10%" nowrap="nowrap">
                                非常满意
                            </td>
                            <td width="10%" nowrap="nowrap">
                                非常满意(%)
                            </td>
                            <td width="10%" nowrap="nowrap">
                                满意
                            </td>
                            <td width="10%" nowrap="nowrap">
                                满意(%)
                            </td>
                            <td width="10%" nowrap="nowrap">
                                一般
                            </td>
                            <td width="10%" nowrap="nowrap">
                                一般(%)
                            </td>
                            <td width="10%" nowrap="nowrap">
                                满意
                            </td>
                            <td width="10%" nowrap="nowrap">
                                满意(%)
                            </td>
                            <td width="10%" nowrap="nowrap">
                                不满意
                            </td>
                            <td width="10%" nowrap="nowrap">
                                不满意(%)
                            </td>
                            <%--<td width="5%" nowrap="nowrap" class="last">
                                操作
                            </td>--%>
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
        <link href="../../DHQRCss/global.css" rel="stylesheet" type="text/css" />
    <link href="../../DHQRCss/frame.css" rel="stylesheet" type="text/css" />
        <link href="../../Content/JqueryUi/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" />
    <script src="../../Scripts/jqueryUi/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="../../Scripts/jqueryUi/timepicker.js" type="text/javascript"></script>
    <script src="../../Scripts/jqueryUi/ui.datepicker-zh-CN.js" type="text/javascript"></script>

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
                initDate();
                getPageData();
            });

            function getPageData() {
                $.post("QuerySatisfaction", { StartDate: $("#startTime").val(), EndDate: $("#endTime").val(),DLVMAN_ID:"" }, function (response) {
                    var dataRows = response.rows;
                    var tbody = $(".r_con_table").find("tbody");
                    tbody.empty();
                    $(dataRows).each(function (i) {
                        var tr = $("<tr></tr>");
                        tr.append('<td style="display:none">' + dataRows[i].DLVMAN_ID + '</td>');


                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].DLVMAN_NAME);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].VerySatisfiedCount);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].VerySatisfiedRate);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].SatisfiedCount);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].SatisfiedRate);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].GeneralCount);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].GeneralRate);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].DissatisfiedCount);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].DissatisfiedRate);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].VeryDissatisfiedCount);
                        tr.append(td);

                        td = $('<td nowrap="nowrap"></td>');
                        td.append(dataRows[i].VeryDissatisfiedRate);
                        tr.append(td);


                        //操作
                        td = $('<td nowrap="nowrap" class="last"></td>');

                        tr.append(td);

                        tbody.append(tr);
                    });
                });
            }

            function initDate() {
                $('#startTime').datepicker({
                    format: 'yyyy-MM-dd',
                    //weekStart: 1,
                    autoclose: true,
                    todayBtn: 'linked',
                    onClose: function (dateText, inst) {
                        // generateReport();
                    }
                });
                $('#endTime').datepicker({
                    format: 'yyyy-MM-dd',
                    //weekStart: 1,
                    autoclose: true,
                    todayBtn: 'linked',
                    onClose: function (dateText, inst) {
                        //       generateReport();
                    }
                });

            }

    </script>

</asp:Content>
