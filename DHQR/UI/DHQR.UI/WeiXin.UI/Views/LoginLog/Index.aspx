<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    登录日志
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="uiCenter" class="ui-layout-center">
        <div class="ui-widget-header" style="height: 28px;">
            <label style="float: left; margin-left: 10px; margin-top: 3px; font-size: 14px;">
                登录日志记录</label>
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
   		                    { name: 'UserName', index: 'UserName', label: '登录名字', width: 50 },
                              { name: 'CreateTime', index: 'CreateTime', formatter: 'date', formatoptions: { newformat: 'Y-m-d H:i:s' }, label: '登录时间', width: 50 },
                               { name: 'LoginIp', index: 'LoginIp', label: '登录IP', width: 50 }
                            ],
                pager: '#listPager',
                sortname: 'CreateTime',
                viewrecords: true,
                datatype:'local',
                sortorder: "desc",
                // caption: "登录日志记录",
                loadonce: true,
                width: 200,
                onCellSelect: function (rowId) {
                    if (rowId != "" && rowId != null) {
                        var SelRowdata = $("#listTable").getRowData(rowId);
                        showObjectOnForm('frmPost', SelRowdata);
                    }
                }
            });

            resetGridData();

            var myLayout = $("#container").layout({
            //                "east": { size: "260", closable: false, resizable: false, slidable: false }
        });

        //        $("#AddBtn").button({ icons: { primary: "ui-icon-plus"} });
        //        $("#EditBtn").button({ icons: { primary: "ui-icon-disk"} });
        //        $("#DelBtn").button({ icons: { primary: "ui-icon-trash"} });
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
        //            grid.jqGrid("setGridParam", {
        //                url: "../../Menu/GetAll",
        //                datatype: "json",
        //                mtype:"POST",
        //                loadonce:false
        //            }).trigger("reloadGrid");
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
            $.post('Del', data, function (data) {
                if (data.IsSuccessful) {
                    refreshGrid('listTable');
                    $.DialogBox.tip(data.OperateMsg);
                    CleanFormData("frmPost");
                }
            });
        }


        function SaveFun() {
            var data = $("#frmPost").serialize();
            if (!$("#frmPost").valid()) {
                return;
            }
            var url = 'Add';
            if ($('#Id').val() != null && $('#Id').val() != "")
            { url = 'Edit'; }
            $.post(url, data, function (data) {
                if (data.IsSuccessful) {
                    refreshGrid('listTable');
                    CleanFormData("frmPost");
                }
                $.DialogBox.tip(data.OperateMsg);
            });
        }
    </script>
</asp:Content>
