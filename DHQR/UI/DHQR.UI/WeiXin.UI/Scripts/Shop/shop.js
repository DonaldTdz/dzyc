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
    peleaseSelectRecord: "请选择要操作的记录。",
    flowStatus: "流程处理状态",
    startFlow: "启动业务流程"
};

//需要用户操作的消息框,消息内容、确定时执行的函数名、标题、[此时，IsModal为false] ShowDialog：以模式窗口形式打开子窗体并返回而不等待该子窗体关闭
function confirmDialog(message, callback, title, isModal, cancelCallback) {
    if (title == undefined) {
        title = __commonMessages.prompt;
        isModal = true;
    }
    message = "<div class='promptInfo'><span class='icon-span ui-icon ui-icon-alert' style='display:inline;float:left;'></span><span class='icon-span span'>" + message + "<span></div>";
    var myDialog = createDialog(message, title, isModal);
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
function createDialog(message, title, isModal) {
    var myDialog = $('<div class="div-dialog" id="div-confirmDialog"></div>').html(message).dialog({
        autoOpen: false,
        title: title,
        resizable: false,        
        modal: isModal
    });
    return myDialog;
}

function ConfirmDialog(title, msg, r) {
    var result = false;
    if ($('#__question').lenght == 1)
        $('#__question').remove().remove();
    var msgText =msg;
    var __question = $('<div id="__question" style="display:none; cursor: default;">' + msgText + '<br/><br/><div style="text-align:center;"><input type="button" id="__yes" value=" 确 定 " /> <input type="button" id="__no" value=" 取 消 " /></div></div>');
    $.blockUI({
        message: __question,
        css: { width: '275px' },
        title: title,
        overlayCSS: { backgroundColor: '#cccccc', opacity: 0.4 },
        showOverlay: true,
        theme: true,
        fadeIn: 3000
    });
    $('#__yes').click(function () {
        $.unblockUI();
        result = true;
    });
    $('#__no').click(function () {
        $.unblockUI();
        result = false;
    });
    $('#__yes').bind("click", function (event) {
        r(result);
    });
    $('#__no').bind("click", function (event) {
        r(result);
    });
}

function MsgAlert(title, message) {
        if (title == undefined) {
            title = __commonMessages.prompt;
            isModal = true;
        }
        message = "<div class='promptInfo'><span class='icon-span ui-icon ui-icon-alert' style='display:inline;float:left;'></span><span class='icon-span span'>" + message + "<span></div>";
        var myDialog = createDialog(message, title, true);
        var buttons = {};
        buttons[__commonMessages.confirm] = function () {
            $(this).dialog("close");
            
        };
        myDialog.dialog("option", "buttons", buttons);
        myDialog.dialog('open');
}