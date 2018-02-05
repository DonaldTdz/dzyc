using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using System.Data.Objects;
using Common.Base;

namespace DHQR.DataAccess.Implement
{
    public class DistCarRunRepository : ProRep<DistCarRun>
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public DistCarRunRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<DistCarRun> EntityCurrentSet
        {
            get { return ActiveContext.DistCarRuns; }
        }


        #region 车辆回程登记

        /// <summary>
        /// 车辆回程登记
        /// </summary>
        /// <param name="data"></param>
        /// <param name="log"></param>
        /// <param name="dohandle"></param>
        public void BackRegist(DistCarRun carRun,List<DistCarRunLine> carRunLine,DistRecordLog log,out DoHandle dohandle)
        {
            dohandle=new DoHandle{IsSuccessful=false,OperateMsg="操作失败!"};
            //var currenDate=DateTime.Now.ToString("yyyy-MM-dd");
            var head = carRun;
            var details = carRunLine;
            head.Id = Guid.NewGuid();
            head.CreateTime =DateTime.Now;
            head.DistYear = head.CreateTime.Year;
            head.DistMonth = head.CreateTime.Month;
          //  head.ACT_MIL = head.THIS_MIL - head.PRE_MIL;
            ActiveContext.DistCarRuns.AddObject(head);
            foreach (var item in details)
            {
                item.Id = Guid.NewGuid();
                ActiveContext.DistCarRunLines.AddObject(item);
            }
            ActiveContext.DistRecordLogs.AddObject(log);
            ActiveContext.SaveChanges();
            dohandle.IsSuccessful = true;
            dohandle.OperateMsg = "操作成功!";
        }

        #endregion


        #region 车辆运行统计

        /// <summary>
        /// 查询车辆运行信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<DistCarRun> QueryCarRun(DistCarRunQueryParam queryParam)
        {
            var reusult=ActiveContext.DistCarRuns.Where(f=>
                                               (string.IsNullOrEmpty(queryParam.CAR_ID) || f.CAR_ID==queryParam.CAR_ID)
                                            &&  f.CreateTime>=queryParam.StartDate
                                            && f.CreateTime<queryParam.EndDate
                
                ).ToList();
            IList<string> carIds = reusult.Select(f => f.CAR_ID).ToList();
            var cars = ActiveContext.LdmDistCars.Where(f => carIds.Contains(f.CAR_ID)).ToList();
            foreach (var item in reusult)
            {
                var car = cars.SingleOrDefault(f => f.CAR_ID == item.CAR_ID);
                item.CAR_NAME = car == null ? " " : car.CAR_NAME;
            }
            return reusult;
        }


        


        /// <summary>
        /// 查询同比增长率(选择年、车辆 展示各月的同比增长率)
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<DistCarRunYearRate> GetYearRateOfCarYearData(DistCarRunRateQueryParam queryParam)
        {
            var preYear = queryParam.Year - 1;
            var thisYearDatas = ActiveContext.DistCarRuns.Where(f => f.DistYear==queryParam.Year
                                                                  && f.CAR_ID==queryParam.CAR_ID
                                                            ).ToList();
            var preYearDatas = ActiveContext.DistCarRuns.Where(f => f.DistYear == preYear
                                                                  && f.CAR_ID==queryParam.CAR_ID
                                                            ).ToList();

            //分组求出每月车辆的费用
            var thisGroupedDatas = thisYearDatas.GroupBy(f => new { f.DistYear, f.DistMonth }).ToList();

            var preGroupedDatas = preYearDatas.GroupBy(f => new { f.DistYear, f.DistMonth }).ToList();

            var car = ActiveContext.LdmDistCars.SingleOrDefault(f => f.CAR_ID == queryParam.CAR_ID);

            //计算同比数据
            IList<DistCarRunYearRate> result = new List<DistCarRunYearRate>();
            foreach (var p in preGroupedDatas)
            {
                var thisYearDt = thisYearDatas.Where(f => f.DistYear == queryParam.Year && f.DistMonth == p.Key.DistMonth).ToList();
                var thisYearFee = thisYearDt.Count > 0 ? thisYearDt.Sum(f => f.AMT_SUM) : 0;
                var preYearFee = p.Sum(f => f.AMT_SUM);
                var rate = preYearFee == 0 ? 0 : ((thisYearFee - preYearFee) / preYearFee);
                DistCarRunYearRate item = new DistCarRunYearRate 
                {
                    CAR_ID=queryParam.CAR_ID,
                    CAR_NAME=car.CAR_NAME,
                    YEAR=p.Key.DistYear,
                    MONTH=p.Key.DistMonth,
                    RATE=decimal.Round(rate.Value,4)*100
                };
                result.Add(item);             
            }
            return result;
        }


        /// <summary>
        /// 查询同比增长率(选择年、月 展示各车辆的同比增长率)
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<DistCarRunYearRate> GetYearRateOfCarMonthData(DistCarRunRateQueryParam queryParam)
        {
            var preYear = queryParam.Year - 1;

            var thisYearDatas = ActiveContext.DistCarRuns.Where(f => f.DistYear == queryParam.Year
                                                      && f.DistMonth == queryParam.Month
                                                ).ToList();
            var preYearDatas = ActiveContext.DistCarRuns.Where(f => f.DistYear == preYear
                                                                  && f.DistMonth == queryParam.Month
                                                            ).ToList();
            IList<string> carIds = preYearDatas.Select(f => f.CAR_ID).Distinct().ToList();
            var cars = ActiveContext.LdmDistCars.Where(f => carIds.Contains(f.CAR_ID)).ToList();

            //分组求出每月车辆的费用
            var thisGroupedDatas = thisYearDatas.GroupBy(f => new { f.DistYear, f.DistMonth,f.CAR_ID }).ToList();

            var preGroupedDatas = preYearDatas.GroupBy(f => new { f.DistYear, f.DistMonth ,f.CAR_ID}).ToList();

            //计算同比数据
            IList<DistCarRunYearRate> result = new List<DistCarRunYearRate>();
            foreach (var p in preGroupedDatas)
            {
                var thisYearDt = thisYearDatas.Where(f => f.CAR_ID==p.Key.CAR_ID).ToList();
                var thisYearFee = thisYearDt.Count > 0 ? thisYearDt.Sum(f => f.AMT_SUM) : 0;
                var preYearFee = p.Sum(f => f.AMT_SUM);
                var rate = preYearFee == 0 ? 0 : ((thisYearFee - preYearFee) / preYearFee);
                var car = cars.SingleOrDefault(f => f.CAR_ID == p.Key.CAR_ID);
                DistCarRunYearRate item = new DistCarRunYearRate
                {
                    CAR_ID = queryParam.CAR_ID,
                    CAR_NAME = car.CAR_NAME,
                    YEAR = p.Key.DistYear,
                    MONTH = p.Key.DistMonth,
                    RATE = decimal.Round(rate.Value, 4) * 100
                };
                result.Add(item);
            }
            return result;
 
        }



        /// <summary>
        /// 查询环比增长率(选择年、车辆 展示各车辆的环比增长率)
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<DistCarRunLinkRate> GetLinkRateOfCarData(DistCarRunRateQueryParam queryParam)
        {
            var preYear = queryParam.Year - 1;
            //今年数据
            var thisYearDatas = ActiveContext.DistCarRuns.Where(f => f.DistYear == queryParam.Year
                                                                  && f.CAR_ID == queryParam.CAR_ID
                                                            ).ToList();
            //去年12月数据
            var preYearDatas = ActiveContext.DistCarRuns.Where(f => f.DistYear == preYear
                                                                  && f.CAR_ID == queryParam.CAR_ID
                                                                  && f.DistMonth==12
                                                            ).ToList();

            //分组求出每月车辆的费用
            var thisGroupedDatas = thisYearDatas.GroupBy(f => new { f.DistYear, f.DistMonth }).ToList();

            var car = ActiveContext.LdmDistCars.SingleOrDefault(f => f.CAR_ID == queryParam.CAR_ID);
            IList<DistCarRunLinkRate> result = new List<DistCarRunLinkRate>();
            for (int i = 1; i <= 12; i++)
            {
                decimal preAmount = 0;
                if (i == 1)
                {
                    preAmount = preYearDatas.Count > 0 ? preYearDatas.Sum(f => f.AMT_SUM).Value : 0;
                }
                else
                {
                    var preDatas = thisGroupedDatas.SingleOrDefault(f => f.Key.DistMonth == i-1);
                    preAmount =preDatas!=null ? preDatas.Sum(f => f.AMT_SUM).Value : 0;
                }

                var currentDatas = thisGroupedDatas.SingleOrDefault(f => f.Key.DistMonth == i);
                var currrentAmount = currentDatas!=null ? currentDatas.Sum(f => f.AMT_SUM).Value : 0;

                var rate = preAmount == 0 ? 0 : ((currrentAmount - preAmount) / preAmount);

                DistCarRunLinkRate item = new DistCarRunLinkRate 
                {
                    YEAR = queryParam.Year,
                    MONTH=i,
                    CAR_ID=car.CAR_ID,
                    CAR_NAME=car.CAR_NAME,
                    RATE = decimal.Round(rate, 4) * 100
                };

                result.Add(item);
            }
            return result;
        }


        #endregion

        /// <summary>
        /// 根据本次车辆查找上期车辆行驶信息
        /// </summary>
        /// <param name="carRun"></param>
        /// <returns></returns>
        public DistCarRun GetPreDistCarRun(DistCarRun carRun)
        {
            var hasCar = ActiveContext.DistCarRuns.Any(f => f.CAR_ID == carRun.CAR_ID);
            if (hasCar)
            {
                var maxTime = ActiveContext.DistCarRuns.Where(f => f.CAR_ID == carRun.CAR_ID).Max(f => f.CreateTime);
                var result = ActiveContext.DistCarRuns.SingleOrDefault(f => f.CreateTime == maxTime && f.CAR_ID == carRun.CAR_ID);
                return result;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 根据车辆ID查找上期车辆行驶信息
        /// </summary>
        /// <param name="carRun"></param>
        /// <returns></returns>
        public PreMilData GetPreMile(string CAR_ID)
        {
            PreMilData result = new PreMilData();
            var hasCar = ActiveContext.DistCarRuns.Any(f => f.CAR_ID == CAR_ID);
            if (hasCar)
            {
                var maxTime = ActiveContext.DistCarRuns.Where(f => f.CAR_ID == CAR_ID).Max(f => f.CreateTime);
                var preMil = ActiveContext.DistCarRuns.SingleOrDefault(f => f.CreateTime == maxTime && f.CAR_ID == CAR_ID);
                result.PRE_MIL = preMil.THIS_MIL;
                return result;
            }
            else
            {
                result.PRE_MIL = 0;
                return result;
            }
        }

        /// <summary>
        /// 查询车辆行驶信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public PagedResults<DistCarRun> QueryDistCarRun(DistCarRunQueryParam queryParam)
        {
            var endDate=queryParam.EndDate.AddDays(1);
            var datas = ActiveContext.DistCarRuns.Where(f => f.CreateTime >= queryParam.StartDate && f.CreateTime < endDate)
                            .ToPagedResults<DistCarRun>(queryParam);
            foreach (var item in datas.Data)
            {
                var ldmDist = ActiveContext.LdmDists.SingleOrDefault(f => f.DIST_NUM == item.REF_ID);
                if (ldmDist != null)
                {
                    item.DLVMAN_NAME = ldmDist.DLVMAN_NAME;
                    item.DRIVER_NAME = ldmDist.DRIVER_NAME;
                    item.LICENSE_CODE = ldmDist.CAR_LICENSE;
                }

                var lines = ActiveContext.DistCarRunLines.Where(f => f.INFO_NUM == item.INFO_NUM).ToList();
                if (lines.Count == 0)
                {
                    item.FUEL_MONEY = 0;
                    item.ROAD_MONEY = 0;
                    item.OTHER_MONRY = 0;
                }
                else
                {
                    item.FUEL_MONEY = lines.Where(f => f.COST_TYPE == "1").Sum(t => t.AMT);
                    item.ROAD_MONEY = lines.Where(f => f.COST_TYPE == "2").Sum(t => t.AMT);
                    item.OTHER_MONRY = lines.Where(f => f.COST_TYPE == "9").Sum(t => t.AMT); 
                }
            }
            return datas;
        }

    }

}
