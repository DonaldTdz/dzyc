<%@ Page validateRequest="false"  Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<DHQR.UI.Models.WeiXinArticleModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    新增或编辑
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
                </ul>
            </div>
            <div id="reply_keyword" class="r_con_wrap">
                <form id="keyword_reply_form" class="r_con_form" action="">
                    <%=Html.HiddenFor(f=>f.Id) %>
                   
                    <div class="rows">
                        <label>名称</label><span class="input"> <%=Html.TextBoxFor(f => f.Name, new { @Class="form_input",@Size="35",@notnull="notnull"})%><font class="fc_red">*</font></span>
                        <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label>标题</label><span class="input"> <%=Html.TextBoxFor(f => f.Title, new { @Class="form_input",@Size="35",@notnull="notnull"})%><font class="fc_red">*</font></span>
                        <div class="clear"></div>
                    </div>
                   <div class="rows">
                        <label>文章类型</label><span class="input">
                             <%=Html.DropDownListFor(f => f.ArticleType,(IEnumerable<SelectListItem>)ViewData["ArticlesType"], new { @Style="width:200px;" })%>
                            <font class="fc_red">*</font></span>
                        <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label>内容</label><span class="input"> 
                            <%--<%=Html.TextBoxFor(f => f.Content, new { @Class="form_input",@Size="35",@notnull="notnull"})%>--%>
                            <%--<textarea  cols="80" class="ckeditor" id="Content" name="Content" rows="10"></textarea>--%>
                             <script id="Content" type="text/plain" style="width:98%"></script>
                            <font class="fc_red">*</font></span>
                        <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label>图片地址</label><span class="input">
                            <input name="FileUpload" id="MsgFileUpload" type="file" /><input class="btn_ok" type="submit" id="upload" value="" style="cursor: pointer; background-position: 0px 0px; height: 27px; width: 118px; background-image: url(../../DHQRImages/uploadFile.jpg)" />
                            <%if(Model!=null&&!string.IsNullOrEmpty(Model.PicUrl)){ %>
                            <img src='<%=Model.PicUrl %>' alt="" style="display:block;" />
                            <%} %>
                            <%=Html.HiddenFor(f=>f.PicUrl) %></span>
                        <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label>描述</label><span class="input"><%=Html.TextAreaFor(f=>f.Description, new { @Class="form_input",@Style="height:60px;overflow:hidden;"}) %></span>
                        <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label>路劲</label><span class="input"><%=Html.TextBoxFor(f=>f.Url, new { @Class="form_input",@Size="35"}) %></span>
                        <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label>排序号</label><span class="input"> <%=Html.TextBoxFor(f=>f.ArticleSort, new { @Class="form_input",@Size="35"}) %></span>
                        <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label>
                        </label>
                        <span class="input">
                            <input type="button" class="btn_green" id="save" name="submit_button" style="cursor:pointer;" value="提交保存" /><a href="Index"
                                class="btn_gray">返回</a></span>
                        <div class="clear">
                        </div>
                    </div>
                </form>
                <div id="outputdiv"></div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8"/>
    <link href="../../DHQRCss/global.css" rel="stylesheet" type="text/css" />
    <link href="../../DHQRCss/main.css" rel="stylesheet" type="text/css" />
    <link href="../../DHQRCss/wechat.css" rel="stylesheet" type="text/css" />
    <link href="../../DHQRCss/operamasks-ui.css" rel="stylesheet" />
    <script src="../../DHQRJs/operamasks-ui.js" type="text/javascript"></script>
    <script src="../../DHQRJs/frame.js" type="text/javascript"></script>
    <script src="../../DHQRJs/global.js" type="text/javascript"></script>
    <script src="../../DHQRJs/wechat.js" type="text/javascript"></script>
    <%--<script type="text/javascript" src="../../Scripts/ckeditor/ckeditor.js"></script>--%>
    <script type="text/javascript" charset="utf-8" src="../../Scripts/ueditor-new/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../Scripts/ueditor-new/ueditor.all.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../Scripts/ueditor-new/lang/zh-cn/zh-cn.js"></script>
    <%--<script type="text/javascript" charset="utf-8" src="../../Scripts/ueditor1_4_3-utf8-net/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="../../Scripts/ueditor1_4_3-utf8-net/ueditor.all.js"></script>--%>
    <script type="text/javascript" src="../../DHQRJs/jquery.form.js"></script>
    <style type="text/css">
        body, html {
            background: url(../../DHQRImages/main/main-bg.jpg) left top fixed no-repeat;
        }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
    <script type="text/javascript">
        var ue = UE.getEditor('Content');
        var emptyGuid = '<%=Guid.Empty%>';
        var content = '<%=Model==null||Model.Content==null?"":Model.Content%>';
        $(document).ready(function () {
            ue.addListener("ready", function () {
                // editor准备好之后才可以使用       
                ue.execCommand('insertHtml', content);
            });
            wechat_obj.wechat_set();
            $("#keyword_reply_form").submit(function () {
                $(this).ajaxSubmit({
                    target: '#outputdiv',
                    type: "post",  //提交方式  
                    dataType: "json", //数据类型 
                    url: "../WeiXinArticle/UpLoadImage",
                    beforeSubmit: function (formData, jqForm, options) {
                        if (!$("#MsgFileUpload").val()) {
                            //alert("提示", "请选择上传图片！", "error");
                            return false;
                        }
                        return true;
                    },
                    error: function (result) {
                        //BLUE.MsgAlert("提示", result.Message, "error");
                    },
                    success: function (responseText, statusText) {
                        $("#upload").next("img").remove();
                        $("<img src='" + responseText + "' alt='' style='display:block;' />").insertAfter($("#upload"));
                        $("#PicUrl").val(responseText);
                        // $('.first .img').html('<a href="../images/fileupload/' + responseText + '" target="_blank"><img src="../images/fileupload/' + responseText + '"></a>');

                    }
                });
                return false;
            });
            $("#save").click(function () {
                if (system_obj.check_form($('*[notnull]'))) {
                    return false
                };
                var postData = {
                    Id: $("#Id").val(), Name: $("#Name").val(), Title: $("#Title").val(), ArticleType: $("#ArticleType").val(), PicUrl: $("#PicUrl").val(),
                    Description: $("#Description").val(), Url: $("#Url").val(), ArticleSort: $("#ArticleSort").val(), Content: ue.getContent()
                }
                var url = postData.Id == emptyGuid ? "../WeiXinArticle/Add" : "../WeiXinArticle/Edit";
                var btn = $(this);
                btn.attr('disabled', true);
                $.post(url, postData, function (result) {
                    if (result.IsSuccessful == true) {
                        window.location = '../WeiXinArticle/Index';
                    } else {
                        alert(result.OperateMsg);
                        btn.attr('disabled', false);
                    }
                });
            });
        });
        

    </script>
</asp:Content>
