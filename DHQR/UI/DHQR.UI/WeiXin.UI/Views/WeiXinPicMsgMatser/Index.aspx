<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    微信图文消息
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
                    <li class="cur"><a href="./?m=material&a=index">图文素材管理</a></li>
                    <li class=""><a href="./?m=material&a=url">自定义URL</a></li>
                    <li class=""><a href="./?m=material&a=url_list">系统URL查询</a></li>
                </ul>
            </div>
            <div id="material" class="r_con_wrap">

                <div class="list">
                    <div class="item first_item">
                        <div>
                            <div><a href="./?m=material&a=index&d=edit&type=0"></a></div>
                            <a href="../WeiXinPicMsgMatser/CreateOrEdit?type=0">+单图文素材</a>
                        </div>
                        <div class="multi">
                            <div><a href="../WeiXinPicMsgMatser/CreateOrEdit"></a></div>
                            <a href="../WeiXinPicMsgMatser/CreateOrEdit?type=1">+多图文素材</a>
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
            $.post("../WeiXinPicMsgMatser/Query", {}, function (result) {
                var container = $("#material .list");
                $.each(result, function (key, val) {
                    var div = $('<div class="item multi" ></div>');
                    var time = $('<div class="time">' +val.CreateTimeStr + '</div>');
                    var first = $('<div class="first"></div>');
                    var info = $('<div class="info"><div class="img"><img src="' + val.PicUrl + '" /></div><div class="title">' + val.Title + '</div></div>');
                    if (val.MaterialType == 0) {
                        div = $('<div class="item one" ></div>')
                        var title =$('<div class="title">'+val.Title+'</div>"');
                        info = $('<div class="img"><img src="' + val.PicUrl + '" /></div><div class="txt">' + val.Description + '</div>');
                        title.appendTo(div);
                        time.appendTo(div);
                        info.appendTo(div);
                        container.append(div);
                    } else {
                        time.appendTo(div);
                        info.appendTo(first);
                        first.appendTo(div);
                        container.append(div);
                        $.each(val.MsgDetails, function (k, v) {
                            var list = $('<div class="list"></div>');
                            info = $('<div class="info"><div class="img"><img src="' + v.PicUrl + '" /></div><div class="title">' + v.Title + '</div></div>');
                            info.appendTo(list);
                            list.appendTo(div);
                        });
                    }
                    var mod_del = $('<div class="mod_del"></div>');
                    var mod = $('<div class="mod"><a href="../WeiXinPicMsgMatser/CreateOrEdit?id=' + val.Id + '&type=' + val.MaterialType + '" style="cursor:pointer;"><img src="../../DHQRImages/ico/mod.gif" /></a></div>');
                    var del = $('<div class="del"><a onclick="deleteItem(this,\'' + val.Id + '\')"  style="cursor:pointer;"><img src="../../DHQRImages/ico/del.gif" /></a></div>')
                    mod.appendTo(mod_del);
                    del.appendTo(mod_del);
                    mod_del.appendTo(div);

                });
                frame_obj.search_form_init();

                material_obj.material_init();
            });
        });
        function deleteItem(obj,id)
        {
            $.post("../../WeiXinPicMsgMatser/DeleteData", { id: id }, function (result) {
                if (result.IsSuccessful) {
                    $(obj).closest(".multi").remove();
                    material_obj.material_init();
                } else {
                    alert(resut.OperateMsg);
                }
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
</asp:Content>
