<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    角色分配权限管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%:Html.Hidden("RoleId",ViewData["RoleId"])%>
    <div id="uiCenter" class="ui-layout-center">
        <div class=" ui-widget-header" style="height: 22px;">
            <button id="SaveBtn" style="float: left; margin-left: 15px; height: 23px;" onclick="SaveFun();">
                保存</button>
            <%--   <button id="DelBtn" style="float: left; margin-left: 15px; height: 23px;" onclick="DelFun();">
                清理</button>--%>
        </div>
        <div id="Contenter" class="gridContainer">
            <div id="treeContent">
                <ul id="treeDemo" class="ztree">
                </ul>
            </div>
        </div>
    </div>
    <div id="uiwest" class="ui-layout-west">
        <div style="height: 24px; background: url(../../images/page/lefttitlebg.gif) repeat;">
            <label style="color: #fff; font-size: 9pt; font-weight: bold; line-height: 27px;
                font-family: Tahoma;">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 已经分配权限
            </label>
        </div>
        <div id="treeContent_New">
            <ul id="tree_New" class="ztree">
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
    <link href="../../Content/JqGrid/ui.jqgrid.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/JqGrid/jquery.jqGrid.src.js" type="text/javascript"></script>
    <script src="../../Scripts/JqGrid/grid.locale-cn.js" type="text/javascript"></script>
    <link href="../../Content/Box/Content.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Box/jquery.Box.min.js" type="text/javascript"></script>
    <link href="../../Content/Ztree/zTreeStyle/zTreeStyle.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/Ztree/jquery.ztree.core-3.3.min.js" type="text/javascript"></script>
    <script src="../../Scripts/Ztree/jquery.ztree.excheck-3.3.min.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
    <script type="text/javascript">
        function resizeGrid() {
            $("#listTable").jqGrid("setGridWidth", $(window).width() - 250);
            $("#listTable").jqGrid("setGridHeight", $(window).height() - 75);
        }
        $(function () {
            $("body").layout();

            var myLayout = $("#container").layout({
                "west": { size: "240", closable: false, resizable: false, slidable: false }
            });
            $("#SaveAllBtn").button({ icons: { primary: "ui-icon-plus"} });
            $("#SaveBtn").button({ icons: { primary: "ui-icon-disk"} });
            $("#DelBtn").button({ icons: { primary: "ui-icon-disk"} });
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
    <%--    //左树操作--%>
    <script type="text/javascript">
        var setting1 = {
            data: {
                simpleData: {
                    enable: true
                }
            },
            callback: {
            //  onMouseDown: onMouseDown
        }
    };
    $(function () {

        //  parent.parent.showWaitMessage("数据加载中，请稍等！");
        $.post('../../Menu/GetRoleMenuTree', { roleId: $('#RoleId').val() }, function (data) {
            $.fn.zTree.init($("#tree_New"), setting1, data);
            $("#treeContent_New").attr("style", "overflow-x: auto;overflow-y: auto;height:" + ($(window).height() - 20) + "px");
            closemsg();
        });
    });

    var loadNum = 0;
    function closemsg() {
        loadNum = loadNum + 1;
        if (loadNum == 2) {
            //            parent.parent.closeMessage();
        }
    }

    </script>
    <%--    //右树操作--%>
    <script type="text/javascript">
        var setting = {
            check: {
                enable: true
            },
            data: {
                simpleData: {
                    enable: true
                }
            },
            callback: {
            //  onMouseDown: onMouseDown
        }
    };
    $(function () {
        $.post('../../Menu/GetConfigRoleMenuTree', { roleId: $('#RoleId').val() }, function (data) {
            $.fn.zTree.init($("#treeDemo"), setting, data);
            $("#treeContent").attr("style", "overflow-x: auto;overflow-y: auto;height:" + ($(window).height() - 30) + "px");
            closemsg();
        });
    });
        
    </script>
    <script type="text/javascript">
        function SaveFun() {

            var treeObj = $.fn.zTree.getZTreeObj("treeDemo");
            var nodes = treeObj.getCheckedNodes(true);
            var ids = new Array();
            for (var i = 0; i < nodes.length; i++) {
                ids.push(nodes[i].id);
            }
            $.post('../../RoleToAction/SaveInfo', { roleId: $('#RoleId').val(), ids: ids }, function (data) {
                location.reload();
            });
        }


        function DelFun() {
            var ids = new Array();
            ids.push('ALL');
            $.post('../../RoleToAction/DelByMenuRole', { roleId: $('#RoleId').val(), menuId: menuId },
             function (status) {
                 loadGrid(menuId);
                 $.DialogBox.tip(status.OperateMsg);
             });
        }
    </script>
</asp:Content>
