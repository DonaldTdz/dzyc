using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace DHQR.Service
{
    /// <summary>
    /// WebTest 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    [SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.RequestElement)] 
    public class WebTest : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        /// <summary>
        /// 客户端登陆
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        [WebMethod]
        public string Login(string loginInfo)
        {
            HandleResult result = new HandleResult {Msg="操作成功!",State=true };
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string obj = serializer.Serialize(result);
            return obj;
        }
    }

    /// <summary>
    /// 返回接口操作结果
    /// </summary>
    public class HandleResult
    {
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Msg { set; get; }


        /// <summary>
        /// 状态
        /// </summary>
        public bool State { set; get; }
    }
}
