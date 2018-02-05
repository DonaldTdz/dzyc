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
    public class AccountController : BaseController
    {
        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public ActionResult LoginInfo(UserModel userModel)
        {
            var service = new UserModelService();
            DoHandle doHandle = new DoHandle();

            try
            {
                service.ValidateUser(userModel, out doHandle);
            }
            catch (Exception ex)
            {
                throw ex;

            }



            return Json(doHandle);
        }

        /// <summary>
        /// 游客登录
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public ActionResult LoginByGuest()
        {
            var service = new UserModelService();
            DoHandle doHandle = new DoHandle();
            UserModel userModel = new UserModel
            {
                Name = "guest",
                PassWord = "guest"
            };
            try
            {
                service.ValidateUser(userModel, out doHandle);
            }
            catch (Exception ex)
            {
                throw ex;

            }



            return Json(doHandle);
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public ActionResult LoginOut()
        {
            var service = new UserModelService();
            service.LoginOut();
            return RedirectToAction("Login");
        }

      

      

        /// <summary>
        /// 返回结果格式的状态
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        protected JsonResult JsonForDoHandle(DoHandle status)
        {
            if (status == null)
                return null;
            return Json(status);
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult Index()
        {
            ViewData["UserName"] = GetCurrentUser().Nickname;
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public JsonResult UserValid(string userName, string passWord)
        {
            return Json(new DoHandle() { IsSuccessful = true, OperateMsg = "验证成功" });
        }

        #region 首页
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public ActionResult Home()
        {
            return View();
        }

        /// <summary>
        /// 取用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public JsonResult GetRolesByUser() 
        {
            ModuleRoleModelService roleSer = new ModuleRoleModelService();
            var result = roleSer.GetRolesByUser(this.GetCurrentUser().Id);
            return Json(result);
        }

      
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public JsonResult GetUnDoResetPassWordApply()
        {
            return null;
        }
        #endregion


        #region New Project

        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public ActionResult WxLogin()
        {
            return View();
        }


        #endregion
    }
}
