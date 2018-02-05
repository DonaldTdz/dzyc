using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.Services.Protocols;
using Common.Base;
using DHQR.BasicLib;
using DHQR.BusinessLogic.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Langchao;

namespace DHQR.Service
{
    /// <summary>
    /// DeliveryConfirm 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    [SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.RequestElement)] 
    public class DeliveryConfirm : System.Web.Services.WebService
    {


        private readonly ServiceCallLogLogic serverlogLogic = new ServiceCallLogLogic();

        #region 下载配送单

        /// <summary>
        /// 下载配送单
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod]
        public string DownloadDist(string data)
        {
            var jser = new JavaScriptSerializer();
            var param = jser.Deserialize<DownloadDistParam>(data);
            DoHandle dohandle;
            #region 取浪潮配送单数据

            List<LdmDist> ldmDists = new List<LdmDist>();//配送单
            List<LdmDistLine> ldmDistLines = new List<LdmDistLine>();//配送单明细
            List<LdmDisItem> ldmDisItems = new List<LdmDisItem>();//配送单商品信息



            LangchaoLogic lcLogic = new LangchaoLogic();
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


            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "DownloadDist", data, param.DLVMAN_ID, dohandle.IsSuccessful);

            #endregion

           


            if (dohandle.IsSuccessful)
            {

                #region 返回配送单到终端

                LdmInfo ldmInfo = new LdmInfo { LdmDists = ldmDists, LdmDistLines = ldmDistLines, LdmDisItems = ldmDisItems };
                var result = jser.Serialize(ldmInfo);

                #endregion


                return result;
            }

            else
            {
                var result = jser.Serialize(dohandle);
                return result;
            }
        }

        #endregion

        #region 下载配送单完成


        /// <summary>
        /// 下载配送单完成
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod]
        public string DownDistFinish(string data)
        {
            var jser = new JavaScriptSerializer();
            DoHandle dohandle;
            var param = jser.Deserialize<DownDistFinish>(data);
            if (param.DistNums == null)
            {
                dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "传入参数错误！" };
                return jser.Serialize(dohandle);
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

            IList<DistRecordLog> distRecords =logs.Select(f=> ConvertFromLC.ConvertRecordLog(f)).ToList();

            #region 写浪潮数据表【送货员操作日志】

            LangchaoLogic lcLogic = new LangchaoLogic();
            lcLogic.WriteDistRecordLog(logs, out dohandle);

            #endregion

            #region 写本地服务器数据表【送货员操作日志】

            if (dohandle.IsSuccessful)
            {
                DistRecordLogLogic logLogic = new DistRecordLogLogic();
                foreach (var d in distRecords)
                {
                    d.Id = Guid.NewGuid();
                }
                logLogic.Create(distRecords, out dohandle);
            }
            #endregion

            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "DownDistFinish", data, param.USER_ID, dohandle.IsSuccessful);

            #endregion


            var result = jser.Serialize(dohandle);
            return result;

        }

        #endregion

        #region 备车检查

        /// <summary>
        /// 备车检查
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
         [WebMethod]
        public string CheckCar(string data)
        {
            var jser = new JavaScriptSerializer();
            DoHandle dohandle;
            var checkInfor = jser.Deserialize<CheckCarParam>(data);
            DistCarCheckLogic carCheckLogic = new DistCarCheckLogic();
            carCheckLogic.CheckCar(checkInfor.CheckDatas, out dohandle);

            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "CheckCar", data, null, dohandle.IsSuccessful);

            #endregion


            var result = jser.Serialize(dohandle);
            return result ;
        }

        #endregion

        #region 装车开始

        /// <summary>
        /// 装车开始
        /// </summary>
        /// <param name="data"></param>
        [WebMethod]
        public string StartLoad(string data)
        {
            var jser = new JavaScriptSerializer();
            DoHandle dohandle;
            var distRecord = jser.Deserialize<DistRecordLog>(data);

            var logKey = new LogKeyLogic().GetLogkey();
            OperationType opType;
            distRecord.LOG_SEQ = logKey;
            if (string.IsNullOrEmpty(distRecord.OPERATION_TYPE))
            {
                distRecord.OPERATION_TYPE = opType.startLoad;//开始装车
            }
            I_DIST_RECORD_LOG log = ConvertToLC.ConvertRecordLog(distRecord);

            #region 写浪潮数据表【送货员操作日志】

            LangchaoLogic lcLogic = new LangchaoLogic();
            lcLogic.WriteDistRecordLog(log, out dohandle);

            #endregion

            #region 写本地服务器数据表【送货员操作日志】

            if (dohandle.IsSuccessful)
            {
                DistRecordLogLogic logLogic = new DistRecordLogLogic();
                distRecord.Id = Guid.NewGuid();
                logLogic.Create(distRecord, out dohandle);
            }
            #endregion

            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "StartLoad", data, distRecord.USER_ID, dohandle.IsSuccessful);

            #endregion

            var result = jser.Serialize(dohandle);
            return result;

        }

        #endregion

        #region 装车结束

        /// <summary>
        /// 装车结束
        /// </summary>
        /// <param name="data"></param>
         [WebMethod]
        public string FinishLoad(string data)
        {
            var jser = new JavaScriptSerializer();
            DoHandle dohandle;
            var distRecord = jser.Deserialize<DistRecordLog>(data);

            var logKey = new LogKeyLogic().GetLogkey();
            OperationType opType;
            distRecord.LOG_SEQ = logKey;
            distRecord.OPERATION_TYPE = opType.finishLoad; ;//装车结束
            I_DIST_RECORD_LOG log = ConvertToLC.ConvertRecordLog(distRecord);

            #region 写浪潮数据表【送货员操作日志】

            LangchaoLogic lcLogic = new LangchaoLogic();
            lcLogic.WriteDistRecordLog(log, out dohandle);

            #endregion

            #region 写本地服务器数据表【送货员操作日志】

            if (dohandle.IsSuccessful)
            {
                DistRecordLogLogic logLogic = new DistRecordLogLogic();
                distRecord.Id = Guid.NewGuid();
                logLogic.Create(distRecord, out dohandle);
            }
            #endregion

            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "FinishLoad", data, distRecord.USER_ID, dohandle.IsSuccessful);

            #endregion


            var result = jser.Serialize(dohandle);
            return result;
        }

        #endregion

        #region 车辆出库

        /// <summary>
        /// 车辆出库
        /// </summary>
        /// <param name="data"></param>
         [WebMethod]
        public string CarOutWhse(string data)
        {
            
            var jser = new JavaScriptSerializer();
            DoHandle dohandle;
            var distRecord = jser.Deserialize<DistRecordLog>(data);

            var logKey = new LogKeyLogic().GetLogkey();
            OperationType opType;
            distRecord.LOG_SEQ = logKey;
            distRecord.OPERATION_TYPE = opType.carOutWhse; ;//车辆出库
            I_DIST_RECORD_LOG log = ConvertToLC.ConvertRecordLog(distRecord);

            #region 写浪潮数据表【送货员操作日志】

            LangchaoLogic lcLogic = new LangchaoLogic();
            lcLogic.WriteDistRecordLog(log, out dohandle);

            #endregion

            #region 写本地服务器数据表【送货员操作日志】

            if (dohandle.IsSuccessful)
            {
                DistRecordLogLogic logLogic = new DistRecordLogLogic();
                distRecord.Id = Guid.NewGuid();
                logLogic.Create(distRecord, out dohandle);
            }
            #endregion

            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "CarOutWhse", data, distRecord.USER_ID, dohandle.IsSuccessful);

            #endregion

            #region 微信通知
            if (dohandle.IsSuccessful)
            {
                try
                {
                    DistCustLogic distLogic = new DistCustLogic();
                    distLogic.SendWxMsgAfterOut(distRecord.REF_ID);
                }
                catch
                {
 
                }
            }
            #endregion

            var result = jser.Serialize(dohandle);
            return result;

        }

        #endregion

        #region 送货任务开始

        /// <summary>
        /// 送货任务开始
        /// </summary>
        /// <param name="data"></param>
         [WebMethod]
        public string MissionStart(string data)
        {
            var jser = new JavaScriptSerializer();
            DoHandle dohandle;
            var distRecord = jser.Deserialize<DistRecordLog>(data);

            var logKey = new LogKeyLogic().GetLogkey();
            OperationType opType;
            distRecord.LOG_SEQ = logKey;
            distRecord.OPERATION_TYPE = opType.startMission; ;//送货任务开始
            I_DIST_RECORD_LOG log = ConvertToLC.ConvertRecordLog(distRecord);

            #region 写浪潮数据表【送货员操作日志】

            LangchaoLogic lcLogic = new LangchaoLogic();
            lcLogic.WriteDistRecordLog(log, out dohandle);

            #endregion

            #region 写本地服务器数据表【送货员操作日志】

            if (dohandle.IsSuccessful)
            {
                DistRecordLogLogic logLogic = new DistRecordLogLogic();
                distRecord.Id = Guid.NewGuid();
                logLogic.Create(distRecord, out dohandle);
            }
            #endregion

            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "MissionStart", data, distRecord.USER_ID, dohandle.IsSuccessful);

            #endregion


            var result = jser.Serialize(dohandle);
            return result;

        }

        #endregion

        #region 车辆入库

        /// <summary>
        /// 车辆入库
        /// </summary>
        /// <param name="data"></param>
         [WebMethod]
        public string CarInWhse(string data)
        {
            var jser = new JavaScriptSerializer();
            DoHandle dohandle;
            var distRecord = jser.Deserialize<DistRecordLog>(data);

            var logKey = new LogKeyLogic().GetLogkey();
            OperationType opType;
            distRecord.LOG_SEQ = logKey;
            distRecord.OPERATION_TYPE = opType.carInWhse; ;//车辆入库
            I_DIST_RECORD_LOG log = ConvertToLC.ConvertRecordLog(distRecord);

            #region 写浪潮数据表【送货员操作日志】

            LangchaoLogic lcLogic = new LangchaoLogic();
            lcLogic.WriteDistRecordLog(log, out dohandle);

            #endregion

            #region 写本地服务器数据表【送货员操作日志】

            if (dohandle.IsSuccessful)
            {
                DistRecordLogLogic logLogic = new DistRecordLogLogic();
                distRecord.Id = Guid.NewGuid();
                logLogic.Create(distRecord, out dohandle);
            }
            #endregion

            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "CarInWhse", data, distRecord.USER_ID, dohandle.IsSuccessful);

            #endregion



            var result = jser.Serialize(dohandle);
            return result;
              
        }

        #endregion

        #region 送货任务完成

         /// <summary>
         /// 车辆入库
         /// </summary>
         /// <param name="data"></param>
         [WebMethod]
         public string missionFinish(string data)
         {
             var jser = new JavaScriptSerializer();
             DoHandle dohandle;
             var distRecord = jser.Deserialize<DistRecordLog>(data);

             var logKey = new LogKeyLogic().GetLogkey();
             OperationType opType;
             distRecord.LOG_SEQ = logKey;
             distRecord.OPERATION_TYPE = opType.carInWhse; ;//车辆入库
             I_DIST_RECORD_LOG log = ConvertToLC.ConvertRecordLog(distRecord);

             #region 写浪潮数据表【送货员操作日志】

             LangchaoLogic lcLogic = new LangchaoLogic();
             lcLogic.WriteDistRecordLog(log, out dohandle);

             #endregion

             #region 写本地服务器数据表【送货员操作日志】

             if (dohandle.IsSuccessful)
             {
                 DistRecordLogLogic logLogic = new DistRecordLogLogic();
                 distRecord.Id = Guid.NewGuid();
                 logLogic.Create(distRecord, out dohandle);
             }
             #endregion

             #region 写服务器日志

             serverlogLogic.InsertLog("DeliveryConfirm", "CarInWhse", data, distRecord.USER_ID, dohandle.IsSuccessful);

             #endregion



             var result = jser.Serialize(dohandle);
             return result;

         }


        #endregion

        #region 回程登记

         /// <summary>
        /// 车辆回程登记
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod]
        public string BackRegist(string data)
        {
            var jser = new JavaScriptSerializer();
            DoHandle dohandle;
            var param = jser.Deserialize<BackRegistParam>(data);
            DistCarRunLogic logic = new DistCarRunLogic();
            logic.BackRegist(param, out dohandle)
                ;
            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "BackRegist", data, param.CarRun.DLVMAN_ID, dohandle.IsSuccessful);

            #endregion

            var result = jser.Serialize(dohandle);
            return result;
  
        }

        #endregion

        #region 到货确认【含服务评价】

        /// <summary>
        /// 到货确认（含服务评价）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod]
        public string ConfirmDelivery(string data)
        {


            var jser = new JavaScriptSerializer();
            DoHandle dohandle;

            //try
            //{

                var param = jser.Deserialize<DistCust>(data);
                if (param == null)
                {
                    dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "传入参数错误！" };
                    return jser.Serialize(dohandle);
                }
                DistCustLogic logic = new DistCustLogic();
                logic.ConfirmDelivery(param, out dohandle);

                #region 写服务器日志

                serverlogLogic.InsertLog("DeliveryConfirm", "ConfirmDelivery", data, param.DLVMAN_ID, dohandle.IsSuccessful);

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
            var result = jser.Serialize(dohandle);
            return result;
        }


        #endregion

        #region 退货


        /// <summary>
        /// 整单退货
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod]
        public string ReturnAllOrder(string data)
        {
            var jser = new JavaScriptSerializer();
            DoHandle dohandle =new DoHandle();
            var param = jser.Deserialize<DistCust>(data);
            CoReturnLogic logic = new CoReturnLogic();
            logic.ReturnAllOrder(param,out dohandle);


            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "ReturnAllOrder", data, param.DLVMAN_ID, dohandle.IsSuccessful);

            #endregion


            var result = jser.Serialize(dohandle);
            return result;
        }



        /// <summary>
        /// 部分退货
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod]
        public string ReturnPatialOrder(string data)
        {
            var jser = new JavaScriptSerializer();
            DoHandle dohandle;
            var param = jser.Deserialize<ReturnPatialOrderParam>(data);
            CoReturnLogic logic = new CoReturnLogic();
            logic.ReturnPatialOrder(param, out dohandle);
            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "ReturnPatialOrder", data, param.Cust.DLVMAN_ID, dohandle.IsSuccessful);

            #endregion

            var result = jser.Serialize(dohandle);
            return result;
        }

        #endregion

        #region 一键上传

        /// <summary>
        /// 一键上传
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod]
        public string BatchAddedUpload(string data)
        {
            var jser = new JavaScriptSerializer();
            DoHandle dohandle;
            var param = jser.Deserialize<BatchAddedUploadParam>(data);
            LdmDistLogic logic = new LdmDistLogic();
            logic.BatchAddedUpload(param, out dohandle);
            var result = jser.Serialize(dohandle);
            return result;
        }

        #endregion

        #region 零售户信息采集

        /// <summary>
        /// 零售户信息采集
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod]
        public string CollectRetailerXY(string data)
        {
            var jser = new JavaScriptSerializer();
            DoHandle dohandle;
            var param = jser.Deserialize<GisCustPois>(data);
            GisCustPoisLogic logic = new GisCustPoisLogic();
            logic.CollectRetailerXY(param, out dohandle);

            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "CollectRetailerXY", data, param.CRT_USER_ID, dohandle.IsSuccessful);

            #endregion

            var result = jser.Serialize(dohandle);
            return result;
        }

        /// <summary>
        /// 根据零售户ID集合获取零售户位置信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetRetailerLocation(string data)
        {
            var jser = new JavaScriptSerializer();
            var param = jser.Deserialize<GetRetailerLocationParam>(data);
            GisCustPoisLogic logic = new GisCustPoisLogic();
            var queryDatas= logic.GetRetailerLocation(param.CustIds);
            var result = jser.Serialize(queryDatas);
            return result;
        }

        #endregion

        #region 位置上传

        /// <summary>
        /// 送货员实时位置上传
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod]
        public string UploadLocation(string data)
        {
            var jser = new JavaScriptSerializer();
            var param = jser.Deserialize<GisLastLocrecord>(data);
            DoHandle dohandle;
            GisLastLocrecordLogic logic = new GisLastLocrecordLogic();
            logic.UploadLocation(param, out dohandle);
            var result = jser.Serialize(dohandle);
            return result;
        }

        #endregion

        #region 文件操作

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="b"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        [WebMethod]
        public string UpfileByte(string data, string FileByte)
        {
            var jser = new JavaScriptSerializer();
            var param = jser.Deserialize<UpfileByteParam>(data);

            //FileStream readFs = new FileStream(Server.MapPath(@"\source\" + "Koala.jpg"), FileMode.Open, FileAccess.Read); 
            //string FileName = Guid.NewGuid().ToString();
            //byte[] flieByte = new byte[readFs.Length];
            //readFs.Read(flieByte, 0, (int)readFs.Length);

          //  Base64Decoder deCoder = new Base64Decoder();

            byte[] flieByte = Convert.FromBase64String(FileByte);
            DoHandle dohandle = new DoHandle { OperateMsg="上传失败",IsSuccessful=false};
            try
            {
                //写数据库
                DistAttachmentInfo info = new DistAttachmentInfo
                {
                    Id = Guid.NewGuid(),
                    DIST_NUM = param.DIST_NUM,
                    CO_NUM = param.CO_NUM,
                    FileName = param.FILE_NAME,
                    FilePath = Server.MapPath(@"\upfile\" + param.FILE_NAME + "." + param.FILE_TYPE),
                    Extention = param.FILE_TYPE,
                    CreateTime = DateTime.Now,
                    Creator = param.DLVMAN_ID
                };

                DistAttachmentInfoLogic distLogic = new DistAttachmentInfoLogic();
                distLogic.Create(info, out dohandle);

                
                MemoryStream m = new MemoryStream(flieByte);
                using (FileStream fs = File.Open(Server.MapPath(@"\upfile\" + info.Id.ToString()+ "." + param.FILE_TYPE), FileMode.Create))
                {

                    m.WriteTo(fs);
                    m.Close();
                    fs.Close();


                    FileupByteReturn fileReturn = new FileupByteReturn 
                    {
                        IsSuccessful=dohandle.IsSuccessful,
                        OperateMsg=dohandle.OperateMsg,
                        FileId=info.Id.ToString()
                    };
                    #region 写服务器日志

                    serverlogLogic.InsertLog("DeliveryConfirm", "UpfileByte", data, param.DLVMAN_ID, dohandle.IsSuccessful);

                    #endregion

                    var result = jser.Serialize(fileReturn);
                    return result;
                }
            }
            catch (Exception xx) 
            {
                dohandle.OperateMsg = xx.Message;

                #region 写服务器日志

                serverlogLogic.InsertLog("DeliveryConfirm", "UpfileByte", data, param.DLVMAN_ID, dohandle.IsSuccessful);

                #endregion

                var result = jser.Serialize(dohandle);
                return result;
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        [WebMethod]
        public byte[] Downfilebyte(ref string FileName)
        {
            try
            {
                using (FileStream fs = File.Open(Server.MapPath(@"\upfile\" + FileName), FileMode.Open))
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




        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="b"></param>
        /// <param name="FileName"></param>
        /// <returns></returns>
        [WebMethod]
        public string TestUpfileByte()
        {
            string data = "{'CO_NUM':'XGY30001246196','DIST_NUM':'GYPS000000117635','DLVMAN_ID':'GYO00000000000621','FILE_NAME':'GYO00000000000621_20150128HH2549.jpg','FILE_TYPE':'jpeg','IsSuccessful':false}";
            string FileByte = "";
            var jser = new JavaScriptSerializer();
            var param = jser.Deserialize<UpfileByteParam>(data);

            //FileStream readFs = new FileStream(Server.MapPath(@"\source\" + "Koala.jpg"), FileMode.Open, FileAccess.Read); 
            //string FileName = Guid.NewGuid().ToString();
            //byte[] flieByte = new byte[readFs.Length];
            //readFs.Read(flieByte, 0, (int)readFs.Length);


            byte[] flieByte = Convert.FromBase64String(FileByte);
            DoHandle dohandle = new DoHandle { OperateMsg = "上传失败", IsSuccessful = false };
            try
            {

                MemoryStream m = new MemoryStream(flieByte);
                using (FileStream fs = File.Open(Server.MapPath(@"\upfile\" + param.FILE_NAME + "." + param.FILE_TYPE), FileMode.Create))
                {

                    m.WriteTo(fs);
                    m.Close();
                    fs.Close();

                    //写数据库
                    DistAttachmentInfo info = new DistAttachmentInfo
                    {
                        Id = Guid.NewGuid(),
                        DIST_NUM = param.DIST_NUM,
                        CO_NUM = param.CO_NUM,
                        FileName = param.FILE_NAME,
                        FilePath = Server.MapPath(@"\upfile\" + param.FILE_NAME + "." + param.FILE_TYPE),
                        Extention = param.FILE_TYPE,
                        CreateTime = DateTime.Now,
                        Creator = param.DLVMAN_ID
                    };

                    DistAttachmentInfoLogic distLogic = new DistAttachmentInfoLogic();
                    distLogic.Create(info, out dohandle);

                    FileupByteReturn fileReturn = new FileupByteReturn
                    {
                        IsSuccessful = dohandle.IsSuccessful,
                        OperateMsg = dohandle.OperateMsg,
                        FileId = info.Id.ToString()
                    };

                    var result = jser.Serialize(fileReturn);
                    return result;
                }
            }
            catch (Exception xx)
            {
                dohandle.OperateMsg = xx.Message;
                var result = jser.Serialize(dohandle);
                return result;
            }
        }



        #region 同步订单信息

        /// <summary>
        /// 根据配送单号同步订单信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod]
        public string SynOrders(string data)
        {
            var jser = new JavaScriptSerializer();
            var param = jser.Deserialize<SynOrderParam>(data);
            LdmDistLineLogic logic = new LdmDistLineLogic();
            var orderInfos=   logic.SynOrders(param); 
            var result = jser.Serialize(orderInfos);
            return result;
        }


        #endregion


        #region 获取车辆上期里程

        /// <summary>
        /// 获取车辆上期里程
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetPreMile(string data)
        {
            var jser = new JavaScriptSerializer();
            var param = jser.Deserialize<GetPreMileParam>(data);
            DistCarRunLogic logic = new DistCarRunLogic();
            var returnData = logic.GetPreMile(param.CAR_ID);
            var result = jser.Serialize(returnData);
            return result;
        }

        #endregion

        #region 微信通知客户

        /// <summary>
        /// 微信通知客户
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod]
        public string WeiXinNotify(string data)
        {
            var jser = new JavaScriptSerializer();
            DoHandle dohandle;
            var param = jser.Deserialize<WeiXinNotifyParam>(data);
            DistCustLogic logic = new DistCustLogic();
            logic.WeiXinNotify(param, out dohandle);

            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "WeiXinNotify", data, param.CO_NUM, dohandle.IsSuccessful);

            #endregion


            var result = jser.Serialize(dohandle);
            return result;
        }



        #endregion

        #region 同步配送单日志

        /// <summary>
        /// 同步配送单日志
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod]
        public string SynLdmDist(string data)
        {
            var jser = new JavaScriptSerializer();
            var param = jser.Deserialize<SynLdmDistParam>(data);
            DistRecordLogLogic logic = new DistRecordLogLogic();
            var records = logic.SynLdmDist(param);

            SynOrderParam orderParam = new SynOrderParam { DIST_NUM = param.DIST_NUM };
            LdmDistLineLogic orderlogic = new LdmDistLineLogic();
            var orderInfos = orderlogic.SynOrders(orderParam);
            
            SynLdmInfoData totalData = new SynLdmInfoData { Logs =records.ToList(),OrderInfo=orderInfos.ToList() };

            var result = jser.Serialize(totalData);

            return result;
        }



        #endregion

        #region NFC查询客户信息

        /// <summary>
        /// NFC查询客户信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod]
        public string GetNfcCustomer(string data)
        {
            var jser = new JavaScriptSerializer();
            var param = jser.Deserialize<GetNfcCustomerParam>(data);
            RetailerLogic logic = new RetailerLogic();
            var returnData = logic.GetNfcCustomer(param);
            var result = jser.Serialize(returnData);
            return result;

        }

        #endregion


        #region 下载配送单(根据日期)

        /// <summary>
        /// 下载配送单
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod]
        public string DownloadDistByDate(string data)
        {
            var jser = new JavaScriptSerializer();
            var param = jser.Deserialize<DownloadDistByDateParam>(data);
            DoHandle dohandle;
            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "DownloadDistByDate", data, param.DLVMAN_ID, true);

            #endregion

            #region 取浪潮配送单数据

            List<LdmDist> ldmDists = new List<LdmDist>();//配送单
            List<LdmDistLine> ldmDistLines = new List<LdmDistLine>();//配送单明细
            List<LdmDisItem> ldmDisItems = new List<LdmDisItem>();//配送单商品信息
            List<I_CO_TEMP_RETURN> tmpReturns = new List<I_CO_TEMP_RETURN>();//再出库暂存订单


            LangchaoLogic lcLogic = new LangchaoLogic();
            lcLogic.DownloadDistByDate(param, out ldmDists, out ldmDistLines, out ldmDisItems,out tmpReturns);

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


            if (dohandle.IsSuccessful)
            {

                #region 返回配送单到终端

                LdmInfo ldmInfo = new LdmInfo { LdmDists = ldmDists, LdmDistLines = ldmDistLines, LdmDisItems = ldmDisItems };
                var result = jser.Serialize(ldmInfo);

                #endregion


                return result;
            }

            else
            {
                var result = jser.Serialize(dohandle);
                return result;
            }
        }


        #endregion

    }
}
