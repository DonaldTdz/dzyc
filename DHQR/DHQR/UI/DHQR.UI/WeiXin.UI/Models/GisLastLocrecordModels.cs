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
    /// 配送员位置信息模型
    /// </summary>
    public class GisLastLocrecordModel
    {
        #region Model
        private Guid _id;
        private decimal _gisid;
        private string _m_code;
        private string _m_type;
        private decimal _original_longitude;
        private decimal _original_latitude;
        private decimal _speed;
        private decimal _direction;
        private decimal _height;
        private int _statllite_num;
        private string _gpstime;
        private string _inputdate;
        private string _state;
        private DateTime _createtime;
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal GisID
        {
            set { _gisid = value; }
            get { return _gisid; }
        }
        /// <summary>
        /// 移动编码
        /// </summary>
        public string M_CODE
        {
            set { _m_code = value; }
            get { return _m_code; }
        }
        /// <summary>
        /// 移动类型
        /// </summary>
        public string M_TYPE
        {
            set { _m_type = value; }
            get { return _m_type; }
        }
        /// <summary>
        /// 原始经度
        /// </summary>
        public decimal ORIGINAL_LONGITUDE
        {
            set { _original_longitude = value; }
            get { return _original_longitude; }
        }
        /// <summary>
        /// 原始纬度
        /// </summary>
        public decimal ORIGINAL_LATITUDE
        {
            set { _original_latitude = value; }
            get { return _original_latitude; }
        }
        /// <summary>
        /// 速度
        /// </summary>
        public decimal SPEED
        {
            set { _speed = value; }
            get { return _speed; }
        }
        /// <summary>
        /// 方向
        /// </summary>
        public decimal DIRECTION
        {
            set { _direction = value; }
            get { return _direction; }
        }
        /// <summary>
        /// 高度
        /// </summary>
        public decimal HEIGHT
        {
            set { _height = value; }
            get { return _height; }
        }
        /// <summary>
        /// 卫星数
        /// </summary>
        public int STATLLITE_NUM
        {
            set { _statllite_num = value; }
            get { return _statllite_num; }
        }
        /// <summary>
        /// 卫星时间
        /// </summary>
        public string GPSTIME
        {
            set { _gpstime = value; }
            get { return _gpstime; }
        }
        /// <summary>
        /// 写入时间
        /// </summary>
        public string INPUTDATE
        {
            set { _inputdate = value; }
            get { return _inputdate; }
        }
        /// <summary>
        /// 定位状态
        /// </summary>
        public string STATE
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        #endregion Model

        #region 扩展字段

        /// <summary>
        /// 满意度描述
        /// </summary>
        public string CAR_NAME
        {
            get;
            set;
        }


        #endregion
    }

    /// <summary>
    /// 配送员位置信息服务模型
    /// </summary>
    public class GisLastLocrecordModelService : BaseModelService<GisLastLocrecord, GisLastLocrecordModel>
    {
        private readonly GisLastLocrecordLogic BusinessLogic;

         public GisLastLocrecordModelService()
        {
            BusinessLogic = new GisLastLocrecordLogic();
        }

         protected override BaseLogic<GisLastLocrecord> BaseLogic
        {
            get { return BusinessLogic; }
        }


        /// <summary>
        /// 获取实时定位信息
        /// </summary>
        /// <returns></returns>
         public IList<GisLastLocrecordModel> GetLatestGisInfos()
         {
             var result = BusinessLogic.GetLatestGisInfos().Select(f => ConvertToModel(f)).ToList();
             return result;
         }

        
        /// <summary>
        /// 查询配送员线路坐标
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
         public IList<GisLastLocrecordModel> GetGisInfosOfDlvman(GisLastLocrecordQueryParam queryParam)
         {
             var result = BusinessLogic.GetGisInfosOfDlvman(queryParam).Select(f => ConvertToModel(f))
                 .OrderBy(k=>k.CreateTime).ToList();
             return result;
         }
    }
}