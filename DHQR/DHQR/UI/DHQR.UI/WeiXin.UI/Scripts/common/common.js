String.prototype.len = function() {
    return this.replace(/[^\x00-\xff]/g, "rr").length;
}

//取字符串
function CountCharacters(str, size) {
    var totalCount = 0;
    var newStr = "";
    for (var i = 0; i < str.length; i++) {
        var c = str.charCodeAt(i);
        if ((c >= 0x0001 && c <= 0x007e) || (0xff60 <= c && c <= 0xff9f)) {
            totalCount++;
        } else {
            totalCount += 2;
        }
        if (totalCount <= size) {
            newStr = str.substring(0, i + 1);
        } else {
            return newStr;
        }
    }
    return newStr;
}


function GetUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}


 

//通用方法 - 根据Value值，返回枚举Name
function ShowEnumStatusFlag(enumObj, val) {
    var obj;
    try {
        var obj = eval(enumObj);
    } catch (e) {
        obj = eval("(" + enumObj + ")")
    }

    for (var i = 0; i < obj.Value.length; i++) {
        if (val == obj.Value[i].name) {
            return obj.Name[i].name;
        }
    }
    return "无效参数";
}


function ShowEntityEnumFlagName(obj, val) {
    for (var i = 0; i < obj.Value.length; i++) {
        if (val == parseInt(obj.Value[i].name)) {
            if (obj.CName != undefined && obj.CName[i] != undefined)
                return obj.CName[i].name;
            else
                return obj.Name[i].name;
        }
    }
    return "--";
}

//设置不同子域为共同domain，解决跨域访问问题
function setDomain() {

    var domain = window.location.hostname;

    var s = domain.split(".");
    
    if (s.length > 3) {
        var index = domain.indexOf(".") + 1;
        domain = domain.substring(index, domain.length);
    }
    //document.domain = domain;
}

///interval ：D表示查询精确到天数的之差
///interval ：H表示查询精确到小时之差
///interval ：M表示查询精确到分钟之差
///interval ：S表示查询精确到秒之差
///interval ：T表示查询精确到毫秒之差  alert(dateDiff('D', '2007-4-1', '2007/04/19'))；
function dateDiff(interval, date1, date2) {
    var objInterval = { 'D': 1000 * 60 * 60 * 24, 'H': 1000 * 60 * 60, 'M': 1000 * 60, 'S': 1000, 'T': 1 };
    interval = interval.toUpperCase();
    var dt1 = new Date(Date.parse(date1.replace(/-/g, '/')));
    var dt2 = new Date(Date.parse(date2.replace(/-/g, '/')));
    try {
        var i = Math.round((dt2.getTime() - dt1.getTime()) / eval('objInterval.' + interval));
        return i == -1 ? 0 : i;
    }
    catch (e) {
        return e.message;
    }
}

//当指定name的复选框选中时，激活相应的按钮
//arguments[0]为指定form，arguments[1]为复选框的name，arguments[2]～arguments[arguments.length - 1]为要激活的按钮
function checkedEnabledButton() {
    for (var i = 0; i < arguments[0].elements.length; i++) {
        var e = arguments[0].elements[i];
        if (e.name == arguments[1] && e.checked) {
            for (var j = 2; j < arguments.length; j++) {
                document.getElementById(arguments[j]).disabled = false;
            }
            return;
        }
    }
    for (var j = 2; j < arguments.length; j++) {
        document.getElementById(arguments[j]).disabled = true;
    }
}

function CheckAllByName(form, tname, checked) {
    for (var i = 0; i < form.elements.length; i++) {
        var e = form.elements[i];
        if (e.name == tname) {
            e.checked = checked;
        }
    }
}
function CheckEnableAllByName(form, tname, checked) {
    for (var i = 0; i < form.elements.length; i++) {
        var e = form.elements[i];
        if (e.name == tname && !e.disabled) {
            e.checked = checked;
        }
    }
}
function getCheckBoxValue(cbName) {
    var objCheckBox = document.getElementsByName(cbName);
    if (objCheckBox == undefined && objCheckBox == null) return "";
    var value = "";
    for (var i = 0; i < objCheckBox.length; i++) {
        if (objCheckBox[i].checked) value = value + objCheckBox[i].value + ",";
    }
    value = value.Trim(',');
    return value;
}