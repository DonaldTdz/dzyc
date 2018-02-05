//将jqgri的行转成json
//grid要转换的grid
//splits:用与几个周期拉通过滤用
//oneRow:传过来的是否是一行数据
//mapToProperty:将对象property映射成其它名称
function rowDataToJson(rowDatas, splits, oneRow, mapToProperty) {
    var propertyLength = 0; //对象属性长度
    var jsonData = ""
    if (oneRow) {
        for (var tmp in rowDatas) {
            propertyLength++;
        }
        jsonData += "{";
        for (var property in rowDatas) {
            propertyLength--;
            if (property == "distinct"||property=="actions") continue;
            var newProperty = property;
            if (mapToProperty != undefined) {
                if (mapToProperty[property] != undefined) {
                    newProperty = mapToProperty[property];
                }
            }
            if (propertyLength == 0) {
                jsonData += newProperty + ": \"" + rowDatas[property] + "\"";
            } else {
                jsonData += newProperty + ": \"" + rowDatas[property] + "\",";
            }

        }
        jsonData += "}";
    } else {
        jsonData = "[";
        for (var tmp in rowDatas[0]) {
            propertyLength++;
        }
        for (var i in rowDatas) {
            if (i == "distinct" || i == "actions" || i=="indexOf") continue;
            var j = propertyLength;
            jsonData += "{";
            for (var property in rowDatas[i]) {
                if (property == "distinct") continue;
                var newProperty = property;
                if (mapToProperty != undefined) {
                    if (mapToProperty[property] != undefined) {
                        newProperty = mapToProperty[property];
                    }
                }
                j--;
                if (j == 0) {
                    jsonData += newProperty + ": \"" + rowDatas[i][property] + "\"";
                } else {
                    jsonData += newProperty + ": \"" + rowDatas[i][property] + "\",";
                }

            }
            jsonData += "},";
        }
        jsonData = jsonData.substring(0, jsonData.length - 1);
        jsonData += "]";
    }
    return jsonData;
}