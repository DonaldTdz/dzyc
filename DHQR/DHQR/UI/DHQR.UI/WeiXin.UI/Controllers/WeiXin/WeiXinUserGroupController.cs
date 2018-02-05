using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Base;
using DHQR.UI.Models;

namespace DHQR.UI.Controllers
{
    public class WeiXinUserGroupController : WeiXinBaseController
    {
        //
        // GET: /WeiXinUserGroup/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取分组信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public JsonResult GetPageData(QueryParam param)
        {
            WeiXinUserGroupModelService modelService = new WeiXinUserGroupModelService();
            PagedResults<WeiXinUserGroupModel> result = modelService.GetPageData(param);
            return JsonForGrid(result);
        }

        /// <summary>
        /// 同步微信用户组
        /// </summary>
        /// <returns></returns>
        public JsonResult SynUserGroup()
        {
            Guid weixinAppId = wechat_id;
            WeiXinUserGroupApi wuserApi = new WeiXinUserGroupApi();
            WeiXinUserGroupModelService modelService = new WeiXinUserGroupModelService();
            string msg;
            DoHandle dohandle;
            var data = wuserApi.QueryGroup(weixinAppId,out msg);
            modelService.SynUserGroup(data, out dohandle);
            return JsonForDoHandle(dohandle);
        }


        /// <summary>
        /// 新增或修改分组
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult EditOrAdd(Guid? Id)
        {
            WeiXinUserGroupModel model = null;
            if (Id.HasValue)
            {
                WeiXinUserGroupModelService modelService = new WeiXinUserGroupModelService();
                model = modelService.GetByKey(Id.Value);
            }
            return View(model);
        }

        //新增分组
        public JsonResult Create(WeiXinUserGroupModel model)
        {
           
            DoHandle doHandle = new DoHandle();
            WeiXinUserGroupModelService modelService = new WeiXinUserGroupModelService();
            modelService.CreateGroup(model, wechat_id, out doHandle);
            return JsonForDoHandle(doHandle);

        }

        //修改分组名称
        public JsonResult Edit(WeiXinUserGroupModel model)
        {
            DoHandle doHandle = new DoHandle();
            WeiXinUserGroupModelService modelService = new WeiXinUserGroupModelService();
            modelService.UpdateGroup(model, wechat_id, out doHandle);
            return JsonForDoHandle(doHandle);

        }


        /// <summary>
        /// 编辑用户所属用户组
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns></returns>
        public ActionResult EditUserGroup(string groupid)
        {

            return View();
        }

        /// <summary>
        /// 获取分组信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public JsonResult GetAll( )
        {
            WeiXinUserGroupModelService modelService = new WeiXinUserGroupModelService();
            var result = modelService.GetAll().ToList();
            return Json(result,JsonRequestBehavior.AllowGet);
        }


        //删除该分组用户
        public JsonResult DelFromGroupByUserIds(string openids)
        {
            DoHandle doHandle = new DoHandle();
            IList<string> openIdList = openids.Split(',').ToList();
            WeiXinUserInfoModelService modelService = new WeiXinUserInfoModelService();
            modelService.DelFromGroupByUserIds(wechat_id, openIdList, out doHandle);
            return JsonForDoHandle(doHandle);

        }

        //将指定用户加入目标分组
        public JsonResult AddToGroupByUserIds(string openids,int groupid)
        {
            DoHandle doHandle = new DoHandle();
            IList<string> openIdList = openids.Split(',').ToList();
            WeiXinUserInfoModelService modelService = new WeiXinUserInfoModelService();
            modelService.AddToGroupByUserIds(wechat_id, openIdList,groupid, out doHandle);
            return JsonForDoHandle(doHandle);

        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllUsers()
        {
            WeiXinUserInfoModelService modelService = new WeiXinUserInfoModelService();
            var data = modelService.GetAll().ToList();
            return JsonForGrid(data);
        }

        /// <summary>
        ///根据分组获取用户
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUserByGroupid(int groupid)
        {
            WeiXinUserInfoModelService modelService = new WeiXinUserInfoModelService();
            var data = modelService.GetByGroupId(groupid);
            return JsonForGrid(data);
        }





    }
}
