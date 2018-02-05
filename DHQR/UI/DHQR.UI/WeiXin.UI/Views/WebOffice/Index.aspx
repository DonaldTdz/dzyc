<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="font-size: 12px; color: #FF0000;">
        注::如果不能自动安装iWebOffice2000插件，请在这里下载<a href="../../InstallClient.zip">[本地安装程序]</a>并安装。
    </div>
    <div id="webOfficeContainer">
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
    <script src="../../Scripts/WebOffice/iWebOffice2000.js" type="text/javascript"></script>
    <script type="text/javascript">
        var param = window.parent.dialogArguments;
        var webOffice;
        $(document).ready(function () {
            $('body').layout();
            webOffice = $("#webOfficeContainer").iWebOffice({
                WebUrl: "HandelMsg",
                RecordID: param.Id,
                FileName: param.Name,
                FileType: ".doc"
            });
            webOffice.loadDocument();
        })

        function saveDocument() {
            webOffice.saveDocument();
        }
    </script>
</asp:Content>
