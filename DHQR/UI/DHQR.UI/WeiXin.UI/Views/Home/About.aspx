<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Default-----------------
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="uinorth" class="ui-layout-north" style="border-style: solid; border-color: #ccc;
        border-width: 0px;">
        <div style="height: 28px; text-align: left; background-color: rgb(242, 246, 250);">
            <label style="width: 50px; float: left; text-align: right; margin-top: 3px; font-size: 12px;">
                头部部分</label>
        </div>
        <table id="Table1">
        </table>
    </div>
    <div id="uiWest" class="ui-layout-west" style="border-style: solid; border-color: #ccc;
        border-width: 0px;">
        asddsadsdasds
        <%-- <div style="height: 28px; text-align: left; background-color: rgb(242, 246, 250);">
            <label style="width: 50px; float: left; text-align: right; margin-top: 3px; font-size: 12px;">
                业务主体</label>
        </div>--%>
        <table id="contractPeriodList">
        </table>
    </div>
    <div id="uiCenter" class="ui-layout-center">
        <iframe></iframe>
    </div>
    <div id="uiSouth" class="ui-layout-south" style="border-style: solid; border-color: #ccc;
        border-width: 0px;">
        底部
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
    <script type="text/javascript">
        $(function () {
            $("body").layout();
            var myLayout = $("#container").layout({
                "west": { size: "290", closable: true, resizable: false, slidable: false },
                "north": { size: "120", closable: false, resizable: false, slidable: false
                     , spacing_open: 1
                    , togglerLength_open: 0			// HIDE the toggler button
	               , togglerLength_closed: -1			// "100%" OR -1 = full width of pane
                },
                "south": { size: "40", closable: false, resizable: false, slidable: false
                     , spacing_open: 1
                    , togglerLength_open: 0			// HIDE the toggler button
	               , togglerLength_closed: -1			// "100%" OR -1 = full width of pane
                }
            });
        });
    </script>
</asp:Content>