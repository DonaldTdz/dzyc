<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/New_Mobile.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    账号未绑定
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div data-role="page" data-theme="d" style="background-color: #fff; position: relative;height:500px;background:url(../../images/mobile/binding.jpg) no-repeat border-box center;background-size:95%"> 
           <%--<img src="../../images/mobile/working.jpg"  style="width:100%" />--%>
<%--          <a id="ctel" href="../Mobile/Bind>" data-role="button" data-theme="a" data-inline="true" style="margin-bottom:10px;" >立即绑定</a>--%>
        </div>
        <!-- /footer -->  
           <div data-role="footer"  data-theme="c" class="footer">
                    <div>
                        <a href="#" target="_self">微官网</a>
                    </div>
                    <div>
                        <a href="#" target="_blank">由和创科技 提供技术支持</a>
                    </div>
        </div>
       
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
    <style type="text/css">
        .footer {
            position: fixed;
            bottom: 0px;
            width: 100%;
        }

            .footer div {
                text-align: center;
            }

            .footer > div > a {
                text-decoration: none;
                font-size: 12px;
                color: #999;
            }

    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
</asp:Content>
