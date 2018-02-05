
/*
Powered by highcreative
成都和创科技有限公司		
*/


//改变
var wechat_obj = {
    wechat_set: function () {
        frame_obj.search_form_init();
        //frame_obj.file_upload($('#QrCodeUpload'), $('#wechat_set_form input[name=QrCodePath]'), $('#QrCodeDetail'));
        //$('#QrCodeDetail').html(frame_obj.upload_img_detail($('#wechat_set_form input[name=QrCodePath]').val()));
        //图文消息封面上传
        
        var ChangeWeiXinType = function () {
            $('#AppIdRow, #AppSecretRow, #VoiceIdenRow, #WechatKfRow').hide();
            var WeiXinType = $('input[name=WeiXinType]:checked').val();
            if (WeiXinType == 1 || WeiXinType == 2) {
                $('#AppIdRow, #AppSecretRow').show();
            } else if (WeiXinType == 3) {
                $('#AppIdRow, #AppSecretRow, #VoiceIdenRow, #WechatKfRow').show();
            }
        }
        ChangeWeiXinType();
        $('input[name=WeiXinType]').click(function () {
            ChangeWeiXinType();
        });

        $('#wechat_set_form').submit(function () { return false; });
        $('#wechat_set_form input:submit').click(function () {
            if (system_obj.check_form($('*[notnull]'))) { return false };

            $(this).attr('disabled', true);
            $.post('../WeiXinApp/Save', $('#wechat_set_form').serialize(), function (data) {
                if (data.IsSuccessful == true) {
                    alert('设置成功！');
                    window.top.location = '../WeiXinApp/Index';
                } else {
                    alert('设置失败，出现未知错误！');
                }
            }, 'json');
        });
    },

    attention_init: function () {
        var display_row = function () {
            if ($('select[name=ReplyMsgType]').val() == 0) {
                $('#text_msg_row').show();
                $('#img_msg_row').hide();
            } else {
                $('#text_msg_row').hide();
                $('#img_msg_row').show();
            }
        }
        display_row();
        $('select[name=ReplyMsgType]').on('change blur', display_row);
        $('#attention_reply_form').submit(function () { return false; });
        $('#attention_reply_form input:submit').click(function () {
            if ($('select[name=ReplyMsgType]').val() == 0) {
                if (global_obj.check_form($('*[notnull], textarea[name=TextContents]'))) { return false };
            } else {
                if (global_obj.check_form($('*[notnull]'))) { return false };
            }

            $(this).attr('disabled', true);
            var action = "Create";
            if ($("#Id").val() != "") {
                action = "Edit";
            }
            var Id = $("#Id").attr("value"), textContent = $("textarea[name='TextContents']").attr("value"),
                           replyMsgType = $('select[name=ReplyMsgType]').find("option:selected").val(),
                           PicMsgOrTirggerInfoId = $('select[name=MaterialId]').find("option:selected").val(),
                           WeiXinAppId = $("#WeiXinAppId").attr("value"),
                           isOpen = $('input[name=ReplySubscribe]').attr("checked") == "checked";

            var params = {
                Id: Id, ContenInfo: textContent, Type: replyMsgType, PicMsgOrTirggerInfoId: PicMsgOrTirggerInfoId,
                WeiXinAppId: WeiXinAppId, IsOpen: isOpen
            };
            $.post(action, params, function (data) {
                if (data.IsSuccessful == true) {
                    window.location = 'Index';
                } else {
                    alert(data.OperateMsg);
                    $('#attention_reply_form input:submit').attr('disabled', false);
                }
            }, 'json');
        });


        //$('#attention_reply_form').submit(function () {
        //    $('#attention_reply_form input:submit').attr('disabled', true);
        //});

    },

    reply_keyword_init: function () {
        var display_row = function () {
            if ($('select[name=ReplyMsgType]').val() == 0) {
                $('#text_msg_row').show();
                $('#img_msg_row').hide();
            } else {
                $('#text_msg_row').hide();
                $('#img_msg_row').show();
            }
        }

        display_row();
        $('select[name=ReplyMsgType]').on('change blur', display_row);
        $('#keyword_reply_form').submit(function () { return false; });
        $('#keyword_reply_form input:submit').click(function () {
            if ($('select[name=ReplyMsgType]').val() == 0) {
                if (global_obj.check_form($('*[notnull], textarea[name=TextContents]'))) { return false };
            } else {
                if (global_obj.check_form($('*[notnull]'))) { return false };
            }
            //shaosc add
            var action = "Create";
            if ($("#Id").val() != "") {
                action = "Edit";
            }
            var Id = $("#Id").attr("value"), keyword = $("textarea[name='Keywords']").attr("value"), textContent = $("textarea[name='TextContents']").attr("value"),
                            patternMethod = $("input:radio[name =PatternMethod]:checked").val(), replyMsgType = $('select[name=ReplyMsgType]').find("option:selected").val(),
                            PicMsgOrTirggerInfoId = $('select[name=MaterialId]').find("option:selected").val(),
                            WeiXinAppId = $("#WeiXinAppId").attr("value");
            var params = { Id: Id, KeyWord: keyword, patternMethod: patternMethod, ContenInfo: textContent, Type: replyMsgType, PicMsgOrTirggerInfoId: PicMsgOrTirggerInfoId, WeiXinAppId: WeiXinAppId };
            //shaosc add end
            $(this).attr('disabled', true);
            $.post(action, params, function (data) {
                if (data.IsSuccessful == true) {
                    window.location = 'Index';
                } else {
                    alert(data.OperateMsg);
                    $('#keyword_reply_form input:submit').attr('disabled', false);
                }
            }, 'json');
        })
    },

    set_token_init: function () {
        $('#set_token_form').submit(function () { return false; });
        $('#set_token_form input:submit').click(function () {
            if (global_obj.check_form($('*[notnull]'))) { return false };

            var btn_value = $('#set_token_form input:submit').val();
            $('.set_token_msg').css({ display: 'none' }).html('');
            $(this).val('对接中，请耐心等待...').attr('disabled', true);

            $.post('../WeiXinApp/Bind', $('form').serialize(), function (data) {
                if (data.status == 1) {
                    window.location = '../WeiXinApp/InterfaceCfg';
                } else {
                    $('.set_token_msg').css({ display: 'block' }).html(data.msg);
                    $('#set_token_form input:submit').val(btn_value).attr('disabled', false);
                }
            }, 'json');
        });
    },

    menu_init: function () {
        var display_row = function () {
            var typeInfo = $('#MenuType').val();
            if (typeInfo == 0) {
                $('#text_msg_row').show();
                $('#img_msg_row').hide();
                $('#url_msg_row').hide();
            }
            if (typeInfo == 1) {
                $('#text_msg_row').hide();
                $('#img_msg_row').show();
                $('#url_msg_row').hide();
            }
            if (typeInfo == 2) {
                $('#text_msg_row').hide();
                $('#img_msg_row').hide();
                $('#url_msg_row').show();
            }
        }
        display_row();
        $('#MenuType').on('change blur', display_row);
        $('#wechat_menu .m_lefter dl').dragsort({
            dragSelector: 'dd',
            dragEnd: function () {
                var data = $(this).parent().children('dd').map(function () {
                    return $(this).attr('MId');
                }).get();
                $.get('?m=wechat&a=menu', { do_action: 'wechat.menu_order', sort_order: data.join('|') });
            },
            dragSelectorExclude: 'ul, a',
            placeHolderTemplate: '<dd class="placeHolder"></dd>',
            scrollSpeed: 5
        });

        $('#wechat_menu .m_lefter ul').dragsort({
            dragSelector: 'li',
            dragEnd: function () {
                var data = $(this).parent().children('li').map(function () {
                    return $(this).attr('MId');
                }).get();
                $.get('?m=wechat&a=menu', { do_action: 'wechat.menu_order', sort_order: data.join('|') });
            },
            dragSelectorExclude: 'a',
            placeHolderTemplate: '<li class="placeHolder"></li>',
            scrollSpeed: 5
        });

        $('#wechat_menu .m_lefter ul li').hover(function () {
            $(this).children('.opt').show();
        }, function () {
            $(this).children('.opt').hide();
        });

        $('#wechat_menu_form select[name=MsgType]').on('change blur', display_row);
        $('#wechat_menu_form').submit(function () { return false; });
        $('#wechat_menu_form input:submit').click(function () {
            if (global_obj.check_form($('*[notnull]'))) { return false };

            $(this).attr('disabled', true);
            $.post('?', $('form').serialize(), function (data) {
                if (data.status == 1) {
                    window.location = '?m=wechat&a=menu';
                } else {
                    alert(data.msg);
                    $('#wechat_menu_form input:submit').attr('disabled', false);
                }
            }, 'json');
        })

        $('#wechat_menu .publish .btn_green').click(function () {
            var btn_value = $(this).val();
            $(this).val('发布中，请耐心等待...').attr('disabled', true);
            $.get('?do_action=wechat.menu_publish', '', function (data) {
                $('#wechat_menu .publish .btn_green').val(btn_value).attr('disabled', false);
                if (data.status == 1) {
                    alert('菜单发布成功，24小时后可看到效果，或取消关注再重新关注可即时看到效果！');
                } else {
                    alert(data.msg);
                }
            }, 'json');
        });

        $('#wechat_menu .publish .btn_gray').click(function () {
            var btn_value = $(this).val();
            $(this).val('删除中...').attr('disabled', true);
            $.get('?do_action=wechat.menu_wx_del', '', function (data) {
                $('#wechat_menu .publish .btn_gray').val(btn_value).attr('disabled', false);
                if (data.status == 1) {
                    alert('菜单删除成功，24小时后可看到效果，或取消关注再重新关注可即时看到效果！');
                } else {
                    alert(data.msg);
                }
            }, 'json');
        });
    },

    auth_init: function () {
        $('#wechat_auth_form').submit(function () { return false; });
        $('#wechat_auth_form input:submit').click(function () {
            if (global_obj.check_form($('*[notnull]'))) { return false };

            $(this).attr('disabled', true);
            $.post('?', $('#wechat_auth_form').serialize(), function (data) {
                if (data.status == 1) {
                    window.location = '?m=wechat&a=auth';
                } else {
                    alert('设置失败，出现未知错误！');
                }
            }, 'json');
        });
    },

    spread_init: function () {
        var spread_type = function () {
            if ($('#spread_form input[name=SpreadType]:checked').val() == 0) {
                $('#spread_form .pcas').show();
                $('#spread_form .url').hide();
            } else {
                $('#spread_form .pcas').hide();
                $('#spread_form .url').show();
            }
        }
        $('#spread_form input[name=SpreadType]').click(function () {
            spread_type();
        });
        spread_type();

        $('#spread_form').submit(function () { return false; });
        $('#spread_form input:submit').click(function () {
            if (global_obj.check_form($('*[notnull]'))) { return false };

            $(this).attr('disabled', true);
            $.post('?', $('#spread_form').serialize(), function (data) {
                if (data.ret == 1) {
                    window.location = '?m=wechat&a=spread';
                } else {
                    alert('设置失败，出现未知错误！');
                }
            }, 'json');
        });
    },

    plugin_init: function () {
        $('#plugin img[plugin]').addClass('pointer').click(function () {
            var img_obj = $(this);
            $.get('?', 'do_action=wechat.plugin&plugin=' + img_obj.attr('plugin') + '&Status=' + img_obj.attr('Status'), function (data) {
                if (data.ret == 1) {
                    var img = img_obj.attr('Status') == 0 ? 'on' : 'off';
                    img_obj.attr('src', system_obj.domain('static') + '/images/ico/' + img + '.gif');
                    img_obj.attr('Status', img_obj.attr('Status') == 0 ? 1 : 0);
                } else {
                    alert('设置失败，出现未知错误！');
                }
            }, 'json');
        });
    },

    shopping_init: function () {
        $('#wechat_payment_form input:checkbox').each(function (index, element) {
            var o = $(element).parent().parent().next();
            if (!$(element).is(':checked')) {
                o.hide();
            }
            $(element).click(function () {
                if ($(this).is(':checked')) {
                    o.show();
                } else {
                    o.hide();
                }
            });
        });

        $('#wechat_payment_form').submit(function () { return false; });
        $('#wechat_payment_form .submit input').click(function () {
            $(this).attr('disabled', true);
            $.post('?', $('#wechat_payment_form').serialize(), function (data) {
                if (data.status == 1) {
                    window.location = '?m=wechat&a=shopping';
                } else {
                    $('#wechat_payment_form .submit input').attr('disabled', false);
                }
            }, 'json');
        });

        $('#shopping .shipping dl').dragsort({
            dragSelector: 'dd',
            dragEnd: function () {
                var data = $(this).parent().children('dd').map(function () {
                    return $(this).attr('SId');
                }).get();
                $.get('?m=shop&a=shopping', { do_action: 'wechat.shopping_shipping_order', sort_order: data.join('|') });
            },
            dragSelectorExclude: 'a',
            placeHolderTemplate: '<dd class="placeHolder"></dd>',
            scrollSpeed: 5
        });
        $('#wechat_shipping_form').submit(function () { return false; });
        $('#wechat_shipping_form input:submit').click(function () {
            if (system_obj.check_form($('*[notnull]'))) { return false };
            $(this).attr('disabled', true);
            $.post('?', $('#wechat_shipping_form').serialize(), function (data) {
                if (data.status == 1) {
                    window.location = '?m=wechat&a=shopping';
                } else {
                    alert(data.msg);
                    $('#wechat_shipping_form input:submit').attr('disabled', false);
                }
            }, 'json');
        });

        $('#wechat_service_area_form').submit(function () { return false; });
        $('#wechat_service_area_form .submit input').click(function () {
            $(this).attr('disabled', true);
            $.post('?', $('#wechat_service_area_form').serialize(), function (data) {
                if (data.status == 1) {
                    window.location = '?m=wechat&a=shopping';
                } else {
                    $('#wechat_service_area_form .submit input').attr('disabled', false);
                }
            }, 'json');
        });

    }
}