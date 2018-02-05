using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHQR.UI.Models;
using Common.Base;
using DHQR.UI.Models.Base;
using DHQR.UI.DHQRCommon;

namespace DHQR.UI.Controllers
{
    /// <summary>
    /// 用户类
    /// </summary>
    public class UserController : BaseController
    {
        //
        // GET: /User/
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult Index()
        {
            //var data1 = new DepartmentModelService().GetAll();
            //var departments = data1
            //    .Select(item => new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            //ViewData["Departments"] = departments;
            return View();
        }
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetPageData(QueryParam queryParam)
        {
            var service = new UserModelService();
            PagedResults<UserModel> data = service.GetPageData(queryParam);
            return JsonForGrid(data);
        
        }

        /// <summary>
        /// 取所有用户
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetAll() 
        {
            var service = new UserModelService();
            var result = service.GetAll().ToList();
            return Json(result);
        }

        ///// <summary>
        ///// 获取所有
        ///// </summary>
        ///// <returns></returns>
        //public JsonResult GetAll()
        //{
        //    var service = new UserModelService();
        //   var data = service.GetAll().ToList();
        //    return JsonForGrid(data);
        //}


        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Add(UserModel userModel)
        {
            var service = new UserModelService();
            DoHandle doHandle;
            service.Add(userModel, out doHandle);
            return JsonForDoHandle(doHandle);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
       [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Edit(UserModel userModel)
        {
            var service = new UserModelService();
            DoHandle doHandle;
            service.Edit(userModel, out doHandle);
            if (doHandle.IsSuccessful)
            {
                service.UpdateOtherUserNames(out doHandle);
            }
            return JsonForDoHandle(doHandle);
        }


        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
       [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Del(UserModel userModel)
        {
            var service = new UserModelService();
            DoHandle doHandle;
            service.Del(userModel, out doHandle);
            return JsonForDoHandle(doHandle);
        }

       [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult Freeze(UserModel userModel)
        {
            var service = new UserModelService();
            DoHandle doHandle;
            service.Freeze(userModel, out doHandle);
            return JsonForDoHandle(doHandle);
        }




        #region 登录密码


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
       [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult ChangePsd()
        {
            ViewData["UserName"] = GetLogonName();

            return View();
        }

        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <param name="oldPsd"></param>
        /// <param name="newPsd"></param>
        /// <returns></returns>
        [HttpPost]
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult LoginPsdChange(string oldPsd, string newPsd)
        {
            var user = new UserModelService().GetByName(GetLogonName());
            DoHandle doHandle;
            new UserModelService().ChangePsd(user.Id, oldPsd, newPsd, out doHandle);
            return JsonForDoHandle(doHandle);
        }



        #endregion

        #region 用户申请
        /// <summary>
        /// 用户申请
        /// </summary>
        /// <returns></returns>
       [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult ApplyReq()
        {
            return View();
        }

      

       
        #endregion

        #region 重置密码申请

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
       [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult ResetPswApply()
        {
            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
       [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult ResetPswAppove()
        {
            return View();
        }

     

        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
       [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult ApplyPswReq(Guid Id, byte State)
        {
            DoHandle dohandle=new DoHandle();
          
            return JsonForDoHandle(dohandle);
        }


        #endregion


    }
}
