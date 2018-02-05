using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Base;
using DHQR.UI.Models;

namespace DHQR.UI.Controllers
{
    /// <summary>
    /// 微信用户基础信息控制层
    /// </summary>
    public class WeiXinUserInfoController : WeiXinBaseController
    {
        //
        // GET: /WeiXinUserInfo/

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 获取微信用户基础信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public JsonResult GetPageData(QueryParam param)
        {
            WeiXinUserInfoModelService modelService = new WeiXinUserInfoModelService();
            PagedResults<WeiXinUserInfoModel> result = modelService.GetPageData(param);
            return JsonForGrid(result);
        }


        /// <summary>
        /// 同步微信用户基础信息
        /// </summary>
        /// <returns></returns>
        public JsonResult SynWeiXinUserInfo()
        {
            WeiXinUserApi wuserApi = new WeiXinUserApi();
            WeiXinUserInfoModelService modelService = new WeiXinUserInfoModelService();
            DoHandle dohandle;
            var data = wuserApi.QueryWeiXinUserInfo(wechat_id);
            modelService.SynWeiXinUserInfo(data, out dohandle);
            return JsonForDoHandle(dohandle);
        }


        /// <summary>
        /// 修改页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditePage(string openid)
        {
            WeiXinUserInfoModelService modelService = new WeiXinUserInfoModelService();
            WeiXinUserGroupModelService groupService = new WeiXinUserGroupModelService();
            var data = modelService.GetByOpenId(openid);
            var groups = groupService.GetUserGroupList();
            ViewData["groups"] = groups;
            return View(data);
        }


       /// <summary>
        /// 更新用户分组和用户备注名
       /// </summary>
       /// <param name="openid"></param>
       /// <param name="groupid"></param>
       /// <param name="remark"></param>
       /// <returns></returns>
        public JsonResult UpdateUser(string openid,int groupid,string remark)
        {
            DoHandle dohandle;
            WeiXinUserInfoModelService modelService = new WeiXinUserInfoModelService();
            modelService.UpdateUser(wechat_id, openid, groupid, remark, out dohandle);
            return JsonForDoHandle(dohandle);
        }


        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAll()
        {
            WeiXinUserInfoModelService modelService = new WeiXinUserInfoModelService();
            var data = modelService.GetAll();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}
