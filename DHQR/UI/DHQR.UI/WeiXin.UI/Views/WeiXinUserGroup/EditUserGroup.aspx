<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    用户组分配
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="uiCenter" class="ui-layout-center">
        <div class="ui-widget-header" style="height: 28px;">
            <label style="float: left; margin-left: 10px; margin-top: 3px; font-size: 14px;">
                用户组</label>
            <button id="btnBack" style="height: 23px; margin-left: 20px; margin-top: 2px;">
                返回</button>
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
            <li><a href="#user_tab">分配用户</a></li>
            <div>
                <button id="sub_Add" style="height: 23px; margin-left: 20px; margin-top: 2px;">
                    新增</button>
                <button id="sub_Del" style="height: 23px; margin-top: 2px;">
                    删除</button>
            </div>
        </ul>
        <div id="user_tab">
            <table id="userList">
            </table>
            <div id="userPager">
            </div>
        </div>
    </div>
    <div id="userDlg" class="div-dialog">
        <table id="selUserList">
        </table>
        <div id="selUserPager">
        </div>
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
            $("#listTable").jqGrid("setGridHeight", $(window).height() - 450);
            $("#userList").jqGrid("setGridWidth", $(window).width() - 5);
            $("#userList").jqGrid("setGridHeight", $(window).height() - 400);
            $("#selUserList").jqGrid("setGridWidth", 610);
            $("#selUserList").jqGrid("setGridHeight", 200);
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

            resizeGrid();
        });
        function InitialGrids() {
            $("#listTable").jqGrid({
                url:"GetAll",
                datatype: 'local',
                colModel: [
   		                    { name: 'Id', index: 'Id', label: 'Id', width: 55, hidden: true },
   		                    { name: 'groupid', index: 'groupid', label: '分组ID', width: 50 },
   		                    { name: 'name', index: 'name', label: '分组名称', width: 50 },
                            { name: 'count', index: 'count', label: '用户数量', width: 80 }
                ],
                pager: '#listPager',
                sortname: 'groupid',
                viewrecords: true,
                sortorder: "desc",
                width: 200,
                loadonce: true,
                onCellSelect: function (rowId) {
                    if (rowId != "" && rowId != null) {
                        var SelRowdata = $("#listTable").getRowData(rowId);
                        showObjectOnForm('frmPost', SelRowdata);
                        var grid = $(this);
                        var postData = { groupid: SelRowdata.groupid };
                        reloadGrid("userList", "userPager", "GetUserByGroupid", postData);
                    }
                },
                loadComplete: function () {
                    var grid = $(this);
                }
            });
            resetGridData();

            $("#userList").jqGrid({
                datatype: 'local',
                colModel: [
   		                    { name: 'Id', index: 'Id', label: 'Id', width: 55, hidden: true },
   		                    { name: 'openid', index: 'openid', label: 'openid', width: 55, hidden: true },
                            { name: 'nickname', index: 'nickname', label: '用户名', width: 50 },
                            { name: 'sex', index: 'sex', label: '性别', width: 50 },
                            { name: 'country', index: 'country', label: '国家', width: 50 },
                            { name: 'province', index: 'province', label: '省份', width: 50 },
                            { name: 'city', index: 'city', label: '城市', width: 50 },
                            { name: 'remark', index: 'remark', label: '备注', width: 50 }
                ],
                pager: '#userPager',
                sortname: 'nickname',
                viewrecords: true,
                multiselect: true,
                sortorder: "desc",
                width: 200,
                loadonce: true,
                onCellSelect: function (rowId) {
                },
                loadComplete: function () {
                   
                }
            });

            $("#selUserList").jqGrid({
                datatype: 'local',
                colModel: [
   		                    { name: 'Id', index: 'Id', label: 'Id', width: 55, hidden: true },
   		                    { name: 'openid', index: 'openid', label: 'openid', width: 55, hidden: true },
                            { name: 'nickname', index: 'nickname', label: '用户名', width: 50 },
                            { name: 'sex', index: 'sex', label: '性别', width: 50 },
                            { name: 'country', index: 'country', label: '国家', width: 50 },
                            { name: 'province', index: 'province', label: '省份', width: 50 },
                            { name: 'city', index: 'city', label: '城市', width: 50 },
                            { name: 'remark', index: 'remark', label: '备注', width: 50 }
                ],
                pager: '#selUserPager',
                sortname: 'nickname',
                viewrecords: true,
                multiselect: true,
                sortorder: "desc",
                width: 200,
                loadonce: true,
                onCellSelect: function (rowId) {

                },
                loadComplete: function () {
                  
                }
            });


        }

        function InitialDialogs() {
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
            
            //返回
            $("#btnBack").button({ icons: { primary: "ui-icon-circle-arrow-w" } }).click(function () {
                window.location.href = "Index";
            });

            //新增
            $("#sub_Add").button({ icons: { primary: "ui-icon-plus" } }).click(function () {
                var grid = $("#listTable");
                var selRowId = grid.jqGrid('getGridParam', 'selrow');
                if (selRowId == undefined) {
                    MsgAlert("警告", "请先选择行！");
                    return;
                }
                var selRowData = grid.getRowData(selRowId);
                    var postData = { roleId: selRowData.Id };
                    reloadGrid("selUserList", "selUserPager", "GetAllUsers", postData);
                    $("#userDlg").dialog("open");
            });

            //删除
            $("#sub_Del").button({ icons: { primary: "ui-icon-trash" } }).click(function () {
                    var grid = $("#userList");
                    var selRowId = grid.jqGrid('getGridParam', 'selrow');
                    if (selRowId == undefined || selRowId == null) {
                        MsgAlert("警告", "请先选择需要删除的行项目！");
                        return;
                    }
                    var selRowData = grid.getRowData(selRowId);
                    confirmDialog("是否将选中用户加入【未分组】", function (r) {
                        DelUsers();
                    }, "删除确认", true, function () { }
                    );

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
                    jsonData = jsonData + data.openid;
                }
                else {
                    jsonData = jsonData + "," + data.openid;
                }
            });
            var selroleId = $("#listTable").jqGrid('getGridParam', 'selrow');
            var selRole = $("#listTable").getRowData(selroleId);
            var postData = { openids: jsonData, groupid: selRole.groupid };
            $.post("AddToGroupByUserIds", postData, function (dohandle) {
                if (DHQR.ProcessStatus(dohandle)) {
                    //var grid = $("#listTable");
                    //var selRowId = grid.jqGrid('getGridParam', 'selrow');
                    //var SelRowdata = grid.getRowData(selRowId);
                    //var postData = { roleId: SelRowdata.Id };
                    //reloadGrid("userList", "userPager", "GetUserByGroupid", postData);
                    $("#userDlg").dialog("close");
                    $("#userList").clearGridData(false);
                    resetGridData();
                }

            });

        }

        function DelUsers() {
            var grid = $("#userList");
            var selRowIds = grid.jqGrid('getGridParam', 'selarrrow');
            if (selRowIds == undefined || selRowIds == null || selRowIds.length == 0) {
                MsgAlert("警告", "请先选择需要删除的用户！");
                return;
            }
            var jsonData = "";
            $.each(selRowIds, function (key, val) {
                var data = grid.getRowData(val);
                if (jsonData == "") {
                    jsonData = jsonData + data.openid;
                }
                else {
                    jsonData = jsonData + "," + data.openid;
                }
            });
            var selroleId = $("#listTable").jqGrid('getGridParam', 'selrow');
            var selRole = $("#listTable").getRowData(selroleId);
            var postData = { openids: jsonData };
            $.post("DelFromGroupByUserIds", postData, function (dohandle) {
                if (DHQR.ProcessStatus(dohandle)) {
                    //var grid = $("#listTable");
                    //var selRowId = grid.jqGrid('getGridParam', 'selrow');
                    //var SelRowdata = grid.getRowData(selRowId);
                    //var postData = { groupid: SelRowdata.groupid };
                    //reloadGrid("userList", "userPager", "GetUserByGroupid", postData);
                    $("#userList").clearGridData(false);
                    resetGridData();
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
</asp:Content>
