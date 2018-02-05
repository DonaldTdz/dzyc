<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<WeiXinUserModel>" %>

<!DOCTYPE html>
<html>

<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,minimum-scale=1,maximum-scale=1,user-scalable=no" />
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <meta content="yes" name="apple-mobile-web-app-capable">
    <meta content="black" name="apple-mobile-web-app-status-bar-style">
    <meta content="telephone=no" name="format-detection">
    <title>广元烟草</title>
    <link href="../../DHQRCss/bind.css" rel="stylesheet" />
    <script src="../../Scripts/Jquery/jquery-1.11.1.min.js"></script>
</head>
<body class="index">
    <div class="wrap" id="bind">
        <div class="tabs">
            <a href="#" hidefocus="true" class="a-active"  style="margin-left: 30px;">零售户</a>
            <a href="#" hidefocus="true" >内部员工</a>
        </div>
        <div class="swiper">
            <div class="slide">
                <input type="text" id="code1" placeholder="专卖证号" />
                <input type="password" id="ps1"  placeholder="收货密码" />
                <input type="text" id="name1" placeholder="姓名" />
                <input type="tel" id="tel1" placeholder="手机号" />
                <br />
                <input type="button" value="一键绑定" onclick="BindUser();" style="background: #00C76A; color: #FFFFFF;cursor:pointer;" />
            </div>
            <div class="slide one">
                <input type="text" id="code2" placeholder="人力资源账号" />
                <input type="password" id="ps2" placeholder="人力资源密码" />
                <input type="text" id="name2" placeholder="姓名" />
                <input type="tel" id="tel2" placeholder="手机号" />
                <br />
                <input type="button" value="一键绑定" onclick="BindInnerUser();" style="background: #00C76A; color: #FFFFFF; cursor: pointer;" />
            </div>
        </div>
    </div>
    <div id="info">
        <div class="nav">
            <p>账号管理</p>
        </div>
        <div style="height: 50px;"></div>
        <div class="index_page">
            <ul>
                <li><font>用户类型</font><span><%=Model.UserTypeDsc%></span></li>
                <li><font>账号</font><span><%=Model.SysName%></span></li>
                <li><font>姓名</font><span><%=Model.Name%></span></li>
                <li><font>手机号</font><span><%=Model.Tel%></span></li>
            </ul>
        </div>
        <div class="index_te">
            <input type="button" value="解除绑定" onclick="RemoveBind();" style="cursor:pointer;" />
        </div>
    </div>
    <input type="hidden" id="HasBind" value='<%=ViewData["HasBind"]%>' />
    <input id="WxUserName" name="WxUserName" type="hidden" value="<%=Model.WxUserName%>" />
    <input id="key" name="key" type="hidden" value="<%=Model.key%>" />

</body>
<script type="text/javascript">
    $(function () {
        //是否绑定
        var hasBind = $("#HasBind").val().toUpperCase();
        if (hasBind == "TRUE") {
            $("#bind").hide();
            $("#info").show();
        }
        else {
            $("#bind").show();
            $("#info").hide();
        }

    });

    $(".tabs a").click(function () {
        var _index = $(this).index();	//获取.tabs a的下标
        $(this).addClass("a-active").siblings().removeClass("a-active");//this就是（.tabs a），给当前的a添加一个样式添加过后并移除这个样式
        $(".slide").eq(_index).show().siblings().hide();//根据下标a，让.slide显示并隐藏
    });


    //绑定零售户
    function BindUser() {
        var WxUserName = $("#WxUserName").val();
        var key = $("#key").val();
        var OrderCode = $("#code1").val(); var OrderPsw = $("#ps1").val();
        var RetailerName = $("#name1").val(); var RetailerTel = $("#tel1").val();
        var UserType = 0;
        if (OrderCode == "" || OrderPsw == "" || RetailerName == "" || RetailerTel == "") {
            alert("请填写完整相关信息！");
        }
        else {
            var postData = { UserType: UserType, SysName: OrderCode, SysPsw: OrderPsw, Name: RetailerName, Tel: RetailerTel, WxUserName: WxUserName, key: key };

            $.ajax({
                url: "BindUser",
                data: postData,
                //contentType: "application/json;charset=utf-8",
                type: "POST",
                catche: false,
                success: function (dohandle) {
                    alert(dohandle.OperateMsg);
                    colsePage();
                    //$("#msg").text(dohandle.OperateMsg);
                    //$("#showA").click();

                },
                error: function (e) {
                    alert(dohandle.OperateMsg);
                    //$("#msg").text(e);
                    //$("#showA").click();
                }

            });
        }
    }

    //绑定内部员工
    function BindInnerUser()
    {
        var WxUserName = $("#WxUserName").val();
        var key = $("#key").val();
        var OrderCode = $("#code2").val(); var OrderPsw = $("#ps2").val();
        var RetailerName = $("#name2").val(); var RetailerTel = $("#tel2").val();
        var UserType = 1;
        if (OrderCode == "" || OrderPsw == "" || RetailerName == "" || RetailerTel == "") {
            alert("请填写完整相关信息！");
        }
        else {
            var postData = { UserType: UserType, SysName: OrderCode, SysPsw: OrderPsw, Name: RetailerName, Tel: RetailerTel, WxUserName: WxUserName, key: key };

            $.ajax({
                url: "BindUser",
                data: postData,
                type: "POST",
                catche: false,
                success: function (dohandle) {
                    alert(dohandle.OperateMsg);
                },
                error: function (e) {
                    alert(dohandle.OperateMsg);
                }

            });
        }
    }

    //解除绑定
    function RemoveBind()
    {
        var WxUserName = $("#WxUserName").val();
        var key = $("#key").val();

        if (confirm("解除绑定后将不能查询订单,是否确认解除？")) {
            var postData = { WxUserName: WxUserName, key: key };

            $.ajax({
                url: "RemoveBind",
                data: postData,
                type: "POST",
                catche: false,
                success: function (dohandle) {
                    alert(dohandle.OperateMsg);
                    colsePage();
                },
                error: function (e) {
                    alert(dohandle.OperateMsg);
                }

            });

        }
        else {
            alert("不解除绑定");
        }
    }

    //关闭页面
    function colsePage() {
        WeixinJSBridge.invoke('closeWindow', {}, function (res) {

        });
    }

</script>
</html>
