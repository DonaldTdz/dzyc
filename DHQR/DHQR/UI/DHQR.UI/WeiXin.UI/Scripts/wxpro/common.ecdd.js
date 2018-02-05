$(function() {

	'use strict';

    var clickEvent = (document.ontouchstart!==null) ? 'click' : 'touchstart';
    var transitionEvent = whichTransitionEvent();
    var animationEvent = whichAnimationEvent();
    // var mySlider, setClassFunc;
    var sliderOpts = {
        wrap: '.wrapper',
        item: '.page',
        lastLocate: false,
        noslide: [0, 1, 2, 3, 4],
        speed: 0,
        // index: 4,
        onslide: function (index) {
            // clearTimeout(setClassFunc);
            // $('.point')[0].className = 'point animation';
            // setClassFunc = setTimeout(function() {
            //     $('.point')[0].className = 'point animation page' + (index + 1);
            //     $('.point').data('page', index);
            // }, index === 4 ? 0 : 2200);

            typeof MtaH5 !== 'undefined' && MtaH5.clickStat('slide' + index);

            // 初始化
            $('.point')[0].className = 'point animation';
            var currentPage = index + 1;
            if (index !== 4) {
                $('.open_statement').hide();
                // setTimeout(function() {
                $('.page' + currentPage + ' h2.page_title').one(animationEvent, function() {
                    $('.point')[0].className = 'point animation page' + currentPage;
                    $('.point').data('page', index);
                });
                // }, 2200);
            } else {
                $('.point')[0].className = 'point animation page' + currentPage;
                $('.point').data('page', index);
                $('.open_statement').show();
            }
        }
    };


    function whichTransitionEvent(){
        var t,
            el = document.createElement("fakeelement");

        var transitions = {
            "transition"      : "transitionend",
            "OTransition"     : "oTransitionEnd",
            "MozTransition"   : "transitionend",
            "WebkitTransition": "webkitTransitionEnd"
        }

        for (t in transitions){
            if (el.style[t] !== undefined){
                return transitions[t];
            }
        }
    }

    function whichAnimationEvent(){
        var t,
            el = document.createElement("fakeelement");

        var animations = {
            "animation"      : "animationend",
            "OAnimation"     : "oAnimationEnd",
            "MozAnimation"   : "animationend",
            "WebkitAnimation": "webkitAnimationEnd"
        }

        for (t in animations){
            if (el.style[t] !== undefined){
                return animations[t];
            }
        }
    }

    // 预加载动画
    new preloader({
        files: preload,
        progress: function(err, response, percent) {
            $('.percent').text(percent + '%');
        },
        complete: function() {
            // 隐藏加载器
            $('.preloader').addClass('loaded');
            $('.preloader').on(animationEvent, function() {
                $('.preloader').remove();
                setTimeout(function() {
                    $('.point .avatar').remove();
                }, 200);
            });

            window.mySlider = new iSlider(sliderOpts);
            bgAudio.play();
        }
    });

    // 首页点击绿点切换到第二页
    $(document).on(clickEvent, '.point', function() {
        var current     = $('.point').data('page');
        var currentPage = current + 1;
        var isPlayed    = $('.point').hasClass('page' + currentPage);
        if (typeof current !== 'undefined' && current < 4 && isPlayed) {
            // mySlider.slideTo(current + 1);
            mySlider.next();
        }
    });

    // 重新回顾按钮
    $(document).on(clickEvent, '.point.page5', function() {
        console.log(this);
        mySlider.slideTo(0);
        bgAudio.play();
        typeof MtaH5 !== 'undefined' && MtaH5.clickStat('replay');
    });

    $(document).on(clickEvent, '.open_statement', function() {
        $('.statement').show();
    });

    // 关闭声明
    $(document).on(clickEvent, '.close_statement', function() {
        $('.statement').hide();
    });

    // 背景音乐控制
    var bgMusic = $('#bgMusic');
    var bgMusicContr = $('#bgMusicContr');
    var bgAudio = document.getElementById("bgMusic");
    $(document).on(clickEvent, '#bgMusicContr', function(e) {
        var $this = $(this);
        if (!$this.hasClass('playing')) {
            document.getElementById("bgMusic").play();
        } else {
            document.getElementById("bgMusic").pause();
        }
    });

    // 播放事件
    bgMusic.on('play', function() {
        bgMusicContr.addClass('playing');
    });

    // 暂停事件
    bgMusic.on('pause', function() {
        bgMusicContr.removeClass('playing');
    });

    // 对不支持自动播放的设备使用一次性的 touchstart 事件触发
    $(document).one('touchstart', function() {
        bgAudio.play();
    });

    // 微信分享
    var shareTitle = '当你打开这个，你与微信的故事就这样发生了';
    var shareDesc  = '2016 微信公开课 PRO 版';
    var shareLink  = 'https://support.weixin.qq.com/cgi-bin/mmsupport-bin/getmyrelationwithwx?action=getme';
    var shareImg   = 'https://res.wx.qq.com/zh_CN/htmledition/getmyrelationwithwx/images/share.jpg';

    document.addEventListener('WeixinJSBridgeReady', function() {
        WeixinJSBridge.on('menu:share:appmessage', function() {
            WeixinJSBridge.invoke('sendAppMessage', {
                "appid": "",
                "img_url": shareImg,
                "img_width": "120",
                "img_height": "120",
                "link": shareLink,
                "desc": shareDesc,
                "title": shareTitle
            }, function(res) {
                // 分享到聊天后回调函数
                typeof MtaH5 !== 'undefined' && MtaH5.clickStat('sendAppMessage');
            });
            return false;
        });
        WeixinJSBridge.on('menu:share:timeline', function() {
            WeixinJSBridge.invoke('shareTimeline', {
                "appid": "",
                "img_url": shareImg,
                "img_width": "120",
                "img_height": "120",
                "link": shareLink,
                "desc": shareDesc,
                "title": shareTitle
            }, function(res) {
                // 分享到朋友圈后回调函数
                typeof MtaH5 !== 'undefined' && MtaH5.clickStat('shareTimeline');
            });
            return false;
        });
    }, false);

});
