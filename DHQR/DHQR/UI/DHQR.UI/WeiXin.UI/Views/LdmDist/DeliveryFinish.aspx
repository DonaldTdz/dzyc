<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    配送任务分析
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
                    <li class="cur"><a href="../DistCarRun/AnalysisByTrend">完成率分析</a></li>
                </ul>
            </div>
            <div id="stores" class="r_con_wrap" style="padding-bottom:0px;min-height:90px;">
                <div class="control_btn" style="padding-bottom:0px;height:15px;margin-bottom:12px;">
                    <div class="tips_info"><strong>提示：</strong>此报表分析每日各线路的任务完成率</div>
                </div>
                <form class="r_con_search_form" method="get">
                    <label>配送日期：</label>
                    <input type="text" name="distDate" id="distDate" class="form_input" style="margin: 0px; padding: 0px; width: 150px;" value="<%=DateTime.Now.ToString("yyyy-MM-dd") %>" />
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
    <script src="../../Scripts/Highcharts-4.0.3/adapters/standalone-framework.js"></script>
    <script src="../../Scripts/Highcharts-4.0.3/themes/grid.js"></script>
    <script src="../../Scripts/Highcharts-4.0.3/highcharts-more.js"></script>
    <script src="../../Scripts/Highcharts-4.0.3/modules/exporting.js"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
<script type="text/javascript">

    $(function () {
        initDate();
        generateReport();
    });

    function initDate() {
        $('#distDate').datepicker({
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
        var distDate = $("#distDate").val(),  keyWord = $("#Keyword").val();
        var param = { DistDate: distDate };
        $.ajax({
            url: "GetLdmDistFinishRate",
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
        var distDate = $("#distDate").val(), keyWord = $("#Keyword").val();
        for (i = 0; i < data.data.length;i++)
        {
            var info = [0, data.data[i]];
            chartData[i] = info;
        }
        $('#charContainer').highcharts({
            chart: {
                type: 'columnrange',
                marginRight: 130,
                marginBottom: 25,
                inverted: true
            },
            title: {
                text: '配送任务完成率',
                x: -20 //center
            },
            subtitle: {
                text: distDate,
                x: -20
            },
            xAxis: {
                categories: data.xAxis

            },
            yAxis: {
                title: {
                    text: '完成率(%)'
                }
                ,
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            tooltip: {
                valueSuffix: '%'
            },
            plotOptions: {
                columnrange: {
                    dataLabels: {
                        enabled: true,
                        formatter: function () {
                            return this.y + '%';
                        }
                    }
                }
            },
            legend: {
                //layout: 'vertical',
                //align: 'right',
                //verticalAlign: 'top',
                //x: -10,
                //y: 100,
                //borderWidth: 0
                enabled: false
               
            },
            series: [{ name: '完成率', data: chartData }]
            ,
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
