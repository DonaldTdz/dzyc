using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Base;
using DHQR.UI.Models;
using DHQR.DataAccess.Entities;

namespace DHQR.UI.Controllers
{
    public class WeiXinUserController : WeiXinBaseController
    {
        //
        // GET: /WeiXinUser/


        /// <summary>
        /// 零售户绑定信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            DistRutModelService rutService = new DistRutModelService();
            ViewData["RutList"] = rutService.GetRutList(true);
            return View();
        }

        /// <summary>
        /// 内部员工绑定信息
        /// </summary>
        /// <returns></returns>
        public ActionResult InnerUser()
        {
            return View();
        }


        public ActionResult EditOrAdd(Guid? Id)
        {
            WeiXinUserModel model = null;
            if (Id.HasValue)
            {
                WeiXinUserModelService modelService = new WeiXinUserModelService();
                model = modelService.GetByKey(Id.Value);
            }
            return View(model);
        }

        //新增微信用户
        public JsonResult Create(WeiXinUserModel model)
        {
            WeiXinUserTypeModelService tyservice = new WeiXinUserTypeModelService();
            WeiXinUserModelService modelService = new WeiXinUserModelService();
            DoHandle doHandle=new DoHandle();
            var type = tyservice.GetByCode(model.UserType);
            model.Id = Guid.NewGuid();
            model.WeiXinUserTypeId = type.Id;
            model.WeiXinAppId = wechat_id;
            modelService.Create(model, out doHandle);
            return JsonForDoHandle(doHandle);

        }

        //编辑
        public JsonResult Edit(WeiXinUserModel model)
        {
            DoHandle doHandle=new DoHandle();
            WeiXinUserModelService modelService = new WeiXinUserModelService();
            modelService.Edit(model, out doHandle);
            return JsonForDoHandle(doHandle);

        }

        //删除数据
        public JsonResult Delete(Guid id)
        {
            DoHandle doHandle;
            WeiXinUserModelService modelService = new WeiXinUserModelService();
            modelService.Delete("WeiXinUsers", id, out doHandle);
            return JsonForDoHandle(doHandle);

        }

        public JsonResult GetPageData(QueryParam param)
        {
            WeiXinUserModelService modelService = new WeiXinUserModelService();
            PagedResults<WeiXinUserModel> result = modelService.GetPageData(param);
            return JsonForGrid(result);
        }

        /// <summary>
        /// 查询绑定信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public JsonResult QueryData(WeiXinUserQueryParam param)
        {
            WeiXinUserModelService modelService = new WeiXinUserModelService();
            PagedResults<WeiXinUserModel> result = modelService.QueryData(param);
            return JsonForGrid(result);
        }


        #region 账号绑定

        /// <summary>
        /// 手动绑定账号
        /// </summary>
        /// <returns></returns>
        public ActionResult Bind()
        {
            return View();
        }

        //绑定
        public JsonResult BindUser(Guid Id, string WxUserName)
        {
            DoHandle doHandle;
            WeiXinUserModelService modelService = new WeiXinUserModelService();
            WeiXinUserModel model = new WeiXinUserModel 
            {
                Id=Id,
                WxUserName=WxUserName,
                WeiXinAppId=wechat_id
            };
            modelService.BindByFromSys(model, out doHandle);
            return JsonForDoHandle(doHandle);

        }


        //新增微信用户
        public JsonResult SendMsg()
        {
            //string msg = "";
            //Guid wppId = Guid.Parse("0CD98D1B-C476-4A02-B4B9-0F619FCCD36F");
            //ConmmonApi cpi = new ConmmonApi();
            //var ss = cpi.ResponseMsgTemplate(wppId, out msg);
            return null;

        }


        #endregion

    }
}
