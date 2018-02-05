<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    车辆费用同比分析
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
                    <li><a href="../DistCarRun/Index">车辆行驶信息</a></li>
                    <li><a href="../DistCarRun/AnalysisByTrend">按费用走势分析</a></li>
                    <li><a href="../DistCarRun/CompareCarFee">各车辆费用比较</a></li>
                    <li><a href="../DistCarRun/YearRateOfCarYear">同比增长率(年)</a></li>
                    <li><a href="../DistCarRun/YearRateOfCarMonth">同比增长率(月)</a></li>
                    <li class="cur"><a href="../DistCarRun/LinkRateOfCar">环比增长率</a></li>
                </ul>
            </div>
            <div id="stores" class="r_con_wrap" style="padding-bottom:0px;min-height:90px;">
                <div class="control_btn" style="padding-bottom:0px;height:15px;margin-bottom:12px;">
                    <div class="tips_info"><strong>提示：</strong>按柱状图展示选定配送车辆的费用环比增长率</div>
                </div>
                <form class="r_con_search_form" method="get">
                    <label>年度：</label>
                   <%=Html.DropDownList("SelYear",ViewData["YearList"] as IList<SelectListItem>,new { @class = "ui-text-select"  }) %>
          
                    <label>配送线路：</label>
                    <%=Html.DropDownList("DeliveryLine",ViewData["DeliveryLineList"] as IList<SelectListItem>,new { @class = "ui-text-select" ,@onchange="generateReport();" }) %>
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

    }

    //根据条件生成统计报表
    function generateReport() {
        var carId = $("#DeliveryLine").val();
        var carName = $("#DeliveryLine").find("option:selected").text();
        var year = $("#SelYear").val();
        var param = { Year: year, CAR_ID: carId };
        $.ajax({
            url: "GetLinkRateOfCarData",
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
        var year = $("#SelYear").val();
        var lineName = $("#DeliveryLine").find("option:selected").text();
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
                text: year + '年' + lineName + '环比增长率',
                x: -20 //center
            },
            subtitle: {
                text: year + "年",
                x: -20
            },
            xAxis: {
                categories: data.xAxis

            },
            yAxis: {
                title: {
                    text: '环比增长率(%)'
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
