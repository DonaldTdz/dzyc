//通用上传
function ImgFile(fileURL, Size, txtImgNameID, imgNameID) {
    for (var i = 1; i < Size; i++) {
        $('#txtImg' + i).uploadify({
            'auto': true, //选定文件后是否自动上传，默认false

            'fileDesc': '*.jpg;*.png;*.gif', //出现在上传对话框中的文件类型描述

            'fileExt': '*.jpg;*.png;*.gif', //控制可上传文件的扩展名，启用本项时需同时声明fileDesc

            'sizeLimit': 2044000, //控制上传文件的大小，单位byte common.js中定义

            'scriptData': { 'FileType': 'Image', 'F': fileURL, 'N': i },

            'onSelectOnce': function() { $("#hidimg").val(parseInt($("#hidimg").val()) + 1); $("#spnimg").html("图片还在上传中，请等待"); },

            'onComplete': function(e, queueID, fileObj, response, data) {
                if (response.indexOf("|") > 0) {
                    var arr = response.split("|");
                    var fileName = arr[1].trim();
                    $("#hidimg").val(parseInt($("#hidimg").val()) - 1);
                    $("#" + txtImgNameID + arr[0]).val(fileName);
                    $("#spnimg" + arr[0]).html("上传成功   <a href='javascript:DelFile(\"spnimg" + arr[0] + "\",\"" + imgNameID + arr[0] + "\",\"" + fileName.replace("\\", "-") + "\",\"" + fileURL + "\",\"" + txtImgNameID + arr[0] + "\")'>删除</a>");
                    if (parseInt($("#hidimg").val()) == 0)
                        $("#spnimg").html("");
                    $("#" + imgNameID + arr[0]).attr("src", arr[2]);
                    $("#" + imgNameID + arr[0]).show();
                    if ($("#dialog-message").find("#spnimg1") != undefined)
                        $("#dialog-message").find("#spnimg1").html("上传成功   <a href='javascript:DelFile(\"spnimg" + arr[0] + "\",\"" + imgNameID + arr[0] + "\",\"" + fileName.replace("\\", "-") + "\",\"" + fileURL + "\",\"" + txtImgNameID + arr[0] + "\")'>删除</a>");
                    if ($("#" + txtImgNameID) != undefined)
                        $("#" + txtImgNameID).val(fileName);
                }
            }
        });
    }
}
// 市场营销图片处理
function ImgMarkFile(siftStr,fileURL,ImgjQuery) {


    $(siftStr).uploadify({
        'auto': true, //选定文件后是否自动上传，默认false

        'fileDesc': '*.jpg;*.png;*.gif', //出现在上传对话框中的文件类型描述

        'fileExt': '*.jpg;*.png;*.gif', //控制可上传文件的扩展名，启用本项时需同时声明fileDesc

        'sizeLimit': 2044000, //控制上传文件的大小，单位byte common.js中定义

        'scriptData': { 'FileType': 'Image', 'F': fileURL, 'N': 1 },

        'onSelectOnce': function() {

        },
        'onComplete': function(e, queueID, fileObj, response, data) {
        
            var jsonData = "[]";
           
            if (response.indexOf("|") > 0) {
           
                var arr = response.split("|");
                if (arr.length > 2) {

                    jsonData = "{\"FilePath\":\"" + arr[1] + "\",UrlPath:\"" + arr[2] + "\"}"
                    // alert("siftStr:" + siftStr + ",ImgjQuery:" + ImgjQuery);
                    $(ImgjQuery).attr("sour_src", arr[1]);
                    $(ImgjQuery).attr("src", arr[2]);
                }
            }
        }
    });
    
}

/*
  公共上传  
  @fileUploadControl :上传标签  
  @DirectoryConfig ：目录标签
  @ImgControl：显示图片控件
  @ImgValControl：存放图片路径控件
*/
function ImgCommanFile(fileUploadControl, DirectoryConfig, ImgControl,ImgValControl) {

    $(fileUploadControl).uploadify({
        'auto': true, //选定文件后是否自动上传，默认false

        'fileDesc': '*.jpg;*.png;*.gif', //出现在上传对话框中的文件类型描述

        'fileExt': '*.jpg;*.png;*.gif', //控制可上传文件的扩展名，启用本项时需同时声明fileDesc

        'sizeLimit': 2044000, //控制上传文件的大小，单位byte common.js中定义

        'scriptData': { 'FileType': 'Image', 'F': DirectoryConfig, 'N': 1 },

        'onSelectOnce': function() {

        },
        'onComplete': function(e, queueID, fileObj, response, data) {
            var jsonData = "[]";
            if (response.indexOf("|") > 0) {
                var arr = response.split("|");
                if (arr.length > 1) {
                    $(ImgValControl).attr("value",arr[1]);
                    $(ImgControl).css("display","block").attr("src", arr[2]);
                    
                }

            }


        }
    });

}





function DelFile(spnID, imgID, fileName, fileURL, txtImgNameID) {

    $.getJSON(UploadUrl + "DelAction.aspx?A=DelTemp&T=Image&D=" + fileURL + "&F=" + fileName + "&format=json&jsoncallback=?",
        function(data) {
            try {
                if (data.result == 1) {
                    $("#" + txtImgNameID).val("");
                    $("#" + spnID).html("删除成功");
                    $("#" + imgID).attr("src", ImgServerUrl + "images/c/" + Version + "/blank.gif");
                }
                else {
                    $("#" + spnID).html($("#" + spnID).html() + "   删除失败");
                }
            }
            catch (e) {

            }
        });
}  