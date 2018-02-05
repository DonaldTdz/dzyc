<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

    //protected void Page_Load(object sender, EventArgs e)
    //{

    //}
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>世博投资信息处理系统－新用户注册</title>
    <link rel="stylesheet" href="../../img/login.css" type="text/css" />
    <style type="text/css">
        .STYLE1
        {
            color: #6E6F71;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="control">
        <div class="top">
            <div class="top_1">
                设为首页&nbsp;<span class="font_1">|</span>&nbsp;添加到收藏夹</div>
            <div class="top_2">
                <span class="font_2">你好，欢迎来到世博投资信息处理系统！</span>&nbsp;&nbsp; <span class="font_3">*******&nbsp;<span
                    class="font_1">|</span>&nbsp;**********&nbsp; <span class="font_1">|</span>&nbsp;<a
                        href="../../home/login">回到首页</a>&nbsp;
            </div>
        </div>
        <div class="login_t">
            <div class="reg_t2">
                <div class="login_t4">
                </div>
                <div class="reg_t5">
                </div>
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="52" colspan="3" bgcolor="#FFFFFF" class="login_aa2" style="line-height: 20px">
                            <b>请认真、仔细地填写以下信息！ (带*为必填项目)</b>
                        </td>
                    </tr>
                    <%-- <tr>
                        <td width="169" align="right" bgcolor="#FFFFFF" style="height: 30px">
                            <span class="font_2">*</span>&nbsp;身份证号：
                        </td>
                        <td width="387" bgcolor="#FFFFFF" style="height: 30px; width: 244px;">
                            <asp:TextBox ID="TextBox1" runat="server" Width="220px"></asp:TextBox><br />
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1"
                                Display="Dynamic" ErrorMessage="请输入用户名！"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBox1"
                                ErrorMessage="用户名为6到20个字符，可使用汉字、英文字母、数字及下划线！" ValidationExpression='[\u4e00-\u9fa5A-Za-z0-9_]{6,20}'
                                Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
                        <td width="658" bgcolor="#FFFFFF" style="height: 30px">
                            &nbsp; <span class="font_3">
                                <input type="submit" name="jiance" value="检查登录名是否可用" id="jiance" />
                        </td>
                    </tr>--%>
                    <tr>
                        <td width="169" align="right" bgcolor="#FFFFFF" style="height: 40px">
                            <span class="font_2">*</span>&nbsp;身份证号：
                        </td>
                        <td bgcolor="#FFFFFF" style="width: 244px; height: 40px;">
                            <asp:TextBox ID="TextBox5" runat="server" Width="220px" TextMode="Password"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox3"
                                Display="Dynamic" ErrorMessage="请输入密码！"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox3"
                                ErrorMessage="密码至少6个字符，可使用字母、数字及下划线！" ValidationExpression="^\w{6,}$" Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
                        <td bgcolor="#FFFFFF" style="height: 40px">
                            <span class="STYLE1">身份证是系统登录使用的用户名，请输入正确！</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#FFFFFF" style="height: 40px">
                            <span class="font_2">*</span>&nbsp;登录密码：
                        </td>
                        <td bgcolor="#FFFFFF" style="width: 244px; height: 40px;">
                            <asp:TextBox ID="TextBox3" runat="server" Width="220px" TextMode="Password"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox3"
                                Display="Dynamic" ErrorMessage="请输入密码！"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TextBox3"
                                ErrorMessage="密码至少6个字符，可使用字母、数字及下划线！" ValidationExpression="^\w{6,}$" Display="Dynamic"></asp:RegularExpressionValidator>
                        </td>
                        <td bgcolor="#FFFFFF" style="height: 40px">
                            <span class="STYLE1">密码由6-20个英文字母组成（区分大小写）或数字组成，建议采用易记、难猜的英文数字组合。</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#FFFFFF" style="height: 40px">
                            <span class="font_2">*</span> 确认密码：
                        </td>
                        <td bgcolor="#FFFFFF" style="width: 244px; height: 40px;">
                            <asp:TextBox ID="TextBox4" runat="server" Width="220px" TextMode="Password"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TextBox4"
                                Display="Dynamic" ErrorMessage="请确认密码"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBox3"
                                ControlToValidate="TextBox4" Display="Dynamic" ErrorMessage="两次输入的密码不一致！"></asp:CompareValidator>
                        </td>
                        <td bgcolor="#FFFFFF" style="height: 40px">
                            <span class="STYLE1">请再输一遍您上面填写的密码。 </span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#FFFFFF" style="height: 40px">
                            姓名：
                        </td>
                        <td bgcolor="#FFFFFF" align="left" style="height: 40px">
                            <asp:TextBox ID="TextBox2" runat="server" Width="220px"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2"
                                Display="Dynamic" ErrorMessage="请输入您的姓名！"></asp:RequiredFieldValidator>
                        </td>
                        <td bgcolor="#FFFFFF" style="height: 40px">
                            <span style="color: #6e6f71">请填写您真实的姓名。 </span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#FFFFFF" style="height: 25px">
                            性别：
                        </td>
                        <td bgcolor="#FFFFFF" align="left" style="height: 25px">
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Selected="True" Value="男">男</asp:ListItem>
                                <asp:ListItem Value="女">女</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td bgcolor="#FFFFFF" style="height: 25px">
                            <span style="color: #6e6f71">请选择您的性别。</span>
                        </td>
                    </tr>
                    <%--     <tr>
                        <td align="right" bgcolor="#FFFFFF" style="height: 40px">
                            <span class="font_2">*</span> 密码提示问题：
                        </td>
                        <td bgcolor="#FFFFFF" align="left">
                            <asp:TextBox ID="TextBox5" runat="server" Width="220px"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox5"
                                ErrorMessage="请输入问题！" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td bgcolor="#FFFFFF" style="height: 40px">
                            <span class="STYLE1">忘记密码的提示问题，最好设成别人不知道答案的问题，以防被人用找回密码功能窃取您的帐号。</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" bgcolor="#FFFFFF" style="height: 40px">
                            <span class="font_2">*</span> 密码提示答案：
                        </td>
                        <td bgcolor="#FFFFFF" align="left">
                            <asp:TextBox ID="TextBox6" runat="server" Width="220px"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBox6"
                                ErrorMessage="请输入回答！" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td bgcolor="#FFFFFF" style="height: 40px">
                            <span class="STYLE1">忘记密码的提示问题答案，用于取回密码。请尽量设得复杂一些，以防被人用找回密码功能窃取您的帐号。</span>
                        </td>
                    </tr>--%>
                    <%--     <tr>
                        <td align="right" bgcolor="#FFFFFF" style="height: 40px">
                            <span class="font_2">*</span>&nbsp;电子邮件：
                        </td>
                        <td bgcolor="#FFFFFF" align="left">
                            <asp:TextBox ID="TextBox7" runat="server" Width="220px"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBox7"
                                Display="Dynamic" ErrorMessage="邮箱不能为空！"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox7"
                                Display="Dynamic" ErrorMessage="邮编格式错误" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </td>
                        <td bgcolor="#FFFFFF" style="height: 40px">
                            <span class="STYLE1">重要！我们需要您通过邮箱完成注册，请填写常用的电子邮箱，这也是客户联系您的首选方式！</span>
                        </td>
                    </tr>--%>
                    <tr>
                        <td align="right" bgcolor="#FFFFFF" style="height: 40px">
                            所在城市：
                        </td>
                        <td colspan="2" align="left" bgcolor="#FFFFFF">
                            <table width="100%" height="33" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="88">
                                        <div align="left">
                                            <asp:DropDownList ID="Ddl_AreaOne" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                                Width="85px">
                                                <asp:ListItem Value="0">
                      全部
                                                </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td width="88">
                                        <div align="left">
                                            <asp:DropDownList ID="Ddl_AreaTwo" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                                Width="85px">
                                                <asp:ListItem Value="0">
                      全部
                                                </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td width="88">
                                        <div align="left">
                                            <asp:DropDownList ID="Ddl_AreaThree" runat="server" AppendDataBoundItems="True" Width="85px">
                                                <asp:ListItem Value="0">
                      全部
                                                </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </td>
                                    <td width="787">
                                        <div align="left">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="40" colspan="3">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:CheckBox ID="CheckBox1" runat="server" />
                            我已经看过并同意《<a href="#" target="_blank"><u>在线服务条款</u></a>》
                        </td>
                    </tr>
                    <tr>
                        <td height="30" colspan="3" align="center">
                            &nbsp;
                            <input type="submit" name="Btn_Reg" value="提　交　注　册" id="Btn_Reg" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <% Html.RenderPartial("Buttom");%>
        <br clear="all" />
    </div>
    </form>
</body>
</html>
