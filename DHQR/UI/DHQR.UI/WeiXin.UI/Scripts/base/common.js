 
function CheckAllByName(name) {
    $("[name='" + name + "']").each(function() {
        if (this.type == "checkbox") {
            this.checked = true;
        }
    });
}
function CheckAllByClass(classs) {
    $("." + classs).each(function() {
        if (this.type == "checkbox") {
            this.checked = true;
        }
    });
}
function CheckAll() {
    $("input").each(function() {
        if (this.type == "checkbox") {
            this.checked = true;
        }
    });
}

function unselectall() {
    if (document.aspnetForm.chkAll.checked)
        document.aspnetForm.chkAll.checked = document.aspnetForm.chkAll.checked & 0;
}


function CheckAll(form) {
    for (var i = 0; i < form.elements.length; i++) {
        var e = form.elements[i];
        if (e.Name != "chkAll" && e.disabled == false)
            e.checked = form.chkAll.checked;
    }
}
//反选
function AntiCheckAllByName(name) {
    $("[name='" + name + "']").each(function() {
        if (this.type == "checkbox") {
            if (this.checked) {
                this.checked = false;
            } else {
                this.checked = true; ;
            }
        }
    });
}
function AntiCheckAllByClass(classs) {
    $("." + classs).each(function() {
        if (this.type == "checkbox") {
            if (this.checked) {
                this.checked = false;
            } else {
                this.checked = true; ;
            }
        }
    });
}
function AntiCheckAll() {
    $("input").each(function() {
        if (this.type == "checkbox") {
            if (this.checked) {
                this.checked = false;
            } else {
                this.checked = true; ;
            }
        }
    });
}

//全不选
function UnCheckAllByName(name) {
    $("[name='" + name + "']").each(function() {
    if (this.type == "checkbox") {
            this.checked = false;
        }
    });
}
function UnCheckAllByClass(classs) {
    $("." + classs).each(function() {
        if (this.type == "checkbox") {
            this.checked = false;
        }
    });
}
function UnCheckAll() {
    $("input").each(function() {
        if (this.type == "checkbox") {
            this.checked = false;
        }
    });
}

//弹出层
//随机生成ID
function randomID() {
    var rnd = "";
    for (var i = 0; i < 4; i++) {
        rnd += Math.floor(Math.random() * 10);
    }
    return rnd;
}
//关闭DIV
function closeDivBase(divID) {
    //this.parentNode.parentNode.parentNode.removeChild(this.parentNode.parentNode);
    $("body #" + divID).remove();
    $("body #bg_" + divID).remove();
}
//打开DIV
function showDivBase(divID, title, url, width, height, isbg, isMove) {

    function DivID() {
        var rnd = "";
        for (var i = 0; i < 4; i++) {
            rnd += Math.floor(Math.random() * 10);
        }
        return "Div_" + rnd;
    }

    ////////////////////////////////////////////默认值
    var opacity = 0.3;
    var zindex = 1;

    if (!divID) {
        divID = DivID();
    }
    if (!title) {
        title = "提示信息";
    }
    if (!width) {
        width = 500;
    }
    if (!height) {
        height = 400;
    }
    //////////////////////////////////////////////////

    if ($("#" + divID) != null && $("#" + divID).html() != null && $("#" + divID).html() != "") {
        alert("禁止重复打开");
        return;
    }

    ////////////////////////////////////////////创建DIV
    if (isbg) {
        zindex++;
        var _bgDiv = '<div id="bg_' + divID + '" style="background:#000000; filter:alpha(opacity=' + (opacity * 100) + '); opacity: ' + opacity + '; width:100%; height:100%; z-index:' + zindex + '; position:absolute; left:0px; top:0px;"></div>';
        $("body").append(_bgDiv);
    }

    var divStr = '<div id="' + divID + '">';
    divStr += '<div id="head_' + divID + '">';
    divStr += '<h3 id="title_' + divID + '">' + title + '</h3>';
    divStr += '<img id="imgClose_' + divID + '" />';
    divStr += '</div>';
    divStr += '<div id="content_' + divID + '"></div>';
    divStr += '</div>';
    $("body").append(divStr);
    //////////////////////////////////////////////////

    ////////////////////////////////////////////加样式
    zindex++;
    var left = (1200 - width) / 2;
    $("#" + divID).css({ "width": width, "height": height, "padding": "1px", "border": "1px solid #4c77aa", "background": "#f2f7fd", "position": "absolute", "z-index": zindex, "left": left, "top": "80px" });
    $("#" + divID + " #head_" + divID).css({ "width": "100%", "height": "25px", "background": "#4c77aa", "cursor": "pointer" });
    $("#" + divID + " #title_" + divID).css({ "color": "#fff", "font-size": "14px", "padding": "5px", "float": "left" });
    $("#" + divID + " #imgClose_" + divID).css({ "float": "right", "padding": "5px", "cursor": "pointer", "height": "12px" });
    $("#" + divID + " #imgClose_" + divID).attr("src", "/Images/themes/icons/cancel.png");
    $("#" + divID + " #imgClose_" + divID).attr("title", "close");
    $("#" + divID + " #content_" + divID).css({ "width": "100%", "height": "100%", "padding": "3px" });

    $("#" + divID + " *").css({ "margin": "0" });
    //////////////////////////////////////////////////

    $("#imgClose_" + divID).click(function() {
        closeDivBase(divID);
    });

    // DIV 加载内容
    /////////////////////////
    $("#" + divID + " #content_" + divID).html("载入中......");

    // iframe
    $("#" + divID + " #content_" + divID).html('<iframe width="' + (width - 10) + '" height="' + (height - 45) + '" frameborder="0" src="' + url + '" id="frmpop" ></iframe>');

    // jquery ajax 加载
    //$("#" + divID + " #text").get(0).src = url;
    //$("#" + divID + " #text").load(url);

    // 暂停1秒
    //var load = function() { $("#" + divID + " #text").load(url) };
    //window.setTimeout(load, 1000);
    /////////////////////////

    // DIV 移动
    /////////////////////////
    if (isMove) {
        zindex++;
        var moveObj = $("#" + divID + " #head_" + divID);
        var global_MOUSEDOWN = false;
        var box = $('<div />');
        box.css({ display: 'none', position: 'absolute', border: '1px dashed #666', "z-index": zindex });
        box.appendTo('body');
        var _x, _y;
        moveObj.mousedown(function(event) {
            global_MOUSEDOWN = true;
            var offset = moveObj.offset();
            _x = event.pageX - offset.left;
            _y = event.pageY - offset.top;
            box.width(width);
            box.height(height);
            box.css({ left: event.pageX - _x, top: event.pageY - _y });
            box.show();
        });
        $(document).mousemove(function(event) {
            if (global_MOUSEDOWN) {
                box.css({ left: event.pageX - _x, top: event.pageY - _y });
            }
        }).mouseup(function(event) {
            if (global_MOUSEDOWN) {
                $("#" + divID).animate({ left: box.offset().left, top: box.offset().top }, 80);
                //$("#" + divID).css({ left: box.offset().left, top: box.offset().top });
                box.hide();
            }
            global_MOUSEDOWN = false;
        });
    }
    //////////////////////////////
}

function ShowEntityEnumFlagName(obj,val)
{
    for(var i=0;i<obj.Value.length;i++)
    {
        if(val==parseInt(obj.Value[i].name))
        {
            if(obj.CName!=undefined && obj.CName[i]!=undefined)
            return obj.CName[i].name;
            else
            return obj.Name[i].name;
        }
    }
    return "--";
}


//初始化数据
String.prototype.Trim = function(trimChar) {
    if (trimChar != undefined && trimChar != "") {
        var regex = new RegExp("(^" + trimChar + "*)|(" + trimChar + "*$)", "g");
        return this.replace(regex, "");
    }
    else {
        return this.replace(/^\s+/g, "").replace(/\s+$/g, "");
    }
}


//Iframe自适应高度  需要在内页调用 iframename 是父页面iframe名字 
function SetAutoIframeHeight(iframeName) {
    var bHeight = document.body.scrollHeight;
    var dHeight = document.documentElement.scrollHeight;
    var height = Math.max(bHeight, dHeight);
    parent.document.getElementById(iframeName).height = height;
}