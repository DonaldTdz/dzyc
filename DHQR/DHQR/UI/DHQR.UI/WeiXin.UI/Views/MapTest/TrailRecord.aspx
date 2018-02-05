<!DOCTYPE HTML>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>轨迹回放</title>
<link rel="stylesheet" type="text/css" href="http://developer.amap.com/Public/css/demo.Default.css" /> 
<script language="javascript" src="http://webapi.amap.com/maps?v=1.3&key=您申请的key值"></script>
<script language="javascript">
    var mapObj;
    //初始化地图对象，加载地图
    function mapInit() {
        mapObj = new AMap.Map("iCenter", {
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
        marker = new AMap.Marker({
            map: mapObj,
            //draggable:true, //是否可拖动
            position: new AMap.LngLat(116.397428, 39.90923),//基点位置
            icon: "http://code.mapabc.com/images/car_03.png", //marker图标，直接传递地址url
            offset: new AMap.Pixel(-26, -13), //相对于基点的位置
            autoRotation: true
        });

        var lngX = 116.397428;
        var latY = 39.90923;
        lineArr = new Array();
        lineArr.push(new AMap.LngLat(lngX, latY));
        for (var i = 1; i < 3; i++) {
            lngX = lngX + Math.random() * 0.05;
            if (i % 2) {
                latY = latY + Math.random() * 0.0001;
            } else {
                latY = latY + Math.random() * 0.06;
            }
            lineArr.push(new AMap.LngLat(lngX, latY));
        }
        //绘制轨迹
        var polyline = new AMap.Polyline({
            map: mapObj,
            path: lineArr,
            strokeColor: "#00A",//线颜色
            strokeOpacity: 1,//线透明度
            strokeWeight: 3,//线宽
            strokeStyle: "solid"//线样式
        });
        mapObj.setFitView();
    }
    function startAnimation() {
        marker.moveAlong(lineArr, 500);
    }
    function stopAnimation() {
        marker.stopMove();
    }
</script>
</head>
<body onLoad="mapInit()">
	<div id="iCenter"></div>
	<div style="padding:2px 0px 0px 5px;font-size:12px">
		<input type="button" value="开始动画" onclick="startAnimation()"/>
	    <input type="button" value="停止动画" onclick="stopAnimation()"/>
	</div>
</body>
</html>					