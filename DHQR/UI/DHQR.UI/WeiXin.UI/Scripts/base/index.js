var SystemManager = "";
//设置登录窗口
function openPwd() {
    $('#w').window({
        title: '修改密码',
        width: 300,
        modal: true,
        shadow: true,
        closed: true,
        height: 160,
        resizable: false
    });
}
//关闭登录窗口
function closePwd() {
    $('#w').window('close');
}

//设置登录窗口
function openWin(title, width, height, zIndex, modal) {
    $('#win').window({
        title: title,
        width: width,
        modal: modal,
        zIndex: zIndex,
        shadow: true,
        closed: true,
        height: height,
        resizable: false
    });
}
//关闭登录窗口
function closeWin() {
    $('#win').window('close');
}

//设置登录窗口
function openWin1(title, width, height, zIndex, modal) {
    $('#win1').window({
        title: title,
        width: width,
        modal: modal,
        zIndex: zIndex,
        shadow: false,
        closed: true,
        height: height,
        resizable: false
    });
}
//关闭登录窗口
function closeWin1() {
    $('#win1').window('close');
}

//修改密码
function serverLogin() {
    var $newpass = $('#txtNewPass');
    var $rePass = $('#txtRePass');

    if ($newpass.val() == '') {
        msgShow('系统提示', '请输入密码！', 'warning');
        return false;
    }
    if ($rePass.val() == '') {
        msgShow('系统提示', '请在一次输入密码！', 'warning');
        return false;
    }

    if ($newpass.val() != $rePass.val()) {
        msgShow('系统提示', '两次密码不一至！请重新输入', 'warning');
        return false;
    }
    $.getJSON(SystemManager + "/AjaxServerPage/AdminUserService.ashx?Name=ChangePassword&PWD=" + $newpass.val() + "&format=json&jsoncallback=?",
        function(data) {
            try {
                if (data.result == 1) {
                    msgShow('系统提示', '恭喜，密码修改成功！<br>您的新密码为：' + $newpass.val(), 'info');
                    $newpass.val('');
                    $rePass.val('');
                    close();
                }
                else {
                    msgShow('系统提示', '修改失败', 'info');
                    // $("#" + spnID).html($("#" + spnID).html() + "   删除失败");
                }
            }
            catch (e) {

            }
        }); 


}

function getWindowHeight() {
    return $(window).height();
}
function getWindowWidth() {
    return $(window).width();
}
function windowResize() {
    var width = getWindowWidth();
    var height = getWindowHeight();
    $('form#form1').width(width);
    $('form#form1').height(height);
    $('form#form1').layout();
}

$(function() {

    openPwd();

    $('#editpass').click(function() {
        $('#w').window('open');
    });

    $('#btnShow2').click(function() {
        openWin1("调试窗口", "400", '200', '9000', false);

        var offset = $("#win").offset();

        $("#win1").parent().css("left", offset.left + 400)
        $('#win1').window('open');
    });

    $('#btnEp').click(function() {
        serverLogin();
    })

    $('#btnCancel').click(function() { closePwd(); })

    $('#loginOut').click(function() {
        $.messager.confirm('系统提示', '您确定要退出本次登录吗?', function(r) {

        if (r) {
                location.href = '/logout.aspx?r='+Date();
            }
        });

    })

    windowResize();
    $(window).resize(function() {
        windowResize();
    });
    setTimeout(function() {
        $('form#form1').layout('collapse', 'east');
    }, 0);
});


function Confirm(msg, control) {
    $.messager.confirm('系统提示', msg, function(r) {
        if (r) {

            eval(control.toString().slice(11)); //截掉 javascript: 并执行  

        }

    });
    return false;
} 




		
