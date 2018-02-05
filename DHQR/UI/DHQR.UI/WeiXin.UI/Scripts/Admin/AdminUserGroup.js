$(document).ready(function () {
    $("#ctl00_ContentPlaceHolderList_ddlOrg").change(function () {
        SearchNewsInfo();
    });
});


function Showrdo(id) {
    if (id == 1) {
        $(".tdSingleFlag0").show();
    }
    else {
        $(".tdSingleFlag0").hide();
    }
}

function Selecisnull() {
    var lstDebitNoteSecond = $(".lstDebitNoteSecond");
    if (!$._regsubmit('btnOK', '')) {
        return false;
    }
    if (lstDebitNoteSecond.find("option").length > 0 && lstCreditNoteSecond.find("option").length > 0) {
        return true;
    }
    else {
        alert("还有信息没有填写完整,不能提交");
        return false;
    }
}

function Add(FristID, SecondID, hidFrist, hidSecond) {
    if ($("#" + FristID).get(0).selectedIndex != -1) {
        var input = false;
        $("#" + SecondID + " option").each(function () {
            if (this.value == $("#" + FristID).val()) {
                input = true;
            }
        })
        if (input != true) {
            $("#" + SecondID).append("<option value='" + $("#" + FristID).val() + "'>" + $("#" + FristID).find("option:selected").text() + "</option>");
            Addhid($("#" + FristID).find("option:selected").text(), $("#" + FristID).val(), hidFrist, hidSecond)
        }
    }
}


function Addhid(tx, va, hidFrist, hidSecond) {
    $("#" + hidFrist).attr("value", $("#" + hidFrist).attr("value") + tx + "|")
    $("#" + hidSecond).attr("value", $("#" + hidSecond).attr("value") + va + "|")
}



function Delehid(id, hidFrist, hidSecond) {
    var hidFirst = document.getElementById(hidFrist);
    var hidSecond = document.getElementById(hidSecond);
    var strFirst = hidFirst.value.split("|");
    var strSecond = hidSecond.value.split("|");
    hidFirst.value = "";
    hidSecond.value = hidSecond.value.replace(id + "|", "");
    for (var i = 0; i < strFirst.length; i++) {
        if (i != id) {
            if (strFirst[i] != "") {
                hidFirst.value = hidFirst.value + strFirst[i] + "|";
                hidSecond.value = hidSecond.value + strSecond[i] + "|";
            }
        }
    }
}


function Del(FristID, hidFrist, hidSecond) {
    var lstDebitNoteSecond = document.getElementById(FristID);
    if (lstDebitNoteSecond.selectedIndex >= 0) {
        for (var i = 0; i < lstDebitNoteSecond.options.length; i++) {
            if (lstDebitNoteSecond.options[i].value == lstDebitNoteSecond.options[lstDebitNoteSecond.selectedIndex].value) {
                Delehid(lstDebitNoteSecond.options[i].value, hidFrist, hidSecond)
                lstDebitNoteSecond.options.remove(i);
                break;
            }
        }
    }
}

function SearchNewsInfo() {
    var orgId = $("#ctl00_ContentPlaceHolderList_ddlOrg").find("option:selected").val();
    if (orgId == -1) {
        return;
    }

    function sparam(param) {
        return orgId;
    };

    /* var param = {
    ClassName: "HQBuy.Business.Passport.AdminGroupBLL",
    MethodName: "GetAdminGroupByOrgID",
    ParamModelName: "System.Int32",
    onComplete: onSubmitSucess,
    onError: onSubmitError,
    onRequest: sparam                
    };
         
    $.ajaxRequest(param);*/
    $.post('/AjaxServerPage/Admin.ashx', {
        Method: "GetAdminGroupByOrgID",
        orgId: orgId
    }, function (model) {
        if (model == null || model.length <= 0) {
            return;
        }
        else {
            onSubmitSucess(model);
        }
    }, "json");

    function onSubmitSucess(model) {
        if (model == null || model.length <= 0) {
            return;
        }
        else {
            $("#lbGroup").empty();
            for (var i = 0; i < model.length; i++) {
                $("#lbGroup").append("<option value=\"" + model[i]["GroupID"] + "\">" + model[i]["GroupName"] + "</option>");
            }

        }
    }
}

function onSubmitError(model) {
    alert("操作错误！");
}