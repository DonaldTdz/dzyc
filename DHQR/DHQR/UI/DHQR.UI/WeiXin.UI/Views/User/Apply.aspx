<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout.Master" Inherits="System.Web.Mvc.ViewPage<UserApplyModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    用户申请审批
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="uiCenter" class="ui-layout-center" style="border-top: 3px sloid #b8b9b8">
        <div class="ui-widget-header" style="height: 28px;">
            <label style="float: left; margin-left: 10px; margin-top: 3px; font-size: 14px;">
                用户申请审批</label>
            <button id="btnEdit" style="height: 23px; margin-left: 20px; margin-top: 2px;">
                修改</button>
            <button id="btnApplyYes" style="height: 23px; margin-left: 20px; margin-top: 2px;">
                审批通过</button>
            <button id="btnApplyNo" style="height: 23px; margin-top: 2px;">
                审批不通过</button>
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
        <div id="detailDlg" class="div-dialog">
        <form id="frmPost" class="form-horizontal" method="post" action="">
        <input id="Id" name="Id" type="hidden" />
        <input id="ModuleRoleId" name="ModuleRoleId" type="hidden" />
        <input id="HasDone" name="HasDone" type="hidden" />
        <input id="State" name="State" type="hidden" />
        <div class="control-group">
            <label class="control-label">
                登录名：</label>
            <div class="controls">
                <input type="text" class="required  ui-input-text ui-input-text-form" id="Name" name="Name" disabled="disabled" readonly="readonly" /></div>
        </div>
        <div class="control-group">
            <label class="control-label">
                昵称：</label>
            <div class="controls">
                <input type="text" class="required  ui-input-text ui-input-text-form" id="Nickname" name="Nickname" disabled="disabled" readonly="readonly" /></div>
        </div>
        
        <div class="control-group">
            <label class="control-label">
                邮箱：</label>
            <div class="controls">
                <input type="text" class="required  ui-input-text ui-input-text-form readonly" id="EMail" name="EMail" disabled="disabled" readonly="readonly"/></div>
        </div>
        <div class="control-group">
            <label class="control-label">
                电话：</label>
            <div class="controls">
                <input type="text" class="required  ui-input-text ui-input-text-form" id="Tel" name="Tel" disabled="disabled" readonly="readonly"/></div>
        </div>
        <div class="control-group">
            <label class="control-label">
                备注：</label>
            <div class="controls">
                <input type="text" class="required  ui-input-text ui-input-text-form" id="Remark" name="Remark" disabled="disabled" readonly="readonly"/></div>
        </div>
        <div class="control-search" id="kk">
            <label class="control-label">
                角色：</label>
               <%:Html.DropDownListFor(f => f.ModuleRoleId, ViewData["ModuleRoles"] as IEnumerable<SelectListItem>, "", new { @readonly = "readonly", @style = "width:175px;margin-top:5px;height:30px;", onchange = "changeRole(this)" })%>
        </div>
        </form>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
    <link href="../../Content/JqGrid/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/JqGrid/jquery.jqGrid.src.js" type="text/javascript"></script>
    <link href="../../Content/Common/fromCommon.css" rel="stylesheet" type="text/css" />
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
            InitialDialogs();
            InitialControls();
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

        var timeout = null;
        $(window).bind("resize", function () {
            if (timeout) {
                clearTimeout(timeout);
            }
            timeout = setTimeout(resizeGrid, 300);
        });

        function InitialControls() {
            //修改
            $("#btnEdit").button({ icons: { primary: "ui-icon-pencil"} }).click(function () {
                var grid = $("#listTable");
                var selRowId = grid.jqGrid('getGridParam', 'selrow');
                if (selRowId == undefined || selRowId == null) {
                    MsgAlert("警告", "请先选择需要操作的行项目！");
                    return;
                }
                var selRowData = grid.getRowData(selRowId);
                if (selRowData.State != 0) {
                    MsgAlert("警告", "该申请已经处理，不能编辑！");
                    return;
                }
                showObjectOnForm("frmPost",selRowData);
                $("#detailDlg").dialog("open");
            });
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
                var msg = "是否审批通过账号【" + selRowData.Name + "】的申请？"
                confirmDialog(msg, function (r) {
                    $.post('ApplyUserReq', { Id: selRowData.Id, State: 1 }, function (data) {
                        if (DHQR.ProcessStatus(data)) {
                            resetGridData();
                        }
                    });
                }, "审批通过确认", true, function () { }
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
                var msg = "是否审批不通过账号【" + selRowData.Name + "】的申请？"
                confirmDialog(msg, function (r) {
                    $.post('ApplyUserReq', { Id: selRowData.Id, State: 2 }, function (data) {
                        if (DHQR.ProcessStatus(data)) {
                            resetGridData();
                        }
                    });
                }, "审批不通过确认", true, function () { }
                    );


            });
        }

        function changeRole(obj) {
            $("#ModuleRoleId").val($(obj).val());
        }

        function InitialDialogs() {
            $("#detailDlg").dialog({
                autoOpen: false,
                resizable: false,
                modal: true,
                width: 425,
                height: 360,
                title: '编辑',
                resizable: true,
                position: ['center', 'center'],
                buttons: {
                    '保存': function (event) {
                        SaveFun();
                    },
                    '取消': function () {
                        $(this).dialog("close");
                    }
                }
            });
        }

        function SaveFun() {
            if (!$("#frmPost").valid()) {
                return;
            }
            var url = 'Add';
            var data = { Id: $("#Id").val(), ModuleRoleId: $("#ModuleRoleId").val() };
            if ($('#Id').val() != null && $('#Id').val() != "")
                url = 'SaveUserApply';
            $.post(url, data, function (status) {
                if (DHQR.ProcessStatus(status)) {
                    resetGridData();
                    CleanFormData("frmPost");
                    $("#detailDlg").dialog("close");
                }
            });
        }

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
