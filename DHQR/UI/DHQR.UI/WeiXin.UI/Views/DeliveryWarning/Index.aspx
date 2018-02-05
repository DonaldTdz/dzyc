<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    配送点预警
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 <div id="iframe_page">
     <div id="frameContent" class="iframe_content">
      <div class="r_nav" id="navBar">
                <ul>
                    <li class="cur"><a href="#">配送点预警</a></li>
                </ul>
      </div>      
      <form class="r_con_search_form" method="get">
                    <label>配送线路：</label>
                    <%=Html.DropDownList("DeliveryLine",ViewData["DeliveryLineList"] as IList<SelectListItem>,new { @class = "ui-text-select" ,@onchange="searchStore();" }) %>
                    <label>配送日期：</label>
                    <input type="text" name="DistDate" id="DistDate" class="form_input" style="margin: 0px; padding: 0px; width: 150px;"  />
                    <label>配送单号：</label>
                    <input type="text" name="DIST_NUM" id="DIST_NUM" value="" class="form_input" size="15" />
                    <input type="button" class="search_btn" value="搜索"  onclick="searchStore()"/>
       </form>                 
      <div class="r_con_form" style="width:1000px;height:600px;">    
      <div id="map"></div>
      </div>
    
    <div id="selectProductDialog" title="显示预警明细">
        <div id="mapDetail">
            <%--<div id="mapDetail"></div>--%>
        </div>
        
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
    <link href="../../Content/JqueryUi/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="http://developer.amap.com/Public/css/demo.Default.css" />
    <script src="http://webapi.amap.com/maps?v=1.3&key=3f4732eca0ff79d46e4379bac44775de" type="text/javascript"></script>

    <script src="../../Scripts/jqueryUi/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="../../Scripts/jqueryUi/timepicker.js" type="text/javascript"></script>
    <script src="../../Scripts/jqueryUi/ui.datepicker-zh-CN.js" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
        <script type="text/javascript">
            var mapObj, marker, currentLng, currentLat,mapDetailObj;
            var currentSelData;
            $(function () {
                initDate();
                resizeMap();
                mapInit();
                //移除版权的字母
                $(".amap-copyright").remove();
                initDialog();
            });
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
                //地图类型切换
                mapObj.plugin(["AMap.MapType"], function () {
                    var type = new AMap.MapType({ defaultType: 0 });//初始状态使用2D地图
                    mapObj.addControl(type);
                });

                //toolBar.hideDirection();
                //为地图注册click事件获取鼠标点击出的经纬度坐标
                var clickEventListener = AMap.event.addListener(mapObj, 'click', function (e) {

                });
                AMap.event.addListener(mapObj, 'complete', function () {
                    GetWarningDatas();
                });

            }


            //从后台获取配送点预警信息
            function GetWarningDatas() {
                $.ajax({
                    url: "GetWarningDatas",
                    type: "GET",
                    success: function (data) {
                        for (var i = 0; i < data.length; i++) {
                            addMarker(data[i]);
                        }
                    },
                    error: function (data) {
                    }
                });

            }


            /*---------------自定义Marker和窗体 -------------------*/
            //添加marker标记
            function addMarker(currentData) {


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
                markerSpan.innerHTML =  currentData.MANAGER;
                markerContent.appendChild(markerSpan);


                var currentPoi = new AMap.LngLat(currentData.RealLongitude, currentData.RealLatitude);
                var currentmarker = new AMap.Marker({
                    map: mapObj,
                    position: currentPoi, //位置 
                    offset: new AMap.Pixel(-18, -36), //相对于基点的偏移位置
                    draggable: false,  //是否可拖动
                    content: markerContent,  //自定义点标记覆盖物内容
                    icon: "http://webapi.amap.com/images/0.png" //复杂图标       
                });

               

                AMap.event.addListener(currentmarker, 'click', function () { //鼠标点击marker弹出自定义的信息窗体
                    SetCustWindow(currentData);
                    currentSelData = currentData;
                    infoWindow.open(mapObj, currentmarker.getPosition());
                });

                currentmarker.setMap(mapObj);  //在地图上添加点
            }


            //实例化信息窗体
            var infoWindow = new AMap.InfoWindow({
                isCustom: true,  //使用自定义窗体
                content: createInfoWindow('方恒假日酒店&nbsp;&nbsp;<span style="font-size:11px;color:#F00;">价格:318</span>', "<img src='http://tpc.googlesyndication.com/simgad/5843493769827749134' style='position:relative;float:left;margin:0 5px 5px 0;'>地址：北京市朝阳区阜通东大街6号院3号楼 东北 8.3 公里<br/>电话：010 64733333<br/><a href='http://baike.baidu.com/view/6748574.htm'>详细信息</a>"),
                offset: new AMap.Pixel(16, -45)//-113, -140
            });

            function SetCustWindow(currentData) {
                var title = currentData.CUST_NAME + '&nbsp;&nbsp;<span style="font-size:11px;color:#F00;">代码:' + currentData.CUST_CODE + '</span>';
                var content = "<img src='../../DHQRImages/logo.png' style='position:relative;float:left;margin:0 5px 5px 0;'>异常原因：" + currentData.Reason + "<br/>电话：" + currentData.TEL + "<br/><a href='#' onclick='showDetail()'>详细信息</a>";
                infoWindow = new AMap.InfoWindow({
                    isCustom: true,  //使用自定义窗体
                    content: createInfoWindow(title, content),
                    offset: new AMap.Pixel(16, -45)//-113, -140
                });
            }


            //构建自定义信息窗体	
            function createInfoWindow(title, content) {
                var info = document.createElement("div");
                info.className = "info";

                //可以通过下面的方式修改自定义窗体的宽高
                //info.style.width = "400px";

                // 定义顶部标题
                var top = document.createElement("div");
                top.className = "info-top";
                var titleD = document.createElement("div");
                titleD.innerHTML = title;
                var closeX = document.createElement("img");
                closeX.src = "http://webapi.amap.com/images/close2.gif";
                closeX.onclick = closeInfoWindow;

                top.appendChild(titleD);
                top.appendChild(closeX);
                info.appendChild(top);


                // 定义中部内容
                var middle = document.createElement("div");
                middle.className = "info-middle";
                middle.style.backgroundColor = 'white';
                middle.innerHTML = content;
                info.appendChild(middle);

                // 定义底部内容
                var bottom = document.createElement("div");
                bottom.className = "info-bottom";
                bottom.style.position = 'relative';
                bottom.style.top = '0px';
                bottom.style.margin = '0 auto';
                var sharp = document.createElement("img");
                sharp.src = "http://webapi.amap.com/images/sharp.png";
                bottom.appendChild(sharp);
                info.appendChild(bottom);
                return info;
            }

            //关闭信息窗体
            function closeInfoWindow() {
                mapObj.clearInfoWindow();
            }


            function resizeMap() {
                var width = $("#navBar").width();
                var height = $("#frameContent").height() - 200;
                $("#map").width(width);
                $("#map").height(height);
            }


            function initDate() {
                $('#DistDate').datepicker({
                    format: 'yyyy-MM-dd',
                    autoclose: true,
                    todayBtn: 'linked',
                    onClose: function (dateText, inst) {
                    }
                });

            }


            //初始化添加产品dialog
            function initDialog() {
               
                $("#selectProductDialog").dialog({
                    autoOpen: false,
                    modal: true,
                    width: 600,
                    height: 500,
                    resizable: true,
                    position: ['center', 'center'],
                    buttons: {
                        //"确定": function () {
                           
                        //    $(this).dialog("close");
                        //},
                        "关闭": function () {
                            $(this).dialog("close");
                        }
                    }
                });
            }


            function showDetail()
            {
                $("#selectProductDialog").dialog("option", "title", currentSelData.MANAGER+"-"+currentSelData.Reason);
                $("#selectProductDialog").dialog("open");
                //$("#selectProductDialog")

                //$("#mapDetail").show();
                mapDetailObj = new AMap.Map("selectProductDialog", {
                    rotateEnable: true,
                    dragEnable: true,
                    zoomEnable: true,
                    //二维地图显示视口
                    view: new AMap.View2D({
                        center: new AMap.LngLat(currentSelData.Longitude, currentSelData.Latitude),//地图中心点
                        zoom: 13 //地图显示的缩放级别
                    })
                });


                AMap.event.addListener(mapDetailObj, 'complete', function () {
                    SetDetailPoint(currentSelData.Longitude, currentSelData.Latitude, "经营位置");
                    SetDetailPoint(currentSelData.RealLongitude, currentSelData.RealLatitude, "实际位置");
                });


            }


            function SetDetailPoint(lx,ly,name)
            {
                //添加点
                //自定义点标记内容   
                var markerContent = document.createElement("div");
                markerContent.className = "markerContentStyle";

                var orignalPoint = new AMap.LngLat(lx, ly);

                //点标记中的图标
                var markerImg = document.createElement("img");
                markerImg.className = "markerlnglat";
                markerImg.src = "http://webapi.amap.com/images/0.png";
                markerContent.appendChild(markerImg);

                //点标记中的文本
                var markerSpan = document.createElement("span");
                markerSpan.innerHTML = name;
                markerContent.appendChild(markerSpan);

                //当前点
                var currentmarker = new AMap.Marker({
                    map: mapDetailObj,
                    position: orignalPoint, //位置 
                    offset: new AMap.Pixel(-18, -36), //相对于基点的偏移位置
                    draggable: false,  //是否可拖动
                    content: markerContent,  //自定义点标记覆盖物内容
                    icon: "http://webapi.amap.com/images/0.png" //复杂图标       
                });


                currentmarker.setMap(mapDetailObj);  //在地图上添加点

            }

</script>
    <style type="text/css">
        .markerContentStyle span
        {
           width:70px;
           color:black;
           font-family:微软雅黑;
        }
      
    </style>

</asp:Content>
