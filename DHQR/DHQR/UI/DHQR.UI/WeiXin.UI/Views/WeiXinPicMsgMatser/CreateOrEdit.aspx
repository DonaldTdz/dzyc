<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<WeiXinPicMsgMatserModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    新增或编辑微信图文信息
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
                    <li class="cur"><a href="./?m=material&a=index">图文素材管理</a></li>
                    <li class=""><a href="./?m=material&a=url">自定义URL</a></li>
                    <li class=""><a href="./?m=material&a=url_list">系统URL查询</a></li>
                </ul>
            </div>
            <div id="material" class="r_con_wrap">
                <form id="material_form" action="../WeiXinPicMsgMatser/UpLoadImage">
                    <% if (Model.MaterialType == 1)
                       { %>
                    <div class="m_lefter multi">
                        <div class="time"><%=Model.CreateTimeStr %></div>
                        <div class="first" id="multi_msg_0">
                            <div class="info">
                                <div class="img">
                                    <%if (!string.IsNullOrEmpty(Model.PicUrl))
                                      { %>
                                    <img src="<%=Model.PicUrl %>" alt="" /><%}
                                      else
                                      { %>
                                封面图片
                                <%} %>
                                </div>
                                <div class="title">
                                    <%if (!string.IsNullOrEmpty(Model.Title))
                                      { %><%=Model.Title %><%}
                                      else
                                      { %>消息标题<%} %>
                                </div>
                            </div>
                            <div class="control">
                                <a href="#mod">
                                    <img src="../../DHQRImages/ico/mod.gif" alt="编辑" /></a>
                            </div>
                            <input type="hidden" name="Title[]" value='<%=Model.Title %>' />
                            <input type="hidden" name="Url[]" value="<%=Model.Url %>" />
                            <input type="hidden" name="ImgPath[]" value='<%=Model.PicUrl %>' />
                        </div>
                        <%
                                      int count = 1;
                                      if (Model.MsgDetails != null && Model.MaterialType == 1)
                                      {
                                          foreach (var item in Model.MsgDetails)
                                          {
                                              string id = "multi_msg_" + count++;
                        %>
                        <div class="list" id='<%=id%>'>
                            <div class="info">
                                <div class="title"><%=item.Title%></div>
                                <div class="img">
                                    <a href="<%=item.PicUrl%>" target="_blank">
                                        <img src="<%=item.PicUrl%>" alt="" /></a>
                                </div>
                            </div>
                            <div class="control">
                                <a href="#mod">
                                    <img src="../../DHQRImages/ico/mod.gif" alt="编辑" /></a>
                                <a href="#del">
                                    <img src="../../DHQRImages/ico/del.gif" alt="删除" /></a>
                            </div>
                            <input type="hidden" name="DetailtId" value="<%=item.Id%>" />
                            <input type="hidden" name="Title[]" value="<%=item.Title%>" />
                            <input type="hidden" name="Url[]" value="<%=item.Url%>" />
                            <input type="hidden" name="ImgPath[]" value="<%=item.PicUrl%>" />
                        </div>
                        <%}
                                                     } %>

                        <div class="list" id="<%="multi_msg_"+(Model.MsgDetails==null?1:Model.MsgDetails.Count()+1) %>">
                            <div class="info">
                                <div class="title">标题</div>
                                <div class="img">缩略图</div>
                            </div>
                            <div class="control">
                                <a href="#mod">
                                    <img src="../../DHQRImages/ico/mod.gif" alt="编辑" /></a>
                                <a href="#del">
                                    <img src="../../DHQRImages/ico/del.gif" alt="删除" /></a>
                            </div>
                            <input type="hidden" name="DetailtId" value="" />
                            <input type="hidden" name="Title[]" value="" />
                            <input type="hidden" name="Url[]" value="" />
                            <input type="hidden" name="ImgPath[]" value="" />
                        </div>
                        <div class="add">
                            <a href="#add">
                                <img src="../../WeiXinImages/ico/add.gif" align="absmiddle" />
                                增加一条</a>
                        </div>

                    </div>
                    <%}
                       else
                       { %>
                    <div class="m_lefter one">
                        <div class="title">
                            <%if (!string.IsNullOrEmpty(Model.Title))
                              { %><%=Model.Title %><%}
                              else
                              { %>消息标题<%} %>
                        </div>
                        <div><%=Model.CreateTimeStr %></div>
                        <div class="img" id="MsgImgDetail">
                            <%if (!string.IsNullOrEmpty(Model.PicUrl))
                              { %>
                            <img src="<%=Model.PicUrl %>" alt="" /><%}
                              else
                              { %>
                                封面图片
                                <%} %>
                        </div>
                        <div class="txt"><%=Model.Description %></div>
                        <input type="hidden" name="Title[]" value='<%=Model.Title %>' />
                        <input type="hidden" name="Url[]" value="<%=Model.Url %>" />
                        <input type="hidden" name="ImgPath[]" value='<%=Model.PicUrl %>' />
                    </div>
                    <%} %>
                    <div class="m_righter">
                        <div class="mod_form">
                            <div class="jt">
                                <img src="http://static.ptweixin.com/images/ico/jt_b.gif" alt="" />
                            </div>
                            <div class="m_form">
                                <span class="fc_red">*</span> 标题<br />
                                <div class="input">
                                    <input name="inputTitle" value="<%=Model.Title %>" type="text" notnull />
                                </div>
                                <div class="blank9"></div>
                                <span class="fc_red">*</span> 封面图片 <span class="tips">图片尺寸建议：<span class="big_img_size_tips">360*200px</span></span><br />
                                <div class="blank6"></div>
                                <div>
                                    <input name="FileUpload" id="MsgFileUpload" type="file" /><input class="btn_ok" type="submit" id="upload" value="" style="cursor: pointer; background-position: 0px 0px; height: 27px; width: 118px; background-image: url(../../DHQRImages/uploadFile.jpg)" />
                                </div>

                                <div class="blank3"></div>
                                <%if (Model.MaterialType == 0)
                                  {%>
                                <div class="blank12"></div>
                                简短介绍<br />
                                <div>
                                    <textarea name="BriefDescription"><%=Model.Description %></textarea>
                                </div>
                                <%} %>
                                <span></span>链接页面:
							<div class="input"><%=Html.DropDownList("inputUrl",(IEnumerable<SelectListItem>)ViewData["url"]) %></div>
                            </div>
                        </div>
                    </div>
                    <div class="clear"></div>
                    <div class="button">
                        <input type="button" id="saveData" class="btn_ok" name="submit_button" value="提交保存" /><a href="../WeiXinPicMsgMatser/Index" class="btn_cancel">返回</a>
                    </div>
                    <input type="hidden" name="Id" value="<%=Model.Id %>" />
                    <input type="hidden" name="MaterialType" value='<%=Model.MaterialType %>' />
                    <input type="hidden" name="do_action" value="material.material_edit" />
                </form>
            </div>
        </div>
        <div id="outputdiv"></div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
    <link href="../../DHQRCss/global.css" rel='stylesheet' type='text/css' />
    <link href="../../DHQRCss/frame.css" rel='stylesheet' type='text/css' />
    <script type='text/javascript' src="../../DHQRJs/jquery-1.7.2.min.js"></script>
    <script type='text/javascript' src="../../DHQRJs/frame.js"></script>
    <link href="../../DHQRCss/material.css" rel='stylesheet' type='text/css' />
    <script type='text/javascript' src="../../DHQRJs/material.js"></script>
    <link href="../../DHQRCss/operamasks-ui.css" rel='stylesheet' type='text/css' />
    <script type='text/javascript' src="../../DHQRJs/operamasks-ui.min.js"></script>
    <script type="text/javascript" src="../../DHQRJs/jquery.form.js"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
    <script type="text/javascript">
        var materialType = '<%=Model.MaterialType%>';
        $(document).ready(function () {
            frame_obj.search_form_init();
            if (materialType == 0) { material_obj.material_one_init(); } else {
                material_obj.material_multi_init();
            }
            $("#saveData").click(function () {
                if (global_obj.check_form($('*[notnull]'))) { return false };
                var detailData = [];
                $(".multi .list").each(function (obj) {
                    var thisObj = $(this);
                    if (!$("input[name=ImgPath\\[\\]]", thisObj).val()) {
                        return true;
                    }
                    var item = '{Id:"' + ($("input[name=DetailtId]", thisObj).val() ? $("input[name=DetailtId]", thisObj).val() : "<%=Guid.Empty%>") + '", Title:"' + $("input[name=Title\\[\\]]", thisObj).val() +
                        '",Url:"' + $("input[name=Url\\[\\]]", thisObj).val() + '", PicUrl:"' + $("input[name=ImgPath\\[\\]]", thisObj).val() + '",Description:"' +<%=string.Empty%>
                    '"}';

                    detailData[obj] = item;
                });
                var postdata = {};
                if (materialType == 0) {
                    var form = $("#material_form");
                    postdata = {
                        detailsStr: '[]', Id: $("input[name=Id]").val(), Title: $("input[name=Title\\[\\]]", form).val(), MaterialType: $("input[name=MaterialType]").val(),
                        PicUrl: $("input[name=ImgPath\\[\\]]", form).val(), Url: $("input[name=Url\\[\\]]", form).val(), Description: $("textarea[name=BriefDescription]", form).val()
                    }
                } else {
                    postdata = {
                        detailsStr: "[" + detailData.join(",") + "]", Id: $("input[name=Id]").val(), Title: $("input[name=Title\\[\\]]", masterDic).val(), MaterialType: $("input[name=MaterialType]").val(),
                        PicUrl: $("input[name=ImgPath\\[\\]]", masterDic).val(), Url: $("input[name=Url\\[\\]]", masterDic).val(), Description: $("textarea[name=BriefDescription]", masterDic).val()
                    };
                }
                var masterDic = $(".multi .first");
                $.post("../WeiXinPicMsgMatser/SaveData",postdata, function (result) {
                    if (result.IsSuccessful) {
                        window.location = "../WeiXinPicMsgMatser/Index";
                    } else {
                        alert(resut.OperateMsg);
                    }
                });
            });
            //$("#MsgFileUpload").css("display", "block");
        });
    </script>
    <script type="text/javascript">

        $(function () {

            $("#material_form").submit(function () {
                $(this).ajaxSubmit({
                    target: '#outputdiv',
                    type:"post",  //提交方式  
                    dataType: "json", //数据类型 
                    url:"../WeiXinPicMsgMatser/UpLoadImage",
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
                        if (materialType == 0) {
                            $("#material_form .img").html('<a href="' + responseText + '" target="_blank"><img src="' + responseText + '"></a>');
                            $('#material_form input[name=ImgPath\\[\\]]').val(responseText);

                        } else {
                            var id = $('.multi').data('cur_id');
                            $(id + " .info .img").html('<a href="' + responseText + '" target="_blank"><img src="' + responseText + '"></a>');
                            $(id + " input[name=ImgPath\\[\\]]").val(responseText);
                        }
                        // $('.first .img').html('<a href="../images/fileupload/' + responseText + '" target="_blank"><img src="../images/fileupload/' + responseText + '"></a>');

                    }
                });
                return false;
            });
        });

    </script>
</asp:Content>
