<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	WeboIndex
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>WeboIndex</h2>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
        $('body').layout();
        jQuery.support.cors = true;
        GetData();
        //            fun1();
    });

    function GetData() {
        //            $.ajax({
        //                url: "http://api.uniweibo.com/2/search/statuses.json?access_token=2.00RjIwQjU0MTZCNTQwNEY3NkNDMTIZ&q=可口可乐",
        //                data: requestEntity,
        //                contentType: "application/json;charset=utf-8",
        //                type: "GET",
        //                dataType: "json",
        //                catche: false,
        //                success: function (status) {
        //                    var kk = status;
        //                },
        //                error: function (XMLHttpRequest, textStatus, errorThrown) {
        //                    alert('提交Error:' + errorThrown.responseText);
        //                    }
        //                        });
        $.ajax({
            type: "GET",
            async: false,
            url: "http://api.uniweibo.com/oauth2/authorize?client_id=2727268457106&response_type=token&redirect_uri=http://127.0.0.1/callback?",
            dataType: "jsonp",
            //                jsonp: "callbackparam", //服务端用于接收callback调用的function名的参数
            //                jsonpCallback: "statuses", //callback的function名称
            success: function (statuses) {
                alert("总数为：" + statuses.data.total_number);
            },
            error: function (err) {
                alert('fail');
            }
        });
        //            $.getJSON("http://api.uniweibo.com/2/search/statuses.json?access_token=2.00RjIwQjU0MTZCNTQwNEY3NkNDMTIZ&q=可口可乐&jsoncallback=?", function (data) {
        //              alert(data.msg);
        //           });

    }
    function callback() {
        alert("OK");
    }

    function jsonCallBack(url, callback) {
        $.getScript(url, function () {
            callback(kkk);
        });
    }
    function fun1() {
        jsonCallBack('http://api.uniweibo.com/2/search/statuses.json?access_token=2.00RjIwQjU0MTZCNTQwNEY3NkNDMTIZ&q=可口可乐', function (statuses) {
            alert(statuses.message);
        })
    }
    function kkk() {

    }

    </script>
</asp:Content>
