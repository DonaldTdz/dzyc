<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<DHQR.UI.Models.DistDateConfigModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    配送日期配置
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
                </ul>
            </div>
            <div id="reply_keyword" class="r_con_wrap">
                <form id="material_form" class="r_con_form" action="../AppVersion/UpLoadImage">
                    <%=Html.HiddenFor(f=>f.Id) %>
                    <input type="hidden" id="today" value="<%=DateTime.Now.ToString("yyyy-MM-dd")%>"/>
                   

                    <div class="rows">
                        <label>系统配送日期</label>
                        <span class="input"> 
                      <input type="text" name="SysDistDate" id="SysDistDate" class="form_input" style="margin: 0px; padding: 0px; width: 150px;" value="<%=Model.SysDistDate%>" />
                            <font class="fc_red">*</font></span>
                        <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label>实际配送日期</label>
                        <span class="input"> 
                      <input type="text" name="RealDistDate" id="RealDistDate" class="form_input" style="margin: 0px; padding: 0px; width: 150px;" value="<%=Model.RealDistDate%>" />
                            <font class="fc_red">*</font></span>
                        <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label>配送日期差</label><span class="input"><%=Html.TextBoxFor(f=>f.Value, new { @Class="form_input",@Size="35",@notnull="notnull",@readonly="readonly"}) %><font class="fc_red">*</font></span>
                        <div class="clear"></div>
                    </div>
                    <div class="rows">
                        <label>
                        </label>
                        <span class="input">
                            <input type="button" class="btn_green" id="save" name="submit_button" style="cursor:pointer;" value="提交保存" /></span>
                        <div class="clear">
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
    <link href="../../Content/JqueryUi/jquery-ui-1.9.2.custom.min.css" rel="stylesheet" />
    <script src="../../Scripts/jqueryUi/jquery-ui-1.9.2.custom.js" type="text/javascript"></script>
    <script src="../../Scripts/jqueryUi/timepicker.js" type="text/javascript"></script>
    <script src="../../Scripts/jqueryUi/ui.datepicker-zh-CN.js" type="text/javascript"></script>

    <link href="../../DHQRCss/global.css" rel="stylesheet" type="text/css" />
    <link href="../../DHQRCss/main.css" rel="stylesheet" type="text/css" />
    <link href="../../DHQRCss/wechat.css" rel="stylesheet" type="text/css" />
    <link href="../../DHQRCss/operamasks-ui.css" rel="stylesheet" />
    <script src="../../DHQRJs/operamasks-ui.js" type="text/javascript"></script>
    <script src="../../DHQRJs/frame.js" type="text/javascript"></script>
    <script src="../../DHQRJs/global.js" type="text/javascript"></script>
    <script src="../../DHQRJs/wechat.js" type="text/javascript"></script>

    <link href="../../DHQRCss/frame.css" rel='stylesheet' type='text/css' />
    <link href="../../DHQRCss/material.css" rel='stylesheet' type='text/css' />
    <script type='text/javascript' src="../../DHQRJs/material.js"></script>
    <link href="../../DHQRCss/operamasks-ui.css" rel='stylesheet' type='text/css' />
    <script type='text/javascript' src="../../DHQRJs/operamasks-ui.min.js"></script>
    <script type="text/javascript" src="../../DHQRJs/jquery.form.js"></script>
    <style type="text/css">
        body, html {
            background: url(../../DHQRImages/main/main-bg.jpg) left top fixed no-repeat;
        }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">

    <script type="text/javascript">
        var emptyGuid = '<%=Guid.Empty%>';
        $(document).ready(function () {
            //frame_obj.search_form_init();
            wechat_obj.wechat_set();
            initDate();
            $("#save").click(function () {
                if (system_obj.check_form($('*[notnull]'))) {
                    return false
                };
                var postData = {
                    Id: $("#Id").val(), Value: $("#Value").val(), RealDistDate: $("#RealDistDate").val(),
                    SysDistDate: $("#SysDistDate").val()
                }
                var url = "../Config/SaveDistDateConfig";
                var btn = $(this);
                btn.attr('disabled', true);
                $.post(url, postData, function (result) {
                    if (result.IsSuccessful == true) {
                        window.location = '../Config/DistDate';
                    } else {
                        alert(result.OperateMsg);
                        btn.attr('disabled', false);
                    }
                });
            });
        });

        function initDate() {
            $('#SysDistDate').datepicker({
                format: 'yyyy-MM-dd',
                //weekStart: 1,
                autoclose: true,
                todayBtn: 'linked',
                onClose: function (dateText, inst) {
                    
                }
            });

            $('#RealDistDate').datepicker({
                format: 'yyyy-MM-dd',
                //weekStart: 1,
                autoclose: true,
                todayBtn: 'linked',
                onClose: function (dateText, inst) {
                    
                }
            });
          
        }

        function setDateDiff()
        {
            var coigfigDate = $("#DistDate").val();
            var today = $("#today").val();

            /*
            var newDate, sDate, eDate;
            //js月份默认是从0开始的所以月份要-1
            newDate = today.split("-")
            sDate = new Date(newDate[2], newDate[1] - 1, newDate[0])

            newDate = coigfigDate.split("-")
            eDate = new Date(newDate[2], newDate[1] - 1, newDate[0])
            //(1000 * 60 * 60 * 24) 得到分钟除60000就好了 
            iDays = Math.floor((eDate - sDate) / 86400000)
            */
            var day = 24 * 60 * 60 * 1000;
            var dateArr = coigfigDate.split("-");
            var checkDate = new Date();
            checkDate.setFullYear(dateArr[0], dateArr[1] - 1, dateArr[2]);
            var checkTime = checkDate.getTime();

            var dateArr2 = today.split("-");
            var checkDate2 = new Date();
            checkDate2.setFullYear(dateArr2[0], dateArr2[1] - 1, dateArr2[2]);
            var checkTime2 = checkDate2.getTime();

            var cha = (checkTime - checkTime2) / day;
            

        }

    </script>
</asp:Content>
