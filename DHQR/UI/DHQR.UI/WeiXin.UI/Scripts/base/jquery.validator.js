/*  验证函数  
陈明森
*/
(function($) {
$.extend({
    _t:['required','email','plusdecimal','plusnumber','decmal','number','username','url'],
    _tip:[
    '必填项',
    '必须是正确的邮件地址',
    '必须是数字(正整数[小数])',
    '必须是数字(正整数)',
    '必须是数字(正负(整数)小数)',
    '必须是数字(正负整数)',
    '长度不对或者含有特殊字符',//至少3位,最长16位,大小写字母,下划线,数字组成
    '网地输入有误'
    ],
    _rul:'rul',
    _input:[':text','textarea',':password'],
    _inputbuilder:function(){
       
        $.each($._input,function(i){
            $($._input[i]).blur(function(e){
                $(this).removeClass("input_text_focus");
            });
            $($._input[i]).focus(function(e){
                $(this).addClass("input_text_focus");
            });
        });
        $.each($._input,function(i){
            $($._input[i]).each(function(){
                if($(this).attr('readonly')||$(this).attr('disabled')){
                    $(this).addClass('input_disabled');
                }
            });
        });
        $.each($(".required"),function(i,n){
            if($("#tip_"+this.id).html()==null)
                $(this).after('<span style="color:red" id="tip_'+this.id+'">&nbsp;*</span>');
        });
        
        $.each($('input[type="text"]:disabled'),function(i,n){
            $(this).css({
                'background':'#EAEAEA',
                'border':'1px solid #CCC'
            });
        });
        $(':text').attr("autocomplete", "off");
    },
 
    _validator:function(rulls,groups,chkblur){
        $._validatorgroup(rulls,groups);
        if(!chkblur)
        {
            $.each($._t,function(o,n){    
                $('.'+n).bind('blur',function(e){                   
                    if(n=='required'){
                        $._required.call($(this),e,rulls,n,'_'+n,$._tip[o]);
                    }else{
                        $._notrequired.call($(this),e,rulls,n,'_'+n,$._tip[o]);
                    }
                });
            });            
        }        
        $.each($._t,function(o,n){
             $('.'+n).bind('_'+n,function(e){
                    if(n=='required'){
                        $._required.call($(this),e,rulls,n,'_'+n,$._tip[o]);
                    }else{
                        $._notrequired.call($(this),e,rulls,n,'_'+n,$._tip[o]);
                    }
             });
        });
        
    },
    _validatorgroup:function(rulls,groups){
        if(!groups || !rulls){
            return false;
        }
        $.each(groups,function(e,n){
            var obj = null;
            if($('[name$=' + n + ']').length>0){
                obj=$('[name$="' + n + '"]')[0];
            }
            if(!obj)return false;
            obj = $(obj);           
            if(obj.attr('type')=='submit'||obj.attr('type')=='button')
            {                
                obj.click(function(){
                    if(!$._regsubmit($(this).attr('id'),'',rulls))
                    {
                        return false;
                    }
                    return true;
                });
            }
            else
            {
               obj.attr($._rul,rulls);
            }
        });
        return true;
    },    
    _validatorsubmit:function(rulls){        
        var f=true;
        $.each($._t,function(i){
            $('.b_'+$._t[i]).remove();
            $.each($('._'+$._t[i]),function(e){
                $(this).removeClass('_'+$._t[i]);            
            });
            $.each($('.'+$._t[i]),function(e,n){
                if(rulls && $(this).attr($._rul)==rulls){
                    //log.write($(this).attr('id')+'执行验证'+rulls);
                    $(this).trigger('_'+$._t[i]);               
                }
                if(!rulls){
                     $(this).trigger('_'+$._t[i]);  
                }             
            });
             if($('.b_'+$._t[i]).length>0 && f){
                f=false;
             }
        });
        return f;       
    },
    _clerequired:function(o){
        $('.b_required').each(function(e,n){
            if(!o){
                if(o==$(this).attr('id')){
                    $(this).remove();
                }
            }else{
                $(this).remove();
            }
        });
        $('._required').each(function(e,n){
             if(!o){
                if(o==$(this).attr('id')){
                    $(this).removeClass('_required');
                }
            }else{
                $(this).removeClass('_required');
            }
        });
    },
    _required:function(e,rull){
           validaterull=$(this).attr($._rul);
           var value = $(this).val();
           var _self=$(this);
           
           $.each($._t,function(i,n){
                if(n!='required'){_self.next(".b_"+n).remove('.b_'+n);}
           });         
           //log.write(value+'----------'+rull+'---------'+validaterull);
           if(!value || value==''){           
                if((!validaterull && !rull)||(validaterull && rull && validaterull.indexOf(rull)!=-1) )
                {
                    _self.addClass('_required');
                    if(!_self.nextAll().hasClass('b_required'))
                    {
                        _self.after('<span class="b_required" style="color:red">&nbsp;<img src="'+ImgServerUrl+'/images/c/v1.0/delete.gif" /></span>');
                    }
                }else{
                    return true;
                }                
           }else{
               //log.write(value+'+++++++++++++++++'+rull);
               _self.removeClass('_required').next(".b_required").remove('.b_required');                
           }
           e.preventDefault();
           e.stopPropagation();
           return true;
    },
    _notrequired:function(e,rull,enumtype,clsname,msg){
        var value = $(this).val();        
        if(!value || value==''){
            $(this).removeClass(clsname);
            $(this).next('.b'+clsname).remove('.b'+clsname);
            return false;
        }
        if(enumtype=='email'){
            enumtype = /^[\w\-\.]+@[\w\-\.]+(\.\w+)+$/;
        }else if(enumtype=='number'){
            enumtype = /^[+-]?[0-9]*$/;
        }else if(enumtype=='plusnumber'){
            enumtype = /^[0-9]*$/;
        }else if(enumtype=='decmal'){
            enumtype = /(^-?\d+)(\.\d+)?$/;
        }else if(enumtype=='plusdecimal'){
            enumtype = /(^\d+)(\.\d+)?$/;
        }else if(enumtype=='username'){
            enumtype = /^\w{3,16}$/;
        }else if(enumtype=='url'){
            enumtype = /http(s)?:\/\/([\w-]+\.)+[\w-]+(\/[\w- ./?%&amp;=]*)?/;
        }

        if(enumtype.test(value)){           
            $(this).removeClass(clsname);
            $(this).next('.b'+clsname).remove('.b'+clsname);
        }else{
            validaterull=$(this).attr($._rul);
            if((!validaterull && !rull)||(validaterull && rull && validaterull.indexOf(rull)!=-1) )
            {
                $(this).addClass(clsname);
                if(!$(this).nextAll().hasClass('b'+clsname))
                {
                    $(this).after('<span class="b'+clsname+'" style="color:red">&nbsp; '+msg+'</span>');
                }
                $(this).submit(function(){
                    return false;
                });
            }else{
                return true;
            }
        }
        e.preventDefault();
        e.stopPropagation();
        return true; 
    },
    _lengthstr:function(e,rull){
    
    },
    _reposition: function(obj) {      
	    var top = (($(window).height() / 2) - ($('#'+obj).outerHeight() / 2));
	    var left = (($(window).width() / 2) - ($('#'+obj).outerWidth() / 2)) ;
	    if( top < 0 ) top = 0;
	    if( left < 0 ) left = 0;
	    if( $.browser.msie && parseInt($.browser.version) <= 6 ) top = top + $(window).scrollTop();
		
	    $('#'+obj).css({
		    top: top + 'px',
		    left: left + 'px',
		    position:'absolute'
	    });
	},
	_regdelete:function(cls){
          $._regconfirm(cls,'确定删除吗?删除后不可恢复!');
	},
	_regconfirm:function(cls,message){     
	     $(cls).click(function(e){
	            if($(this).attr('disabled')){return true;}
	            var id= $(this).attr('id');
                jConfirm(message,'',function(ev){
                    if(ev){
                       if(typeof(Sys) && typeof(Sys)!='undefined')
                       {
                            //log.write('Sys............'+typeof(Sys));
                            var prm = Sys.WebForms.PageRequestManager.getInstance();
                            prm._doPostBack(id.replace(/_/g,'$'),'');
                            return false;
                       }else{
                            __doPostBack(id.replace(/_/g,'$'),'');
                       }
                    }
                });
                return false;
          });
	},	
	_regsubmit:function (eventTarget,eventArgument,rull,callBack,callBackArg){ 
    try{
        var t =$._validatorsubmit(rull);
        if(!t){
            alert('还有信息没有填写完整,不能提交');
            $('#'+eventTarget).attr('disabled','');
            return false;
        }
        return true;
     } 
    catch(e){ 
         alert(e);
         return false;
     } 
   },
   
   _selectchk:function(ctl,chkwhere)
    {
        xState=ctl.checked;
        $.each($(":checkbox"),function(){
            if($(this).val()==chkwhere){
                $(this).attr('checked',false);
                return true;
            }
            $(this).attr('checked',xState);
        });
    },
    _selectchkbyctl:function (ctl,o)
    {
        $.each($('#'+ctl+' :checkbox'),function(){
           if($(this)[0]!=$(o)[0]){
                $(this).attr('checked',!$(this).attr('checked'));
           }
        });        
    },
    _iscookie:function(){
        return navigator.cookieEnabled;
    },
    _isTime:function (str)
    {
	    var a = str.match(/^(\d{1,2})(:)?(\d{1,2})\2(\d{1,2})$/);
	    if (a == null) {return false}
	    if (a[1]>24 || a[3]>60 || a[4]>60)
	    {
		    return false;
	    }
	    return true;
    },
    _isDate:function (str)
    {
	    var r = str.match(/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/); 
	    if(r==null)return false; 
	    var d= new Date(r[1], r[3]-1, r[4]); 
	    return (d.getFullYear()==r[1]&&(d.getMonth()+1)==r[3]&&d.getDate()==r[4]);
    },
    _isDateTime:function (str)
    {
	    var reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/; 
	    var r = str.match(reg); 
	    if(r==null) return false; 
	    var d= new Date(r[1], r[3]-1,r[4],r[5],r[6],r[7]); 
	    return (d.getFullYear()==r[1]&&(d.getMonth()+1)==r[3]&&d.getDate()==r[4]&&d.getHours()==r[5]&&d.getMinutes()==r[6]&&d.getSeconds()==r[7]);
    },
    _includepath:'',
    _include: function(file){
        var files = typeof file == "string" ? [file] : file;
        for (var i = 0; i < files.length; i++)
        {
            var name = files[i].replace(/^\s|\s$/g, "");
            var att = name.split('.');
            var ext = att[att.length - 1].toLowerCase();
            var isCSS = ext == "css";
            var tag = isCSS ? "link" : "script";
            var attr = isCSS ? " type='text/css' rel='stylesheet' media='all' " : " language='javascript' type='text/javascript' ";
            var link = (isCSS ? "href" : "src") + "='" + $._includepath + name + "'";
            if ($(tag + "[" + link + "]").length == 0) 
           // document.write("<" + tag + attr + link + "></" + tag + ">");
           var htm="<" + tag + attr + link + "></" + tag + ">";
           if($('head').html().indexOf(htm.toUpperCase())==-1){
               // log.write('插入了JS..........'+htm.toUpperCase().replace('\"',''));
                $('head').append(htm);
           }
        }
    },
    _jsSelect:function(objSelect,item)   
    {   
         var isExit = false;
         var index=0;
         $.each($(objSelect+'>option'),function(i,n){
           if(!isExit){
               var val = $(objSelect+'>option').get(i).value;
               if(val==item){
                  isExit=true;
                  $(objSelect)[0].options[index].selected = true;
               }
           }
           index++;
         });     
         return isExit;   
    }
    
});

$.fn.extend({
    poptooltip:function(urls,tab,param){
            obj=$(this);
            $.ajax({
                type: "POST",
                url: urls,
                data: param,
                dataType: "xml",
                async:false,
                success: function(res) {
                    try{
                        var offset=obj.offset();
                        var tables=new Array();
                        var t="";
                        $(res).find('NewDataSet').each(function(){
                            var nodes=$(this)[0].childNodes;
                            len =  nodes.length;
                            for(var i=0;i<len;i++){
                                if(t!=nodes[i].nodeName){
                                    tables.push(nodes[i].nodeName);
                                }
                                t=nodes[i].nodeName;
                            }
                        });
                        var html="";
                        for(i=0;i<tables.length;i++){ 
                            html+='<br/>';
                            html+='<table cellspacing="0" rules="all" border="1"  style="background:white;width:300;border-collapse:collapse; position:relative;border:1px solid #C1DAD7">';
                            $(res).find(tables[i]).each(function(){
                                var nodes = $(this)[0].childNodes;
                                var len=nodes.length;
                                html+='<tr>';
                                if(tab.cols[i]){
                                    for(j=0;j<tab.cols[i].length;j++)
                                    {
                                        html+='<td style="height:19px;border:1px solid #C1DAD7">';
                                        html+= $(this).find(tab.cols[i][j][0]).text();
                                        html+='</td>';
                                    }
                                }else{
                                    html+='<td style="height:19px;border:1px solid #C1DAD7">';
                                    html+='没有传列名';
                                    html+='</td>';
                                }
                                html+='</tr>';
                            });
                            html+='</table>';
                        }                                         
                       $('body').append("<div id='___show_pop' style='border:1px solid #CCC;background:white;position:absolute;top:"+(offset.top)+"px;left:"
                            +(offset.left+obj.width()+5)+"px;'></div>");
                       $('#___show_pop').html(html);
                    }catch(e){alert(e)}
                }
            });
    },
    tooltip:function(url,tab,param){
      _obj=$(this);
      _obj.hover(
           function(){
                $(this).poptooltip(url,tab,param);
           },
           function(){
                $('#___show_pop').remove();
        });
    },
    boxgroup:function(){
        $(this).find("[type='checkbox']").bind('click',function(){
            _child=$(this);
            $(this).parent().find("[type='checkbox']").each(function(){
                if($(this)[0]!=_child[0]){
                    $(this).attr('checked','');
                }
            });
        });
    },
    boxgroups:function(){
        $(this).each(function(){
            $(this).boxgroup();
        });
    },
    selbox:function (ctl,area) {
      $()
    },
    autocompeteoff:function(){
        
    }
});

})(jQuery);
