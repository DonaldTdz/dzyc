<!DOCTYPE HTML>
<html>
<head>
<meta name="viewport" content="initial-scale=1.0,user-scalable=no">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>英文、中英文地图</title>
<link rel="stylesheet" type="text/css" href="http://developer.amap.com/Public/css/demo.Default.css" />
<script language="javascript" src="http://webapi.amap.com/maps?v=1.3&key=3f4732eca0ff79d46e4379bac44775de"></script>
<script language="javascript">
    var mapObj, Marker1, Marker2, Marker3;
    var lat = new AMap.LngLat(104.177556, 30.55214);
    //自定义定位标记
    var customMarker = new AMap.Marker({
        offset: new AMap.Pixel(-14, -34),//相对于基点的位置
        icon: new AMap.Icon({  //复杂图标
            size: new AMap.Size(27, 36),//图标大小
            image: "http://webapi.amap.com/images/custom_a_j.png", //大图地址
            imageOffset: new AMap.Pixel(-56, 0)//相对于大图的取图位置
        })
    });
    var customMarker1 = new AMap.Marker({
        offset: new AMap.Pixel(-14, -34),//相对于基点的位置
        position: lat,
        icon: new AMap.Icon({  //复杂图标
            size: new AMap.Size(27, 36),//图标大小
            image: "http://webapi.amap.com/images/custom_a_j.png", //大图地址
            imageOffset: new AMap.Pixel(0, 0)//相对于大图的取图位置
            //,cursor: "pointer"
        })
    });
    //初始化地图对象，加载地图
    function mapInit() {
        mapObj = new AMap.Map("iCenter", {
            rotateEnable: true,
            dragEnable: true,
            zoomEnable: true,
            //二维地图显示视口
            view: new AMap.View2D({
                center: new AMap.LngLat(121.498586, 31.239637),//地图中心点
                zoom: 13 //地图显示的缩放级别
            })
        });
        //地图中添加地图操作ToolBar插件
        mapObj.plugin(["AMap.ToolBar"], function () {
            toolBar = new AMap.ToolBar({ locationMarker: customMarker }); //设置地位标记为自定义标记
            mapObj.addControl(toolBar);
            AMap.event.addListener(toolBar, 'location', function callback(e) {
                locationInfo = e.lnglat;
            });
        });
        toolBar.doLocation();
        customMarker1.setMap(mapObj);
       // customMarker1.setAnimation('AMAP_ANIMATION_BOUNCE'); //设置点标记的动画效果，此处为弹跳效果   
        customMarker1.setTitle('我是地图中心点哦~'); //设置鼠标划过点标记显示的文字提示

    }

    /*
 *获取用户所在城市信息
 */
    function showCityInfo() {
        //加载城市查询插件
        AMap.service(["AMap.CitySearch"], function () {
            //实例化城市查询类
            var citysearch = new AMap.CitySearch();
            //自动获取用户IP，返回当前城市
            citysearch.getLocalCity(function (status, result) {
                if (status === 'complete' && result.info === 'OK') {
                    if (result && result.city && result.bounds) {
                        var cityinfo = result.city;
                        var citybounds = result.bounds;
                        document.getElementById('info').innerHTML = "您当前所在城市：" + cityinfo + "";
                        //地图显示当前城市
                        mapObj.setBounds(citybounds);
                    }
                } else {
                    document.getElementById('info').innerHTML = "您当前所在城市：" + result.info + "";
                }
            });
        });
    }
    function en_map() {
        mapObj.setLang("en");
    }
    function zh_en_map() {
        mapObj.setLang("zh_en");
    }
    function zh_map() {
        mapObj.setLang("zh_cn");
    }
</script>
</head>
<body onLoad="mapInit()">
    <div id="iCenter"></div>
     <div style="padding:2px 0px 0px 5px;font-size:12px"> 
        <input type="button" value="显示英文底图" onClick="javascript: en_map()"/> 
        <input type="button" value="显示中英文对照底图" onClick="javascript: zh_en_map()"/>
        <input type="button" value="显示中文底图" onClick="javascript: zh_map()"/>
        <input type="button" value="显示当前城市" onClick="javascript: showCityInfo()"/> 
        <div id="info" style="margin-top:10px;margin-left:10px;height:30px"></div>
    </div>   
</body>
</html>                  		