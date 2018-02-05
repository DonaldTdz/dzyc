window.onerror = function (msg, url, line) {
    var str = "You have found an error as below: \n\n";
    str += "Err: " + msg + " on line: " + line;
    alert(str);
    return true;
}



$(document).ready(function () {
    workflow.init(xmlobject);
    $(".attributecontentleft div").click(function () {
        $(".attributecontentleft div").removeClass("attractive");
        $(".attributecontentright div").hide();
        var id = $(this).attr("cid");
        $(this).addClass("attractive");
        $("#" + id).show();
        $("#" + id + " div").show();
        workflow.toggleDiv([], ['textDiv', 'enumDiv', 'funDiv', 'rfunDiv', 'loadDiv', 'appDiv']);
        workflow.editAttribute();
    });
});

//设置格子
function setgrid(obj) {
    if ($("#paintarea").attr("grid") == 1) {/*$(obj).removeClass("menuitemactive");*/
        $("#paintarea").removeClass("grid"); $("#paintarea").attr("grid", "0");
    } else {/*$(obj).addClass("menuitemactive");*/
        $("#paintarea").addClass("grid"); $("#paintarea").attr("grid", "1");
    }
}

//保存方法
function save() {
    var xml = workflow.getXml();
    if (xml) {
        $("#dialog-confirm").dialog("open");
    }
}
function fabu() { alert("发布") }
function daoru() {
    $._showPromptArea(function (xml) {
        workflow.parseXml(createXml(xml), true);
    });
}
function daochu() {
    alert("导出");
}
//删除
function shanchu() { workflow.delnode(); }

var workflow = {
    init: function (xmlobject) {
        this.xmlobject = xmlobject;
        this.xmlobject.nodelist = this.nodelist;
        this.xmlobject.linelist = this.linelist;
        this.resize();
        var thisobj = this;
        $(window).resize(function () { thisobj.resize(); });
        this.config.browser = $.browser;
        this.paintpanelclick(); //画布点击事件
        this.changetool(); //更换工具
        this.settool(0); //设置默认工具
        this.config.demonode = $("#demonode");
        $("#paintarea").append("<div class='resizediv' id='resizel'></div>");
        $("#paintarea").append("<div class='resizediv' id='resizet'></div>");
        $("#paintarea").append("<div class='resizediv' id='resizeb'></div>");
        $("#paintarea").append("<div class='resizediv' id='resizer'></div>");
        $("#paintarea").append("<div class='resizediv' id='resizelt'></div>");
        $("#paintarea").append("<div class='resizediv' id='resizelb'></div>");
        $("#paintarea").append("<div class='resizediv' id='resizert'></div>");
        $("#paintarea").append("<div class='resizediv' id='resizerb'></div>");

        $("#paintarea").append("<div class='resizediv linediv' id='linestart'></div>");
        $("#paintarea").append("<div class='resizediv linediv' id='lineend'></div>");
        if (this.config.browser.msie) {
            $("#paintarea").append('<v:line id="drawbaseline" class="vline" from="0,0" to="150,150" ></v:line>'); //添加ie画线
        }
        else {
            $("#paintarea").append('<div id="drawbaseline" style="display:none" class="ffline"><a></a><div></div></div>'); //添非ie画线
        }
        var wflow = this;
        $("#paintarea").click(function (e) {
            wflow.hidemovediv();
        });

        $(document).mouseup(function (e) {
            $("#drawbaseline").hide();
            if (wflow.config.moveobj.ismove) {
                var obj = wflow.config.moveobj.obj;
                wflow.config.moveobj.ismove = false;
                wflow.resetdeawline();
                var objpos = wflow.tool.getobjpos(obj);
                if (objpos) {
                    var nodeobj = wflow.nodelist.getByElement(obj);
                    if (nodeobj) {
                        nodeobj.x = objpos.left;
                        nodeobj.y = objpos.top;
                    }
                }
            }
            return false;
        });


        $(document).mousemove(function (e) {
            if (!wflow.config.moveobj.ismove) { return }
            var mousepos = wflow.tool.mousePosition(e);
            if (wflow.config.moveobj.tag == 1) {
                mousepos.x = mousepos.x + $("#paintarea").scrollLeft() - wflow.config.paintpos.left;
                mousepos.y = mousepos.y + $("#paintarea").scrollTop() - wflow.config.paintpos.top;
            }
            wflow.config.moveobj.pos = { x: mousepos.x, y: mousepos.y };
            if (wflow.config.moveobj.tag == 1) {
                wflow.config.moveobj.obj.css("left", mousepos.x - wflow.config.moveobj.offsetpos.x + "px");
                wflow.config.moveobj.obj.css("top", mousepos.y - wflow.config.moveobj.offsetpos.y + "px");
                wflow.showmovediv(wflow.config.moveobj.obj);
                wflow.resetlineposition(wflow.config.moveobj.obj);
            }
        });

        document.getElementById("paintarea").onselectstart = new Function('event.returnValue=false;');

        $("#paintarea").mousemove(function (e) {
            if (wflow.config.tooltype.type == "line" && wflow.config.drawline.drawtag && wflow.config.browser.msie) {
                var mousepos = wflow.tool.mouseOnPanelPosition(e);
                if (wflow.config.drawline.from.x < mousepos.x) { mousepos.x = mousepos.x - 2; } else { mousepos.x = mousepos.x + 2; }
                if (wflow.config.drawline.from.y < mousepos.y) { mousepos.y = mousepos.y - 2; } else { mousepos.y = mousepos.y + 2; }
                $("#drawbaseline").attr("to", mousepos.x + "," + (mousepos.y));
                return false;
            }
            else if (wflow.config.tooltype.type == "line" && wflow.config.drawline.drawtag) {
                var mousepos = wflow.tool.mouseOnPanelPosition(e);
                if (wflow.config.drawline.from.x < mousepos.x) { mousepos.x = mousepos.x - 2; } else { mousepos.x = mousepos.x + 2; }
                if (wflow.config.drawline.from.y < mousepos.y) { mousepos.y = mousepos.y - 2; } else { mousepos.y = mousepos.y + 2; }
                wflow.setNoIElinePos($("#drawbaseline"), wflow.config.drawline.from, mousepos);
                return false;
            }
            //wflow.config.browser.msie
        })


        $(document).keyup(function (e) {
            e = e || window.event;
            if (e.keyCode == 46) {
                if (wflow.config.nowselect != null) {
                    wflow.delnode();
                }
            }
        });
    },
    resize: function () {
        var ww = $(window).width();
        var wh = $(window).height();
        var woffset = 4;
        var hoffset = 4;
        //leftmenu  dragdiv  paintarea
        $("#leftmenu").css("left", woffset + "px");
        woffset = woffset + $("#leftmenu").width();
        $("#dragdiv").css("left", woffset + "px");
        woffset = woffset + 5;
        $("#paintarea").css("left", woffset + "px");
        $("#paintarea").width(ww - woffset - 5);

        $(".setheight").height(wh - $("#attribute").height() - 40);
        hoffset = $("#paintarea").height() + 35;
        $("#dragy").css("top", hoffset + "px");
        hoffset = hoffset + 5;
        $("#attribute").css("top", hoffset + "px");
        $("#attribute").width(ww - 8);
        //$("#attribute").height(wh-hoffset-4);
        $("#attributecontentright").width($("#attribute").width() - 170);

        this.config.paintpos = $("#paintarea").offset(); //设置画布位置
    },
    //节点列表
    nodelist: {
        list: new Array(),
        add: function (node) { this.list[this.list.length] = node; },
        remove: function (id) { for (var i = 0; i < this.list.length; i++) { var node = this.list[i]; if (node.id == id) { this.list.splice(i, 1) } } },
        size: function () { return this.list.length },
        get: function (id) { for (var i = 0; i < this.list.length; i++) { var node = this.list[i]; if (node.id == id) { return node; } } return null },
        getByElement: function (eleobj) { eleobj = workflow.tool.tojQuery(eleobj); var id = eleobj.attr("id"); return this.get(id); },
        getByIndex: function (index) { if (this.list.length > index && index >= 0) { return this.list[index] } return null }
    },
    //连接线列表
    linelist: {
        list: new Array(),
        add: function (node) { this.list[this.list.length] = node; },
        remove: function (id) { for (var i = 0; i < this.list.length; i++) { var node = this.list[i]; if (node.id == id) { this.list.splice(i, 1) } } },
        size: function () { return this.list.length },
        get: function (id) { for (var i = 0; i < this.list.length; i++) { var node = this.list[i]; if (node.id == id) { return node; } } return null },
        getByElement: function (eleobj) { eleobj = workflow.tool.tojQuery(eleobj); var id = eleobj.attr("id"); return this.get(id); },
        getByIndex: function (index) { if (this.list.length > index && index >= 0) { return this.list[index] } return null }
    },
    //节点对象
    nodeobject: {
        basenode: function () {
            this.id = "";
            this.icon = "";
            this.nodetype = 1;
            this.Text = "新建节点";
            this.nodetext = "";
            this.x = 0;
            this.y = 0;
            this.width = 130;
            this.height = 51;
            this.inputlist = new Array();
            this.outputlist = new Array();
            this.inputtype = 0; /*输入节点数量 0：无输入 1：1输入 2：多输入*/
            this.outputtype = 1;
            this.zindex = 100;
            this.state = 0;
            /*删除输入节点*/
            this.removeInput = function (id) { for (var i = 0; i < this.inputlist.length; i++) { if (this.inputlist[i] == id) { this.inputlist.splice(i, 1); return; } } },
            /*删除输出节点*/
			this.removeOutput = function (id) { for (var i = 0; i < this.outputlist.length; i++) { if (this.outputlist[i] == id) { this.outputlist.splice(i, 1); return; } } },
            /*删除所有线*/
			this.removeAllLine = function () { for (var i = 0; i < this.inputlist.length; i++) { var lineid = this.inputlist[i] + "_" + this.id; $("#" + lineid).remove(); var parobj = workflow.nodelist.get(this.inputlist[i]); parobj.removeOutput(this.id); workflow.linelist.remove(lineid); } for (var i = 0; i < this.outputlist.length; i++) { var lineid = this.id + "_" + this.outputlist[i]; $("#" + lineid).remove(); var subobj = workflow.nodelist.get(this.outputlist[i]); subobj.removeInput(this.id); workflow.linelist.remove(lineid); } }
        },
        1: function () {
            this.icon = "nodeicon1";
            this.nodetype = 1;
            this.nodetext = "开始";
            this.Text = '开始';
            this.inputtype = 0; /*输入节点数量 0：无输入 1：1输入 2：多输入  1.开始,2 结束,3 分支. 4 合并 5 任务*/
            this.outputtype = 1;
        },
        2: function () {
            this.icon = "nodeicon2";
            this.nodetype = 2;
            this.nodetext = "结束";
            this.Text = '结束';
            this.inputtype = 2; /*输入节点数量 0：无输入 1：1输入 2：多输入*/
            this.outputtype = 0;
            this.state = 0;
        },
        3: function () {
            this.icon = "nodeicon3";
            this.nodetype = 3;
            this.nodetext = "分支";
            this.inputtype = 1; /*输入节点数量 0：无输入 1：1输入 2：多输入*/
            this.outputtype = 2;
            this.state = 0;
        },
        4: function () {
            this.icon = "nodeicon4";
            this.nodetype = 4;
            this.nodetext = "合并";
            this.inputtype = 2; /*输入节点数量 0：无输入 1：1输入 2：多输入*/
            this.outputtype = 1;
            this.state = 0;
        },
        5: function () {
            this.icon = "nodeicon5";
            this.nodetype = 5;
            this.nodetext = "任务";
            this.inputtype = 2; /*输入节点数量 0：无输入 1：1输入 2：多输入*/
            this.outputtype = 1;
            this.state = 0;
        },
        100: function () {
            this.id = "";
            this.nodetype = 100;
            this.Text = "";
            this.zindex = 100;
        },
        getNode: function (key) {
            var node = new this.basenode();
            var snode = new this[key]();
            for (var k in snode) {
                node[k] = snode[k];
            }
            return node;
        }
    },
    config: {//变量
        paintpos: { left: 0, top: 0 }, //画布位置
        browser: {}, //浏览器信息
        tooltype: 0, //工具类型 0：选择
        demonode: null, //元素样例
        zindex: 100, //z-index
        moveobj: { obj: null, ismove: false, pos: { x: 0, y: 0 }, offsetpos: { x: 0, y: 0 }, tag: 0 }, //当前移动对象
        drawline: { start: null, end: null, drawtag: false, from: { x: 0, y: 0} },
        nowselect: null,
        a: 0
    },
    tool: {//工具
        //获得event对象
        getevent: function (e) { return e || window.event; },
        //鼠标位置
        mousePosition: function (ev) { ev = this.getevent(ev); if (ev.pageX || ev.pageY) { return { x: ev.pageX, y: ev.pageY }; } return { x: ev.clientX + document.body.scrollLeft - document.body.clientLeft, y: ev.clientY + document.body.scrollTop - document.body.clientTop} },
        //获取事件源
        getResource: function (event) { event = this.getevent(event); var obj = event.srcElement ? event.srcElement : event.target; return obj; },
        //获取鼠标在画布上的位置
        mouseOnPanelPosition: function (e) { var sl = $("#paintarea").scrollLeft(); var st = $("#paintarea").scrollTop(); var mousepos = workflow.tool.mousePosition(e); var panelpos = workflow.config.paintpos; mousepos.x = mousepos.x - panelpos.left + sl; mousepos.y = mousepos.y - panelpos.top + st; return mousepos; },
        //获取新随机id
        newname: function () {
            var myDate = new Date(); var tm = myDate.getYear() + "-" + myDate.getMonth() + "-" + myDate.getDate() + "-" + myDate.getDay() + myDate.getTime() + "-" + myDate.getHours() + "-" + myDate.getMinutes() + "-" + myDate.getSeconds() + "-" + myDate.getMilliseconds() + "-" + Math.random();
            return $.md5(tm).toUpperCase();
        },
        //转化成jquery对象
        tojQuery: function (obj) { if (!(obj instanceof jQuery)) { obj = $(obj) } return obj; },
        //获得offset位置
        offset: function (obj) { obj = this.tojQuery(obj); var objpos = obj.offset(); var parentobj = obj.offsetParent(); var oleft = objpos.left + parentobj.scrollLeft(); var otop = objpos.top + parentobj.scrollTop(); return { left: oleft, top: otop }; },
        getobjpos: function (obj) { obj = this.tojQuery(obj); if (obj.length <= 0) { return { left: 0, top: 0} } var left = obj.css("left"); left = left.replace("px", ""); left = parseInt(left); var top = obj.css("top"); top = top.replace("px", ""); top = parseInt(top); return { left: left, top: top} },
        a: function () { }
    },
    changetool: function ()//更换工具
    {
        var wf = this;
        $(".tool").click(function () {
            wf.config.tooltype = { type: $(this).attr("type"), value: $(this).attr("value") };
            $(".tool").removeClass("menuitemactive");
            $(this).addClass("menuitemactive");
        });
    },
    //设置工具
    settool: function (value) {
        $(".tool").removeClass("menuitemactive");
        $(".tool[value=" + value + "]").addClass("menuitemactive");
        this.config.tooltype = { type: $(".tool[value=" + value + "]").attr("type"), value: $(".tool[value=" + value + "]").attr("value") };
    },
    paintpanelclick: function ()//画布点击事件
    {
        var wf = this;
        $("#paintarea").click(function (e) {
            var tooltype = wf.config.tooltype; //工具类型
            e = workflow.tool.getevent(e);
            if (tooltype.type == "tool")//添加节点元素
            {
                //nodeobject//nodelist
                var nodetype = tooltype.value;
                //var nodeobj = new wf.nodeobject[nodetype];
                var nodeobj = wf.nodeobject.getNode(nodetype);

                wf.config.zindex = wf.config.zindex + 1;
                var zindex = wf.config.zindex;
                var newid = wf.tool.newname(); //随机生成新id
                var mousePosition = workflow.tool.mouseOnPanelPosition(e);
                var node = wf.config.demonode.clone();
                node.css("left", mousePosition.x);
                node.css("top", mousePosition.y);
                node.css("z-index", zindex);
                node.attr("id", newid);
                node.height(nodeobj.height);
                node.width(nodeobj.width);
                $(">.c>.icon>.t", node).html("&lt;&lt;" + nodeobj.nodetext + "&gt;&gt;&nbsp;&nbsp;");
                $(">.c>.icon>.tt", node).html(nodeobj.Text);
                $(">.c>.icon", node).width(nodeobj.width - 10);
                $(">.c>.icon", node).addClass(nodeobj.icon);
                $(">.c>.icon", node).addClass("nodeicon");
                nodeobj.id = newid;
                nodeobj.x = mousePosition.x;
                nodeobj.zindex = zindex;
                nodeobj.y = mousePosition.y;
                wf.nodelist.add(nodeobj);
                node.bind("click", wf.nodeclick);
                node.bind("mousedown", wf.nodemousedown);
                node.bind("mouseup", wf.nodemouseup);
                $(this).append(node);
                wf.settool(0); //设置工具
            }
        });
    },
    //节点点击事件
    nodeclick: function (e) {
        if (workflow.config.tooltype.type == "select") {
            workflow.config.moveobj.obj = $(this);
            workflow.config.nowselect = $(this);
            workflow.showmovediv($(this)); /*显示移动div*/
            var node = workflow.nodelist.getByElement(this);
            /*设置属性*/
            workflow.editAttribute();
            return false;
        }

        return true;
    },
    //鼠标点击节点
    nodemousedown: function (e) {
        var wflow = workflow;
        wflow.hidemovediv();
        if (wflow.config.tooltype.type == "select") {
            var mousepos = wflow.tool.mousePosition(e);
            mousepos.x = mousepos.x + $("#paintarea").scrollLeft();
            mousepos.y = mousepos.y + $("#paintarea").scrollTop();
            var thispos = wflow.tool.offset(this);

            var offsetpos = { x: (mousepos.x - thispos.left), y: (mousepos.y - thispos.top) };
            wflow.config.moveobj.offsetpos = offsetpos;
            wflow.config.moveobj.tag = 1;
            wflow.config.moveobj.ismove = true;
            wflow.showmovediv($(this)); //显示移动div
            wflow.config.moveobj.obj = $(this);
            wflow.config.nowselect = $(this);
        }
        else if (wflow.config.tooltype.type == "line") {
            wflow.config.drawline.start = null;
            wflow.config.drawline.end = null;
            wflow.config.drawline.start = $(this);

            var mousepos = wflow.tool.mouseOnPanelPosition(e);
            if (wflow.config.browser.msie) {
                $("#drawbaseline").attr("from", mousepos.x + "," + mousepos.y);
                $("#drawbaseline").attr("to", mousepos.x + "," + mousepos.y);
            }
            else {
                wflow.setNoIElinePos($("#drawbaseline"), mousepos, mousepos);
            }
            wflow.config.drawline.drawtag = true;
            wflow.config.drawline.from.x = mousepos.x;
            wflow.config.drawline.from.y = mousepos.y;
            $("#drawbaseline").show();
        }
        return false;
    },
    //鼠标弹起事件
    nodemouseup: function () {

        var wflow = workflow;
        if (wflow.config.tooltype.type == "line" && wflow.config.drawline.drawtag) {
            wflow.config.drawline.end = $(this);
            var eanbleline = wflow.eanbleline();
            if (eanbleline) { wflow.drawline(wflow.config.drawline.start, wflow.config.drawline.end); }
            wflow.config.drawline.drawtag = false;
            $("#drawbaseline").hide();
        }
        else {
            //alert("other")
        }
        //return false;
    },
    //非ie设置线的位置
    setNoIElinePos: function setpos(obj, startpos, endpos) {
        obj = this.tool.tojQuery(obj);
        var x1 = startpos.x;
        var y1 = startpos.y;
        var x2 = endpos.x;
        var y2 = endpos.y;
        var minx = Math.min(x1, x2);
        var miny = Math.min(y1, y2);

        var xoff = x2 - x1;
        var yoff = y2 - y1;
        var ay = Math.abs(parseInt(yoff / 2)) - 3 + miny;
        obj.css("top", ay + "px");
        var width = Math.pow((xoff * xoff + yoff * yoff), 0.5); //线长度
        var cos = xoff / width;
        var rad = Math.acos(cos);
        var deg = 180 / (Math.PI / rad);
        if (yoff < 0) deg = -deg;
        var ax = xoff / 2;
        ax = x1 - width / 2 + ax;
        obj.css("-webkit-transform", "rotate(" + deg + "deg);")
        obj.css("-moz-transform", "rotate(" + deg + "deg)")
        obj.width(width);
        obj.css("left", ax + "px");
    },
    /**
    * 画连接线
    * start：开始节点
    * end：结束节点
    * isimport：是否导入，导入时须将此参数设置非空
    */
    drawline: function (start, end, isimport, bind) {
        start = this.tool.tojQuery(start);
        end = this.tool.tojQuery(end);
        var lineid = start.attr("id") + "_" + end.attr("id");

        if (this.config.browser.msie && start.attr("id") != end.attr("id")) {

            var line = document.createElement("v:line");
            var jiantou = document.createElement("v:stroke");
            jiantou.setAttribute("EndArrow", "Classic");
            line.appendChild(jiantou);
            line.setAttribute("id", lineid);
            line.className = "newline";
            if (bind) {
                $(line).bind("click", this.lineclick);
            }
            var startid = start.attr("id");
            var endid = end.attr("id");
            var fromobj = this.nodelist.get(startid);
            var toobj = this.nodelist.get(endid);
            //debugger;
            if (!isimport) {
                var lineobj = new this.nodeobject[100];
                lineobj.id = lineid;
                fromobj.outputlist[fromobj.outputlist.length] = endid;
                toobj.inputlist[toobj.inputlist.length] = startid;
                this.linelist.add(lineobj);
            }
            else {
                var nowline = this.linelist.get(lineid);
                if (nowline) {
                    if ($(">div", line).length == 0) { $(line).append("<div style='position:absolute;left:50%;top:50%;overflow:visible;cursor:default;'></div>") }
                    $(">div", line).html(nowline.Text);
                }
            }
            document.getElementById("paintarea").appendChild(line);
            this.setlineposition(start.attr("id"), end.attr("id"));
        }
        else if (start.attr("id") != end.attr("id")) {
            var line = document.createElement("div");
            var jiantou = document.createElement("a");
            line.appendChild(jiantou);
            line.setAttribute("id", lineid);
            line.className = "ffline";
            if (bind) {
                $(line).bind("click", this.lineclick);
            }
            var startid = start.attr("id");
            var endid = end.attr("id");
            var fromobj = this.nodelist.get(startid);
            var toobj = this.nodelist.get(endid);

            if (!isimport) {
                var lineobj = new this.nodeobject[100];
                lineobj.id = lineid;
                fromobj.outputlist[fromobj.outputlist.length] = endid;
                toobj.inputlist[toobj.inputlist.length] = startid;
                this.linelist.add(lineobj);
            }
            else {
                var nowline = this.linelist.get(lineid);
                if (nowline) {
                    if ($(">div", line).length == 0) { $(line).append("<div></div>") }
                    $(">div", line).html(nowline.Text);
                }
            }


            document.getElementById("paintarea").appendChild(line);
            this.setlineposition(start.attr("id"), end.attr("id"));
        }
        this.resetdeawline();
    },
    toggleDiv: function (show, hide) {
        $(show).each(function (ndx, name) {
            $('#' + name).show();
        });
        $(hide).each(function (ndx, name) {
            $('#' + name).hide();
        });
    },
    /*线点击*/
    lineclick: function () {
        var wflow = workflow;
        if (wflow.config.tooltype.type == "select") {
            if (wflow.config.browser.msie) {
                wflow.hidemovediv();
                wflow.config.nowselect = $(this);
                var from = $(this).attr("from");
                var to = $(this).attr("to");
                $("#linestart").css("left", parseInt(from.x - 2) + "pt");
                $("#linestart").css("top", parseInt(from.y - 2) + "pt");
                $("#lineend").css("left", parseInt(to.x - 2) + "pt");
                $("#lineend").css("top", parseInt(to.y - 2) + "pt");
                $("#lineend").show();
                $("#linestart").show();
            }
            else {
                wflow.hidemovediv();
                wflow.config.nowselect = $(this);
                $(this).css("border", "1px dashed red");
            }
            wflow.toggleDiv(['textDiv'], ['enumDiv', 'funDiv', 'rfunDiv', 'loadDiv', 'appDiv']);
            wflow.editAttribute();
            return false;
        }
    },

    /*重置drawline参数*/
    resetdeawline: function () {
        this.config.drawline.start = null;
        this.config.drawline.end = null;
        this.config.drawline.drawtag = false;
        this.config.drawline.from = { x: 0, y: 0 };
    },
    /*检查是否可以连接*/
    eanbleline: function () {
        var start = this.config.drawline.start;
        var end = this.config.drawline.end;
        var startid = start.attr("id");
        var endid = end.attr("id");
        start = this.nodelist.get(startid);
        end = this.nodelist.get(endid);

        if (start == null || end == null || (startid == endid)) { return false }
        if (start.outputtype == 0 || (start.outputtype == 1 && start.outputlist.length >= 1)) { return false }
        if (end.inputtype == 0 || (end.inputtype == 1 && end.inputlist.length >= 1)) { return false }
        return true;
    },
    /*重新设置连线*/
    resetlineposition: function (nodeobj) {
        nodeobj = this.tool.tojQuery(nodeobj);
        var nodeid = nodeobj.attr("id");
        var node = this.nodelist.get(nodeid);
        if (node != null) {
            var left = nodeobj.css("left"); left = left.replace("px", "");
            var top = nodeobj.css("top"); top = top.replace("px", "");
            var attr = left + "," + top;
            var inputlist = node.inputlist;
            var outputlist = node.outputlist;
            /**/
            for (var i = 0; i < inputlist.length; i++) {
                var lineid = inputlist[i] + "_" + nodeid;
                //if(this.config.browser.msie){$("#"+lineid).attr("to",attr);}
                this.setlineposition(inputlist[i], nodeid);
            }
            for (var i = 0; i < outputlist.length; i++) {
                var lineid = nodeid + "_" + outputlist[i];
                //if(this.config.browser.msie){$("#"+lineid).attr("from",attr);}
                this.setlineposition(nodeid, outputlist[i]);
            }
        }
    },
    /*设置线条位置*/
    setlineposition: function (startid, endid) {
        var lineid = startid + "_" + endid;
        var start = $("#" + startid);
        var end = $("#" + endid);
        var line = $("#" + lineid);
        if (start.length <= 0 || end.length <= 0 || line.length <= 0) { return }
        var startpos = this.tool.getobjpos(start);
        var endpos = this.tool.getobjpos(end);
        var startwidth = start.width();
        var startheight = start.height();
        var endwidth = end.width();
        var endheight = end.height();

        var x1 = 0; var y1 = 0; var x2 = 0; var y2 = 0;
        if (startpos.left + startwidth < endpos.left || startpos.left > endpos.left + endwidth) {
            x1 = startpos.left + startwidth < endpos.left ? startpos.left + startwidth : startpos.left;
            x2 = startpos.left + startwidth < endpos.left ? endpos.left : endpos.left + endwidth;
        }
        else {
            var leftoffset = startpos.left > endpos.left ? startpos.left : endpos.left;
            x1 = Math.abs(startpos.left - endpos.left - endwidth) > (endwidth + startwidth) / 2 ? endpos.left - startpos.left - startwidth : startpos.left - (endpos.left + endwidth);
            x1 = Math.abs(x1) / 2 + leftoffset;
            x2 = x1;
        }
        if (startpos.top + startheight < endpos.top || startpos.top > endpos.top + endheight) {
            y1 = startpos.top + startheight < endpos.top ? startpos.top + startheight : startpos.top;
            y2 = startpos.top + startheight < endpos.top ? endpos.top : endpos.top + endheight;
        }
        else {
            var topoffset = startpos.top > endpos.top ? startpos.top : endpos.top;
            y1 = Math.abs(startpos.top - endpos.top - endheight) > (endheight + startheight) / 2 ? endpos.top - startpos.top - startheight : startpos.top - (endpos.top + endheight);
            y1 = Math.abs(y1) / 2 + topoffset;
            y2 = y1;
        }
        if (this.config.browser.msie) {
            line.attr("from", x1 + "," + y1);
            line.attr("to", x2 + "," + y2);
        }
        else {
            line.attr("x1", x1);
            line.attr("y1", y1);
            line.attr("x2", x2);
            line.attr("y2", y2);
            this.setNoIElinePos(line, { x: x1, y: y1 }, { x: x2, y: y2 });
        }
    },
    //删除节点
    delnode: function () {//this.config.nowselect
        var node = this.config.nowselect;

        if (node == null) return;
        var id = node.attr("id");
        if (id.indexOf("_") < 0) {
            var nodeobj = this.nodelist.get(id);
            nodeobj.removeAllLine();
            this.nodelist.remove(id); /*删除节点*/
            node = this.tool.tojQuery(node);
            node.remove();
            this.config.moveobj.obj = null;
            this.config.nowselect = null;
            this.config.moveobj.tag = 0;
            this.config.moveobj.ismove = false;
            this.hidemovediv();
        }
        else {
            var nodeidlist = id.split("_");
            if (nodeidlist.length < 2) { return }
            var start = this.nodelist.get(nodeidlist[0]);
            var end = this.nodelist.get(nodeidlist[1]);
            if (start != null) { start.removeOutput(nodeidlist[1]) }
            if (end != null) { end.removeInput(nodeidlist[0]) }
            this.linelist.remove(id);
            this.hidemovediv();
            node.remove();
        }
    },
    /*设置节点属性*/
    setnodeattr: function (attrlist) {
        var node = this.config.nowselect;
        node = this.tool.tojQuery(node);
        if (node.length <= 0) { return }
        var id = node.attr("id");
        var nodeobj = null;
        if (id.indexOf("_") >= 0) { nodeobj = this.linelist.get(id); } else { nodeobj = this.nodelist.get(id); }
        if (nodeobj == null) { return }

        if (id.indexOf("_") < 0) {
            for (var i = 0; i < attrlist.length; i++) {
                var attr = attrlist[i];
                nodeobj[attr.name] = attr.value;
                if (attr.text) {
                    nodeobj[attr.name + "Text"] = attr.text;
                }
                if (attr.name == "Text") { $(">.c>.icon>.tt", node).html(attr.value); }
            }
        }
        else//线属性
        {
            if (this.config.browser.msie) {
                var linenodediv = $("#" + id + " div");
                if (linenodediv.length > 0) {
                    linenodediv.html(attrlist[0].value);
                }
                else {
                    nodeobj.Text = attrlist[0].value;
                    $("#" + id).append("<div style='position:absolute;left:50%;top:50%;overflow:visible;cursor:default;'>" + attrlist[0].value + "</div>");
                }
            }
            else {
                var linenodediv = $("#" + id + " div");
                if (linenodediv.length > 0) {
                    linenodediv.html(attrlist[0].value);
                }
                else {
                    nodeobj.Text = attrlist[0].value;
                    $("#" + id).append("<div style='position:absolute;left:50%;top:50%;overflow:visible;cursor:default;'>" + attrlist[0].value + "</div>");
                }
            }
        }
    },
    //显示移动div
    showmovediv: function (obj) {
        $("#linestart").hide(); $("#lineend").hide();
        $(".node").css("cursor", "default"); obj = this.tool.tojQuery(obj); obj.css("cursor", "move"); var paintpost = this.config.paintpos; var objpos = workflow.tool.offset(obj); var oleft = objpos.left; var otop = objpos.top;
        var owidth = obj.width(); var owidth2 = parseInt(owidth / 2); var oheight = obj.height(); var oheight2 = parseInt(oheight / 2); var os = 2; var resize = 4; var l = oleft - paintpost.left; var t = otop - paintpost.top;
        $("#resizelt").css("left", l - (resize + os) + "px"); $("#resizelt").css("top", t - (resize + os) + "px"); $("#resizel").css("left", l - (resize + os) + "px"); $("#resizel").css("top", t - resize / 2 + oheight2 + "px"); $("#resizeb").css("left", l + (owidth2 - resize / 2) + "px"); $("#resizeb").css("top", t + oheight + "px"); $("#resizet").css("left", l + (owidth2 - resize / 2) + "px"); $("#resizet").css("top", t - (resize + os) + "px"); $("#resizelb").css("left", l - (resize + os) + "px"); $("#resizelb").css("top", t + oheight + "px"); $("#resizer").css("left", l + owidth - 4 + "px"); $("#resizer").css("top", t - resize / 2 + oheight2 + "px"); $("#resizert").css("left", l + owidth - 4 + "px"); $("#resizert").css("top", t - (resize + os) + "px"); $("#resizerb").css("left", l + owidth - 4 + "px"); $("#resizerb").css("top", t + oheight + "px");
        $("#resizelt").show(); $("#resizel").show(); $("#resizelb").show(); $("#resizet").show(); $("#resizeb").show(); $("#resizer").show(); $("#resizert").show(); $("#resizerb").show();
    },
    //隐藏移动div
    hidemovediv: function () { this.config.moveobj.obj = null; this.config.nowselect = null; $(".ffline").css("border", "none"); $(".node").css("cursor", "default"); $("#resizelt").hide(); $("#resizel").hide(); $("#resizelb").hide(); $("#resizet").hide(); $("#resizeb").hide(); $("#resizer").hide(); $("#resizert").hide(); $("#resizerb").hide(); $("#linestart").hide(); $("#lineend").hide(); },

    /*编辑属性*/
    editAttribute: function () {
        var node = this.config.nowselect;
        if (node == null) { return }
        node = this.tool.tojQuery(node);
        var id = node.attr("id");
        if (id.indexOf("_") < 0)/*元素节点*/
        {
            var nodeobj = this.nodelist.get(id);
            //1.开始,2 结束,3 分支. 4 合并 5 任务
            if (nodeobj.nodetype == 1) {
                this.toggleDiv(['textDiv'], ['enumDiv', 'funDiv', 'rfunDiv', 'loadDiv', 'appDiv']);
            } else if (nodeobj.nodetype == 2) {
                this.toggleDiv(['textDiv', 'funDiv', 'rfunDiv'], ['enumDiv', 'loadDiv', 'appDiv']);
            } else if (nodeobj.nodetype == 3) {
                this.toggleDiv(['textDiv', 'funDiv', 'enumDiv'], ['rfunDiv', 'loadDiv', 'appDiv']);
            } else if (nodeobj.nodetype == 4) {
                this.toggleDiv(['textDiv'], ['enumDiv', 'funDiv', 'rfunDiv', 'loadDiv', 'appDiv']);
            } else if (nodeobj.nodetype == 5) {
                this.toggleDiv(['textDiv', 'enumDiv', 'funDiv', 'rfunDiv', 'loadDiv', 'appDiv']);
            }
            if (!$("#attribute1 #txtText").is(":hidden")) {
                $("#txtText").val(nodeobj.Text || "");
            }
            if (!$("#attribute1 #txtEnumValue").is(":hidden")) {
                $("#txtEnumValue").val(nodeobj.EnumValue || "");
            }
            if (!$("#attribute1 #txtFuncName").is(":hidden")) {
                $("#txtExeFuncID").val(nodeobj.ExeFuncIDText || "");
                $("#hidExeFuncID").val(nodeobj.ExeFuncID || "");
            }
            if (!$("#attribute1 #txtReFuncName").is(":hidden")) {
                $("#txtExeReFuncID").val(nodeobj.ExeReFuncIDText || "");
                $("#hidExeReFuncID").val(nodeobj.ExeReFuncID || "");
            }
            if (!$("#attribute1 #txtLoadFilePath").is(":hidden")) {
                $("#txtLoadFilePath").val(nodeobj.LoadFilePath || "");
            }
            if (!$("#attribute1 #txtUserID").is(":hidden")) {
                $("#ddlUserType").val(nodeobj.UserType || "");
                $("#ddlUserType").trigger('change');
                $("#txtUserID").val(nodeobj.UserIDText || "");
                $("#hidUserID").val(nodeobj.UserID || "");

            }
        }
        else/*连线节点*/
        {
            var nodeobj = this.linelist.get(id.trim());
            if (nodeobj && nodeobj.Text)
                $("#attrtext").val(nodeobj.Text);
            else
                $("#attrtext").val("");
        }
    },
    //XML生成、解析
    xmlobject: null,
    getXml: function () {
        if (this.xmlobject == null || this.xmlobject.checkNode == null) {
            return;
        }
        var res = this.xmlobject.checkNode();
        if (res) {
            return this.xmlobject.createXml();
        }
    },
    //导入xml
    parseXml: function (xml, bind) {
        if (!xml) return;
        if (this.xmlobject != null && this.xmlobject.parseXml != null) {
            var listobj = this.xmlobject.parseXml(xml);
            //return;
            if (listobj != null && listobj.nodelist != null && listobj.linelist != null) {
                var nodelist = listobj.nodelist;
                var linelist = listobj.linelist;
                this.nodelist.list = nodelist;
                this.linelist.list = linelist;
                //未完待续
                //绘制节点
                for (var i = 0; i < this.nodelist.list.length; i++) {
                    var nodeobj = this.nodelist.list[i];

                    var node = this.config.demonode.clone();
                    node.css("left", nodeobj.x);
                    node.css("top", nodeobj.y);
                    node.css("z-index", nodeobj.zindex);
                    node.attr("id", nodeobj.id);
                    node.height(nodeobj.height);
                    node.width(nodeobj.width);
                    if (nodeobj.State == 1) {
                        $(">.c", node).addClass('c1');
                        $(">.l", node).addClass('l1');
                        $(">.r", node).addClass('r1');
                    }
                    $(">.c>.icon>.t", node).html("&lt;&lt;" + nodeobj.nodetext + "&gt;&gt;&nbsp;");
                    $(">.c>.icon>.tt", node).html(nodeobj.Text);
                    $(">.c>.icon", node).width(nodeobj.width - 10);
                    $(">.c>.icon", node).addClass(nodeobj.icon);
                    $(">.c>.icon", node).addClass("nodeicon");
                    if (bind) {
                        node.bind("click", this.nodeclick);
                        node.bind("mousedown", this.nodemousedown);
                        node.bind("mouseup", this.nodemouseup);
                    }
                    $("#paintarea").append(node);
                }

                for (var i = 0; i < this.nodelist.list.length; i++) {
                    var start = this.nodelist.list[i];

                    //drawline
                    for (var j = 0; j < start.outputlist.length; j++) {
                        var endid = start.outputlist[j];
                        var end = $("#" + endid);
                        this.drawline(start, end, 1, bind);
                    }

                }


            }
        }
    }
}

//保存属性
function saveattribute1() {
    var attrlist = new Array();
    var length = 0;
    if (!$("#attribute1 #textDiv").is(":hidden")) {
        var value = $("#attribute1 #txtText").val();
        attrlist[length] = { name: "Text", value: value };
        length++;
    };
    if (!$("#attribute1 #enumDiv").is(":hidden")) {
        var value = $("#attribute1 #txtEnumValue").val();
        attrlist[length] = { name: "EnumValue", value: value };
        length++;
    }
    if (!$("#attribute1 #funDiv").is(":hidden")) {
        var text = $("#attribute1 #txtExeFuncID").val();
        var value = $("#attribute1 #hidExeFuncID").val();
        attrlist[length] = { name: "ExeFuncID", value: value, text: text };
        length++;
    }
    if (!$("#attribute1 #rfunDiv").is(":hidden")) {
        var text = $("#attribute1 #txtExeReFuncID").val();
        var value = $("#attribute1 #hidExeReFuncID").val();
        attrlist[length] = { name: "ExeReFuncID", value: value, text: text };
        length++;
    }
    if (!$("#attribute1 #loadDiv").is(":hidden")) {
        var value = $("#attribute1 #txtLoadFilePath").val();
        attrlist[length] = { name: "LoadFilePath", value: value };
        length++;
    }
    if (!$("#attribute1 #appDiv").is(":hidden")) {
        var text = $("#attribute1 #txtUserID").val();
        var value = $("#attribute1 #hidUserID").val();
        attrlist[length] = { name: "UserID", value: value, text: text };
        length++;
        var type = $('#attribute1 #ddlUserType').val();
        attrlist[length] = { name: "UserType", value: type };
        length++;
    }
    workflow.setnodeattr(attrlist);

}

//显示隐藏属性栏,tag=1:显示
function hideattribute(tag) { if (tag == 1) { $("#attributecontent").show(); $("#attribute").css("height", "auto"); } else { $("#attribute").css("height", "auto"); $("#attributecontent").hide(); } workflow.resize(); }




var xmlobject = {
    nodelist: null, //节点列表
    linelist: null, //连线列表
    createXml: function () {
        var border = document.createElement("div");
        var root = document.createElement("root");

        border.appendChild(root);
        for (var i = 0; i < this.nodelist.size(); i++) {
            var node = this.nodelist.getByIndex(i);
            var data = document.createElement("node");
            for (var key in node) {
                if (key == "inputlist" ||
                 key == "outputlist" ||
                 key == "inputtype" ||
                 key == "outputtype" ||
                 key == "icon") {
                    continue;
                }
                var value = node[key];
                if ($.isFunction(value)) {
                    continue;
                }
                if (value && value != "" && isNaN(value) && typeof (value) == 'string') {
                    value = value.trim();
                    value += " ";
                }
                if (value && value != "") {
                    data.setAttribute(key, value);
                }
            }

            var parentlist = document.createElement("parentlist");
            var childlist = document.createElement("childlist");
            for (var j = 0; j < node.inputlist.length; j++) {
                var parentid = document.createElement("parentid");
                parentid.innerHTML = node.inputlist[j];
                parentlist.appendChild(parentid);
            }
            for (var j = 0; j < node.outputlist.length; j++) {
                var childid = document.createElement("childid");
                childid.innerHTML = node.outputlist[j];
                childlist.appendChild(childid);
            }
            data.appendChild(parentlist);
            data.appendChild(childlist);
            root.appendChild(data);
        }
        var linelist = document.createElement("linelist");
        root.appendChild(linelist);
        for (var i = 0; i < this.linelist.size(); i++) {
            var lineobj = this.linelist.getByIndex(i);
            var line = document.createElement("line");
            var text = lineobj.Text;
            if (!text) { text = ""; }
            line.innerHTML = text;
            line.setAttribute("id", lineobj.id.trim() + " ");
            linelist.appendChild(line);
        }
        return border.innerHTML;
    },
    /**
    * 解析xml
    * xml：xml字符串
    * return {nodelist:节点列表,linelist:连线列表}
    */
    parseXml: function (xmlobj) {
        var nodelist = new Array();
        var linelist = new Array();
        var roots = xmlobj.getElementsByTagName("root");
        if (roots.length > 0) {
            var root = roots[0];
            var nodes = root.getElementsByTagName("node");
            for (var i = 0; i < nodes.length; i++) {
                var node = nodes[i];
                var nodeobj = null;
                for (var s = 0; s < node.attributes.length; s++) {
                    if (nodeobj == null) {
                        var nodetype = node.getAttribute('nodetype');
                        nodeobj = workflow.nodeobject.getNode(nodetype.trim());
                    }
                    var name = node.attributes[s].name.trim();
                    var value = node.attributes[s].value.trim();
                    if (name != "nodetype") {
                        //                        if (!isNaN(value) && value) {
                        //                            value = parseInt(value);
                        //                        }
                        nodeobj[name] = value;
                    }
                }
                var parentlist = node.getElementsByTagName("parentlist");
                if (parentlist.length > 0) {
                    parentlist = parentlist[0];
                    var parentids = parentlist.childNodes;
                    for (var j = 0; j < parentids.length; j++) {
                        var parentid = parentids[j];
                        parentid = parentid.text || parentid.textContent;
                        nodeobj.inputlist[nodeobj.inputlist.length] = parentid.trim();
                    }
                }
                var childlist = node.getElementsByTagName("childlist");
                if (childlist.length > 0) {
                    childlist = childlist[0];
                    var childids = childlist.childNodes;
                    for (var j = 0; j < childids.length; j++) {
                        var childid = childids[j];
                        childid = childid.text || childid.textContent;
                        nodeobj.outputlist[nodeobj.outputlist.length] = childid.trim();
                    }
                }
                nodelist[nodelist.length] = nodeobj;
            }


            var linelists = root.getElementsByTagName("linelist");
            if (linelists.length > 0) {
                linelists = linelists[0];
                var lines = linelists.getElementsByTagName("line");
                for (var i = 0; i < lines.length; i++) {
                    var line = lines[i];
                    var lineobj = new workflow.nodeobject[100];
                    lineobj.id = line.getAttribute("id").trim();
                    lineobj.Text = line.text || line.textContent;
                    linelist[linelist.length] = lineobj;
                }
            }
        }
        return { linelist: linelist, nodelist: nodelist };
    },
    //检查
    checkNode: function () {
        var start = 0; /*输入节点数量*/
        var end = 0; /*输出节点数量*/
        var inputnull = 0; /*空输入数量*/
        var outputnull = 0; /*空输出数量*/
        var othererror = 0; /*输入类型和输入节点数量不匹配数量*/

        for (var i = 0; i < this.nodelist.size(); i++) {
            var node = this.nodelist.getByIndex(i);
            if (node != null) {
                /*计算开始、结束节点数量*/
                if (node.nodetype == 1) { start++; } else if (node.nodetype == 2) { end++; }
                /*没有输入节点的数量*/
                if (node.inputtype > 0 && node.inputlist.length == 0) { inputnull++; }
                /*没有输出节点的数量*/
                if (node.outputtype > 0 && node.outputlist.length == 0) { outputnull++; }
                /*输入、输出节点类型和节点数量不匹配*/
                if (node.inputtype == 1 && node.inputlist.length != 1) { othererror++ } else if (node.inputtype == 0 && node.inputlist.length != 0) { othererror++ } if (node.outputtype == 1 && node.outputlist.length != 1) { othererror++ } else if (node.outputtype == 0 && node.outputlist.length != 0) { othererror++ }
            }
        }
        if (start == 1 && end >= 1 && inputnull == 0 && outputnull == 0 && othererror == 0) { return true }
        var str = "错误信息";
        str += "\n输入节点数量：" + start;
        str += "\n输出节点数量：" + end;
        str += "\n空输入数量：" + inputnull;
        str += "\n空输出数量：" + outputnull;
        str += "\n输入类型和输入、输出数量不匹配：" + othererror;
        alert(str);
        return false;
    }
}

function createXml(str) {
    if (document.all) {
        try {
            var xmlDom = new ActiveXObject("Microsoft.XMLDOM")
            xmlDom.loadXML(str);
            if (xmlDom.parseError.reason) {
                alert(xmlDom.parseError.reason);
                return null;
            }
            return xmlDom;
        } catch (e) {
            alert(e);
        }
    }
    else
        return new DOMParser().parseFromString(str, "text/xml")
}
String.prototype.trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, "");
}