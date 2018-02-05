//栋梁提供异步请求
function Service(url, parameters, success, error, sType, sDataType, sAsync, sCache) {
    /// <summary>
    /// Ajax
    /// </summary>
    /// <param name="url">请求页面地址</param>
    /// <param name="parameters">参数集合{，}</param>
    /// <param name="success">返回请求成功信息</param>
    /// <param name="error">返回请求失败信息</param>
    /// <param name="sType">提交类型：POST、GET 默认POST</param>
    /// <param name="sDataType">返回数据类型 html、json 默认html</param>
    /// <param name="sAsync">数据同步或异步  true、false 默认false</param>
    /// <param name="sCache">是否缓存 true、false 默认false</param>

    $.ajax(
    {
        type: sType == "" || sType == undefined ? "POST" : sType,
        url: url || url == "" || "",
        data: parameters == "" || parameters == undefined ? "{}" : parameters,
        cache: sCache == "" || sCache == undefined ? false : sCache,
        dataType: sDataType == "" || sDataType == undefined ? "html" : sDataType,
        success: success,
        async: sAsync == "" || sAsync == undefined ? true : sAsync,
        error: error
    });
}
/////////
var _menus = null;
$(function () {
    $('#loading-mask').fadeOut('slow');
    //    InitLeftMenu();
    tabClose();
    tabCloseEven();

    $('#tabs').tabs({
        onSelect: function (title) {
            var currTab = $('#tabs').tabs('getTab', title);
            var iframe = $(currTab.panel('options').content);

            var src = iframe.attr('src');
            //            if (src)
            //                $('#tabs').tabs('update', { tab: currTab, options: { content: createFrame(src)} });

        }
    });

    /*
    var date = new Date();
    $.getJSON("Config/admin.aspx?t=" + date, function(data) {
    SetMenuData(data);
    });
    */

    //异步请求动态加载分类菜单
    var date = new Date();
    Service("/AjaxServerPage/GetMenuService.ashx?Method=BindMenu&t=" + date, null,
           function (model) {
               if (model != undefined) {
                   SetMenuData(model);
               }
           },
          function (data) {
              alert(data.responseText);
          }, "GET", "json");
})

var _menuName = "";
function SetMenuData(obj) {
    _menus = obj;
    $('#css3menu a').click(function () {
        $('#css3menu a').removeClass('active');
        $(this).addClass('active');

        //如果是当前选中就不在重新加载
        if (_menuName != $(this).attr('name')) {
            _menuName = $(this).attr('name');

            var d = _menus[$(this).attr('name')];
            Clearnav();
            addNav(d);
            InitLeftMenu();
        }
    });

    //导航菜单绑定初始化
    $("#wnav").accordion({
        animate: false
    });

    var firstMenuName = $('#css3menu a:first').attr('name');
    _menuName = firstMenuName;
    addNav(_menus[firstMenuName]);
    InitLeftMenu();
}

//初始化左侧
function Clearnav() {
    var pp = $('#wnav').accordion('panels');

    //修正移除操作
    var titleArr = new Array();
    $.each(pp, function (i, n) {
        if (n) {
            var t = n.panel('options').title;
            titleArr[i] = t;
            //$('#wnav').accordion('remove', t);
        }
    });

    $.each(titleArr, function (i, n) {
        if (n) {
            $('#wnav').accordion('remove', n);
        }
    });

    pp = $('#wnav').accordion('getSelected');
    if (pp) {
        var title = pp.panel('options').title;
        $('#wnav').accordion('remove', title);
    }
}

//修改简化HTML代码
function addNav(data) {
    if (!data) return;
    $.each(data, function (i, sm) {
        var menulist = "";
        menulist += '<ul>';
        $.each(sm.menus, function (j, o) {
            menulist += '<li><div><a href="#" rel="'
					+ o.url + '" >&nbsp;<span class="nav">' + o.menuname
					+ '</span></a></div></li> ';
        });
        menulist += '</ul>';

        $('#wnav').accordion('add', {
            title: sm.menuname,
            content: menulist
        });
    });

    var pp = $('#wnav').accordion('panels');
    var t = pp[0].panel('options').title;
    $('#wnav').accordion('select', t);

}

//function addNav(data) {
//    $.each(data, function(i, sm) {
//        var menulist = "";
//        menulist += '<ul>';
//        $.each(sm.menus, function(j, o) {
//            menulist += '<li><div><a ref="' + o.menuid + '" href="#" rel="'
//					+ o.url + '" rights="' + o.rights + '"><span>&nbsp;</span><span class="nav">' + o.menuname
//					+ '</span></a></div></li> ';
//        });
//        menulist += '</ul>';
//        $('#wnav').accordion('add', {
//            title: sm.menuname,
//            content: menulist
//        });
//    });
//    var pp = $('#wnav').accordion('panels');
//    var t = pp[0].panel('options').title;
//    $('#wnav').accordion('select', t);
//}

// 初始化左侧
function InitLeftMenu() {

    hoverMenuItem();

    $('#wnav li a').live('click', function () {
        var tabTitle = $(this).children('.nav').text();

        var url = $(this).attr("rel");
        var menuid = $(this).attr("ref");

        addTab(tabTitle, url);
        $('#wnav li div').removeClass("selected");
        $(this).parent().addClass("selected");
    });

}

/**
* 菜单项鼠标Hover
*/
function hoverMenuItem() {
    $(".easyui-accordion").find('a').hover(function () {
        $(this).parent().addClass("hover");
    }, function () {
        $(this).parent().removeClass("hover");
    });
}
//获取左侧导航的图标
function getIcon(menuid) {
    var icon = 'icon ';
    $.each(_menus.menus, function (i, n) {
        $.each(n.menus, function (j, o) {
            if (o.menuid == menuid) {
                icon += o.icon;
            }
        })
    })

    return icon;
}

//////////////////////////////////////////////////////////////////////////
function addTab(subtitle, url) {
    if (!$('#tabs').tabs('exists', subtitle)) {
        $('#tabs').tabs('add', {
            title: subtitle,
            content: createFrame(url),
            closable: true
        });
    } else {
        $('#tabs').tabs('select', subtitle);
        var currTab = $('#tabs').tabs('getSelected');
        $('#tabs').tabs('update', {
            tab: currTab,
            options: {
                content: createFrame(url)
            }
        })
        $('#mm-tabupdate').click();
    }
    tabClose();
}

function createFrame(url) {
    var s = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="width:100%;height:100%;"></iframe>';
    return s;
}

function tabClose() {
    /*双击关闭TAB选项卡*/
    $(".tabs-inner").dblclick(function () {
        var subtitle = $(this).children(".tabs-closable").text();
        $('#tabs').tabs('close', subtitle);
    })
    /*为选项卡绑定右键*/
    $(".tabs-inner").bind('contextmenu', function (e) {
        $('#mm').menu('show', {
            left: e.pageX,
            top: e.pageY
        });

        var subtitle = $(this).children(".tabs-closable").text();

        $('#mm').data("currtab", subtitle);
        $('#tabs').tabs('select', subtitle);
        return false;
    });
}
//绑定右键菜单事件
function tabCloseEven() {
    //刷新
    $('#mm-tabupdate').click(function () {
        var currTab = $('#tabs').tabs('getSelected');
        var url = $(currTab.panel('options').content).attr('src');
        $('#tabs').tabs('update', {
            tab: currTab,
            options: {
                content: createFrame(url)
            }
        })
    })
    //关闭当前
    $('#mm-tabclose').click(function () {
        var currtab_title = $('#mm').data("currtab");
        $('#tabs').tabs('close', currtab_title);
    })
    //全部关闭
    $('#mm-tabcloseall').click(function () {
        $('.tabs-inner span').each(function (i, n) {
            var t = $(n).text();
            $('#tabs').tabs('close', t);
        });
    });
    //关闭除当前之外的TAB
    $('#mm-tabcloseother').click(function () {
        $('#mm-tabcloseright').click();
        $('#mm-tabcloseleft').click();
    });
    //关闭当前右侧的TAB
    $('#mm-tabcloseright').click(function () {
        var nextall = $('.tabs-selected').nextAll();
        if (nextall.length == 0) {
            return false;
        }
        nextall.each(function (i, n) {
            var t = $('a:eq(0) span', $(n)).text();
            $('#tabs').tabs('close', t);
        });
        return false;
    });
    //关闭当前左侧的TAB
    $('#mm-tabcloseleft').click(function () {
        var prevall = $('.tabs-selected').prevAll();
        if (prevall.length <= 1) {
            return false;
        }
        prevall.each(function (i, n) {
            var t = $('a:eq(0) span', $(n)).text();
            $('#tabs').tabs('close', t);
        });
        return false;
    });

    //退出
    $("#mm-exit").click(function () {
        $('#mm').menu('hide');
    })
}

//弹出信息窗口 title:标题 msgString:提示信息 msgType:信息类型 [error,info,question,warning]
function msgShow(title, msgString, msgType) {
    $.messager.alert(title, msgString, msgType);
}
