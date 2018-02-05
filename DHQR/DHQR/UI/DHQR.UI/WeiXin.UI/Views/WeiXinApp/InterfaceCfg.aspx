<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<WeiXinAppModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    微信接口配置
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="iframe_page">
	<div class="iframe_content">
      <div class="r_nav">
	        <ul>
					<li class=""><a href="../WeiXinApp/Index">微信授权配置</a></li>
					<li class="cur"><a href="../WeiXinApp/InterfaceCfg">微信接口配置</a></li>
					<li class=""><a href="../FirstIn/Index">首次关注设置</a></li>
					<li class=""><a href="../WeiXinMenu/Index">自定义菜单设置</a></li>
					<li class=""><a href="../KeyWord/Index">关键词回复</a></li>
			</ul>
      </div>
      <div id="token" class="r_con_wrap">
			<div class="r_con_tips">你已成功绑定微信公众平台，绑定信息：</div>
		<div class="r_con_form">
			<div class="rows">
				<label>帐号名称:</label>
				<span class="input"><span class="tips"><%=Model==null?"": Model.Name%></span></span>
				<div class="clear"></div>
			</div>
			<div class="rows">
				<label>接口URL:</label>
				<span class="input"><span class="tips"><%=Model==null?"": Model.Url%></span></span>
				<div class="clear"></div>
			</div>
			<div class="rows">
				<label>接口Token:</label>
				<span class="input"><span class="tips"><%=Model==null?"": Model.Token%></span></span>
				<div class="clear"></div>
			</div>
		</div>
	</div>	
	</div>
</div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
    <script src="../../DHQRJs/frame.js" type="text/javascript"></script>
    <link href="../../DHQRCss/frame.css" rel="stylesheet" type="text/css" />
    <link href="../../DHQRCss/wechat.css" rel="stylesheet" />
    <script src="../../DHQRJs/wechat.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
  <script language="javascript" type="text/javascript">
    $(document).ready(wechat_obj.set_token_init);

  </script>
</asp:Content>
