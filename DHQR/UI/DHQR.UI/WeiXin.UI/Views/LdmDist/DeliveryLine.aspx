<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    配送线路查询
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 <div id="iframe_page">
     <div id="frameContent" class="iframe_content">
      <div class="r_nav" id="navBar">
                <ul>
                    <li class="cur"><a href="../Retailer/Index">配送线路查询</a></li>
                </ul>
      </div>      
      <form class="r_con_search_form" method="get">
                    <label>配送线路：</label>
                    <%=Html.DropDownList("DeliveryLine",ViewData["DeliveryLineList"] as IList<SelectListItem>,new { @class = "ui-text-select"  }) %>
                    <label>配送日期：</label>
                    <input type="text" name="DistDate" id="DistDate" value="<%=DateTime.Now.ToString("yyyy-MM-dd")%>" class="form_input" style="margin: 0px; padding: 0px; width: 150px;"  />
                    <label>配送单号：</label>
                    <input type="text" name="DIST_NUM" id="DIST_NUM" value="" class="form_input" size="15" />
                    <input type="button" class="search_btn" value="搜索"  onclick="GetPathDatas()"/>
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
    <link href="../../Content/JqueryUi/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="http://developer.amap.com/Public/css/demo.Default.css" />
    <script src="http://webapi.amap.com/maps?v=1.3&key=3f4732eca0ff79d46e4379bac44775de" type="text/javascript"></script>

    <script src="../../Scripts/jqueryUi/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="../../Scripts/jqueryUi/timepicker.js" type="text/javascript"></script>
    <script src="../../Scripts/jqueryUi/ui.datepicker-zh-CN.js" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
        <script type="text/javascript">
            var mapObj, marker, currentLng, currentLat;
            var route_text, steps;
            var polyline;
            var isCurrentDlv = false;
            //起、终点
            var start_xy = new AMap.LngLat(105.81529900, 32.42820100);
            var end_xy = new AMap.LngLat(105.78299000, 32.43313400);

            $(function () {
                initDate();
                resizeMap();
                mapInit();
                //移除版权的字母
                $(".amap-copyright").remove();
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
                    GetPathDatas();
                });

            }

            //从后台获取线路信息
            function GetPathDatas() {
                var distDate = $("#DistDate").val();
                var rutId = $("#DeliveryLine").val();
                var param = { DIST_DATE: distDate, RUT_ID: rutId };
                $.ajax({
                    url: "GetDeliveryLine",
                    type: "POST",
                    data: param,
                    success: function (data) {
                        for (var i = 0; i < data.length; i++)
                        {
                            if (i < data.length - 1) {
                                start_xy = new AMap.LngLat(data[i].CustLongitude, data[i].CustLatitude);
                                end_xy = new AMap.LngLat(data[i + 1].CustLongitude, data[i + 1].CustLatitude);
                                driving_route(data[i].HasConfirm);
                            }
                            if (i > 0) {
                                if (data[i - 1].HasConfirm && !data[i].HasConfirm) {
                                    isCurrentDlv = true;
                                }
                            }

                            if (i == 0) {
                                addMarker(data[i], true, false);
                            }
                            else if (i == data.length - 1) {
                                addMarker(data[i], false, true);
                            }
                            else {
                                addMarker(data[i], false, false);
                            }
                        }
                    },
                    error: function (data) {
                    }
                });

            }

            //驾车导航
            function driving_route(HasConfirm) {
                var MDrive;
                AMap.service(["AMap.Driving"], function () {
                    var DrivingOption = {
                        //驾车策略，包括 LEAST_TIME，LEAST_FEE, LEAST_DISTANCE,REAL_TRAFFIC
                        policy: AMap.DrivingPolicy.REAL_TRAFFIC
                    };
                    MDrive = new AMap.Driving(DrivingOption); //构造驾车导航类 
                    //根据起终点坐标规划驾车路线
                    MDrive.search(start_xy, end_xy, function (status, result) {
                        if (status === 'complete' && result.info === 'OK') {
                            driving_routeCallBack(result, HasConfirm);
                        } else {
                            alert(result);
                        }
                    });
                });
            }

            //导航结果处理
            function driving_routeCallBack(data, HasConfirm) {
                var routeS = data.routes;
                if (routeS.length <= 0) {
                    alert("无法进行导航！");
                }
                else {
                    route_text="";
                    for (var v = 0; v < routeS.length; v++) {
                        //驾车步骤数
                        steps = routeS[v].steps
                        var route_count = steps.length;
                        //行车距离（米）
                        var distance = routeS[v].distance;
                    }
                    drivingDrawLine(HasConfirm);
                }
            }

            //绘制驾车导航路线
            function drivingDrawLine(HasConfirm) {

                var drawpath = new Array();
                for (var s = 0; s < steps.length; s++) {
                    var plength = steps[s].path.length;
                    for (var p = 0; p < plength; p++) {
                        drawpath.push(steps[s].path[p]);
                    }
                }
                if (HasConfirm) {
                    var polyline = new AMap.Polyline({
                        map: mapObj,
                        path: drawpath,
                        strokeColor: "#6600FF",
                        strokeOpacity: 0.8,
                        strokeWeight: 6,
                        strokeDasharray: [10, 5]
                    });
                }
                else {
                    var polyline = new AMap.Polyline({
                        map: mapObj,
                        path: drawpath,
                        strokeColor: "#F70909",
                        strokeOpacity: 0.6,
                        strokeWeight: 4,
                        strokeDasharray: [10, 5]
                    });
                }
                mapObj.setFitView();
            }

            /*---------------自定义Marker和窗体 -------------------*/
            //添加marker标记
            function addMarker(currentData,isStart,isEnd) {
 

                  //自定义点标记内容   
                  var markerContent = document.createElement("div");
                  markerContent.className = "markerContentStyle";

                  //点标记中的图标
                  var markerImg = document.createElement("img");
                  markerImg.className = "markerlnglat";
                //  markerImg.src = "../../DHQRImages/0S.png";
                  markerImg.src = "http://webapi.amap.com/images/0.png";
                  markerContent.appendChild(markerImg);
                  if (isStart)
                  {
                      markerImg.src = "../../DHQRImages/startptn.png";
                  }
                  if (isEnd)
                  {
                      markerImg.src = "../../DHQRImages/endPnt.png";
                  }

                  //点标记中的文本
                  var markerSpan = document.createElement("span");
                  markerSpan.innerHTML = currentData.SEQ;//+"-"+ currentData.MANAGER;
                  markerContent.appendChild(markerSpan);

               
                  var currentPoi = new AMap.LngLat(currentData.CustLongitude, currentData.CustLatitude);
                  var  currentmarker = new AMap.Marker({
                      map: mapObj,
                      position: currentPoi, //位置 
                      offset: new AMap.Pixel(-18, -36), //相对于基点的偏移位置
                      draggable: false,  //是否可拖动
                      content: markerContent,  //自定义点标记覆盖物内容
                      icon: "http://webapi.amap.com/images/0.png" //复杂图标 
                      //icon: "../../DHQRImages/0S.png" //复杂图标
                  });

                  if (isCurrentDlv) {
                      currentmarker.setAnimation("AMAP_ANIMATION_BOUNCE");
                      isCurrentDlv = false;
                  }
                  if (isStart)
                  {
                      //currentmarker.setIcon(sicon);
                  }
                  if (isEnd)
                  {

                  }
                 
                  AMap.event.addListener(currentmarker, 'click', function () { //鼠标点击marker弹出自定义的信息窗体
                    SetCustWindow(currentData);
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
                var title = currentData.CUST_NAME + '&nbsp;&nbsp;<span style="font-size:11px;color:#F00;">电话:' + currentData.TEL + '</span>';
                var content = "<img src='../../DHQRImages/logo.png' style='position:relative;float:left;margin:0 5px 5px 0;'>地址：" + currentData.ADDR + "<br/>电话：" + currentData.TEL + "<br/><a href='http://www.baidu.com/'>详细信息</a>";
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



        </script>
    <style type="text/css">
        .markerContentStyle span
        {
           width:20px;
           
           color:black;
           font-family:微软雅黑;
        }
      
    </style>

</asp:Content>
