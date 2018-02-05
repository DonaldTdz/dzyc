<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
                    <li class=""><a href="../WeiXinPicMsgMatser/Index">图文消息管理</a></li>
                    <li class="cur"><a href="../WeiXinCustomUrl/Index">自定义URL</a></li>
                    <li class=""><a href="../WeiXinSysUrl/Index">系统URL查询</a></li>
                </ul>
            </div>
            <div id="url" class="r_con_wrap">
                <form id="add_form" class="add_form " method="post">
                <table border="0" cellpadding="5" cellspacing="0">
                    <tr>
                        <td>
                            名称
                            <input type="text" name="Name" id="Name" value="" size="25" class="form_input" notnull />
                        </td>
                        <td>
                            Url
                            <input type="text" name="Url" id="Url" value="" size="40" class="form_input" notnull />
                        </td>
                        <td>
                            排序号
                            <input type="text" name="Number" id="Number" value="" size="40" class="form_input" notnull />
                        </td>
                        <td>
                            <input type="button" class="submit" id="sub" value="添加URL" name="submit_btn" onclick="CreateOrUpdate();" />
                        </td>
                    </tr>
                </table>
                <input type="hidden" name="do_action" value="material.url" />
                <input type="hidden" name="UId" value="0" />
                <input type="hidden" name="page" value="0" />
                <input type="hidden" id="Id"/>
                <input type="hidden" id="WeiXinAppId"/>

                </form>
                <table border="0" cellpadding="5" cellspacing="0" class="r_con_table">
                    <thead>
                        <tr>
                            <td width="10%" nowrap="nowrap">
                                序号
                            </td>
                            <td width="30%" nowrap="nowrap">
                                名称
                            </td>
                            <td width="50%" nowrap="nowrap">
                                Url
                            </td>
                            <td width="50%" nowrap="nowrap">
                                排序号
                            </td>
                            <td width="10%" nowrap="nowrap" class="last">
                                操作
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        
                    </tbody>
                </table>
                <div class="blank20">
                </div>
                <div id="turn_page">
                    <font class="page_noclick">&lt;&lt;上一页</font>&nbsp;<font class="page_item_current">0</font>&nbsp;<font
                        class="page_noclick">下一页&gt;&gt;</font>
                </div>
            </div>

        </div>
        </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
    <script src="../../DHQRJs/material.js" type="text/javascript"></script>
    <link href="../../DHQRCss/material.css" rel="stylesheet" />
    <link href="../../DHQRCss/wechat.css" rel="stylesheet" />
    <script src="../../DHQRJs/wechat.js" type="text/javascript"></script>
    <style type="text/css">
        body, html
        {
            background: url(../../DHQRImages/main/main-bg.jpg) left top fixed no-repeat;
        }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">

    <script type="text/javascript">
        $(document).ready(material_obj.url_init);
        var isCreate = true;
        $(function () {
            getPageData(1);
        });

        function getPageData(pageIndex) {
            $.post("GetPageData", { Page: pageIndex, Sidx: "Number" }, function (response) {
                var dataRows = response.rows;
                var tbody = $(".r_con_table").find("tbody");
                tbody.empty();
                $(dataRows).each(function (i) {
                    var tr = $("<tr></tr>");
                    tr.append('<td style="display:none">' + dataRows[i].Id + '</td>');

                    td = $('<td nowrap="nowrap"></td>');
                    td.append(dataRows[i].Number);
                    tr.append(td);

                    td = $('<td nowrap="nowrap"></td>');
                    td.append(dataRows[i].Name);
                    tr.append(td);

                    td = $('<td nowrap="nowrap"></td>');
                    td.append(dataRows[i].Url);
                    tr.append(td);

                    td = $('<td nowrap="nowrap"></td>');
                    td.append(dataRows[i].Number);
                    tr.append(td);
                    //操作
                    td = $('<td nowrap="nowrap" class="last"></td>');
                    // var kl = "SetToEdit();"
                    var efuc = "SetToEdit(" + "'" + dataRows[i].Id + "'" + "," + "'" + dataRows[i].Number + "'" + "," + "'" + dataRows[i].Name + "'" + "," + "'" + dataRows[i].Url + "'" + ");"
                    var modifyATag = $('<a onclick=' + efuc + '><img src="../../DHQRImages/ico/mod.gif" align="absmiddle" alt="修改"></a>');
                    td.append(modifyATag);
                    td.append('<sapn style="display:inline-block; width:5px;"></span>');
                    var delATag = $('<a href="javascript:void(0)" onclick="delKeyword(this)" style="cursor:pointer;"><img src="../../DHQRImages/ico/del.gif" align="absmiddle" alt="删除"></a>');
                    td.append(delATag);

                    //设置分页数据
                    setPageDiv("turn_page", pageIndex, response.total, "getPageData");
                    tr.append(td);

                    tbody.append(tr);
                });
            });
        }

        function delKeyword(obj) {
            if (!confirm("删除后不可恢复，继续吗？")) { return false };
            var Id = $(obj).parents("tr")[0].children[0].innerText;
            $.post("Delete", { id: Id }, function (data) {
                if (data.IsSuccessful == true) {
                    $(obj).parents("tr").remove();
                } else {
                    alert(data.OperateMsg);
                }
            }, 'json');
        }

        function CreateOrUpdate() {
            if (isCreate) {
                Create();
            }
            else {
                Update();
            }
        }

        //创建
        function Create() {
            if (global_obj.check_form($('*[notnull]'))) { return false; };
            var url = $("#Url").val();
            var name = $("#Name").val();
            var number = $("#Number").val();

            var postData = { Url: url, Name: name, Number: number };
            $.ajax({
                url: "../WeiXinCustomUrl/Create",
                data: postData,
                type: "POST",
                catche: false,
                success: function (data) {
                    if (data.IsSuccessful) {
                        window.location.reload();
                    } else {
                        alert(data.OperateMsg);
                        $('#config_form input:submit').attr('disabled', false);
                    }
                }
            });
        }

        function Update() {
            if (global_obj.check_form($('*[notnull]'))) { return false; };
            var id = $("#Id").val();
            var number = $("#Number").val();
            var url = $("#Url").val();
            var name = $("#Name").val();
            var number = $("#Number").val();

            var postData = { Id: id, Number: number, Url: url, Name: name , Number: number};
            $.ajax({
                url: "../WeiXinCustomUrl/Edit",
                data: postData,
                type: "POST",
                catche: false,
                success: function (data) {
                    if (data.IsSuccessful) {
                        window.location.reload();
                    } else {
                        alert(data.OperateMsg);
                        $('#config_form input:submit').attr('disabled', false);
                    }
                }
            });

        }

        //设置编辑
        function SetToEdit(Id, Number, Name, Url) {
            isCreate = false;
            $("#Id").val(Id);
            $("#Number").val(Number);
            $("#Name").val(Name);
            $("#Url").val(Url);
            $("#sub").text("更新URL");
            $("#sub").val("更新URL");
        }


        //设置翻页
        function setPageDiv(pageDivId, pageIndex, totalPage, refashFn) {
            var pageDiv = $("#" + pageDivId);
            var pageHtml = "";
            var prevPage, nextPage;
            if (pageIndex == 1 || totalPage == 0) {
                prevPage = '<font class="page_noclick">&lt;&lt;上一页</font>';
            } else {
                prevPage = '<a style="cursor:pointer;" onclick="getPageData(' + (pageIndex - 1) + ')"><font>&lt;&lt;上一页</font></a>';
            }
            if (pageIndex == totalPage) {
                nextPage = '<font class="page_noclick">下一页&gt;&gt;</font>';
            } else {
                nextPage = '<a style="cursor:pointer;" onclick="' + refashFn + '(' + (pageIndex + 1) + ')"><font class="page_button">下一页&gt;&gt;</font></a>';
            }
            pageHtml = prevPage;
            for (var i = 1; i <= totalPage; i++) {
                if (i == pageIndex) {
                    pageHtml += '&nbsp;<font class="page_item_current">' + i + '</font>';
                } else {
                    pageHtml += '&nbsp;<a style="cursor:pointer;" onclick="' + refashFn + '(' + i + ')"><font>' + i + '</font></a>';
                }
            }
            if (totalPage == 0) {
                pageHtml += '&nbsp;<font class="page_item_current">' + 0 + '</font>';
            }
            pageHtml += '&nbsp' + nextPage;
            pageDiv.html(pageHtml);
        }
    </script>
</asp:Content>
