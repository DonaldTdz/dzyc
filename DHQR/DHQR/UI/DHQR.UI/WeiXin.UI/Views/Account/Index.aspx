<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>主页</title>
    <link rel="Stylesheet" href="../../Content/css.src/mainStyle.css" />
    <link rel="Stylesheet" href="../../Content/css.src/menuStyle.css" />
    <script src="../../Scripts/jqueryUi/jquery-1.8.3.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/extantion.js/DHQR.menu.new.js"></script>
    <script src="../../Scripts/jqueryUi/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <link href="../../Content/JqueryUi/jquery-ui-1.9.2.custom.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/font-awesome/css/font-awesome.css" rel="Stylesheet" />
    <link href="../../Content/font-awesome/css/font.css" rel="Stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#center").css("height", document.documentElement.clientHeight - 80);
            $("#menu").css("height", document.documentElement.clientHeight - 115);
            $.post("../../Menu/GetMenuTree", {}, function (result) {
                var options = handleMenu(result, "");
                $("#menu").menuInit({ items: options, selected: 0 });
            });
            $("#main").height((document.body.clientHeight - 120) + "");
        });

        function handleMenu(data, title) {
            var result = [];
            $.each(data, function (key, val) {
                var item = { title: val.Title, icons: val.Icons };
                var currentTitle = title;
                if (title != "") {
                    currentTitle = title + "/" + val.Title;
                } else {
                    currentTitle = val.Title;
                }
                if (val.HasSubMenu) {
                    item.hasSubMenu = true;

                    item.items = handleMenu(val.ChildNodes, currentTitle);
                } else {
                    item.click = function () {
                        $("#main").attr("src", val.Url);

                        $("#currentDoc span").text(currentTitle);
                    }
                }
                result.push(item);
            });
            return result;
        }
        function setHome(url,name) {
            $("#main").attr("src", url);
            $("#currentDoc span").text(name)
        }
       
    </script>
</head>
<body>
    <div id="container">
        <div class="top" id="top">
            <div id="systemLogo" style="">
                <a href="../../Account/Index" style="cursor:pointer;display:inline-block;"><img alt="logo" src="../../Content/img/DHQR_Logo_Index.png" style="border:0px; height:55px;margin-top:10px;margin-left:20px;" /></a>
            </div>
            <div id="systemName">
                <span></span>
            </div>
            <div id="systemSet">
                <div class="welcome">
                    <span>
                        <%=ViewData["UserName"].ToString()%>，欢迎你使用信息处理系统！</span></div>
                <div class="userAciton">
                   <a onclick="setHome('Home','首页');"><i class="icon icon-book" style="position:relative;left:-10px;"></i>首页</a>
                   <a href="#"><i class="icon icon-cog" style="position:relative;left:-10px;"></i> 个人设置</a>
                   <a href="#"><i class="icon-question-sign" style="position:relative;left:-10px;"></i>帮助</a>
                   <a href="../../Account/LoginOut"><i class="icon icon-share-alt" style="position:relative;left:-10px;"></i>退出</a>
                </div>
            </div>
        </div>
        <div class="center" id="center">
            <div id="menuContianer" style="">
                <div style=" font-family:@黑体; line-height: 35px; width: 200px;
                    text-align: center; color: #eee; font-size: 18px; letter-spacing: 40px;
                    font-weight:bold;background-color: rgb(28,22,13);background-image: url(../../Content/JqueryUi/images/ui-bg_gloss-wave_30_44372c_500x100.png);background-position:50% 50%;">
                    菜单
                </div>
                <div id="menu" style="overflow-y: auto;">
                </div>
            </div>
            <div id="content">
                <div id="currentDoc">
                    <span style="font-family: @方正粗圆简体">首页</span>
                </div>
                <iframe style="width: 100%; border: none; overflow-y: auto;" src="Home" id="main">
                </iframe>
            </div>
        </div>
    </div>
</body>
</html>
