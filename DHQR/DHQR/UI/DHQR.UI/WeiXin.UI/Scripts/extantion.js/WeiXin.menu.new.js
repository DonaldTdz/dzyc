(function ($) {
    $.menu = $.menu || {};
    $.extend($.menu, {
        init: function (option) {

        }
    });
    $.fn.menuInit = function (options) {
        var obj = {
            selected: 0,
            width: 200,
            height: "auto",
            title: "菜单",
            items: []
        };
        $.extend(obj, options);
        if (this.length == 0) {
            return;
        }
        this.css({ width: obj.width, maring: "0px", padding: "0px" });
        this[0].menuObj = obj;
        //var divTitle = $("<div style='font-family:\"Open Sans\", sans-serif;line-height:35px; width:200px;text-align:center;color:#eee;font-size:16px;background:#666;letter-spacing:20px;font-weight:bolder;'>" + obj.title + "</div>");
        //divTitle.insertBefore(this);

        var createItem = function (items, level) {
            var currentElemnet = this;
            var menuUl = $("<ul></ul>");
            menuUl.appendTo(currentElemnet);
            //shaosc add
            if (level == 3) {
                var level2Menu = currentElemnet.first("div");
                level2Menu.addClass("menu_3_parent").removeClass("menu_2");
            }
            //shaosc add end
            if (level != 1) {
                menuUl.css("display", "none");
            } else {
                menuUl.css("display", "block");
            }
            $.each(items, function (key, val) {
                var li = $("<li class='menu_li'></li>");
                li.appendTo(menuUl);
                var div = $("<div level=" + level + " class='menu menu_" + level + "'></div>");
                //shaosc add
                if (key == 0 && level == 3) {
                    div.addClass("menu_3_first");
                }
                //shaosc add end
                var spanImg = $("<span class='" + val.icons + "'></span>");
                var link = $("<a class='menu_link'></a>");
                if (val.click) {
                    link.click(val.click);
                }
                var titleLabel = $("<label>" + val.title + "</label>");
                div.click(function () {
                    var currentDiv = $(this);
                    if (val.items && val.items.length > 0) {
                        var sumMenu = currentDiv.next("ul");
                        var currentLevel = currentDiv.attr("level");
                        var display = sumMenu.css("display");
                        var allDiv = $(".menu_" + currentLevel).next("ul").slideUp();
                        $.each(allDiv, function (key, val) {
                            $("ul", val).slideUp();
                        });
                        //$("" + 2).next("ul").slideUp();
                        if (display == "none") {
                            sumMenu.slideDown();
                        } else {
                            sumMenu.slideUp();
                        }
                    } else {

                        $(".selected_1").addClass("menu_link").removeClass("selected_1");
                        $(".selected_2").addClass("menu_link").removeClass("selected_2");
                        $(".selected_3").addClass("menu_link").removeClass("selected_3");
                        $(".selected_4").addClass("menu_link").removeClass("selected_4");
                        $(".selected_5").addClass("menu_link").removeClass("selected_5");
                        currentDiv.children('a').addClass("selected_" + currentDiv.attr("level")).removeClass("menu_link");
                    }
                });
                spanImg.appendTo(link);
                titleLabel.appendTo(link);
                link.appendTo(div);
                div.appendTo(li);
                if (val.items && val.items.length > 0) {
                    var nextLevel = level + 1;
                    createItem.call(li, val.items, nextLevel);
                }
            });
        }
        createItem.call(this, obj.items, 1);
    }
})(jQuery)