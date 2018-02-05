using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.UI.CooKies;
using Common.UI.EncryptTool;
using DHQR.UI.Models;

namespace DHQR.UI.Controllers
{
    public class WeiXinBaseController : BaseController
    {
        public Guid user_id = Guid.Empty;
        public string user_name = "";
        public Guid wechat_id = Guid.Empty;
        public string wechat_name = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var userName =EncryptTool.Decrypt( CooKies.GetCookies("token"));
            var uId = EncryptTool.Decrypt(CooKies.GetCookies("user_id"));
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(uId))
            {
               

            }
            else
            {
                getuser();
                if (!getapi())
                {
                    if (Request.QueryString["bindapi"] == "1")
                    {
                        Response.Write("<script Language=\"Javascript\">alert(\"请先配置微信接口：【基础配置】》【微信授权配置】\")");
                        Response.Write("</script>");
                        Response.Write("<script Language=\"Javascript\">location.href=\"/WeixinApp/Index\";");
                        Response.Write("</script>");
                        Response.End();
                    }
                }
            }
        }
        

        #region 获取用户信息
        private void getuser()
        {
            var userName =EncryptTool.Decrypt( CooKies.GetCookies("token"));
            var userId = Guid.Parse(EncryptTool.Decrypt(CooKies.GetCookies("user_id")));
            user_id = userId;
            user_name = userName;
        }
        #endregion

        #region 获取接口信息
        private bool getapi()
        {
            var vi = EncryptTool.Decrypt(CooKies.GetCookies("wechat_id"));
            if (vi != null)
            {
                wechat_id = Guid.Parse(vi);
                wechat_name = EncryptTool.Decrypt(CooKies.GetCookies("wechat_name"));
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

    }
}
