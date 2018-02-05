<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    满意度分析
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
                    <li><a href="../DistCust/TableData">满意度数据</a></li>
                    <li ><a href="../DistCust/SatisfyByDay">按单日任务分析</a></li>
                    <li class="cur"><a href="../DistCust/SatisfyOfRete">按评价项分析</a></li>
                    <li ><a href="../DistCust/NotSatisReason">不满意原因分析</a></li>
                </ul>
            </div>
            <div id="stores" class="r_con_wrap" style="padding-bottom:0px;min-height:90px;">
                <div class="control_btn" style="padding-bottom:0px;height:15px;margin-bottom:12px;">
                    <div class="tips_info"><strong>提示：</strong>按柱状图展示各个线路满意度指标</div>
                </div>
                <form class="r_con_search_form" method="get">
                    <label>开始时间：</label>
                    <input type="text" name="startTime" id="startTime" class="form_input" style="margin: 0px; padding: 0px; width: 150px;" value="<%=DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") %>" />
                    <label>结束时间：</label>
                    <input type="text" name="endTime" id="endTime" class="form_input" style="margin: 0px; padding: 0px; width: 150px;" value="<%=DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") %>" />
                    <label>满意度指标：</label>
                    <%=Html.DropDownList("SatisfyName",ViewData["SatisfyNameList"] as IList<SelectListItem>,new { @class = "ui-text-select" ,@onchange="generateReport();" }) %>
                    <label>关键字：</label>
                    <input type="text" name="Keyword" value="" class="form_input" size="15" />
                    <input type="button" class="search_btn" value="搜索"  onclick="generateReport()"/>
                </form>
                <div id="charContainer">
                </div>

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

    <script src="../../Scripts/Highcharts-4.0.3/highcharts.src.js"></script>
    <script src="../../Scripts/Highcharts-4.0.3/highcharts-3d.js"></script>
    <script src="../../Scripts/Highcharts-4.0.3/adapters/standalone-framework.js"></script>
    <script src="../../Scripts/Highcharts-4.0.3/themes/grid.js"></script>
    <script src="../../Scripts/Highcharts-4.0.3/modules/exporting.js"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
<script type="text/javascript">

    $(function () {
        initDate();
        generateReport();
    });

    function initDate() {
        $('#startTime').datepicker({
            format: 'yyyy-MM-dd',
            //weekStart: 1,
            autoclose: true,
            todayBtn: 'linked',
            onClose: function (dateText, inst) {
               generateReport();
            }
        });
        $('#endTime').datepicker({
            format: 'yyyy-MM-dd',
            //weekStart: 1,
            autoclose: true,
            todayBtn: 'linked',
            onClose: function (dateText, inst) {
               generateReport();
            }
        });
    }

    //根据条件生成统计报表
    function generateReport() {
        var startTime = $("#startTime").val(), endTime = $("#endTime").val(), keyWord = $("#Keyword").val();
        var satisfyValue = $("#SatisfyName").val();
        var satisfyName = $("#SatisfyName").find("option:selected").text();
        var param = { StartDate: startTime, EndDate: endTime, KeyWord: keyWord, SatisfyValue: satisfyValue ,SatisfyName:satisfyName};
        $.ajax({
            url: "GetSatisfyOfRete",
            dataType: "json",
            data: param,
            type: "post",
            success: function (data) {
                loadChart(data);
            },
            error: function (e) {
            }
        });

    }


    //重新设置图表
    function loadChart(data) {
        var chartData = [];
        var startDate = $("#startTime").val(), endDate = $("#endTime").val(), keyWord = $("#keyWord").val();
        for (var i = 0; i < data.data.length; i++) {
            var info = { name: data.data[i].name, data: data.data[i].decimalData };
            chartData[i] = info;
        }
        $('#charContainer').highcharts({
            chart: {
                type: 'column',
                marginRight: 130,
                marginBottom: 25
            },
            title: {
                text: '满意度指标分析',
                x: -20 //center
            },
            subtitle: {
                text: startDate + '至' + endDate,
                x: -20
            },
            xAxis: {
                categories: data.xAxis

            },
            yAxis: {
                title: {
                    text: '占比(%)'
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            tooltip: {
                valueSuffix: '%'
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'top',
                x: -10,
                y: 100,
                borderWidth: 0
            },
            series: chartData,
            exporting: {
                buttons: {
                    contextButton: {
                        menuItems: [{
                            text: '报表导出为 JPEG',
                            onclick: function () {
                                this.exportChart({
                                    type: 'image/jpeg'
                                });
                            }
                        }]
                    }
                }
            }

        });

    }



    </script>
</asp:Content>
