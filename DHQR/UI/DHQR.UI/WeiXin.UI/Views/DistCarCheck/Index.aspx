<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    车辆检查记录
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
					<li class="cur"><a href="../DistCarCheck/Index">车辆检查记录</a></li>
                </ul>
            </div>
            <div id="reply_keyword" class="r_con_wrap">
                <div class="control_btn">
                <form class="r_con_search_form" method="get">
                    <label>日期：</label>
                    <input type="text" name="logDate" id="logDate" class="form_input" style="margin: 0px; padding: 0px; width: 150px;" value="<%=DateTime.Now.ToString("yyyy-MM-dd")%>" />
                    <input type="button" class="search_btn" value="搜索"  onclick="getPageData()"/>
                </form>
                      </div>
                <table border="0" cellpadding="5" cellspacing="0" class="r_con_table">
                    <thead>
                        <tr>
                            <td style="width:100px;">
                                车牌号
                            </td>
                            <td style="width:100px;">
                                送货员
                            </td>
                            <td style="width:100px;">
                                驾驶员
                            </td>
                            <td style="width:100px;">
                                检查类型
                            </td>
                            <td style="width:100px;">
                                全部正常
                            </td>
                            <td style="width:100px;">
                                异常信息
                            </td>
                            <%--<td style="width:100px;" class="last">
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
                $.post("GetCarCheckResult", { CheckDate: $("#logDate").val() }, function (response) {
                    var dataRows = response.rows;
                    var tbody = $(".r_con_table").find("tbody");
                    tbody.empty();
                    $(dataRows).each(function (i) {
                        var tr = $("<tr></tr>");
                        tr.append('<td style="display:none">' + dataRows[i].CHECK_TYPE + '</td>');


                        td = $('<td></td>');
                        td.append(dataRows[i].CAR_LICENSE);
                        tr.append(td);

                        td = $('<td></td>');
                        td.append(dataRows[i].DLVMAN_NAME);
                        tr.append(td);

                        td = $('<td></td>');
                        td.append(dataRows[i].DRIVER_NAME);
                        tr.append(td);

                        td = $('<td></td>');
                        td.append(dataRows[i].CHECK_TYPE_DESC);
                        tr.append(td);

                        td = $('<td></td>');
                        td.append(dataRows[i].IsAllOk ? "是" : "否");
                        tr.append(td);

                        td = $('<td></td>');
                        td.append(dataRows[i].ABNORMAL_DETAIL);
                        tr.append(td);

                        //操作
                        /*
                        td = $('<td class="last"></td>');
                        var modifyATag = $('<a href="javascript:void(0)" onclick="confirmCarOut(\'' + dataRows[i].DLVMAN_ID + '\',\'' + dataRows[i].LOG_DATE + '\')" style="cursor:pointer;"><img src="../../DHQRImages/ico/view.gif" align="absmiddle" alt="发车送货"></a>');
                        td.append(modifyATag);

                        td.append('<sapn style="display:inline-block; width:5px;"></span>');

                        var send = $('<a href="javascript:void(0)"  onclick="confirmCarIn(\'' + dataRows[i].DLVMAN_ID + '\',\'' + dataRows[i].LOG_DATE + '\')" style="cursor:pointer;"><img src="../../DHQRImages/ico/send.png" align="absmiddle" alt="车辆入库"></a>');
                        td.append(send);

                        tr.append(td);
                        */

                        tbody.append(tr);
                    });
                });
            }

            function initDate() {
                $('#logDate').datepicker({
                    format: 'yyyy-MM-dd',
                    //weekStart: 1,
                    autoclose: true,
                    todayBtn: 'linked',
                    onClose: function (dateText, inst) {
                        getPageData();
                    }
                });

            }


    </script>

</asp:Content>
