<%@ Page Title="" Language="C#"  Inherits="System.Web.Mvc.ViewPage<DHQR.UI.Models.WeiXinRetailerProModel>" %>

<!DOCTYPE html>
<html lang="zh-CN" style="font-size: 100px;">
	<head>
		<meta charset="UTF-8">
		<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no">
		<meta name="format-detection" content="telephone=no">
		<meta name="format-detection" content="email=no">
		<meta name="apple-mobile-web-app-capable" content="yes">
		<meta name="apple-mobile-web-app-status-bar-style" content="black">
		<meta name="author" content="Tencent-CDC">
		<meta name="copyright" content="Tencent">
		<title>2015年到货确认年终会</title>
        <link href="../../Css/wxpro/common.8d0e.css" rel="stylesheet" type="text/css" />
		<script>
		    var evt = "onorientationchange" in window ? "orientationchange" : "resize";

		    function resize() {
		        var html = document.documentElement,
					fontSize = 100, //100为根元素字体大小
					pageW = 375, //设计稿页面宽度
					pageH = 604, //设计稿页面高度
					//根据具体情况修改宽高值，可以用页面主体内容的宽高值
					wk = 375; //页面大小缩放的上限阀值，当页面大小超过此值时将不再放大页面
		        hk = 604; //页面大小缩放的上限阀值，当页面大小超过此值时将不再放大页面
		        if (html.clientWidth / html.clientHeight > pageW / pageH) {
		            if (html.clientHeight <= hk) {
		                html.style.fontSize = html.clientHeight / pageH * fontSize + "px";
		            } else {
		                html.style.fontSize = hk / pageH * fontSize + "px";
		            }
		        } else {
		            if (html.clientWidth <= wk) {
		                html.style.fontSize = html.clientWidth / pageW * fontSize + "px";
		            } else {
		                html.style.fontSize = wk / pageW * fontSize + "px";
		            }
		        }
		    }
		    resize();
		</script>
		<style>
			html,body{width:100%;height:100%;overflow:hidden}
		</style>
	</head>

	<body>

		<div id="wrapper" class="wrapper" style="display: block; position: relative; width: 100%; height: 100%;">
			<div class="page page1 play" style="display: block; position: absolute; left: 0px; top: 0px; width: 100%; height: 100%; -webkit-transform: translate3d(0px, 0px, 0px);">
				<div class="circle">
					<div class="line line1">
						<div class="lr"><i></i></div>
					</div>
					<div class="line line2"><i></i></div>
					<div class="line line3">
						<div class="lr"><i></i></div>
					</div>
					<div class="line line4"><i></i></div>
					<div class="line line5"><i></i></div>
					<div class="line line6"><i></i></div>
					<div class="line line7">
						<div class="lr"><i></i></div>
					</div>
					<div class="line line8"><i></i></div>
					<div class="line line9"><i></i></div>
					<div class="line line10"><i></i></div>
					<div class="line line11">
						<div class="lr"><i></i></div>
					</div>
					<div class="line line12">
						<div class="lr"><i></i></div>
					</div>
					<div class="line line13"><i></i></div>
					<div class="line line14"><i></i></div>
					<div class="line line15">
						<div class="lr"><i></i></div>
					</div>
				</div>
				<h2 class="page_title">2015年过去了，我很怀念它。</h2>
				<div class="action_tips">微信将获取你的信息，轻按按钮进入下一步</div>
			</div>
			<div class="page page2" style="display: block; position: absolute; left: 0px; top: 0px; width: 100%; height: 100%; -webkit-transform: translate3d(0px, 826px, 0px);">
				<h2 class="page_title">那一天，我与广元烟草相识</h2>
				<div class="ray ray1"><i></i></div>
				<div class="ray ray2"><i></i></div>
				<div class="ray ray3"><i></i></div>
				<div class="ol ol1">
					<div class="lr"><i></i></div>
				</div>
				<div class="ol ol2">
					<div class="lr"><i></i></div>
				</div>
				<div class="dot dot1">
					<div class="inner">1</div>
					<div class="line line1"><i></i></div>
					<div class="line line2">
						<div class="lr"><i></i></div>
					</div>
					<div class="line line3"><i></i></div>
					<div class="line line4"><i></i></div>
				</div>
				<div class="dot dot2">
					<div class="inner">1</div>
					<div class="line line1"><i></i></div>
					<div class="line line2">
						<div class="lr"><i></i></div>
					</div>
					<div class="line line3"><i></i></div>
					<div class="line line4"><i></i></div>
					<div class="line line5">
						<div class="lr"><i></i></div>
					</div>
					<div class="line line6"><i></i></div>
				</div>
				<div class="dot dot3">
					<div class="inner">1</div>
					<div class="line line1"><i></i></div>
					<div class="line line2">
						<div class="lr"><i></i></div>
					</div>
					<div class="line line3"><i></i></div>
					<div class="line line4">
						<div class="lr"><i></i></div>
					</div>
				</div>
				<div class="section section1">
					<p class="data">2012.06.08</p>
					<p>我关注了广元烟草</p>
					<p>我是第</p>
					<p class="data">134624705</p>
					<p>个广元烟草粉丝</p>
				</div>
				<div class="section section2 ">
					<p class="data">2011.01.25</p>
					<p>我绑定了账号</p>
					<p>成为广元烟草微信零售户</p>
				</div>
				<div class="section section3">
					<div class="avatar"><img src="http://wx.qlogo.cn/mmhead/3SLrnmeFlhWjhxs5sXlIbop8GwiaGrKWFPefcAduib1vc/132" alt="avatar"> </div>
					<p class="data">温瞳 ❤</p>
					<p>你的送货员</p>
					<p>PS: 你们现在还在联系吗</p>
				</div>
			</div>
		</div>

		<div id="landscape_hinter">
			<div class="hinter_phone"></div>
			<div class="hinter_rotate"></div>
			<div class="hinter_text">竖屏体验效果更佳</div>
		</div>

		<div class="point animation page1">
			<div class="dot">1</div>
			<div class="after_wrap">
				<div class="after">1</div>
			</div>
			<div class="touch_tip"></div>
			<div class="touch_effect">1</div>
		</div>
		<a href="javascript:;" id="bgMusicContr" class="music"></a>
		<audio id="bgMusic" src="../../Content/wxpro/music.1dfb.mp3" loop="" preload="auto"></audio>

		
		<script id="tmpl" type="text/tmpl">
			<!-- page1 START -->
			<div class="page page1">
				<div class="circle">
					<div class="line line1">
						<div class="lr"><i></i></div>
					</div>
					<div class="line line2"><i></i></div>
					<div class="line line3">
						<div class="lr"><i></i></div>
					</div>
					<div class="line line4"><i></i></div>
					<div class="line line5"><i></i></div>
					<div class="line line6"><i></i></div>
					<div class="line line7">
						<div class="lr"><i></i></div>
					</div>
					<div class="line line8"><i></i></div>
					<div class="line line9"><i></i></div>
					<div class="line line10"><i></i></div>
					<div class="line line11">
						<div class="lr"><i></i></div>
					</div>
					<div class="line line12">
						<div class="lr"><i></i></div>
					</div>
					<div class="line line13"><i></i></div>
					<div class="line line14"><i></i></div>
					<div class="line line15">
						<div class="lr"><i></i></div>
					</div>
				</div>
				<h2 class="page_title">2015年过去了，我很怀念它。</h2>
				<div class="action_tips">广元烟草微信将获取你的信息，轻按按钮进入下一步</div>
			</div>
			<!-- page1 END -->
			<!-- page2 START -->
			<div class="page page2">
				<h2 class="page_title">那一天，我与广元烟草相识</h2>

				<div class="ray ray1"><i></i></div>
				<div class="ray ray2"><i></i></div>
				<div class="ray ray3"><i></i></div>

				<div class="ol ol1">
					<div class="lr"><i></i></div>
				</div>
				<div class="ol ol2">
					<div class="lr"><i></i></div>
				</div>

				<div class="dot dot1">
					<div class="inner">1</div>
					<div class="line line1"><i></i></div>
					<div class="line line2">
						<div class="lr"><i></i></div>
					</div>
					<div class="line line3"><i></i></div>
					<div class="line line4"><i></i></div>
				</div>
				<div class="dot dot2">
					<div class="inner">1</div>
					<div class="line line1"><i></i></div>
					<div class="line line2">
						<div class="lr"><i></i></div>
					</div>
					<div class="line line3"><i></i></div>
					<div class="line line4"><i></i></div>
					<div class="line line5">
						<div class="lr"><i></i></div>
					</div>
					<div class="line line6"><i></i></div>
				</div>
				<div class="dot dot3">
					<div class="inner">1</div>
					<div class="line line1"><i></i></div>
					<div class="line line2">
						<div class="lr"><i></i></div>
					</div>
					<div class="line line3"><i></i></div>
					<div class="line line4">
						<div class="lr"><i></i></div>
					</div>
				</div>

				<div class="section section1">
					<p class="data">{{!it.FstUseWXTime}}</p>
					<p>我关注了广元烟草</p>
					<p>我是第</p>
					<p class="data">{{!it.WXRankNum}}</p>
					<p>个广元烟草粉丝</p>
				</div>

				<div class="section section2 {{?!it.FstSnsTime}}section_empty{{?}}">
					{{? it.FstSnsTime}}
					<p class="data">{{!it.FstSnsTime}}</p>
					<p>我绑定了账号</p>
					<p>成为了广元烟草微信零售户</p>
					{{??}}
					<p>亲爱的零售客户</p>
					<p>绑定是为您提供更便捷的服务</p>
					<p>绑定账号吧</p>
					<p>让生意做得更方便</p>
					{{?}}
				</div>

				<div class="section section3">
					{{? it.FstFriendNickName}}
					<div class="avatar"><img src="{{=it.FstFriendImg}}" alt="avatar">
					</div>
					<p class="data">{{!it.FstFriendNickName}}</p>
					<p>是我的第一个送货员</p>
					<p>PS: 你们现在还有联系吗</p>
					{{??}}
					<p>绑定账号</p>
					<p>寻找你的</p>
					<p>第一个送货员吧</p>
					{{?}}
				</div>
			</div>
			<!-- page2 END -->
			<!-- page3 START -->
			<div class="page page3">
				<h2 class="page_title">这一年，广元烟草微信上的我</h2>

				<div class="triangle triangle1"><i>1</i></div>
				<div class="triangle triangle2"><i>1</i></div>
				<div class="triangle triangle3"><i>1</i></div>
				<div class="triangle triangle4"><i>1</i></div>

				<div class="ray ray1"><i></i></div>
				<div class="ray ray2"><i></i></div>
				<div class="ray ray3"><i></i></div>

				<div class="dot dot1">
					<div class="inner">1</div>
					<div class="line line1"><i></i></div>
					<div class="line line2">
						<div class="lr"><i></i></div>
					</div>
				</div>
				<div class="dot dot2">
					<div class="inner">1</div>
					<div class="line line1"><i></i></div>
					<div class="line line2">
						<div class="lr"><i></i></div>
					</div>
				</div>
				<div class="dot dot3">
					<div class="inner">1</div>
					<div class="line line1"><i></i></div>
					<div class="line line2">
						<div class="lr"><i></i></div>
					</div>
					<div class="line line3"><i></i></div>
				</div>

				<div class="section section1 {{?it.SnsNum == 0}}section_empty{{?}}">
					<h3>订单</h3> {{? it.SnsNum == 0}}
					<p>亲爱的零售客户</p>
					<p>绑定是为您提供更便捷的服务</p>
					<p>绑定账号吧</p>
					<p>让生意做得更方便</p>
					{{??}}
					<p>这一年</p>
					<p>我一共下了</p>
					<p><span class="data">{{!it.SnsNum}}</span>个订单</p>
					{{?}}
				</div>

				<div class="section section2">
					<h3>刷卡</h3> {{? it.RecRedEnvelope == 0 && it.SendRedEnvelope == 0 }}
					<p>亲爱的</p>
					<p>有了到货确认系统</p>
					<p>刷卡确认更便捷</p>
					{{??}}
					<p>这一年</p>
					<p>我共刷了</p>
					<p><span class="data">{{!it.RecRedEnvelope}}</span>次领货卡</p>
					{{?}}
				</div>

				<div class="section section3">
					<h3>评价</h3> {{? it.isLocateEmpty }}
					<p>送货员也辛苦</p>
					<p>给他们一个好评吧</p>
					{{??}}
					<p>这一年评价了</p>
					<p><span class="data">{{!it.EvaluateNum}}</span>次送货员</p>
					<p>PS: 你还记得他们吗</p>
					{{?}}
				</div>
			</div>
			<!-- page3 END -->
			<!-- page4 START -->
			<div class="page page4">
				<h2 class="page_title">这一年，广元烟草微信上的我们</h2>

				<div class="rect rect1"><i></i></div>
				<div class="rect rect2"><i></i></div>
				<div class="rect rect3"><i></i></div>

				<div class="circle circle1"><i></i></div>
				<div class="circle circle2"><i></i></div>

				<div class="ray ray1"><i></i></div>
				<div class="ray ray2"><i></i></div>
				<div class="ray ray3"><i></i></div>
				<div class="ray ray4"><i></i></div>
				<div class="ray ray5"><i></i></div>
				<div class="ray ray6"><i></i></div>
				<div class="ray ray7"><i></i></div>
				<div class="ray ray8"><i></i></div>
				<div class="ray ray9"><i></i></div>
				<div class="ray ray10"><i></i></div>

				<div class="dot dot1">
					<div class="inner">1</div>
					<div class="line line1">
						<div class="lr"><i></i></div>
					</div>
					<div class="line line2"><i></i></div>
				</div>
				<div class="dot dot2">
					<div class="inner">1</div>
					<div class="line line1"><i></i></div>
					<div class="line line2">
						<div class="lr"><i></i></div>
					</div>
				</div>
				<div class="dot dot3">
					<div class="inner">1</div>
					<div class="line line1"><i></i></div>
					<div class="line line2">
						<div class="lr"><i></i></div>
					</div>
					<div class="line line3"><i></i></div>
				</div>

				<div class="section section1">
					{{? it.AllFriendNum == 0}}
					<p>快绑定账号</p>
					<p>看看您的送货员吧</p>
					{{??}}
					<p>我新认识了</p>
					<p><span class="data">{{!it.AddFriendNum}}</span>个送货员</p>
					{{?}}
				</div>

				<div class="section section2 {{?it.RecLike == 0}}section_empty{{?}}">
					{{? it.RecLike == 0}}
					<p>亲爱的</p>
					<p>赶快给您的送货员</p>
					<p>“赞”起来</p>
					{{??}}
					<p>我给他们</p>
					<p><span class="data">{{!it.RecLike}}</span>个非常满意</p>
					<p>感谢他们!</p>
					{{?}}
				</div>

				<div class="section section3 {{?it.Steps == 0 || it.NeedAttention == 1}}section_empty{{?}}">
					{{? it.NeedAttention == 0}} {{? it.Steps == 0}}
					<p>广元烟草，感谢有你</p>
					
					{{??}}
					<p>我在微信运动上</p>
					<p>走出了</p>
					<p class="steps"><span class="data">{{!it.Steps}}</span>步</p>
					{{? it.Marathon != 0 }}
					<p>相当于跑了</p>
					<p><span class="data">{{!it.Marathon}}</span>个马拉松</p>
					{{?}} {{?}} {{?}} {{? it.NeedAttention == 1}}
					<p>暂时没有</p>
					<p>任何步数数据</p>
					<p>快来关注微信运动吧</p>
					{{?}} {{? it.NeedAttention == -1}}
					<p>获取微信运动数据出错。</p>
					{{?}}
				</div>
			</div>
			<!-- page4 END -->
			<!-- page5 START -->
			<div class="page page5">
				<div class="end"></div>
				<h2 class="page_title">重新回顾<br>你和广元烟草的故事</h2>
			</div>
			<!-- page5 END -->
		</script>
		<script src="../../Scripts/wxpro/1.11.3.js"></script>
		<script src="../../Scripts/wxpro/doT.min.js"></script>
		<script src="../../Scripts/wxpro/moment.min.js"></script>
		<script src="../../Scripts/wxpro/preloader.js"></script>
		<script src="../../Scripts/wxpro/iSlider.js"></script>
		<script>
		    // 预加载资源
		    var preload = [
				'../../Content/wxpro/music.1dfb.mp3',
				'../../images/wxpro/bg@2x.4612.jpg',
				'../../images/wxpro/logo@2x.595c.png',
				'../../images/wxpro/end@2x_2a0e.png',
				'../../images/wxpro/sound@2x_257e.png',
				'../../images/wxpro/sound_muted@2x_a2c6.png',
				'../../images/wxpro/hand@2x_34a4.png',
				'../../images/wxpro/rect_dashed@2x.8e8d.png',
				'../../images/wxpro/triangle@2x_77c4.png',
				'../../images/wxpro/triangle_solid@2x.66a3.png',
				'../../images/wxpro/close@2x.4a0d.png',
				'../../images/wxpro/source@2x.bdad.png'
		    ];



		    // 线上数据
		    var wechatPro = {
		        "FstUseWXTime": "<%=Model.FstUseWXTime%>",
		        "WXRankNum": "<%=Model.WXRankNum%>",
		        "FstFriendNickName":  "<%=Model.FstFriendNickName%>",
		        "FstFriendImg": "<%=Model.FstFriendImg%>",
		        "SnsNum": "<%=Model.SnsNum%>",
		        "SnsRankNum": "100",
		        "LocateNum": 1,
		        "AddFriendNum": "<%=Model.AddFriendNum%>",
		        "AllFriendNum": 100,
		        "BoyFriendNum": 95,
		        "GirlFriendNum": 37,
		        "SendLike": 95,
		        "EvaluateNum": "<%=Model.EvaluateNum%>",
		        "RecLike": "<%=Model.RecLike%>",
		        "RecRedEnvelope": "<%=Model.RecRedEnvelope%>",
		        "SendRedEnvelope": 164,
		        "LocateCountryList": ["中南海市"],
		        "FriendCountryList": ["上海市", "北京市", "北美地区", "四川省", "山东省", "广东省", "广西", "江苏省", "海南省", "湖北省", "湖南省", "韩国"],
		        "FstSnsTime": "<%=Model.FstSnsTime%>",
		        "FstSnsType": 1,
		        "NeedAttention": 1
		    };
		    wechatPro.WXRankNum = wechatPro.WXRankNum >= 1000000000 ? '999999999+' : wechatPro.WXRankNum;
		    //wechatPro.FstSnsTime = wechatPro.FstSnsTime ? moment.unix(wechatPro.FstSnsTime).format('YYYY.MM.DD') : false;
		    wechatPro.FstSnsTime = moment(wechatPro.FstSnsTime, 'YYYYMMDD').format('YYYY.MM.DD');
		    wechatPro.FstUseWXTime = moment(wechatPro.FstUseWXTime, 'YYYYMMDD').format('YYYY.MM.DD');
		    wechatPro.Marathon = Math.floor(wechatPro.Steps / 52500);
		    wechatPro.isLocateEmpty = typeof wechatPro.LocateCountryList == 'undefined' || wechatPro.LocateCountryList.length == 0; // 判断是否有地理数据
		    wechatPro.locateCity = !wechatPro.isLocateEmpty ? wechatPro.LocateCountryList[0] : false; // 显示国内第一个城市
		    // 国内城市删除最后一个市字
		    if (wechatPro.locateCity && wechatPro.locateCity.substr(-1) == '市') {
		        wechatPro.locateCity = wechatPro.locateCity.substr(0, wechatPro.locateCity.length - 1);
		    }
		    var tempFn = doT.template(jQuery('#tmpl').html());
		    jQuery('#wrapper').html(tempFn(wechatPro));
        </script>
		
        <script src="../../Scripts/wxpro/common.ecdd.js" type="text/javascript"></script>
		<div style="position: absolute; left: -1000px; top: 0px; width: 1px; height: 1px; overflow: hidden;"><img src="../../images/wxpro/sound_muted@2x_a2c6.png"><img src="../../images/wxpro/end@2x_2a0e.png"><img src="../../images/wxpro/sound@2x_257e.png">
			<img
			src="../../images/wxpro/logo@2x.595c.png"><img src="../../images/wxpro/bg@2x.4612.jpg"><img src="../../images/wxpro/hand@2x_34a4.png"><img src="../../images/wxpro/triangle@2x.77c4.png">
				<img
				src="../../images/wxpro/rect_dashed@2x.8e8d.png"><img src="../../images/wxpro/triangle_solid@2x.66a3.png"><img src="../../images/wxpro/source@2x.bdad.png"><img src="../../images/wxpro/close@2x.4a0d.png"></div>
	</body>

</html>