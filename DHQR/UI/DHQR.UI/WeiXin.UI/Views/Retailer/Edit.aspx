<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<RetailerModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    零售户编辑
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.Hidden("Id",Model.Id) %>
    <%=Html.Hidden("PSW",Model.PSW) %>
    <%=Html.Hidden("COM_ID",Model.COM_ID) %>
    <%=Html.Hidden("STATUS",Model.STATUS) %>
    <%=Html.Hidden("RecieveType",Model.RecieveType) %>
        <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
                    <li class="cur"><a href="../Retailer/Index">零售户管理</a></li>
                </ul>
            </div>
            <div id="stores" class="r_con_wrap">
                <form id="stores_form" class="r_con_form" method="post">
                    <div class="rows">
                        <label>客户内码</label>
                        <span class="input">
                            <%=Html.TextBox("CUST_ID", Model==null?"":Model.CUST_ID,new {Class="form_input" ,@Disabled="Disabled", maxlength="30" ,notnull=""})%>
                            <font class="fc_red">*</font></span>
                        <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label>客户名称</label>
                        <span class="input">
                            <%=Html.TextBox("CUST_NAME", Model==null?"":Model.CUST_NAME,new {Class="form_input" ,@Disabled="Disabled",maxlength="30" ,notnull=""})%>
                            <font class="fc_red">*</font></span>
                        <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label>专卖证件号</label>
                        <span class="input">
                            <%=Html.TextBox("LICENSE_CODE", Model==null?"":Model.LICENSE_CODE,new {Class="form_input",@Disabled="Disabled" ,maxlength="30" ,notnull=""})%>
                            <font class="fc_red">*</font></span>
                        <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label>客户状态</label>
                        <span class="input">
                            <%=Html.TextBox("StatusStr", Model==null?"":Model.StatusStr,new {Class="form_input" ,@Disabled="Disabled",maxlength="30" ,notnull=""})%>
                            <font class="fc_red">*</font></span>
                        <div class="clear"></div>
                    </div>
                    <div class="rows">
                         <label>接收方式</label>
                         <span class="input">
                        <%=Html.DropDownListFor(f=>f.RecieveTypeName,ViewData["RecieveTypeList"] as IList<SelectListItem>,new { @class = "ui-text-select" ,@onchange="setRecieveValue();" }) %>
                           <font class="fc_red">*</font></span>
                         <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label>详细地址</label>
                        <span class="input">
                            <%=Html.TextBox("Address", Model==null?"":Model.Address,new {Class="form_input" , size="45",maxlength="100" ,notnull="", autocomplete="off"})%>
                            <div id="result1" name="result1" style="overflow: auto; border: 1px solid gray;display: none;"></div>                                
                            <span class="primary" id="Primary"  onclick="placeSearch()">定位</span>
                            <font class="fc_red">*</font><br>
                            <div class="tips">如果输入地址后点击定位按钮无法定位，请在地图上直接点击选择地点</div>
                            <div id="map"></div>
                        </span>
                        <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label></label>
                        <span class="input">
                            <input type="submit" class="btn_ok" name="submit_button" value="提交保存" />
                            <a href="../Retailer/Index" class="btn_cancel">返回</a></span>
                        <div class="clear"></div>
                    </div>
                    <%=Html.Hidden("PrimaryLng",Model.LONGITUDE) %>
                    <%=Html.Hidden("PrimaryLat",Model.LATITUDE) %>
                    <%=Html.Hidden("ProvinceAndCity",Model.ProvinceAndCity) %>
                    <%=Html.Hidden("AreaCode",Model.AreaCode) %>
                </form>
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
    <script src="../../Scripts/jqueryUi/jquery-ui-1.9.2.custom.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Box/jquery.Box.min.js" type="text/javascript"></script>
    <script src="../../Scripts/JQueryValidate/jquery.validate.min.js" type="text/javascript"></script>
    <script src="../../Scripts/JqGrid/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script src="../../Scripts/JqGrid/grid.locale-cn.js" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
<script type="text/javascript">

    $(function () {

        initMap();
        reSetControls();
        //提交按钮
        //ajax提交更新，返回
        $('#stores_form').submit(function () { return false; });
        $('#stores_form input:submit').click(function () {
            if (system_obj.check_form($('*[notnull]'))) { return false };
            $(this).attr('disabled', true);
            var Id = $("#Id").val(), STATUS = $("#STATUS").val(), RecieveType = $("#RecieveType").val(), Address = $("#Address").val(),
                CUST_ID = $("#CUST_ID").val(), CUST_NAME = $("#CUST_NAME").val(), LICENSE_CODE = $("#LICENSE_CODE").val(),
                 PrimaryLng = currentLng, PrimaryLat = currentLat, ProvinceAndCity = $("#ProvinceAndCity").val(), COM_ID = $("#COM_ID").val(), PSW = $("#PSW").val()
               AreaCode = $("#AreaCode").val(), RecieveTypeName = $("#RecieveTypeName").find("option:selected").text();
            var param = {
                Id: Id, STATUS: STATUS, RecieveType: RecieveType, CUST_ID: CUST_ID, CUST_NAME: CUST_NAME,
                LICENSE_CODE: LICENSE_CODE, LONGITUDE: PrimaryLng, LATITUDE: PrimaryLat, ProvinceAndCity: ProvinceAndCity
                , AreaCode: AreaCode, RecieveTypeName: RecieveTypeName, Address: Address, PSW: PSW, COM_ID: COM_ID
            };
            $.ajax({
                url: "SaveEditData",
                data: param,
                type: "post",
                //context: document.body,
                success: function (data) {
                    if (data.IsSuccessful == 1) {
                        window.location = 'Index';
                    } else {
                        alert(data.OperateMsg);
                        $('#stores_form input:submit').attr('disabled', false);
                    }
                },
                error: function (data) {

                }
            });
        });

    });


    var mapObj, Marker, currentLng,currentLat;
    function initMap() {
        var myAddress = $('input[name=Address]').val();
        currentLng = ($('input[name=PrimaryLng]').val() == "" || $('input[name=PrimaryLng]').val() == undefined) ? "105.843353" : $('input[name=PrimaryLng]').val();
        currentLat = ($('input[name=PrimaryLat]').val() == "" || $('input[name=PrimaryLat]').val() == undefined) ? "32.435389" : $('input[name=PrimaryLat]').val();
        //自定义定位标记
        Marker = new AMap.Marker({
            position: new AMap.LngLat(currentLng, currentLat),//地图中心点
            draggable: false, 
            cursor: 'move',  //鼠标悬停点标记时的鼠标样式
            raiseOnDrag: true//鼠标拖拽点标记时开启点标记离开地图的效果

        });

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
        //mapObj.plugin(["AMap.ToolBar"], function () {
        //    toolBar = new AMap.ToolBar();
        //    mapObj.addControl(toolBar);
        //});
        //toolBar.hideDirection();
        //为地图注册click事件获取鼠标点击出的经纬度坐标
        var clickEventListener = AMap.event.addListener(mapObj, 'click', function (e) {
            //currentLng = e.lnglat.getLng();
            //currentLat = e.lnglat.getLat();
            setCurrentMarker(e);            
        });
        Marker.setMap(mapObj);
        document.getElementById("Address").onkeyup = keydown;

    }

    //自动提示
    //输入提示
    function autoSearch() {
        var keywords = document.getElementById("Address").value;
        var auto;
        var areaCode = $("#AreaCode").val();
        //加载输入提示插件
        AMap.service(["AMap.Autocomplete"], function () {
            var autoOptions = {
                city: areaCode //城市，默认全国
            };
            auto = new AMap.Autocomplete(autoOptions);
            //查询成功时返回查询结果
            if (keywords.length > 0) {
                auto.search(keywords, function (status, result) {
                    autocomplete_CallBack(result);
                });
            }
            else {
                document.getElementById("result1").style.display = "none";
            }
        });

    }
    //输出输入提示结果的回调函数
    function autocomplete_CallBack(data) {
        var resultStr = "";
        var tipArr = data.tips;
        if (tipArr && tipArr.length > 0) {
            for (var i = 0; i < tipArr.length; i++) {
                resultStr += "<div id='divid" + (i + 1) + "' onmouseover='openMarkerTipById(" + (i + 1)
                            + ",this)' onclick='selectResult(" + i + ")' onmouseout='onmouseout_MarkerStyle(" + (i + 1)
                            + ",this)' style=\"font-size: 13px;cursor:pointer;padding:5px 5px 5px 5px;\"" + "data=" + tipArr[i].adcode + ">" + tipArr[i].name + "<span style='color:#C1C1C1;'>" + tipArr[i].district + "</span></div>";
            }
        }
        else {
            resultStr = " π__π 亲,人家找不到结果!<br />要不试试：<br />1.请确保所有字词拼写正确<br />2.尝试不同的关键字<br />3.尝试更宽泛的关键字";
        }
        document.getElementById("result1").curSelect = -1;
        document.getElementById("result1").tipArr = tipArr;
        document.getElementById("result1").innerHTML = resultStr;
        document.getElementById("result1").style.display = "block";

    }



    //输入提示框鼠标滑过时的样式
    function openMarkerTipById(pointid, thiss) {//根据id打开搜索结果点tip 
        thiss.style.background = '#CAE1FF';
    }

    //输入提示框鼠标移出时的样式
    function onmouseout_MarkerStyle(pointid, thiss) {  //鼠标移开后点样式恢复 
        thiss.style.background = "";
    }

    //从输入提示框中选择关键字并查询
    function selectResult(index) {
        if (index < 0) {
            return;
        }
        if (navigator.userAgent.indexOf("MSIE") > 0) {
            document.getElementById("Address").onpropertychange = null;
            document.getElementById("Address").onfocus = focus_callback;
        }
        //截取输入提示的关键字部分
        var text = document.getElementById("divid" + (index + 1)).innerHTML.replace(/<[^>].*?>.*<\/[^>].*?>/g, "");
        var cityCode = document.getElementById("divid" + (index + 1)).getAttribute('data');
        document.getElementById("Address").value = text;
        document.getElementById("result1").style.display = "none";
        //根据选择的输入提示关键字查询
        mapObj.plugin(["AMap.PlaceSearch"], function () {
            var msearch = new AMap.PlaceSearch();  //构造地点查询类
            AMap.event.addListener(msearch, "complete", placeSearch_CallBack); //查询成功时的回调函数
            msearch.setCity(cityCode);
            msearch.search(text);  //关键字查询查询
        });


    }

    //定位选择输入提示关键字
    function focus_callback() {
        if (navigator.userAgent.indexOf("MSIE") > 0) {
            document.getElementById("Address").onpropertychange = autoSearch;
        }
    }

    //输出关键字查询结果的回调函数
    function placeSearch_CallBack(data) {
        //清空地图上的InfoWindow和Marker
        windowsArr = [];
        marker = [];
        mapObj.clearMap();
        var resultStr1 = "";
        var poiArr = data.poiList.pois;
        var resultCount = poiArr.length;
        for (var i = 0; i < resultCount; i++) {
            resultStr1 += "<div id='divid" + (i + 1) + "' onmouseover='openMarkerTipById1(" + i + ",this)' onmouseout='onmouseout_MarkerStyle(" + (i + 1) + ",this)' style=\"font-size: 12px;cursor:pointer;padding:0px 0 4px 2px; border-bottom:1px solid #C1FFC1;\"><table><tr><td><img src=\"http://webapi.amap.com/images/" + (i + 1) + ".png\"></td>" + "<td><h3><font color=\"#00a6ac\">名称: " + poiArr[i].name + "</font></h3>";
            resultStr1 += TipContents(poiArr[i].type, poiArr[i].address, poiArr[i].tel) + "</td></tr></table></div>";
            addmarker(i, poiArr[i]);
        }
        //更新当前选择的坐标
        Marker.setPosition(new AMap.LngLat(currentLng, currentLat)); //更新点标记位置
        Marker.setMap(mapObj);
        mapObj.setFitView();
    }

    //鼠标滑过查询结果改变背景样式，根据id打开信息窗体
    function openMarkerTipById1(pointid, thiss) {  //根据id 打开搜索结果点tip
        thiss.style.background = '#CAE1FF';
        windowsArr[pointid].open(mapObj, marker[pointid]);
    }

    //添加marker&infowindow   
    function addmarker(i, d) {
        var lngX = d.location.getLng();
        var latY = d.location.getLat();
        var markerOption = {
            map: mapObj,
            icon: "http://webapi.amap.com/images/" + (i + 1) + ".png",
            position: new AMap.LngLat(lngX, latY)
        };
        var mar = new AMap.Marker(markerOption);
        marker.push(new AMap.LngLat(lngX, latY));

        var infoWindow = new AMap.InfoWindow({
            content: "<h3><font color=\"#00a6ac\">  " + (i + 1) + ". " + d.name + "</font></h3>" + TipContents(d.type, d.address, d.tel),
            size: new AMap.Size(300, 0),
            autoMove: true,
            offset: new AMap.Pixel(0, -30)
        });
        windowsArr.push(infoWindow);
        //var aa = function (e) { infoWindow.open(mapObj, mar.getPosition()); };
        //AMap.event.addListener(mar, "click", aa);
        AMap.event.addListener(mar, "click", function (e) { setCurrentMarker(e); });

    }

    $('input[name=Address]').keyup(function (event) {
        if (event.which == 13) {
            $('#Primary').click();
        }
    });

    //infowindow显示内容
    function TipContents(type, address, tel) {  //窗体内容
        if (type == "" || type == "undefined" || type == null || type == " undefined" || typeof type == "undefined") {
            type = "暂无";
        }
        if (address == "" || address == "undefined" || address == null || address == " undefined" || typeof address == "undefined") {
            address = "暂无";
        }
        if (tel == "" || tel == "undefined" || tel == null || tel == " undefined" || typeof address == "tel") {
            tel = "暂无";
        }
        var str = "  地址：" + address + "<br />  电话：" + tel + " <br />  类型：" + type;
        return str;

    }

    function keydown(event) {
        var key = (event || window.event).keyCode;
        var result = document.getElementById("result1")
        var cur = result.curSelect;
        if (key === 40) {//down
            if (cur + 1 < result.childNodes.length) {
                if (result.childNodes[cur]) {
                    result.childNodes[cur].style.background = '';
                }
                result.curSelect = cur + 1;
                result.childNodes[cur + 1].style.background = '#CAE1FF';
                document.getElementById("Address").value = result.tipArr[cur + 1].name;
            }
        } else if (key === 38) {//up
            if (cur - 1 >= 0) {
                if (result.childNodes[cur]) {
                    result.childNodes[cur].style.background = '';
                }
                result.curSelect = cur - 1;
                result.childNodes[cur - 1].style.background = '#CAE1FF';
                document.getElementById("Address").value = result.tipArr[cur - 1].name;
            }
        } else if (key === 13) {
            var res = document.getElementById("result1");
            if (res && res['curSelect'] !== -1) {
                selectResult(document.getElementById("result1").curSelect);
            }
        } else {
            autoSearch();
        }

    }


    var marker = new Array();
    var windowsArr = new Array();
    function placeSearch() {
        var MSearch;
        var areaCode = $("#AreaCode").val();
        var placeName = $("#Address").val();
        AMap.service(["AMap.PlaceSearch"], function () {
            MSearch = new AMap.PlaceSearch({ //构造地点查询类
                pageSize: 10,
                pageIndex: 1,
                city: areaCode //城市
            });

            //关键字查询
            MSearch.search(placeName, function (status, result) {
                if (status === 'complete' && result.info === 'OK') {
                    keywordSearch_CallBack(result);
                }
            });
        });
    }


    //回调函数
    function keywordSearch_CallBack(data) {
        var resultStr = "";
        var poiArr = data.poiList.pois;
        var resultCount = poiArr.length;
        for (var i = 0; i < resultCount; i++) {
            resultStr += "<div id='divid" + (i + 1) + "' onmouseover='openMarkerTipById1(" + i + ",this)' onmouseout='onmouseout_MarkerStyle(" + (i + 1) + ",this)' style=\"font-size: 12px;cursor:pointer;padding:0px 0 4px 2px; border-bottom:1px solid #C1FFC1;\"><table><tr><td><img src=\"http://webapi.amap.com/images/" + (i + 1) + ".png\"></td>" + "<td><h3><font color=\"#00a6ac\">名称: " + poiArr[i].name + "</font></h3>";
            resultStr += TipContents(poiArr[i].type, poiArr[i].address, poiArr[i].tel) + "</td></tr></table></div>";
            addmarker(i, poiArr[i]);
        }

        mapObj.setFitView();
        //document.getElementById("result").innerHTML = resultStr;
    }

    function onmouseout_MarkerStyle(pointid, thiss) { //鼠标移开后点样式恢复
        thiss.style.background = "";
    }

    //移除之前的坐标点，添加现有坐标
    function setCurrentMarker(e)
    {
        currentLng = e.lnglat.getLng();
        currentLat = e.lnglat.getLat();
     //   setCurrentMarker();
        //当前选择的坐标
        Marker.setPosition(new AMap.LngLat(currentLng, currentLat)); //更新点标记位置
        Marker.setMap(mapObj);
        geocoder();
    }

    //逆地理编码
    function geocoder() {
        var lnglatXY = new AMap.LngLat(currentLng, currentLat);
        var MGeocoder;
        //加载地理编码插件
        AMap.service(["AMap.Geocoder"], function () {
            MGeocoder = new AMap.Geocoder({
                radius: 1000,
                extensions: "all"
            });
            //逆地理编码
            MGeocoder.getAddress(lnglatXY, function (status, result) {
                if (status === 'complete' && result.info === 'OK') {
                    geocoder_CallBack(result);
                }
            });
        });
    }

    //回调函数
    function geocoder_CallBack(data) {
        //返回地址描述
        $("#Address").attr("value", data.regeocode.formattedAddress);
        $("#ProvinceAndCity").attr("value", data.regeocode.addressComponent.province + data.regeocode.addressComponent.city);
        $("#AreaCode").attr("value", data.regeocode.addressComponent.citycode);

    }
    

    //重设控件属性
    var reSetControls = function () {
        var selwidth = $("#CUST_ID").width()*1.1;
        $("#stores_form .ui-text-select").css("width", selwidth);
        setSelectList();
    };

    function setSelectList()
    {
        var selectLst = $("#RecieveTypeName");
        var recieveTypeValue = $("#RecieveType").val();
        for (var i = 0; i < selectLst[0].length; i++) {
            if (selectLst[0][i].value == recieveTypeValue) {
                selectLst[0][i].selected = true;
            }
        }
    }

    function setRecieveValue()
    {
        var recieveTypeValue = $("#RecieveTypeName").val();
        $("#RecieveType").val(recieveTypeValue);
    }

    </script>

<style type="text/css">
    #result1 {
    position:absolute;
    width:30%;
    /*height:115px*/
    z-index:100;    
    /*left: 20%;*/
    margin-left:1px;
    background-color:white;
    /*top: 60px;*/
}

</style>
</asp:Content>
