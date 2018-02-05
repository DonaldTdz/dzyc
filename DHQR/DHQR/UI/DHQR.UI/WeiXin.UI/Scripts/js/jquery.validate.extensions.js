//设置默认值
$.validator.setDefaults({
    errorPlacement: function (error, element) {
        error.appendTo(element.parent().parent());
    }
});
//代码
$.validator.addMethod("code", function (value, element) {
    return this.optional(element) || /^([a-z0-9A-Z-])+$/i.test(value);
},"代码只能是数字、字母或-");
$.validator.addClassRules("code", {
    required: true,
    code: true
});
//字母
$.validator.addMethod("letter", function (value, element) {
    return this.optional(element) || /^([a-zA-Z])+$/i.test(value);
}, "请输入字母");
$.validator.addClassRules("letter", {
    letter: true
});
//字母和数字
$.validator.addMethod("letterAndNumber", function (value, element) {
    return this.optional(element) || /^([a-z0-9A-Z])+$/i.test(value);
}, "请输入字母或数字");
$.validator.addClassRules("letterAndNumber", {
    letterAndNumber: true
});
//带点的字母和数字
$.validator.addMethod("letterAndNumberWithPoint", function (value, element) {
    return this.optional(element) || /^([a-z0-9A-Z\.])+$/i.test(value);
}, "请输入字母、数字或.");
$.validator.addClassRules("letterAndNumberWithPoint", {
    letterAndNumberWithPoint: true
});
//不能包含空格
$.validator.addMethod("noWhitespace", function (value, element) {
    return this.optional(element) || /^\S+$/i.test(value);
}, "不能输入空格");
$.validator.addClassRules("noWhitespace", {
    noWhitespace: true
});
//移动电话
$.validator.addMethod("mobile", function (value, element) {
    return this.optional(element) || /^((\+86)|(86))?(1)\d{10}$/i.test(value);
}, "请输入有效的手机号码");
$.validator.addClassRules("mobile", {
    mobile: true
});
//验证固定电话   
$.validator.addMethod("telephone", function (value, element) {
    var pattern = /^(([0\+]\d{2,3}-)?(0\d{2,3})-)(\d{7,8})(-(\d{3,}))?$/;
    if (value != '') { if (!pattern.exec(value)) { return false; } };
    return true;
}, " 请输入有效的固定电话(例如：0000-0000000)");   
$.validator.addClassRules("telephone", {
    telephone: true
});
//邮政编码
$.validator.addMethod("postcode", function (value, element) {
    return this.optional(element) || /\d{6}/i.test(value);
}, "请输入有效的邮政编码");
$.validator.addClassRules("postcode", {
    postcode: true
});
//身份证号
$.validator.addMethod("idCard", function (value, element) {
    return this.optional(element) || /^\d{15}$|\d{17}[\dXx]$/i.test(value);
}, "请输入有效的身份证号码");
$.validator.addClassRules("idCard", {
    idCard: true
});
//一维或两位小数20位以内正整数
$.validator.addMethod("TwoPointNumber", function (value, element) {
    return this.optional(element) || /^\d{1,20}($|\.\d{1,2})$/i.test(value);
}, "请输入俩位小数以内的正数");
$.validator.addClassRules("TwoPointNumber", {
    TwoPointNumber: true
});
//正整数
$.validator.addMethod("NoPointNumber", function (value, element) {
    return this.optional(element) || /^\d*$/i.test(value);
}, "请输入正整数");
$.validator.addClassRules("NoPointNumber", {
    NoPointNumber: true
});
//截至时间
$.validator.addMethod("endTime", function (value, element) {
    var endTimeId = $(element).attr("id");
    var beginTimeId = endTimeId.replace("EndTime", "BeginTime");
    var beginTimeElement = $("#" + beginTimeId);
    if (beginTimeElement.length == 0) {
        beginTimeId = endTimeId.replace("EndTime", "StartTime");
        beginTimeElement = $("#" + beginTimeId);
    }
    if (beginTimeElement.length == 0) {
        throw "开始时间的Id中必须包括BeginTime或StartTime，并且和截至时间（EndTime）匹配。";
    }
    var beginTimeValue = beginTimeElement.val();
    return Date.parse(beginTimeValue.replace(/\-/g, "/")) <= Date.parse(value.replace(/\-/g, "/"));
}, "截至时间不能小于开始时间");
$.validator.addClassRules("endTime", {
    endTime: true
});
//登录名规则
$.validator.addMethod("logonName", function (value, element) {
    return this.optional(element) || /^([a-z0-9A-Z.-_@])+$/i.test(value);
}, "请输入有效的登录名，如：字母、数字、@、点（.）、中划线(-)、下划线（_）。");
$.validator.addClassRules("logonName", {
    logonName: true
});            
