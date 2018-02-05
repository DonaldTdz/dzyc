var KTree = [];
(function (kTree) {
    // 判断Jquery是否引入
    if (typeof window.jQuery == "undefined") {
        alert('请引入jquery库');
    };
    kTree.Common = {
        Post: function (url, data, success, error, alway) {
            $.ajax({
                type: "post",
                url: url,
                data: data,
                dataType: "json",
                cache: false,
                success: function (result) {
                    if (typeof success != 'undefined') {
                        success(result);
                    }
                },
                error: function (result) {
                    if (typeof error != 'undefined') {
                        error(result);
                    }
                }
            }).always(function () {
                if (typeof alway != 'undefined') {
                    alway();
                }
            });
        },
        Get: function (url, data, success, error) {
            $.ajax({
                type: "get",
                url: url,
                data: data,
                dataType: "json",
                cache: false,
                success: function (result) {
                    if (typeof success != 'undefined') {
                        success(result);
                    }
                },
                error: function (result) {
                    if (typeof error != 'undefined') {
                        error(result);
                    }
                }
            });
        },
        ToDate: function (val) {
            //将字符串转换为日期 
            var date;
            if (val == null || val == "/Date(-62135596800000)/") {
                return null;
            }
            if (val.indexOf("Date(") > 0) {
                var str = val.substr(val.indexOf("(") + 1);
                str = str.substr(0, str.indexOf(")"));
                date = new Date(Number(str));
            } else {
                date = new Date(Date.parse(val.replace(/-/g, "/")));
            }
            return date.pattern("yyyy-MM-dd hh:mm:ss");
        },
        ToDateHMS: function (val) {
            //将字符串转换为日期 
            var date;
            if (val == null || val == "/Date(-62135596800000)/") {
                return null;
            }
            if (val.indexOf("Date(") > 0) {
                var str = val.substr(val.indexOf("(") + 1);
                str = str.substr(0, str.indexOf(")"));
                date = new Date(Number(str));
            } else {
                date = new Date(Date.parse(val.replace(/-/g, "/")));
            }
            return date.pattern("yyyy-MM-dd");
        },
        YdateTime: function (time) {   //验证日期是否有效
            var reg_hhmmss = /^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))(\s(([01]\d{1})|(2[0123])):([0-5]\d):([0-5]\d))?$/;
            if (!reg_hhmmss.test(time)) {
                return false;
            } else {
                return true;
            }
        },
        //controlId  控件Id  enumType 枚举类型  value值绑定id
        DropDownData: function (controlId, enumType) {
            controlId.empty();
            var data = { enumType: enumType };
            KTree.Common.Post("/BaseEnum/GetAllBaseEnmByHfsType", data, function (result) {
                if (result) {
                    for (var i = 0; i < result.length; i++) {
                        controlId.append("<option value='" + result[i].Id + "'>" + result[i].ValueNote + "</option>");
                    }
                }
            }, function (result) { });
        },
        //controlId  控件Id  enumType 枚举类型  value值绑定Value
        DropDownDataValue: function (controlId, enumType, url) {
            var data = { enumType: enumType };
            KTree.Common.Post(url, data, function (result) {
                if (result) {
                    controlId.empty();
                    controlId.append("<option value=''>--请选择--</option>");
                    for (var i = 0; i < result.length; i++) {
                        controlId.append("<option value='" + result[i].Value + "'>" + result[i].ValueNote + "</option>");
                    }
                }
            }, function (result) { });
        },
        Group: function (controlId, url) {  //controlId  控件id，url  访问路径
            var data = {};
            if (url == null) {
                return;
            }
            KTree.Common.Post(url, data, function (result) {
                if (result) {
                    controlId.empty();
                    controlId.append("<option value=''>--请选择--</option>");
                    for (var i = 0; i < result.length; i++) {
                        controlId.append("<option value='" + result[i].Id + "'>" + result[i].Name + "</option>");
                    }
                }
            }, function (result) { });
        },
        CarInfo: function (controlId, url, status) {  //controlId  控件id，url  访问路径 ,status----车辆状态代码  车辆绑定
            if (url == null) {
                return;
            }
            var data = { status: status };
            controlId.empty();
            KTree.Common.Post(url, data, function (result) {
                if (result) {
                    controlId.empty();
                    controlId.append("<option value=''>--请选择--</option>");
                    for (var i = 0; i < result.length; i++) {
                        controlId.append("<option value='" + result[i].Id + "'>" + result[i].License + "</option>");
                    }
                }
            }, function (result) { });
        },
        Department: function (controlId, url) {  //controlId  控件id，url  访问路径 ,status----车辆状态代码  部门/组织机构绑定
            if (url == null) {
                return;
            }
            var data = {};
            KTree.Common.Post(url, data, function (result) {
                if (result) {
                    controlId.empty();
                    controlId.append("<option value=''>--请选择--</option>");
                    for (var i = 0; i < result.length; i++) {
                        controlId.append("<option value='" + result[i].Id + "'>" + result[i].OrganizeName + "</option>");
                    }
                }
            }, function (result) { });
        },
        Week: function () {
            var date = new Date;    //此处也可以写成 17/07/2014 一样识别    也可以写成 07-17-2014  但需要正则转换   
            var day = new Date(Date.parse(date));   //需要正则转换的则 此处为 ： var day = new Date(Date.parse(date.replace(/-/g, '/')));  
            var today = new Array('日', '一', '二', '三', '四', '五', '六');
            var week = today[day.getDay()];
            return week;
        },
        Number: function (id) {
            var number = $("#" + id).val();
            var zz = /^[0-9]*$/;
            if (!zz.test(number)) {
                $("#" + id).val(1);
            }
        },
        Upload: function (uploadId, dataId, fileQueueId) {
            //要引用jquery-1.10.2.js    
            //  uploadId   上传空间Id
            //  dataId   附件上传后返回值写入div  Id
            //  fileQueueId  队列 div Id
            $("#" + dataId).empty();
            $("#" + uploadId).uploadify({
                'swf': '../../Scripts/UpLoadify321/uploadify.swf',  //FLash文件路径
                'buttonText': '浏  览',                                 //按钮文本
                'uploader': '../../Scripts/UpLoadify321/UploadHandler.ashx',                       //处理文件上传Action
                'folder': '../../Scripts/UpLoadify321/uplad',
                'queueID': fileQueueId,//队列的ID
                'cancelImg': '../../Scripts/UpLoadify321/uploadify-cancel.png',
                'queueSizeLimit': 10,                          //队列最多可上传文件数量，默认为999
                'auto': false,                                 //选择文件后是否自动上传，默认为true
                'multi': true,                                 //是否为多选，默认为true
                'removeCompleted': true,                       //是否完成后移除序列，默认为true
                'fileSizeLimit': '10MB',                       //单个文件大小，0为无限制，可接受KB,MB,GB等单位的字符串值
                'onUploadSuccess': function (file, data, response) {   //每次文件操作成功后触发  file :文件对象  response:操作是否成功  data:为后台处理程序返回的值
                    if (response) {
                        //$("#" + dataId).append("<input  class='" + dataId + "' type='hidden' value='" + data + "' />");
                        $("#" + dataId).append("<a href=javascript:void(0); class=" + dataId + " id=" + data + "></a>");
                    }
                },              //文件描述
                'fileTypeExts': '*.gif; *.jpg; *.png;; *.doc,; *.docx,; *.xls,; *.xlsx,; *.ppt; *.bmp;*.tif;*.doc;*.xls;*.zip',  //上传的文件后缀过滤器
            });
        },
        GetUrlQueryString: function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(decodeURI(r[2])); return null;
        }

    };
    Date.prototype.pattern = function (fmt) {
        var hours = this.getHours();
        var o = {};
        if (hours == 12)
            o = {
                "M+": this.getMonth() + 1, //月份     
                "d+": this.getDate(), //日  
                "h+": hours > 12 ? hours % 12 == 0 ? 12 : hours % 12 + 12 : hours % 12 == 0 ? 12 : hours % 12, //小时     
                "H+": hours, //小时     
                "m+": this.getMinutes(), //分     
                "s+": this.getSeconds(), //秒     
                "q+": Math.floor((this.getMonth() + 3) / 3), //季度     
                "S": this.getMilliseconds() //毫秒     
            };
        else {
            o = {
                "M+": this.getMonth() + 1, //月份     
                "d+": this.getDate(), //日  
                "h+": hours > 12 ? hours % 12 == 0 ? 00 : hours % 12 + 12 : hours % 12 == 0 ? 00 : hours % 12, //小时   
                "H+": hours, //小时     
                "m+": this.getMinutes(), //分     
                "s+": this.getSeconds(), //秒     
                "q+": Math.floor((this.getMonth() + 3) / 3), //季度     
                "S": this.getMilliseconds() //毫秒     
            };
        }
        var week = {
            "0": "\u65e5",
            "1": "\u4e00",
            "2": "\u4e8c",
            "3": "\u4e09",
            "4": "\u56db",
            "5": "\u4e94",
            "6": "\u516d"
        };
        if (/(y+)/.test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        }
        if (/(E+)/.test(fmt)) {
            fmt = fmt.replace(RegExp.$1, ((RegExp.$1.length > 1) ? (RegExp.$1.length > 2 ? "\u661f\u671f" : "\u5468") : "") + week[this.getDay() + ""]);
        }
        for (var k in o) {
            if (new RegExp("(" + k + ")").test(fmt)) {
                fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
            }
        }
        return fmt;
    };
})(KTree);