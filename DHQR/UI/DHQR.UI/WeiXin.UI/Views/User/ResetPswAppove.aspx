<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    重置密码申请
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="uiCenter" class="ui-layout-center" style="border-top: 3px sloid #b8b9b8">
        <div class="ui-widget-header" style="height: 28px;">
            <label style="float: left; margin-left: 10px; margin-top: 3px; font-size: 14px;">
                重置密码申请</label>
            <button id="btnApplyYes" style="height: 23px; margin-left: 20px; margin-top: 2px;">
                重置密码</button>
            <button id="btnApplyNo" style="height: 23px; margin-top: 2px;">
                拒绝处理</button>
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
            InitialControls();
            $("#listTable").jqGrid({
                //                url: 'GetAllUserApply',
                datatype: "local",
                colModel: [
   		                    { name: 'Id', index: 'Id', label: 'Id', width: 55, hidden: true },
                            { name: 'State', index: 'State', label: 'State', width: 55, hidden: true },
   		                    { name: 'UserName', index: 'UserName', label: '登录名', width: 50 },
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

        var timeout = null;
        $(window).bind("resize", function () {
            if (timeout) {
                clearTimeout(timeout);
            }
            timeout = setTimeout(resizeGrid, 300);
        });

        function InitialControls() {
            //审批通过
            $("#btnApplyYes").button({ icons: { primary: "ui-icon-check"} }).click(function () {
                var grid = $("#listTable");
                var selRowId = grid.jqGrid('getGridParam', 'selrow');
                if (selRowId == undefined || selRowId == null) {
                    MsgAlert("警告", "请先选择需要操作的行项目！");
                    return;
                }
                var selRowData = grid.getRowData(selRowId);
                if (selRowData.State != 0) {
                    MsgAlert("警告", "该申请已经处理，请不要重复处理！");
                    return;
                }
                var msg = "是否重置【" + selRowData.UserName + "】的密码？"
                confirmDialog(msg, function (r) {
                    $.post('ApplyPswReq', { Id: selRowData.Id, State: 1 }, function (data) {
                        if (DHQR.ProcessStatus(data)) {
                            resetGridData();
                        }
                    });
                }, "重置密码确认", true, function () { }
                    );

            });

            //审批不通过
            $("#btnApplyNo").button({ icons: { primary: "ui-icon-closethick"} }).click(function () {
                var grid = $("#listTable");
                var selRowId = grid.jqGrid('getGridParam', 'selrow');
                if (selRowId == undefined || selRowId == null) {
                    MsgAlert("警告", "请先选择需要操作的行项目！");
                    return;
                }
                var selRowData = grid.getRowData(selRowId);
                if (selRowData.State != 0) {
                    MsgAlert("警告", "该申请已经处理，请不要重复处理！");
                    return;
                }
                var msg = "是否拒绝处理【" + selRowData.UserName + "】的重置密码申请？"
                confirmDialog(msg, function (r) {
                    $.post('ApplyPswReq', { Id: selRowData.Id, State: 2 }, function (data) {
                        if (DHQR.ProcessStatus(data)) {
                            resetGridData();
                        }
                    });
                }, "拒绝处理", true, function () { }
                    );


            });
        }
        //加载项目类型数据
        function resetGridData() {
            var grid = $("#listTable");
            $.post("GetAllPswApply", {}, function (result) {
                grid.clearGridData(false);
                grid.jqGrid("setGridParam", {
                    data: result,
                    datatype: 'local'
                }).trigger("reloadGrid");
            });
        }

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
