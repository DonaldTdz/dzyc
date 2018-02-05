<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<WeiXinAppModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    微信授权管理
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="iframe_page">
	<div class="iframe_content">
<div class="r_nav">
	<ul>
					<li class="cur"><a href="../WeiXinApp/Index">微信授权配置</a></li>
					<li class=""><a href="../WeiXinApp/InterfaceCfg">微信接口配置</a></li>
					<li class=""><a href="../FirstIn/Index">首次关注设置</a></li>
					<li class=""><a href="../WeiXinMenu/Index">自定义菜单设置</a></li>
					<li class=""><a href="../KeyWord/Index">关键词回复</a></li>
			</ul>
</div>
<div id="wechat_set" class="r_con_wrap">
	<div class="r_con_tips">
		1. <span>订阅号并已通过微信认证</span>可使用自定义菜单。<br />
		2. <span>服务号</span>无需微信认证可直接使用自定义菜单，<span>服务号并已通过微信认证</span>可使用高级接口。<br />
		3. 在公众平台申请接口使用的<span>AppId</span>和<span>AppSecret</span>，然后填入下边表单。<br />
		4. <span>服务认证号</span>请在微信公众平台高级接口处的<span>OAuth2.0网页授权</span>设置授权回调页面域名为<span>www.microservices.com.cn</span>
	</div>
	<form id="wechat_set_form" class="r_con_form">
        <input name="Id" type="hidden" value="<%=Model==null?Guid.Empty:Model.Id%>" class="form_input"/>
        <input name="Token" type="hidden" value="<%=Model==null?null:Model.Token%>" class="form_input"/>
        <input name="WeiXinKey" type="hidden" value="<%=Model==null?null:Model.WeiXinKey%>" class="form_input"/>
        <input name="UserId" type="hidden" value="<%=Model==null?Guid.Empty:Model.UserId%>" class="form_input"/>
        <input name="CreateTime" type="hidden" value="<%=Model==null?DateTime.Now:Model.CreateTime%>" class="form_input"/>
        <input name="Url" type="hidden" value="<%=Model==null?null:Model.Url%>" class="form_input"/>

		<div class="rows">
			<label>微信原始ID</label>
			<span class="input"><input id="OriginalId" name="OriginalId" value="<%=Model.OriginalId%>" type="text" class="form_input" size="35" maxlength="15" notnull/> <font class="fc_red">*</font></span>
			<div class="clear"></div>
		</div>
		<div class="rows">
			<label>微信号</label>
			<span class="input"><input name="Name" id="Name" value="<%=Model.Name%>" type="text" class="form_input" size="35" maxlength="30" notnull/> <font class="fc_red">*</font></span>
			<div class="clear"></div>
		</div>
		<div class="rows">
			<label>公众号类型</label>
			<span class="input">
				<input type='radio' value='<%=Model.WeiXinType%>' name='WeixinType'/><span class='tips'>订阅号</span><input type='radio' value='1' name='WeiXinType'  /><span class='tips'>订阅认证号</span><input type='radio' value='2' name='WeiXinType'  /><span class='tips'>服务号</span><input type='radio' value='3' name='WeiXinType' checked /><span class='tips'>服务认证号</span>			</span>
			<div class="clear"></div>
		</div>
		<div class="rows">
			<label>公众号二维码</label>
			<span class="input">
				<span class="upload_file">
					<div>
						<div class="up_input"><input name="QrCodeUpload" id="QrCodeUpload" type="file" /></div>
						<div class="tips"></div>
						<div class="clear"></div>
					</div>
					<div class="img" id="QrCodeDetail">
                       <a href="<%=Model==null?"":Model.PicUrl %>" target="_blank">
                           <img src="<%=Model == null ? "" : Model.PicUrl %>" /></a>
                     </div>
                       <div id="ReplyImgUploadQueue" class="om-fileupload-queue">
                            </div>
                            <div class="blank12">
                            </div>
                    <%=Html.Hidden("PicUrl") %>
				</span>
			</span>
			<div class="clear"></div>
		</div>
		<div class="rows" id="AppIdRow">
			<label>AppId</label>
			<span class="input"><input name="AppId" id="AppId" value="<%=Model.AppId%>" type="text" class="form_input" size="35" maxlength="18"/></span>
			<div class="clear"></div>
		</div>
		<div class="rows" id="AppSecretRow">
			<label>AppSecret</label>
			<span class="input"><input name="AppSecret" id="AppSecret" value="<%=Model.AppSecret%>" type="text" class="form_input" size="35" maxlength="32"/></span>
			<div class="clear"></div>
		</div>
		<div class="rows" id="VoiceIdenRow">
			<label>语音关键词回复</label>
			<span class="input"><input type="checkbox" value="1" name="VoiceIden" checked /> <span class="tips">如果您需要开启语音关键词回复，请勾选此选项，开启后，系统将自动识别出语音内容并启用模糊匹配方式进行关键字回复</span></span>
			<div class="clear"></div>
		</div>
		<div class="rows" id="WechatKfRow">
			<label>微信客服系统</label>
			<span class="input"><input type="checkbox" value="1" name="WechatKf"  /> <span class="tips">如果您需要使用微信多客服系统，请勾选此选项</span></span>
			<div class="clear"></div>
		</div>
		<div class="rows">
			<label></label>
			<span class="input"><input type="submit" class="btn_ok" name="submit_button" value="提交保存" /></span>
			<div class="clear"></div>
		</div>
		<input type="hidden" name="QrCodePath" value="" />
		<input type="hidden" name="do_action" value="wechat.wechat_set"/>
	</form>
</div>	</div>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
    <link href="../../DHQRCss/frame.css" rel="stylesheet"  type='text/css'/>
    <script src="../../DHQRJs/frame.js" type="text/javascript"></script>
    <script src="../../DHQRJs/wechat.js" type="text/javascript"></script>
    <link href="../../DHQRCss/wechat.css" rel="stylesheet" type="text/css" />
    <link href="../../DHQRCss/operamasks-ui.css" rel="stylesheet" />
    <script src="../../DHQRJs/operamasks-ui.js" type="text/javascript"></script>
    <script src="../../DHQRJs/jquery.mousewheel.js"  type="text/javascript"></script>
    <script src="../../DHQRJs/jquery.jscrollpane.js"  type="text/javascript"></script>
    <script src="../../DHQRJs/material.js"  type="text/javascript"></script>
    <link href="../../DHQRCss/stores.css" rel="stylesheet" />
    <script src="../../DHQRJs/jquery.watermark-1.3.js" type="text/javascript"></script>
    <script src="../../Scripts/uploadFile/swfobject.js" type="text/javascript"></script>
    <script src="../../Scripts/uploadFile/jquery.uploadify.v2.1.0.js" type="text/javascript"></script>

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
        <script type="text/javascript">
            $(document).ready(global_obj.page_init);
            window.onresize = global_obj.page_init;
            $(document).ready(wechat_obj.wechat_set);
            $(function () {
                $("#QrCodeUpload").uploadify({
                    uploader: '../../Scripts/uploadFile/uploadify.swf',
                    script: '../../WeiXinTriggerInfo/UpLoadImage',
                    cancelImg: '../../images/fileupload/uploadify-cancel.png',
                    buttonImg: '../../images/fileupload/selectFile.jpg',
                    folder: '',
                    queueID: 'ReplyImgUploadQueue',
                    sizeLimit: 1024 * 500,
                    fileDesc: '选择上传的图片',
                    fileExt: '*.jpg;*.png;*.gif;*.jpeg;*.bmp',
                    methos: "post",
                    auto: true,
                    multi: false,
                    onComplete: ReplyImgUploadComplete,
                    onError: function (file, data, response) {
                        $('#' + file.id).find('.data').html('上传失败！');
                    }
                });
            })

            function ReplyImgUploadComplete(file, data, response, serverData) {
                var serverJson = eval("[" + serverData + "]")[0];
                if (serverJson.status == 0) {
                    $('#' + file.id).find('.data').html('上传失败！');
                    return;
                }
                $("#QrCodeDetail img").attr("src", serverJson.filepath);
                $("#QrCodeDetail a").attr("href", serverJson.filepath);
                $("#PicUrl").val(serverJson.filepath);
            }

    </script>



</asp:Content>
