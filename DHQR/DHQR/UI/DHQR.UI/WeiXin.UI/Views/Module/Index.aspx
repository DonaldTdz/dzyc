<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    功能项管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="uiCenter" class="ui-layout-center" style="border-top: 3px sloid #b8b9b8">
        <div class="ui-widget-header" style="height: 28px;">
            <label style="float: left; margin-left: 10px; margin-top: 3px; font-size: 14px;">
                功能项管理</label>
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
    <div id="uieast" class="ui-layout-west">
        <div class="ui-widget-header" style="height: 28px;">
            <button id="AddBtn" style="float: left; height: 23px; margin-top: 2px;" onclick="AddFun();">
                清空</button>
            <button id="EditBtn" style="float: left; height: 23px; margin-top: 2px;" onclick="SaveFun();">
                保存</button>
            <button id="DelBtn" style="float: left; height: 23px; margin-top: 2px;" onclick="DelFun();">
                删除</button>
        </div>
        <div style="height: 10px;">
        </div>
        <form id="frmPost" class="frmPost" method="post" action="">
        <input id="Id" name="Id" type="hidden" />
        <div class="control-group">
            <label class="control-label">
                代码</label>
            <div class="controls">
                <input id="Code" name="Code" class="required  ui-input-text ui-input-text-form" />
            </div>
        </div>
        <div class="control-group">
            <label class="control-label">
                名称</label>
            <div class="controls">
                <input id="Name" name="Name" class="required  ui-input-text ui-input-text-form" />
            </div>
        </div>
        <div class="control-group">
            <label class="control-label">
                控制器名称</label>
            <div class="controls">
               <%-- <input id="ControllerName" name="ControllerName" class="required  ui-input-text ui-input-text-form" />--%>
                <textarea rows="3" type="text"  class="required  ui-input-text ui-input-text-form" id="ControllerName" name="ControllerName"
                     ></textarea>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label">
                Action名称</label>
            <div class="controls">
              <%--  <input id="ActionName" name="ActionName" class="required  ui-input-text ui-input-text-form" />--%>
                <textarea rows="3" type="text"  class="required  ui-input-text ui-input-text-form" id="ActionName" name="ActionName"
                     ></textarea>
            </div>
        </div>
        <div class="control-group">
            <label class="control-label">
                功能项地址</label>
            <div class="controls">
                <input id="Url" name="Url" class="required  ui-input-text ui-input-text-form" />
            </div>
        </div>
        <div class="control-group">
            <label class="control-label">
                是否菜单项</label>
            <div class="controls">
                <input id="IsMenu" name="IsMenu" value="true" type="checkbox" />
            </div>
        </div>
        </form>
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
            $("#listTable").jqGrid("setGridWidth", $(window).width() - 270);
            $("#listTable").jqGrid("setGridHeight", $(window).height() - 80);
        }
        $(function () {
            $("body").layout();
            var myLayout = $("#container").layout({
                "west": { size: "260", closable: false, resizable: false, slidable: false }
            });
            $("#listTable").jqGrid({
                url: 'GetAll',
                datatype: "local",
                colModel: [
   		                    { name: 'Id', index: 'Id', label: 'Id', width: 55, hidden: true },
   		                    { name: 'Code', index: 'Code', label: '代码', width: 50 },
   		                    { name: 'Name', index: 'Name', label: '名称', width: 50 },
   		                    { name: 'ControllerName', index: 'ControllerName', label: '控制器类名', width: 50 },
   		                    { name: 'ActionName', index: 'ActionName', label: 'ACTION名称', width: 50 },
                            { name: 'Url', index: 'Url', label: '功能项地址', width: 80 },
                            { name: 'IsMenu', index: 'IsMenu', label: '是否菜单项', formatter: 'checkbox', editoptions: { value: "true:false" }, width: 80, align: "center" }
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
                    if (rowId != "" && rowId != null) {
                        var SelRowdata = $("#listTable").getRowData(rowId);
                        showObjectOnForm('frmPost', SelRowdata);
                    }
                }
            });
            resetGridData();

            $("#AddBtn").button({ icons: { primary: "ui-icon-plus"} });
            $("#EditBtn").button({ icons: { primary: "ui-icon-disk"} });
            $("#DelBtn").button({ icons: { primary: "ui-icon-trash"} });
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
            $.post("GetAll", {}, function (result) {
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
        function AddFun() {
            CleanFormData("frmPost");
        }

        function DelFun() {
            var data = $("#frmPost").serialize();
            if ($('#Id').val() == null || $('#Id').val() == "")
            { $.DialogBox.tip('请选择需要删除的项!', 'error'); return; }

            confirmDialog("是否删除所选项", function (r) {
                $.post('Del', data, function (data) {
                    if (data.IsSuccessful) {
                        resetGridData();
                        $.DialogBox.tip(data.OperateMsg);
                        CleanFormData("frmPost");
                    }
                });
            }, "删除确认", true, function () { }
                    );


        }


        function SaveFun() {
            var data = $("#frmPost").serialize();
            if (!$("#frmPost").valid()) {
                return;
            }
            var url = 'Add';

            if ($('#Id').val() != null && $('#Id').val() != "")
                url = 'Edit';
            $.post(url, data, function (data) {
                if (data.IsSuccessful) {
                    resetGridData();
                    $.DialogBox.tip(data.OperateMsg);
                    CleanFormData("frmPost");
                }
            });
        }


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
