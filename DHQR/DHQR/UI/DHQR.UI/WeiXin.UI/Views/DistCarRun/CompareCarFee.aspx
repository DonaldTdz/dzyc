<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    满意度分析
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
                    <li><a href="../DistCarRun/Index">车辆行驶信息</a></li>
                    <li><a href="../DistCarRun/AnalysisByTrend">按费用走势分析</a></li>
                    <li class="cur"><a href="../DistCarRun/CompareCarFee">各车辆费用比较</a></li>
                    <li><a href="../DistCarRun/YearRateOfCarYear">同比增长率(年)</a></li>
                    <li><a href="../DistCarRun/YearRateOfCarMonth">同比增长率(月)</a></li>
                    <li><a href="../DistCarRun/LinkRateOfCar">环比增长率</a></li>
                </ul>
            </div>
            <div id="stores" class="r_con_wrap" style="padding-bottom:0px;min-height:90px;">
                <div class="control_btn" style="padding-bottom:0px;height:15px;margin-bottom:12px;">
                    <div class="tips_info"><strong>提示：</strong>按折线图展示各车辆费用走势</div>
                </div>
                <form class="r_con_search_form" method="get">
                    <label>开始时间：</label>
                    <input type="text" name="startTime" id="startTime" class="form_input" style="margin: 0px; padding: 0px; width: 150px;" value="<%=DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") %>" />
                    <label>结束时间：</label>
                    <input type="text" name="endTime" id="endTime" class="form_input" style="margin: 0px; padding: 0px; width: 150px;" value="<%=DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") %>" />
                  <%--  <label>配送线路：</label>
                    <%=Html.DropDownList("DeliveryLine",ViewData["DeliveryLineList"] as IList<SelectListItem>,new { @class = "ui-text-select" ,@onchange="searchStore();" }) %>--%>
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
        var param = { StartDate: startTime, EndDate: endTime };
        $.ajax({
            url: "GetCompareCarFeeData",
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

    var chart;
    //重新设置图表
    function loadChart(chartdata) {

        var colors = Highcharts.getOptions().colors;
        var chartData = [];
        var startDate = $("#startTime").val(), endDate = $("#endTime").val(), keyWord = $("#keyWord").val();
        for (var i = 0; i < chartdata.data[0].decimalData.length; i++) {
            var info = {
                name: chartdata.xAxis[i], y: chartdata.data[i].decimalData[i], color: colors[i]
                , drilldown: { name: chartdata.data[i].drilldown.name, categories: chartdata.data[i].drilldown.categories, data: chartdata.data[i].drilldown.decimalData, color: colors[i] }
            };
            chartData[i] = info;
        }
         chart = $('#charContainer').highcharts({
            chart: { type: 'column' }, 
            title: { text: '车辆费用信息比较' }, 
            subtitle: { text: startDate + '至' + endDate },
            xAxis: { categories: chartdata.xAxis },
            yAxis: { title: { text: '费用(元)' } }, 
            plotOptions: { 
                column: { cursor: 'pointer', 
                    point: { 
                        events: { 
                            click: function() 
                            { 
                                var drilldown = this.drilldown; 
                                if (drilldown) {
                                    // drill down 
                                    setChart(drilldown.name, drilldown.categories, drilldown.data, drilldown.color);
                                } else { 
                                    // restore 
                                    setChart("车辆信息统计", chartdata.xAxis, chartData);
                                }
                            }
                        }
                    }, dataLabels:
                        {
                            enabled: true,
                            color: colors[0],
                            style: { fontWeight: 'bold' },
                            formatter: function ()
                            {
                                return this.y + '元';
                            }
                        }
                }
            },
            tooltip:
                {
                    formatter:
                        function ()
                        {
                            var point = this.point, s = this.x + ':<b>' + this.y + '元</b><br><br>';
                            s += '<i>点击查看费用明细</i>';
                            return s;
                        }
                },
            series: [{ name: "车辆信息统计", data: chartData }],
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
        }).highcharts(); 

    }

    function setChart(name, categories, data, color)
    {
        chart.xAxis[0].setCategories(categories, false);
        chart.series[0].remove(false);
        chart.addSeries({ name: name, data: data, color: color || 'white' }, false);
        chart.redraw();
    }

    </script>
</asp:Content>
