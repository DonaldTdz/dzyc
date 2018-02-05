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
    /// 配送任务模型
    /// </summary>
    public class LdmDistModel
    {
        #region Model
        private Guid _id;
        private string _dist_num;
        private string _rut_id;
        private string _rut_name;
        private string _dist_date;
        private string _dlvman_id;
        private string _dlvman_name;
        private string _driver_id;
        private string _driver_name;
        private string _car_id;
        private string _car_license;
        private int _dist_cust_sum;
        private decimal _qty_bar;
        private decimal _amt_sum;
        private string _status;
        private string _is_download;
        private string _is_mrb;
        private string _driver_card_id;
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
        /// 线路编码
        /// </summary>
        public string RUT_ID
        {
            set { _rut_id = value; }
            get { return _rut_id; }
        }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RUT_NAME
        {
            set { _rut_name = value; }
            get { return _rut_name; }
        }
        /// <summary>
        /// 送货日期
        /// </summary>
        public string DIST_DATE
        {
            set { _dist_date = value; }
            get { return _dist_date; }
        }
        /// <summary>
        /// 送货员编码
        /// </summary>
        public string DLVMAN_ID
        {
            set { _dlvman_id = value; }
            get { return _dlvman_id; }
        }
        /// <summary>
        /// 送货员名称
        /// </summary>
        public string DLVMAN_NAME
        {
            set { _dlvman_name = value; }
            get { return _dlvman_name; }
        }
        /// <summary>
        /// 驾驶员编码
        /// </summary>
        public string DRIVER_ID
        {
            set { _driver_id = value; }
            get { return _driver_id; }
        }
        /// <summary>
        /// 驾驶员名称
        /// </summary>
        public string DRIVER_NAME
        {
            set { _driver_name = value; }
            get { return _driver_name; }
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
        /// 送货车车牌号
        /// </summary>
        public string CAR_LICENSE
        {
            set { _car_license = value; }
            get { return _car_license; }
        }
        /// <summary>
        /// 客户数
        /// </summary>
        public int DIST_CUST_SUM
        {
            set { _dist_cust_sum = value; }
            get { return _dist_cust_sum; }
        }
        /// <summary>
        /// 送货量
        /// </summary>
        public decimal QTY_BAR
        {
            set { _qty_bar = value; }
            get { return _qty_bar; }
        }
        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal AMT_SUM
        {
            set { _amt_sum = value; }
            get { return _amt_sum; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public string STATUS
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 下载状态
        /// </summary>
        public string IS_DOWNLOAD
        {
            set { _is_download = value; }
            get { return _is_download; }
        }
        /// <summary>
        /// 是否使用
        /// </summary>
        public string IS_MRB
        {
            set { _is_mrb = value; }
            get { return _is_mrb; }
        }
        /// <summary>
        /// 送货员卡编码
        /// </summary>
        public string DRIVER_CARD_ID
        {
            set { _driver_card_id = value; }
            get { return _driver_card_id; }
        }
        #endregion Model
    }


    /// <summary>
    /// 配送任务服务模型
    /// </summary>
    public class LdmDistModelService : BaseModelService<LdmDist, LdmDistModel>
    {
        private readonly LdmDistLogic BusinessLogic;

        public LdmDistModelService()
        {
            BusinessLogic = new LdmDistLogic();
        }

        protected override BaseLogic<LdmDist> BaseLogic
        {
            get { return BusinessLogic; }
        }


        #region 配送任务完成率

        /// <summary>
        /// 查询配送任务完成率
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public ColumnRange GetLdmDistFinishRate(LdmFinishQueryParam queryParam)
        {
            var datas = BusinessLogic.GetLdmDistFinishRate(queryParam);

            IList<string> carName = datas.Select(f => f.CAR_NAME).ToList();

            IList<decimal> decimalDatas = datas.Select(f => f.FinishRate).ToList();

            ColumnRange result = new ColumnRange { xAxis = carName, data = decimalDatas };

            return result;
        }

        #endregion

        /// <summary>
        /// 查询配送单信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public PagedResults<LdmDistModel> QueryLdmDist(LdmDistQueryParam queryParam)
        {
            //queryParam.DIST_DATE = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
            var distDate = DateTime.Parse(queryParam.DIST_DATE);
            queryParam.DIST_DATE = distDate.AddDays(-1).ToString("yyyyMMdd");
            var datas = BusinessLogic.QueryLdmDist(queryParam);
            var pagedInfo = datas.PagerInfo;
            var dt = datas.Data.Select(f => ConvertToModel(f)).ToList();
            var result = new PagedResults<LdmDistModel> { Data = dt, PagerInfo = pagedInfo };
            return result;

        }

        /// <summary>
        /// 查询配送单信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public PagedResults<LdmDistModel> QueryLdmDistPc(LdmDistQueryParam queryParam)
        {
            queryParam.DIST_DATE = queryParam.DistDate.AddDays(-1).ToString("yyyyMMdd");
            var datas = BusinessLogic.QueryLdmDist(queryParam);
            var pagedInfo = datas.PagerInfo;
            var dt = datas.Data.Select(f => ConvertToModel(f)).ToList();
            var result = new PagedResults<LdmDistModel> { Data = dt, PagerInfo = pagedInfo };
            return result;

        }

        /// <summary>
        /// 当日配送单信息
        /// </summary>
        /// <param name="distCount"></param>
        /// <param name="finishCount"></param>
        /// <param name="notfinishCount"></param>
        /// <param name="totalMoney"></param>
        public void GetLdmDistInfo(out int distCount, out int finishCount, out int notfinishCount, out decimal totalMoney)
        {
            BusinessLogic.GetLdmDistInfo(out distCount, out finishCount, out notfinishCount, out totalMoney);
        }

        /// <summary>
        /// 手工到货确认
        /// </summary>
        /// <param name="CO_NUM"></param>
        /// <param name="dohandle"></param>
        public void HandleConfirm(string CO_NUM,string DIST_NUM, out DoHandle dohandle)
        {
            DistCustLogic logic = new DistCustLogic();
            logic.HandleConfirm(CO_NUM, DIST_NUM, out dohandle);
        }

        
    }
}