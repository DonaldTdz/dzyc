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
    /// 订单模型
    /// </summary>
    public class LdmDistLineModel
    {
        #region Model
        private Guid _id;
        private string _dist_num;
        private string _co_num;
        private string _cust_id;
        private string _cust_code;
        private string _cust_name;
        private string _manager;
        private string _addr;
        private string _tel;
        private decimal _qty_bar;
        private decimal _amt_ar;
        private decimal _amt_or;
        private string _pmt_status;
        private string _type;
        private int _seq;
        private string _license_code;
        private string _pay_type;
        private decimal? _longitude;
        private decimal? _latitude;
        private string _cust_card_id;
        private string _cust_card_code;
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
        /// 客户内码
        /// </summary>
        public string CUST_ID
        {
            set { _cust_id = value; }
            get { return _cust_id; }
        }
        /// <summary>
        /// 客户编码
        /// </summary>
        public string CUST_CODE
        {
            set { _cust_code = value; }
            get { return _cust_code; }
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CUST_NAME
        {
            set { _cust_name = value; }
            get { return _cust_name; }
        }
        /// <summary>
        /// 负责人
        /// </summary>
        public string MANAGER
        {
            set { _manager = value; }
            get { return _manager; }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string ADDR
        {
            set { _addr = value; }
            get { return _addr; }
        }
        /// <summary>
        /// 电话
        /// </summary>
        public string TEL
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal QTY_BAR
        {
            set { _qty_bar = value; }
            get { return _qty_bar; }
        }
        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal AMT_AR
        {
            set { _amt_ar = value; }
            get { return _amt_ar; }
        }
        /// <summary>
        /// 已收金额
        /// </summary>
        public decimal AMT_OR
        {
            set { _amt_or = value; }
            get { return _amt_or; }
        }
        /// <summary>
        /// 付款状态
        /// </summary>
        public string PMT_STATUS
        {
            set { _pmt_status = value; }
            get { return _pmt_status; }
        }
        /// <summary>
        /// 订单类型
        /// </summary>
        public string TYPE
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 送货顺序
        /// </summary>
        public int SEQ
        {
            set { _seq = value; }
            get { return _seq; }
        }
        /// <summary>
        /// 许可证专卖号
        /// </summary>
        public string LICENSE_CODE
        {
            set { _license_code = value; }
            get { return _license_code; }
        }
        /// <summary>
        /// 结算方式
        /// </summary>
        public string PAY_TYPE
        {
            set { _pay_type = value; }
            get { return _pay_type; }
        }
        /// <summary>
        /// 位置经度
        /// </summary>
        public decimal? LONGITUDE
        {
            set { _longitude = value; }
            get { return _longitude; }
        }
        /// <summary>
        /// 位置纬度
        /// </summary>
        public decimal? LATITUDE
        {
            set { _latitude = value; }
            get { return _latitude; }
        }
        /// <summary>
        /// 零售户物理卡全球唯一码
        /// </summary>
        public string CUST_CARD_ID
        {
            set { _cust_card_id = value; }
            get { return _cust_card_id; }
        }
        /// <summary>
        /// 零售户物理卡编号
        /// </summary>
        public string CUST_CARD_CODE
        {
            set { _cust_card_code = value; }
            get { return _cust_card_code; }
        }


        /// <summary>
        /// 零售户纬度
        /// </summary>
        public decimal CustLongitude { get; set; }

        /// <summary>
        /// 零售户经度
        /// </summary>
        public decimal CustLatitude { get; set; }

        /// <summary>
        /// 是否到货确认
        /// </summary>
        public bool HasConfirm { get; set; }

        /// <summary>
        /// 收货状态
        /// </summary>
        public string REC_STATE { get; set; }

        /// <summary>
        /// 刷卡时间
        /// </summary>
        public string REC_CIG_TIME { get; set; }

        /// <summary>
        /// 是否暂存订单
        /// </summary>
        public bool IsTemp { get; set; }


        #endregion Model

    }

    /// <summary>
    /// 订单服务模型
    /// </summary>
    public class LdmDistLineModelService : BaseModelService<LdmDistLine, LdmDistLineModel>
    {
        private readonly LdmDistLineLogic BusinessLogic;

        public LdmDistLineModelService()
        {
            BusinessLogic = new LdmDistLineLogic();
        }

        protected override BaseLogic<LdmDistLine> BaseLogic
        {
            get { return BusinessLogic; }
        }

        
         /// <summary>
        /// 根据条件获取线路明细信息
         /// </summary>
         /// <param name="queryParam"></param>
         /// <returns></returns>
        public IList<LdmDistLineModel> QueryDeliveryLine(LdmDistLineQueryParam queryParam)
        {
            LdmDistLogic distLogic=new LdmDistLogic();
            var realTime = DateTime.Parse(queryParam.DIST_DATE).AddDays(-1).ToString("yyyyMMdd");
            
            //根据日期和线路找出配送单
            var ldmDist = distLogic.GetDistByRutAndDate(queryParam.RUT_ID, realTime);
            queryParam.DIST_NUM = ldmDist == null ? "" : ldmDist.DIST_NUM;
            var result = BusinessLogic.QueryDeliveryLine(queryParam).Select(f => ConvertToModel(f)).OrderBy(f=>f.SEQ).ToList();
            return result;
        }

         

        /// <summary>
        /// 微信订单查询
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public PagedResults<LdmDistLineModel> QueryOrders(LdmDistLineQueryParam queryParam)
        {
            var realDate = DateTime.Parse(queryParam.DIST_DATE);
            var realDateStr = realDate.AddDays(-1).ToString("yyyyMMdd");

            queryParam.DIST_DATE = realDateStr;
            var datas = BusinessLogic.QueryOrders(queryParam);
            var pagedInfo = datas.PagerInfo;
            var dt = datas.Data.Select(f => ConvertToModel(f)).ToList();
            var result = new PagedResults<LdmDistLineModel> { Data = dt, PagerInfo = pagedInfo };
            return result;
        }

        #region 订单查询

        /// <summary>
        /// 查询订单信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public PagedResults<LdmDistLineModel> QueryLdmdistLine(LdmDistLineQueryParam queryParam)
        {
            var datas = BusinessLogic.QueryLdmdistLine(queryParam);
            var pagedInfo = datas.PagerInfo;
            var dt = datas.Data.Select(f => ConvertToModel(f)).ToList();
            var result = new PagedResults<LdmDistLineModel> { Data = dt, PagerInfo = pagedInfo };
            return result;

        }

        #endregion

    }

}