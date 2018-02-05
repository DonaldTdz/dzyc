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
    /// 到货确认模型
    /// </summary>
    public class DistCustModel
    {
        #region Model
        private Guid _id;
        private string _dist_num;
        private string _co_num;
        private string _cust_id;
        private string _is_received;
        private string _dist_satis;
        private string _unload_reason;
        private string _rec_date;
        private string _rec_arrive_time;
        private string _rec_leave_time;
        private decimal _handover_time;
        private string _notsatis_reason;
        private string _unusual_type;
        private string _evaluate_info;
        private string _sign_type;
        private string _exp_sign_reason;
        private decimal? _unload_lon;
        private decimal? _unload_lat;
        private decimal? _unload_distance;
        private string _evaluate_time;
        private string _dlvman_id;
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
        /// 收货方式
        /// </summary>
        public string IS_RECEIVED
        {
            set { _is_received = value; }
            get { return _is_received; }
        }
        /// <summary>
        /// 送货满意度
        /// </summary>
        public string DIST_SATIS
        {
            set { _dist_satis = value; }
            get { return _dist_satis; }
        }
        /// <summary>
        /// 收货状态
        /// </summary>
        public string UNLOAD_REASON
        {
            set { _unload_reason = value; }
            get { return _unload_reason; }
        }
        /// <summary>
        /// 实际送达日期
        /// </summary>
        public string REC_DATE
        {
            set { _rec_date = value; }
            get { return _rec_date; }
        }
        /// <summary>
        /// 实际到达时间
        /// </summary>
        public string REC_ARRIVE_TIME
        {
            set { _rec_arrive_time = value; }
            get { return _rec_arrive_time; }
        }
        /// <summary>
        /// 实际离开时间
        /// </summary>
        public string REC_LEAVE_TIME
        {
            set { _rec_leave_time = value; }
            get { return _rec_leave_time; }
        }
        /// <summary>
        /// 交接时间
        /// </summary>
        public decimal HANDOVER_TIME
        {
            set { _handover_time = value; }
            get { return _handover_time; }
        }
        /// <summary>
        /// 不满意原因
        /// </summary>
        public string NOTSATIS_REASON
        {
            set { _notsatis_reason = value; }
            get { return _notsatis_reason; }
        }
        /// <summary>
        /// 异常处理方式
        /// </summary>
        public string UNUSUAL_TYPE
        {
            set { _unusual_type = value; }
            get { return _unusual_type; }
        }
        /// <summary>
        /// 送货评价
        /// </summary>
        public string EVALUATE_INFO
        {
            set { _evaluate_info = value; }
            get { return _evaluate_info; }
        }
        /// <summary>
        /// 签到方式
        /// </summary>
        public string SIGN_TYPE
        {
            set { _sign_type = value; }
            get { return _sign_type; }
        }
        /// <summary>
        /// 异常签到原因
        /// </summary>
        public string EXP_SIGN_REASON
        {
            set { _exp_sign_reason = value; }
            get { return _exp_sign_reason; }
        }
        /// <summary>
        /// 卸货经度
        /// </summary>
        public decimal? UNLOAD_LON
        {
            set { _unload_lon = value; }
            get { return _unload_lon; }
        }
        /// <summary>
        /// 卸货纬度
        /// </summary>
        public decimal? UNLOAD_LAT
        {
            set { _unload_lat = value; }
            get { return _unload_lat; }
        }
        /// <summary>
        /// 卸货距离
        /// </summary>
        public decimal? UNLOAD_DISTANCE
        {
            set { _unload_distance = value; }
            get { return _unload_distance; }
        }
        /// <summary>
        /// 评价时间
        /// </summary>
        public string EVALUATE_TIME
        {
            set { _evaluate_time = value; }
            get { return _evaluate_time; }
        }
        /// <summary>
        /// 送货员编码
        /// </summary>
        public string DLVMAN_ID
        {
            set { _dlvman_id = value; }
            get { return _dlvman_id; }
        }
        #endregion Model

        #region 扩展

        /// <summary>
        /// 满意度描述
        /// </summary>
        public string DIST_SATIS_DCR
        {
            get;
            set;
        }


        #endregion

    }

    /// <summary>
    /// 满意度模型
    /// </summary>
    public class SatisfyModel
    {

        /// <summary>
        /// 车辆名称（线路+车牌）
        /// </summary>
        public string CAR_NAME
        {
            set;
            get;
        }

        /// <summary>
        /// 配送员工号
        /// </summary>
        public string DLVMAN_ID
        {
            set;
            get;
        }

        /// <summary>
        /// 满意度
        /// </summary>
        public string SatisfyValue
        {
            set;
            get;
        }



        /// <summary>
        /// 满意度描述
        /// </summary>
        public string SatisfyName
        {
            set;
            get;
        }

        /// <summary>
        /// 满意度占比
        /// </summary>
        public decimal SatisfyRate
        {
            set;
            get;
        }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDate
        {
            set;
            get;
        }


        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate
        {
            set;
            get;
        }


    }

    /// <summary>
    /// 到货确认服务模型
    /// </summary>
    public class DistCustModelService : BaseModelService<DistCust, DistCustModel>
    {
        private readonly DistCustLogic BusinessLogic;

         public DistCustModelService()
        {
            BusinessLogic = new DistCustLogic();
        }

         protected override BaseLogic<DistCust> BaseLogic
        {
            get { return BusinessLogic; }
        }


        #region 满意度分析

        /// <summary>
         /// 获取单个配送员每天满意度信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
         public IList<PieModel> GetSatisfyByDayData(DistCustByDayQueryParam queryParam)
         {
             IList<PieModel> result = new List<PieModel>();
             var datas = BusinessLogic.GetSatisfyByDayData(queryParam).Select(f=>ConvertToModel(f)).ToList();
             var groupedDatas = datas.GroupBy(f => f.DIST_SATIS_DCR).Distinct().ToList();
             foreach (var item in groupedDatas)
             {
                 PieModel model = new PieModel
                 {
                     name = item.Key,
                     data = item.Count(),
                     
                 };
                 result.Add(model);
             }
             return result;
         }


        /// <summary>
        /// 获取时间段内满意度指标【按柱状图展示各个线路信息】
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
         public ReportMaster GetSatisfyOfRete(SatisfyQueryParam queryParam)
         {
             IList<ReportDetail> data = new List<ReportDetail>();
             var distDatas = BusinessLogic.GetSatisfyDatas(queryParam);
             var saytisDatas = ConverToSatisfyModels(distDatas, queryParam);
             IList<string> xAxis = new List<string> { queryParam.SatisfyName };

             foreach (var item in saytisDatas)
             {
                 IList<decimal> itemData = new List<decimal>();
                 itemData.Add(item.SatisfyRate);
                 ReportDetail orderedData = new ReportDetail { name = string.IsNullOrEmpty(item.CAR_NAME) ? "无" : item.CAR_NAME, decimalData = itemData };
                 data.Add(orderedData);
             }

             ReportMaster dayOrderInfo = new ReportMaster { xAxis = xAxis, data = data };
             return dayOrderInfo;
         }


         /// <summary>
         /// 转换成满意度指标模型
         /// </summary>
         /// <param name="distCusts"></param>
         /// <param name="queryParam"></param>
         /// <returns></returns>
         public IList<SatisfyModel> ConverToSatisfyModels(IList<DistCust> distCusts, SatisfyQueryParam queryParam)
         {
             IList<SatisfyModel> result=new List<SatisfyModel>();
             var groupedDatas = distCusts.GroupBy(f => f.DLVMAN_ID);
             IList<string> dlvmanIds = groupedDatas.Select(f => f.Key).ToList();
             DistDlvmanLogic manLogic = new DistDlvmanLogic();
             var dlvmen = manLogic.GetAll().Where(f => dlvmanIds.Contains(f.POSITION_CODE)).ToList();
             foreach (var item in groupedDatas)
             {
                 var dlvman = dlvmen.FirstOrDefault(f => f.POSITION_CODE == item.Key);
                 var currentSaydatas = item.Where(f => f.DIST_SATIS == queryParam.SatisfyValue).ToList();
                 var itemCount=item.Count();
                 SatisfyModel m = new SatisfyModel 
                 {
                     StartDate=queryParam.StartDate,
                     EndDate=queryParam.EndDate,
                     DLVMAN_ID=item.Key,
                     CAR_NAME = dlvman == null ? "" : dlvman.USER_NAME,
                     SatisfyValue=queryParam.SatisfyValue,
                     SatisfyRate=itemCount==0?0: (decimal.Round(((decimal)currentSaydatas.Count/(decimal)itemCount),4))*100
                 };
                 switch (m.SatisfyValue)
                 {
                     case "10": m.SatisfyName= "非常满意";
                         break;
                     case "11": m.SatisfyName = "满意";
                         break;
                     case "12": m.SatisfyName = "一般";
                         break;
                     case "13": m.SatisfyName = "不满意";
                         break;
                     case "14": m.SatisfyName = "非常不满意";
                         break;
                     default: m.SatisfyName = "未知";
                         break;
                 }
                 result.Add(m);
             }
             return result;
         }


         /// <summary>
         /// 获取满意度指标列表
         /// </summary>
         /// <returns></returns>
         public IList<SelectListItem> GetSatisfyList()
         {
             IList<SelectListItem> selectList = new List<SelectListItem>();
             BaseEnumLogic enumLogic = new BaseEnumLogic();
             var datas = enumLogic.GetByType("Satisfy").OrderBy(f=>f.Value).ToList();
             foreach (var item in datas)
             {
                 selectList.Add(new SelectListItem
                 {
                     Text = item.ValueNote,
                     Value = item.Value
                 });
             }
             return selectList;

         }

         /// <summary>
         /// 获取不满意原因
         /// </summary>
         /// <param name="queryParam"></param>
         /// <returns></returns>
         public IList<PieModel> GetNotSatisReasons(SatisfyQueryParam queryParam)
         {
             IList<PieModel> result = new List<PieModel>();
             var datas = BusinessLogic.GetNotSatisReasons(queryParam);
             foreach (var item in datas)
             {
                 PieModel model = new PieModel
                 {
                     name = item.ReasonName,
                     data = item.Count

                 };
                 result.Add(model);
             }
             return result;

         }
         

        /// <summary>
        /// 查询满意度信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
         public IList<SatisfactionModel> QuerySatisfaction(SatisfyQueryParam queryParam)
         {
             return BusinessLogic.QuerySatisfaction(queryParam);
         }


        #endregion

        #region 时间点查询

        /// <summary>
         /// 时间点查询
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
         public IList<TimeRecordDetail> QueryTimeRecord(TimeRecordQueryParam queryParam)
         {
             DistRecordLogLogic logLogic = new DistRecordLogLogic();
             var result = logLogic.QueryTimeRecord(queryParam);
             return result;
         }


         #region 手动出入库

         /// <summary>
         /// 手动出库
         /// </summary>
         /// <param name="CO_NUM"></param>
         /// <param name="dohandle"></param>
         public void HandleCarOut(string DLVMAN_ID, string LOG_DATE, out DoHandle dohandle)
         {
             DistRecordLogLogic logic = new DistRecordLogLogic();
             logic.HandleCarOut(DLVMAN_ID, LOG_DATE, out dohandle);
         }

         /// <summary>
         /// 手动出库
         /// </summary>
         /// <param name="CO_NUM"></param>
         /// <param name="dohandle"></param>
         public void HandleCarIn(string DLVMAN_ID, string LOG_DATE, out DoHandle dohandle)
         {
             DistRecordLogLogic logic = new DistRecordLogLogic();
             logic.HandleCarIn(DLVMAN_ID, LOG_DATE, out dohandle);
         }

         #endregion
        #endregion
    }

}