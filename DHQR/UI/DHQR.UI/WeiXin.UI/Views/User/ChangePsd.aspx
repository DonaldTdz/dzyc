<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DipsPage.Master" Inherits="System.Web.Mvc.ViewPage<UserModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="">
        <h1 class="main_tit">
            <span></span>修改登录密码 <strong>Password</strong>
        </h1>
        <div class="form_box">
            <form name="frmPost" id="frmPost" action="">
            <dl>
                <dt>用户名：</dt>
                <dd>
                    <%=ViewData["UserName"]%></dd>
            </dl>
            <dl>
                <dt>旧密码：</dt>
                <dd>
                    <input name="txtOldPassword" id="txtOldPassword" type="password" class="input txt required"
                        minlength="6" maxlength="50" /></dd>
            </dl>
            <dl>
                <dt>新密码：</dt>
                <dd>
                    <input name="txtPassword" id="txtPassword" type="password" class="input txt required"
                        minlength="6" maxlength="50" /></dd>
            </dl>
            <dl>
                <dt>确认新密码：</dt>
                <dd>
                    <input name="txtPassword1" id="txtPassword1" type="password" class="input txt required"
                        equalto="#txtPassword" minlength="6" maxlength="20" /></dd>
            </dl>
            </form>
            <dl>
                <dt></dt>
                <dd>
                    <input name="btnSubmit" id="btnSubmit" type="button" class="btn_submit" onclick="save();"
                        value="确认修改" /></dd>
            </dl>
        </div>
        <!--/修改密码-->
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ResourceContent" runat="server">

    <link href="../../Content/Box/Content.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Box/jquery.Box.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BottomScript" runat="server">
    <script type="text/javascript">
        function save() {

            if ($('#txtPassword').val() != $('#txtPassword1').val()) {
                alert('新密码输入不一致');
                $('#txtPassword').val("");
                $('#txtPassword1').val("");
            }

            $.post('../../User/LoginPsdChange', { OldPsd: $('#txtOldPassword').val(), NewPsd: $('#txtPassword').val() },
            function (data) {
                $.DialogBox.tip(data.OperateMsg);
                if (data.IsSuccessful) {
                    CleanFormData("frmPost");
                }

            });

        }
    </script>
</asp:Content>
