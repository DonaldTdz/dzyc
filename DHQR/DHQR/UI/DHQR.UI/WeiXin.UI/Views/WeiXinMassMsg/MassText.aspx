<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    满意度分析
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
                    <li><a href="../WeiXinMassMsg/SourceManage">图文群发</a></li>
                    <li class="cur"><a href="../WeiXinMassMsg/MassText">文字群发</a></li>
                   <%-- <li><a href="#">图片群发</a></li>
                    <li><a href="#">语音群发</a></li>
                    <li><a href="#">视频群发</a></li>--%>
                </ul>
            </div>
            <div id="stores" class="r_con_wrap" style="padding-bottom:0px;min-height:450px;">
                <div class="control_btn" style="padding-bottom:0px;height:15px;margin-bottom:12px;">
                    <div class="tips_info"><strong>提示：</strong>输入文字进行群发操作</div>
                </div>
                <form class="r_con_search_form" method="get">
                     <label>群发对象：</label>
                    <%=Html.DropDownList("TargetList",ViewData["TargetList"] as IList<SelectListItem>,new { @class = "ui-text-select"  }) %>
                     <label>分组：</label>
                    <%=Html.DropDownList("GroupList",ViewData["GroupList"] as IList<SelectListItem>,new { @class = "ui-text-select"  }) %>
                </form>
                <div id="charContainer">
                  <textarea id="content" style="width:80%;height:300px;font-family:'Microsoft YaHei' "></textarea>
                </div>
                <div style="margin-top:2%;">
                 <input type="button" id="saveData" class="btn_ok" name="submit_button" onclick="SendText();" value="群发" />
                 <%--<a href="../WeiXinMassMsg/SourceManage" class="btn_cancel">返回</a>--%>              

                </div>
            </div>

        </div>
    </div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
    <link href="../../DHQRCss/global.css" rel="stylesheet" type="text/css" />
    <link href="../../DHQRCss/frame.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/JqueryUi/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" />
    <script src="../../Scripts/jqueryUi/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="../../Scripts/jqueryUi/timepicker.js" type="text/javascript"></script>
    <script src="../../Scripts/jqueryUi/ui.datepicker-zh-CN.js" type="text/javascript"></script>
        <script src="../../Scripts/ckeditor/ckeditor.js"></script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
    <script type="text/javascript">

        //群发信息
        function SendText() {
            var content = $("#content").val();
            if (trim(content) == "" || content == undefined)
            {
                alert("群发消息不能为空！");
                return;
            }
            if (confirm("确定要群发吗？")) {
                var target = $("#TargetList").val(), groupid = $("#GroupList").val();
               
                $.post("../../WeiXinMassMsg/SendTextMsg", { content: content, target: target, groupid: groupid }, function (result) {
                    if (result.IsSuccessful) {
                        alert("群发成功");
                    } else {
                        alert(resut.OperateMsg);
                    }
                });
            }
            else {
            }

        }

        //供使用者调用 
        function trim(s) {
            return trimRight(trimLeft(s));
        }
        //去掉左边的空白 
        function trimLeft(s) {
            if (s == null) {
                return "";
            }
            var whitespace = new String(" \t\n\r");
            var str = new String(s);
            if (whitespace.indexOf(str.charAt(0)) != -1) {
                var j = 0, i = str.length;
                while (j < i && whitespace.indexOf(str.charAt(j)) != -1) {
                    j++;
                }
                str = str.substring(j, i);
            }
            return str;
        }
        //去掉右边的空白 
        function trimRight(s) {
            if (s == null) return "";
            var whitespace = new String(" \t\n\r");
            var str = new String(s);
            if (whitespace.indexOf(str.charAt(str.length - 1)) != -1) {
                var i = str.length - 1;
                while (i >= 0 && whitespace.indexOf(str.charAt(i)) != -1) {
                    i--;
                }
                str = str.substring(0, i + 1);
            }
            return str;
        }

    </script>
</asp:Content>
