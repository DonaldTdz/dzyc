
function SearchUser() {


    if($("#"+ddlOrgID).val() == -1)
    { 
      $("#selUserList").empty(); 
      return;
    }
     function sparam(param)
    {
        return $("#"+ddlOrgID).val();
    } 

    $.post('/AjaxServerPage/Admin.ashx', {
        Method: "GetAdminUserByOrgID",
        OrdId: sparam
    }, function(model) {
    
        if (model == null || model.length <= 0) {
            return;
        }
        else {

            onSubmitSucess(model);
        }
    }, "json");
    
    function onSubmitSucess(model)
    {
 
       if(model==null||model.length<=0)
       {
         $("#selUserList").empty(); 
        return;
       }
       else
       {
        $("#selUserList").empty(); 
        for(var i=0;i<model.length;i++)
        {
        $("#selUserList").append("<option value=\"" + model[i]["UserID"] + "\">" + model[i]["UserName"]+"--"+model[i]["TrueName"] + "</option>"); 
        }
        
       }
    }
}
function onSubmitError(model)
{
    alert("操作错误！");
}

function SelectAllUser()
{
    $.each($("#selUserList option"), function(i,own){                   
            var sValue = $(own).val();
            var sText = $(own).text();
            if(!checkRepeat("selSelectUserList",sValue))
                $("#selSelectUserList").append("<option value=\"" + sValue + "\" selected=selected>" + sText.substring(sText.lastIndexOf("-")+1) + "</option>");
        });
}

function RemoveSeledctUser()
{
    $.each($("#selUserList option:selected"), function(i,own){                   
            var sValue = $(own).val();
            var sText = $(own).text();
            if(!checkRepeat("selSelectUserList",sValue))
                $("#selSelectUserList").append("<option value=\"" + sValue + "\" selected=selected>" + sText.substring(sText.lastIndexOf("-")+1) + "</option>");
        });
}

function DeleteSelectUser()
{
    $.each($("#selSelectUserList option:selected"), function(i,own){                   
             $(own).remove(); 
        });
}

function DeleteAllSelectUser()
{ 
    $.each($("#selSelectUserList option"), function(i,own){           
             $(own).remove(); 
        });
}


function checkRepeat(ctrolID,value)
{
    var result=false
    $.each($("#"+ctrolID+" option:selected"), function(i,own){                   
            var sValue = $(own).val();            
            if(sValue==value)
                result= true;
        });
    return result;
}



function GetSelectedUser()
{
    var dialogkey='#'+$._GetSearch('dialogkey');
    var vs = ""; 
    var ts = "";     
     $.each($("#selSelectUserList option"), function(i,own){ 
          
          if(ts!="")
          {
              vs += ",";
              ts += ",";          
          }         
          vs += $(own).val();
          ts += $(own).text();   
        }); 
 
    $(window.parent.document).find(dialogkey).val(ts); 
    $(window.parent.document).find(dialogkey.replace("txt","hid")).val(vs);
    $(window.parent.document).find('#winclose').click();
}

function InitValue()
{ 
 
   var dialogkey='#'+$._GetSearch('dialogkey');
   var vaa = ""; 
   var text = "";     
   text = $(window.parent.document).find(dialogkey).val(); 
   vaa = $(window.parent.document).find(dialogkey.replace("txt","hid")).val(); 
 
    var texts= new Array();    
    texts=text.split(",");     
    var vs= new Array();    
    vs=vaa.split(",");    //value.split会出错
    for (i=0;i<texts.length ;i++ )    
    {      
        if(texts[i].length>0 )
        {
             $("#selSelectUserList").append("<option value=\"" + vs[i] + "\" selected=selected>" + texts[i] + "</option>");
        }
    
    }  
}

 