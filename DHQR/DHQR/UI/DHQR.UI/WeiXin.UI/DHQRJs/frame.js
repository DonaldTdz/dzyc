

var frame_obj = {
    main_page_init: function () {
        if (self.location != top.location) { top.location = self.location; }
        $('body, html').css('overflow', 'hidden');
        $('a').click(function () { this.blur(); });
        $('#main .menu a').click(function () {
            $('#main .menu div').removeClass('cur');
            $(this).parent().addClass('cur');
        });

        $('#main').height($(window).height() - $('#header').height() - $('#footer').height());
        var w = $(window).width() - 220;
        w = w < 780 ? 780 : w;
        $('#main .iframe').width(w);
        $('#main .menu').jScrollPane();

        if (!$('#main dl.show').size()) {
            $('#header a').click(function () {
                $('#main .menu dt, #main .menu div').removeClass('cur');
                $('#main .menu dd').hide();
                $('#main .menu').jScrollPane();
            });
            $('#main .menu dt').off().click(function () {
                $('#main .menu dt, #main .menu div').removeClass('cur');
                $('#main .menu dd').not($(this).next().filter('dd')).hide();
                var url = $(this).next().find('div:first').addClass('cur').find('a:first').attr('href');
                $(this).addClass('cur').next().filter('dd').slideDown(function () {
                    $('#main .menu').jScrollPane();
                    $('iframe').attr('src', url);
                });
            });
        }
    },

    search_form_init: function () {
        $('.r_con_search_form input[type=text]').each(function () {
            $(this).val(frame_obj.utf8_decode(frame_obj.get_request_value($(this).attr('name'))));
        });
        $('.r_con_search_form select').each(function () {
            $(this).find('option[value=' + frame_obj.utf8_decode(frame_obj.get_request_value($(this).attr('name'))) + ']').attr('selected', true);
        });
    },

    get_request_value: function (argname) {
        var url = document.location.href;
        var arrStr = url.substring(url.indexOf('?') + 1).split('&');
        for (var i = 0; i < arrStr.length; i++) {
            var loc = arrStr[i].indexOf(argname + '=');
            if (loc != -1) {
                return arrStr[i].replace(argname + '=', '').replace('?', '');
                break;
            }
        }
        return '';
    },

    utf8_decode: function (txt) {
        txt = unescape(txt).replace(/\+/g, ' ');
        var string = '';
        var i = c = c1 = c2 = 0;
        while (i < txt.length) {
            c = txt.charCodeAt(i);
            if (c < 128) {
                string += String.fromCharCode(c);
                i++;
            } else if (c > 191 && c < 224) {
                c2 = txt.charCodeAt(i + 1);
                string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
                i += 2;
            } else {
                c2 = txt.charCodeAt(i + 1);
                c3 = txt.charCodeAt(i + 2);
                string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
                i += 3;
            }
        }
        return string;
    },

    ajax_post_tips: function (remove, msg) {
        if (remove == 1) {
            $('#ajax_post_tips').html(msg).fadeOut(5000, function () {
                $('#ajax_post_tips').remove();
            });
        } else {
            $('body').prepend('<div id="ajax_post_tips">数据提交中...</div>');
            $('#ajax_post_tips').css({ position: 'fixed', width: 250, height: 36, 'line-height': '36px', 'font-size': '12px', background: '#1584D5', color: '#fff', 'text-align': 'center', left: $(window).width() / 2 - 125, top: 200 });
        }
    },

    file_upload: function (file_input_obj, filepath_input_obj, img_detail_obj,action,size) {
        var multi = (typeof (arguments[4]) == 'undefined') ? false : arguments[4];
        var queueSizeLimit = (typeof (arguments[5]) == 'undefined') ? 5 : arguments[5];
        var callback = arguments[6];
        var fileExt = (typeof (arguments[7]) == 'undefined') ? '*.jpg;*.png;*.gif;*.jpeg;*.bmp' : arguments[7];
        var fileSize = (typeof (arguments[8]) == 'undefined') ? 300 : arguments[8];

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
            swf: '../../DHQRJs/fileupload.swf?r=' + Math.random(),
            method: 'post',
            onComplete: function (ID, fileObj, response, data, event) {
                var jsonData = eval('(' + response + ')');
                if (jsonData.status == 1) {
                    if ($.isFunction(callback)) {
                        callback(jsonData.filepath);
                    } else {
                        filepath_input_obj.val(jsonData.filepath);
                        img_detail_obj.html(frame_obj.upload_img_detail(jsonData.filepath));
                    }
                } else {
                    alert('文件上传失败，出现未知错误！');
                };
            }
        });
    },

    upload_img_detail: function (img) {
        if (!img) { return; }
        return '<a href="' + img + '" target="_blank"><img src="' + img + '"></a>';
    },

    config_form_init: function () {
        frame_obj.file_upload($('#ReplyImgUpload'), $('#config_form input[name=ReplyImgPath]'), $('#ReplyImgDetail'));
        $('#ReplyImgDetail').html(frame_obj.upload_img_detail($('#config_form input[name=ReplyImgPath]').val()));

        $('#config_form').submit(function () { return false; });
        $('#config_form input:submit').click(function () {
            if (system_obj.check_form($('*[notnull]'))) { return false; };

            $(this).attr('disabled', true);
            $.post('?', $('#config_form').serialize(), function (data) {
                if (data.status == 1) {
                    window.location.reload();
                } else {
                    alert(data.msg);
                    $('#config_form input:submit').attr('disabled', false);
                }
            }, 'json');
        });
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
            local = new BMap.LocalSearch(map, { renderOptions: { map: map } }); //智能搜索
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
            if (system_obj.check_form($('input[name=Address]'))) { return false };
            $(this).attr('disabled', true);
            local = new BMap.LocalSearch(map, { renderOptions: { map: map } }); //智能搜索
            local.setMarkersSetCallback(markersCallback);
            local.search($('input[name=Address]').val());
            return false;
        });
    },

    chart: function () {
        $('.chart').height(500).highcharts({
            chart: {
                type: 'line',
                backgroundColor: '#F3F3F3'
            },
            title: { text: '' },
            xAxis: { categories: chart_data.date },
            yAxis: [{
                title: { text: '' },
                min: 0
            }],
            plotOptions: {
                line: {
                    dataLabels: { enabled: true },
                    enableMouseTracking: false
                }
            },
            series: chart_data.count,
            exporting: { enabled: false }
        });
    },

    chart_pie: function () {
        $('.chart').height(500).highcharts({
            title: { text: '' },
            credits: { enabled: false },
            tooltip: {
                pointFormat: '{series.name}: <b>{point.percentage:.2f}%</b>'
            },
            plotOptions: {
                pie: {
                    allowPointSelect: true,
                    cursor: 'pointer',
                    dataLabels: {
                        enabled: true,
                        color: '#000000',
                        connectorColor: '#000000',
                        format: '<b>{point.name}</b>: {point.percentage:.2f} %'
                    }
                }
            },
            series: [{
                type: 'pie',
                name: '百分比',
                data: user_area_data
            }]
        });
    }
}