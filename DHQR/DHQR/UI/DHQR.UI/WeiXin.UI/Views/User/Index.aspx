<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout.Master" Inherits="System.Web.Mvc.ViewPage<UserModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    用户管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="uiCenter" class="ui-layout-center">
        <div class="ui-widget-header" style="height: 28px;">
            <label style="float: left; margin-left: 10px; margin-top: 3px; font-size: 14px;">
                用户清单</label>
              <%--  <button id="allotModule" style="height: 23px; margin-left: 20px; margin-top: 2px;display:hidden;"/>
                    分配角色</button>--%>
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
        <div class=" ui-widget-header" style="height: 28px;">
            <button id="AddBtn" style="float: left; margin-top: 2px; height: 23px;" onclick="AddFun();">
                新增</button>
            <button id="EditBtn" style="float: left; margin-top: 2px; height: 23px;" onclick="SaveFun();">
                保存</button>
            <button id="DelBtn" style="float: left; margin-top: 2px; height: 23px;" onclick="DelFun();">
                冻结</button>
        </div>
        <div style="height: 10px;">
        </div>
        <form id="frmPost" class="form-horizontal" method="post" action="">
        <input id="Id" name="Id" type="hidden" />
        <div class="control-group">
            <label class="control-label">
                登录名：</label>
            <div class="controls">
                <input type="text" class="required" id="Name" name="Name" /></div>
        </div>
        <div class="control-group">
            <label class="control-label">
                昵称：</label>
            <div class="controls">
                <input type="text" class="required" id="Nickname" name="Nickname" /></div>
        </div>
        <div class="control-group">
            <label class="control-label">
                密码：</label>
            <div class="controls">
                <input type="password" class="required" id="PassWord" name="PassWord" /></div>
        </div>
        <div class="control-group">
            <label class="control-label">
                是否冻结：</label>
            <div class="controls">
                <input id="IsVisible" name="IsVisible" value="true" type="checkbox" /></div>
        </div>
        </form>
    </div>
    <div id="dialog-confirm" title="提示">
        <div id="UserNameInfo">
        </div>
    </div>
    <div id="moduleDlg" class="div-dialog">
        <ul>
            <li><a href="#allot-tab">已分配角色</a></li>
            <li><a href="#unallot-tab">未分配角色</a></li>
            <div>
                <button id="sub_Add" style="height: 23px; margin-left: 20px; margin-top: 2px;">
                    分配</button>
                <button id="sub_Del" style="height: 23px; margin-top: 2px;">
                    删除</button>
            </div>
        </ul>
        <div id="allot-tab">
            <table id="allotList">
            </table>
            <div id="allotPager">
            </div>
        </div>
        <div id="unallot_tab">
            <table id="unallotList">
            </table>
            <div id="unallotPager">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
    <link href="../../Content/JqGrid/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/css/select2.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/Common/fromCommon.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/JqGrid/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script src="../../Scripts/JqGrid/grid.locale-cn.js" type="text/javascript"></script>
    <link href="../../Content/Box/Content.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Box/jquery.Box.min.js" type="text/javascript"></script>
    <script src="../../Scripts/js/Common.js" type="text/javascript"></script>
    <script src="../../Scripts/js/select2.min.js" type="text/javascript"></script>
    <style type="text/css">
        .controls input[type="text"], .controls textarea, .controls input[type="password"]
        {
            width: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
    <script type="text/javascript">
        function resizeGrid() {
            //  alert($(window).height());
            $("#listTable").jqGrid("setGridWidth", $(window).width() - 270);
            $("#listTable").jqGrid("setGridHeight", $(window).height() - 80);
            $("#listTable").jqGrid("setGridWidth", $(window).width() - 270);
            $("#listTable").jqGrid("setGridHeight", $(window).height() - 80);
            $("#listTable").jqGrid("setGridWidth", $(window).width() - 270);
            $("#listTable").jqGrid("setGridHeight", $(window).height() - 80);


        }
        $(function () {
            $("body").layout();
            $("#listTable").jqGrid({
                url: '../../User/GetPageData',
                colModel: [
                            { name: 'Actions', index: 'Actions', label: '操作', width: 50 },
   		                    { name: 'Id', index: 'Id', label: 'Id', width: 55, hidden: true },
                             { name: 'key', index: 'key', label: 'key', hidden: true },
                             { name: 'Name', index: 'Name', label: '登录名', width: 50 },
                             { name: 'Nickname', index: 'Nickname', label: '用户姓名', width: 50 },
                            { name: 'IsFreeze', index: 'IsFreeze', label: '冻结', width: 80,
                                formatter: 'checkbox', editoptions: { value: "true:false" }, align: 'center'
                            }
                           ],
                pager: '#listPager',
                sortname: 'id',
                datatype: 'local',
                viewrecords: true,
                sortorder: "desc",
                loadonce: true,
                width: 200,
                loadComplete: function () {
                    addActions();
                },
                onCellSelect: function (rowId) {
                    if (rowId != "" && rowId != null) {
                        var SelRowdata = $("#listTable").getRowData(rowId);
                        showObjectOnForm('frmPost', SelRowdata);
                        $("#Name").attr("disabled", "disabled");
                    }
                }
            });
            InitialDialogs();
            InitialControls();
            $("#moduleDlg").tabs({
                collapsible: false,
                selected: 0,
                select: function (event, ui) {

                }
            });

            //grid加载
            function addActions() {
                var grid = $("#listTable");
                var ids = grid.getDataIDs();
                for (var i = 0; i < ids.length; i++) {
                    var rowId = ids[i];
                    var detailBtn = Format("<span  onclick='TryFreeze({0})' title='冻结' class='ui-icon ui-icon-play  gridToolWidget' style='float:left;margin-left:1px' ></span>", rowId);
                    var rowData = $("#listTable").getRowData(rowId);
                    if (rowData.IsFreeze == "true")
                    { detailBtn = Format("<span  onclick='UndoFreeze({0})' title='解冻' class='ui-icon ui-icon-pause  gridToolWidget' style='float:left;margin-left:1px' ></span>", rowId); }
                    grid.jqGrid("setRowData", rowId, { Actions: detailBtn });
                }
            }
            DHQR.formValid("frmPost");

            resetGridData();

            $("#allotList").jqGrid({
                url: 'GetPageData',
                datatype: 'local',
                colModel: [
                            { name: 'Actions', index: 'Actions', label: '操作', width: 20 },
   		                    { name: 'Id', index: 'Id', label: 'Id', width: 55, hidden: true },
   		                    { name: 'Code', index: 'Name', label: '角色代码', width: 50 },
   		                    { name: 'Name', index: 'Name', label: '角色名称', width: 50 },
                            { name: 'Note', index: 'Note', label: '角色说明', width: 80 }
                           ],
                pager: '#allotPager',
                sortname: 'id',
                viewrecords: true,
                sortorder: "desc",
                width: 200,
                loadonce: true,
                onCellSelect: function (rowId) {
                },
                loadComplete: function () {
                }
            });

            $("#unallotList").jqGrid({
                url: 'GetPageData',
                datatype: 'local',
                colModel: [
                            { name: 'Actions', index: 'Actions', label: '操作', width: 20 },
   		                    { name: 'Id', index: 'Id', label: 'Id', width: 55, hidden: true },
   		                    { name: 'Code', index: 'Name', label: '角色代码', width: 50 },
   		                    { name: 'Name', index: 'Name', label: '角色名称', width: 50 },
                            { name: 'Note', index: 'Note', label: '角色说明', width: 80 }
                           ],
                pager: '#unallotPager',
                sortname: 'id',
                viewrecords: true,
                sortorder: "desc",
                width: 200,
                loadonce: true,
                onCellSelect: function (rowId) {
                },
                loadComplete: function () {
                }
            });



            var myLayout = $("#container").layout({
                "west": { size: "260", closable: false, resizable: false, slidable: false }
            });

            $("#AddBtn").button({ icons: { primary: "ui-icon-plus"} });
            $("#EditBtn").button({ icons: { primary: "ui-icon-plus"} });
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

        var timeout = null;
        $(window).bind("resize", function () {
            if (timeout) {
                clearTimeout(timeout);
            }
            timeout = setTimeout(resizeGrid, 300);
        });

        function InitialDialogs() {
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

        }

        function InitialControls() {
            //打开对话框
            $("#allotModule").button({ icons: { primary: "ui-icon-plus"} }).click(function () {
                $("#moduleDlg").dialog("open");
            });


            //新增
            $("#sub_Add").button({ icons: { primary: "ui-icon-plus"} }).click(function () {
            });

            //删除
            $("#sub_Del").button({ icons: { primary: "ui-icon-trash"} }).click(function () {
            });


        }

    </script>
    <script type="text/javascript">
        function AddFun() {
            $("#Name").removeAttr("disabled");
            CleanFormData("frmPost");
        }
        function DelFun() {
            var data = $("#frmPost").serialize();
            if ($('#Id').val() == null || $('#Id').val() == "")
            { $.DialogBox.tip('请选择需要删除的项!', 'error'); return; }
            $.post('Del', data, function (status) {
                if (status.IsSuccessful) {
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
                    CleanFormData("frmPost");
                }
            });
        }
    </script>
    <script type="text/javascript">
        var selRowId;
        $(function () {
            $("#dialog:ui-dialog").dialog("destroy");
            $("#dialog-confirm").dialog({
                autoOpen: false,
                resizable: false,
                height: 140,
                modal: true,
                buttons: {
                    "确定": function () {
                        var data = $("#listTable").getRowData(selRowId);
                        $.post('Freeze', { Id: data.Id, IsFreeze: data.IsFreeze },
                         function (status) {
                             resetGridData();
                             DHQR.ProcessStatus(status);
                             $('#dialog-confirm').dialog("close");
                         });
                    },
                    '取消': function () {
                        $(this).dialog("close");
                    }
                }
            });

        });
        function TryFreeze(rowId) {
            selRowId = rowId;
            var data = $("#listTable").getRowData(selRowId);
            $('#UserNameInfo').html("是否冻结该用户[" + data.Name + "]");
            $("#dialog-confirm").dialog("open");
        }

        function UndoFreeze(rowId) {
            selRowId = rowId;
            var data = $("#listTable").getRowData(selRowId);
            $('#UserNameInfo').html("是否解除冻结用户[" + data.Name + "]");
            $("#dialog-confirm").dialog("open");
        }
    </script>
</asp:Content>
