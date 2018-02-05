<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    服务调用日志
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="uiCenter" class="ui-layout-center">
        <div class="ui-widget-header" style="height: 28px;">
            <label style="float: left; margin-left: 10px; margin-top: 3px; font-size: 14px;">
                服务调用日志</label>
            <button id="search" style="float: right; height: 23px; margin-top: 2px;">
                查询</button>
            <input id="searchVal" type="text" style="float: right; margin-top: 3px;" />
        </div>
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
    <script src="../../Scripts/js/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
    <script type="text/javascript">
        var page, rows, sidx, sortname, url;

        function resizeGrid() {
            $("#listTable").jqGrid("setGridWidth", $(window).width() - 10);
            $("#listTable").jqGrid("setGridHeight", $(window).height() - 80);
        }
        $(function () {
            $("body").layout();
            var param = { Page: page, Rows: 15, Sidx: "RequestTime", Sord: "desc"};
            $("#listTable").jqGrid({
                url: 'GetPageData',
                colModel: [
   		                   { name: 'Id', index: 'Id', label: 'Id', width: 55, hidden: true },
   		                   { name: 'ServiceName', index: 'ServiceName', label: '服务名称', width: 50 },
                           { name: 'MethodName', index: 'MethodName', label: '方法名称', width: 50 },
                           { name: 'RequestParam', index: 'RequestParam', label: '调用参数', width: 100 },
                           { name: 'IsSucessful', index: 'IsSucessful', align: "center", width: 30, label: '是否成功', formatter: "checkbox", formatoptions: { value: "true:false" } },
                           { name: 'RequestTime', index: 'RequestTime', formatter: 'date', formatoptions: { newformat: 'Y-m-d H:i:s' }, label: '调用时间', width: 50 },
                          { name: 'UserName', index: 'UserName', label: '调用方', width: 50 }
                ],
                pager: '#listPager',
                sortname: 'RequestTime',
                param:param,
                viewrecords: true,
                datatype: 'json',
                sortorder: "desc",
                mtype: 'POST',
                // caption: "登录日志记录",
                loadonce: false,
                rowNum:15,
                width: 200,
                onCellSelect: function (rowId) {
                    if (rowId != "" && rowId != null) {
                        var SelRowdata = $("#listTable").getRowData(rowId);
                        showObjectOnForm('frmPost', SelRowdata);
                    }
                }
            });
            var myLayout = $("#container").layout({
            });

            $("#search").button({ icons: { primary: "ui-icon-search" } }).click(function () {
                var value = $("#searchVal").val();
                resetGridData();
            });
            resizeGrid();
        });

        function resetGridData() {
            var grid = $("#listTable");
            page = $('#listTable').jqGrid('getGridParam', 'page');
            url = $('#listTable').jqGrid('getGridParam', 'url');
            sortname = $('#listTable').jqGrid('getGridParam', 'sortname');
            sidx = $('#listTable').jqGrid('getGridParam', 'sortorder');
            rows = $('#listTable').jqGrid('getGridParam', 'rowNum');
            var param = {  Rows: rows, Sidx: sortname, Sord: sidx,StartIndex:page };
            //$.post("GetPageData", param, function (result) {
            //    grid.clearGridData(false);
            //    grid.jqGrid("setGridParam", {
            //        data: result,
            //        datatype: "json",
            //        mtype: 'POST',
            //        pager: '#listPager'
            //    }).trigger("reloadGrid");
            //});

            $.ajax({
                url: "GetPageData",
                type: "POST",
                data: param,
                dataType:"json",
                success: function (data) {
                        grid.clearGridData(false);
                        grid.jqGrid("setGridParam", {
                            data: data,
                            datatype: "json"
                        }).trigger("reloadGrid");
                },
                error: function (data) {
                }
            });

        }

        var timeout = null;
        $(window).bind("resize", function () {
            if (timeout) {
                clearTimeout(timeout);
            }
            timeout = setTimeout(resizeGrid, 300);
        });
    </script>
</asp:Content>
