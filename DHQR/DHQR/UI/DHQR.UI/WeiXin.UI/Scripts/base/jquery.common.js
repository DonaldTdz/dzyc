Function.prototype.bind = function(o) {
    var _f = this;
    return function() {
        return _f.apply(o, arguments);
    };
};

$.fn.outer = function() {
    return $('<div></div>').append(this.eq(0).clone()).html();
};

jQuery.extend(
 {
     evalJSON: function(strJson) {
         return eval("(" + strJson + ")");
     },
     toJSON: function(object) {
         var type = typeof object;
         if ('object' == type) {
             if (Array == object.constructor)
                 type = 'array';
             else if (RegExp == object.constructor)
                 type = 'regexp';
             else
                 type = 'object';
         }
         switch (type) {
             case 'undefined':
             case 'unknown':
                 return;
                 break;
             case 'function':
             case 'boolean':
             case 'regexp':
                 return object.toString();
                 break;
             case 'number':
                 return isFinite(object) ? object.toString() : 'null';
                 break;
             case 'string':
                 return '"' + object.replace(/(\\|\")/g, "\\$1").replace(/\n|\r|\t/g,
       function() {
           var a = arguments[0];
           return (a == '\n') ? '\\n' :
                       (a == '\r') ? '\\r' :
                       (a == '\t') ? '\\t' : ""
       }) + '"';
                 break;
             case 'object':
                 if (object === null) return 'null';
                 var results = [];
                 for (var property in object) {
                     if (object[property] != null) {
                         var value = jQuery.toJSON(object[property]);
                         if (value !== undefined) {
                             results.push(jQuery.toJSON(property) + ':' + value);
                         }
                     } else {
                         results.push(property + ':null');
                     }
                 }
                 return '{' + results.join(',') + '}';
                 break;
             case 'array':
                 var results = [];
                 for (var i = 0; i < object.length; i++) {
                     var value = jQuery.toJSON(object[i]);
                     if (value !== undefined) results.push(value);
                 }
                 return '[' + results.join(',') + ']';
                 break;
         }
     }
 });


 $.fieldValue = function(el, successful) {
     var n = el.name, t = el.type, tag = el.tagName.toLowerCase();
     if (typeof successful == 'undefined') successful = true;

     if (successful && (!n || el.disabled || t == 'reset' || t == 'button' ||
        (t == 'checkbox' || t == 'radio') && !el.checked ||
        (t == 'submit' || t == 'image') && el.form && el.form.clk != el ||
        tag == 'select' && el.selectedIndex == -1))
         return null;

     if (tag == 'select') {
         var index = el.selectedIndex;
         if (index < 0) return null;
         var a = [], ops = el.options;
         var one = (t == 'select-one');
         var max = (one ? index + 1 : ops.length);
         for (var i = (one ? index : 0); i < max; i++) {
             var op = ops[i];
             if (op.selected) {
                 var v = op.value;
                 if (!v) 
                     v = (op.attributes && op.attributes['value'] && !(op.attributes['value'].specified)) ? op.text : op.value;
                 if (one) return v;
                 a.push(v);
             }
         }
         return a;
     }
     return el.value;
 };

 $.fn.setFieldValue = function(sValue) {

     return this.each(function() {
         var t = this.type, tag = this.tagName.toLowerCase(),eEl=this;
         
          alert(t);
          
         switch (t) {
             case 'hidden':
             case 'text':
             case 'file':
             case 'password':
             case 'textarea':
                 this.value = sValue;
                 break;
             case 'checkbox':                 
                 var oReg = new RegExp('(^|,) + eEl.value + (,|$)', 'g');
                 eEl.checked = oReg.test(sValue);
                 break;
             case 'radio':
                 if (eEl.value == sValue)
                     eEl.checked = true;
                 break;
             case 'select-one':          
                 for (var j = 0; j < eEl.options.length; j++) {
                     if (eEl.options[j].value == sValue) {
                         eEl.options[j].selected = true;
                         break;
                     }
                 }
                 break;
         }
     });
 };

 $.fn.clearForm = function() {
     return this.each(function() {
         $('input,select,textarea', this).clearFields();
     });
 };


 $.fn.clearFields = $.fn.clearInputs = function() {
     return this.each(function() {
         var t = this.type, tag = this.tagName.toLowerCase();
         if (t == 'text' || t == 'password' || tag == 'textarea')
             this.value = '';
         else if (t == 'checkbox' || t == 'radio')
             this.checked = false;
         else if (tag == 'select')
             this.selectedIndex = -1;
     });
 };


 $.fn.resetForm = function() {
     return this.each(function() {
         if (typeof this.reset == 'function' || (typeof this.reset == 'object' && !this.reset.nodeType))
             this.reset();
     });
 };


 $.fn.enable = function(b) {
     if (b == undefined) b = true;
     return this.each(function() {
         this.disabled = !b;
     });
 };
 
 $.fn.selected = function(select) {
     if (select == undefined) select = true;
     return this.each(function() {
         var t = this.type;
         if (t == 'checkbox' || t == 'radio')
             this.checked = select;
         else if (this.tagName.toLowerCase() == 'option') {
             var $sel = $(this).parent('select');
             if (select && $sel[0] && $sel[0].type == 'select-one') {
                 $sel.find('option').selected(false);
             }
             this.selected = select;
         }
     });
 };
 
 $.selected = function(type,typeName,setValue) {
    if (setValue == undefined)
    {
        //alert($('input[name='+typeName+']:checked').val());
        if (type == 'checkbox' || type == 'radio')
            //return $(":"+type+"[name="+typeName+"][checked=true]").val();
            return $('input[name='+typeName+']:checked').val();
        else
            return $(":"+type+"[name="+typeName+"][select=true]").val();
    }
    else
    {
        $(":"+type+"[name="+typeName+"]").each(function(i)
        {
            if (setValue == this.value)
            {
                if (type == 'checkbox' || type == 'radio')
                    $("#"+this.id).attr("checked","true");
                else
                    $("#"+this.id).attr("select","true");
            }
        });
    }
 };
 
 /*
 //弹出层
 var TipLoading = {
        id:'I-Loading',
        type:'Loading',
        message:'数据加载中，请稍后',
        TipShowType:true,
        width:160,
        height:30,
        hasMask:false,
  
    _createHtml:function(){
        var eId = document.getElementById(this.id);
        if(!eId){
             if(this.TipShowType)
             {
                this.width="160";
             }
            var iTxtHeight = this.height - 12;
            if(iTxtHeight < 0) iTxtHeight = 0;

            //创建元素
            var eTipLoading = $('<div style="display:none"></div>').attr({ "class":"I-Loading", "id" : this.id});
            $('body').append(eTipLoading);
            
            var eMask = $('<div></div>')
            .attr({ "class" : "Mask", "id" : this.id + '_Mask'}).css("opacity",0.3);
            eTipLoading.append(eMask);
            
            var eMaskIframe = $('<iframe></iframe>')
            .attr({ "name" : this.id + '_Mask', "id" : this.id + '_Iframe','src':'#'}).css("opacity",0.3);
            eMask.append(eMaskIframe);
            
            
            var eTag = $('<div></div>')
            .attr({ "class" : "Tag", "id" : this.id + '_Tag'}).css("width",this.width.toString()+ 'px');
            eTipLoading.append(eTag);
            
             var eTxt = $('<div></div>')
            .attr("class" , "Txt").css("height",iTxtHeight.toString() + 'px').html(this.message);
            eTag.append(eTxt);

        } 
    },
    
    show:function(){
        this._createHtml();        
        var eTipLoading = document.getElementById(this.id);
        var	eTag = document.getElementById(this.id + '_Tag');
        $(eTipLoading).css('display','block');
        
        var	eMask = document.getElementById(this.id + '_Mask');
        var	eMaskIframe = document.getElementById(this.id + '_Iframe');
        if(this.hasMask){
            $(eMask).css({'width':$(window).width().toString() + 'px','height':$(window).height().toString() + 'px', 'display':'block'});
            $(eMaskIframe).css({'width':$(window).width().toString() + 'px','height':$(window).height().toString() + 'px'});
        }else{
            $(eMask).css({'display':'none'});
        }
        //设置位置
        var iTop;
        var iLeft;
        if(this.TipShowType)
        {
             iTop = ($(window).height() - $(eTag).height()) / 2;
             iLeft = ($(window).width() - $(eTag).width()) / 2;
        }
        else
        {
             iTop = ($(window).height() - $(eTag).height()) / 2;
             
             iLeft = $(window).width()-153;
        }
        $(eTag).css('top',iTop.toString() + 'px');
        $(eTag).css('left',iLeft.toString() + 'px');
        
    },
    hide:function(){
       var eId =  document.getElementById(this.id);
       if(eId!=null){eId.style.display="none";}
    }
};
*/
String.prototype.trim = function() {        
    return this.replace(/^\s+/g,"").replace(/\s+$/g,"");      
}

Number.prototype.numFormat = function() {
   var temp=this.toFixed(2);
	 return temp.numFormat(); 
}

String.prototype.numFormat = function() {
    var value =this;
    var indexOf=value.indexOf(".");
    if(indexOf>=0)
    {
        var temp=value.substring(0,indexOf)+value.substring(indexOf,indexOf+3);
         if(value.length==indexOf+2)
            return  temp+"0";
        else if(value.length==indexOf+1)
            return  temp+"00";
		else 
		     return temp;
    }
    else
    {
      return  value+".00";
    } 
}
Number.prototype.currencyFormat=function(){
     var temp=this.toFixed(2);
	 temp=temp.numFormat(); 
	 temp="&yen;"+temp;
	 return  temp;
}


String.prototype.replaceAll  = function(s1,s2){   
    return this.replace(new RegExp(s1,"gm"),s2);   
}  


function JsAlert(msg) {
    $("#spnError").html(msg);
}

function JsAlertAgo(msg) {
    alert(msg); history.go(-1);
}

function JsAlertHref(msg,href) {
    alert(msg); window.location.href=href;
}

function DrawImage(ImgD,FitWidth,FitHeight){ 
    var image=new Image(); 
    image.src=ImgD.src; 
    if(image.width>0 && image.height>0){ 
        if(image.width/image.height>= FitWidth/FitHeight){ 
            if(image.width>FitWidth){ 
                ImgD.width=FitWidth; 
                ImgD.height=(image.height*FitWidth)/image.width; 
            }else{ 
                ImgD.width=image.width; 
                ImgD.height=image.height; 
            } 
        } else{ 
            if(image.height>FitHeight){ 
                ImgD.height=FitHeight; 
                ImgD.width=(image.width*FitHeight)/image.height; 
            }else{ 
                ImgD.width=image.width; 
                ImgD.height=image.height; 
            } 
        } 
    } } 



function FloatDiv(divID)
{
   var _over={overlayOpacity: 0.9,overlayColor: '#ccc'}
   $('body').append('<div id="d_overlay" style="overflow:hidden;"></div>'); 
   //$('body').append('<iframe id="d_overlay" style="overflow:hidden;filter:alpha(opacity=90); moz-opacity:0.9;opacity: 0.9;"></iframe>');
    $("#d_overlay").css({
	    position: 'absolute',
	    zIndex: 9998,
	    top: 0,
	    left: '0px',
	    height: $(document).height(),
	    width: $(document).width(),
	    background:_over.overlayColor,
	    opacity: _over.overlayOpacity,
	    textAlign:'center'
    });

     $('#'+divID).css({
        position: 'absolute',
	    zIndex: 9999,
	    left: $(window).width()/2-$('#'+divID).width()/2,
	    //top:$(document).height()-($('#'+divID).height()+$(window).height())/2,
	    top:$(document).scrollTop()+($(window).height()-$('#'+divID).height())/2,
	    display:"block"			
    });
    /*ie下*/
    $('body').attr('scroll','no');
    /*firefox*/
    document.body.style.overflow="hidden";
    $('body').css('overflow-y','hidden');
    $('#winclose').click(function(){
        CloseFloatDiv(divID);
    });
    
}

function CloseFloatDiv(divID){
    $("#d_overlay").html('');
    $("#d_overlay").remove();
    $('body').attr('scroll','');
    document.body.style.overflow='auto';
    $('body').css('overflow-y','');
    $('#'+divID).css('display','none');
}

function FloatFrame(pageLink,width,height)
{
    var _over={overlayOpacity: 1.0,overlayColor: '#ccc'}
    $('body').append('<div id="f_overlay" style="overflow:hidden"><div id="f_win"  style="z-index:9999;width:'+width+'px;height:'+height+'px;"><iframe width="'+width+'px"  height='+height+'px" frameborder="0" src="'+pageLink+'" scrolling="no" ></iframe></div></div>'); 
    var divID="f_win";
    var lefts=$(window).width()/2-$('#'+divID).width()/2;
     $('#'+divID).css({
        position: 'absolute',
        left: $(window).width()/2-$('#'+divID).width()/2,
        top:$(document).scrollTop()+($(window).height()-$('#'+divID).height())/2,
        display:"block"			
    });
      
    $("#f_overlay").css({
        position: 'absolute',
        zIndex: 9998,
        top: 0,
        left: '0px',
        height: $(document).height(),
        width: $(document).width(),
        background:_over.overlayColor,
        opacity: _over.overlayOpacity,
        textAlign:'center'
    });
      
    $(window).bind("resize",function(){
         $('#'+divID).css({
            left: $(window).width()/2-$('#'+divID).width()/2,
            top:$(document).scrollTop()+($(window).height()-$('#'+divID).height())/2
        });
        $("#f_overlay").css({
            height: $(document).height(),
            width: $(window).width()
        });
    });

//        /*ie下*/
//        //$('body').attr('scroll','no');
//        /*firefox*/
//        //document.body.style.overflow="hidden";
//        //$('body').css('overflow-y','hidden');
}


function FloatFrameAuto(pageLink,width,height)
{
    var _over={overlayOpacity: .90,overlayColor: '#ccc'}
    $('body').append('<div id="f_overlay" style="overflow:auto"><div id="f_win"  style="z-index:9999;width:'+width+'px;height:'+height+'px;"><iframe width="'+width+'px"  height='+height+'px" frameborder="0" src="'+pageLink+'" scrolling="no" ></iframe></div></div>'); 
    var divID="f_win";
    var lefts=$(window).width()/2-$('#'+divID).width()/2;
     $('#'+divID).css({
        position: 'absolute',
        left: $(window).width()/2-$('#'+divID).width()/2,
        top:$(document).scrollTop()+($(window).height()-$('#'+divID).height())/2,
        display:"block"			
    });
      
    $("#f_overlay").css({
        position: 'absolute',
        zIndex: 9998,
        top: 0,
        left: '0px',
        height: $(document).height(),
        width: $(document).width(),
        background:_over.overlayColor,
        opacity: _over.overlayOpacity,
        textAlign:'center'
    });

}


function FloatFrameForLogin(pageLink,width,height)
{
    var _over={overlayOpacity: 0.9,overlayColor: '#ccc'}
    if($.browser.msie&&$.browser.version<=6)
    {
        $('body').append('<div id="f_overlay" style="overflow:hidden;filter:alpha(opacity=95);opacity:0.95;"><iframe style="width:100%;height:100%;" frameborder="0" scrolling="no"></iframe><div id="f_win"  style="z-index:9999;width:'+width+'px;height:'+height+'px;"><iframe width="'+width+'px"  height='+height+'px" frameborder="0" src="'+pageLink+'" scrolling="no" ></iframe></div></div>'); 
        setTimeout(function(){
            $('#f_overlay>iframe').contents().find('body').css('background','#ccc');
        },0);
    }
    else
        $('body').append('<div id="f_overlay" style="overflow:hidden;filter:alpha(opacity=95);opacity:0.95;"><div id="f_win"  style="z-index:9999;width:'+width+'px;height:'+height+'px;"><iframe width="'+width+'px"  height='+height+'px" frameborder="0" src="'+pageLink+'" scrolling="no" ></iframe></div></div>'); 
    var divID="f_win";
    var lefts=$(window).width()/2-$('#'+divID).width()/2;
     $('#'+divID).css({
        position: 'absolute',
        left: $(window).width()/2-$('#'+divID).width()/2,
        top:$(document).scrollTop()+($(window).height()-$('#'+divID).height())/2,
        display:"block"			
    });
      
    $("#f_overlay").css({
        position: 'absolute',
        zIndex: 9900,
        top: 0,
        left: '0px',
        height: $(document).height(),
        width: $(document).width(),
        background:_over.overlayColor,
        textAlign:'center'
    });
    
    $(window).bind("resize",function(){
         $('#'+divID).css({
            left: $(window).width()/2-$('#'+divID).width()/2,
            top:$(document).scrollTop()+($(window).height()-$('#'+divID).height())/2
        });
        $("#f_overlay").css({
            height: $(document).height(),
            width: $(document).width()
        });
    });
  
//        /*ie下*/
//        //$('body').attr('scroll','no');
//        /*firefox*/
//        //document.body.style.overflow="hidden";
//        //$('body').css('overflow-y','hidden');
}

function FloatDivForPayOrder(divID,link)
{
    var _over={overlayOpacity: .90,overlayColor: '#ccc'}
    $('body').append('<div id="d_overlay" style="overflow:hidden"></div>'); 
    $("#d_overlay").css({
	    position: 'absolute',
	    zIndex: 9998,
	    top: 0,
	    left: '0px',
	    height: $(document).height(),
	    width: $(document).width(),
	    background:_over.overlayColor,
	    opacity: _over.overlayOpacity,
	    textAlign:'center'
    });

     $('#'+divID).css({
        position: 'absolute',
	    zIndex: 9999,
	    left: $(window).width()/2-$('#'+divID).width()/2,
	    //top:$(document).height()-($('#'+divID).height()+$(window).height())/2,
	    top:$(document).scrollTop()+($(window).height()-$('#'+divID).height())/2,
	    display:"block"			
    });
    /*ie下*/
    $('body').attr('scroll','no');
    /*firefox*/
    document.body.style.overflow="hidden";
    $('body').css('overflow-y','hidden');
    $('#winclose').click(function(){
        CloseFloatDiv(divID);
    });
    
    window.open(link);

}

var charset = 'utf-8';
function mb_strlen(str) {
    var len = 0;
    for (var i = 0; i < str.length; i++) {
        len += str.charCodeAt(i) < 0 || str.charCodeAt(i) > 255 ? (charset == 'utf-8' ? 3 : 2) : 1;
    }
    return len;
}

function mb_cutstr(str, maxlen, dot) {
    var len = 0;
    var ret = '';
    var dot = !dot ? '...' : '';
    maxlen = maxlen - dot.length;
    for (var i = 0; i < str.length; i++) {
        len += str.charCodeAt(i) < 0 || str.charCodeAt(i) > 255 ? (charset == 'utf-8' ? 3 : 2) : 1;
        if (len > maxlen) {
            ret += dot;
            break;
        }
        ret += str.substr(i, 1);
    }
    return ret;
}
function GetUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}



function isUndefined(variable) {
    return typeof variable == 'undefined' ? true : false;
}
