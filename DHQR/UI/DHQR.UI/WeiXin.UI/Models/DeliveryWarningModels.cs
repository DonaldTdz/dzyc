using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.UI.Model;
using DHQR.BusinessLogic.Implement;
using Common.BLL.Implement;
using Common.Base;
using DHQR.DataAccess.Entities;
using System.Web.Mvc;
using System.Runtime.Serialization;

namespace DHQR.UI.Models
{
    /// <summary>
    /// 配送点预警模型
    /// </summary>
    public class DeliveryWarningModel
    {

        #region Model
        private Guid _id;
        private string _dist_num;
        private string _co_num;
        private decimal? _longitude;
        private decimal? _latitude;
        private decimal? _reallongitude;
        private decimal? _reallatitude;
        private DateTime _deliverytime;
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 配送单编号
        /// </summary>
        public string DIST_NUM
        {
            set { _dist_num = value; }
            get { return _dist_num; }
        }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string CO_NUM
        {
            set { _co_num = value; }
            get { return _co_num; }
        }
        /// <summary>
        /// 原始经度
        /// </summary>
        public decimal? Longitude
        {
            set { _longitude = value; }
            get { return _longitude; }
        }
        /// <summary>
        /// 原始纬度
        /// </summary>
        public decimal? Latitude
        {
            set { _latitude = value; }
            get { return _latitude; }
        }
        /// <summary>
        /// 实际纬度
        /// </summary>
        public decimal? RealLongitude
        {
            set { _reallongitude = value; }
            get { return _reallongitude; }
        }
        /// <summary>
        /// 实际经度
        /// </summary>
        public decimal? RealLatitude
        {
            set { _reallatitude = value; }
            get { return _reallatitude; }
        }
        /// <summary>
        /// 签收时间
        /// </summary>
        public DateTime DeliveryTime
        {
            set { _deliverytime = value; }
            get { return _deliverytime; }
        }

        #endregion Model


        #region 扩展

        /// <summary>
        /// 线路ID
        /// </summary>
        [DataMember]
        public string RUT_ID { get; set; }

        /// <summary>
        /// 线路名称
        /// </summary>
        [DataMember]
        public string RUT_NAME { get; set; }

        /// <summary>
        /// 配送员ID
        /// </summary>
        [DataMember]
        public string DLVMAN_ID { get; set; }

        /// <summary>
        /// 配送员名称
        /// </summary>
        [DataMember]
        public string DLVMAN_NAME { get; set; }

        /// <summary>
        /// 配送车ID
        /// </summary>
        [DataMember]
        public string CAR_ID { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [DataMember]
        public string CAR_LICENSE { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        [DataMember]
        public string CUST_ID { get; set; }

        /// <summary>
        /// 客户代码
        /// </summary>
        [DataMember]
        public string CUST_CODE { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        [DataMember]
        public string CUST_NAME { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        [DataMember]
        public string MANAGER { get; set; }

        /// <summary>
        /// 客户经营地址
        /// </summary>
        [DataMember]
        public string ADDR { get; set; }

        /// <summary>
        /// 客户电话
        /// </summary>
        [DataMember]
        public string TEL { get; set; }

        /// <summary>
        /// 原因
        /// </summary>
        [DataMember]
        public string Reason { get; set; }


        #endregion
    }

    /// <summary>
    /// 配送点预警服务模型
    /// </summary>
    public class DeliveryWarningModelService : BaseModelService<DeliveryWarning, DeliveryWarningModel>
    {
         private readonly DeliveryWarningLogic BusinessLogic;

         public DeliveryWarningModelService()
        {
            BusinessLogic = new DeliveryWarningLogic();
        }

         protected override BaseLogic<DeliveryWarning> BaseLogic
        {
            get { return BusinessLogic; }
        }

        /// <summary>
        /// 获取配送点预警信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
         public IList<DeliveryWarningModel> GetWarningDatas(DeliveryWarningQueryParam queryParam)
         {
             var result = BusinessLogic.GetWarningDatas(queryParam).Select(f=>ConvertToModel(f)).ToList();
             return result;
         }
    }
}