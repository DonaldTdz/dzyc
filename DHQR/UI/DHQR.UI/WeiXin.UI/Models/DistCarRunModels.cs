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
    /// 车辆运行模型
    /// </summary>
    public class DistCarRunModel
    {
        #region Model
        private Guid _id;
        private string _info_num;
        private string _ref_type;
        private string _ref_id;
        private string _car_id;
        private string _dlvman_id;
        private string _crt_date;
        private decimal? _amt_sum;
        private decimal? _pre_mil;
        private decimal? _this_mil;
        private decimal? _act_mil;
        private string _note;
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
        /// 流水号
        /// </summary>
        public string INFO_NUM
        {
            set { _info_num = value; }
            get { return _info_num; }
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
        /// 配送单编号
        /// </summary>
        public string REF_ID
        {
            set { _ref_id = value; }
            get { return _ref_id; }
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
        /// 送货员编码
        /// </summary>
        public string DLVMAN_ID
        {
            set { _dlvman_id = value; }
            get { return _dlvman_id; }
        }
        /// <summary>
        /// 录入时间
        /// </summary>
        public string CRT_DATE
        {
            set { _crt_date = value; }
            get { return _crt_date; }
        }
        /// <summary>
        /// 费用总和
        /// </summary>
        public decimal? AMT_SUM
        {
            set { _amt_sum = value; }
            get { return _amt_sum; }
        }
        /// <summary>
        /// 上期里程数
        /// </summary>
        public decimal? PRE_MIL
        {
            set { _pre_mil = value; }
            get { return _pre_mil; }
        }
        /// <summary>
        /// 本期里程数
        /// </summary>
        public decimal? THIS_MIL
        {
            set { _this_mil = value; }
            get { return _this_mil; }
        }
        /// <summary>
        /// 行驶里程数
        /// </summary>
        public decimal? ACT_MIL
        {
            set { _act_mil = value; }
            get { return _act_mil; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string NOTE
        {
            set { _note = value; }
            get { return _note; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        #endregion Model

        #region 扩展

        /// <summary>
        /// 车辆名称
        /// </summary>
        public string CAR_NAME
        {
            get;
            set;
        }

        /// <summary>
        /// 燃油费
        /// </summary>
        public decimal FUEL_MONEY
        {
            get;
            set;
        }
        /// <summary>
        /// 过路费
        /// </summary>
        public decimal ROAD_MONEY
        {
            get;
            set;
        }
        /// <summary>
        /// 其他费
        /// </summary>
        public decimal OTHER_MONRY
        {
            get;
            set;
        }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string LICENSE_CODE
        {
            get;
            set;
        }

        /// <summary>
        /// 送货员
        /// </summary>
        public string DLVMAN_NAME
        {
            get;
            set;
        }

        /// <summary>
        /// 驾驶员
        /// </summary>
        public string DRIVER_NAME
        {
            get;
            set;
        }

        /// <summary>
        /// 采集时间
        /// </summary>
        public string CreateTimeStr
        {
            get;
            set;
        }

        #endregion
    }

    /// <summary>
    /// 车辆运行服务模型
    /// </summary>
    public class DistCarRunModelService : BaseModelService<DistCarRun, DistCarRunModel>
    {
        private readonly DistCarRunLogic BusinessLogic;

        public DistCarRunModelService()
        {
            BusinessLogic = new DistCarRunLogic();
        }

        protected override BaseLogic<DistCarRun> BaseLogic
        {
            get { return BusinessLogic; }
        }

        #region 年度和月份

        /// <summary>
        /// 获取年度列表
        /// </summary>
        /// <returns></returns>
        public IList<SelectListItem> GetYearList()
        {
            IList<SelectListItem> selectList = new List<SelectListItem>();

            var yearNow = DateTime.Now.Year;
            for (var i = yearNow; i >= 2010; i--) 
             {
                 SelectListItem item = new SelectListItem 
                 {
                     Text=i.ToString(),
                     Value=i.ToString()
                 };
                 selectList.Add(item);
             }
            return selectList;
        }

        /// <summary>
        /// 获取月份列表
        /// </summary>
        /// <returns></returns>
        public IList<SelectListItem> GetMonthList()
        {
            IList<SelectListItem> selectList = new List<SelectListItem>();

            for (var i = 1; i <= 12; i++)
            {
                SelectListItem item = new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                };
                selectList.Add(item);
            }
            return selectList;
        }


        #endregion


        #region 折线图分析


        /// <summary>
        /// 查询车辆费用走势数据
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public ReportMaster GetAnalysisByTrend(DistCarRunQueryParam queryParam)
        {
            IList<ReportDetail> data = new List<ReportDetail>();
            var carRunDatas = BusinessLogic.QueryCarRun(queryParam);
            var groupedDatas = carRunDatas.GroupBy(f => new {f.CAR_ID,f.CAR_NAME}).Distinct().ToList();
            //时间集合
            IList<string> days = new List<string>();
            for (var d = queryParam.StartDate; d <= queryParam.EndDate; d = d.AddDays(1))
            {
                days.Add(d.ToString("yy-MM-dd"));
            }
            //配送车辆集合
            Dictionary<string, string> carInfos = new Dictionary<string, string>();
            foreach (var c in groupedDatas)
            {
                carInfos.Add(c.Key.CAR_ID,c.Key.CAR_NAME);
            }
            foreach (var item in carInfos)
            {
                IList<decimal> itemData = new List<decimal>();
                foreach (var day in days)
                {
                    var hasData = carRunDatas.Any(f => f.CreateTime.ToString("yy-MM-dd") == day && f.CAR_ID == item.Key);
                    if (!hasData)
                    {
                        itemData.Add(0);
                    }
                    else
                    {
                        var currentCarRunDatas = carRunDatas.Where(f => f.CreateTime.ToString("yy-MM-dd") == day && f.CAR_ID == item.Key).ToList();
                        var currentCost = currentCarRunDatas.Sum(f => f.AMT_SUM.Value);
                        itemData.Add(currentCost);
                    }

                }

                ReportDetail orderByTime = new ReportDetail { name = item.Value, decimalData = itemData };
                data.Add(orderByTime);
            }
            ReportMaster dayOrderInfo = new ReportMaster { xAxis = days, data = data };
            return dayOrderInfo;
        }



        #endregion

        #region 柱状图分析

        /// <summary>
        /// 查询车辆费用走势数据
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public ColunReportModel GetCompareCarFeeData(DistCarRunQueryParam queryParam)
        {
            DistCarRunLineLogic lineLogic = new DistCarRunLineLogic();
            DistCarRunLineQueryParam lineParam = new DistCarRunLineQueryParam {CAR_ID=queryParam.CAR_ID,StartDate=queryParam.StartDate,EndDate=queryParam.EndDate };
            var carRunlineDatas = lineLogic.QueryCarRunLine(lineParam);

            IList<ColunLineModel> data = new List<ColunLineModel>();
            var carRunDatas = BusinessLogic.QueryCarRun(queryParam);
            var groupedDatas = carRunDatas.GroupBy(f => new { f.CAR_ID, f.CAR_NAME }).Distinct().ToList();
            //配送车辆集合
            Dictionary<string, string> carInfos = new Dictionary<string, string>();
            foreach (var c in groupedDatas)
            {
                carInfos.Add(c.Key.CAR_ID, c.Key.CAR_NAME);
            }
            //时间集合
            IList<string> xAxis = carInfos.Select(f => f.Value).ToList();
            IList<decimal> itemData = new List<decimal>();
            
            foreach (var item in carInfos)
            {
                IList<ColunItemModel> itemModel = new List<ColunItemModel>();
                var totalFee = carRunDatas.Where(f => f.CAR_ID == item.Key).Sum(t => t.AMT_SUM.Value);
                itemData.Add(totalFee) ;
                var nums=carRunDatas.Where(f => f.CAR_ID == item.Key).Select(f=>f.INFO_NUM).ToList();
                //获取子类型
                var lineDatas = carRunlineDatas.Where(f => nums.Contains(f.INFO_NUM)).ToList();
                var lineGroupDatas = lineDatas.GroupBy(f => new {f.COST_TYPE,f.COST_TYPE_DCR });
                var lineCategories = lineGroupDatas.Select(f => f.Key.COST_TYPE_DCR).ToList();
                ColunItemModel cm = new ColunItemModel
                {
                    name=item.Value,
                    categories = lineCategories
                };
                IList<decimal> lineValues = new List<decimal>();
                foreach (var l in lineGroupDatas)
                {
                    var currentLineValue = lineDatas.Where(f => f.COST_TYPE == l.Key.COST_TYPE).Sum(t=>t.AMT);
                    lineValues.Add(currentLineValue);
                }
                cm.decimalData = lineValues;

                ColunLineModel lineModel = new ColunLineModel { decimalData = itemData,drilldown=cm };
                data.Add(lineModel);
            }
            ColunReportModel result = new ColunReportModel { xAxis = xAxis, data = data };
            return result;
        }


        #endregion

        #region 增长率分析

        /// <summary>
        /// 查询同比增长率(选择年、车辆 展示各月的同比增长率)
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public ReportMaster GetYearRateOfCarYearData(DistCarRunRateQueryParam queryParam)
        {
            IList<ReportDetail> data = new List<ReportDetail>();
            var distDatas = BusinessLogic.GetYearRateOfCarYearData(queryParam);
            var car = new LdmDistCarLogic().GetAll().SingleOrDefault(f => f.CAR_ID == queryParam.CAR_ID);
            IList<string> xAxis = new List<string>();
            for (int i = 1; i <= 12; i++)
            {
                xAxis.Add(i.ToString());
            }
            IList<decimal> itemData = new List<decimal>();
            foreach (var item in xAxis)
            {
                var month=int.Parse(item);
                var current = distDatas.SingleOrDefault(f => f.MONTH == month);
                if (current != null)
                {
                    itemData.Add(current.RATE);
                }
                else
                {
                    itemData.Add(0);
                }
            }
            ReportDetail orderedData = new ReportDetail { name = car.CAR_NAME, decimalData = itemData };
            data.Add(orderedData);

            ReportMaster dayOrderInfo = new ReportMaster { xAxis = xAxis, data = data };
            return dayOrderInfo;

        }


        /// <summary>
        /// 查询同比增长率(选择年、月 展示各车辆的同比增长率)
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public ReportMaster GetYearRateOfCarMonthData(DistCarRunRateQueryParam queryParam)
        {
            IList<ReportDetail> data = new List<ReportDetail>();
            var distDatas = BusinessLogic.GetYearRateOfCarMonthData(queryParam);
            IList<string> xAxis = distDatas.Select(f => f.CAR_NAME).ToList();
            int i = 0;
            int totalCount = xAxis.Count;
            foreach (var item in xAxis)
            {
                IList<decimal> itemData = new List<decimal>();
                var current = distDatas.SingleOrDefault(f => f.CAR_NAME == item);
                for (int j = 0; j < i; j++) 
                {
                    itemData.Add(0);
                }
                itemData.Add(current.RATE);
                for (int k = i; k < totalCount-1; k++)
                {
                    itemData.Add(0);
                }
                ReportDetail orderedData = new ReportDetail { name = item, decimalData = itemData };
                data.Add(orderedData);
                i++;
            }

            ReportMaster dayOrderInfo = new ReportMaster { xAxis = xAxis, data = data };
            return dayOrderInfo;

        }


        /// <summary>
        /// 查询环比增长率(选择年、车辆 展示各车辆的环比增长率)
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public ReportMaster GetLinkRateOfCarData(DistCarRunRateQueryParam queryParam)
        {
            IList<ReportDetail> data = new List<ReportDetail>();
            var distDatas = BusinessLogic.GetLinkRateOfCarData(queryParam);
            var car = new LdmDistCarLogic().GetAll().SingleOrDefault(f => f.CAR_ID == queryParam.CAR_ID);
            IList<string> xAxis = new List<string>();
            for (int i = 1; i <= 12; i++)
            {
                xAxis.Add(i.ToString());
            }
            IList<decimal> itemData = new List<decimal>();
            foreach (var item in xAxis)
            {
                var month = int.Parse(item);
                var current = distDatas.SingleOrDefault(f => f.MONTH == month);
                if (current != null)
                {
                    itemData.Add(current.RATE);
                }
                else
                {
                    itemData.Add(0);
                }
            }
            ReportDetail orderedData = new ReportDetail { name = car.CAR_NAME, decimalData = itemData };
            data.Add(orderedData);

            ReportMaster dayOrderInfo = new ReportMaster { xAxis = xAxis, data = data };
            return dayOrderInfo;

        }

        #endregion

        /// <summary>
        /// 查询车辆行驶信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public PagedResults<DistCarRunModel> QueryDistCarRun(DistCarRunQueryParam queryParam)
        {
            var datas = BusinessLogic.QueryDistCarRun(queryParam);
            var pagedInfo = datas.PagerInfo;
            var dt = datas.Data.Select(f => ConvertToModel(f)).ToList();
            var result = new PagedResults<DistCarRunModel> { Data = dt, PagerInfo = pagedInfo };
            return result;

        }

    }
}