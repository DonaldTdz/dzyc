var qunfa_obj = {
    material_init: function () {
        $('#qunfa>.list').masonry({ itemSelector: '.item', columnWidth: 367 });
        $('#qunfa .upload a').click(function () {
            var o = $(this);
            if (o.html() != '上传') { return; }
            o.html('上传中...');

            $.get('?', 'do_action=qunfa.upload&MId=' + o.attr('rel'), function (data) {
                alert(data.msg);
                if (data.ret == 1) {
                    o.parent().html('已上传');
                } else {
                    o.html('上传');
                }
            }, 'json');
        });
    },

    material_edit_init: function () {
        $('#qunfa').height(605);
        var myckeditor = CKEDITOR.replace('Description', {
            width: 370,
            height: 250,
            toolbar: [['Source', 'Image', 'Bold', 'Link', 'Unlink'], ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'TextColor', 'BGColor', 'FontSize']]
        });

        var cur_id = '#multi_msg_0';
        var material_list_even = function () {
            $('.m_lefter .first, .m_lefter .list').each(function () {
                var children = $(this).children('.control');
                $(this).mouseover(function () { children.css({ display: 'block' }); });
                $(this).mouseout(function () { children.css({ display: 'none' }); });

                children.children('a[href*=#del]').click(function () {
                    if ($('.m_lefter .list').size() < 1) {
                        alert('无法删除，素材至少需要1条消息！');
                        return false;
                    }
                    if (confirm('删除后不可恢复，继续吗？')) {
                        $(this).parent().parent().remove();
                        $('.m_lefter .first a[href*=#mod]').click();
                        $('.mod_form').css({ top: 37 });
                        $('#qunfa').height(605);
                    }
                });

                children.children('a[href*=#mod]').click(function () {
                    var position = $(this).parent().offset();
                    var material_form_position = $('#qunfa_material_form').offset();
                    cur_id = '#' + $(this).parent().parent().attr('id');
                    $('.mod_form').css({ top: position.top - material_form_position.top });
                    $('#qunfa').height(position.top - material_form_position.top + 570);
                    $('.mod_form input[name=inputTitle]').val($(cur_id + ' input[name=Title\\[\\]]').val());
                    $('.mod_form select[name=inputUrl]').find("option[value='" + $(cur_id + ' input[name=Url\\[\\]]').val() + "']").attr('selected', true);
                    myckeditor.setData($(cur_id + ' textarea').val());
                    $('.big_img_size_tips').html((cur_id == '#multi_msg_0' ? '640*360px' : '300*300px') + '，128KB以内');
                    $('.m_lefter').data('cur_id', cur_id);
                    frame_obj.file_upload($('#MsgFileUpload'), $(cur_id + ' input[name=ImgPath\\[\\]]'), $(cur_id + ' .img'));
                });
                $('.mod_form select[name=inputUrl]').find("option[value='" + $('input[name=Url\\[\\]]').val() + "']").attr('selected', true);
            });
        }

        frame_obj.file_upload($('#MsgFileUpload'), $('.m_lefter .first input[name=ImgPath\\[\\]]'), $('.first .img'));
        $('.m_lefter').data('cur_id', '#' + $('.m_lefter .first').attr('id'));
        $('.mod_form input').filter('[name=inputTitle]').on('keyup paste blur', function () {
            $(cur_id + ' input[name=Title\\[\\]]').val($(this).val());
            $(cur_id + ' .title').html($(this).val());
        })
        $('.mod_form select').filter('[name=inputUrl]').change(function () {
            $(cur_id + ' input[name=Url\\[\\]]').val($(this).val());
        });
        setInterval(function () {
            $(cur_id + ' textarea').val(myckeditor.getData());
        }, 200);

        var list_item_html = '<div class="info"><div class="title">标题</div><div class="img">缩略图</div></div>';
        list_item_html += '<div class="control"><a href="#mod"><img src="' + system_obj.domain('static') + '/images/ico/mod.gif" /></a> <a href="#del"><img src="' + system_obj.domain('static') + '/images/ico/del.gif" /></a></div>';
        list_item_html += '<input type="hidden" name="Title[]" value="" /><input type="hidden" name="Url[]" value="" /><input type="hidden" name="ImgPath[]" value="" /><textarea name="Description[]"></textarea>';

        material_list_even();
        $('a[href=#add]').click(function () {
            $(this).blur();
            if ($('.m_lefter .list').size() >= 9) {
                alert('你最多只可以加入10条图文消息！');
                return false;
            }
            $('.m_lefter .list, a[href*=#mod], a[href*=#del]').off();
            $('<div class="list" id="id_' + Math.floor(Math.random() * 1000000) + '">' + list_item_html + '</div>').insertBefore($('.m_lefter .add'));
            $('.m_lefter .list:last').children('.info').children('.title').html('标题').siblings('.img').html('缩略图');
            $('.m_lefter .list:last input').filter('[name=Title\\[\\]]').val('').end().filter('[name=Url\\[\\]]').val('').end().filter('[name=ImgPath\\[\\]]').val('');
            material_list_even();
        });

        $('#qunfa_material_form').submit(function () {
            if (system_obj.check_form($('*[notnull]'))) { return false };
            $('#qunfa_material_form input:submit').attr('disabled', true);
            return true;
        });
    },

    send_init: function () {
        $('#send_form').submit(function () { return false; });
        $('#send_form input:submit').click(function () {
            $(this).attr('disabled', true);
            $.post('?', $('#send_form').serialize(), function (data) {
                $('#send_form input:submit').attr('disabled', false);
                alert(data.msg);
                if (data.ret == 1) {
                    window.location = './?m=qunfa&a=send_logs';
                }
            }, 'json');
        });
    }
}