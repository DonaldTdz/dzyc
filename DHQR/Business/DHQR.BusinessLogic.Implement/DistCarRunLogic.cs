using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.BLL.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;
using Common.DAL.Implement;
using Common.Base;

namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// 车辆运行信息抬头逻辑层
    /// </summary>
    public class DistCarRunLogic : BaseLogic<DistCarRun>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private DistCarRunRepository DistCarRunRep { get { return new DistCarRunRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<DistCarRun> Repository
        {
            get { return DistCarRunRep; }
        }

        #region 车辆回程登记

        /// <summary>
        /// 车辆回程登记
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dohandle"></param>
        public void BackRegist(BackRegistParam data, out DoHandle dohandle)
        {
            decimal preMile = 0;

            if (data.CarRun == null || data.CarRunLine == null || data.CarRunLine.Count == 0)
            {
                dohandle = new DoHandle {IsSuccessful=false,OperateMsg="传入参数错误！" };
                return;
            }
            
            var logSeq = new LogKeyLogic().GetLogkey();
            OperationType opType;
            DistRecordLog log = new DistRecordLog 
            {
                Id=Guid.NewGuid(),
                LOG_SEQ=logSeq,
                REF_TYPE=data.CarRun.REF_TYPE,//配送单
                REF_ID =data.CarRun.REF_ID,
                OPERATION_TYPE = opType.missionFinish,
                LOG_DATE=DateTime.Now.ToString("yyyyMMdd"),
                LOG_TIME=DateTime.Now.ToString("HHmmss"),
                USER_ID=data.CarRun.DLVMAN_ID,
                LONGITUDE=0,
                LATITUDE=0,
                NOTE="送货完成",
                OPERATE_MODE="0"
            };
            var carKey = new CarRunKeyLogic().GetLogkey();
            data.CarRun.INFO_NUM = carKey;

            //加三种类型费用
            List<DistCarRunLine> realLines = new List<DistCarRunLine>();
            //int i=1;
            //foreach (var item in data.CarRunLine)
            //{
            //    item.INFO_NUM = carKey;
            //    item.LINE_ID = i;
            //    if (string.IsNullOrEmpty(item.FUEL_TYPE))
            //    {
            //        item.FUEL_TYPE = "4";
            //    }
            //    if (item.LITRE_SUM == null)
            //    {
            //        item.LITRE_SUM = 0;
            //    }
            //    if (item.FUEL_PRI == null)
            //    {
            //        item.FUEL_PRI = 0;
            //    }
            //    i++;
            //}

            ///燃油费
            DistCarRunLine fuelCost;
            var fuelData = data.CarRunLine.SingleOrDefault(f => f.COST_TYPE == "1");
            if (fuelData == null)
            {
                fuelCost = new DistCarRunLine
                {
                      Id=Guid.NewGuid(),
                      INFO_NUM=carKey,
                      LINE_ID=1,
                      COST_TYPE="1",
                      FUEL_TYPE="4",
                      LITRE_SUM=0,
                      FUEL_PRI=0,
                      AMT=0,
                      INV_NUM=""
                };
            }
            else
            {
                fuelCost = fuelData;
                fuelCost.Id = Guid.NewGuid();
                fuelCost.INFO_NUM = carKey;
                fuelCost.LINE_ID = 1;
                if (string.IsNullOrEmpty(fuelCost.FUEL_TYPE))
                {
                    fuelCost.FUEL_TYPE = "4";
                }
                if (fuelCost.LITRE_SUM == null)
                {
                    fuelCost.LITRE_SUM = 0;
                }
                if (fuelCost.FUEL_PRI == null)
                {
                    fuelCost.FUEL_PRI = 0;
                }

            }
            realLines.Add(fuelCost);

            ///过路过桥费
            DistCarRunLine roadCost;
            var roadData = data.CarRunLine.SingleOrDefault(f => f.COST_TYPE == "2");
            if (roadData == null)
            {
                roadCost = new DistCarRunLine
                {
                    Id = Guid.NewGuid(),
                    INFO_NUM = carKey,
                    LINE_ID = 2,
                    COST_TYPE = "2",
                    FUEL_TYPE = "4",
                    LITRE_SUM = 0,
                    FUEL_PRI = 0,
                    AMT = 0,
                    INV_NUM = ""
                };
            }
            else
            {
                roadCost = roadData;
                roadCost.Id = Guid.NewGuid();
                roadCost.INFO_NUM = carKey;
                roadCost.LINE_ID = 2;

                    roadCost.FUEL_TYPE = "4";


                    roadCost.LITRE_SUM = 0;


                    roadCost.FUEL_PRI = 0;


            }
            realLines.Add(roadCost);

            ///其他费用
            DistCarRunLine otherCost;
            var otherData = data.CarRunLine.SingleOrDefault(f => f.COST_TYPE == "9");
            if (otherData == null)
            {
                otherCost = new DistCarRunLine
                {
                    Id = Guid.NewGuid(),
                    INFO_NUM = carKey,
                    LINE_ID = 3,
                    COST_TYPE = "9",
                    FUEL_TYPE = "4",
                    LITRE_SUM = 0,
                    FUEL_PRI = 0,
                    AMT = 0,
                    INV_NUM = ""
                };
            }
            else
            {
                otherCost = otherData;
                otherCost.Id = Guid.NewGuid();
                otherCost.INFO_NUM = carKey;
                otherCost.LINE_ID = 3;

                    otherCost.FUEL_TYPE = "4";

                    otherCost.LITRE_SUM = 0;

                    otherCost.FUEL_PRI = 0;


            }
            realLines.Add(otherCost);

            LangchaoLogic lcLogic = new LangchaoLogic();

            //计算里程信息
            //查找是否存在上期里程信息
            var preCanRun = DistCarRunRep.GetPreDistCarRun(data.CarRun);

            //如果不存在，则用输入的值
            if (preCanRun == null)
            {
                preMile = data.CarRun.PRE_MIL == null ? 0 : data.CarRun.PRE_MIL.Value;
            }
            else
            {
                preMile = preCanRun.THIS_MIL.Value;
            }

            data.CarRun.ACT_MIL = data.CarRun.THIS_MIL - preMile;

            lcLogic.BackRegist(data.CarRun, realLines, log, out dohandle);

            if (dohandle.IsSuccessful)
            {
                DistCarRunRep.BackRegist(data.CarRun,realLines, log, out dohandle);
            }
        }


        /// <summary>
        /// 达州车辆回程登记
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dohandle"></param>
        public void BackRegistDZ(BackRegistParam data, out DoHandle dohandle)
        {
            decimal preMile = 0;

            if (data.CarRun == null || data.CarRunLine == null || data.CarRunLine.Count == 0)
            {
                dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "传入参数错误！" };
                return;
            }

            var logSeq = new LogKeyLogic().GetLogkey();
            OperationType opType;
            DistRecordLog log = new DistRecordLog
            {
                Id = Guid.NewGuid(),
                LOG_SEQ = logSeq,
                REF_TYPE = data.CarRun.REF_TYPE,//配送单
                REF_ID = data.CarRun.REF_ID,
                OPERATION_TYPE = opType.missionFinish,
                LOG_DATE = DateTime.Now.ToString("yyyyMMdd"),
                LOG_TIME = DateTime.Now.ToString("HHmmss"),
                USER_ID = data.CarRun.DLVMAN_ID,
                LONGITUDE = 0,
                LATITUDE = 0,
                NOTE = "送货完成",
                OPERATE_MODE = "0"
            };
            var carKey = new CarRunKeyLogic().GetLogkey();
            data.CarRun.INFO_NUM = carKey;

            //加三种类型费用
            List<DistCarRunLine> realLines = new List<DistCarRunLine>();
            //int i=1;
            //foreach (var item in data.CarRunLine)
            //{
            //    item.INFO_NUM = carKey;
            //    item.LINE_ID = i;
            //    if (string.IsNullOrEmpty(item.FUEL_TYPE))
            //    {
            //        item.FUEL_TYPE = "4";
            //    }
            //    if (item.LITRE_SUM == null)
            //    {
            //        item.LITRE_SUM = 0;
            //    }
            //    if (item.FUEL_PRI == null)
            //    {
            //        item.FUEL_PRI = 0;
            //    }
            //    i++;
            //}

            ///燃油费
            DistCarRunLine fuelCost;
            var fuelData = data.CarRunLine.SingleOrDefault(f => f.COST_TYPE == "1");
            if (fuelData == null)
            {
                fuelCost = new DistCarRunLine
                {
                    Id = Guid.NewGuid(),
                    INFO_NUM = carKey,
                    LINE_ID = 1,
                    COST_TYPE = "1",
                    FUEL_TYPE = "4",
                    LITRE_SUM = 0,
                    FUEL_PRI = 0,
                    AMT = 0,
                    INV_NUM = ""
                };
            }
            else
            {
                fuelCost = fuelData;
                fuelCost.Id = Guid.NewGuid();
                fuelCost.INFO_NUM = carKey;
                fuelCost.LINE_ID = 1;
                if (string.IsNullOrEmpty(fuelCost.FUEL_TYPE))
                {
                    fuelCost.FUEL_TYPE = "4";
                }
                if (fuelCost.LITRE_SUM == null)
                {
                    fuelCost.LITRE_SUM = 0;
                }
                if (fuelCost.FUEL_PRI == null)
                {
                    fuelCost.FUEL_PRI = 0;
                }

            }
            realLines.Add(fuelCost);

            ///过路过桥费
            DistCarRunLine roadCost;
            var roadData = data.CarRunLine.SingleOrDefault(f => f.COST_TYPE == "2");
            if (roadData == null)
            {
                roadCost = new DistCarRunLine
                {
                    Id = Guid.NewGuid(),
                    INFO_NUM = carKey,
                    LINE_ID = 2,
                    COST_TYPE = "2",
                    FUEL_TYPE = "4",
                    LITRE_SUM = 0,
                    FUEL_PRI = 0,
                    AMT = 0,
                    INV_NUM = ""
                };
            }
            else
            {
                roadCost = roadData;
                roadCost.Id = Guid.NewGuid();
                roadCost.INFO_NUM = carKey;
                roadCost.LINE_ID = 2;

                roadCost.FUEL_TYPE = "4";


                roadCost.LITRE_SUM = 0;


                roadCost.FUEL_PRI = 0;


            }
            realLines.Add(roadCost);

            ///其他费用
            DistCarRunLine otherCost;
            var otherData = data.CarRunLine.SingleOrDefault(f => f.COST_TYPE == "9");
            if (otherData == null)
            {
                otherCost = new DistCarRunLine
                {
                    Id = Guid.NewGuid(),
                    INFO_NUM = carKey,
                    LINE_ID = 3,
                    COST_TYPE = "9",
                    FUEL_TYPE = "4",
                    LITRE_SUM = 0,
                    FUEL_PRI = 0,
                    AMT = 0,
                    INV_NUM = ""
                };
            }
            else
            {
                otherCost = otherData;
                otherCost.Id = Guid.NewGuid();
                otherCost.INFO_NUM = carKey;
                otherCost.LINE_ID = 3;

                otherCost.FUEL_TYPE = "4";

                otherCost.LITRE_SUM = 0;

                otherCost.FUEL_PRI = 0;


            }
            realLines.Add(otherCost);

            //LangchaoLogic lcLogic = new LangchaoLogic();

            //计算里程信息
            //查找是否存在上期里程信息
            var preCanRun = DistCarRunRep.GetPreDistCarRun(data.CarRun);

            //如果不存在，则用输入的值
            if (preCanRun == null)
            {
                preMile = data.CarRun.PRE_MIL == null ? 0 : data.CarRun.PRE_MIL.Value;
            }
            else
            {
                preMile = preCanRun.THIS_MIL.Value;
            }

            data.CarRun.ACT_MIL = data.CarRun.THIS_MIL - preMile;
            //达州先注释回传数据
            //lcLogic.BackRegist(data.CarRun, realLines, log, out dohandle);

            //if (dohandle.IsSuccessful)
            //{
                DistCarRunRep.BackRegist(data.CarRun, realLines, log, out dohandle);
            //}
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
            var reusult = DistCarRunRep.QueryCarRun(queryParam);
            return reusult;
        }



        /// <summary>
        /// 查询同比增长率(选择年、车辆 展示各月的同比增长率)
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<DistCarRunYearRate> GetYearRateOfCarYearData(DistCarRunRateQueryParam queryParam)
        {
            return DistCarRunRep.GetYearRateOfCarYearData(queryParam);
        }

        
        /// <summary>
        /// 查询同比增长率(选择年、月 展示各车辆的同比增长率)
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<DistCarRunYearRate> GetYearRateOfCarMonthData(DistCarRunRateQueryParam queryParam)
        {
            return DistCarRunRep.GetYearRateOfCarMonthData(queryParam);
        }


        /// <summary>
        /// 查询环比增长率(选择年、月 展示各车辆的同比增长率)
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<DistCarRunLinkRate> GetLinkRateOfCarData(DistCarRunRateQueryParam queryParam)
        {
            return DistCarRunRep.GetLinkRateOfCarData(queryParam);
        }


        #endregion



        #region 根据车辆ID查找上期车辆行驶信息

        /// <summary>
        /// 根据车辆ID查找上期车辆行驶信息
        /// </summary>
        /// <param name="carRun"></param>
        /// <returns></returns>
        public PreMilData GetPreMile(string CAR_ID)
        {
            return DistCarRunRep.GetPreMile(CAR_ID);
        }

        #endregion


         /// <summary>
        /// 查询车辆行驶信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public PagedResults<DistCarRun> QueryDistCarRun(DistCarRunQueryParam queryParam)
        {
            return DistCarRunRep.QueryDistCarRun(queryParam);
        }
    }
}
