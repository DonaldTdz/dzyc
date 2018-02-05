<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    人员轨迹回放
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <div id="iframe_page">
     <div id="frameContent" class="iframe_content">
      <div class="r_nav" id="navBar">
                <ul>
                    <li><a href="../GisLastLocrecord/Index">人员定位查询</a></li>
                     <li class="cur"><a href="../GisLastLocrecord/DlvmanLine">人员轨迹回放</a></li>
                </ul>
      </div>
      <form class="r_con_search_form" method="get">
                    <label>开始时间：</label>
                    <input type="text" name="startDate" id="startDate" class="form_input" style="margin: 0px; padding: 0px; width: 150px;" value="<%=DateTime.Now.AddHours(-8).ToString("yyyy-MM-dd HH:mm:ss") %>" />
                    <label>结束时间：</label>
                    <input type="text" name="endDate" id="endDate" class="form_input" style="margin: 0px; padding: 0px; width: 150px;" value="<%=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") %>" />
                    <label>配送员：</label>
                    <%=Html.DropDownList("DLVMAN_ID",ViewData["DlvManList"] as IList<SelectListItem>,new { @class = "ui-text-select" ,@onchange="completeEventHandler();"}) %>
          <input type="button" class="search_btn" value="搜索"  onclick="completeEventHandler()"/>
                    <input type="button" class="search_btn" value="重新回放"  onclick="startAnimation()"/>
       </form>                 
      <div class="r_con_form" style="width:1000px;height:600px;">    
      <div id="map"></div>
      </div>
     </div>
  </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
    <link href="../../DHQRCss/global.css" rel="stylesheet" type="text/css" />
    <link href="../../DHQRCss/frame.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/themes/base/jquery-ui.css" rel="stylesheet" />
    <link href="../../Css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/Box/Content.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/themes/base/jquery-ui.css" rel="stylesheet" />
    <link href="../../Css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/Box/Content.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/JqueryUi/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" />

    <link rel="stylesheet" type="text/css" href="http://developer.amap.com/Public/css/demo.Default.css" />
    <script src="http://webapi.amap.com/maps?v=1.3&key=3f4732eca0ff79d46e4379bac44775de" type="text/javascript"></script>
    <script src="../../Scripts/jqueryUi/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="../../Scripts/jqueryUi/timepicker.js" type="text/javascript"></script>
    <script src="../../Scripts/jqueryUi/ui.datepicker-zh-CN.js" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
<script type="text/javascript">
    var mapObj;
    var marker;
    var polyline;
    var isfirst = true;
    $(function () {
        initDate();
        mapInit();
        //移除版权的字母
        $(".amap-copyright").remove();
        resizeMap();
    });

    function GetGisDatas() {
        

    }


    //初始化地图对象，加载地图
    function mapInit() {      
        mapObj = new AMap.Map("map", {
            //二维地图显示视口
            view: new AMap.View2D({
                center: new AMap.LngLat(116.397428, 39.90923),//地图中心点
                zoom: 17 //地图显示的缩放级别
            }),
            continuousZoomEnable: false
        });
        AMap.event.addListener(mapObj, "complete", completeEventHandler);
    }

    //地图图块加载完毕后执行函数
    function completeEventHandler() {
        if (!isfirst) {
            if (polyline != undefined) {
                polyline.setMap(null);
                marker.setMap(null);
            }
        }
        isfirst = false;
        var startTime = $("#startDate").val(), endTime = $("#endDate").val(), DLVMAN_ID = $("#DLVMAN_ID").val();
        var param = { StartDate: startTime, EndDate: endTime, DLVMAN_ID: DLVMAN_ID };
        $.ajax({
            url: "GetGisInfosOfDlvman",
            type: "POST",
            data:param,
            success: function (data) {
                var startPiont = data[0];
                if (startPiont == null)
                {
                    return;
                }
                marker = new AMap.Marker({
                    map: mapObj,
                    //draggable:true, //是否可拖动
                    position: new AMap.LngLat(data[0].ORIGINAL_LONGITUDE, data[0].ORIGINAL_LATITUDE),//基点位置
                    icon: "http://code.mapabc.com/images/car_03.png", //marker图标，直接传递地址url
                    offset: new AMap.Pixel(-26, -13), //相对于基点的位置
                    autoRotation: true
                });

                var lngX = data[0].ORIGINAL_LONGITUDE;
                var latY = data[0].ORIGINAL_LATITUDE;
                lineArr = new Array();
                lineArr.push(new AMap.LngLat(lngX, latY));
                for (var i = 1; i < data.length; i++) {
                    var cx = data[i].ORIGINAL_LONGITUDE;
                    var cy = data[i].ORIGINAL_LATITUDE;
                    lineArr.push(new AMap.LngLat(cx, cy));
                }
                //绘制轨迹
                 polyline = new AMap.Polyline({
                    map: mapObj,
                    path: lineArr,
                    strokeColor: "#00A",//线颜色
                    strokeOpacity: 1,//线透明度
                    strokeWeight: 3,//线宽
                    strokeStyle: "solid"//线样式
                });
                mapObj.setFitView();
                startAnimation();
            },
            error: function (data) {

            }
        });

    }
    function startAnimation() {
        marker.moveAlong(lineArr, 500);
    }
    function stopAnimation() {
        marker.stopMove();
    }

    function resizeMap() {
        var width = $("#navBar").width();
        var height = $("#frameContent").height() - 150;
        $("#map").width(width);
        $("#map").height(height);
    }

    function initDate() {
        $('#startDate').datetimepicker({
            //format: 'yyyy-MM-dd HH:mm:ss',
            dateFormat: 'yy-mm-dd',
            timeFormat: 'hh:mm:ss',//格式化时间
            autoclose: true,
            todayBtn: 'linked',
            showSecond: true,
            onClose: function (dateText, inst) {
            }
        });
        $('#endDate').datetimepicker({
            //format: 'yyyy-MM-dd HH:mm:ss',
            dateFormat: 'yy-mm-dd',
            timeFormat: 'hh:mm:ss',//格式化时间
            autoclose: true,
            todayBtn: 'linked',
            showSecond: true,
            onClose: function (dateText, inst) {
            }
        });
    }

</script>

    <style type="text/css">
        .markerContentStyle span
        {
           color:black;
           font-family:微软雅黑;
        }
        /* css for timepicker */

.ui-timepicker-div .ui-widget-header { margin-bottom: 8px; }

.ui-timepicker-div dl { text-align: left; }

.ui-timepicker-div dl dt { float: left; clear:left; padding: 0 0 0 5px; }

.ui-timepicker-div dl dd { margin: 0 10px 10px 45%; }

.ui-timepicker-div td { font-size: 90%; }

.ui-tpicker-grid-label { background: none; border: none; margin: 0; padding: 0; }


.ui-timepicker-rtl{ direction: rtl; }

.ui-timepicker-rtl dl { text-align: right; padding: 0 5px 0 0; }

.ui-timepicker-rtl dl dt{ float: right; clear: right; }

.ui-timepicker-rtl dl dd { margin: 0 45% 10px 10px; }

      
    </style>
</asp:Content>
