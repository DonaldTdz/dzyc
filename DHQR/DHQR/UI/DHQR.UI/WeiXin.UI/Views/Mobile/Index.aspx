<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/New_Mobile.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    婚礼导航
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div data-url="demo-page" data-role="page" data-theme="c" id="demo-page" class="my-page" style="position: relative; background-image: none; background-color: #fff;">
        <div data-role="content"  role="main" class="ui-content" style="background-color: #fff; position: relative; height: 470px;">
            <div style="width: 100%; padding-left: 30px;">
                <h1 style="font-size: 24px; color: rgb(57,123,72); display: inline-block;">吴磊&卿姮 婚礼导航</h1>
             </div>
            <img src="../../images/mobile/绿色横条.png" style="width: 100%;" />
            <div id="map" data-role="content" class="ui-content" style="background-color: #fff; position: relative; height: 88%;width:100%;"></div>
        </div>
        <div id="mbutton" style="margin-bottom:30px;">
	<%--<span class="" onclick="$('#mcover').show()"><i class="icon-share-alt"></i> 转发</span>
	<span class="" onclick="$('#mcover').show()"><i class="icon-group"></i> 分享</span>--%>
</div>
<div id="mcover" onclick="$(this).hide()"><img src="../../WeiXinImages/guide.png"></div>

        <!-- /footer -->
        <div data-role="footer" data-theme="c" class="footer">

            <div>
                        <a href="#" target="_self">欢迎您及家人的光临</a>
            </div>
            <div>
                <a href="#" target="_blank">由吴磊 提供技术支持 ^-^</a>
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

    <style type="text/css">
        .footer {
            position: absolute;
            bottom: 0px;
            width: 100%;
        }

            .footer div {
                text-align: center;
            }

            .footer > div > a {
                text-decoration: none;
                font-size: 12px;
                color: #999;
            }
        #mbutton{padding:15px 10px 15px 10px;  overflow:hidden;  border-bottom:1px #DDD solid; }
        #mbutton > span{float:right; display:inline-block; background:RGB(107,187,116); border:1px RGB(107,187,116) solid; color:#FFF; height:30px; line-height:30px; padding:0 10px; margin-left:10px;}
        #mcover{ position:fixed;top:0;left:0; width:100%;height:100%;background:rgba(0, 0, 0, 0.7); display:none; z-index:20000; }
        #mcover img{position:fixed;right: 18px;top:5px;width:260px;height:180px;z-index:20001;}
        span:hover
        {
            cursor:pointer;
        }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">

          <script type="text/javascript">
              var map, marker, currentLng, currentLat;
              var route_text, steps;
              var polyline;
              var isCurrentDlv = false;
              var toolBar, locationInfo;
              //起、终点
              var start_xy = new AMap.LngLat(105.81529900, 32.42820100);
              var end_xy = new AMap.LngLat(105.78299000, 32.43313400);

              $(function () {
         
                  resizeMap();
                  mapInit();
                  //移除版权的字母
                  $(".amap-copyright").remove();
                 
              });
              //初始化地图对象，加载地图
              function mapInit() {
                  var route_text, steps;
                 
                  //基本地图加载
                   map = new AMap.Map("map", {
                      resizeEnable: true,
                      view: new AMap.View2D({
                          center: new AMap.LngLat(116.397428, 39.90923),//地图中心点
                          zoom: 13 //地图显示的缩放级别
                      })
                  });
                  //地图中添加地图操作ToolBar插件
                  map.plugin(["AMap.ToolBar"], function () {
                      toolBar = new AMap.ToolBar(); //设置地位标记为自定义标记
                      map.addControl(toolBar);
                      AMap.event.addListener(toolBar, 'location', function callback(e) {
                          locationInfo = e.lnglat;
                          var start_xy = new AMap.LngLat(locationInfo.lng, locationInfo.lat);
                          //var start_xy = new AMap.LngLat(105.89456430, 32.24990775);
                          var end_xy = new AMap.LngLat(103.667915, 30.657185);
                          //驾车导航
                          function driving_route() {
                              var MDrive;
                              AMap.service(["AMap.Driving"], function () {
                                  var DrivingOption = {
                                      //驾车策略，包括 LEAST_TIME，LEAST_FEE, LEAST_DISTANCE,REAL_TRAFFIC
                                      policy: AMap.DrivingPolicy.LEAST_TIME
                                  };
                                  MDrive = new AMap.Driving(DrivingOption); //构造驾车导航类 
                                  //根据起终点坐标规划驾车路线
                                  MDrive.search(start_xy, end_xy, function (status, result) {
                                      if (status === 'complete' && result.info === 'OK') {
                                          driving_routeCallBack(result);
                                      } else {
                                          alert(result);
                                      }
                                  });
                              });
                          }
                          //导航结果展示
                          function driving_routeCallBack(data) {
                              var routeS = data.routes;
                              if (routeS.length <= 0) {

                              }
                              else {
                                  route_text = "";
                                  for (var v = 0; v < routeS.length; v++) {
                                      //驾车步骤数
                                      steps = routeS[v].steps
                                      var route_count = steps.length;
                                      //行车距离（米）
                                      var distance = routeS[v].distance;
                                      //拼接输出html
                                      for (var i = 0 ; i < steps.length; i++) {

                                      }
                                  }
                                  //输出行车路线指示

                                  drivingDrawLine();
                              }
                          }
                          //绘制驾车导航路线
                          function drivingDrawLine(s) {
                              addMarker(true, start_xy.lng, start_xy.lat);
                              addMarker(false, end_xy.lng, end_xy.lat);
                              //起点、终点图标
                              //var sicon = new AMap.Icon({
                              //    image: "http://api.amap.com/Public/images/js/poi.png",
                              //    size: new AMap.Size(44, 44),
                              //    imageOffset: new AMap.Pixel(-334, -180)
                              //});
                              //var startmarker = new AMap.Marker({
                              //    icon: sicon, //复杂图标
                              //    visible: true,
                              //    position: start_xy,
                              //    map: map,
                              //    offset: {
                              //        x: -16,
                              //        y: -40
                              //    }
                              //});
                              //var eicon = new AMap.Icon({
                              //    image: "http://api.amap.com/Public/images/js/poi.png",
                              //    size: new AMap.Size(44, 44),
                              //    imageOffset: new AMap.Pixel(-334, -134)
                              //});
                              //var endmarker = new AMap.Marker({
                              //    icon: eicon, //复杂图标
                              //    visible: true,
                              //    position: end_xy,
                              //    map: map,
                              //    offset: {
                              //        x: -16,
                              //        y: -40
                              //    }
                              //});
                              //起点到路线的起点 路线的终点到终点 绘制无道路部分
                              var extra_path1 = new Array();
                              extra_path1.push(start_xy);
                              extra_path1.push(steps[0].path[0]);
                              var extra_line1 = new AMap.Polyline({
                                  map: map,
                                  path: extra_path1,
                                  strokeColor: "#9400D3",
                                  strokeOpacity: 0.7,
                                  strokeWeight: 4,
                                  strokeStyle: "dashed",
                                  strokeDasharray: [10, 5]
                              });

                              var extra_path2 = new Array();
                              var path_xy = steps[(steps.length - 1)].path;
                              extra_path2.push(end_xy);
                              extra_path2.push(path_xy[(path_xy.length - 1)]);
                              var extra_line2 = new AMap.Polyline({
                                  map: map,
                                  path: extra_path2,
                                  strokeColor: "#9400D3",
                                  strokeOpacity: 0.7,
                                  strokeWeight: 4,
                                  strokeStyle: "dashed",
                                  strokeDasharray: [10, 5]
                              });

                              var drawpath = new Array();
                              for (var s = 0; s < steps.length; s++) {
                                  var plength = steps[s].path.length;
                                  for (var p = 0; p < plength; p++) {
                                      drawpath.push(steps[s].path[p]);
                                  }
                              }
                               polyline = new AMap.Polyline({
                                  map: map,
                                  path: drawpath,
                                  strokeColor: "#9400D3",
                                  strokeOpacity: 0.7,
                                  strokeWeight: 4,
                                  strokeDasharray: [10, 5]
                              });
                              map.setFitView();
                          }
                          //绘制驾车导航路段
                          function driveDrawFoldline(num) {
                              var drawpath1 = new Array();
                              drawpath1 = steps[num].path;
                              if (polyline != null) {
                                  polyline.setMap(null);
                              }
                              polyline = new AMap.Polyline({
                                  map: map,
                                  path: drawpath1,
                                  strokeColor: "#FF3030",
                                  strokeOpacity: 0.9,
                                  strokeWeight: 4,
                                  strokeDasharray: [10, 5]
                              });

                              map.setFitView(polyline);
                              
                          }
                          driving_route();
                        
                         
                      });
                     
                  });

                  AMap.event.addListener(map, 'complete', function () {
                      map.setFitView(polyline);
                      toolBar.doLocation();
                  });
                 
                  //while (locationInfo == undefined || locationInfo==null)
                  //{
                  //    continue;
                  //}
                  //起、终点
                  //var start_xy = new AMap.LngLat(locationInfo.lng, locationInfo.lat);
                 

              }

              /*---------------自定义Marker和窗体 -------------------*/
              //添加marker标记
              function addMarker(isStart,lng,lat) {


                  //自定义点标记内容   
                  var markerContent = document.createElement("div");
                  markerContent.className = "markerContentStyle";

                  //点标记中的图标
                  var markerImg = document.createElement("img");
                  markerImg.className = "markerlnglat";
                  //  markerImg.src = "../../DHQRImages/0S.png";
                  markerContent.appendChild(markerImg);
                  if (isStart) {
                      markerImg.src = "../../DHQRImages/startptn.png";
                  }
                  else  {
                      markerImg.src = "../../DHQRImages/endPnt.png";
                  }

                  //点标记中的文本
                  var markerSpan = document.createElement("span");
                  if (isStart) {
                      markerSpan.innerHTML = "您的位置"
                  }
                  else {
                      markerSpan.innerHTML = "婚礼现场"
                  }
                  markerContent.appendChild(markerSpan);


                  var currentPoi = new AMap.LngLat(lng, lat);
                  var currentmarker = new AMap.Marker({
                      map: map,
                      position: currentPoi, //位置 
                      offset: new AMap.Pixel(-18, -36), //相对于基点的偏移位置
                      draggable: false,  //是否可拖动
                      content: markerContent,  //自定义点标记覆盖物内容
                      icon: "http://webapi.amap.com/images/0.png" //复杂图标 
                  });

                  //if (!isStart) {
                  //    currentmarker.setAnimation("AMAP_ANIMATION_BOUNCE");
                  //}
                  if (!isStart) {
                      AMap.event.addListener(currentmarker, 'click', function () { //鼠标点击marker弹出自定义的信息窗体
                          SetCustWindow();
                          infoWindow.open(map, currentmarker.getPosition());
                      });
                  }

                  currentmarker.setMap(map);  //在地图上添加点
              }


              //实例化信息窗体
              var infoWindow = new AMap.InfoWindow({
                  isCustom: true,  //使用自定义窗体
                  content: createInfoWindow('方恒假日酒店&nbsp;&nbsp;<span style="font-size:11px;color:#F00;">婚礼地点</span>', "<img src='http://tpc.googlesyndication.com/simgad/5843493769827749134' style='position:relative;float:left;margin:0 5px 5px 0;'>地址：北京市朝阳区阜通东大街6号院3号楼 东北 8.3 公里<br/>电话：010 64733333<br/><a href='http://baike.baidu.com/view/6748574.htm'>详细信息</a>"),
                  offset: new AMap.Pixel(16, -45)//-113, -140
              });

              function SetCustWindow() {
                  //var title = "【易心园】" + '&nbsp;&nbsp;<span style="font-size:11px;color:#F00;">电话:' + currentData.TEL + '</span>';
                  //var content = "<img src='../../DHQRImages/logo.png' style='position:relative;float:left;margin:0 5px 5px 0;'>地址：" + currentData.ADDR + "<br/>电话：" + currentData.TEL + "<br/><a href='http://www.baidu.com/'>详细信息</a>";
                  infoWindow = new AMap.InfoWindow({
                      isCustom: true,  //使用自定义窗体
                      content: createInfoWindow('【易心园】&nbsp;&nbsp;<span style="font-size:11px;color:#F00;">婚礼地点</span>', "<img src='http://tpc.googlesyndication.com/simgad/5843493769827749134' style='position:relative;float:left;margin:0 5px 5px 0;'>地址：四川崇州市世纪广场华怀路街子古镇方向1500米 左边<br/>吴磊电话：18982150910<br/><a href='http://www.baidu.com'>详细信息</a>"),
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
                  map.clearInfoWindow();
              }
             

              function resizeMap() {
                  //var width = $("#navBar").width();
                  //var height = $("#frameContent").height() - 200;
                  //$("#map").width(width);
                  //$("#map").height(height);
              }


           



        </script>
    <style type="text/css">
        .markerContentStyle span
        {
           width:40px;
           
           color:black;
           font-family:微软雅黑;
        }
      
    </style>
  

</asp:Content>
