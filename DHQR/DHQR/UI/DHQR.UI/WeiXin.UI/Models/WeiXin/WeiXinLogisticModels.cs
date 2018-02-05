using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.BLL.Implement;
using Common.UI.Model;
using DHQR.BusinessLogic.Implement;
using DHQR.DataAccess.Entities;
using System.Web.Mvc;
using Common.Base;


namespace DHQR.UI.Models
{
    /// <summary>
    /// 物流信息模型
    /// </summary>
    public class WeiXinLogisticModel
    {
        #region 基元属性

        public System.Guid Id { get; set; }
        public string UniCode { get; set; }
        public string StateDsc { get; set; }
        public string Sender { get; set; }
        public string SenderTel { get; set; }
        public DateTime ArriveTime { get; set; }

        #endregion


        #region 扩展字段

        public string Code { get; set; }
        public int Sequence { get; set; }
        public string RetailerCode { get; set; }
        public string RetailerName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime SortDate { get; set; }
        public int AllNum { get; set; }
        public string DlCode { get; set; }
        public string DlName { get; set; }
        public string State { get; set; }
        public string ArriveTimeStr 
        {
            get 
            {
                return this.ArriveTime.ToString("yyyy-MM-dd hh:mm:ss");
            }
        }

        #endregion
    }

    /// <summary>
    /// 物流信息服务模型
    /// </summary>
    public class WeiXinLogisticModelService : BaseModelService<WeiXinLogistic, WeiXinLogisticModel>
    {
        private readonly WeiXinLogisticLogic BusinessLogic;
         public WeiXinLogisticModelService()
        {
            BusinessLogic = new WeiXinLogisticLogic();
        }

         protected override BaseLogic<WeiXinLogistic> BaseLogic
        {
            get { return BusinessLogic; }
        }


        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
         public override WeiXinLogisticModel ConvertToModel(WeiXinLogistic entity)
         {
             //var model = base.ConvertToModel(entity);
             //var order = new WeiXinOrderLogic().GetByUniCode(entity.UniCode);
             //model.Code = order.Code;
             //model.Sequence = order.Sequence;
             //model.RetailerCode = order.RetailerCode;
             //model.RetailerName = order.RetailerName;
             //model.OrderDate = order.OrderDate;
             //model.SortDate = order.SortDate;
             //model.AllNum = order.AllNum;
             //model.DlCode = order.DlCode;
             //model.DlName = order.DlName;
             //model.State = order.State;
             //return model; 
             return null;
         }
        
        /// <summary>
        /// 根据订单唯一码获取
        /// </summary>
        /// <param name="Unicode"></param>
        /// <returns></returns>
         public WeiXinLogisticModel GetByUniCode(string Unicode)
         {
             var data =ConvertToModel( BusinessLogic.GetByUniCode(Unicode));
             return data;
         }
    }
}