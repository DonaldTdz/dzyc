<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/New_Mobile.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    订单明细
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="wrapper"  data-role="page" data-theme="d" style=" background-color:#fff;background-image:none;box-sizing:border-box;max-width:100%;">
        
        <div data-role="content" style="background-color: #fff; position: relative; height: 500px;">
            <div style="width: 100%; padding-left: 30px;box-sizing:border-box;max-width:100%;">
                <h1 style="margin: 6px 0; font-size: 24px; color: rgb(57,123,72); display: inline-block;"><%=ViewData["WeiXinAppName"].ToString()%></h1>
                <div style="position: absolute; display: inline-block; right: 20px; top: 20px; line-height: 25px;">
                    <img src="../../images/mobile/用户小.png" style="width: 25px;" /><span style="font-family: '楷体'; float: right;"><%=ViewData["WxUserRealName"] %></span>
                </div>
            </div>
            <img src="../../images/mobile/绿色横条.png" style="width: 100%;" />            
            <div style="padding-left:20px;">
                <img src="../../images/mobile/放大镜小.png" style="height:30px;float:left;margin-top:15px;" />
                <h4 style="float:left;margin-left:5px;">
                    订单明细</h4>
                <div style="clear:left;"></div>
            </div>
            <table id="orderList" style="width: 80%; margin-left: 10%; border-collapse: collapse;">
                <thead>
                    <tr>
                      
                        <th data-priority="1">卷烟名称</th>
                        <th data-priority="1">卷烟单价</th>
                        <th data-priority="1">订单数量</th>
                        <th data-priority="1">订单金额</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
           <%-- <div style="width: 100%; height: 46px;" class="linkBtn">
                <button id="prev" class="prev able"><&nbsp;&nbsp;&nbsp;上一页</button>

                <button id="next" class="next able">下一页&nbsp;&nbsp;&nbsp;></button>
            </div>--%>
            <input type="hidden" id="CO_NUM" value="<%=ViewData["CO_NUM"]%>" />
        </div>
        <%--<div data-role="footer" data-theme="c" class="footer">
            <div>
                        <a href="#" target="_self">微官网</a>
            </div>
            <div>
                <a href="#" target="_blank">由和创科技 提供技术支持</a>
            </div>

        </div>--%>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ResourceContent" runat="server">
    <style type="text/css">
        th, td {
            text-align: center;
        }

        #orderList thead tr {
            line-height: 40px;
        }

            #orderList thead tr th {
                width: 20%;
                font-family: sans-serif;
                font-weight: bolder;
                font-size: 14px;
            }
          #totaltr td{
                width: 20%;
                font-family: sans-serif;
                font-weight: bolder;
                font-size: 24px;
            }

        #orderList tbody tr {
            border-bottom: 2px solid #cfcccc;
            
            line-height: 20px;
        }

            #orderList tbody tr:last-child {
                border-bottom: none;
            }

            #orderList tbody tr td {
                font-size: 12px;
                padding:5px 0;
            }

                #orderList tbody tr td a {
                    text-decoration: none;
                }

       button.prev {
            width: 35%;
            float: left;
            left: 0px;
            margin-left: 10px;
            margin-top: 10px;
            line-height: 25px;
            padding:5px 0px;
            border: 1px solid #d4c9c9;
            border-radius: 4px;
            display: inline-block;
            color: #fff;
            line-height:25px;
            text-align: center;
            cursor: pointer;
        }

            .next:hover, a.prev:hover {
                color: #fff;
            }

        button.next {
            width: 35%;
            cursor: pointer;
            line-height:25px;
             padding:5px 0px;
            float: right;
            right: 0px;
            margin-top: 10px;
            margin-right: 10px;
            border: 1px solid #d4c9c9;
            border-radius: 2px;
            display: inline-block;
            color: #fff;
            text-align: center;
        }

        button#next.able,button#prev.able {
            background-image: url(../../images/mobile/able.png);
            background-repeat:repeat;
            color: #fff;
        }

        button#next.unable,button#prev.unable {
            background-image: url(../../images/mobile/unable.png);
            background-repeat:repeat;
            color: #fff;
        }

        .number {
            border: 1px solid gray;
            border-radius: 5px;
            width: 50%;
            margin: 0 auto;
        }

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
       #wrapper{
    		max-width: 100%;
    		box-sizing:border-box;
    	}

    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="BottomScript" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            GetPageData(1);

        });
        function GetPageData(pageIndex) {
            var CO_NUM = $("#CO_NUM").val();
            $.ajax({
                url: "QueryOrderDetails",
                data: { Page: pageIndex, Rows: 5, Sidx: "ITEM_NAME", CO_NUM: CO_NUM },
                type: "POST",
                catche: false,
                success: function (result) {
                    var totalPage = result.total, records = result.records, data = result.rows, startIndex = Number((pageIndex - 1) * 10) + 1;
                    var tbody = $("#orderList tbody");
                    tbody.children().remove()
                    var pageDiv = $("#pageIndex");
                    pageDiv.children().remove();
                    var totalQty = 0;
                    var totalAmt = 0;
                    if (totalPage == 0) {
                        $("#prev").attr("disabled", "");
                        $("#next").attr("disabled", "");
                    } else {
                        $.each(data, function (key, val) {
                            var tr = $("<tr><td>" + val.ITEM_NAME + "</td><td>" + val.PRICE + "</td><td><div class='number'>" + val.QTY + "</div></td><td><div>"
                                + val.AMT + "</div></td></tr>");
                            tr.appendTo(tbody);
                            totalQty = totalQty + parseInt(val.QTY);
                            totalAmt =accAdd(parseFloat( totalAmt), parseFloat(val.AMT));
                        });
                        var tr = $("<tr id='totaltr'><td>总计：</td><td></td><td>" + totalQty + "</td><td>" + totalAmt + "</td>");
                        tr.appendTo(tbody);
                        for (var i = 1; i <= totalPage; i++) {
                            if (i == pageIndex) {
                                pageDiv.append($("<button   data-theme='e' disabled='' >" + i + " </button>"));

                            } else {
                                pageDiv.append($("<button  onclick='GetPageData(" + i + ")'  data-theme='e' >" + i + " </button>"));
                            }
                        }

                        if (pageIndex != 1) {
                            $("#prev").attr("disabled", false);
                            $("#prev").addClass("able").removeClass("unable");
                        } else {
                            $("#prev").attr("disabled", true);
                            $("#prev").removeClass("able").addClass("unable");
                        }
                        if (pageIndex != totalPage) {
                            $("#next").attr("disabled", false);
                            $("#next").addClass("able").removeClass("unable");
                        } else {
                            $("#next").attr("disabled", true);
                            $("#next").removeClass("able").addClass("unable");
                        }
                        $("#prev").unbind("click").bind("click", function () {
                            GetPageData(Number(pageIndex) - 1);
                        });
                        $("#next").unbind("click").bind("click", function () {
                            GetPageData(Number(pageIndex) + 1);
                        });
                        $("#next,#prev").mouseout();
                    }

                },
                error: function (e) {
                }

            });
        }

        // 两个浮点数求和
        function accAdd(num1, num2) {
            var r1, r2, m;
            try {
                r1 = num1.toString().split('.')[1].length;
            } catch (e) {
                r1 = 0;
            }
            try {
                r2 = num2.toString().split(".")[1].length;
            } catch (e) {
                r2 = 0;
            }
            m = Math.pow(10, Math.max(r1, r2));
            // return (num1*m+num2*m)/m;
            return Math.round(num1 * m + num2 * m) / m;
        }
    </script>
</asp:Content>
