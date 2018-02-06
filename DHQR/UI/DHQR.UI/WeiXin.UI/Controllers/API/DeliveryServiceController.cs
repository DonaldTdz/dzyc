using Common.Base;
using DHQR.BusinessLogic.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Langchao;
using DHQR.UI.Controllers.API.Dto;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DHQR.UI.Controllers.API
{
    public class DeliveryServiceController : ApiController
    {
        public JObject PostRestultTest([FromBody]JObject jparam)
        {
            var param = jparam.ToObject<DownloadDistParam>();
            var result = new APIResultDTO() { Code = 0, Data = param, Message = "获取数据成功" };
            return JObject.FromObject(result);
        }

        private readonly ServiceCallLogLogic serverlogLogic = new ServiceCallLogLogic();

        #region 下载配送单

        /// <summary>
        /// 下载配送单
        /// </summary>
        public JObject PostDownloadDist([FromBody]JObject jparam)
        {
            var param = jparam.ToObject<DownloadDistParam>();
            DoHandle dohandle;
            #region 取浪潮配送单数据

            List<LdmDist> ldmDists = new List<LdmDist>();//配送单
            List<LdmDistLine> ldmDistLines = new List<LdmDistLine>();//配送单明细
            List<LdmDisItem> ldmDisItems = new List<LdmDisItem>();//配送单商品信息
            DZLangchaoLogic lcLogic = new DZLangchaoLogic();
            lcLogic.DownloadDists(param, out ldmDists, out ldmDistLines, out ldmDisItems);

            #endregion

            #region 写本地数据库

            LdmDistLogic ldmLogic = new LdmDistLogic();
            ldmLogic.DownloadDists(ldmDists, ldmDistLines, ldmDisItems, out dohandle);

            #endregion

            if (ldmDists.Count == 0)
            {
                dohandle.IsSuccessful = false;
                dohandle.OperateMsg = "未找到配送单,请与配送调度管理员联系!";
            }


            //写服务器日志
            serverlogLogic.InsertLog("DeliveryConfirm", "DownloadDist", jparam.ToString(), param.DLVMAN_ID, dohandle.IsSuccessful);

            var result = new APIResultDTO();

            if (dohandle.IsSuccessful)
            {

                #region 返回配送单到终端

                LdmInfo ldmInfo = new LdmInfo { LdmDists = ldmDists, LdmDistLines = ldmDistLines, LdmDisItems = ldmDisItems };
                result.Code = 0;
                result.Data = ldmInfo;
                result.Message = "获取数据成功";
                #endregion
            }
            else
            {
                result.Message = dohandle.OperateMsg;
                result.Code = 901;
            }
            return JObject.FromObject(result);
        }

        #endregion

        #region 下载配送单完成


        /// <summary>
        /// 下载配送单完成
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public JObject PostDownDistFinish([FromBody]JObject jparam)
        {
            DoHandle dohandle;
            var param = jparam.ToObject<DownDistFinish>();
            if (param.DistNums == null)
            {
                return JObject.FromObject(new APIResultDTO() { Code = 701, Message = "传入参数错误！" });
            }

            #region 浪潮数据库回写日志

            IList<I_DIST_RECORD_LOG> logs = new List<I_DIST_RECORD_LOG>();
            foreach (var item in param.DistNums)
            {

                I_DIST_RECORD_LOG log = new I_DIST_RECORD_LOG();
                var logKey = new LogKeyLogic().GetLogkey();
                OperationType opType;
                log.LOG_SEQ = logKey;
                log.OPERATION_TYPE = opType.downDistFinish;//下载完成
                log.REF_TYPE = param.REF_TYPE;
                log.REF_ID = item;
                log.LOG_DATE = param.LOG_DATE;
                log.LOG_TIME = param.LOG_TIME;
                log.USER_ID = param.USER_ID;
                log.LONGITUDE = param.LONGITUDE;
                log.LATITUDE = param.LATITUDE;
                log.OPERATE_MODE = param.OPERATE_MODE;
                logs.Add(log);
            }

            #endregion

            IList<DistRecordLog> distRecords = logs.Select(f => ConvertFromLC.ConvertRecordLog(f)).ToList();

            #region 写浪潮数据表【送货员操作日志】

            //LangchaoLogic lcLogic = new LangchaoLogic();
            //lcLogic.WriteDistRecordLog(logs, out dohandle);

            #endregion

            #region 写本地服务器数据表【送货员操作日志】

            //if (dohandle.IsSuccessful)
            //{
                DistRecordLogLogic logLogic = new DistRecordLogLogic();
                foreach (var d in distRecords)
                {
                    d.Id = Guid.NewGuid();
                }
                logLogic.Create(distRecords, out dohandle);
            //}
            #endregion

            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "DownDistFinish", jparam.ToString(), param.USER_ID, dohandle.IsSuccessful);

            #endregion


            var result = new APIResultDTO() { Code = (dohandle.IsSuccessful? 0 : 901), Message = dohandle.OperateMsg };
            return JObject.FromObject(result);

        }

        #endregion
    }
}