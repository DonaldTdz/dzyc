using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Base;
using DHQR.UI.Models;
using DHQR.UI.DHQRCommon;

namespace DHQR.UI.Controllers
{
    public partial class WxMobileController : MobileBaseController
    {
        //
        // GET: /WxMobileController.Pro/

        /// <summary>
        /// 零售户年度报告
        /// </summary>
        /// <returns></returns>
         [MobileIgnoreAttribute(MobileIgnoreType.IgnoreCheck)]
        public ActionResult RetailerYear()
        {
            OAuthRedirectUri ot = new OAuthRedirectUri();
             OAuthUser OAuthUser_Model =new OAuthUser();
            if (!string.IsNullOrEmpty(Request.QueryString["code"]))
            {
                string Code = Request.QueryString["code"].ToString();
                //获得Token  
                OAuth_Token Model = ot.Get_token(Code);
                //Response.Write(Model.access_token);  
                 OAuthUser_Model = ot.Get_UserInfo(Model.access_token, Model.openid);
                //Response.Write("用户OPENID:" + OAuthUser_Model.openid + "<br>用户昵称:" + OAuthUser_Model.nickname + "<br>性别:" + OAuthUser_Model.sex + "<br>所在省:" + OAuthUser_Model.province + "<br>所在市:" + OAuthUser_Model.city + "<br>所在国家:" + OAuthUser_Model.country + "<br>头像地址:" + OAuthUser_Model.headimgurl + "<br>用户特权信息:" + OAuthUser_Model.privilege);
                //Response.End();
            }

            //获取零售户相关信息
            WeiXinRetailerProModel model = new WeiXinUserInfoModelService().GetWeiXinRetailerPro(OAuthUser_Model.openid);
            return View(model);
        }


        public ActionResult DlvManYear()
        {
            return View();
        }

    }
}
