﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    用户申请
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="uiCenter" class="ui-layout-center" style="border-top: 3px sloid #b8b9b8">
        <div class="ui-widget-header" style="height: 28px;">
            <label style="float: left; margin-left: 10px; margin-top: 3px; font-size: 14px;">
                用户申请</label>
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
    <script src="../../Scripts/Shop/shop.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
    <script type="text/javascript">
        function resizeGrid() {
            $("#listTable").jqGrid("setGridWidth", $(window).width() - 5);
            $("#listTable").jqGrid("setGridHeight", $(window).height() - 80);
        }
        $(function () {
            $("body").layout();
            var myLayout = $("#container").layout({
                "west": { size: "260", closable: false, resizable: false, slidable: false }
            });
            $("#listTable").jqGrid({
//                url: 'GetAllUserApply',
                datatype: "local",
                colModel: [
   		                    { name: 'Id', index: 'Id', label: 'Id', width: 55, hidden: true },
                            { name: 'State', index: 'State', label: 'State', width: 55, hidden: true },
                            { name: 'ModuleRoleId', index: 'ModuleRoleId', label: 'ModuleRoleId', width: 55, hidden: true },
   		                    { name: 'Name', index: 'Name', label: '登录名', width: 50 },
   		                    { name: 'Nickname', index: 'Nickname', label: '昵称', width: 50 },
   		                    { name: 'ModuleRoleName', index: 'ModuleRoleName', label: '角色', width: 100 },
   		                    { name: 'EMail', index: 'EMail', label: '邮箱', width: 100 },
                            { name: 'Tel', index: 'Tel', label: '电话', width: 100 },
                            { name: 'Remark', index: 'Remark', label: '申请说明', width: 100 },
                            { name: 'HasDone', index: 'HasDone', hidden: true, label: '是否处理', formatter: 'checkbox', editoptions: { value: "true:false" }, width: 80, align: "center" },
                            { name: 'StateStr', index: 'StateStr', label: '处理状态', width: 100 }
                            ],
                pager: '#listPager',
                sortname: 'Code',
                viewrecords: true,
                loadonce: true,
                autowidth: false,
                shrinkToFit: true,
                autoScroll: false,
                loadComplete: function () {
                },
                onCellSelect: function (rowId) {
                }
            });
            resetGridData();

            $("#search").button({ icons: { primary: "ui-icon-search"} }).click(function () {
                var value = $("#searchVal").val();
                var grid = $("#listTable");
                var data = grid.data("beforeRefashData");
                if (!data) {
                    data = grid.jqGrid("getGridParam", "data");
                    grid.data("beforeRefashData", data);
                }
                var returnData = DHQR.QuickSearch(data, value, grid);
                if (value == "") {
                    returnData = data;
                }
                grid.clearGridData(false);
                grid.jqGrid("setGridParam", {
                    data: returnData,
                    datatype: "local"
                }).trigger("reloadGrid");
            });
            resizeGrid();
        });

        //加载项目类型数据
        function resetGridData() {
            var grid = $("#listTable");
            $.post("GetAllUserApply", {}, function (result) {
                grid.clearGridData(false);
                grid.jqGrid("setGridParam", {
                    data: result,
                    datatype: 'local'
                }).trigger("reloadGrid");
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
    <script type="text/javascript">

        //重新加载grid数据
        //gridid:gridId
        //url请求f地址
        //param请求参数
        function reloadGrid(gridid, url, param) {
            $("#" + gridid).jqGrid("setGridParam", {
                url: url,
                datatype: 'json',
                postData: param,
                mtype: 'POST',
                pager: '#pager'
            }).trigger("reloadGrid");
        }

    </script>
</asp:Content>
