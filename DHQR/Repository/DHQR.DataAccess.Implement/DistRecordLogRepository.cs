using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using System.Data.Objects;
using System.Threading;
using System.Globalization;
using DHQR.BasicLib;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 配送操作日志数据访问层
    /// </summary>
    public class DistRecordLogRepository : ProRep<DistRecordLog>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public DistRecordLogRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<DistRecordLog> EntityCurrentSet
        {
            get { return ActiveContext.DistRecordLogs; }
        }


        /// <summary>
        /// 同步配送单日志
        /// </summary>
        /// <param name="synParam"></param>
        /// <returns></returns>
        public IList<LdmLog> SynLdmDist(SynLdmDistParam synParam)
        {
            IList<LdmLog> result = new List<LdmLog>();
            var ldst = ActiveContext.LdmDists.SingleOrDefault(f => f.DIST_NUM == synParam.DIST_NUM);
            IList<string> distNums = ActiveContext.LdmDists.Where(f => f.DLVMAN_ID == ldst.DLVMAN_ID && f.DIST_DATE == ldst.DIST_DATE).Select(t => t.DIST_NUM).ToList();
            var logs = ActiveContext.DistRecordLogs.Where(f => distNums.Contains(f.REF_ID) && f.OPERATION_TYPE != "downDistFinish").GroupBy(t => t.OPERATION_TYPE).ToList();
            foreach (var item in logs)
            {
                if (item.Key != "prepareCar")
                {
                    DistRecordLog d = item.First();
                    LdmLog currentLog = new LdmLog { DIST_NUM = synParam.DIST_NUM,OPERATION_TYPE=d.OPERATION_TYPE };
                    result.Add(currentLog);
                }
                else
                {
                    foreach (var t in item)
                    {
                        LdmLog currentLog = new LdmLog { DIST_NUM = synParam.DIST_NUM, OPERATION_TYPE = t.OPERATION_TYPE };
                        result.Add(currentLog);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 查询时间节点
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<TimeRecordDetail> QueryTimeRecord(TimeRecordQueryParam queryParam)
        {
            IList<TimeRecordDetail> result = new List<TimeRecordDetail>();
            OperationType opType;
            var queryDate = queryParam.QueryDate.ToString("yyyyMMdd");
            var records = (from a in ActiveContext.DistRecordLogs
                          join b in ActiveContext.LdmDists
                          on a.REF_ID equals b.DIST_NUM
                          where a.LOG_DATE == queryDate
                          select new TimeRecord 
                          {
                              CAR_ID=b.CAR_ID,
                              CAR_LICENSE=b.CAR_LICENSE,
                              DLVMAN_ID=b.DLVMAN_ID,
                              DLVMAN_NAME=b.DLVMAN_NAME,
                              DRIVER_NAME=b.DRIVER_NAME,
                              OPERATION_TYPE=a.OPERATION_TYPE,
                              LOG_DATE=a.LOG_DATE,
                              LOG_TIME=a.LOG_TIME
                              //ExactTime=DateTimeExtOpt.ConvertToDateTime(a.LOG_DATE + a.LOG_TIME, "yyyyMMddHHmmss")

                          }).ToList();
            foreach (var r in records)
            {
                r.ExactTime = DateTimeExtOpt.ConvertToDateTime(r.LOG_DATE + r.LOG_TIME, "yyyyMMddHHmmss");
            }
            var groupedDatas = records.GroupBy(f => new { f.CAR_ID, f.CAR_LICENSE, f.DLVMAN_ID, f.DLVMAN_NAME, f.DRIVER_NAME });
            
            foreach (var item in groupedDatas)
            {
                TimeRecordDetail td = new TimeRecordDetail();
                td.CAR_ID = item.Key.CAR_ID;
                td.CAR_LICENSE = item.Key.CAR_LICENSE;
                td.DLVMAN_ID = item.Key.DLVMAN_ID;
                td.DLVMAN_NAME = item.Key.DLVMAN_NAME;
                td.DRIVER_NAME = item.Key.DRIVER_NAME;
                td.LOG_DATE = item.First().LOG_DATE;
                //备车检查时间
                var prepareItem = item.Where(f => f.OPERATION_TYPE == opType.prepareCar).OrderBy(t=>t.ExactTime).FirstOrDefault();
                td.prepareCar = prepareItem == null ? string.Empty : prepareItem.ExactTime.ToString("yyyy-MM-dd HH:mm:ss");
             
                //开始装车时间
                var startLoadCollection = item.Where(f => f.OPERATION_TYPE == opType.startLoad).OrderBy(t=>t.ExactTime).ToList();
                var startLoadItem = startLoadCollection.FirstOrDefault();
                td.startLoad = startLoadItem == null ? string.Empty : startLoadItem.ExactTime.ToString("yyyy-MM-dd HH:mm:ss");

                //装车结束时间
                if (startLoadCollection.Count > 1)
                {
                    var finishLoadItem = startLoadCollection[1];
                    td.finishLoad =  finishLoadItem.ExactTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    var finishLoadItem = item.Where(f => f.OPERATION_TYPE == opType.finishLoad).OrderBy(t => t.ExactTime).FirstOrDefault();
                    td.finishLoad = finishLoadItem == null ? string.Empty : finishLoadItem.ExactTime.ToString("yyyy-MM-dd HH:mm:ss");
                }

                //车辆出库时间
                var carOutWhseItem = item.Where(f => f.OPERATION_TYPE == opType.carOutWhse).OrderBy(t => t.ExactTime).FirstOrDefault();
                td.carOutWhse = carOutWhseItem == null ? string.Empty : carOutWhseItem.ExactTime.ToString("yyyy-MM-dd HH:mm:ss");
                if (carOutWhseItem == null)
                {
                    var startMissionItem = item.Where(f => f.OPERATION_TYPE == opType.startMission).OrderBy(t => t.ExactTime).FirstOrDefault();
                    td.carOutWhse = startMissionItem == null ? string.Empty : startMissionItem.ExactTime.ToString("yyyy-MM-dd HH:mm:ss");
                }


                //车辆入库时间
                var carInWhseItem = item.Where(f => f.OPERATION_TYPE == opType.carInWhse).OrderBy(t => t.ExactTime).FirstOrDefault();
                td.carInWhse = carInWhseItem == null ? string.Empty : carInWhseItem.ExactTime.ToString("yyyy-MM-dd HH:mm:ss");
                if (carInWhseItem == null)
                {
                    var missionFinishItem = item.Where(f => f.OPERATION_TYPE == opType.missionFinish).OrderBy(t => t.ExactTime).FirstOrDefault();
                    td.carInWhse = missionFinishItem == null ? string.Empty : missionFinishItem.ExactTime.ToString("yyyy-MM-dd HH:mm:ss");

                }
                

                result.Add(td);

            }
            return result;
        }

        /// <summary>
        /// 获取日志配送单号
        /// </summary>
        /// <param name="DLVMAN_ID"></param>
        /// <param name="LOG_DATE"></param>
        /// <returns></returns>
        public string GetRefId(string DLVMAN_ID, string LOG_DATE)
        {
            var log = ActiveContext.DistRecordLogs.FirstOrDefault(f => f.USER_ID == DLVMAN_ID
                                    && f.LOG_DATE == LOG_DATE && f.OPERATION_TYPE == "prepareCar");
            if (log == null)
            {
                log = ActiveContext.DistRecordLogs.FirstOrDefault(f => f.USER_ID == DLVMAN_ID
                                                   && f.LOG_DATE == LOG_DATE && f.OPERATION_TYPE == "startLoad");
            }
            if (log == null)
            {
                log = ActiveContext.DistRecordLogs.FirstOrDefault(f => f.USER_ID == DLVMAN_ID
                                                   && f.LOG_DATE == LOG_DATE && f.OPERATION_TYPE == "finishLoad");
            }
            return log==null?null: log.REF_ID;

        }

        /// <summary>
        /// 获取日志配送单号
        /// </summary>
        /// <param name="DLVMAN_ID"></param>
        /// <param name="LOG_DATE"></param>
        /// <returns></returns>
        public string GetRefInId(string DLVMAN_ID, string LOG_DATE)
        {
            var log = ActiveContext.DistRecordLogs.FirstOrDefault(f => f.USER_ID == DLVMAN_ID
                                    && f.LOG_DATE == LOG_DATE && f.OPERATION_TYPE == "carOutWhse");
            return log == null ? null : log.REF_ID;

        }

    }
}
