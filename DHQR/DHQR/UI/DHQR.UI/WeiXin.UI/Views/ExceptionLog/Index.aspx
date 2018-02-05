<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    异常日志
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="uiCenter" class="ui-layout-center">
        <div id="Contenter" class="gridContainer">
            <table id="listTable">
            </table>
            <div id="listPager">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
    <link href="../../Content/JqGrid/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/JqGrid/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script src="../../Scripts/JqGrid/grid.locale-cn.js" type="text/javascript"></script>
    <link href="../../Content/Box/Content.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Box/jquery.Box.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
    <script type="text/javascript">
        function resizeGrid() {
            $("#listTable").jqGrid("setGridWidth", $(window).width() - 10);
            $("#listTable").jqGrid("setGridHeight", $(window).height() - 80);
        }
        $(function () {
            $("body").layout();
            $("#listTable").jqGrid({
                url: 'GetPageData',
                colModel: [
   		                   { name: 'Id', index: 'Id', label: 'Id', width: 55, hidden: true },
                              { name: 'CreateTime', index: 'CreateTime', formatter: 'date', formatoptions: { newformat: 'Y-m-d H:i:s' }, label: '执行时间', width: 100 },
                               { name: 'Ip', index: 'Ip', label: '操作人IP', width: 50 },
   		                    { name: 'UserName', index: 'UserName', label: '操作人', width: 50 },
                               { name: 'Message', index: 'Message', label: '异常消息', width: 180 },
                               { name: 'InnerException', index: 'InnerException', label: '内部异常', width: 180 },
                            ],
                pager: '#listPager',
                sortname: 'CreateTime',
                viewrecords: true,
                sortorder: "desc",
                caption: "异常日志",
                width: 200,
                onCellSelect: function (rowId) {
                    if (rowId != "" && rowId != null) {
                        var SelRowdata = $("#listTable").getRowData(rowId);
                        showObjectOnForm('frmPost', SelRowdata);
                    }
                }
            });
            var myLayout = $("#container").layout({
            //                "east": { size: "260", closable: false, resizable: false, slidable: false }
        });

        $("#AddBtn").button({ icons: { primary: "ui-icon-plus"} });
        $("#EditBtn").button({ icons: { primary: "ui-icon-disk"} });
        $("#DelBtn").button({ icons: { primary: "ui-icon-trash"} });
        resizeGrid();
    });
    var timeout = null;
    $(window).bind("resize", function () {
        if (timeout) {
            clearTimeout(timeout);
        }
        timeout = setTimeout(resizeGrid, 300);
    });
    </script>
</asp:Content>
