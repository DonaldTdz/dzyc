(function ($) {
    $.extend({
        _overlay: function (msg) {
            var _over = { overlayOpacity: .90, overlayColor: '#AAA' }
            $('body').append('<div id="d_overlay" style="overflow:hidden"><div id="d_c_lay" >' + msg + '</div></div>');
            $("#d_overlay").css({
                position: 'absolute',
                zIndex: 9998,
                top: $(window).scrollTop(),
                left: '0px',
                height: $(document).height(),
                background: _over.overlayColor,
                opacity: _over.overlayOpacity,
                textAlign: 'center'
            });
            $("#d_overlay").height($(window).height());
            $("#d_overlay").width($(window).width());
            $('#d_c_lay').css({
                position: 'relative',
                zIndex: 9999,
                top: $(window).height() / 2 - 50,
                color: 'white'
            });

            /*ie下*/
            $('body').attr('scroll', 'no');
            /*firefox*/
            document.body.style.overflow = "hidden";
            $('body').css('overflow-y', 'hidden');

            $(window).resize(function () {
                $("#d_overlay").height($(window).height());
                $("#d_overlay").width($(window).width());
                $("#d_overlay").css({
                    top: $(window).scrollTop(),
                    left: '0px'
                });
            });
        },

        _dialogs: function (url, isRef, options, isresult, userid, typeid, contid, hrefs) {
            if (!url) {
                return false;
            }
            // flush cache
            function flushCache() {
                cache = {};
                cache.length = 0;
            };

            // flush cache
            flushCache();
            if (options) {
                if (typeof (options) == 'function') {
                    cache.length++;
                    cache['callback'] = options;
                } else {
                    for (var data in options) {
                        cache[data] = options[data];
                        cache.length++;
                        //King.log.write(data);
                    }
                    //King.log.write(cache.length+'........................');
                }
            }

            $('#d_overlay').append('<div id="winpop"><div class="win" ><div id="win_title"></div><iframe width="100%"  height="95%" frameborder="0" src="' + url + '" id="frmpop" ></iframe></div><div style="height:5px">&nbsp;</div></div>');
            var wleft = (parseInt($(window).width() - $('#winpop').width()) / 2) + 'px';
            $('#winpop').css('left', wleft);
            var htm = '<span style="float:left;">地址薄</span><span id="winclose" style="cursor:default;">关闭</span>';
            $('#win_title').html(htm);
            $(window).resize(function () {
                var wleft = (parseInt($(window).width() - $('#winpop').width()) / 2) + 'px';
                $('#winpop').css('left', wleft);
            });
        },

        _dialog: function (o, url, src, options) {
            if (!o || !url) {
                return false;
            }
            var es = (src ? 'click' : 'dblclick');
            src = src || o;

            var key = "?";
            if (url.indexOf("?") != -1) {
                key = "&";
            }
            if ($('#' + o).attr('disabled')) {
                $('#' + src).bind(es, function () {
                    alert('当前不能选择,已禁用选择');
                });
                return false;
            }

            url = url + key + "dialog=1&dialogkey=" + o;
            //King.log.write(url);
            $('#' + src).unbind(es);
            $('#' + src).bind(es, function () {
                $._showdialog(o, url, options);
            });
        },

        _showdialog: function (o, url, options) {
            // flush cache
            function flushCache() {
                cache = {};
                cache.length = 0;
            };

            // flush cache
            flushCache();
            if (options) {
                if (typeof (options) == 'function') {
                    cache.length++;
                    cache['callback'] = options;
                } else {
                    for (var data in options) {
                        cache[data] = options[data];
                        cache.length++;
                    }
                }
            };
            var html = '<div id="winpop"  style="width:99%; height:90%;left:20px; top:50px;"><div class="win" ><div id="win_title"></div><iframe width="100%"  height="95%" frameborder="0" src="' + url + '" id="frmpop" ></iframe></div><div style="height:5px">&nbsp;</div></div>';
            $('#d_overlay').append(html);
            var wleft = (parseInt($(window).width() - $('#winpop').width()) / 2) + 'px';
            $('#winpop').css('left', wleft);

            var htm;
            if ($('#frmpop').attr('src').indexOf('UserShopInfoPop') > 0) {
                htm = '<span style="float:left;">请选择卖家</span><span id="winclose" style="cursor:default;">关闭</span>';
            }
            else {
                htm = '<span style="float:left; ">地址信息:' + mb_cutstr($('#frmpop').attr('src'), 150, "") + '</span><span id="winclose" style="cursor:default;">关闭</span>';
            }

            $('#win_title').html(htm);
            $(window).resize(function () {
                var wleft = (parseInt($(window).width() - $('#winpop').width()) / 2) + 'px';
                $('#winpop').css('left', wleft);
            });
        },
        _showPromptArea: function (func) {
            $._overlay('');
            var winWidth = 600;
            var winHeight = 300;
            var wleft = (parseInt($(window).width() - winWidth) / 2);
            var wtop = (parseInt($(window).height() - winHeight) / 2);
            var html = '<div id="winpop"  style="width:' + winWidth + 'px; height:' + winHeight + 'px;left:' + wleft + 'px; top:' + wtop + 'px;"><div class="win" ><div id="win_title"><span style="float:left;">请输入文字</span><span id="winclose" style="cursor:default;">关闭</span></div><div style="height:5px">&nbsp;</div>';
            html += ' <div style="text-align:left; padding-left:10px;"><textarea id="txtPromptValue" style="" name="txtPromptValue" style="width:580px; height:230px;" class="input_text"></textarea></div>';
            html += '<div style="text-align:right; padding-right:20px;"><input type="button" value="&nbsp;&nbsp;确定&nbsp;&nbsp;" id="btnPropmtClick"></div>';
            html += "</div>";
            $('#d_overlay').append(html);
            $(window).resize(function () {
                var wleft = (parseInt($(window).width() - $('#winpop').width()) / 2) + 'px';
                var wtop = (parseInt($(window).height() - $('#winpop').height()) / 2) + 'px';
                $('#winpop').css('left', wleft);
                $('#winpop').css('top', wtop);
            });
            $('#btnPropmtClick').click(function () {
                var value = $('#txtPromptValue').val();
                if (value) {
                    if ($.isFunction(func)) {
                        func(value);
                    }
                }

            });
        },
        _showPrompt: function (title, text, func) {
            $._overlay('');
            var winWidth = 600;
            var winHeight = 90;
            var wleft = (parseInt($(window).width() - winWidth) / 2);
            var wtop = (parseInt($(window).height() - winHeight) / 2);

            $(window).resize(function () {
                var wleft = (parseInt($(window).width() - $('#winpop').width()) / 2) + 'px';
                var wtop = (parseInt($(window).height() - $('#winpop').height()) / 2) + 'px';
                $('#winpop').css('left', wleft);
                $('#winpop').css('top', wtop);
            });
            $('#btnPropmtClick').click(function () {
                var value = $('#txtPromptValue').val();
                if (value) {
                    if ($.isFunction(func)) {
                        func(value);
                    }
                }

            });
        },
        _getSearchAsArray: function (srchStr) {
            var results = new Array(); var input = unescape(srchStr.substr(1));
            if (input) {
                var srchArray = input.split("&");
                var tempArray = new Array();
                for (var i = 0; i < srchArray.length; i++) { tempArray = srchArray[i].split("="); results[tempArray[0]] = tempArray[1]; }
            }
            return results;
        },
        _GetSearch: function (key, val) {
            var s = location.search;

            var value = $._GetStrSearch(s, key);
            if (value) {
                return value;
            }
            return val ? val : 0;
        },
        _GetStrSearch: function (url, key) {
            var sArray = $._getSearchAsArray(url);
            value = sArray[key];
            return value;
        },

        _dialogkey: function (key, val, stype) {
            dialogkey = '#' + $._GetSearch('dialogkey');
            $(window.parent.document).find(dialogkey).val(val.trim());
            $(window.parent.document).find(dialogkey.replace("txt", "hid")).val(key);
            if (key) {
                $(window.parent.document).find(dialogkey).trigger('change');
            }
            if (!stype && parseInt(stype) > 0) {
                $(window.parent.document).find('hidtype').val($._GetSearch('stype'));
            }
            //window.parent.dialog_callback;
            if (typeof (window.parent.dialog_callback) == 'function') {
                window.parent.dialog_callback(dialogkey);
            }
            //        if(typeof(window.parent.dialogcallback)=='function')
            //        {
            //            window.parent.dialogcallback();            
            //        }
            $(window.parent.document).find(dialogkey).trigger('_required', [$(window.parent.document).find(dialogkey).attr('validaterull')]);


            /*ie下*/
            $(window.parent.document).find('body').attr('scroll', 'yes');
            /*firefox*/
            window.parent.document.body.style.overflow = 'auto';
            $(window.parent.document).find('body').css('overflow-y', '');
            //King.log.write(val);
            $(window.parent.document).find('#d_overlay').remove();
        },


        _regdilogkey: function (o, eq1, eq2, trclick) {
            if (trclick != 'false') {
                $('#' + o + ' tr').click(function (e, n) {
                    if ($(this).attr("rowIndex") > 0) {
                        key = $(this).find('td').eq(eq1).html();
                        val = $(this).find('td').eq(eq2).html();
                        $._dialogkey(key, val);
                    }
                });
            } else {
                $('#' + o + ' tr').find('td:eq(' + eq2 + ')').click(function (e, n) {
                    if ($(this).parent().attr("rowIndex") > 0) {
                        key = $(this).parent().find('td').eq(eq1).html();
                        val = $(this).parent().find('td').eq(eq2).html();
                        $._dialogkey(key, val);
                    }
                });
            }
        },


        _regdilogkeys: function (o, eq1, eq2, trclick) {
            if (trclick != 'false') {
                $('#' + o + ' span').click(function (e, n) {
                    if ($(this).parent().parent().attr("rowIndex") > 0) {
                        key = $(this).parent().parent().find('td').eq(eq1).html();
                        val = $(this).parent().parent().find('td').eq(eq2).html();
                        $._dialogkey(key, val);
                    }
                });
            } else {
                $('#' + o + ' span').find('td:eq(' + eq2 + ')').click(function (e, n) {
                    if ($(this).parent().attr("rowIndex") > 0) {
                        key = $(this).parent().find('td').eq(eq1).html();
                        val = $(this).parent().find('td').eq(eq2).html();
                        $._dialogkey(key, val);
                    }
                });
            }
        },
        _regDilogClicks: function (o, eq1, eq2) {
            $('#' + o + ' span').click(function (e, n) {
                if ($(this).parent().parent().attr("rowIndex") > 0) {
                    key = $(this).parent().parent().find('td').eq(eq1).find("input[type='checkbox']").val();
                    val = $(this).parent().parent().find('td').eq(eq1).find("input[type='checkbox']").val();
                    $._dialogkey(key, val);
                }
            });
        },
        _regDilogClick: function (id, key, cbName) {
            $('#' + id).click(function (e, n) {
                var val = getCheckBoxValue(cbName);
                dialogkey = '#' + $._GetSearch('dialogkey');
                $(window.parent.document).find(dialogkey).val(val.trim());
                if (typeof (window.parent.dialog_callback) == 'function') {
                    window.parent.dialog_callback(dialogkey);
                }
                $(window.parent.document).find(dialogkey).trigger('_required', [$(window.parent.document).find(dialogkey).attr('validaterull')]);
                /*ie下*/
                $(window.parent.document).find('body').attr('scroll', 'yes');
                /*firefox*/
                window.parent.document.body.style.overflow = 'auto';
                $(window.parent.document).find('body').css('overflow-y', '');
                //King.log.write(val);
                $(window.parent.document).find('#d_overlay').remove();
            });
        },
        _selectVal: function (selectVal) {
            dialogkey = '#' + $._GetSearch('dialogkey');
            $(window.parent.document).find(dialogkey).val(selectVal.trim());
            if (typeof (window.parent.dialog_callback) == 'function') {
                window.parent.dialog_callback(dialogkey);
            }
            $(window.parent.document).find(dialogkey).trigger('_required', [$(window.parent.document).find(dialogkey).attr('validaterull')]);
            /*ie下*/
            $(window.parent.document).find('body').attr('scroll', 'yes');
            /*firefox*/
            window.parent.document.body.style.overflow = 'auto';
            $(window.parent.document).find('body').css('overflow-y', '');
            //King.log.write(val);
            $(window.parent.document).find('#d_overlay').remove();
        }
    });
})(jQuery);
function dialog_callback(dialogkey) {
    if (cache.callback) {
        setTimeout(function () { cache.callback(dialogkey) }, 1);
    }
}


