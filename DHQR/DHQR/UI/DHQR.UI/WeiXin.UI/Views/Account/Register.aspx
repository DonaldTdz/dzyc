<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout.Master" Inherits="System.Web.Mvc.ViewPage<UserApplyModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    用户申请
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="uinorth" class="ui-layout-north">
        <div id="regmain" style="width:660px; margin: 0px auto;">
            <!-- head begin -->
            <div style="padding-bottom: 0px;margin: 0px auto; padding-left: 0px; padding-right: 0px;
                padding-top: 0px">
                <div class="header">
                    <div class="logo">
                        <img alt="注册" src="../../Content/img/DHQRLogo.png">
                    </div>
                    <div id="systemSet">
                        <div class="userAciton">
                            <a href="../../Account/login"><i class="icon icon-home"></i>首页</a><a href="#"><i
                                class="icon-question-sign"></i>帮助中心</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="uiCenter" class="ui-layout-center">
        <div style="width:660px;margin:0px auto;">
        <table align="center" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div id="wizard" class="swMain">
                        <ul>
                            <li><a href="#step-1">
                                <label class="stepNumber">
                                    1</label>
                                <span class="stepDesc">第一步<br />
                                    <small>申请用户名</small> </span></a></li>
                            <li><a href="#step-2">
                                <label class="stepNumber">
                                    2</label>
                                <span class="stepDesc">第二步<br />
                                    <small>详细信息</small> </span></a></li>
                            <li><a href="#step-3">
                                <label class="stepNumber">
                                    3</label>
                                <span class="stepDesc">完成<br />
                                    <small>提交处理</small> </span></a></li>
                        </ul>
                        <div id="step-1" style="width:400px;">
                            <h2 class="StepTitle">
                                填写基本信息:</h2>
                            <form id="Form1" class="frmPost" method="post" action="">
                            <div class="form-horizontal">
                                <label>
                                    用户名</label>
                                <input id="Name" name="Name" type="text" class="required  ui-input-text ui-input-text-form" />
                            </div>
                            <div class="form-horizontal">
                                <label>
                                    密码</label>
                                <input id="PassWord" name="PassWord" type="password" class="required  ui-input-text ui-input-text-form" />
                            </div>
                            <div class="form-horizontal">
                                <label>
                                    确认密码</label>
                                <input id="psd1" name="psd1" type="password" class="required  ui-input-text ui-input-text-form" />
                            </div>
                            <div class="control-group">
                                <label>
                                    申请角色：</label>
                                    <%:Html.DropDownListFor(f=>f.ModuleRoleId, ViewData["ModuleRoles"] as IEnumerable<SelectListItem>, "", new { @readonly = "readonly" })%>
                            </div>
                            </form>
                        </div>
                        <div id="step-2" style="width:400px;">
                            <form id="Form2" class="frmPost" method="post" action="">
                            <h2 class="StepTitle">
                                填写详情信息</h2>
                            <div class="form-horizontal">
                                <label>
                                    昵称</label>
                                <input id="Nickname" type="text" name="Nickname" class="required  ui-input-text ui-input-text-form" />
                            </div>
                            <div class="form-horizontal">
                                <label>
                                    邮箱地址
                                </label>
                                <input id="EMail" type="text" name="EMail" class="required  ui-input-text ui-input-text-form" />
                            </div>
                            <div class="form-horizontal">
                                <label>
                                    电话号码
                                </label>
                                <input id="Tel" type="text" name="Tel" class="required  ui-input-text ui-input-text-form" />
                            </div>
                            <div class="form-horizontal">
                                <label>
                                    申请说明</label>
                                <input id="Remark" type="text" name="Remark" class="required  ui-input-text ui-input-text-form" />
                            </div>
                            </form>
                        </div>
                        <div id="step-3" style="width:400px;">
                            <form id="Form3" class="frmPost" method="post" action="">
                            <h2 class="StepTitle">
                                &nbsp;&nbsp;&nbsp;&nbsp; 确认填入内容：</h2>
                            <div style="height: 20px;">
                            </div>
                            <div style="height: 20px;" class="form-horizontal">
                                &nbsp;&nbsp;&nbsp;&nbsp; 用户名：<span id="Span1"></span>
                            </div>
                            <div style="height: 20px;" class="form-horizontal">
                                &nbsp;&nbsp;&nbsp;&nbsp; 昵称：<span id="Span2"></span>
                            </div>
                            <div style="height: 20px;" class="form-horizontal">
                                &nbsp;&nbsp;&nbsp;&nbsp; 申请角色：<span id="Span6"></span>
                            </div>
                            <div style="height: 20px;" class="form-horizontal">
                                &nbsp;&nbsp;&nbsp;&nbsp; 邮箱地址：<span id="Span3"></span>
                            </div>
                            <div style="height: 20px;" class="form-horizontal">
                                &nbsp;&nbsp;&nbsp;&nbsp; 电话号码：<span id="Span5"></span>
                            </div>
                            <div style="height: 20px;" class="form-horizontal">
                                &nbsp;&nbsp;&nbsp;&nbsp; 申请说明：<span id="Span4"></span>
                            </div>
                            </form>
                        </div>
                    </div>
                </td>
            </tr>
        </table></div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
    <link href="../../Content/SmartWizard/smart_wizard_vertical.css" rel="stylesheet"
        type="text/css" />
    <link href="../../Content/JqGrid/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/css/select2.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/Common/fromCommon.css" rel="stylesheet" type="text/css" />
    <link rel="Stylesheet" href="../../Content/css.src/mainStyle.css" />
    <link rel="Stylesheet" href="../../Content/css.src/menuStyle.css" />
    <script src="../../Scripts/SmartWizard/jquery.smartWizard-2.0.min.js" type="text/javascript"></script>
    <link href="../../css/Regster.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/font-awesome/css/font-awesome.css" rel="Stylesheet" />
    <link href="../../Content/font-awesome/css/font.css" rel="Stylesheet" />
    <style type="text/css">
        .header
        {
            padding-bottom: 0px;
            margin: 0px auto;
            padding-left: 20px;
            width: 660px;
            padding-right: 0px;
            height: 75px;
            padding-top: 20px;
        }
        .logo
        {
            width: 220px;
            float: left;
            margin-left:200px;
        }
        .other
        {
            width: 250px;
            float: right;
        }
        .other LI
        {
            text-align: center;
            line-height: 56px;
            width: 60px;
            float: left;
        }
        
        #uiCenter
        {
            background-color: rgb(46, 54, 63);
            color: rgb(46, 54, 63);
        }
        
        #uinorth
        {
            background-color: rgb(46, 54, 63);
            color: rgb(46, 54, 63);
        }
        .actionBar
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
    <script type="text/javascript">
        $(function () {
            $("body").layout();
            $("#ModuleRoleId").css("width", "165px");
        });

        var myLayout = $("#container").layout({
            "north": { size: "130", closable: false, resizable: false, slidable: false
                     , spacing_open: 0, togglerLength_open: 0, togglerLength_closed: 0
            }
        });

        $(document).ready(function () {
            // Smart Wizard 	
            $('#wizard').smartWizard({
                selected: 0,  // Selected Step, 0 = first step   
                keyNavigation: true, // Enable/Disable key navigation(left and right keys are used if enabled)
                enableAllSteps: false,
                transitionEffect: 'fade', // Effect on navigation, none/fade/slide/slideleft
                contentURL: null, // content url, Enables Ajax content loading
                contentCache: false, // cache step contents, if false content is fetched always from ajax url
                cycleSteps: false, // cycle step navigation
                enableFinishButton: false, // make finish button enabled always
                errorSteps: [],    // Array Steps with errors 
                onLeaveStep: leave, // triggers when leaving a step
                onShowStep: null,  // triggers when showing a step
                onFinish: show, // triggers when Finish button is clicked  
                labelNext: '下一步',
                labelPrevious: '上一步',
                labelFinish: '提交申请'
            });
            $(".actionBar").width(658);
            function onFinishCallback() {
                $('#wizard').smartWizard('showMessage', 'Finish Clicked');
            }
        });


        function show() {
            var name = $('#Name').val();
            var psd = $('#PassWord').val();
            var psd1 = $('#psd1').val();

            var Nickname = $('#Nickname').val();
            var email = $('#EMail').val();
            var tel = $('#Tel').val();
            var roleId = $("#ModuleRoleId").val();
            if (psd == "" || psd == null) {
                alert("密码不能为空，请在第一步里面重新输入！");
                return;
            }

            if (psd != psd1) {
                alert("二次输入密码不一致，请在第一步里面重新输入！"); return;
            }
            if (name == "" || name == null) {
                alert("用户名不能为空，请在第一步里面重新输入！");
                return;
            }
            if (name.length < 4) {
                alert("用户名必须长度大于4位，请在第一步里面重新输入！");
                return;
            }
            if (Nickname == "" || Nickname == null) {
                alert("昵称不能为空，请在第二步里面重新输入！");
                return;
            }
            if (roleId == "" || roleId == null) {
                alert("申请角色不能为空，请在第一步里面重新输入！");
                return;
            }
            if (email == "" || email == null) {
                alert("邮箱不能为空，请在第二步里面重新输入！");
                return;
            }
            if (tel == "" || tel == null) {
                alert("电话不能为空，请在第二步里面重新输入！");
                return;
            }

            var data1 = $("#Form1").serialize();
            var data2 = $("#Form2").serialize();
            var data3 = $("#Form3").serialize();
            var data = (data1 + "&" + data2 + "&" + data3);
            $.post('../../Account/DoRegister', data, function (data) {
                alert(data.OperateMsg);
                if (data.IsSuccessful) {
                    document.location = "../../Account/Login";
                }
            });
        }
        function leave() {
            var selRoleName = getSelectedText("ModuleRoleId");
            $('#Span1').text($('#Name').val());
            $('#Span2').text($('#Nickname').val());
            $('#Span3').text($('#EMail').val());
            $('#Span4').text($('#Remark').val());
            $('#Span5').text($('#Tel').val());
            $('#Span6').text(selRoleName);
            return true;
        }
        //获取下拉列表选中项的文本
        function getSelectedText(Id) {
            var obj = document.getElementById(Id);
            for (i = 0; i < obj.length; i++) {
                if (obj[i].selected == true) {
                    return obj[i].innerText;      //关键是通过option对象的innerText属性获取到选项文本
                }
            }
        }

        //获取下拉列表选中项的值
        function getSelectedValue(Id) {
            var obj = document.getElementById(Id);
            return obj.value;      //如此简单，直接用其对象的value属性便可获取到
        }

    </script>
</asp:Content>
