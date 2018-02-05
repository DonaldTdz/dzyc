<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    图文群发
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
                    <li class="cur"><a href="#">图文群发</a></li>
                    <li><a href="#">文字群发</a></li>
                    <li><a href="#">图片群发</a></li>
                    <li><a href="#">语音群发</a></li>
                    <li><a href="#">视频群发</a></li>
                </ul>
            </div>
            <div id="stores" class="r_con_wrap" style="padding-bottom:0px;min-height:90px;">
                <div class="control_btn" style="padding-bottom:0px;height:15px;margin-bottom:12px;">
                    <div class="tips_info"><strong>提示：</strong>为保障用户体验，微信公众平台严禁恶意营销以及诱导分享朋友圈，严禁发布色情低俗、暴力血腥、政治谣言等各类违反法律法规及相关政策规定的信息。一旦发现，我们将严厉打击和处理。 </div>
                </div>
                <form class="r_con_search_form" method="get">
                     <label>配送员：</label>
                    <%=Html.DropDownList("DLVMAN_ID",ViewData["DlvManList"] as IList<SelectListItem>,new { @class = "ui-text-select" ,@onchange="generateReport();" }) %>

                    <label>日期：</label>
                    <input type="text" name="startTime" id="startTime" class="form_input" style="margin: 0px; padding: 0px; width: 150px;" value="<%=DateTime.Now.ToString("yyyy-MM-dd")%>" />
                    <label>关键字：</label>
                    <input type="text" name="Keyword" value="" class="form_input" size="15" />
                    <%--<label>配送员：</label>
                    <%=Html.DropDownList("RecieveTypeName",ViewData["RecieveTypeList"] as IList<SelectListItem>,new { @class = "ui-text-select" ,@onchange="generateReport();" }) %>--%>
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

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
    <script type="text/javascript">

        $(function () {
            initDate();
            generateReport();
            setContainer();
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

        }

        //根据条件生成统计报表
        function generateReport() {
            var dlvman = $("#DLVMAN_ID").val();
            var dlvManName = $("#DLVMAN_ID").find("option:selected").text();
            var startDate = $("#startTime").val(), keyWord = $("#Keyword").val();
            var param = { DistTime: startDate, KeyWord: keyWord, DLVMAN_ID: dlvman };
            $.ajax({
                url: "GetSatisfyByDayData",
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
            var startDate = $("#startTime").val(), keyWord = $("#Keyword").val(), dlvman = $("#DLVMAN_ID").val();
            for (var i = 0; i < data.length; i++) {
                var info = [];
                info[0] = data[i].name; info[1] = data[i].data;
                chartData[i] = info;
            }
            $('#charContainer').highcharts({
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                    , options3d:
                        {
                            enabled: true,
                            alpha: 45,
                            beta: 0
                        }
                },
                title: {
                    text: '配送员【' + (dlvman == "" ? "全部" : dlvman) + '】在【' + startDate + '】的满意度分析',
                    x: -20 //center
                },
                //subtitle: {
                //    text: startDate,
                //    x: -20 //center
                //},
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage}%</b>',
                    percentageDecimals: 2
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        showInLegend: true,
                        depth: 35,
                        dataLabels: {
                            enabled: true,
                            //color: '#FEC701',
                            //connectorColor: '#FEFA01',
                            formatter: function () {
                                return '<b>' + this.point.name + '(' + this.point.y + ')</b>:<br/> ' + this.percentage.toPrecision(4) + ' %';
                            }
                        }
                    }
                },
                series: [{
                    type: 'pie',
                    name: '占比',
                    data: chartData
                }],
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
                            }, {
                                text: '报表导出为 PNG',
                                onclick: function () {
                                    this.exportChart();
                                }
                            }, {
                                text: '报表导出为 PDF',
                                onclick: function () {
                                    this.exportChart({
                                        type: 'application/pdf'
                                    });
                                }
                            }]
                        }
                    }
                }

            });

        }


        function setContainer() {
            var width = $("#stores").width();
            $("#charContainer").width(width);
        }

    </script>
</asp:Content>
