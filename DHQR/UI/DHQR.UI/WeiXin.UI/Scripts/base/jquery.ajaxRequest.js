var __FunAssemblyName = "KingNET.Business.dll";
var __ParamAssemblyName = "KingNET.Entity.dll";
$.fn.ajaxForm = function(c) {
    if (c.initData != null) {
        this.each(function() {
            c.form = $(this);
            $.ajaxRequest(c)
        });
        c.initData = null
    }
    return this.ajaxFormUnbind().bind('submit.form-plugin', 
    function() {
        $(this).ajaxSubmit(c);
        return false
    }).each(function() {
        $(":submit,input:image", this).bind('click.form-plugin', 
        function(e) {
            var a = this.form;
            a.clk = this;
            if (this.type == 'image') {
                if (e.offsetX != undefined) {
                    a.clk_x = e.offsetX;
                    a.clk_y = e.offsetY
                } else if (typeof $.fn.offset == 'function') {
                    var b = $(this).offset();
                    a.clk_x = e.pageX - b.left;
                    a.clk_y = e.pageY - b.top
                } else {
                    a.clk_x = e.pageX - this.offsetLeft;
                    a.clk_y = e.pageY - this.offsetTop
                }
            }
            setTimeout(function() {
                a.clk = a.clk_x = a.clk_y = null
            },
            10)
        })
    })
};
$.fn.ajaxFormUnbind = function() {
    this.unbind('submit.form-plugin');
    return this.each(function() {
        $(":submit,input:image", this).unbind('click.form-plugin')
    })
};
$.fn.ajaxSubmit = function(a) {
    return this.each(function() {
        a.form = $(this);
        $.ajaxRequest(a)
    })
};
$.fn.formToJSON = function() {
    var a = {};
    if (this.length == 0) return a;
    var b = $("input,select,textarea", this[0]);
    for (var i = 0, max = b.length; i < max; i++) {
        var c = $.fieldValue(b[i]);
        if (c === null || typeof c == 'undefined' || c == '' || ($.isArray(c) && !c.length)) {
            continue
        }
        var d = b[i].name.split('__');
        if (d.length == 2) {
            if (!a[d[0]]) {
                a[d[0]] = {}
            }
            a[d[0]][d[1]] = (!a[d[0]][d[1]]) ? c: a[d[0]][d[1]] + "," + c
        } else {
            a[b[i].name] = (!a[b[i].name]) ? c: a[b[i].name] + "," + c
        }
    }
    return a
};
$.fn.fillTemplate = function(b) {
    return this.each(function() {
        if (b == null) b = {};
        var a = $.replaceTemplate($(this).html(), b);
        $(this).html(a);
        $(this).attr("JsonData", $.toJSON(b))
    })
};
$.fn.fillTableTemplate = function(g) {
    return this.each(function() {
        var a = $("tr", this);
        var b = a.eq(0).outer();
        var c = a.eq(1);
        if (c == undefined || c == null || c.html() == null) c = a.eq(0);
        var d = c.outer();
        var e = c.css("display", "").outer();
        var f = "";
        e = e.replace(/%7B/g, '{');
        e = e.replace(/%7D/g, '}');
        e = e.replace(/%7b/g, '{');
        e = e.replace(/%7d/g, '}');
        var url = document.location.href.substring(0, document.location.href.indexOf(window.document.location.pathname)) + document.location.pathname.substring(0, document.location.pathname.lastIndexOf("/") + 1);
        if (url == "http:/")
            url = document.location;
        var rep = new RegExp("src=\"" + url, "g");
        e = e.replace(rep, "src=\"");
        for (var i = 0; i < g.length; i++) {
            g[i].__RowIndex__ = i + 1;
            f += $.replaceTRTemplate(e, g[i])
        }

        $(this).html(b + d + $("#" + $(this).attr("id") + "-NoData").outer() + $("#" + $(this).attr("id") + "-Loading").outer() + f);
        $(this).attr("JsonData", $.toJSON(g))
    })
};

$.fn.fillTableNoDataTemplate = function(g) {
    return this.each(function() {
        var a = $("tr", this);
        var b = a.eq(0).outer();
        var c = a.eq(1);
        var d = c.outer();
        var e = c.css("display", "").outer();
        var f = "";
        $(this).html(b + d+$("#"+$(this).attr("id")+"-NoData").outer()+$("#"+$(this).attr("id")+"-Loading").outer() + f);
    })
};


$.fn.fillDivTemplate = function(g) {
    return this.each(function() {
        var a = $("li", this);
        //var b = a.eq(0).outer();
        var c = a.eq(0);
        var d = c.outer();
        var e = c.css("display", "").outer();
        var f = "";
        e = e.replace(/%7B/g, '{');
        e = e.replace(/%7D/g, '}');
        e = e.replace(/%7b/g, '{');
        e = e.replace(/%7d/g, '}');
        var url = document.location.href.substring(0,document.location.href.indexOf(window.document.location.pathname)) + document.location.pathname.substring(0,document.location.pathname.lastIndexOf("/")+1);
       if(url=="http:/")
            url=document.location;
        var rep =new RegExp("src=\""+url,"g");
        e = e.replace(rep,"src=\"");
        for (var i = 0; i < g.length; i++) {
            g[i].__RowIndex__ = i + 1;
            f += $.replaceTRTemplate(e, g[i])
        }
        $(this).html("<ul>"+d+f+"</ul>");
        $(this).attr("JsonData", $.toJSON(g))
    })
};
$.fn.fillULTemplate = function(g) {
    return this.each(function() {
        var a = $("li", this);
        //var b = a.eq(0).outer();
        var c = a.eq(0);
        var d = c.outer();
        var e = c.css("display", "").outer();
        var f = "";
        e = e.replace(/%7B/g, '{');
        e = e.replace(/%7D/g, '}');
        e = e.replace(/%7b/g, '{');
        e = e.replace(/%7d/g, '}');
        var url = document.location.href.substring(0,document.location.href.indexOf(window.document.location.pathname)) + document.location.pathname.substring(0,              document.location.pathname.lastIndexOf("/")+1);
        if(url=="http:/")
            url=document.location;
        var rep =new RegExp("src=\""+url,"g");
        e = e.replace(rep,"src=\"");
        for (var i = 0; i < g.length; i++) {
            g[i].__RowIndex__ = i + 1;
            f += $.replaceTRTemplate(e, g[i])
        }
        $(this).html(d+f+"");
        $(this).attr("JsonData", $.toJSON(g))
    })
};
$.fn.fillPageList = function(a, b) {
    return this.each(function() {
        if (a == null) a = [];
        $(this).fillTableTemplate(a);
        Pageing(b,$(this).attr("id"));
    })
};
$.fn.fillDivPageList = function(a, b) {
    return this.each(function() {
        if (a == null) a = [];
        $(this).fillDivTemplate(a);
        Pageing(b,$(this).attr("id"));
    })
};
$.fn.fillULPageList = function(a, b) {
    return this.each(function() {
        if (a == null) a = [];
        $(this).fillULTemplate(a);
        Pageing(b,$(this).attr("id"));
    })
};
$.fn.pageBar = function(c) {
    return this.each(function() {
        var a = {
            PageIndex: 1,
            PageSize: 15,
            TotalPage: 0,
            RecordCount: 0,
            showPageCount: 8,
            onPageClick: function() {
                return false
            }
        };
        $.extend(a, c);
        var b = {
            callback: a.onPageClick
        };
        b.items_per_page = a.PageSize;
        b.num_display_entries = a.showPageCount;
        b.current_page = --a.PageIndex;
        b.num_edge_entries = 1;
        b.prev_text = "上一页";
        b.next_text = "下一页";
        $(this).pagination(a.RecordCount, b);
        return this
    })
};
$.fn.fillForm = function(c) {
    return this.each(function() {
        if (c == null) c = {};
        var a = $("input,textarea,select", this);
        for (var i = 0, max = a.length; i < max; i++) {
            var b = c[a[i].name];
            if (typeof b == 'undefined') continue;
            $(a[i]).setFieldValue(b)
        }
    })
};
$.fn.getRow = function(a) {
    var b = {};
    var c = this.attr("JsonData");
    if ((c == null) || (typeof c == 'undefined')) return b;
    var d = $.evalJSON(c);
    if (!$.isArray(d)) return b;
    if (a < 1) a = 1;
    if (d.length >= a) {
        b = d[a - 1]
    }
    return b
};
$.fn.getSelectedRows = function() {
    var a = [];
    if (this.length == 0) return a;
    var b = $(this[0]);
    $("input[name='" + b.attr("id") + "_CheckBoxIds']", this[0]).each(function() {
        if (this.disabled || !this.checked) return;
        a.push(b.getRow(this.value))
    });
    return a
};
$.ajaxRequest = function(e) {
    //    alert($(this).attr("id"));
    var f = {
        AssemblyName: __FunAssemblyName,
        ClassName: null,
        MethodName: null,
        ParamModelName: null,
        ParamAssemblyName: __ParamAssemblyName,
        form: null,
        target: null,
        page: {
            AllowPaging: false,
            PageIndex: 0,
            PageSize: 15,
            TotalPage: 0,
            RecordCount: 0,
            showPageCount: 8
        },
        onRequest: null,
        onResponse: null,
        onComplete: null,
        onError: null,
        initData: null
    };

    e.page = $.extend({},
    f.page, e.page);
    $.extend(f, e);

    var g = {};
    if (f.form) {
        g = f.form.formToJSON()
    }
    if (f.onRequest) {
        g = f.onRequest(g)
    }
    var h = null;
    if (f.initData == null) {
        h = serializeRequest(f, g)
    }
    var tableID = "";
    if (f.target != null) {
        tableID = f.target.attr("id");

        $("#" + tableID + "-Loading").html("<td colspan=\"" + $("#" + tableID + " tr:eq(0) td").length + "\">数据加载中，请稍后...</td>")
    }
    var i = {
        requestConfigs: f,
        requestParam: g,
        url: '/Common/WebAdapter.aspx',
        type: "Post",
        dataType: "json",
        data: h,
        error: function(a) {
            //TipLoading.hide();
            requestConfigs = this.requestConfigs;
            if (requestConfigs.onError) {
                requestConfigs.onError(a)
            } else {
                alert("服务器端出错！")
            }
        },
        success: function(b) {

            //TipLoading.hide();
            var c = b,
            mValue = {},
            oPage = {},
            requestConfigs = this.requestConfigs,
            requestParam = this.requestParam,
            ajaxOpt = this;
            if (c.Complete.toLowerCase() == 'succeed') {
                if (c.Result != null) {
                    switch (c.ResultType.toLowerCase()) {
                        case 'conditionresult':
                            oPage.AllowPaging = c.Result.AllowPaging;
                            oPage.TotalPage = c.Result.TotalPage;
                            oPage.PageIndex = c.Result.PageIndex;
                            oPage.PageSize = c.Result.PageSize;
                            oPage.RecordCount = c.Result.TotalCount;
                            oPage.showPageCount = requestConfigs.page.showPageCount;
                            oPage = $.extend({},
                        requestConfigs.page, oPage);
                            oPage.onPageClick = function(a) {
                                $.extend(requestConfigs.page, oPage);
                                requestConfigs.page.PageIndex = ++a;
                                ajaxOpt.data = serializeRequest(requestConfigs, requestParam);
                                //TipLoading.show();
                                $.ajax(ajaxOpt)
                            };
                            mValue = c.Result.ResultList;
                            break;
                        default:
                            mValue = c.Result;
                            break
                    }
                }
                if (c.Result == undefined)
                    mValue = null;


                if (mValue == null || mValue == "") {
                    if (tableID != "") {
                        $("#" + tableID + "-Loading").html("");
                        if (requestConfigs.onResponse) {
                            requestConfigs.onResponse(null, null)
                        }
                        if (requestConfigs.target) {
                            if (requestConfigs.target.attr("tagName") != undefined) {
                                var d = requestConfigs.target.attr("tagName").toLowerCase();
                                if (d == "table") {
                                    $(requestConfigs.target).fillTableNoDataTemplate(null);
                                    $("#" + tableID + "_PageBar").html("");
                                }
                            }
                        }
                    }
                    if (requestConfigs.onComplete) {
                        requestConfigs.onComplete(mValue, null)
                    }
                    return;
                }

                if (requestConfigs.onResponse) {
                    setPageIndex(requestConfigs.target, oPage);
                    mValue = requestConfigs.onResponse(mValue, oPage)
                }
                if (requestConfigs.target) {
                    var d = requestConfigs.target.attr("tagName");
                    if (d == undefined || d == null) return;
                    var d = d.toLowerCase();
                    switch (d) {
                        case "table":
                            requestConfigs.target.fillPageList(mValue, oPage);
                            break;
                        case "form":
                            requestConfigs.target.fillForm(mValue);
                            break;
                        case "div":
                            requestConfigs.target.fillDivPageList(mValue, oPage);
                            break;
                        case "ul":
                            requestConfigs.target.fillULPageList(mValue, oPage);
                            break;
                        default:
                            requestConfigs.target.fillTemplate(mValue);
                            break
                    }
                }

                if (requestConfigs.onComplete) {
                    requestConfigs.onComplete(mValue, oPage)
                }
            } else {
                alert("系统异常.");
                //                wiw.msg = c.Message;
                //                wiw.title = "操作提示";
                //                wiw.Alert()
            }
            if (f.target != null)
                $("#" + tableID + "-Loading").html("");
        }
    };
    if (f.initData != null) {
        i.success(f.initData)
    } else {
        //TipLoading.show();
        $.ajax(i)
    }
    function setPageIndex(a, b) {
        if (a == null || b == null) {
            return false
        }
        b.PageIndex = b.PageIndex == 0 ? 1 : b.PageIndex;
        var c = $(a).attr("id") + "_PageIndex";
        var d = $("#" + c);
        if (d.length == 0) {
            $('body').append('<input type="hidden" value="' + b.PageIndex + '" id="' + c + '" \/>')
        } else {
            $(d).val(b.PageIndex)
        }
    };
    function serializeRequest(a, b) {
        if (f.page.AllowPaging) {
            b.AllowPaging = true;
            b.PageSize = f.page.PageSize;
            b.RecordCount = f.page.RecordCount;
            var c = $("#" + $(a.target).attr("id") + "_PageIndex");
            if (f.page.PageIndex < 1) {
                b.PageIndex = $(c).length == 0 ? 1 : $(c).val()
            } else {
                b.PageIndex = f.page.PageIndex
            }
        }
        var d = {
            FunAssemblyName: f.AssemblyName,
            FunClassName: f.ClassName,
            FunMethodName: f.MethodName,
            ParamAssemblyName: f.ParamAssemblyName,
            ParamClassName: f.ParamModelName,
            ParamType: $.isArray(b) ? "List" : "Entity",
            ParamData: $.toJSON(b)
        };
        return $.toJSON(d)
    }
};
$.replaceTemplate = function(b, c) {

    b = b.replace(/%7B/g, '{');
    b = b.replace(/%7D/g, '}');
    b = b.replace(/%7b/g, '{');
    b = b.replace(/%7d/g, '}');
    var d = new RegExp('\\{\\$(.+?)\\$\\}', 'g');
    var e = b.replace(d, 
    function() {
        var a = c[arguments[1]];
        if (a == undefined || a == null) {
            a = ""
        }

        return a
    });
    return e
};
$.replaceTRTemplate = function(b, c) {

    var d = new RegExp('\\{\\$(.+?)\\$\\}', 'g');
    var e = b.replace(d, 
    function() {
        var a = c[arguments[1]];
        if (a == undefined || a == null) {
            a = ""
        }
        return a
    });
    return e
};

function Pageing(b,thisID)
{
    //if(b.AllowPaging!=undefined || b.AllowPaging==true  )
    if(b.AllowPaging==true && b.RecordCount> b.PageSize )
    { 
        if($("#" + thisID + "_PageBarTemp").html()==null)
        {
            $("#" + thisID + "_PageBar").append("<div class=\"sale-page\"><div class=\"pagination\"> <div class=\"page-bottom\" id="+thisID + "_PageBarTemp> </div></div></div>");
        }
        $("#" + thisID + "_PageBarTemp").pageBar(b)
    }
    else
        $("#" + thisID + "_PageBar").html("");
}