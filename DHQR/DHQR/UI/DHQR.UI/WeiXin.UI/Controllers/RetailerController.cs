using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Base;
using DHQR.UI.Models.Base;
using DHQR.UI.Models;
using DHQR.UI.DHQRCommon;
using DHQR.DataAccess.Entities;

namespace DHQR.UI.Controllers
{
    /// <summary>
    /// 零售户信息
    /// </summary>
    public class RetailerController : BaseController
    {
        //
        // GET: /Retailer/


        /// <summary>
        /// 零售户维护主页
        /// </summary>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult Index()
        {
            RetailerModelService modelService = new RetailerModelService();
            ViewData["RecieveTypeList"] = modelService.GetRecieveTypeList(null);
            ViewData["RetailerStatusList"] = modelService.GetRetailerStatusList();
            return View();
        }


        /// <summary>
        /// 零售户编辑页面
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public ActionResult Edit(Guid Id)
        {
            RetailerModelService modelService = new RetailerModelService();
            RetailerModel model = modelService.GetByKey(Id);
            ViewData["RecieveTypeList"] = modelService.GetRecieveTypeList(model.RecieveType);
            return View(model);
        }

      
        /// <summary>
        /// 获取零售户信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult GetPageData(RetailerQueryParam param)
        {
            RetailerModelService modelService = new RetailerModelService();
            PagedResults<RetailerModel> result = modelService.GetRetailerPageData(param);
            return JsonForGrid(result);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult SaveEditData(RetailerModel model)
        {
            DoHandle dohandle;
            RetailerModelService modelService = new RetailerModelService();
            modelService.Update(model, out dohandle);
            return JsonForDoHandle(dohandle);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreModule)]
        public JsonResult ResetPsw(Guid Id)
        {
            DoHandle dohandle;
            RetailerModelService modelService = new RetailerModelService();
            modelService.ResetPsw(Id, out dohandle);
            return JsonForDoHandle(dohandle);
        }


    }
}
