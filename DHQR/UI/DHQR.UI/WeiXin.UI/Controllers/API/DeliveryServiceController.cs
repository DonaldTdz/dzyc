using Common.Base;
using DHQR.BusinessLogic.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Langchao;
using DHQR.UI.Controllers.API.Dto;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
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

        #region 备车检查

        /// <summary>
        /// 备车检查
        /// </summary>
        public JObject PostCheckCar([FromBody]JObject jparam)
        {
            DoHandle dohandle;
            var checkInfor = jparam.ToObject<CheckCarParam>();
            DistCarCheckLogic carCheckLogic = new DistCarCheckLogic();
            carCheckLogic.CheckDZCar(checkInfor.CheckDatas, out dohandle);

            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "CheckCar", jparam.ToString(), null, dohandle.IsSuccessful);

            #endregion


            var result = new APIResultDTO() { Code = (dohandle.IsSuccessful? 0 : 901), Message = dohandle.OperateMsg };
            return JObject.FromObject(result);
        }

        #endregion

        #region 装车开始

        /// <summary>
        /// 装车开始
        /// </summary>
        public JObject PostStartLoad([FromBody]JObject jparam)
        {
            DoHandle dohandle;
            var distRecord = jparam.ToObject<DistRecordLog>();

            var logKey = new LogKeyLogic().GetLogkey();
            OperationType opType;
            distRecord.LOG_SEQ = logKey;
            if (string.IsNullOrEmpty(distRecord.OPERATION_TYPE))
            {
                distRecord.OPERATION_TYPE = opType.startLoad;//开始装车
            }
            I_DIST_RECORD_LOG log = ConvertToLC.ConvertRecordLog(distRecord);

            #region 写浪潮数据表【送货员操作日志】

            //LangchaoLogic lcLogic = new LangchaoLogic();
            //lcLogic.WriteDistRecordLog(log, out dohandle);

            #endregion

            #region 写本地服务器数据表【送货员操作日志】

            //if (dohandle.IsSuccessful)
            //{
                DistRecordLogLogic logLogic = new DistRecordLogLogic();
                distRecord.Id = Guid.NewGuid();
                logLogic.Create(distRecord, out dohandle);
            //}
            #endregion

            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "StartLoad", jparam.ToString(), distRecord.USER_ID, dohandle.IsSuccessful);

            #endregion

            var result = new APIResultDTO() { Code=(dohandle.IsSuccessful?0:901), Message = dohandle.OperateMsg };
            return JObject.FromObject(result);

        }

        #endregion

        #region 装车结束

        /// <summary>
        /// 装车结束
        /// </summary>
        public JObject PostFinishLoad([FromBody]JObject jparam)
        {
            DoHandle dohandle;
            var distRecord = jparam.ToObject<DistRecordLog>();

            var logKey = new LogKeyLogic().GetLogkey();
            OperationType opType;
            distRecord.LOG_SEQ = logKey;
            distRecord.OPERATION_TYPE = opType.finishLoad; ;//装车结束
            I_DIST_RECORD_LOG log = ConvertToLC.ConvertRecordLog(distRecord);

            #region 写浪潮数据表【送货员操作日志】

            //LangchaoLogic lcLogic = new LangchaoLogic();
            //lcLogic.WriteDistRecordLog(log, out dohandle);

            #endregion

            #region 写本地服务器数据表【送货员操作日志】

            //if (dohandle.IsSuccessful)
            //{
                DistRecordLogLogic logLogic = new DistRecordLogLogic();
                distRecord.Id = Guid.NewGuid();
                logLogic.Create(distRecord, out dohandle);
            //}
            #endregion

            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "FinishLoad", jparam.ToString(), distRecord.USER_ID, dohandle.IsSuccessful);

            #endregion


            var result = new APIResultDTO() { Code = (dohandle.IsSuccessful ? 0 : 901), Message = dohandle.OperateMsg };
            return JObject.FromObject(result);
        }

        #endregion

        #region 车辆出库

        /// <summary>
        /// 车辆出库
        /// </summary>
        public JObject PostCarOutWhse([FromBody]JObject jparam)
        {
            DoHandle dohandle;
            var distRecord = jparam.ToObject<DistRecordLog>();

            var logKey = new LogKeyLogic().GetLogkey();
            OperationType opType;
            distRecord.LOG_SEQ = logKey;
            distRecord.OPERATION_TYPE = opType.carOutWhse; ;//车辆出库
            I_DIST_RECORD_LOG log = ConvertToLC.ConvertRecordLog(distRecord);

            #region 写浪潮数据表【送货员操作日志】

            //LangchaoLogic lcLogic = new LangchaoLogic();
            //lcLogic.WriteDistRecordLog(log, out dohandle);

            #endregion

            #region 写本地服务器数据表【送货员操作日志】

            //if (dohandle.IsSuccessful)
            //{
                DistRecordLogLogic logLogic = new DistRecordLogLogic();
                distRecord.Id = Guid.NewGuid();
                logLogic.Create(distRecord, out dohandle);
            //}
            #endregion

            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "CarOutWhse", jparam.ToString(), distRecord.USER_ID, dohandle.IsSuccessful);

            #endregion

            #region 微信通知
            //if (dohandle.IsSuccessful)
            //{
            //    try
            //    {
            //        DistCustLogic distLogic = new DistCustLogic();
            //        distLogic.SendWxMsgAfterOut(distRecord.REF_ID);
            //    }
            //    catch
            //    {

            //    }
            //}
            #endregion

            var result = new APIResultDTO() { Code = (dohandle.IsSuccessful ? 0 : 901), Message = dohandle.OperateMsg };
            return JObject.FromObject(result);

        }

        #endregion

        #region 送货任务开始

        /// <summary>
        /// 送货任务开始
        /// </summary>
        public JObject PostMissionStart([FromBody]JObject jparam)
        {
            DoHandle dohandle;
            var distRecord = jparam.ToObject<DistRecordLog>();

            var logKey = new LogKeyLogic().GetLogkey();
            OperationType opType;
            distRecord.LOG_SEQ = logKey;
            distRecord.OPERATION_TYPE = opType.startMission; ;//送货任务开始
            I_DIST_RECORD_LOG log = ConvertToLC.ConvertRecordLog(distRecord);

            #region 写浪潮数据表【送货员操作日志】

            //LangchaoLogic lcLogic = new LangchaoLogic();
            //lcLogic.WriteDistRecordLog(log, out dohandle);

            #endregion

            #region 写本地服务器数据表【送货员操作日志】

            //if (dohandle.IsSuccessful)
            //{
                DistRecordLogLogic logLogic = new DistRecordLogLogic();
                distRecord.Id = Guid.NewGuid();
                logLogic.Create(distRecord, out dohandle);
            //}
            #endregion

            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "MissionStart", jparam.ToString(), distRecord.USER_ID, dohandle.IsSuccessful);

            #endregion


            var result = new APIResultDTO() { Code = (dohandle.IsSuccessful ? 0 : 901), Message = dohandle.OperateMsg };
            return JObject.FromObject(result);

        }

        #endregion

        #region 回程登记

        /// <summary>
        /// 车辆回程登记
        /// </summary>
        public JObject PostBackRegist([FromBody]JObject jparam)
        {
            DoHandle dohandle;
            var param = jparam.ToObject<BackRegistParam>();
            DistCarRunLogic logic = new DistCarRunLogic();
            logic.BackRegistDZ(param, out dohandle);//logic.BackRegist(param, out dohandle);
            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "BackRegist", jparam.ToString(), param.CarRun.DLVMAN_ID, dohandle.IsSuccessful);

            #endregion

            var result = new APIResultDTO() { Code = (dohandle.IsSuccessful ? 0 : 901), Message = dohandle.OperateMsg };
            return JObject.FromObject(result);

        }

        #endregion

        #region 到货确认【含服务评价】

        /// <summary>
        /// 到货确认（含服务评价）
        /// </summary>
        public JObject PostConfirmDelivery([FromBody]JObject jparam)
        {
            DoHandle dohandle;

            //try
            //{

            var param = jparam.ToObject<DistCust>();
            if (param == null)
            {
                //dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "传入参数错误！" };
                //return jser.Serialize(dohandle);
                return JObject.FromObject(new APIResultDTO() { Code = 701, Message = "传入参数错误！" });
            }
            DistCustLogic logic = new DistCustLogic();
            //logic.ConfirmDelivery(param, out dohandle);
            logic.ConfirmDeliveryDZ(param, out dohandle);

            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "ConfirmDelivery", jparam.ToString(), param.DLVMAN_ID, dohandle.IsSuccessful);

            #endregion

            //}
            //catch(Exception ex)
            //{
            //    dohandle = new DoHandle();

            //    //写异常日志
            //    ExceptionLog exLog = new ExceptionLog
            //    {
            //        Id = Guid.NewGuid(),
            //        Message = ex.Message,
            //        InnerException = (ex.InnerException == null || ex.InnerException.Message == null) ? "" : ex.InnerException.Message,
            //        Ip = "",
            //        CreateTime = DateTime.Now,
            //        UserName = "System"
            //    };
            //    ExceptionLogLogic exLogic = new ExceptionLogLogic();
            //    exLogic.Create(exLog,out dohandle);

            //    dohandle.IsSuccessful = false;
            //    dohandle.OperateMsg = ex.Message;

            //}
            var result = new APIResultDTO() { Code = (dohandle.IsSuccessful ? 0 : 901), Message = dohandle.OperateMsg };
            return JObject.FromObject(result);
        }


        #endregion

        #region 退货


        /// <summary>
        /// 整单退货
        /// </summary>
        public JObject PostReturnAllOrder([FromBody]JObject jparam)
        {
            DoHandle dohandle = new DoHandle();
            var param = jparam.ToObject<DistCust>();
            CoReturnLogic logic = new CoReturnLogic();
            logic.ReturnAllOrderDZ(param, out dohandle);//logic.ReturnAllOrder(param, out dohandle);


            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "ReturnAllOrder", jparam.ToString(), param.DLVMAN_ID, dohandle.IsSuccessful);

            #endregion


            var result = new APIResultDTO() { Code = (dohandle.IsSuccessful ? 0 : 901), Message = dohandle.OperateMsg };
            return JObject.FromObject(result);
        }



        /// <summary>
        /// 部分退货
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public JObject PostReturnPatialOrder([FromBody]JObject jparam)
        {
            DoHandle dohandle;
            var param = jparam.ToObject<ReturnPatialOrderParam>();
            CoReturnLogic logic = new CoReturnLogic();
            logic.ReturnPatialOrderDZ(param, out dohandle);//logic.ReturnPatialOrder(param, out dohandle);
            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "ReturnPatialOrder", jparam.ToString(), param.Cust.DLVMAN_ID, dohandle.IsSuccessful);

            #endregion

            var result = new APIResultDTO() { Code = (dohandle.IsSuccessful ? 0 : 901), Message = dohandle.OperateMsg };
            return JObject.FromObject(result);
        }

        #endregion

        #region 一键上传

        /// <summary>
        /// 一键上传
        /// </summary>
        public JObject PostBatchAddedUpload([FromBody]JObject jparam)
        {
            DoHandle dohandle;
            var param = jparam.ToObject<BatchAddedUploadParam>();
            LdmDistLogic logic = new LdmDistLogic();
            logic.BatchAddedUpload(param, out dohandle);
            var result = new APIResultDTO() { Code = (dohandle.IsSuccessful ? 0 : 901), Message = dohandle.OperateMsg };
            return JObject.FromObject(result);
        }

        #endregion

        #region 零售户信息采集

        /// <summary>
        /// 零售户信息采集
        /// </summary>
        public JObject PostCollectRetailerXY([FromBody]JObject jparam)
        {
            DoHandle dohandle;
            var param = jparam.ToObject<GisCustPois>();
            GisCustPoisLogic logic = new GisCustPoisLogic();
            logic.CollectRetailerXYDZ(param, out dohandle);
            //logic.CollectRetailerXY(param, out dohandle);

            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "CollectRetailerXY", jparam.ToString(), param.CRT_USER_ID, dohandle.IsSuccessful);

            #endregion

            var result = new APIResultDTO() { Code = (dohandle.IsSuccessful ? 0 : 901), Message = dohandle.OperateMsg };
            return JObject.FromObject(result);
        }

        /// <summary>
        /// 根据零售户ID集合获取零售户位置信息
        /// </summary>
        public JObject PostGetRetailerLocation([FromBody]JObject jparam)
        {
            var param = jparam.ToObject<GetRetailerLocationParam>();
            GisCustPoisLogic logic = new GisCustPoisLogic();
            var queryDatas = logic.GetRetailerLocation(param.CustIds);
            var result = new APIResultDTO();
            result.Data = queryDatas;
            result.Code = 0;
            result.Message = "获取数据成功";
            return JObject.FromObject(result);
        }

        #endregion

        #region 位置上传

        /// <summary>
        /// 送货员实时位置上传
        /// </summary>
        public JObject PostUploadLocation([FromBody]JObject jparam)
        {
            var param = jparam.ToObject<GisLastLocrecord>();
            DoHandle dohandle;
            GisLastLocrecordLogic logic = new GisLastLocrecordLogic();
            logic.UploadLocationDZ(param, out dohandle);//logic.UploadLocation(param, out dohandle);
            var result = new APIResultDTO() { Code = (dohandle.IsSuccessful ? 0 : 901), Message = dohandle.OperateMsg };
            return JObject.FromObject(result);
        }

        #endregion

        #region 文件操作

        /// <summary>
        /// 上传文件
        /// </summary>
        public JObject PostUpfileByte(string FileByte, [FromBody]JObject jparam)
        {
            var param = jparam.ToObject<UpfileByteParam>();

            //FileStream readFs = new FileStream(Server.MapPath(@"\source\" + "Koala.jpg"), FileMode.Open, FileAccess.Read); 
            //string FileName = Guid.NewGuid().ToString();
            //byte[] flieByte = new byte[readFs.Length];
            //readFs.Read(flieByte, 0, (int)readFs.Length);

            //  Base64Decoder deCoder = new Base64Decoder();

            byte[] flieByte = Convert.FromBase64String(FileByte);
            DoHandle dohandle = new DoHandle { OperateMsg = "上传失败", IsSuccessful = false };
            try
            {
                //写数据库
                DistAttachmentInfo info = new DistAttachmentInfo
                {
                    Id = Guid.NewGuid(),
                    DIST_NUM = param.DIST_NUM,
                    CO_NUM = param.CO_NUM,
                    FileName = param.FILE_NAME,
                    FilePath = HostingEnvironment.MapPath(@"\upfile\" + param.FILE_NAME + "." + param.FILE_TYPE), //Server.MapPath(@"\upfile\" + param.FILE_NAME + "." + param.FILE_TYPE),
                    Extention = param.FILE_TYPE,
                    CreateTime = DateTime.Now,
                    Creator = param.DLVMAN_ID
                };

                DistAttachmentInfoLogic distLogic = new DistAttachmentInfoLogic();
                distLogic.Create(info, out dohandle);


                MemoryStream m = new MemoryStream(flieByte);
                using (FileStream fs = File.Open(HostingEnvironment.MapPath(@"\upfile\" + info.Id.ToString() + "." + param.FILE_TYPE), FileMode.Create))
                {

                    m.WriteTo(fs);
                    m.Close();
                    fs.Close();


                    APIResultDTO fileReturn = new APIResultDTO
                    {
                        Code = (dohandle.IsSuccessful ? 0 : 701),
                        Message = dohandle.OperateMsg,
                        Data = info.Id.ToString()
                    };
                    #region 写服务器日志

                    serverlogLogic.InsertLog("DeliveryConfirm", "UpfileByte", jparam.ToString(), param.DLVMAN_ID, dohandle.IsSuccessful);

                    #endregion

                    return JObject.FromObject(fileReturn);
                }
            }
            catch (Exception xx)
            {
                dohandle.OperateMsg = xx.Message;

                #region 写服务器日志

                serverlogLogic.InsertLog("DeliveryConfirm", "UpfileByte", jparam.ToString(), param.DLVMAN_ID, dohandle.IsSuccessful);

                #endregion

                var result = new APIResultDTO() { Code = (dohandle.IsSuccessful ? 0 : 901), Message = dohandle.OperateMsg };
                return JObject.FromObject(result);
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="FileName"></param>
        public byte[] Downfilebyte(ref string FileName)
        {
            try
            {
                using (FileStream fs = File.Open(HostingEnvironment.MapPath(@"\upfile\" + FileName), FileMode.Open))
                {
                    byte[] b = new byte[fs.Length];
                    fs.Read(b, 0, Convert.ToInt32(fs.Length));
                    fs.Close();
                    FileName = "";
                    return b;
                }
            }
            catch (Exception xx) { FileName = xx.Message; return null; }
        }

        #endregion

        #region 同步订单信息

        /// <summary>
        /// 根据配送单号同步订单信息
        /// </summary>
        public JObject PostSynOrders([FromBody]JObject jparam)
        {
            var param = jparam.ToObject<SynOrderParam>();
            LdmDistLineLogic logic = new LdmDistLineLogic();
            var orderInfos = logic.SynOrders(param);
            var result = new APIResultDTO();
            result.Code = 0;
            result.Data = orderInfos;
            result.Message = "同步数据成功";
            return JObject.FromObject(result);
        }


        #endregion

        #region 获取车辆上期里程

        /// <summary>
        /// 获取车辆上期里程
        /// </summary>
        public JObject PostGetPreMile([FromBody]JObject jparam)
        {
            var param = jparam.ToObject<GetPreMileParam>();
            DistCarRunLogic logic = new DistCarRunLogic();
            var returnData = logic.GetPreMile(param.CAR_ID);
            var result = new APIResultDTO();
            result.Code = 0;
            result.Data = returnData;
            result.Message = "获取数据成功";
            return JObject.FromObject(result);
        }

        #endregion

        #region 微信通知客户

        /// <summary>
        /// 微信通知客户
        /// </summary>
        public JObject PostWeiXinNotify([FromBody]JObject jparam)
        {
            DoHandle dohandle;
            var param = jparam.ToObject<WeiXinNotifyParam>();
            DistCustLogic logic = new DistCustLogic();
            //logic.WeiXinNotify(param, out dohandle);

            #region 写服务器日志

            //serverlogLogic.InsertLog("DeliveryConfirm", "WeiXinNotify", jparam.ToString(), param.CO_NUM, dohandle.IsSuccessful);

            #endregion

            var result = new APIResultDTO() { Code = 902, Message = "暂不支持" };
            return JObject.FromObject(result);
        }



        #endregion

        #region 同步配送单日志

        /// <summary>
        /// 同步配送单日志
        /// </summary>
        public JObject PostSynLdmDist([FromBody]JObject jparam)
        {
            var param = jparam.ToObject<SynLdmDistParam>();
            DistRecordLogLogic logic = new DistRecordLogLogic();
            var records = logic.SynLdmDist(param);

            SynOrderParam orderParam = new SynOrderParam { DIST_NUM = param.DIST_NUM };
            LdmDistLineLogic orderlogic = new LdmDistLineLogic();
            var orderInfos = orderlogic.SynOrders(orderParam);

            SynLdmInfoData totalData = new SynLdmInfoData { Logs = records.ToList(), OrderInfo = orderInfos.ToList() };

            var result = new APIResultDTO();
            result.Code = 0;
            result.Data = totalData;
            result.Message = "同步数据成功";
            return JObject.FromObject(result);
        }



        #endregion

        #region NFC查询客户信息

        /// <summary>
        /// NFC查询客户信息
        /// </summary>
        public JObject PostGetNfcCustomer([FromBody]JObject jparam)
        {
            var param = jparam.ToObject<GetNfcCustomerParam>();
            RetailerLogic logic = new RetailerLogic();
            var returnData = logic.GetNfcCustomer(param);
            var result = new APIResultDTO();
            result.Code = 0;
            result.Data = returnData;
            result.Message = "获取数据成功";
            return JObject.FromObject(result);
        }

        #endregion


        #region 下载配送单(根据日期)

        /// <summary>
        /// 下载配送单
        /// </summary>
        public JObject PostDownloadDistByDate([FromBody]JObject jparam)
        {
            var param = jparam.ToObject<DownloadDistByDateParam>();
            DoHandle dohandle;
            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "DownloadDistByDate", jparam.ToString(), param.DLVMAN_ID, true);

            #endregion

            #region 取浪潮配送单数据

            List<LdmDist> ldmDists = new List<LdmDist>();//配送单
            List<LdmDistLine> ldmDistLines = new List<LdmDistLine>();//配送单明细
            List<LdmDisItem> ldmDisItems = new List<LdmDisItem>();//配送单商品信息
            List<I_CO_TEMP_RETURN> tmpReturns = new List<I_CO_TEMP_RETURN>();//再出库暂存订单


            DZLangchaoLogic lcLogic = new DZLangchaoLogic();
            lcLogic.DownloadDistByDate(param, out ldmDists, out ldmDistLines, out ldmDisItems, out tmpReturns);

            #endregion

            #region 写本地数据库

            LdmDistLogic ldmLogic = new LdmDistLogic();
            ldmLogic.DownloadDistsByDate(ldmDists, ldmDistLines, ldmDisItems, tmpReturns, out dohandle);

            #endregion

            if (ldmDists.Count == 0)
            {
                dohandle.IsSuccessful = false;
                dohandle.OperateMsg = "未找到配送单,请与配送调度管理员联系!";
            }

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
    }
}