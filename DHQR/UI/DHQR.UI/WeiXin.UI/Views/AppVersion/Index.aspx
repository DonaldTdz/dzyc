<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    APP版本管理
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
                </ul>
            </div>
            <div id="reply_keyword" class="r_con_wrap">
                <div class="control_btn">
                    <a href="../AppVersion/CreateOrEdit" class="btn_green btn_w_120">新增版本信息</a>
                </div>
                <table border="0" cellpadding="5" cellspacing="0" class="r_con_table">
                    <thead>
                        <tr>
                            <td style="width:100px;">包名
                            </td>
                            <td style="width:100px;">APK名称
                            </td>
                            <td style="width:100px;">版本号
                            </td>
                            <td style="width:200px;">版本描述
                            </td>
                            <td style="width:200px;">APK地址
                            </td>
                            <td style="width:100px;">是否有效
                            </td>
                            <td style="width:100px;" class="last">操作</td>
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
    <link href="../../DHQRCss/global.css" rel="stylesheet" type="text/css" />
    <link href="../../DHQRCss/main.css" rel="stylesheet" type="text/css" />
    <link href="../../DHQRCss/wechat.css" rel="stylesheet" type="text/css" />
    <script src="../../DHQRJs/global.js" type="text/javascript"></script>
    <script src="../../DHQRJs/wechat.js" type="text/javascript"></script>
    <style type="text/css">
        body, html {
            background: url(../../DHQRImages/main/main-bg.jpg) left top fixed no-repeat;
             
        }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
    <script type="text/javascript">
        $(function () {
            getPageData(1);
        });

        function getPageData(pageIndex) {
            $.post("GetPageData", { Page: pageIndex, Rows: 10, Sidx: "VersionCode",Sord:"desc" }, function (response) {
                var dataRows = response.rows;
                var tbody = $(".r_con_table").find("tbody");
                tbody.empty();
                $(dataRows).each(function (i) {
                    var tr = $("<tr></tr>");
                    tr.append('<td style="display:none">' + dataRows[i].Id + '</td>');

                    var td = $('<td></td>');
                    td.append(dataRows[i].ApkPacket);
                    tr.append(td);

                    td = $('<td></td>');
                    td.append(dataRows[i].ApkName);
                    tr.append(td);

                    td = $('<td></td>');
                    td.append(dataRows[i].VersionCode);
                    tr.append(td);

                    td = $('<td></td>');
                    td.append(dataRows[i].VersionName);
                    tr.append(td);

                    td = $('<td></td>');
                    td.append(dataRows[i].Url);
                    tr.append(td);

                    td = $('<td></td>');
                    td.append(dataRows[i].IsValid ? "是" : "否");
                    tr.append(td);

                    
                    //操作
                    td = $('<td class="last"></td>');
                    var modifyATag = $('<a href="../AppVersion/CreateOrEdit?Id=' + dataRows[i].Id + '"><img src="../../DHQRImages/ico/mod.gif" align="absmiddle" alt="修改"></a>');
                    td.append(modifyATag);
                    td.append('<sapn style="display:inline-block; width:5px;"></span>');
                    var delATag = $('<a href="javascript:void(0)" onclick="delItem(this)" style="cursor:pointer;"><img src="../../DHQRImages/ico/del.gif" align="absmiddle" alt="删除"></a>');
                    td.append(delATag);

                    //设置分页数据
                    setPageDiv("turn_page", pageIndex, response.total, "getPageData");
                    //var pageDiv = $("#turn_page");
                    //var pageHtml="";
                    //var prevPage, nextPage;
                    //if (pageIndex == 1||response.total==0) {
                    //    prevPage = '<font class="page_noclick">&lt;&lt;上一页</font>';
                    //} else {
                    //    prevPage = '<a style="cursor:pointer;" onclick="getPageData('+(pageIndex-1)+')"><font>&lt;&lt;上一页</font></a>';
                    //}
                    //if (pageIndex == response.total) {
                    //    nextPage = '<font class="page_noclick">下一页&gt;&gt;</font>';
                    //} else {
                    //    nextPage = '<a style="cursor:pointer;" onclick="getPageData(' + (pageIndex + 1) + ')"><font class="page_button">下一页&gt;&gt;</font></a>';
                    //}
                    //pageHtml = prevPage;
                    //for (var i = 1; i <= response.total; i++)
                    //{                           
                    //    if (i == pageIndex) {
                    //        pageHtml += '&nbsp;<font class="page_item_current">' + i + '</font>';
                    //    } else {
                    //        pageHtml += '&nbsp;<a style="cursor:pointer;" onclick="getPageData('+i+')"><font>' + i + '</font></a>';
                    //    }
                    //}
                    //if (response.total == 0) {
                    //    pageHtml += '&nbsp;<font class="page_item_current">' + 0 + '</font>';
                    //}
                    //pageHtml += '&nbsp' + nextPage;
                    //pageDiv.html(pageHtml);
                    tr.append(td);

                    tbody.append(tr);
                });
            });
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

        function delItem(obj) {
            if (!confirm("删除后不可恢复，继续吗？")) { return false };
            var Id = $(obj).parents("tr")[0].children[0].innerText;
            $.post("Del", { Id: Id }, function (data) {
                if (data.IsSuccessful == true) {
                    $(obj).parents("tr").remove();
                } else {
                    alert(data.OperateMsg);
                }
            }, 'json');
        }


        //        $(function () {
        //            //初始化button
        //            $("#AddBtn").button({
        //                icons: { primary: "ui-icon-plus" }
        //            }).click(function () {
        //                clearDialog();
        //                changMsgType(0);
        //                $("#replyMsgType").find("option[value=0]").attr("selected", true);
        //                $("#add_edit_dialog").dialog({
        //                    buttons: {
        //                        "提交保存": function () {
        //                            submitData("Create");
        //                            $(this).dialog("close");
        //                        },
        //                        "返回": function () {
        //                            $(this).dialog("close");
        //                        }
        //                    }
        //                });
        //                $("#add_edit_dialog").dialog("option", "title", "新增关键词").dialog("open");
        //            });

        //            $("#add_edit_dialog").dialog({
        //                autoOpen: false,
        //                resizable: true,
        //                modal: true,
        //                width: $(window).width() * 0.6,
        //                height: $(window).height() * 0.8,
        //                title: '新增关键词',
        //                position: ['center', 'center'],
        //                buttons: {
        //                    "提交保存": function () {
        //                        $(this).dialog("close");
        //                    },
        //                    "返回": function () {
        //                        $(this).dialog("close");
        //                    }
        //                }
        //            });
        //        });

        //        var timeout = null;
        //        $(window).bind("resize", function () {
        //            if (timeout) {
        //                clearTimeout(timeout);
        //            }
        //            timeout = setTimeout(resizeGrid, 300);
        //        });

        //        function resizeGrid() {
        //            //  alert($(window).height());
        //            $("#listTable").jqGrid("setGridWidth", $(window).width() - 80);
        //            $("#listTable").jqGrid("setGridHeight", $(window).height() - 250);
        //        }

        //               //grid加载
        //        function addActions() {
        //            var grid = $("#listTable");
        //            var ids = grid.getDataIDs();
        //            for (var i = 0; i < ids.length; i++) {
        //                var rowId = ids[i], rowData = grid.getRowData(rowId);
        //                var editBtn = Format("<span  onclick='edit({0})' title='编辑' class='ui-icon ui-icon-pencil gridToolWidget' style='float:left;margin-left:1px;cursor:pointer' ></span>", rowId);
        //                var deleteBtn = Format("<span  onclick='deleteKeyWord({0})' title='删除' class='ui-icon ui-icon-trash gridToolWidget' style='margin-left:40px;cursor:pointer' ></span>", rowId);
        //                grid.jqGrid("setRowData", rowId, { Actions: editBtn + " " + deleteBtn });
        //            }
        //        }


        //        function edit(rowId) {
        //            var grid = $("#listTable"), rowData = grid.getRowData(rowId);
        //            $("#keyWordId").attr("value", rowData.Id);
        //            $("#add_edit_dialog").dialog("option", "title", "编辑关键词").dialog("open");
        //            $("#keyword").attr("value", rowData.KeyWord);
        //            changMsgType(rowData.Type);
        //            $("#replyMsgType").find("option[value=" + rowData.Type + "]").attr("selected", true);
        //            $("input[name =patternMethod][value=" + rowData.PatternMethod + "]").attr("checked", 'checked');
        //            $("#textContent").attr("value", rowData.Value);
        //            $("#DHQRSourceSelect").find("option[value=" + rowData.DHQRSourceId + "]").attr("selected", true);
        //            $("#add_edit_dialog").dialog({
        //                buttons: {
        //                    "提交保存": function () {
        //                        submitData("Edit");
        //                        $(this).dialog("close");
        //                    },
        //                    "返回": function () {
        //                        $(this).dialog("close");
        //                    }
        //                } 
        //            });

        //        }

        //        function deleteKeyWord(rowId) {
        //            var grid = $("#listTable"), rowData = grid.getRowData(rowId);
        //            $.post("Delete", { Id: rowData.Id }, function (data) {
        //                if (data.IsSuccessful == true) {
        //                    reloadGrid();
        //                }
        //            });
        //        }

        //        function gotoPage(value) {
        //            //  location.href = "../../KeyWord/Index";
        //        }

        //        function clearDialog() {
        //            var textareas = $("#add_edit_dialog").find("textarea");
        //            textareas.each(function (index) {
        //                $(this).attr("value", "");
        //            });

        //            $("#keyWordId").attr("value", "");
        //        }

        //        function changMsgType(type) {
        //            if (type == 0) {
        //                $("#text_msg_row").show();
        //                $("#img_msg_row").hide();
        //            } else {
        //                $("#img_msg_row").show();
        //                $("#text_msg_row").hide();
        //            }

        //            $("#textContent").attr("value", "");
        //            $("#DHQRSourceSelect").find("option[value='']").attr("selected", true);
        //        }

        //        function submitData(actionUrl) {
        //            var Id = $("#keyWordId").attr("value"), keyword = $("#keyword").attr("value"), textContent = $("#textContent").attr("value"),
        //                patternMethod = $("input:radio[name =patternMethod]:checked").val(), replyMsgType = $("#replyMsgType").find("option:selected").val(),
        //                DHQRSourceId = $("#DHQRSourceSelect").find("option:selected").val();
        //            var params = { Id: Id, KeyWord: keyword, patternMethod: patternMethod, Value: textContent, Type: replyMsgType, DHQRSourceId: DHQRSourceId };
        //            $.post(actionUrl, params, function (data) {
        //                if (data.IsSuccessful == true) {
        //                    reloadGrid();
        //                }
        //            });
        //        }

        //        function reloadGrid() {
        //            $("#listTable").jqGrid("setGridParam", {
        //                url: "GetPageData",
        //                mtype: 'POST',
        //               // postData: data,
        //                datatype: 'json',
        //                loadonce: false,
        //                loadComplete: function () {
        //                    addActions();
        //                }
        //            }).trigger("reloadGrid");
        //        }
    </script>
</asp:Content>
