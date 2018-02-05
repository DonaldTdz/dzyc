<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Layout.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%--    <input type="file" name="uploadify" id="uploadify" />
    <p>
        <a href="#" onclick="uploadFile()">上传</a>| <a href="javascript:$('#uploadify').uploadifyClearQueue()">
            取消上传</a>
    </p>
    <div id="fileQueue">
    </div>--%>
    <input type="file"  />
    <div id="upload"></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
    <%--<link href="../../Css/uploadify.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../Css/html5uploader.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
  <%--  <script type="text/javascript" src="../../Scripts/uploadFile/swfobject.js"></script>
    <script type="text/javascript" src="../../Scripts/uploadFile/jquery.uploadify.v2.1.0.js"></script>--%>
    <script type="text/javascript" src="../../Scripts/html5uploader/jquery.html5uploader.js"></script>
    <script type="text/javascript">
        $(function () {
            $('body').layout();
            $('#upload').html5uploader({
                auto: false,
                multi: true,
                removeTimeout: 9999999,
                url: '../../Handler/UploadHandler.ashx',
                onUploadStart: function () {
                    alert('开始上传');
                },
                onInit: function () {
                    alert('初始化');
                },
                onUploadComplete: function () {
                    alert('上传完成');
                }
            });
        });

//        $(function () {
//            $('body').layout();
//            $("#uploadify").uploadify({
//                uploader: '../../Scripts/uploadFile/uploadify.swf',
//                script: '../../Handler/UploadHandler.ashx',
//                cancelImg: '../../img/cancel.png',
//                folder: '_27FBA647-6B20-4FBC-8101-D167C2546F29',
//                queueID: 'fileQueue',
//                buttonImg: "../../img/selectFile.png",
//                quenueSizeLimit: 10,
//                sizeLimit: 1024 * 1024 * 20,
//                fileDesc: "只能上传World文件...",
//                fileExt: "*.doc;*.docx",
//                methos:"post",
//                auto: false,
//                multi: true
//            })
//        });

//        function uploadFile() {
//            $('#uploadify').uploadifyUpload();
//        }

    </script>
</asp:Content>
