using Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHQR.UI.Models;
using DHQR.UI.DHQRCommon;

namespace DHQR.UI.Controllers
{
    /// <summary>
    /// 文章类型
    /// </summary>
    public class WeiXinArticlesTypeController : WeiXinBaseController
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly WeiXinArticlesTypeModelService modelSer= new WeiXinArticlesTypeModelService();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public JsonResult GetPageData(QueryParam param)
        {
            var result = modelSer.GetPageData(param);
            return JsonForGrid(result);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public JsonResult Del(WeiXinArticlesTypeModel model)
        {
            DoHandle doHandle = new DoHandle();
            modelSer.Del(model, out doHandle);
            return JsonForDoHandle(doHandle);
        }

        #region 新增、编辑
        /// <summary>
        /// 新增或编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public ActionResult CreateOrEdit(Guid? id)
        {
            WeiXinArticlesTypeModel model = new WeiXinArticlesTypeModel();
            if (id.HasValue)
            {
                model = modelSer.GetByKey(id.Value);
            }
            return View(model);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        [ValidateInput(false)]
        public JsonResult Add(WeiXinArticlesTypeModel model)
        {
            DoHandle doHandle = new DoHandle();
            //model.WeiXinAppId = wechat_id;
            model.Creator = user_name;
            model.CreateTime = DateTime.Now;
            modelSer.Add(model, out doHandle);
            return JsonForDoHandle(doHandle);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [IgnoreModule(IgnoreType.IgnoreLogon)]
        public JsonResult Edit(WeiXinArticlesTypeModel model)
        {
            DoHandle doHandle = new DoHandle();
            modelSer.Edit(model, out doHandle);
            return JsonForDoHandle(doHandle);
        }
        #endregion
    }
}
