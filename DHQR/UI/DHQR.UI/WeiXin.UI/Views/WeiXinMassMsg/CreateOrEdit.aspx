<%@ Page Title="" Language="C#" ValidateRequest="false" EnableEventValidation="false" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<WeiXinMassGroupModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    新增或编辑微信图文信息
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
                    <li class="cur"><a href="#">图文素材</a></li>
                </ul>
            </div>
            <div id="material" class="r_con_wrap" style="min-height:1200px;">
                <form id="material_form" action="../WeiXinPicMsgMatser/UpLoadImage">
                    <div class="m_lefter multi">
                        <div class="time"><%=Model.MsgHeader.CreateTimeStr %></div>
                        <div class="first" id="multi_msg_0">
                            <div class="info">
                                <div class="img">
                                    <%if (!string.IsNullOrEmpty(Model.MsgHeader.pic_url))
                                      { %>
                                    <img src="<%=Model.MsgHeader.pic_url %>" alt="" /><%}
                                      else
                                      { %>
                                封面图片
                                <%} %>
                                </div>
                                <div class="title">
                                    <%if (!string.IsNullOrEmpty(Model.MsgHeader.title))
                                      { %><%=Model.MsgHeader.title %><%}
                                      else
                                      { %>消息标题<%} %>
                                </div>

                            </div>
                            <div class="control">
                                <a href="#mod">
                                    <img src="../../DHQRImages/ico/mod.gif" alt="编辑" /></a>
                            </div>
                            <input type="hidden" name="Id[]" value='<%=Model.MsgHeader.Id %>' />
                            <input type="hidden" name="title[]" value='<%=Model.MsgHeader.title %>' />
                            <input type="hidden" name="author[]" value='<%=Model.MsgHeader.author %>' />
                            <input type="hidden" name="digest[]" value='<%=Model.MsgHeader.digest %>' />
                            <%--<input type="hidden" name="content[]" value='<%=Model.MsgHeader.content %>' />--%>
                            <input type="hidden" name="content_source_url[]" value="<%=Model.MsgHeader.content_source_url %>" />
                            <input type="hidden" name="ImgPath[]" value='<%=Model.MsgHeader.pic_url %>' />

                        </div>
                        <%
                                      int count = 1;
                                      if (Model != null)
                                      {
                                          foreach (var item in Model.MsgDetails)
                                          {
                                              string id = "multi_msg_" + count++;
                        %>
                        <div class="list" id='<%=id%>'>
                            <div class="info">
                                <div class="title"><%=item.title%></div>
                                <div class="img">
                                    <a href="<%=item.pic_url%>" target="_blank">
                                        <img src="<%=item.pic_url%>" alt="" /></a>
                                </div>
                            </div>
                            <div class="control">
                                <a href="#mod">
                                    <img src="../../DHQRImages/ico/mod.gif" alt="编辑" /></a>
                                <a href="#del">
                                    <img src="../../DHQRImages/ico/del.gif" alt="删除" /></a>
                            </div>
                            <input type="hidden" name="Id" value="<%=item.Id%>" />
                            <input type="hidden" name="title[]" value="<%=item.title%>" />
                            <input type="hidden" name="content_source_url[]" value="<%=item.content_source_url%>" />
                            <input type="hidden" name="author[]" value='<%=item.author %>' />
                            <input type="hidden" name="digest[]" value='<%=item.digest %>' />
                            <input type="hidden" name="content[]" value='<%=item.content %>' />
                            <input type="hidden" name="ImgPath[]" value="<%=item.pic_url%>" />
                        </div>
                        <%}
                                      }%>

                        <div class="list" id='<%="multi_msg_"+(Model.MsgDetails==null?1:Model.MsgDetails.Count()+1) %>'>
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
                            <input type="hidden" name="Id" value="" />
                            <input type="hidden" name="title[]" value="" />
                            <input type="hidden" name="author[]" value="" />
                            <input type="hidden" name="digest[]" value="" />
                            <input type="hidden" name="content[]" value="" />
                            <input type="hidden" name="content_source_url[]" value="" />
                            <input type="hidden" name="ImgPath[]" value="" />
                        </div>
                        <div class="add">
                            <a href="#add">
                                <img src="../../DHQRImages/ico/add.gif" align="absmiddle" />
                                增加一条</a>
                        </div>

                    </div>
                    <div class="m_righter">
                        <div class="mod_form">
                            <div class="jt">
                                <img src="http://static.ptweixin.com/images/ico/jt_b.gif" alt="" />
                            </div>
                            <div class="m_form">
                                <span class="fc_red">*</span> 标题<br />
                                <div class="input">
                                    <input name="inputTitle" value="<%=Model.MsgHeader.title %>" type="text" notnull />
                                </div>
                                <span></span> 作者<br />
                                <div class="input">
                                    <input name="inputAuthor" value="<%=Model.MsgHeader.author %>" type="text"/>
                                </div>
                                <span></span> 摘要<br />
                                <div class="input">
                                    <input name="inputDigest" value="<%=Model.MsgHeader.digest %>" type="text"/>
                                </div>
                                <div class="blank9"></div>
                                <span class="fc_red">*</span> 封面图片 <span class="tips">图片尺寸建议：<span class="big_img_size_tips">900像素 * 500像素</span></span><br />
                                <div class="blank6"></div>
                                <div>
                                    <input name="FileUpload" id="MsgFileUpload" type="file" /><input class="btn_ok" type="submit" id="upload" value="" style="cursor: pointer; background-position: 0px 0px; height: 27px; width: 118px; background-image: url(../../DHQRImages/uploadFile.jpg)" />
                                </div>
                               <%-- <div class="blank12"></div>
                                详细内容<br />
                                <div>
                                    <textarea name="content" id="content" onkeyup="setAreaValue();" ></textarea>
                                </div>--%>
                                <span></span>链接页面:
							<div class="input">
                                <%=Html.DropDownList("inputUrl",(IEnumerable<SelectListItem>)ViewData["url"])%> 

							</div>
                            </div>
                        </div>
                    </div>
                    <div class="clear"></div>
                    <div class="button">
                        <input type="button" id="saveData" class="btn_ok" style="width:30%;" name="submit_button" onclick="SaveData(false);" value="保存" />
                        <input type="button" id="saveSend" class="btn_ok" style="width:30%;" value="保存并群发" onclick="SaveData(true);" />
                        <%--<input type="button" id="Button1" class="btn_ok" name="submit_button" value="返回" />--%>
                        <a href="../WeiXinMassMsg/SourceManage" class="btn_cancel">返回</a>
                    </div>
                    <%--<input type="hidden" name="Id" value="<%=Model.MsgHeader.Id%>" />--%>
                    <input type="hidden" name="do_action" value="material.material_edit" />
                </form>
            </div>
        </div>
        <div id="outputdiv"></div>
    </div>
    <%=Html.Hidden("target")%>
    <%=Html.Hidden("groupid")%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
    <link href="../../DHQRCss/global.css" rel='stylesheet' type='text/css' />
    <link href="../../DHQRCss/frame.css" rel='stylesheet' type='text/css' />
    <script type='text/javascript' src="../../DHQRJs/jquery-1.7.2.min.js"></script>
    <script type='text/javascript' src="../../DHQRJs/frame.js"></script>
    <link href="../../DHQRCss/material.css" rel='stylesheet' type='text/css' />
    <script type='text/javascript' src="../../DHQRJs/massmsg.js"></script>
    <link href="../../DHQRCss/operamasks-ui.css" rel='stylesheet' type='text/css' />
    <script type='text/javascript' src="../../DHQRJs/operamasks-ui.min.js"></script>
    <script type="text/javascript" src="../../DHQRJs/jquery.form.js"></script>
    <script src="../../Scripts/ckeditor/ckeditor.js"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            frame_obj.search_form_init();        
            material_obj.material_multi_init();
            
        });
        function setAreaValue() {
            var cur_id = $('.multi').data('cur_id');
            $(cur_id + ' input[name=content\\[\\]]').val($(this).val());
        }
    </script>
    <script type="text/javascript">
       // CKEDITOR.replace("content",
       //{
       //   width: 390, height: 75
       //});
       // CKEDITOR.instances["content"].on("instanceReady", function () {
       //     //set keyup event  
       //     this.document.on("keyup", function () {
       //         var cur_id = $('.multi').data('cur_id');
       //         var currentValue = CKEDITOR.instances.content.getData();
       //         $(cur_id + ' input[name=content\\[\\]]').val(currentValue);

       //     });
       //     //and click event  
       //     //this.document.on("click", setAreaValue);
       //     //and select event  
       //     //this.document.on("select", setAreaValue);
       // });
</script>
    <script type="text/javascript">

        $(function () {
            $("#material_form").submit(function () {
                $(this).ajaxSubmit({
                    target: '#outputdiv',
                    type: "post",  //提交方式  
                    dataType: "json", //数据类型 
                    url: "../WeiXinPicMsgMatser/UpLoadImage",
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
                            var id = $('.multi').data('cur_id');
                            $(id + " .info .img").html('<a href="' + responseText + '" target="_blank"><img src="' + responseText + '"></a>');
                            $(id + " input[name=ImgPath\\[\\]]").val(responseText);
                        // $('.first .img').html('<a href="../images/fileupload/' + responseText + '" target="_blank"><img src="../images/fileupload/' + responseText + '"></a>');

                    }
                });
                return false;
            });
        });


        function SaveData(needSend)
        {
            if (global_obj.check_form($('*[notnull]'))) { return false };
            var detailData = [];
            var groupid = $("#groupid").val();
            var target = $("#target").val();
            $(".multi .list").each(function (obj) {
                var thisObj = $(this);
                if (!$("input[name=ImgPath\\[\\]]", thisObj).val()) {
                    return true;
                }
                var item = '{Id:"' + ($("input[name=Id]", thisObj).val() ? $("input[name=Id]", thisObj).val() : "<%=Guid.Empty%>") + '", title:"' + $("input[name=title\\[\\]]", thisObj).val() +
                        '",content_source_url:"' + $("input[name=content_source_url\\[\\]]", thisObj).val() + '", pic_url:"' + $("input[name=ImgPath\\[\\]]", thisObj).val() 
            //+ '",content:"' + $("input[name=content\\[\\]]", thisObj).val()
            + '",author:"' + $("input[name=author\\[\\]]", thisObj).val() + '",digest:"' + $("input[name=digest\\[\\]]", thisObj).val()
                    + '"}';

                    detailData[obj] = item;
                });
                var masterDic = $(".multi .first");
                var postdata = {};
                postdata = {
                    detailsStr: "[" + detailData.join(",") + "]", Id: $("input[name=Id\\[\\]]", masterDic).val(), title: $("input[name=title\\[\\]]", masterDic).val(),//content: $("input[name=content\\[\\]]", masterDic).val(),
                    pic_url: $("input[name=ImgPath\\[\\]]", masterDic).val(), content_source_url: $("input[name=content_source_url\\[\\]]", masterDic).val(), digest: $("input[name=digest\\[\\]]", masterDic).val(),
                    author: $("input[name=author\\[\\]]", masterDic).val(), needSend: needSend,groupid:groupid,target:target
                };
                $.post("../WeiXinMassMsg/SaveData", postdata, function (result) {
                    if (result.IsSuccessful) {
                        window.location = "../WeiXinMassMsg/SourceManage";                       
                        //$('#add_form input:submit').attr('disabled', false);
                    } else {
                        alert(resut.OperateMsg);
                    }
                });
        
        }

       
    </script>
</asp:Content>
