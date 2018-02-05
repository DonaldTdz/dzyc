<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    人员定位查询
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <div id="iframe_page">
     <div id="frameContent" class="iframe_content">
      <div class="r_nav" id="navBar">
                <ul>
                    <li class="cur"><a href="../GisLastLocrecord/Index">人员定位查询</a></li>
                     <li><a href="../GisLastLocrecord/DlvmanLine">人员轨迹回放</a></li>
                </ul>
      </div>
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
    <link rel="stylesheet" type="text/css" href="http://developer.amap.com/Public/css/demo.Default.css" />
    <script src="http://webapi.amap.com/maps?v=1.3&key=3f4732eca0ff79d46e4379bac44775de" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
    <script type="text/javascript">
        
        $(function () {
            resizeMap();
            mapInit();
            //移除版权的字母
            $(".amap-copyright").remove();
        });
        var mapObj, marker,currentLng, currentLat;;
        //初始化地图对象，加载地图
        function mapInit() {
            currentLng = "105.843353";
            currentLat = "32.435389";
            mapObj = new AMap.Map("map", {
                rotateEnable: true,
                dragEnable: true,
                zoomEnable: true,
                //二维地图显示视口
                view: new AMap.View2D({
                    center: new AMap.LngLat(currentLng, currentLat),//地图中心点
                    zoom: 13 //地图显示的缩放级别
                })
            });
            //在地图中添加ToolBar插件
            mapObj.plugin(["AMap.ToolBar"], function () {
                toolBar = new AMap.ToolBar();
                mapObj.addControl(toolBar);
            });
            //toolBar.hideDirection();
            //为地图注册click事件获取鼠标点击出的经纬度坐标
            var clickEventListener = AMap.event.addListener(mapObj, 'click', function (e) {
            });
            AMap.event.addListener(mapObj, 'complete', function () {
                GetGisDatas();
            });
        }

        function GetGisDatas()
        {
            $.ajax({
                url: "GetLatestGisInfos",
                type: "POST",
                success: function (data) {
                        addMarkers(data);
                },
                error: function (data) {

                }
            });

        }

        //添加带文本的点标记覆盖物      
        function addMarkers(datas) {

            for (var i = 0; i < datas.length; i++) {
                //自定义点标记内容   
                var markerContent = document.createElement("div");
                markerContent.className = "markerContentStyle";

                //点标记中的图标
                var markerImg = document.createElement("img");
                markerImg.className = "markerlnglat";
                markerImg.src = "http://webapi.amap.com/images/0.png";
                markerContent.appendChild(markerImg);

                //点标记中的文本
                var markerSpan = document.createElement("span");
                markerSpan.innerHTML = datas[i].CAR_NAME;
                markerContent.appendChild(markerSpan);
                marker = new AMap.Marker({
                    map: mapObj,
                    position: new AMap.LngLat(datas[i].ORIGINAL_LONGITUDE, datas[i].ORIGINAL_LATITUDE), //基点位置
                    offset: new AMap.Pixel(-18, -36), //相对于基点的偏移位置
                    draggable: false,  //是否可拖动
                    content: markerContent   //自定义点标记覆盖物内容
                });
                marker.setMap(mapObj);  //在地图上添加点
            }
        }

        function resizeMap()
        {
            var width = $("#navBar").width();
            var height = $("#frameContent").height()-150;
            $("#map").width(width);
            $("#map").height(height);
        }
</script>

    <style type="text/css">
        .markerContentStyle span
        {
           color:black;
           font-family:微软雅黑;
        }
      
    </style>
</asp:Content>
