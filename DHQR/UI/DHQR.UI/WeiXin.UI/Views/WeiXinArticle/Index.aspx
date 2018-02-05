<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DHQR.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="iframe_page">
        <div class="iframe_content">
            <div class="r_nav">
                <ul>
                </ul>
            </div>
            <div id="reply_keyword" class="r_con_wrap">
                <div class="control_btn">
                    <a href="../WeiXinArticle/CreateOrEdit" class="btn_green btn_w_120">添加文章</a>
                </div>
                <table border="0" cellpadding="5" cellspacing="0" class="r_con_table">
                    <thead>
                        <tr>
                            <td width="10%" nowrap="nowrap">名称
                            </td>
                            <td width="15%" nowrap="nowrap">标题
                            </td>
                            <td width="10%" nowrap="nowrap">文章类型
                            </td>
                            <td width="10%" nowrap="nowrap">图片地址
                            </td>
                            <td width="10%" nowrap="nowrap">描述
                            </td>
                            <td width="10%" nowrap="nowrap">路径</td>
                            <td width="10%" nowrap="nowrap">排序号</td>
                            <td width="10%" nowrap="nowrap" class="last">操作</td>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
                <div class="blank20">
                </div>
                <div id="turn_page">
                    <font class="page_noclick">&lt;&lt;上一页</font>&nbsp;<font class="page_item_current">0</font>&nbsp;<font
                        class="page_noclick">下一页&gt;&gt;</font>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
    <link href="../../DHQRCss/global.css" rel="stylesheet" type="text/css" />
    <link href="../../DHQRCss/main.css" rel="stylesheet" type="text/css" />
    <link href="../../DHQRCss/wechat.css" rel="stylesheet" type="text/css" />
    <script src="../../DHQRJs/global.js" type="text/javascript"></script>
    <script src="../../DHQRJs/wechat.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
   <script type="text/javascript">
       $(document).ready(function () {
           getPageData(1);
       });
       function getPageData(pageIndex)
       {
           $.post("GetPageData", { Page: pageIndex, Rows: 10, Sidx: "ArticleSort" }, function (response) {
               var dataRows = response.rows;
               var tbody = $(".r_con_table").find("tbody");
               tbody.empty();
               $(dataRows).each(function (i) {
                   var tr = $("<tr></tr>");
                   tr.append('<td style="display:none">' + dataRows[i].Id + '</td>');

                   var td = $('<td nowrap="nowrap"></td>');
                   td.append(dataRows[i].Name);
                   tr.append(td);

                   td = $('<td nowrap="nowrap"></td>');
                   td.append(dataRows[i].Title);
                   tr.append(td);

                   td = $('<td nowrap="nowrap"></td>');
                   td.append(dataRows[i].ArticleTypeName);
                   tr.append(td);

                   td = $('<td nowrap="nowrap"></td>');
                   td.append(dataRows[i].PicUrl);
                   tr.append(td);

                   td = $('<td nowrap="nowrap"></td>');
                   td.append(dataRows[i].Description);
                   tr.append(td);

                   td = $('<td nowrap="nowrap"></td>');
                   td.append(dataRows[i].Url);
                   tr.append(td);

                   td = $('<td nowrap="nowrap"></td>');
                   td.append(dataRows[i].ArticleSort);
                   tr.append(td);
                   //操作
                   td = $('<td nowrap="nowrap" class="last"></td>');
                   var modifyATag = $('<a href="../WeiXinArticle/CreateOrEdit?Id=' + dataRows[i].Id + '"><img src="../../DHQRImages/ico/mod.gif" align="absmiddle" alt="修改"></a>');
                   td.append(modifyATag);
                   td.append('<sapn style="display:inline-block; width:5px;"></span>');
                   var delATag = $('<a href="javascript:void(0)" onclick="delItem(this)" style="cursor:pointer;"><img src="../../DHQRImages/ico/del.gif" align="absmiddle" alt="删除"></a>');
                   td.append(delATag);

                   //设置分页数据
                   setPageDiv("turn_page", pageIndex, response.total, "getPageData");
                   tr.append(td);

                   tbody.append(tr);
               });
           });
       }

       //设置翻页
       function setPageDiv(pageDivId, pageIndex, totalPage, refashFn) {
           var pageDiv = $("#" + pageDivId);
           var pageHtml = "";
           var prevPage, nextPage;
           if (pageIndex == 1 || totalPage == 0) {
               prevPage = '<font class="page_noclick">&lt;&lt;上一页</font>';
           } else {
               prevPage = '<a style="cursor:pointer;" onclick="getPageData(' + (pageIndex - 1) + ')"><font class="page_button">&lt;&lt;上一页</font></a>';
           }
           if (pageIndex == totalPage) {
               nextPage = '<font class="page_noclick">下一页&gt;&gt;</font>';
           } else {
               nextPage = '<a style="cursor:pointer;" onclick="' + refashFn + '(' + (pageIndex + 1) + ')"><font class="page_button">下一页&gt;&gt;</font></a>';
           }
           pageHtml = prevPage;
           for (var i = 1; i <= totalPage; i++) {
               if (i == pageIndex) {
                   pageHtml += '&nbsp;<font class="page_item_current">' + i + '</font>';
               } else {
                   pageHtml += '&nbsp;<a style="cursor:pointer;" onclick="' + refashFn + '(' + i + ')"><font>' + i + '</font></a>';
               }
           }
           if (totalPage == 0) {
               pageHtml += '&nbsp;<font class="page_item_current">' + 0 + '</font>';
           }
           pageHtml += '&nbsp' + nextPage;
           pageDiv.html(pageHtml);
       }
       function delItem(obj) {
           if (!confirm("删除后不可恢复，继续吗？")) { return false };
           var Id = $(obj).parents("tr")[0].children[0].innerText;
           $.post("Del", { Id: Id }, function (data) {
               if (data.IsSuccessful == true) {
                   $(obj).parents("tr").remove();
               } else {
                   alert(data.OperateMsg);
               }
           }, 'json');
       }
   </script>
</asp:Content>
