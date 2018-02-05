


var DHQR = {};
var __commonMessages = {
    systemError: "出现了系统错误，请与系统管理员联系。",
    message: "消息",
    prompt: "提示",
    operationTip: "操作提示",
    confirm: "确定",
    loadingText: "正在发送请求，请稍后...",
    cancel: "取消",
    edit: "编辑",
    del: "删除",
    loadingflow: "正在启动工作流，请稍后...",
    peleaseSelectRecord: "请选择要操作的记录。",
    flowStatus: "流程处理状态",
    startFlow: "启动业务流程"
};
//快速查询
DHQR.QuickSearch = function (data, value, grid) {
    var returnData = [];
    var showColName = [];
    $.each(grid.jqGrid("getGridParam", "colModel"), function (key, val) {
        if (val.hidden) {
            return true;
        }
        showColName.push(val.name);
    });
    $.each(data, function (key, val) {
        for (var per in val) {
            if (!val[per] || typeof (val[per]) == "boolean" || $.inArray(per, showColName) == -1) {
                continue;
            }
            var thisVal = val[per].toString().toLocaleLowerCase();
            if (thisVal.indexOf(value) > -1) {
                returnData.push(val);
                break;
            }
        }
    });
    return returnData;
}

/**
* 表单验证提示插件初始化设置
*/
var DHQR_validateTipsSetting = {
    pnotify_title: "",              //提示框的标题  
    pnotify_text: "",               //提示框的内容  
    pnotify_history: false,          //显示一个可以可拉的菜单以回显历史提示  
    pnotify_type: "error",          //显示一个可以可拉的菜单以回显历史提示   
    pnotify_error_icon: "ui-icon ui-icon-alert",    //'error'提示的图标class  
    pnotify_width: "150px",         //提示框的宽度  
    pnotify_delay: 8000,            //显示时间（毫秒）  
    pnotify_stack: false,
    pnotify_closer: true
};

/**
* 表单验证初始化设置
* @param {Object} input
*/
var DHQR_validatorSetting = {
    highlight: function (input) {
        $(input).addClass("ui-state-highlight");
    },
    unhighlight: function (input) {
        $(input).removeClass("ui-state-highlight");
    },
    errorPlacement: function (error, element) {
        var $validateTips;
        if (!$("#pnotify_" + element.attr("id")).html()) {
            $validateTips = $.pnotify(DHQR_validateTipsSetting);
            $validateTips.attr("id", "pnotify_" + element.attr("id"));
        } else {
            $validateTips = $("#pnotify_" + element.attr("id"));
        }
        var $label = $validateTips.find(".ui-pnotify-text");
        $label.html(error);
        if ($validateTips.not(":visible")) {
            $validateTips.css({ 'top': element.offset().top - $(document).scrollTop()+3, 'left': element.offset().left + element.width() + 15 });
            $validateTips.appendTo(element.parent().parent());
            $validateTips.show();
        }
    },
    success: function (label) {
        label.parents(".ui-pnotify").remove();
    }
};
/**
* 设置表单验证默认属性
*/
DHQR.formValid = function (formId) {
    $.validator.setDefaults(DHQR_validatorSetting);
    $("#" + formId).validate();
};

/**
* 添加必输标识
* @param {Object} $input
*/
DHQR.addRequiredMark = function ($input) {
    var $thisInput = $(".required");
    if ($input) {
        $thisInput = $input;
    }
    $thisInput.parent("div").prev('label').find(".requiredLabel").remove();
    $thisInput.parent("div").prev('label').append("<span class='requiredLabel' style='color:#f00'>*</span>");
};
/**
* 移除必输标识
* @param {Object} $input
*/
DHQR.removeRequiredMark = function ($input) {
    var $thisInput = $(".required");
    if ($input) {
        $thisInput = $input;
    }
    $thisInput.prev("label").find(".requiredLabel").remove();
};
/**
*
* 为必须输入项标签添加标识
*/
$(function () {
    DHQR.removeRequiredMark();
    DHQR.addRequiredMark();
});

DHQR.MsgAlert = function (title, msg, iconType) {
    $.pnotify({
        pnotify_title: title,
        pnotify_text: msg,
        pnotify_history: false,
        pnotify_shadow: true,
        pnotify_animation: 'show',
        pnotify_delay: 5000,
        pnotify_type: iconType
    });
};
DHQR.ProcessStatus = function (status) {
    var re = false;
    if (status == null) {
        DHQR.MsgAlert(__commonMessages.operationTip, __commonMessages.systemError, 'error');
        re = false;
    }
    else if (!status.IsSuccessful) {
        var message = status.OperateMsg == undefined ? status : status.OperateMsg;
        DHQR.MsgAlert(__commonMessages.operationTip, message, 'error');
        re = false;
    }
    else {
        DHQR.MsgAlert(__commonMessages.operationTip, status.OperateMsg, 'info');
        re = true;
    }
    return re;
};
//需要用户操作的消息框,消息内容、确定时执行的函数名、标题、[此时，IsModal为false] ShowDialog：以模式窗口形式打开子窗体并返回而不等待该子窗体关闭
DHQR.confirmDialog= function (message, callback, title, isModal, cancelCallback) {
    if (title == undefined) {
        title = __commonMessages.prompt;
        isModal = true;
    }
    message = "<div class='promptInfo'><span class='icon-span ui-icon ui-icon-alert' style='display:inline;float:left;'></span><span class='icon-span span'>" + message + "<span></div>";
    var myDialog = DHQR.createDialog(message, title, isModal);
    var buttons = {};
    buttons[__commonMessages.confirm] = function () {
        $(this).dialog("close");
        if ($.isFunction(callback)) {
            callback.apply();
        }
    };
    buttons[__commonMessages.cancel] = function () {
        $(this).dialog("close");
        if ($.isFunction(cancelCallback)) {
            cancelCallback.apply();
        }
    };
    myDialog.dialog("option", "buttons", buttons);
    myDialog.dialog('open');
}
//生成一个最基本的Dialog,消息内容、标题、[此时，IsModal为false] ShowDialog：以模式窗口形式打开子窗体并返回而不等待该子窗体关闭
DHQR.createDialog= function(message, title, isModal) {
    var myDialog = $('<div class="div-dialog" id="div-confirmDialog"></div>').html(message).dialog({
        autoOpen: false,
        title: title,
        modal: isModal
    });
    return myDialog;
}
