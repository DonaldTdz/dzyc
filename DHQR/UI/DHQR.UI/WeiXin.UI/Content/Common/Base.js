
//=================grid操作======================//
//设置grid默认参数
$(function () {
    if ($.jgrid != undefined) {
        $.jgrid.defaults = $.extend($.jgrid.defaults, {
            datatype: "local",
            datatype: "json",
            mtype: "POST",
            rowNum: 15,
            rowList: [15, 30],
            autowidth: true,
            sortorder: "asc",
            pagerpos: "left",
            rownumbers: true,
            recordpos: "right",
            height: 300,
            jsonReader: { repeatitems: false },
            viewrecords: true
        });
    }
});

//刷新grid，使之重新从服务器加载数据
//loadonce时使用post直接获取array格式数据，否则用grid咨询获取数据
function refreshGrid(gridId) {
    var grid = $("#" + gridId);
    if (grid.length == 0) {
        throw "传入的gridId：" + gridId + "不正确。";
    }
    if (grid.jqGrid("getGridParam", "loadonce")) {
        var currentPage = grid.jqGrid("getGridParam", "page");
        if (currentPage == 0) {
            currentPage = 1;
        }
        var url = grid.jqGrid("getGridParam", "url");
        var postData = grid.jqGrid("getGridParam", "postData");
        $.post(url, postData, function (data) {
            grid.removeData("beforeFilterData");
            grid.jqGrid('clearGridData', true);
            grid.jqGrid("setGridParam", {
                data: data,
                page: currentPage
            }).trigger("reloadGrid");
        });
    } else {
        grid.jqGrid("setGridParam", {
            datatype: "json"
        }).trigger("reloadGrid");
    }
}
//清理数据
function CleanFormData(formId) { showObjectOnForm(formId, null); }

//=================表单======================//
//将对象显示到表单上
function showObjectOnForm(formId, obj) {
    //文本框、下拉框、文本域
    var inputs = $("#" + formId + " input,textarea").not(function (i) {
        var selector = "#" + formId + " input[type=checkbox][name=" + $(this).attr("name") + "]";
        if ($(selector).length > 0) {
            return $(this);
        }
    });
    inputs.each(function (index) {
        var value = "";
        var current = $(this);
        var isCombox = current.hasClass("ui-combobox-input");
        if (obj != null && obj != undefined) {
            var tempValue;
            if (isCombox) {
                tempValue = obj[current.prev().attr("name")]
            } else {
                tempValue = obj[current.attr("name")];
            }
            if (tempValue != undefined) {
                value = tempValue;
            }
        }
        if (isCombox && value != "") {
            current.prev().combobox("setValue", value);
        } else {
            current.val(value);
        }
    });
    //复选框
    var checkboxs = $("#" + formId + " input[type=checkbox]");
    checkboxs.each(function (index) {
        var checked = false;
        var current = $(this);
        if (obj != null) {
            tempChecked = obj[current.attr("name")];
            if (tempChecked != undefined) {
                if (typeof (tempChecked) == "string") {
                    checked = tempChecked == "true";
                } else if (typeof (tempChecked) == "boolean") {
                    checked = tempChecked;
                } else {
                    throw current.attr("name") + "的值" + tempChecked + "不是有效的booleen类型";
                }
            }
        }
        if (checked) {
            current.attr("checked", "checked");
        } else {
            current.removeAttr("checked");
        }
    });


    //
    //SELECT
    var select = $("#" + formId + " select");
    select.each(function (index) {
        var current = $(this);
        if (obj != null) {
            tempChecked = obj[current.attr("name")];
            current.val(tempChecked);
        }
    });


}

//StringBuilder
function StringBuilder() {
    this.tmp = new Array();
}
StringBuilder.prototype.Append = function (value) {
    this.tmp.push(value);
    return this;
}
StringBuilder.prototype.RemoveLast = function () {
    this.tmp.pop();
    return this;
}
StringBuilder.prototype.Length = function () {
    return this.tmp.length;
}
StringBuilder.prototype.AppendFormat = function () {
    var nLength = arguments.length;
    var strResult = '';
    if (nLength > 0) {
        strResult = arguments[0];
        for (var i = 1; i < nLength; i++) {
            strResult = strResult.replace('{' + (i - 1) + '}', arguments[i]);
        }
    }
    this.tmp.push(strResult);
    return this;
}
StringBuilder.prototype.Clear = function () {
    this.tmp.length = 0;
}
StringBuilder.prototype.toString = function () {
    return this.tmp.join('');
}
String.prototype.trim = function () {
    return this.replace(/(^\s*)|(\s*$)/g, "");
}
String.prototype.ltrim = function () { return this.replace(/(^\s*)/g, ""); }
String.prototype.rtrim = function () { return this.replace(/(\s*$)/g, ""); }
//格式化字符串
function Format() {
    var nLength = arguments.length;
    if (nLength == 0)
        return '';
    var strResult = arguments[0];
    for (var i = 1; i < nLength; i++) {
        strResult = strResult.replace('{' + (i - 1) + '}', arguments[i]);
    }
    return strResult;
}


function getTimeStr() {
    var myDate = new Date();
    var year = myDate.getYear();      //获取当前年份(2位) 
    var day = myDate.getDate();      //获取当前日(1-31)
    var week = myDate.getDay();        //获取当前星期X(0-6,0代表星期天)
    var time = myDate.getTime();      //获取当前时间(从1970.1.1开始的毫秒数)
    var hour = myDate.getHours();      //获取当前小时数(0-23)
    var minute = myDate.getMinutes();    //获取当前分钟数(0-59)
    var sec = myDate.getSeconds();    //获取当前秒数(0-59)
    var dateStr = day.toString() + hour.toString() + minute.toString() + sec.toString();
    return dateStr;
}

//全屏显示模态对话框
function openScreenDialog(m_url) {
    var w = screen.width;
    var h = screen.availHeight;
    var options = 'modal=yes;dialogWidth=' + w + 'px;dialogHeight=' + h + 'px;toolbar=no;menubar=no;scrollbars=both;location=no;status=no;'
    return window.showModalDialog(m_url + "&randomitem=" + getTimeStr(), window, options);
}
 