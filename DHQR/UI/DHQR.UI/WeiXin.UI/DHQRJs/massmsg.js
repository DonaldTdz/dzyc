/*
成都和创科技有限公司
*/

var material_obj={
	material_init:function(){
		$('#material>.list').masonry({itemSelector:'.item', columnWidth:367});
	},
	
	material_one_init:function(){
		//global_obj.file_upload($('#MsgFileUpload'), $('#material_form input[name=ImgPath]'), $('#MsgImgDetail'));
		$('#MsgImgDetail').html(global_obj.img_link($('#material_form input[name=ImgPath]').val()));
		
		$('#material_form input[name=inputTitle]').on('keyup paste blur', function () {
		    $('#material_form .title').html($(this).val());
		    $('#material_form input[name=title\\[\\]]').val($(this).val());
		    $('#material_form .author').html($(this).val());
		    $('#material_form input[name=author\\[\\]]').val($(this).val());
		    $('#material_form .digest').html($(this).val());
		    $('#material_form input[name=digest\\[\\]]').val($(this).val());
		});
		$('#material_form textarea').on('keyup paste blur', function(){
			$('#material_form .txt').html($(this).val().replace(/([^>\r\n]?)(\r\n|\n\r|\r|\n)/g, '$1<br>$2'));
		});
		$('.mod_form select[name=inputUrl]').find("option[value='" + $('input[name=content_source_url\\[\\]]').val() + "']").attr("selected", true);
		//$("select[name=Url]").find("option[value='"+$('input[name=oUrl]').val()+"']").attr("selected", true);
		
		//$('#material_form').submit(function(){
		//	if(global_obj.check_form($('*[notnull]'))){return false};
		//	$('#material_form input:submit').attr('disabled', true);
		//	return true;
	    //});
		$('.mod_form select').filter('[name=inputUrl]').change(function () {		    
		    $('#material_form input[name=content_source_url\\[\\]]').val($(this).val());
		});
	},
	
	material_multi_init:function(){
		var material_multi_list_even=function(){
			$('.multi .first, .multi .list').each(function(){
				var children=$(this).children('.control');
				$(this).mouseover(function(){children.css({display:'block'});});
				$(this).mouseout(function(){children.css({display:'none'});});
				
				children.children('a[href*=#del]').click(function(){
					if($('.multi .list').size()<=1){
						alert('无法删除，多条图文至少需要2条消息！');
						return false;
					}
					if(confirm('删除后不可恢复，继续吗？')){
						$(this).parent().parent().remove();
						$('.multi .first a[href*=#mod]').click();
						$('.mod_form').css({top:37});
					}
				});
				
				children.children('a[href*=#mod]').click(function(){
				    var position = $(this).parent().offset();
					var material_form_position=$('#material_form').offset();
					var cur_id='#'+$(this).parent().parent().attr('id');
					$('.mod_form').css({ top: position.top - material_form_position.top });
                    
					$('.mod_form input[name=inputTitle]').val($(cur_id + ' input[name=title\\[\\]]').val());
					$('.mod_form input[name=inputAuthor]').val($(cur_id + ' input[name=author\\[\\]]').val());
					$('.mod_form input[name=inputDigest]').val($(cur_id + ' input[name=digest\\[\\]]').val());
					//CKEDITOR.instances.content.setData($(cur_id + ' input[name=content\\[\\]]').val());
					//$('.mod_form textarea[name=content]').val($(cur_id + ' input[name=content\\[\\]]').val());
					$('.mod_form select[name=inputUrl]').find("option[value='" + $(cur_id + ' input[name=content_source_url\\[\\]]').val() + "']").attr("selected", true);
					$('.big_img_size_tips').html(cur_id == '#multi_msg_0' ? '640*360px' : '300*300px');

					$('.multi').data('cur_id', cur_id);
					//CKEDITOR.instances["content"].on("instanceReady", function () {
					//    //set keyup event  
					//    this.document.on("keyup", function () {
					//        var cur_id = $('.multi').data('cur_id');
					//        var currentValue = CKEDITOR.instances.content.getData();
					//        $(cur_id + ' input[name=content\\[\\]]').val(currentValue);

					//    });
					//});
				});
				$('.mod_form select[name=inputUrl]').find("option[value='" + $('input[name=content_source_url\\[\\]]').val() + "']").attr("selected", true);
			});
		}
		$('.multi').data('cur_id', '#'+$('.multi .first').attr('id'));
		$('.mod_form input').filter('[name=inputTitle]').on('keyup paste blur', function(){
			var cur_id=$('.multi').data('cur_id');
			$(cur_id + ' input[name=title\\[\\]]').val($(this).val());		
			$(cur_id+' .title').html($(this).val());
		})
		$('.mod_form input').filter('[name=inputAuthor]').on('keyup paste blur', function () {
		    var cur_id = $('.multi').data('cur_id');
		    $(cur_id + ' input[name=author\\[\\]]').val($(this).val());
		    
		})
		$('.mod_form input').filter('[name=inputDigest]').on('keyup paste blur', function () {
		    var cur_id = $('.multi').data('cur_id');
		    $(cur_id + ' input[name=digest\\[\\]]').val($(this).val());
		    
		})
	

		//$('.mod_form input').filter('[id=content]').on('keyup paste blur', function () {
		//    var cur_id = $('.multi').data('cur_id');
		//    $(cur_id + ' input[name=content\\[\\]]').val($(this).val());
		   
		//})
		$('.mod_form select').filter('[name=inputUrl]').change(function(){
			var cur_id=$('.multi').data('cur_id');
			$(cur_id + ' input[name=content_source_url\\[\\]]').val($(this).val());
		});
		
		material_multi_list_even();
		$('a[href=#add]').click(function(){
			$(this).blur();
			if($('.multi .list').size()>=7){
				alert('你最多只可以加入8条图文消息！');
				return false;
			}
			$('.multi .list, a[href*=#mod], a[href*=#del]').off();
			$('<div class="list" id="id_'+Math.floor(Math.random()*1000000)+'">'+$('.multi .list:last').html()+'</div>').insertAfter($('.multi .list:last'));
			$('.multi .list:last').children('.info').children('.title').html('标题').siblings('.img').html('缩略图');
			$('.multi .list:last input').filter('[name=title\\[\\]]').val('').end().filter('[name=content_source_url\\[\\]]').val('').end().filter('[name=ImgPath\\[\\]]').val('')
		    .end().filter('[name=author\\[\\]]').val('').end().filter('[name=digest\\[\\]]').val('').end().filter('[name=content\\[\\]]').val('');
			material_multi_list_even();
		});
		
		//$('#material_form').submit(function(){
		//	if(global_obj.check_form($('*[notnull]'))){return false};
		//	$('#material_form input:submit').attr('disabled', true);
		//	return true;
		//});
	},
	
	url_init:function(){
		$('#add_form').submit(function(){
			if(global_obj.check_form($('*[notnull]'))){return false};
			$('#add_form input:submit').attr('disabled', true);
			return true;
		});
	}


}