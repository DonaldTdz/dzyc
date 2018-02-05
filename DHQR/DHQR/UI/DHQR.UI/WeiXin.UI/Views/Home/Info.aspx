<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <meta charset="utf-8">
    <title>烟草微信平台</title>
    <link href='../../DHQRCss/global.css' rel='stylesheet' type='text/css' />
    <link href='../../DHQRCss/main.css?t=20140127' rel='stylesheet' type='text/css' />
    <script type='text/javascript' src='../../DHQRJs/jquery-1.7.2.min.js'></script>
    <script type='text/javascript' src='../../DHQRJs/global.js'></script>
    <link href="../../DHQRCss/account.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body, html
        {
            background: url(../../DHQRImages/main/main-bg.jpg) left top fixed no-repeat;
        }
    </style>
</head>
<body>
    <div id="iframe_page">
        <div class="iframe_content">
            <link href='../../DHQRCss/account.css?t=20140127' rel='stylesheet' type='text/css' />
            <script src="../../DHQRJs/account.js" type="text/javascript"></script>
            <script language="javascript">                $(document).ready(account_obj.index_init);</script>
            <div id="home">
                <div class="welcome">
                    <div>
                    </div>
                    <span>广元烟草</span></div>
                <div class="info">
                    <div class="member">
                        <div class="face">
                            <img src="../../DHQRImages/account/face.jpg" /></div>
                        <ul class="info_list">
                            <li>
                                <img src="../../DHQRImages/account/icon-0.png" />公众账号：已绑定【gyycgs@163.com】</li>
                            <li>
                                <img src="../../DHQRImages/account/icon-1.png" />帐号状态：正常</li>
                            <li>
                                <img src="../../DHQRImages/account/icon-3.png" />当前日期：<%=DateTime.Now.ToString("yyyy-MM-dd") %></li>
                            <li>
                                <img src="../../DHQRImages/account/icon-2.png" />当前用户：<%=ViewData["CurrentUser"]%></li>
                        </ul>
                    </div>
                    <div class="data">
                        <div class="d0">
                            <a href="#" group="statistics">
                                <h1>
                                    <%=ViewData["DistCount"]%></h1>
                                <h2>
                                    今日配送任务</h2>
                            </a>
                        </div>
                        <div class="d1">
                            <a href="#" group="statistics">
                                <h1>
                                    <%=ViewData["FinishCount"]%></h1>
                                <h2>
                                    已完成任务</h2>
                            </a>
                        </div>
                        <div class="d2">
                            <a href="#"  group="app">
                                <h1>
                                    <%=ViewData["NotfinishCount"]%></h1>
                                <h2>
                                    未完成任务</h2>
                            </a>
                        </div>
                        <div class="d3">
                            <a href="#" group="action">
                                <h1>
                                    <%=ViewData["TotalMoney"]%></h1>
                                <h2>
                                    货款金额</h2>
                            </a>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="list">
                    <div>
                        <img src="../../DHQRImages/account/list-app.png" /></div>
                    <ul>
                        <li><a href="../../home/info" group="#">
                            <img src="../../DHQRImages/account/account.png"><br />
                            我的帐号</a></li>
                        <li><a href="#" group="wechat">
                            <img src="../../DHQRImages/account/reply_keyword.png"><br />
                            配送任务</a></li>
                        <li><a href="#" group="material">
                            <img src="../../DHQRImages/account/material.png"><br />
                            配送人员</a></li>
                        <li><a href="#" group="web">
                            <img src="../../DHQRImages/account/web.png"><br />
                            送货监督</a></li>
                        <li><a href="#" group="shop">
                            <img src="../../DHQRImages/account/shop.png"><br />
                            配送路线查询</a></li>
                    </ul>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
</body>
</html>
