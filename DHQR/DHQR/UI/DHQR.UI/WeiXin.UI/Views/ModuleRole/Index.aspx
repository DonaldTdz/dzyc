<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    角色管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="uiCenter" class="ui-layout-center">
        <div class="ui-widget-header" style="height: 28px;">
            <label style="float: left; margin-left: 10px; margin-top: 3px; font-size: 14px;">
                角色清单</label>
            <button id="btnAdd" style="height: 23px; margin-left: 20px; margin-top: 2px;">
                新增</button>
            <button id="btnEdit" style="height: 23px; margin-top: 2px;">
                编辑</button>
            <button id="btnDelete" style="height: 23px; margin-top: 2px;">
                删除</button>
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
    <div id="southContener" class="gridContainer">
        <ul>
            <li><a href="#module-tab">分配功能项</a></li>
            <li><a href="#user_tab">分配用户</a></li>
            <div>
                <button id="sub_Add" style="height: 23px; margin-left: 20px; margin-top: 2px;">
                    新增</button>
                <button id="sub_Del" style="height: 23px; margin-top: 2px;">
                    删除</button>
            </div>
        </ul>
        <div id="module-tab">
            <table id="moduleList">
            </table>
            <div id="modulePager">
            </div>
        </div>
        <div id="user_tab">
            <table id="userList">
            </table>
            <div id="userPager">
            </div>
        </div>
    </div>
    <div id="detailDlg" class="div-dialog">
        <form id="frmPost" class="form-horizontal" method="post" action="">
        <input id="Id" name="Id" type="hidden" />
        <div id="summary">
        </div>
        <div class="control-group">
            <label class="control-label">
                角色代码：</label>
            <div class="controls">
                <input type="text" class="required  ui-input-text ui-input-text-form" id="Code" name="Code" /></div>
        </div>
        <div class="control-group">
            <label class="control-label">
                角色名称：</label>
            <div class="controls">
                <input type="text" class="required  ui-input-text ui-input-text-form" id="Name" name="Name" /></div>
        </div>
        <div class="control-group">
            <label class="control-label">
                角色说明：</label>
            <div class="controls">
                <textarea rows="3" type="text" class="required  ui-input-text ui-input-text-form"
                    id="Note" name="Note"></textarea></div>
        </div>
        </form>
    </div>
    <div id="moduleDlg" class="div-dialog">
        <table id="selModuleList">
        </table>
        <div id="selModulePager">
        </div>
    </div>
    <div id="userDlg" class="div-dialog">
        <table id="selUserList">
        </table>
        <div id="selUserPager">
        </div>
    </div>
    <%--权限对话框--%>
    <div id="RoleDetailDialog" class="div-dialog">
        <div id="RoleDetailDialogTips" class="ui-state-highlight validationTips-default">
        </div>
        <iframe id="DetailFrame" name="DetailFrame" class="ui-layout-center" width="100%"
            height="350" frameborder="0" scrolling="auto" src=""></iframe>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
    <link href="../../Content/Box/Content.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/Common/fromCommon.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/JqGrid/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/JqGrid/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script src="../../Scripts/JqGrid/grid.locale-cn.js" type="text/javascript"></script>
    <script src="../../Scripts/Box/jquery.Box.min.js" type="text/javascript"></script>
    <script src="../../Scripts/js/Common.js" type="text/javascript"></script>
    <script src="../../Scripts/Shop/shop.js" type="text/javascript"></script>
    <style type="text/css">
        .controls input[type="text"], .controls textarea
        {
            width: 130px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
    <script type="text/javascript">
        var currentTabId = 0;
        function resizeGrid() {
            $("#listTable").jqGrid("setGridWidth", $(window).width() - 5);
            $("#listTable").jqGrid("setGridHeight", $(window).height() - 370);
            $("#moduleList").jqGrid("setGridWidth", $(window).width() - 5);
            $("#moduleList").jqGrid("setGridHeight", $(window).height() - 310);
            $("#userList").jqGrid("setGridWidth", $(window).width() - 5);
            $("#userList").jqGrid("setGridHeight", $(window).height() - 310);
            $("#selUserList").jqGrid("setGridWidth", 610);
            $("#selUserList").jqGrid("setGridHeight", 200);
            $("#selModuleList").jqGrid("setGridWidth", 610);
            $("#selModuleList").jqGrid("setGridHeight", 200);


        }
        $(function () {
            $("body").layout();
            $("#southContener").tabs({
                collapsible: false,
                selected: 0,
                select: function (event, ui) {
                    currentTabId = ui.index;
                }
            });

            InitialDialogs();
            InitialControls();
            DHQR.formValid("frmPost");
            InitialGrids();
            //grid加载

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
        function addActions() {
            var grid = $("#listTable");
            var ids = grid.getDataIDs();
            for (var i = 0; i < ids.length; i++) {
                var rowId = ids[i];
                var detailBtn = Format("<span  onclick='DoConfig({0})' title='配置' class='ui-icon ui-icon-wrench  gridToolWidget' style='float:left;margin-left:1px' ></span>", rowId);
                grid.jqGrid("setRowData", rowId, { Actions: detailBtn });
            }
        }

        function InitialGrids() {
            $("#listTable").jqGrid({
                url: 'GetPageData',
                datatype: 'local',
                colModel: [
                            { name: 'Actions', index: 'Actions', label: '操作', width: 20 },
   		                    { name: 'Id', index: 'Id', label: 'Id', width: 55, hidden: true },
   		                    { name: 'Code', index: 'Name', label: '角色代码', width: 50 },
   		                    { name: 'Name', index: 'Name', label: '角色名称', width: 50 },
                            { name: 'Note', index: 'Note', label: '角色说明', width: 80 }
                           ],
                pager: '#listPager',
                sortname: 'id',
                viewrecords: true,
                sortorder: "desc",
                width: 200,
                loadonce: true,
                onCellSelect: function (rowId) {
                    if (rowId != "" && rowId != null) {
                        var SelRowdata = $("#listTable").getRowData(rowId);
                        showObjectOnForm('frmPost', SelRowdata);
                        var grid = $(this);
                        var postData = { roleId: SelRowdata.Id };
                        reloadGrid("moduleList", "modulePager", "GetModuleByRoleId", postData);
                        reloadGrid("userList", "userPager", "GetUserByRoleId", postData);
                    }
                },
                loadComplete: function () {
                    addActions();
                }
            });
            resetGridData();

            $("#moduleList").jqGrid({
                datatype: 'local',
                colModel: [
   		                    { name: 'Id', index: 'Id', label: 'Id', width: 55, hidden: true },
   		                    { name: 'Code', index: 'Code', label: '代码', width: 50 },
   		                    { name: 'Name', index: 'Name', label: '名称', width: 50 },
   		                    { name: 'ControllerName', index: 'ControllerName', label: '控制器类名', width: 50 },
   		                    { name: 'ActionName', index: 'ActionName', label: 'ACTION名称', width: 50 },
                            { name: 'Url', index: 'Url', label: '功能项地址', width: 80 },
                            { name: 'IsMenu', index: 'IsMenu', label: '是否菜单项', formatter: 'checkbox', editoptions: { value: "true:false" }, width: 80, align: "center" }
                           ],
                pager: '#modulePager',
                sortname: 'Code',
                viewrecords: true,
                sortorder: "desc",
                multiselect:true,
                width: 200,
                loadonce: true,
                onCellSelect: function (rowId) {

                },
                loadComplete: function () {
                    addActions();
                }
            });

            $("#userList").jqGrid({
                datatype: 'local',
                colModel: [
   		                    { name: 'Id', index: 'Id', label: 'Id', width: 55, hidden: true },
                            { name: 'key', index: 'key', label: 'key', hidden: true },
                            { name: 'Name', index: 'Name', label: '登录名', width: 50 },
                            { name: 'Nickname', index: 'Nickname', label: '名称', width: 50 },
                            { name: 'DepartmentName', index: 'DepartmentId', label: '部门名称', width: 80 }
                           ],
                pager: '#userPager',
                sortname: 'Name',
                viewrecords: true,
                multiselect: true,
                sortorder: "desc",
                width: 200,
                loadonce: true,
                onCellSelect: function (rowId) {
                },
                loadComplete: function () {
                    addActions();
                }
            });

            $("#selModuleList").jqGrid({
                datatype: 'local',
                colModel: [
   		                    { name: 'Id', index: 'Id', label: 'Id', width: 55, hidden: true },
   		                    { name: 'Code', index: 'Code', label: '代码', width: 50 },
   		                    { name: 'Name', index: 'Name', label: '名称', width: 50 },
   		                    { name: 'ControllerName', index: 'ControllerName', label: '控制器类名', width: 50 },
   		                    { name: 'ActionName', index: 'ActionName', label: 'ACTION名称', width: 50 },
                            { name: 'Url', index: 'Url', label: '功能项地址', width: 80 },
                            { name: 'IsMenu', index: 'IsMenu', label: '是否菜单项', formatter: 'checkbox', editoptions: { value: "true:false" }, width: 80, align: "center" }
                           ],
                pager: '#selModulePager',
                sortname: 'Code',
                viewrecords: true,
                sortorder: "desc",
                width: 200,
                multiselect: true,
                loadonce: true,
                onCellSelect: function (rowId) {
                    
                },
                loadComplete: function () {
                    addActions();
                }
            });

            $("#selUserList").jqGrid({
                datatype: 'local',
                colModel: [
   		                    { name: 'Id', index: 'Id', label: 'Id', width: 55, hidden: true },
                            { name: 'key', index: 'key', label: 'key', hidden: true },
                            { name: 'Name', index: 'Name', label: '登录名', width: 50 },
                            { name: 'Nickname', index: 'Nickname', label: '名称', width: 50 },
                            { name: 'DepartmentName', index: 'DepartmentId', label: '部门名称', width: 80 }
                           ],
                pager: '#selUserPager',
                sortname: 'Name',
                viewrecords: true,
                multiselect: true,
                sortorder: "desc",
                width: 200,
                loadonce: true,
                onCellSelect: function (rowId) {
                   
                },
                loadComplete: function () {
                    addActions();
                }
            });


        }

        function InitialDialogs() {
            $("#detailDlg").dialog({
                autoOpen: false,
                resizable: false,
                modal: true,
                width: 425,
                height: 260,
                title: '编辑',
                resizable: true,
                position: ['center', 'center'],
                buttons: {
                    '保存': function (event) {
                        SaveFun();
                        $(this).dialog("close");
                    },
                    '取消': function () {
                        $(this).dialog("close");
                    }
                }
            });
            $("#moduleDlg").dialog({
                autoOpen: false,
                resizable: false,
                modal: true,
                width: 650,
                height: 360,
                title: '选择功能项',
                resizable: true,
                position: ['center', 'center'],
                buttons: {
                    '保存': function (event) {
                        SaveModules();
                    },
                    '取消': function () {
                        $(this).dialog("close");
                    }
                }
            });

            $("#userDlg").dialog({
                autoOpen: false,
                resizable: false,
                modal: true,
                width: 650,
                height: 360,
                title: '选择用户',
                resizable: true,
                position: ['center', 'center'],
                buttons: {
                    '保存': function (event) {
                        SaveUsers();
                    },
                    '取消': function () {
                        $(this).dialog("close");
                    }
                }
            });

        }

        function InitialControls() {
            //新增
            $("#btnAdd").button({ icons: { primary: "ui-icon-plus"} }).click(function () {
                CleanFormData("frmPost");
                $("#detailDlg").dialog('option', 'title', '新增');
                $("#detailDlg").dialog("open");
            });

            //编辑
            $("#btnEdit").button({ icons: { primary: "ui-icon-disk"} }).click(function () {
                var grid = $("#listTable");
                var selRowId = grid.jqGrid('getGridParam', 'selrow');
                if (selRowId == undefined) {
                    MsgAlert("警告", "请先选择需要编辑的行项目！");
                    return;
                }
                var selRowData = grid.getRowData(selRowId);
                showObjectOnForm("frmPost", selRowData);
                $("#detailDlg").dialog('option', 'title', '编辑');
                $("#detailDlg").dialog("open");
            });

            //删除
            $("#btnDelete").button({ icons: { primary: "ui-icon-trash"} }).click(function () {
                var grid = $("#listTable");
                var selRowId = grid.jqGrid('getGridParam', 'selrow');
                if (selRowId == undefined || selRowId == null) {
                    MsgAlert("警告", "请先选择需要删除的行项目！");
                    return;
                }
                var selRowData = grid.getRowData(selRowId);
                confirmDialog("是否删除所选项", function (r) {
                    $.post('Del', { Id: selRowData.Id ,Code:selRowData.Code}, function (data) {
                        if (DHQR.ProcessStatus(data)) {
                            resetGridData();
                            CleanFormData("frmPost");
                        }
                    });
                }, "删除确认", true, function () { }
                    );

            });

            //新增
            $("#sub_Add").button({ icons: { primary: "ui-icon-plus"} }).click(function () {
                var grid = $("#listTable");
                var selRowId = grid.jqGrid('getGridParam', 'selrow');
                if (selRowId == undefined) {
                    MsgAlert("警告", "请先选择行！");
                    return;
                }
                var selRowData = grid.getRowData(selRowId);
                if (currentTabId == 0) {
                    var postData = { roleId: selRowData.Id };
                    reloadGrid("selModuleList", "selModulePager", "GetUnSelModuleByRoleId", postData);
                    $("#moduleDlg").dialog("open");
                }
                else {
                    var postData = { roleId: selRowData.Id };
                    reloadGrid("selUserList", "selUserPager", "GetUnselUserByRoleId", postData);
                    $("#userDlg").dialog("open");

                }
            });

            //删除
            $("#sub_Del").button({ icons: { primary: "ui-icon-trash"} }).click(function () {
                if (currentTabId == 0) {
                    var grid = $("#moduleList");
                    var selRowId = grid.jqGrid('getGridParam', 'selrow');
                    if (selRowId == undefined || selRowId == null) {
                        MsgAlert("警告", "请先选择需要删除的行项目！");
                        return;
                    }
                    var selRowData = grid.getRowData(selRowId);
                    confirmDialog("是否删除所功能项", function (r) {
                        DelModules();
                    }, "删除确认", true, function () { }
                    );
                }

                else {
                    var grid = $("#userList");
                    var selRowId = grid.jqGrid('getGridParam', 'selrow');
                    if (selRowId == undefined || selRowId == null) {
                        MsgAlert("警告", "请先选择需要删除的行项目！");
                        return;
                    }
                    var selRowData = grid.getRowData(selRowId);
                    confirmDialog("是否删除所选中用户", function (r) {
                        DelUsers();
                    }, "删除确认", true, function () { }
                    );

                }
            });


        }

        function SaveModules() {
            var grid = $("#selModuleList");
            var selRowIds = grid.jqGrid('getGridParam', 'selarrrow');
            if (selRowIds == undefined || selRowIds == null || selRowIds.length == 0) {
                MsgAlert("警告", "请先选择需要新增的功能项！");
                return;
            }
            var jsonData = "";
            $.each(selRowIds, function (key, val) {
                var data = grid.getRowData(val);
                if (jsonData == "") {
                    jsonData = jsonData + data.Id;
                }
                else {
                    jsonData = jsonData + "," + data.Id;
                }
            });
            var selroleId = $("#listTable").jqGrid('getGridParam', 'selrow');
            var selRole = $("#listTable").getRowData(selroleId);
            var postData = { moduleIds: jsonData, roleId: selRole.Id };
            $.post("AddModuleToRole", postData, function (dohandle) {
                if (DHQR.ProcessStatus(dohandle)) {
                    var grid = $("#listTable");
                    var selRowId = grid.jqGrid('getGridParam', 'selrow');
                    var SelRowdata = grid.getRowData(selRowId);
                    var postData = { roleId: SelRowdata.Id };
                    reloadGrid("moduleList", "modulePager", "GetModuleByRoleId", postData);
                    $("#moduleDlg").dialog("close");
                }

            });
        }

        function SaveUsers() {
            var grid = $("#selUserList");
            var selRowIds = grid.jqGrid('getGridParam', 'selarrrow');
            if (selRowIds == undefined || selRowIds == null || selRowIds.length == 0) {
                MsgAlert("警告", "请先选择需要新增的用户！");
                return;
            }
            var jsonData = "";
            $.each(selRowIds, function (key, val) {
                var data = grid.getRowData(val);
                if (jsonData == "") {
                    jsonData = jsonData + data.Id;
                }
                else {
                    jsonData = jsonData + "," + data.Id;
                }
            });
            var selroleId = $("#listTable").jqGrid('getGridParam', 'selrow');
            var selRole = $("#listTable").getRowData(selroleId);
            var postData = { userIds: jsonData, roleId: selRole.Id };
            $.post("AddUserToRole", postData, function (dohandle) {
                if (DHQR.ProcessStatus(dohandle)) {
                    var grid = $("#listTable");
                    var selRowId = grid.jqGrid('getGridParam', 'selrow');
                    var SelRowdata = grid.getRowData(selRowId);
                    var postData = { roleId: SelRowdata.Id };
                    reloadGrid("userList", "userPager", "GetUserByRoleId", postData);
                    $("#userDlg").dialog("close");
                }

            });

        }

        function DelModules() {
            var grid = $("#moduleList");
            var selRowIds = grid.jqGrid('getGridParam', 'selarrrow');
            if (selRowIds == undefined || selRowIds == null || selRowIds.length == 0) {
                MsgAlert("警告", "请先选择需要删除的功能项！");
                return;
            }
            var jsonData = "";
            $.each(selRowIds, function (key, val) {
                var data = grid.getRowData(val);
                if (jsonData == "") {
                    jsonData = jsonData + data.Id;
                }
                else {
                    jsonData = jsonData + "," + data.Id;
                }
            });
            var selroleId = $("#listTable").jqGrid('getGridParam', 'selrow');
            var selRole = $("#listTable").getRowData(selroleId);
            var postData = { moduleIds: jsonData, roleId: selRole.Id };
            $.post("DelModuleToRole", postData, function (dohandle) {
                if (DHQR.ProcessStatus(dohandle)) {
                    var grid = $("#listTable");
                    var selRowId = grid.jqGrid('getGridParam', 'selrow');
                    var SelRowdata = grid.getRowData(selRowId);
                    var postData = { roleId: SelRowdata.Id };
                    reloadGrid("moduleList", "modulePager", "GetModuleByRoleId", postData);
                }

            });

        }

        function DelUsers() {
            var grid = $("#userList");
            var selRowIds = grid.jqGrid('getGridParam', 'selarrrow');
            if (selRowIds == undefined || selRowIds == null || selRowIds.length == 0) {
                MsgAlert("警告", "请先选择需要删除的功能项！");
                return;
            }
            var jsonData = "";
            $.each(selRowIds, function (key, val) {
                var data = grid.getRowData(val);
                if (jsonData == "") {
                    jsonData = jsonData + data.Id;
                }
                else {
                    jsonData = jsonData + "," + data.Id;
                }
            });
            var selroleId = $("#listTable").jqGrid('getGridParam', 'selrow');
            var selRole = $("#listTable").getRowData(selroleId);
            var postData = { userIds: jsonData, roleId: selRole.Id };
            $.post("DelUserToRole", postData, function (dohandle) {
                if (DHQR.ProcessStatus(dohandle)) {
                    var grid = $("#listTable");
                    var selRowId = grid.jqGrid('getGridParam', 'selrow');
                    var SelRowdata = grid.getRowData(selRowId);
                    var postData = { roleId: SelRowdata.Id };
                    reloadGrid("userList", "userPager", "GetUserByRoleId", postData);
                }

            });
        }

        function resetGridData() {
            var grid = $("#listTable");
            $.post("GetAll", {}, function (result) {
                grid.clearGridData(false);
                grid.jqGrid("setGridParam", {
                    data: result,
                    datatype: "local"
                }).trigger("reloadGrid");
            });
        }

        //重新加载grid数据
        //gridid:gridId
        //url请求f地址
        //param请求参数
        function reloadGrid(gridid, pagerId, url, param) {
            $("#" + gridid).jqGrid("setGridParam", {
                url: url,
                datatype: 'json',
                postData: param,
                mtype: 'POST',
                pager: '#' + pagerId
            }).trigger("reloadGrid");
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
            $.post('Del', data, function (status) {
                if (DHQR.ProcessStatus(status)) {
                    //refreshGrid('listTable');
                    resetGridData();
                    CleanFormData("frmPost");
                }
            });
        }


        function SaveFun() {
            if (!$("#frmPost").valid()) {
                return;
            }
            var url = 'Add';
            var data = $("#frmPost").serialize();
            if ($('#Id').val() != null && $('#Id').val() != "")
                url = 'Edit';
            $.post(url, data, function (status) {
                if (DHQR.ProcessStatus(status)) {
                    resetGridData();
                    //$.DialogBox.tip(status.OperateMsg);
                    CleanFormData("frmPost");
                }
            });
        }
        //配置权限
        function DoConfig(rowId) {
            var SelRowdata = $("#listTable").getRowData(rowId);
            $('#SelRole').html(SelRowdata.Name);
            var url = "../../ModuleRole/RoleToAction?RoleId=" + SelRowdata.Id;
            $('#DetailFrame').attr('src', url);
            $("#RoleDetailDialog").dialog('open');
        }
        //权限
        $(function () {
            $("#RoleDetailDialog").dialog({
                autoOpen: false,
                resizable: false,
                height: 400,
                width: 620,
                modal: true,
                title: '分配权限：角色【<span id="SelRole"></span>】',
                close: function () {
                }
            });
        });
    </script>
</asp:Content>
