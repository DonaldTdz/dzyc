﻿var global_obj = {
    file_upload: function (file_input_obj, filepath_input_obj, img_detail_obj, action, size) {
        var multi = (typeof (arguments[5]) == 'undefined') ? false : arguments[4];
        var queueSizeLimit = (typeof (arguments[6]) == 'undefined') ? 6 : arguments[6];
        var callback = arguments[7];
        var fileExt = (typeof (arguments[8]) == 'undefined') ? '*.jpg;*.png;*.gif;*.jpeg;*.bmp' : arguments[8];
        var fileSize = (typeof (arguments[9]) == 'undefined') ? 200 : arguments[9];

        file_input_obj.omFileUpload({
            action: action,
            actionData: {
                do_action: 'action.file_upload',
                size: size
            },
            fileExt: fileExt,
            fileDesc: 'Files',
            sizeLimit: fileSize * 1024,
            onError: function (ID, fileObj, errorObj, event) {
                if (errorObj.type == 'File Size') {
                    alert('上传的文件大小不能超过' + fileSize + 'KB！');
                }
            },
            autoUpload: true,
            multi: multi,
            queueSizeLimit: queueSizeLimit,
            swf: "../DHQRJs/om-fileupload.swf",
            method: 'post',
            onComplete: function (ID, fileObj, response, data, event) {
                var jsonData = eval('(' + response + ')');
                if (jsonData.status == 1) {
                    if ($.isFunction(callback)) {
                        callback(jsonData.filepath);
                    } else {
                        if (jsonData.filepath.indexOf("@") >= 0) {
                            var filePath = jsonData.filepath.split("@", 1);
                            var fileSourceId = jsonData.filepath.split("@", 2)[1];
                            filepath_input_obj.val(filePath);
                            img_detail_obj.html(global_obj.img_link(filePath));
                            $("#DHQRSourceId").val(fileSourceId);
                        }
                        else {
                            filepath_input_obj.val(jsonData.filepath);
                            img_detail_obj.html(global_obj.img_link(jsonData.filepath));
                        }
                    }
                } else {
                    alert('文件上传失败，出现未知错误！');
                };
            }
        });
    },

    img_link: function (img) {
        if (!img) { return; }
        return '<a href="' + img + '" target="_blank"><img src="' + img + '"></a>';
    },

    check_form: function (obj) {
        var flag = false;
        obj.each(function () {
            if ($(this).val() == '') {
                $(this).css('border', '1px solid red');
                flag == false && ($(this).focus());
                flag = true;
            } else {
                $(this).removeAttr('style');
            }
        });
        return flag;
    },

    config_form_init: function (args) {
        global_obj.file_upload($('#ReplyImgUpload'), $('#config_form input[name=ReplyImgPath]'), $('#ReplyImgDetail'), args.uploadAction);
        $('#ReplyImgDetail').html(global_obj.img_link($('#config_form input[name=ReplyImgPath]').val()));

        //        $('#config_form input[name="submit_button"]').click(function () {
        //            if (global_obj.check_form($('*[notnull]'))) { return false; };
        //            //shaosc add
        //            var Id = $("#Id").attr("value"), keyword = $("input[name='ReplyKeyword']").attr("value"), storesName = $("input[name='StoresName']").attr("value"),
        //                            patternMethod = $("input:radio[name =PatternMethod]:checked").val(), msgTitle = $("input[name='ReplyTitle']").attr("value"),
        //                            DHQRTriggerInfoId = $('#DHQRTriggerInfoId').val(),msgCoverPath = $("input[name='ReplyImgPath']").attr("value");
        //            var params = { Id: Id, KeyWord: keyword, patternMethod: patternMethod, Name: storesName,MsgTitle:msgTitle, MsgCoverPath:msgCoverPath,DHQRTriggerInfoId: DHQRTriggerInfoId };
        //            //shaosc add end
        //            $(this).attr('disabled', true);
        //            $.post(args.submitAction, params, function (data) {
        //                if (data.status == 1) {
        //                    window.location.reload();
        //                } else {
        //                    alert(data.msg);
        //                    $('#config_form input:submit').attr('disabled', false);
        //                }
        //            }, 'json');
        //        });
    },

    config_mulform_init: function (args) {
        global_obj.file_upload(args.imgUpload, $('#config_form input[name=ReplyImgPath]'), $('#ReplyImgDetail'), args.uploadAction);
        global_obj.file_upload($('#ReplyImgUpload'), $('#config_form input[name=ReplyImgPath]'), $('#ReplyImgDetail'), args.uploadAction);
        $('#ReplyImgDetail').html(global_obj.img_link($('#config_form input[name=ReplyImgPath]').val()));

    },


    reserve_form_init: function () {
        $('.reverve_field_table .input_add').click(function () {
            $('.reverve_field_table tr[FieldType=text]:hidden').eq(0).show();
            if (!$('.reverve_field_table tr[FieldType=text]:hidden').size()) {
                $(this).hide();
            }
        });
        $('.reverve_field_table .input_del').click(function () {
            $('.reverve_field_table .input_add').show();
            $(this).parent().parent().hide().find('input').val('');
        });
        $('.reverve_field_table .select_add').click(function () {
            $('.reverve_field_table tr[FieldType=select]:hidden').eq(0).show();
            if (!$('.reverve_field_table tr[FieldType=select]:hidden').size()) {
                $(this).hide();
            }
        });
        $('.reverve_field_table .select_del').click(function () {
            $('.reverve_field_table .select_add').show();
            $(this).parent().parent().hide().find('input').val('');
        });
    },

    map_init: function () {
        var myAddress = $('input[name=Address]').val();
        var destPoint = new BMap.Point($('input[name=PrimaryLng]').val(), $('input[name=PrimaryLat]').val());
        var map = new BMap.Map('map');
        map.centerAndZoom(new BMap.Point(destPoint.lng, destPoint.lat), 20);
        map.enableScrollWheelZoom();
        map.addControl(new BMap.NavigationControl());
        var marker = new BMap.Marker(destPoint);
        map.addOverlay(marker);

        map.addEventListener('click', function (e) {
            destPoint = e.point;
            set_primary_input();
            map.clearOverlays();
            map.addOverlay(new BMap.Marker(destPoint));
        });

        var ac = new BMap.Autocomplete({ 'input': 'Address', 'location': map });
        ac.addEventListener('onhighlight', function (e) {
            ac.setInputValue(e.toitem.value.business);
        });

        ac.setInputValue(myAddress);
        ac.addEventListener('onconfirm', function (e) {//鼠标点击下拉列表后的事件
            var _value = e.item.value;
            myAddress = _value.business;
            ac.setInputValue(myAddress);

            map.clearOverlays();    //清除地图上所有覆盖物
            local = new BMap.LocalSearch(map, { renderOptions: { map: map} }); //智能搜索
            local.setMarkersSetCallback(markersCallback);
            local.search(myAddress);
        });

        var markersCallback = function (posi) {
            $('#Primary').attr('disabled', false);
            if (posi.length == 0) {
                alert('定位失败，请重新输入详细地址或直接点击地图选择地点！');
                return false;
            }
            for (var i = 0; i < posi.length; i++) {
                if (i == 0) {
                    destPoint = posi[0].point;
                    set_primary_input();
                }
                posi[i].marker.addEventListener('click', function (data) {
                    destPoint = data.target.getPosition(0);
                });
            }
        }

        var set_primary_input = function () {
            $('input').filter('[name=PrimaryLng]').val(destPoint.lng).end().filter('[name=PrimaryLat]').val(destPoint.lat);
        }

        $('input[name=Address]').keyup(function (event) {
            if (event.which == 13) {
                $('#Primary').click();
            }
        });

        $('#Primary').click(function () {
            if (global_obj.check_form($('input[name=Address]'))) { return false };
            $(this).attr('disabled', true);
            local = new BMap.LocalSearch(map, { renderOptions: { map: map} }); //智能搜索
            local.setMarkersSetCallback(markersCallback);
            local.search($('input[name=Address]').val());
            return false;
        });
    }
}