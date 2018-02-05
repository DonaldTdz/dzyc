String.prototype.Trim = function(trimChar) {
    if (trimChar != undefined && trimChar != "") {
        var regex = new RegExp("(^" + trimChar + "*)|(" + trimChar + "*$)", "g");
        return this.replace(regex, "");
    }
    else {
        return this.replace(/^\s+/g, "").replace(/\s+$/g, "");
    }
}

//所有省市县数据
function getProvince(ID) {
    if (ID == undefined) ID = "";
    if (province == undefined || province == null || province.length <= 0) {
        return;
    }
    var pathID = $("#hidPathID" + ID).val();
    if (pathID == null || pathID == "") pathID = "^-1^-1^-1^";
    pathID = pathID.Trim('\\^');
    pathID = pathID.split('^');
    $("#sltProvince" + ID).empty();
    $("#sltProvince" + ID).append("<option value=\"-1\">--请选择--</option>");
    if (province == undefined || province == null || province.length == 0) return;
    for (var i = 0; i < province.length; i++) {
        $("#sltProvince" + ID).get(0).options.add(new Option(province[i]["AreaName"], province[i]["AreaID"]));
    }
    if (pathID != null && pathID.length == 4) {
        $("#sltProvince" + ID).val(pathID[1]);
        getCity(ID, pathID[2], pathID[1]);
        getCountry(ID, pathID[3], pathID[2]);
        return;
    }
    var length = pathID.length;
    $("#sltProvince" + ID).val(pathID[0]);
    switch (length) {
        case 1:
            getCity(ID, "-1", pathID[0]);
            break;
        case 2:
            getCity(ID, pathID[1], pathID[0]);
            getCountry(ID, "-1", pathID[1]);
            break;
        default:
            getCity(ID, pathID[1], pathID[0]);
            getCountry(ID, pathID[2], pathID[1]);
            break;
    }
}
//绑定市级
function getCity(ID, currentID, prentID) {
    if (ID == undefined) ID = "";
    var selectedID = "";
    if (prentID == undefined || prentID == "") {
        $("#sltProvince" + ID + " option:selected").each(function(i, own) {
            selectedID = $(own).val();
        });
    }
    else {
        selectedID = prentID;
    }
    $("#sltCity" + ID + " option").remove();
    $("#sltCity" + ID).append("<option value=\"-1\">--请选择--</option>");
    $("#sltCounty" + ID).empty();
    $("#sltCounty" + ID).append("<option value=\"-1\">--请选择--</option>");
    if (city == null || city == undefined || city.length == 0) return;
    for (var i = 0; i < city.length; i++) {
        if (city[i]["ParentID"] == selectedID) {
            $("#sltCity" + ID).get(0).options.add(new Option(city[i]["AreaName"], city[i]["AreaID"]));
        }
    }
    $("#sltCity" + ID).val(currentID);

}
//绑定县级
function getCountry(ID, currentID, prentID) {

    if (ID == undefined) ID = "";
    var selectedID = "";
    if (prentID == undefined || prentID == "") {
        $("#sltCity" + ID + " option:selected").each(function(i, own) {
            selectedID = $(own).val();
        });
    }
    else {
        selectedID = prentID;
    }
    $("#sltCounty" + ID + " option").remove();
    $("#sltCounty" + ID).append("<option value=\"-1\">--请选择--</option>");
    if (country != null || country == undefined || country.length == 0)
        for (var i = 0; i < country.length; i++) {
        if (country[i]["ParentID"] == selectedID) {
            $("#sltCounty" + ID).get(0).options.add(new Option(country[i]["AreaName"], country[i]["AreaID"]));
        }
    }
    $("#sltCounty" + ID).val(currentID);

}
