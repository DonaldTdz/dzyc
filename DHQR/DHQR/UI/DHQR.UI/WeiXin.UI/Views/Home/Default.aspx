<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <meta charset="utf-8" content="到货确认系统">
    <title>到货确认系统</title>
    <link href="../../DHQRCss/global.css" rel="stylesheet" type="text/css" />
    <link href="../../DHQRCss/main.css" rel="stylesheet" type="text/css" />
    <script src="../../DHQRJs/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="../../DHQRJs/global.js" type="text/javascript"></script>
    <script src="../../DHQRJs/jquery.mousewheel.js" type="text/javascript"></script>
    <script src="../../DHQRJs/jquery.jscrollpane.js" type="text/javascript"></script>
    <link href="../../DHQRCss/jquery.jscrollpane.css" rel="stylesheet" type="text/css" />
    <script src="../../DHQRJs/main.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(initMenu);
        window.onresize = main_obj.page_init;
        function initMenu()
        {
            $.post("../Menu/GetUserMenuByUser", {}, function (result) {
                var parentData=[],data=[];
                $.each(result, function (key, val) {
                    if (val.IsParent) {
                        parentData.push(val);
                    } else {
                        data.push(val);
                    }
                });
                var dl = $("<dl></dl>");
                $.each(parentData, function (key, val) {
                    var dt = $('<dt><img src="../../DHQRImages/main/'+val.Icons+'" alt="'+val.Name+'" />'+val.Name+'</dt>');
                    var dd = $("<dd></dd>");
                    $.each(data, function (k, v) {
                        if (v.ParentMenuID == val.Id) {
                            var div = $('<div>◇<a href="' + v.Url + '" target="iframe" alt="">' + v.Name + '</a></div>');
                            div.appendTo(dd);
                        }
                    });
                    dt.appendTo(dl);
                    dd.appendTo(dl);
                });
                $(".menu").append(dl);
                main_obj.page_init();
            });
        }
    </script>
</head>
<body>
    <div id="header">
        <div class="logo">
            <img src="../../DHQRimages/WeiXin_Logo.png" />
            </div>
        <ul>
            <li class="ico-0"><a href="../../home/info" target="iframe">我的帐号</a></li>
            <li class="ico-6"><a href="./?m=account&a=profile" target="iframe">修改密码</a></li>
            <li class="ico-1"><a href="./?m=html&a=anstructions" target="iframe">使用说明</a></li>
            <li class="ico-2"><a href="../../home/info">使用手册下载</a></li>
            <li class="ico-3"><a href="./?m=html&a=vedio" target="iframe">视频教程</a></li>
            <%--<li class="ico-4"><a href="./?m=html&a=spread" target="iframe">推广技巧</a></li>--%>
            <li class="ico-5"><a href="../../Account/LoginOut">退出登录</a></li>
        </ul>
    </div>
    <div id="main">
        <div class="menu">
        </div>
        <div class="iframe">
            <iframe src="../../Home/Info" name="iframe" frameborder="0" scrolling="auto"></iframe>
        </div>
        <div class="clear">
        </div>
    </div>
    <div id="footer">
        <div class="oem">
            技术支持：和创科技</div>
    </div>
</body>
</html>
