var user_obj = {
    card_config_init: function () {
        global_obj.file_upload($('#CardLogoUpload'), $('#config_form input[name=CardLogo]'), $('#CardLogoDetail'));
        global_obj.file_upload($('#CustomImgUpload'), $('#config_form input[name=CustomImgPath]'), $('#CustomImgDetail'));
        $('#CardLogoDetail').html(global_obj.img_link($('#config_form input[name=CardLogo]').val()));
        $('#CustomImgDetail').html(global_obj.img_link($('#config_form input[name=CustomImgPath]').val()));

        $('#card_style .file .del a').click(function () {
            $('#CardLogoDetail').html('');
            $('#config_form input[name=CardLogo]').val('');
            return false;
        });

        $('#config_form input:checkbox[name=IsIntegral]').click(function () {
            if ($(this).attr('checked')) {
                $(this).parent().next('td').show();
            } else {
                $(this).parent().next('td').hide();
            }
        });

        $('#config_form input:checkbox[name=IsSign]').click(function () {
            if ($(this).attr('checked')) {
                $(this).parent().children('span').eq(1).show();
            } else {
                $(this).parent().children('span').eq(1).hide();
            }
        });

        if ($('input[name=CardStyleCustom]').attr('checked')) {
            $('#CardStyleCustomBox, #CustomImgDetail').show();
            $('#card_style_select').hide();
        } else {
            $('#CardStyleCustomBox, #CustomImgDetail').hide();
            $('#card_style_select').show();
        }

        $('input[name=CardStyleCustom]').click(function () {
            if ($('input[name=CardStyleCustom]').attr('checked')) {
                $('#CardStyleCustomBox, #CustomImgDetail').show();
                $('#card_style_select').hide();
            } else {
                $('#CardStyleCustomBox, #CustomImgDetail').hide();
                $('#card_style_select').show();
            }
        });

        $('a[href=#card_style_list]').click(function () { $('#card_style_list').leanModal(); });
        $('#card_style_list .list a').click(function () {
            $('#config_form input[name=CardStyle]').val($(this).attr('value'));
            $('#card_style_list .list a').removeClass('cur');
            $(this).addClass('cur');
            //$("#config_form #card_style .style img").attr('src', "static/images/card/card-style-bg-"+$(this).attr('value')+".png");
            $("#config_form #card_style .style img").attr('src', $(this).find('img').attr('src'));
            $('.modal_close').click();
        });
    },

    level_init: function () {
        for (i = 0; i < 5; i++) {
            global_obj.file_upload($('#LevelImgPath_' + i), $('#level_form input[name=LevelImgPath\\[\\]]').eq(i), $('#card_level .pic').eq(i));
            var PicContents = $('#level_form input[name=LevelImgPath\\[\\]]').eq(i).val() ? global_obj.img_link($('#level_form input[name=LevelImgPath\\[\\]]').eq(i).val()) + '<br /><a href="javascript:;">删除</a>' : '默认背景';
            $('#card_level .pic').eq(i).html(PicContents);
        }
        $('#card_level .pic a').click(function () {
            $(this).parent().html('默认背景').parent().find('input[name=LevelImgPath\\[\\]]').val('');
        });

        $('.card_level_table .input_add').click(function () {
            $('.card_level_table tr[FieldType=text]:hidden').eq(0).show();
            if (!$('.card_level_table tr[FieldType=text]:hidden').size()) {
                $(this).hide();
            }
        });
        $('.card_level_table .input_del').click(function () {
            $('.card_level_table .input_add').show();
            $(this).parent().parent().hide().find('input').val('').parent().parent().find('span.pic').html('默认背景');
        });

        $('#level_form .back').click(function () { window.location = '?m=user&a=card_level'; });
        $('#level_form').submit(function () {
            if (global_obj.check_form($('*[notnull]'))) { return false };
            $('#level_form input:submit').attr('disabled', true);
            return true;
        });
    },

    business_keyword: function () {
        $('#add_form').submit(function () {
            if (global_obj.check_form($('*[notnull]'))) { return false };
            $('#add_form input:submit').attr('disabled', true);
            return true;
        });
    },

    card_article_init: function () {
        var date_str = new Date();
        $('#card_article_form input[name=Time]').daterangepicker({
            timePicker: true,
            //minDate:new Date(date_str.getFullYear(), date_str.getMonth(), date_str.getDate()),
            format: 'YYYY/MM/DD HH:mm:00'
        }
		)

        $('#card_article_form .back').click(function () { window.location = '?m=user&a=card_article'; });
        $('#card_article_form').submit(function () {
            if (global_obj.check_form($('*[notnull]'))) { return false };
            $('#card_article_form input:submit').attr('disabled', true);
            return true;
        });
    },

    card_benefits_init: function () {
        var date_str = new Date();
        $('#card_benefits_form input[name=Time]').daterangepicker({
            timePicker: true,
            //minDate:new Date(date_str.getFullYear(), date_str.getMonth(), date_str.getDate()),
            format: 'YYYY/MM/DD HH:mm:00'
        }
		)

        $('#card_benefits_form .back').click(function () { window.location = '?m=user&a=card_benefits'; });
        $('#card_benefits_form').submit(function () {
            if (global_obj.check_form($('*[notnull]'))) { return false };
            $('#card_benefits_form input:submit').attr('disabled', true);
            return true;
        });
    },

    coupon_list_init: function () {
        global_obj.file_upload($('#ReplyImgUpload'), $('form input[name=ReplyImgPath]'), $('#ReplyImgDetail'));
        global_obj.file_upload($('#ImgPathUpload'), $('form input[name=ImgPath]'), $('#ImgPathDetail'));
        $('#ReplyImgDetail').html(global_obj.img_link($('form input[name=ReplyImgPath]').val()));
        $('#ImgPathDetail').html(global_obj.img_link($('form input[name=ImgPath]').val()));

        var date_str = new Date();
        $('#coupon_list_form input[name=Time]').daterangepicker({
            timePicker: true,
            //minDate:new Date(date_str.getFullYear(), date_str.getMonth(), date_str.getDate()),
            format: 'YYYY/MM/DD HH:mm:00'
        }
		)

        $('#coupon_list_form .back').click(function () { window.location = '?m=user&a=coupon_list'; });
        $('#coupon_list_form').submit(function () {
            if (global_obj.check_form($('*[notnull]'))) { return false };
            $('#coupon_list_form input:submit').attr('disabled', true);
            return true;
        });
    },

    user_init: function () {
        $('#search_form input:button').click(function () {
            window.location = './?' + $('#search_form').serialize() + '&do_action=user.user_export';
        });

        $('a[href=#modpass]').each(function () {
            $(this).click(function () {
                $('#mod_user_pass .h span').html(' (' + $(this).parent().parent().children('td[field=1]').find('span').html() + ')');
                $('#mod_user_pass form input[name=Password]').val('');
                $('#mod_user_pass form input[name=UserId]').val($(this).parent().parent().attr('UserId'));
                $('#mod_user_pass form').show();
                $('#mod_user_pass .tips').hide();
                $('#mod_user_pass').leanModal();
            });
        });

        $('#mod_user_pass form').submit(function () { return false; });
        $('#mod_user_pass form input:submit').click(function () {
            if (global_obj.check_form($('*[notnull]'))) { return false };

            $(this).attr('disabled', true);
            $.post('?', $('#mod_user_pass form').serialize(), function (data) {
                $('#mod_user_pass form input:submit').attr('disabled', false);
                if (data.status == 1) {
                    $('#mod_user_pass .tips').html('修改密码成功！').show();
                } else {
                    $('#mod_user_pass .tips').html('修改密码失败，出现未知错误！').show();
                };
                $('#mod_user_pass form').hide();
                $('#mod_user_pass').leanModal();
            }, 'json');
        });

        //       //数据修改
        $('.upd_rows').dblclick(function () {
            var o = $(this).children('.upd_txt');
            if (o.children('input').size()) { return false; }

            o.data('text', o.html().trim()).html('<input value="' + (o.html() != '无' ? o.html().trim() : '') + '">');
            o.children('input').select();
            o.children('input').keyup(function (event) {
                if (event.which == 13) {
                    $(this).blur();
                    var value = $(this).val();
                    if (value == '' || value == '无' || value == o.data('text')) {
                        o.html(o.data('text'));
                        return false;
                    }
                    $('#update_post_tips').html('数据提交中...').css({ left: $(window).width() / 2 - 100 }).show();

                    var param = {};
                    param["Id"] = o.parents("tr").attr('VipId');
                    param["field"] = o.parent().attr('field');
                    param["value"] = value;
                    $.post('EditVipInfo', param, function (data) {
                        if (data.IsSuccessful == 1) {
                            var msg = '修改成功！';
                            o.html(value);
                        } else if (data.OperateMsg != '') {
                            var msg = data.OperateMsg;
                            o.html(o.data('text'));
                        } else {
                            var msg = '修改失败，出现未知错误！';
                            o.html(o.data('text'));
                        }
                        $('#update_post_tips').html(msg).fadeOut(3000);
                    }, 'json');
                }
            });
        });

        $('.upd_select').dblclick(function () {
            var o = $(this).children('.upd_txt');
            if (o.children('select').size()) { return false; }

            var vipLevelSelect = $("#vipLevelId")[0];
            var s_html = '<select>';
            for (i = 1; i < vipLevelSelect.length; i++) {
                var selected = o.html() == vipLevelSelect[i].text ? 'selected' : '';
                s_html += '<option value="' + vipLevelSelect[i].value + '" ' + selected + '>' + vipLevelSelect[i].text + '</option>';
            }
            s_html += '</select>';
            o.data('text', o.html()).html(s_html);
            o.children('select').focus();

            o.children('select').bind('change blur', function () {
                //                var value = parseInt($(this).val());
                //                if (value >= vipLevelSelect.length) {
                //                    value = 0;
                //                }
                value = $(this).val();
                var text = $(vipLevelSelect).find("option[value=" + value + "]")[0].text;
                if (text == o.data('text')) {
                    o.html(o.data('text'));
                    return false;
                }
                $('#update_post_tips').html('数据提交中...').css({ left: $(window).width() / 2 - 100 }).show();

                var param = {};
                param["Id"] = o.parents("tr").attr('VipId');
                param["field"] = o.parent().attr('field');
                param["value"] = value;
                $.post('EditVipInfo', param, function (data) {
                    if (data.IsSuccessful == 1) {
                        var msg = '修改成功！';
                        o.html(text);
                    } else if (data.OperateMsg != '') {
                        var msg = data.OperateMsg;
                        o.html(o.data('text'));
                    } else {
                        var msg = '修改失败，出现未知错误！';
                        o.html(o.data('text'));
                    }
                    $('#update_post_tips').html(msg).fadeOut(3000);
                }, 'json');
            });
        });

        $('.upd_points').dblclick(function () {
            var o = $(this).children('.upd_txt');
            if (o.children('select').size() && o.children('input').size()) { return false; }

            var s_html = '<select><option value="0">加积分</option><option value="1">减积分</option></select><br /><input value="" />';
            o.data('text', o.html()).html(s_html);
            o.children('input').select();
            o.children('input, select').keyup(function (event) {
                if (event.which == 13) {
                    $(this).blur();
                    var value = isNaN($(this).parent().find('input').val()) ? 0 : parseInt($(this).parent().find('input').val());
                    if (value == '' || !value || isNaN(value)) {
                        o.html(o.data('text'));
                        return false;
                    }

                    var c = $(this).parent().find('select').val();
                    if (c == 1) {
                        value = -value;
                    } 
                    $('#update_post_tips').html('数据提交中...').css({ left: $(window).width() / 2 - 100 }).show();

                    var param = {};
                    param["Id"] = o.parents("tr").attr('VipId');
                    param["field"] = o.parent().attr('field');
                    param["value"] = value;

                    $.post('EditVipInfo', param, function (data) {
                        if (data.IsSuccessful == 1) {
                            var msg = '修改成功！';
                            o.html(parseInt(o.data('text')) + value);
                            if (data.lvl == 1) {
                                //alert(o.parent().parent().children('.upd_select').children('.upd_txt').html());
                                o.parent().parent().children('.upd_select').children('.upd_txt').html(data.level);
                            }
                        } else if (data.OperateMsg != '') {
                            var msg = data.OperateMsg;
                            o.html(o.data('text'));
                        } else {
                            var msg = '修改失败，出现未知错误！';
                            o.html(o.data('text'));
                        }
                        $('#update_post_tips').html(msg).fadeOut(3000);
                    }, 'json');
                }
            });
        });
    }
}

///**
//* 删除左右两端的空格
//*/
//String.prototype.trim=function()
//{
//    return this.replace(/(^\s*)|(\s*$)/g, "");
//}
///**
//* 删除左边的空格
//*/
//String.prototype.ltrim=function()
//{
//     return this.replace(/(^\s*)/g,"");
//}
///**
//* 删除右边的空格
//*/
//String.prototype.rtrim=function()
//{
//     return this.replace(/(\s*$)/g,"");
//}