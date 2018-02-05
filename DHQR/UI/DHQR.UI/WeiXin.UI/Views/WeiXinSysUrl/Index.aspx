<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    系统URL
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="iframe_page">
	<div class="iframe_content">
<div class="r_nav">
	<ul>
                    <li class=""><a href="../WeiXinPicMsgMatser/Index">图文消息管理</a></li>
                    <li class=""><a href="../WeiXinCustomUrl/Index">自定义URL</a></li>
                    <li class="cur"><a href="../WeiXinSysUrl/Index">系统URL查询</a></li>
			</ul>
</div><div id="url" class="r_con_wrap">
    <form id="add_form" class="add_form " method="post">
                <table border="0" cellpadding="5" cellspacing="0">
                    <tr>
                        <td>
                            名称
                            <input type="text" name="Name" id="Name" value="" size="25" class="form_input" notnull />
                        </td>
                        <td>
                            Url
                            <input type="text" name="Url" id="Url" value="" size="40" class="form_input" notnull />
                        </td>
                        <td>
                            排序号
                            <input type="text" name="Number" id="Number" value="" size="40" class="form_input" notnull />
                        </td>
                        <td>
                            <input type="button" class="submit" id="sub" value="添加URL" name="submit_btn" onclick="CreateOrUpdate();" />
                        </td>
                    </tr>
                </table>
                <input type="hidden" name="do_action" value="material.url" />
                <input type="hidden" name="UId" value="0" />
                <input type="hidden" name="page" value="0" />
                <input type="hidden" id="Id"/>
                <input type="hidden" id="WeiXinSysTypeId"/>
                <input type="hidden" id="WeiXinSysTypeName"/>
                <input type="hidden" id="WeiXinAppId"/>
                </form>
	<div class="type" id="stype">
	  <div class="clear"></div>
	</div>
	<table border="0" cellpadding="5" cellspacing="0" class="r_con_table">
		<thead>
			<tr>
				<td width="10%" nowrap="nowrap">序号</td>
				<td width="20%" nowrap="nowrap">名称</td>
				<td width="60%" nowrap="nowrap">Url</td>
				<td width="60%" nowrap="nowrap" class="last">操作</td>
			</tr>
		</thead>
		<tbody>
	    </tbody>
	</table>
</div>	</div>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
    <link href="../../DHQRCss/frame.css" rel="stylesheet"  type='text/css'/>
    <script src="../../DHQRJs/frame.js" type="text/javascript"></script>
    <link href="../../DHQRCss/material.css" rel="stylesheet" />
    <script src="../../DHQRJs/material.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
    <script language="javascript" type="text/javascript">
        var firstId = "";
        var WeiXinSysTypeId = "";
        var WeiXinSysTypeName = "";
        var isCreate = true;
        $(document).ready(
            frame_obj.search_form_init
            );

        $(function () {
            LoadTypes();
        });


        //加载模块类型
        function LoadTypes()
        {
            $.post("../WeiXinSysType/GetByAppId", {}, function (datas) {
                $(datas).each(function (i) {
                    if (i == 0)
                    {
                        firstId = datas[i].Id;
                        WeiXinSysTypeId = datas[i].Id;
                        WeiXinSysTypeName = datas[i].Name;
                    }
                    var tdiv = $("#stype");
                    var d = "'" + datas[i].Id + "','" + datas[i].Name+"'";
                    var ss = '<a onclick="LoadData(' + d + ')">' + datas[i].Name + '</a>';
                    tdiv.append(ss);
                });
                LoadData(firstId, WeiXinSysTypeName);
            });

        }

        //根据模块类型加载系统URL
        function LoadData(id,name)
        {
            WeiXinSysTypeId = id;
            WeiXinSysTypeName = name;
            $.post("../WeiXinSysUrl/GetByTypeId", { TypeId: id }, function (datas) {
                var tbody = $(".r_con_table").find("tbody");
                tbody.empty();
                $(datas).each(function (i) {
                    var tr = $("<tr></tr>");
                    tr.append('<td style="display:none">' + datas[i].Id + '</td>');
                    tr.append('<td style="display:none">' + datas[i].Number + '</td>');
                    tr.append('<td style="display:none">' + datas[i].WeiXinSysTypeId + '</td>');
                    tr.append('<td style="display:none">' + datas[i].WeiXinSysTypeName + '</td>');
                    tr.append('<td style="display:none">' + datas[i].WeiXinAppId + '</td>');

                    td = $('<td nowrap="nowrap"></td>');
                    td.append(i+1);
                    tr.append(td);

                    td = $('<td nowrap="nowrap"></td>');
                    td.append(datas[i].Name);
                    tr.append(td);

                    td = $('<td nowrap="nowrap"></td>');
                    td.append(datas[i].Url);
                    tr.append(td);

                    //操作
                    td = $('<td nowrap="nowrap" class="last"></td>');
                    var efuc = "SetToEdit(" + "'" + datas[i].Id + "'" + "," + "'" + datas[i].Number + "'" + "," + "'" + datas[i].Name + "'" + "," + "'" + datas[i].Url + "'" + "," + "'" + datas[i].WeiXinSysTypeId + "'" + "," + "'" + datas[i].WeiXinSysTypeName + "'" + ");"
                    var modifyATag = $('<a onclick=' + efuc + '><img src="../../DHQRImages/ico/mod.gif" align="absmiddle" alt="修改"></a>');
                    td.append(modifyATag);
                    td.append('<sapn style="display:inline-block; width:5px;"></span>');
                    var delATag = $('<a href="javascript:void(0)" onclick="delKeyword(this)" style="cursor:pointer;"><img src="../../DHQRImages/ico/del.gif" align="absmiddle" alt="删除"></a>');
                    td.append(delATag);
                    tr.append(td);

                    tbody.append(tr);
                });
            });
        }


        function delKeyword(obj) {
            if (!confirm("删除后不可恢复，继续吗？")) { return false };
            var Id = $(obj).parents("tr")[0].children[0].innerText;
            $.post("Delete", { id: Id }, function (data) {
                if (data.IsSuccessful == true) {
                    $(obj).parents("tr").remove();
                } else {
                    alert(data.OperateMsg);
                }
            }, 'json');
        }

        function CreateOrUpdate() {
            if (isCreate) {
                Create();
            }
            else {
                Update();
            }
        }

        //创建
        function Create() {
            if (global_obj.check_form($('*[notnull]'))) { return false; };
            var url = $("#Url").val();
            var name = $("#Name").val();
            var number = $("#Number").val();

            var postData = { Url: url, Name: name, Number: number, WeiXinSysTypeId: WeiXinSysTypeId, WeiXinSysTypeName: WeiXinSysTypeName };
            $.ajax({
                url: "../WeiXinSysUrl/Create",
                data: postData,
                type: "POST",
                catche: false,
                success: function (data) {
                    if (data.IsSuccessful) {
                        window.location.reload();
                    } else {
                        alert(data.OperateMsg);
                        $('#config_form input:submit').attr('disabled', false);
                    }
                }
            });
        }

        function Update() {
            if (global_obj.check_form($('*[notnull]'))) { return false; };
            var id = $("#Id").val();
            var number = $("#Number").val();
            var url = $("#Url").val();
            var name = $("#Name").val();
            var number = $("#Number").val();
            var weiXinSysTypeId = $("#WeiXinSysTypeId").val();
            var weiXinSysTypeName = $("#WeiXinSysTypeName").val();

            var postData = {
                Id: id, Number: number, Url: url, Name: name, Number: number, WeiXinSysTypeId: weiXinSysTypeId,
                WeiXinSysTypeName: weiXinSysTypeName
            };
            $.ajax({
                url: "../WeiXinSysUrl/Edit",
                data: postData,
                type: "POST",
                catche: false,
                success: function (data) {
                    if (data.IsSuccessful) {
                        window.location.reload();
                    } else {
                        alert(data.OperateMsg);
                        $('#config_form input:submit').attr('disabled', false);
                    }
                }
            });

        }

        //设置编辑
        function SetToEdit(Id, Number, Name, Url, WeiXinSysTypeId, WeiXinSysTypeName) {
            isCreate = false;
            $("#Id").val(Id);
            $("#Number").val(Number);
            $("#Name").val(Name);
            $("#Url").val(Url);
            $("#WeiXinSysTypeId").val(WeiXinSysTypeId);
            $("#WeiXinSysTypeName").val(WeiXinSysTypeName);
            $("#sub").text("更新URL");
            $("#sub").val("更新URL");
        }


    </script>
</asp:Content>
