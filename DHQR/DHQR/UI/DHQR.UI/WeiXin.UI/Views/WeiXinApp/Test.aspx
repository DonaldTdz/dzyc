<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/WeiXin.Master" Inherits="System.Web.Mvc.ViewPage<WeiXinAppModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    系统URL
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="iframe_page">
	<div class="iframe_content">
<div class="r_nav">
	<ul>
                    <li class=""><a href="../SourceMaterial/Index">图文消息管理</a></li>
                    <li class="cur"><a href="../WeiXinCustomUrl/Index">自定义URL</a></li>
                    <li class=""><a href="../WeiXinUrl/Index">系统URL查询</a></li>
			</ul>
</div><div id="url" class="r_con_wrap">
	<div class="type">
                   
					<a href="?m=material&a=url_list&type=0" class="cur">系统</a>
					<a href="?m=material&a=url_list&type=1" class="">微官网</a>
					<a href="?m=material&a=url_list&type=2" class="">微商城</a>
					<a href="?m=material&a=url_list&type=3" class="">微信墙</a>
					<a href="?m=material&a=url_list&type=4" class="">微相册</a>
					<a href="?m=material&a=url_list&type=5" class="">节日贺卡</a>
					<a href="?m=material&a=url_list&type=6" class="">一战到底</a>
					<a href="?m=material&a=url_list&type=7" class="">在线预约</a>
					<a href="?m=material&a=url_list&type=8" class="">微邀约</a>
					<a href="?m=material&a=url_list&type=100" class="">微婚庆</a>
					<a href="?m=material&a=url_list&type=101" class="">微餐饮</a>
					<a href="?m=material&a=url_list&type=102" class="">微房产</a>
					<a href="?m=material&a=url_list&type=103" class="">微酒店</a>
					<a href="?m=material&a=url_list&type=104" class="">微医疗</a>
					<a href="?m=material&a=url_list&type=105" class="">微汽车</a>
		        <div class="clear"></div>
	</div>
	<table border="0" cellpadding="5" cellspacing="0" class="r_con_table">
		<thead>
			<tr>
				<td width="10%" nowrap="nowrap">序号</td>
				<td width="20%" nowrap="nowrap">名称</td>
				<td width="60%" nowrap="nowrap" class="last">Url</td>
			</tr>
		</thead>
		<tbody>
								<tr>
						<td nowrap="nowrap">1</td>
						<td nowrap="nowrap">微官网</td>
						<td nowrap="nowrap" class="left last">
						http://www.ptweixin.com/api/7302ef4dd6/web/						</td>
					</tr>
									<tr>
						<td nowrap="nowrap">2</td>
						<td nowrap="nowrap">微商城</td>
						<td nowrap="nowrap" class="left last">
						http://www.ptweixin.com/api/7302ef4dd6/shop/						</td>
					</tr>
									<tr>
						<td nowrap="nowrap">3</td>
						<td nowrap="nowrap">会员中心</td>
						<td nowrap="nowrap" class="left last">
						http://www.ptweixin.com/api/7302ef4dd6/user/						</td>
					</tr>
									<tr>
						<td nowrap="nowrap">4</td>
						<td nowrap="nowrap">在线预约</td>
						<td nowrap="nowrap" class="left last">
						http://www.ptweixin.com/api/7302ef4dd6/reserve/						</td>
					</tr>
									<tr>
						<td nowrap="nowrap">5</td>
						<td nowrap="nowrap">微团购</td>
						<td nowrap="nowrap" class="left last">
						http://www.ptweixin.com/api/7302ef4dd6/tuan/						</td>
					</tr>
									<tr>
						<td nowrap="nowrap">6</td>
						<td nowrap="nowrap">微调研</td>
						<td nowrap="nowrap" class="left last">
						http://www.ptweixin.com/api/7302ef4dd6/survey/						</td>
					</tr>
									<tr>
						<td nowrap="nowrap">7</td>
						<td nowrap="nowrap">微相册</td>
						<td nowrap="nowrap" class="left last">
						http://www.ptweixin.com/api/7302ef4dd6/albums/						</td>
					</tr>
									<tr>
						<td nowrap="nowrap">8</td>
						<td nowrap="nowrap">360°全景图</td>
						<td nowrap="nowrap" class="left last">
						http://www.ptweixin.com/api/7302ef4dd6/panoramic/						</td>
					</tr>
									<tr>
						<td nowrap="nowrap">9</td>
						<td nowrap="nowrap">微吧</td>
						<td nowrap="nowrap" class="left last">
						http://www.ptweixin.com/api/7302ef4dd6/microbar/						</td>
					</tr>
									<tr>
						<td nowrap="nowrap">10</td>
						<td nowrap="nowrap">微秒杀</td>
						<td nowrap="nowrap" class="left last">
						http://www.ptweixin.com/api/7302ef4dd6/spike/						</td>
					</tr>
									<tr>
						<td nowrap="nowrap">11</td>
						<td nowrap="nowrap">微邀约</td>
						<td nowrap="nowrap" class="left last">
						http://www.ptweixin.com/api/7302ef4dd6/invite/						</td>
					</tr>
									<tr>
						<td nowrap="nowrap">12</td>
						<td nowrap="nowrap">微投票</td>
						<td nowrap="nowrap" class="left last">
						http://www.ptweixin.com/api/7302ef4dd6/vote/						</td>
					</tr>
									<tr>
						<td nowrap="nowrap">13</td>
						<td nowrap="nowrap">LBS门店定位</td>
						<td nowrap="nowrap" class="left last">
						http://www.ptweixin.com/api/7302ef4dd6/stores/						</td>
					</tr>
									<tr>
						<td nowrap="nowrap">14</td>
						<td nowrap="nowrap">刮刮卡</td>
						<td nowrap="nowrap" class="left last">
						http://www.ptweixin.com/api/7302ef4dd6/scratch/						</td>
					</tr>
									<tr>
						<td nowrap="nowrap">15</td>
						<td nowrap="nowrap">水果达人</td>
						<td nowrap="nowrap" class="left last">
						http://www.ptweixin.com/api/7302ef4dd6/fruit/						</td>
					</tr>
									<tr>
						<td nowrap="nowrap">16</td>
						<td nowrap="nowrap">欢乐大转盘</td>
						<td nowrap="nowrap" class="left last">
						http://www.ptweixin.com/api/7302ef4dd6/turntable/						</td>
					</tr>
									<tr>
						<td nowrap="nowrap">17</td>
						<td nowrap="nowrap">一战到底</td>
						<td nowrap="nowrap" class="left last">
						http://www.ptweixin.com/api/7302ef4dd6/battle/						</td>
					</tr>
						</tbody>
	</table>
</div>	</div>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
    <link href="../../WeiXinCss/frame.css" rel="stylesheet"  type='text/css'/>
    <script src="../../WeiXinJs/frame.js" type="text/javascript"></script>
    <link href="../../WeiXinCss/material.css" rel="stylesheet" />
    <script src="../../WeiXinJs/material.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
    <script language="javascript" type="text/javascript">

        $(document).ready(frame_obj.search_form_init);


    </script>
</asp:Content>
