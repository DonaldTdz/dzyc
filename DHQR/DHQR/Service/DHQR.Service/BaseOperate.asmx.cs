using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.Services.Protocols;
using Common.Base;
using DHQR.BusinessLogic.Implement;
using DHQR.DataAccess.Entities;

namespace DHQR.Service
{
    /// <summary>
    ///到货确认基础服务
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    [SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.RequestElement)] 
    public class BaseOperate : System.Web.Services.WebService
    {


        #region  账号信息

        /// <summary>
        /// 系统登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="psw"></param>
        /// <returns></returns>
        [WebMethod]
        public string Login(string data)
        {
            var jser = new JavaScriptSerializer();
            DistDlvmanLogic logic = new DistDlvmanLogic();
            LoginParam loginParam = jser.Deserialize<LoginParam>(data);
            DoHandle dohandle;
            DistDlvman car = logic.Login(loginParam.UserName, loginParam.Psw, out dohandle);
            //登录成功
            if (dohandle.IsSuccessful)
            {
                var result = jser.Serialize(car);
                return result;
            }
                //登录失败
            else
            {
                return jser.Serialize(car);
            }

        }


        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod]
        public string ChangeLoginPsw(string data)
        {
            var jser = new JavaScriptSerializer();
            ChageLoginPswParam param = jser.Deserialize<ChageLoginPswParam>(data);
            DistDlvmanLogic logic = new DistDlvmanLogic();
            DoHandle dohandle;
            logic.ChangeLoginPsw(param, out dohandle);
            var result = jser.Serialize(dohandle);
            return result;
              
        }



        #endregion

        #region 零售户收货密码修改

        /// <summary>
        /// 零售户收货密码修改
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod]
        public string ChangeDeliveryPsw(string data)
        {
            var jser = new JavaScriptSerializer();
            ChangeDeliveryPswParam param = jser.Deserialize<ChangeDeliveryPswParam>(data);
            RetailerLogic logic = new RetailerLogic();
            DoHandle dohandle;
            logic.ChangeDeliveryPswParam(param, out dohandle);
            var result = jser.Serialize(dohandle);
            return result;
        }


        /// <summary>
        /// 同步车辆信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod]
        public string SysDistCar(string data)
        {
            var jser = new JavaScriptSerializer();
            DoHandle dohandle;
            LangchaoLogic logic = new LangchaoLogic();
            logic.SysDistCars(out dohandle);
            var result = jser.Serialize(dohandle);
            return result;

        }


        #endregion

        #region APP版本更新


        /// <summary>
        /// 检查更新
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod]
        public string CheckAppVersion(string data)
        {
            var jser = new JavaScriptSerializer();
            AppVersion param = jser.Deserialize<AppVersion>(data);
            AppVersionLogic logic = new AppVersionLogic();
            var appVersion = logic.CheckAppVersion(param);
            var result = jser.Serialize(appVersion);
            return result;
        }


        #endregion

        #region 进出门卡信息


        /// <summary>
        /// 获取进出门卡信息
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetEntranceCards()
        {
            var jser = new JavaScriptSerializer();
            EntranceCardLogic logic = new EntranceCardLogic();
            var cards = logic.GetAll().Where(f => f.IsValid == true).ToList();
            var result = jser.Serialize(cards);
            return result;
        }


        #endregion

    }
}
