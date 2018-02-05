/**************************************************
 * 弹出层
 * 2009.5.26
 * creat by zhengchong
 **************************************************/
var wiw={
    width:350,
    height:250,
    el:'',
    okClick:'',
    onClose:'',
    title:'提示',
    okText:'确定',
    cleText:'关闭',
    delok:false,//判断是否只留下关闭按纽
    aType:0,   //1:失败，2:成功，3:警告，4:帮助，
    msg:'',  //文字提示
    isHid:false,
    hTime:3000,
    upload:false,
    //创建遮罩层
    _creatMask : function(mType){
		var sWidth,sHeight;
		sWidth=this._windowWidth();
		sHeight=this._windowHeight();
        if (window.ActiveXObject){
            sHeight=sHeight;
        } 


		//创建遮罩背景
		var maskObj = document.createElement("div");
		maskObj.setAttribute('id','maskDiv'+mType);
		maskObj.style.position = "absolute";
		maskObj.style.top = "0";
		maskObj.style.left = "0";
		maskObj.style.background = "#777";
		maskObj.style.filter = "Alpha(opacity=30);";
		maskObj.style.opacity = "0.3";
		maskObj.style.width = sWidth + "px";
		maskObj.style.height = sHeight + "px";
		maskObj.style.zIndex = "10000";
		document.body.appendChild(maskObj);
		var maskifr = document.createElement("iframe");
		maskifr.setAttribute("id",'ifr'+mType);
		maskifr.style.position = "absolute";
		maskifr.style.top = "0";
		maskifr.style.left = "0";	
		maskifr.style.filter = "Alpha(opacity=0);";
		maskifr.style.opacity = "0";
		maskifr.style.width = sWidth-15 + "px";
		maskifr.style.height = sHeight-15 + "px";
		maskifr.style.zIndex = "9999";	
		document.body.appendChild(maskifr);		
    },
    _reset : function(){
        this.title='提示';
        this.okText='确定';
        this.cleText='关闭';
        this.delok=false;
        this.aType=0;
        this.isHid=false;
    },      
    //创建弹出div
    show : function(){
        this._creatMask('win');
        var _this=this;
        var iHtml=document.getElementById(_this.el);
        var c_height = _this.height + 'px';
        var c_width = _this.width + 'px';
        var vheight=(parseInt(c_height)-120);
        var vwidth=(parseInt(c_height)-30);
        if(vheight)
		var q_heihgt = parseInt(c_height) + 12 + 'px';
		var w_height = parseInt(c_height) + 76 + 'px';
		var e_height = parseInt(c_height) + 78 + 'px';
		var r_height = parseInt(c_height) + 80 + 'px';
		var q_width = parseInt(c_width) + 12 + 'px';
		var w_width = parseInt(c_width) + 24 + 'px';
		var e_width = parseInt(c_width) + 26 + 'px';
		var r_width = parseInt(c_width) + 28 + 'px';
		
        var iWidth=this._pageWidth();
        var iHeight=this._pageHeight();
		var ileft = this._leftPosition();
		var itop = this._topPosition();     
        var wWidth=parseInt(c_width) + 28;
        var wHeight=parseInt(c_height) + 80;
                
		var toppos = itop + (iHeight / 2) - (wHeight / 2) + 'px';
		var leftpos = ileft + (iWidth / 2) - (wWidth / 2) + 'px';
		
		var windiv=document.createElement("div");
		windiv.setAttribute("id","windiv");
		var il ="";
        il=il+"<div class='c-windows' id='c-window' style='height:"+r_height+";width:"+r_width+";left:"+leftpos+"; top:"+toppos+";'>";
        il=il+"<div class='c-a' style='height:"+e_height+"; width:"+e_width+";'>";
        il=il+"<div class='c-b' style='height:"+w_height+";width:"+w_width+";'>";
        il=il+"<div class='c-handler' id='c-handler' style='width:"+w_width+";'>"
        il=il+"<div class='c-ha'>";
        il=il+"<div class='c-title'>"+_this.title+"</div>";
        il=il+"<div class='c-close' onclick=\"wiw.close();\" title='关闭' onmouseover=\"this.className='c-closec'\" onmouseout=\"this.className='c-close'\"></div>";
        il=il+"<div class='c-clear'></div>";
        il=il+"</div>";
        il=il+"</div>";
        il=il+"<div class='c-cont'>";
        il=il+"<div class='c-cta' style='height:"+q_heihgt+";width:"+q_width+";'>";
        il=il+"<div class='c-ctb'>";
        il=il+"<div class='c-ctc' style='height:"+c_height+";width:"+c_width+";' id='ghtml'>";
        il=il+$(iHtml).html();
        il=il+"</div>";
        il=il+"</div>";
        il=il+" </div>";
        il=il+"<div class='c-btns'>";
		if(_this.delok){
            //il=il+"<input onmouseover=\"this.className='c-btnc'\" onmouseout=\"this.className='c-btn'\" onclick=\"wiw.close();\" class='c-btn' value=' "+_this.cleText+" ' type='button' />";
		}else{
            il=il+"<input onmouseover=\"this.className='c-btnc'\" onclick='"+_this.okClick+";' onmouseout=\"this.className='c-btn'\" class='c-btn' value=' "+_this.okText+" ' type='button' />";
        }
        if(_this.onClose.length>0){
            il=il+"<input onmouseover=\"this.className='c-btnc'\" onmouseout=\"this.className='c-btn'\" onclick='"+_this.onClose+";wiw.close();' class='c-btn' value=' "+_this.cleText+" ' type='button' />";
        }else{
            il=il+"<input onmouseover=\"this.className='c-btnc'\" onmouseout=\"this.className='c-btn'\" onclick=\"wiw.close();\" class='c-btn' value=' "+_this.cleText+" ' type='button' />";
        }
		        
        il=il+"</div>";
        il=il+"</div>";
        il=il+"</div>";
        il=il+"</div>";
        il=il+"</div>";
        $(windiv).html(il);
        $(iHtml).html("");
        document.body.appendChild(windiv);
        var Hander = document.getElementById("c-handler");
        var iWin= document.getElementById("c-window");
		var dWidth,dHeight;
		var scrollT = document.documentElement.scrollTop;
//        if (window.ActiveXObject){
//            dWidth=iWidth - wWidth-2;
//        }else{
//            dWidth=iWidth - wWidth-18;
//        }	
        dHeight=this._windowHeight();
		dHeight=dHeight - wHeight;
        if(_this.isHid){
            setTimeout("wiw.close()",_this.hTime);
        }
		this._reset();
        Drag.init(Hander,iWin,0,null,0,null);

    },
    Alert : function(){
            this._creatMask('alert');
            var _this=this;
            var ImgStyle="";
            switch(_this.aType){
              case 1:
              ImgStyle="errimg";
              break;
              case 2:
              ImgStyle="okimg";
              break;  
              case 3:
              ImgStyle="infoimg";
              break; 
              case 4:
              ImgStyle="askimg";
              break;               
              default:
              ImgStyle="";
            }
            
            var alertdiv=document.createElement("div");
            alertdiv.setAttribute('id','alertdiv');
            var ahm="";
            ahm=ahm+"<div class='alert' id='alert_win'>";
            ahm=ahm+"<div class='qalert'>";
            ahm=ahm+"<div class='walert'>";
            ahm=ahm+"<div class='ahandler' id='alert_hand'>";
            ahm=ahm+"<div class='aha'>";
            ahm=ahm+"<div class='atitle'>"+_this.title+"</div>";
            ahm=ahm+"<div onmouseover=\"this.className='aclosec'\" onclick=\"wiw.Aclose();\" onmouseout=\"this.className='aclose'\" title='关闭' class='aclose'></div>";
            ahm=ahm+"<div class='aclear'></div>"
            ahm=ahm+"</div>";
            ahm=ahm+"</div>";
            ahm=ahm+"<div class='acontent'>";
            ahm=ahm+"<div class='aqcontent'>";
            if(_this.aType!=0){
                ahm=ahm+"<div class='"+ImgStyle+"'></div>";
            }
            ahm=ahm+"<div id='c_content_div' class='atdiv'>";
            ahm=ahm+"<span id='c_content_span'>"+_this.msg+"</span>"
            ahm=ahm+"</div>"
            ahm=ahm+"<div class='aclear'></div>";
            ahm=ahm+"</div>";
            ahm=ahm+"</div>";
            ahm=ahm+"<div class='btnarea'>";
            if(_this.aType==3){
            ahm=ahm+"<input onmouseover=\"this.className='abtnc'\" onclick='"+_this.okClick+";' onmouseout=\"this.className='abtn'\" class='abtn' value=' "+_this.okText+" ' onclick=\"wiw.Aclose();\" type='button' />";
            }
            ahm=ahm+"<input onmouseover=\"this.className='abtnc'\" onmouseout=\"this.className='abtn'\" class='abtn' value=' "+_this.cleText+" ' onclick=\"wiw.Aclose();\" type='button' />";
            ahm=ahm+"</div>";
            ahm=ahm+"</div>";
            ahm=ahm+"</div>"
            ahm=ahm+"</div>";   
            alertdiv.innerHTML=ahm;
            document.body.appendChild(alertdiv);
            var awin=document.getElementById("alert_win");
            var c_con=document.getElementById("c_content_div");
            var c_span=document.getElementById("c_content_span");
            var wWidth=0;     
            if(c_span)
            {
                wWidth=c_span.offsetWidth+70;
                if(wWidth<200){
                    wWidth=200;
                }
                if(wWidth>500){
                    wWidth=500;
                }

            }
            var iWidth=this._pageWidth();
            var iHeight=this._pageHeight();
            var ileft = this._leftPosition();
            var itop = this._topPosition();     

            var wHeight=awin.clientHeight;

            var toppos = itop + (iHeight / 2) - (wHeight / 2)-50 + 'px';
            var leftpos = ileft + (iWidth / 2) - (wWidth / 2) + 'px';  
            
            awin.style.width=wWidth+'px';
            c_con.style.width=wWidth-30+'px';
            awin.style.left=leftpos ;           
            awin.style.top=toppos ;

            var Hander = document.getElementById("alert_hand");
            var iWin= document.getElementById("alert_win");
            var dWidth,dHeight;
            var scrollT = document.documentElement.scrollTop;
            if (window.ActiveXObject){
                dWidth=iWidth - wWidth-2;
            }else{
                dWidth=iWidth - wWidth-18;
            }	
            dHeight=this._windowHeight();
            dHeight=dHeight - wHeight;
            if(_this.isHid){
                setTimeout("wiw.Aclose()",_this.hTime);
            }	            
            this._reset();
            Drag.init(Hander,iWin,0,null,0,null);          
    },
    //关闭弹出层
    close : function(){
        var maskObj=document.getElementById("maskDivwin");
        var winObj=document.getElementById("windiv");
        var ifrObj=document.getElementById("ifrwin");
        var ghtml= $("#ghtml");//document.getElementById("ghtml");
        var _this=this;
        var iHtml=document.getElementById(_this.el);
        if(iHtml!=undefined){
            iHtml.innerHTML= ghtml.html();//ghtml.innerHTML;
        }
        if(maskObj){
            document.body.removeChild(maskObj);
        }
        if(winObj){
            document.body.removeChild(winObj);
        }   
        if(ifrObj){
            document.body.removeChild(ifrObj);
        }  
        if(_this.upload){
            var divObj=document.getElementById("uploaddiv");
            if(divObj){
                document.body.removeChild(divObj);
            }
        }            
    },
    Aclose : function(){
        var maskObj=document.getElementById("maskDivalert");
        var winObj=document.getElementById("alertdiv");
        var ifrObj=document.getElementById("ifralert");
        if(maskObj){
            document.body.removeChild(maskObj);
        }
        if(winObj){
            document.body.removeChild(winObj);
        }
        if(ifrObj){
            document.body.removeChild(ifrObj);
        }                 
    },  
    _windowWidth : function(){
        var rootEl = document.compatMode == 'CSS1Compat' ? document.documentElement: document.body;
        var sWidth = Math.max(rootEl.scrollWidth,rootEl.clientWidth);
        return sWidth;
    },
   _windowHeight : function(){
        var rootEl = document.compatMode == 'CSS1Compat' ? document.documentElement: document.body;
        var sHeight = Math.max(rootEl.scrollHeight,rootEl.clientHeight);
        return sHeight;
    },  
    //计算当前窗口的宽度
    _pageWidth : function() {
        return window.innerWidth != null ? window.innerWidth : document.documentElement && document.documentElement.clientWidth ? document.documentElement.clientWidth : document.body != null ? document.body.clientWidth : null;
    },
    //计算当前窗口的高度
    _pageHeight :function() {
        return window.innerHeight != null? window.innerHeight : document.documentElement && document.documentElement.clientHeight ? document.documentElement.clientHeight : document.body != null? document.body.clientHeight : null;
    },
    //计算当前窗口的上边滚动条
    _topPosition : function(){
		return typeof window.pageYOffset != 'undefined' ? window.pageYOffset : document.documentElement && document.documentElement.scrollTop ? document.documentElement.scrollTop : document.body.scrollTop ? document.body.scrollTop : 0;
    },
    //计算当前窗口的左边滚动条
    _leftPosition : function(){
		return typeof window.pageXOffset != 'undefined' ? window.pageXOffset : document.documentElement && document.documentElement.scrollLeft ? document.documentElement.scrollLeft : document.body.scrollLeft ? document.body.scrollLeft : 0;
    }
}

var Drag = {
	obj : null,
	init : function(o, oRoot, minX, maxX, minY, maxY, bSwapHorzRef, bSwapVertRef, fXMapper, fYMapper)
	{
		o.onmousedown = Drag.start;
		o.hmode = bSwapHorzRef ? false : true ;
		o.vmode	= bSwapVertRef ? false : true ;
		o.root = oRoot && oRoot != null ? oRoot : o ;
		if (o.hmode  && isNaN(parseInt(o.root.style.left))) o.root.style.left   = "0px";
		if (o.vmode  && isNaN(parseInt(o.root.style.top))) o.root.style.top    = "0px";
		if (!o.hmode && isNaN(parseInt(o.root.style.right))) o.root.style.right  = "0px";
		if (!o.vmode && isNaN(parseInt(o.root.style.bottom))) o.root.style.bottom = "0px";
		o.minX	= typeof minX != 'undefined' ? minX : null;
		o.minY	= typeof minY != 'undefined' ? minY : null;
		o.maxX	= typeof maxX != 'undefined' ? maxX : null;
		o.maxY	= typeof maxY != 'undefined' ? maxY : null;
		o.xMapper = fXMapper ? fXMapper : null;
		o.yMapper = fYMapper ? fYMapper : null;
		o.root.onDragStart	= new Function();
		o.root.onDragEnd = new Function();
		o.root.onDrag = new Function();
	},
	start : function(e)
	{
		var o = Drag.obj = this;
		e = Drag.fixE(e);
		var y = parseInt(o.vmode ? o.root.style.top  : o.root.style.bottom);
		var x = parseInt(o.hmode ? o.root.style.left : o.root.style.right );
		o.root.onDragStart(x, y);
		o.lastMouseX = e.clientX;
		o.lastMouseY = e.clientY;
		if (o.hmode) {
			if (o.minX != null)	o.minMouseX	= e.clientX - x + o.minX;
			if (o.maxX != null)	o.maxMouseX	= o.minMouseX + o.maxX - o.minX;
		} else {
			if (o.minX != null) o.maxMouseX = -o.minX + e.clientX + x;
			if (o.maxX != null) o.minMouseX = -o.maxX + e.clientX + x;
		}
		if (o.vmode) {
			if (o.minY != null)	o.minMouseY	= e.clientY - y + o.minY;
			if (o.maxY != null)	o.maxMouseY	= o.minMouseY + o.maxY - o.minY;
		} else {
			if (o.minY != null) o.maxMouseY = -o.minY + e.clientY + y;
			if (o.maxY != null) o.minMouseY = -o.maxY + e.clientY + y;
		}
		document.onmousemove	= Drag.drag;
		document.onmouseup		= Drag.end;
		return false;
	},

	drag : function(e)
	{
		e = Drag.fixE(e);
		var o = Drag.obj;
		var ey	= e.clientY;
		var ex	= e.clientX;
		var y = parseInt(o.vmode ? o.root.style.top  : o.root.style.bottom);
		var x = parseInt(o.hmode ? o.root.style.left : o.root.style.right );
		var nx, ny;
		if (o.minX != null) ex = o.hmode ? Math.max(ex, o.minMouseX) : Math.min(ex, o.maxMouseX);
		if (o.maxX != null) ex = o.hmode ? Math.min(ex, o.maxMouseX) : Math.max(ex, o.minMouseX);
		if (o.minY != null) ey = o.vmode ? Math.max(ey, o.minMouseY) : Math.min(ey, o.maxMouseY);
		if (o.maxY != null) ey = o.vmode ? Math.min(ey, o.maxMouseY) : Math.max(ey, o.minMouseY);
		nx = x + ((ex - o.lastMouseX) * (o.hmode ? 1 : -1));
		ny = y + ((ey - o.lastMouseY) * (o.vmode ? 1 : -1));
		if (o.xMapper) nx = o.xMapper(y)
		else if (o.yMapper)	ny = o.yMapper(x)
		Drag.obj.root.style[o.hmode ? "left" : "right"] = nx + "px";
		Drag.obj.root.style[o.vmode ? "top" : "bottom"] = ny + "px";
		Drag.obj.lastMouseX	= ex;
		Drag.obj.lastMouseY	= ey;
		Drag.obj.root.onDrag(nx, ny);
		return false;
	},

	end : function()
	{
		document.onmousemove = null;
		document.onmouseup   = null;
		Drag.obj.root.onDragEnd(parseInt(Drag.obj.root.style[Drag.obj.hmode ? "left" : "right"]), parseInt(Drag.obj.root.style[Drag.obj.vmode ? "top" : "bottom"]));
		Drag.obj = null;
	},

	fixE : function(e)
	{
		if (typeof e == 'undefined') e = window.event;
		if (typeof e.layerX == 'undefined') e.layerX = e.offsetX;
		if (typeof e.layerY == 'undefined') e.layerY = e.offsetY;
		return e;
	}
}