using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.DAL.Entities;
using Common.UI.Model;
using DHQR.UI.Models;
using Common.Base;

namespace DHQR.UI.Controllers
{
        //
        // GET: /BaseUi/
        public abstract class BaseUiController<TModel, TEntity> : BaseController
            where TModel : class
            where TEntity : class,IEntityKey
        {
            /// <summary>
            /// 派生类中需要实现的
            /// </summary>
            protected abstract BaseModelService<TEntity, TModel> Service { get; }
            /// <summary>
            /// 创建
            /// </summary>
            /// <param name="model"></param>
            /// <returns></returns>
            public virtual JsonResult Add(TModel model)
            {
                if (model is IRecordModel)
                {
                    var modelMd = (IRecordModel)model;
                    modelMd.Creator = GetLogonName();
                    modelMd.CreateTime = DateTime.Now;
                    modelMd.Editor = "";
                    modelMd.EditTime = DateTime.Now;
                }

                DoHandle doHandle;
                Service.Add(model, out doHandle);
                return JsonForDoHandle(doHandle);
            }
            /// <summary>
            /// 获取查询页面数据信息
            /// </summary>
            /// <param name="queryParam"></param>
            /// <returns></returns>
            public virtual JsonResult GetPageData(QueryParam queryParam)
            {
                PagedResults<TModel> data = Service.GetPageData(queryParam);
                return JsonForGrid(data);
            }


            /// <summary>
            /// 修改
            /// </summary>
            /// <param name="model"></param>
            /// <returns></returns>
            public virtual JsonResult Edit(TModel model)
            {
                DoHandle doHandle;
                if (model is IRecordModel)
                {
                    var modelMd = (IRecordModel)model;
                    modelMd.Editor = GetLogonName();
                    modelMd.EditTime = DateTime.Now;
                }

                Service.Edit(model, out doHandle);
                return JsonForDoHandle(doHandle);
            }


            /// <summary>
            /// 创建
            /// </summary>
            /// <param name="model"></param>
            /// <returns></returns>
            public JsonResult Del(TModel model)
            {
                DoHandle doHandle;
                Service.Del(model, out doHandle);
                return JsonForDoHandle(doHandle);
            }


        }

}
