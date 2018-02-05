<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>烟草微信管理平台</title>
    <script src="../../DHQRJs/Login/common.js" type="text/javascript"></script>
    <script src="../../DHQRJs/Login/SoftKeyBoard.js" type="text/javascript"></script>
    <script src="../../Scripts/Jquery/jquery-1.8.2.js" type="text/javascript"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <style type="text/css">
        html
        {
            color: #000;
            font-family: Arial,sans-serif;
            font-size: 12px;
        }
        h1, h2, h3, h4, h5, h6, p, ul, ol, div, span, dl, dt, dd, li, body, em, i, form, input, i, cite, button, img, cite, strong, em, label, fieldset, pre, code, blockquote, table, td, th, tr
        {
            padding: 0;
            margin: 0;
            outline: 0 none;
        }
        img, table, td, th, tr
        {
            border: 0;
        }
        address, caption, cite, code, dfn, em, th, var
        {
            font-style: normal;
            font-weight: normal;
        }
        select, img, select
        {
            font-size: 12px;
            vertical-align: middle;
            color: #666;
            font-family: Arial,sans-serif;
        }
        .checkbox
        {
            vertical-align: middle;
            margin-right: 5px;
            margin-top: -2px;
            margin-bottom: 1px;
        }
        textarea
        {
            font-size: 12px;
            color: #666;
            font-family: Arial,sans-serif;
        }
        table
        {
            border-collapse: collapse;
            border-spacing: 0;
        }
        ul, ol, li
        {
            list-style-type: none;
        }
        a
        {
            color: #0082cb;
            text-decoration: none;
        }
        a:hover
        {
            text-decoration: none;
        }
        ul:after, .clearfix:after
        {
            content: ".";
            display: block;
            height: 0;
            clear: both;
            visibility: hidden;
        }
        /* 不适合用clear时使用 */
        ul, .clearfix
        {
            zoom: 1;
        }
        .clear
        {
            clear: both;
            font-size: 0px;
            line-height: 0px;
            height: 1px;
            overflow: hidden;
        }
        /*  空白占位  */
        body
        {
            margin: 0 auto;
            font-size: 12px;
            background: #E0F1FB;
            color: #666;
            position: relative;
        }
        #wrap
        {
            margin-top: 80px;
        }
        .main
        {
            width: 800px;
            margin: 0px auto;
        }
        .main_L
        {
            width: 380px;
            float: right;
            background: url(../../images/linebg.png) no-repeat left center;
            padding-left: 25px;
            margin-right: 17px;
            display: inline;
        }
        .tabbox ul
        {
            margin-top: 10px;
        }
        .tabbox li
        {
            padding: 3px 0px 5px;
            position: relative;
        }
        .tabbox li.btn
        {
            padding-top: 10px;
            padding-left: 98px;
        }
        .tabbox .label
        {
            width: 350px;
            height: 38px;
            background: url(../../images/textbg.png) right -45px no-repeat;
        }
        .tabbox .label:hover
        {
            background: url(../../images/textbg.png) right 0px no-repeat;
        }
        .labelhover
        {
            width: 350px;
            height: 38px;
            background: url(../../images/textbg.png) right 0px no-repeat;
        }
        .tabbox label
        {
            font-size: 14px;
            color: #666;
        }
        .tabbox .input, .tabbox .textinput
        {
            width: 230px;
            height: 26px;
            line-height: 26px;
            padding: 2px;
            padding-left: 5px;
            border: 0px;
            margin-top: 3px;
            margin-left: 10px;
            background-color: transparent;
            font-family: Verdana, Arial, Helvetica, sans-serif;
        }
        .tabbox .textinputhover, .tabbox .textinputhover
        {
            border: 1px solid #aaa;
        }
        .regsubmit
        {
            width: 182px;
            height: 53px;
            border: 0px none;
            background: url(../../images/reg_btn.jpg) 0px 0px no-repeat;
            cursor: pointer;
        }
        .regsubmit:hover
        {
            background: url(../../images/reg_btn.jpg) 0px -53px no-repeat;
        }
        .main_R
        {
            width: 330px;
            float: left;
            margin-left: 38px;
            display: inline;
        }
        .tabbox .companyul
        {
            margin-top: 20px;
        }
        .rzm
        {
            margin-left: 30px;
            line-height: 25px;
            color: #999999;
        }
        .rzm span
        {
            color: #CC0000;
        }
        .family
        {
            margin-top: 80px;
            line-height: 25px;
            font-size: 14px;
            font-family: "微软雅黑";
        }
        .family h3
        {
            height: 40px;
            text-align: center;
            line-height: 40px;
            font-size: 30px;
            font-weight: bold;
            color: #FD8504;
        }
        .family h3 span
        {
            font-size: 30px;
            color: #666;
        }
        .foot
        {
            width: 800px;
            margin: 0px auto;
            text-align: center;
            padding: 8px 0 0 0px;
            line-height: 24px;
        }
        .foot a
        {
            color: #474747;
        }
        .foot a:visited
        {
            color: #666;
        }
    </style>
</head>
<body id="wrap">
    <table width="809" border="0" height="418" align="center" style="margin: 0 auto;
        background: url(../../images/regbg.png);">
        <tr>
            <td>
                <div id="step_1" class="main">
                    <div class="main_L">
                        <div class="tabbox">
                            <ul id="regSpan" class="companyul">
                                <li style="z-index: 1000">
                                    <div class="label">
                                        <label for="LoginName" style="padding-left: 28px">
                                            登录账号：</label><input type="text" name="LoginName" id="LoginName" class="textinput"
                                                tabindex="1" autocomplete="off" />
                                    </div>
                                </li>
                                <li>
                                    <div class="label">
                                        <label for="PassWord" style="padding-left: 28px">
                                            登录密码：</label><input type="password" tabindex="2" name="PassWord" id="PassWord" class="textinput" />
                                    </div>
                                </li>
                                <li class="btn" id="nextStep">
                                    <input type="button" tabindex="5" class="regsubmit" value=" " id="btnLogin" name="btnLogin">
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="main_R">
                        <div class="family">
                            <h3>
                                烟草微信<span>管理平台</span></h3>
                            <br />
                            欢迎您使用<sup>TM</sup>产品,我们一直在努力并提供能为您带来顶级体验的软件产品...
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".label").hover(function () {
                $(this).removeClass("label"); $(this).addClass("labelhover");
            }, function () {
                $(this).removeClass("labelhover"); $(this).addClass("label");
            });
        });
        setTimeout(function () { $("#LoginName").focus(); }, 500);
        $(function () {
            $("#btnLogin").click(function () {
                $('#btnLogin').attr('disabled', 'disabled');
                $.post('../../Account/LoginInfo',
             { Name: $('#LoginName').val(), PassWord: $('#PassWord').val() },
             function (data) {
                 if (data.IsSuccessful)
                 { document.location = "../../home/default"; }
                 else {
                     $('#btnLogin').attr('disabled', '');
                     $('#btnLogin').removeAttr("disabled");
                     alert(data.OperateMsg);
                 }
             });
            });

            document.onkeydown = function () {
                if ((event.keyCode == 13)) {
                    $("#btnLogin").click();
                }
            };
        });
    </script>
</body>
</html>
