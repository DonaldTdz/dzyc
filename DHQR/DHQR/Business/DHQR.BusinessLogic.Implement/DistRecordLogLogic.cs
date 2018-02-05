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
    /// 配送操作日志逻辑层
    /// </summary>
    public class DistRecordLogLogic : BaseLogic<DistRecordLog>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private DistRecordLogRepository DistRecordLogRep { get { return new DistRecordLogRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<DistRecordLog> Repository
        {
            get { return DistRecordLogRep; }
        }

        /// <summary>
        /// 同步配送单日志
        /// </summary>
        /// <param name="synParam"></param>
        /// <returns></returns>
        public IList<LdmLog> SynLdmDist(SynLdmDistParam synParam)
        {
            return DistRecordLogRep.SynLdmDist(synParam);
        }

         /// <summary>
        /// 查询时间节点
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<TimeRecordDetail> QueryTimeRecord(TimeRecordQueryParam queryParam)
        {
            return DistRecordLogRep.QueryTimeRecord(queryParam);
        }

        /// <summary>
        /// 手动出库
        /// </summary>
        /// <param name="CO_NUM"></param>
        /// <param name="dohandle"></param>
        public void HandleCarOut(string DLVMAN_ID, string LOG_DATE,  out DoHandle dohandle)
        {
            dohandle = new DoHandle();

            //获取配送单号
            var refId = DistRecordLogRep.GetRefId(DLVMAN_ID, LOG_DATE);
            if (refId == null)
            {
                dohandle.IsSuccessful = false;
                dohandle.OperateMsg = "当前不能进行此操作";
                return;
            }
            var logKey = new LogKeyLogic().GetLogkey();

            DistRecordLog log = new DistRecordLog 
            {
                Id=Guid.NewGuid(),
                LOG_SEQ=logKey,
                REF_TYPE="1",
                REF_ID = refId,
                OPERATION_TYPE = "carOutWhse",
                LOG_DATE=LOG_DATE,
                LOG_TIME=DateTime.Now.ToString("HHmmss"),
                USER_ID=DLVMAN_ID,
                OPERATE_MODE="1"

            };

            LangchaoLogic lcLogic = new LangchaoLogic();
            var lcData = ConvertToLC.ConvertRecordLog(log);

            lcLogic.WriteDistRecordLog(lcData, out dohandle);

            if (dohandle.IsSuccessful)
            {
                DistRecordLogRep.Create(log,out dohandle);
            }

        }

        /// <summary>
        /// 手动入库
        /// </summary>
        /// <param name="CO_NUM"></param>
        /// <param name="dohandle"></param>
        public void HandleCarIn(string DLVMAN_ID, string LOG_DATE, out DoHandle dohandle)
        {
            dohandle = new DoHandle();

            //获取配送单号
            var refId = DistRecordLogRep.GetRefInId(DLVMAN_ID, LOG_DATE);
            if (refId == null)
            {
                dohandle.IsSuccessful = false;
                dohandle.OperateMsg = "当前不能进行此操作";
                return;
            }
            var logKey = new LogKeyLogic().GetLogkey();

            DistRecordLog log = new DistRecordLog
            {
                Id = Guid.NewGuid(),
                LOG_SEQ = logKey,
                REF_TYPE = "1",
                REF_ID = refId,
                OPERATION_TYPE = "carInWhse",
                LOG_DATE = LOG_DATE,
                LOG_TIME = DateTime.Now.ToString("HHmmss"),
                USER_ID = DLVMAN_ID,
                OPERATE_MODE = "1"

            };

            LangchaoLogic lcLogic = new LangchaoLogic();
            var lcData = ConvertToLC.ConvertRecordLog(log);

            lcLogic.WriteDistRecordLog(lcData, out dohandle);

            if (dohandle.IsSuccessful)
            {
                DistRecordLogRep.Create(log, out dohandle);
            }

        }


    }
}
