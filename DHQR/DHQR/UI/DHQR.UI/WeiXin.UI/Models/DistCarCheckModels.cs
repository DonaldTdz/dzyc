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

namespace DHQR.UI.Models
{

    /// <summary>
    /// 车辆检查模型
    /// </summary>
    public class DistCarCheckModel
    {
        #region Model
        private Guid _id;
        private string _check_id;
        private string _car_id;
        private string _ref_type;
        private string _ref_id;
        private string _abnormal_detail;
        private string _abnormal_type;
        private string _check_time;
        private decimal? _longitude;
        private decimal? _latitude;
        private string _check_type;
        private string _operate_mode;
        private string _note;
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 车辆检查流水号
        /// </summary>
        public string CHECK_ID
        {
            set { _check_id = value; }
            get { return _check_id; }
        }
        /// <summary>
        /// 送货车编码
        /// </summary>
        public string CAR_ID
        {
            set { _car_id = value; }
            get { return _car_id; }
        }
        /// <summary>
        /// 单据类型
        /// </summary>
        public string REF_TYPE
        {
            set { _ref_type = value; }
            get { return _ref_type; }
        }
        /// <summary>
        /// 单据编号
        /// </summary>
        public string REF_ID
        {
            set { _ref_id = value; }
            get { return _ref_id; }
        }
        /// <summary>
        /// 三检异常明细
        /// </summary>
        public string ABNORMAL_DETAIL
        {
            set { _abnormal_detail = value; }
            get { return _abnormal_detail; }
        }
        /// <summary>
        /// 三检异常类型
        /// </summary>
        public string ABNORMAL_TYPE
        {
            set { _abnormal_type = value; }
            get { return _abnormal_type; }
        }
        /// <summary>
        /// 检查时间
        /// </summary>
        public string CHECK_TIME
        {
            set { _check_time = value; }
            get { return _check_time; }
        }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal? LONGITUDE
        {
            set { _longitude = value; }
            get { return _longitude; }
        }
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal? LATITUDE
        {
            set { _latitude = value; }
            get { return _latitude; }
        }
        /// <summary>
        /// 检查类型
        /// </summary>
        public string CHECK_TYPE
        {
            set { _check_type = value; }
            get { return _check_type; }
        }
        /// <summary>
        /// 操作方式
        /// </summary>
        public string OPERATE_MODE
        {
            set { _operate_mode = value; }
            get { return _operate_mode; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string NOTE
        {
            set { _note = value; }
            get { return _note; }
        }
        #endregion Model

    }


    /// <summary>
    /// 车辆检查服务模型
    /// </summary>
    public class DistCarCheckModelService : BaseModelService<DistCarCheck, DistCarCheckModel>
    {
        private readonly DistCarCheckLogic BusinessLogic;

        public DistCarCheckModelService()
        {
            BusinessLogic = new DistCarCheckLogic();
        }

        protected override BaseLogic<DistCarCheck> BaseLogic
        {
            get { return BusinessLogic; }
        }

        /// <summary>
        /// 查询车辆检查信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<DistCarCheckResult> GetCarCheckResult(DistCarCheckQueryParam queryParam)
        {
            var result = BusinessLogic.GetCarCheckResult(queryParam);
            return result;
        }

    }
}