<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    群发图文素材
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
                    <li class="cur"><a href="../WeiXinMassMsg/SourceManage">图文群发</a></li>
                    <li><a href="../WeiXinMassMsg/MassText">文字群发</a></li>
                   <%-- <li><a href="#">图片群发</a></li>
                    <li><a href="#">语音群发</a></li>
                    <li><a href="#">视频群发</a></li>--%>
                </ul>
            </div>
            <div id="material" class="r_con_wrap">
                <form class="r_con_search_form" method="get">
                     <label>群发对象：</label>
                    <%=Html.DropDownList("TargetList",ViewData["TargetList"] as IList<SelectListItem>,new { @class = "ui-text-select"  }) %>
                     <label>分组：</label>
                    <%=Html.DropDownList("GroupList",ViewData["GroupList"] as IList<SelectListItem>,new { @class = "ui-text-select"  }) %>
                </form>

                <div class="list">
                    <div class="item first_item">
                        <div class="multi">
                            <div><a href="#" onclick="redirectTo();"></a></div>
                            <a href="#" onclick="redirectTo();">+新增图文素材</a>
                        </div>
                    </div>
                    <div class="clear"></div>
                </div>
                <div class="blank12"></div>
                <%--                <div id="turn_page"><font class='page_noclick'>上一页</font>&nbsp;<font class='page_item_current'>1</font>&nbsp;<font class='page_noclick'>下一页>></font></div>--%>
            </div>
        </div>
    </div>
   
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
    <link href="../../DHQRCss/global.css" rel='stylesheet' type='text/css' />
    <link href="../../DHQRCss/frame.css" rel='stylesheet' type='text/css' />
    <script type='text/javascript' src="../../DHQRJs/jquery-1.7.2.min.js"></script>
    <script type='text/javascript' src="../../DHQRJs/frame.js"></script>
    <link href="../../DHQRCss/material.css" rel='stylesheet' type='text/css' />
    <script type='text/javascript' src="../../DHQRJs/masonry.min.js"></script>
    <script type='text/javascript' src="../../DHQRJs/material.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $.post("../WeiXinMassMsg/QueryMassGroup", {}, function (result) {
                var container = $("#material .list");
                var target = $("#TargetList").val(), groupid = $("#GroupList").val();
                $.each(result, function (key, val) {
                    var div = $('<div class="item multi" ></div>');
                    var time = $('<div class="time">' + val.MsgHeader.CreateTimeStr + '</div>');
                    var first = $('<div class="first"></div>');
                    var info = $('<div class="info"><div class="img"><img src="' + val.MsgHeader.pic_url + '" /></div><div class="title">' + val.MsgHeader.title + '</div></div>');
                        time.appendTo(div);
                        info.appendTo(first);
                        first.appendTo(div);
                        container.append(div);
                        $.each(val.MsgDetails, function (k, v) {
                            var list = $('<div class="list"></div>');
                            info = $('<div class="info"><div class="img"><img src="' + v.pic_url + '" /></div><div class="title">' + v.title + '</div></div>');
                            info.appendTo(list);
                            list.appendTo(div);
                        });
                        var mod_del = $('<div class="mod_del"></div>');
                        var mod = $('<div class="mod" style="width:100px;"><a href="../WeiXinMassMsg/CreateOrEdit?id=' + val.MsgHeader.Id + '&target=' + target + '&groupid='+groupid + '" style="cursor:pointer;"><img src="../../DHQRImages/ico/mod.gif" /></a></div>');
                        var del = $('<div class="mod" style="width:100px;"><a onclick="deleteItem(this,\'' + val.MsgHeader.Id + '\')"  style="cursor:pointer;"><img src="../../DHQRImages/ico/del.gif" /></a></div>')
                        var send = $('<div class="del" style="width:100px;"><a onclick="sendMsg(this,\'' + val.MsgHeader.Id + '\')"  style="cursor:pointer;"><img src="../../DHQRImages/ico/send.png" /></a></div>')
                    mod.appendTo(mod_del);
                    del.appendTo(mod_del);
                    send.appendTo(mod_del);
                    mod_del.appendTo(div);

                });
                frame_obj.search_form_init();

                material_obj.material_init();
            });
        });


        function deleteItem(obj, id) {
            //$.post("../../WeiXinMassMsg/DeleteData", { id: id }, function (result) {
            //    if (result.IsSuccessful) {
            //        $(obj).closest(".multi").remove();
            //        material_obj.material_init();
            //    } else {
            //        alert(resut.OperateMsg);
            //    }
            //});
            if (confirm("确定要删除吗？")) {
                $.post("../../WeiXinMassMsg/DelMassMsg", { masterId: id }, function (result) {
                    if (result.IsSuccessful) {
                        alert("删除成功");
                        window.location.href = "../WeiXinMassMsg/SourceManage";
                    } else {
                        alert(resut.OperateMsg);
                    }
                });
            }
            else {
            }
        }
        function sendMsg(obj, id) {
            
            if (confirm("确定要群发吗？")) {
                var target = $("#TargetList").val(), groupid = $("#GroupList").val();
                $.post("../../WeiXinMassMsg/SendMassMsg", { masterId: id ,target:target,groupid:groupid}, function (result) {
                    if (result.IsSuccessful) {
                        alert("群发成功");
                    } else {
                        alert(resut.OperateMsg);
                    }
                });
            }
            else {
            }

            /*
            $.post("../../WeiXinMassMsg/SendMsg", { id: id }, function (result) {
                if (result.IsSuccessful) {
                    $(obj).closest(".multi").remove();
                    material_obj.material_init();
                } else {
                    alert(resut.OperateMsg);
                }
            });
            */
        }

        //页面跳转
        function redirectTo()
        {
            var target = $("#TargetList").val(), groupid = $("#GroupList").val();
            window.location.href = "../WeiXinMassMsg/CreateOrEdit?target=" + target + "&groupid="+groupid;
        }

    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
</asp:Content>
