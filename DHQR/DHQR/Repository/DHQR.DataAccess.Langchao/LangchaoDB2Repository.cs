using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Common.Base;
using DHQR.DataAccess.Entities;

namespace DHQR.DataAccess.Langchao
{
    /// <summary>
    /// 浪潮DB2数据访问层
    /// </summary>
    public class LangchaoDB2Repository
    {

        #region 同步零售户

        /// <summary>
        /// 获取零售户信息
        /// </summary>
        /// <param name="COM_ID">公司ID</param>
        /// <returns></returns>
        public List<I_CUST> GetCustomer(string COM_ID)
        {
            IDictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("COM_ID",COM_ID);
            //dic.Add("STATUS", "01");
            //dic.Add("STATUS", "02");
            var result = new DB2Helper<I_CUST>().QueryData(dic);
            return result;
        }


        #endregion

        #region 同步车辆和配送员信息

        /// <summary>
        /// 获取所有配送车辆
        /// </summary>
        /// <param name="COM_ID">公司ID</param>
        /// <returns></returns>
        public List<I_DIST_CAR> GetDistCar(string COM_ID)
        {
            IDictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("COM_ID", COM_ID);
            var result = new DB2Helper<I_DIST_CAR>().QueryData(dic);
            return result;

        }

        #endregion

        #region 同步配送员信息

        /// <summary>
        /// 同步配送员信息
        /// </summary>
        /// <param name="COM_ID">公司ID</param>
        /// <returns></returns>
        public List<I_DIST_DLVMAN> GetDistDlvmans(string COM_ID)
        {
            IDictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("COM_ID", COM_ID);
            var result = new DB2Helper<I_DIST_DLVMAN>().QueryData(dic);
            return result;

        }

        #endregion

        #region 下载配送单

        /// <summary>
        /// 从浪潮下载配送单信息
        /// </summary>
        /// <param name="param"></param>
        /// <param name="ldmDists"></param>
        /// <param name="ldmDistLines"></param>
        /// <param name="ldmDisItems"></param>
        public void DownloadDists(DownloadDistParam param,int distDate ,out List<I_DIST> ldmDists, out List<I_DIST_LINE> ldmDistLines, out List<I_DIST_ITEM> ldmDisItems)
        {
            //当天时间字符串
            string currentDate =DateTime.Now.AddDays(-1).AddDays(-distDate).ToString("yyyyMMdd");

            
            IDictionary<string, string> dic1 = new Dictionary<string, string> ();
            IDictionary<string, IList<string>> dic2 = new Dictionary<string, IList<string>>();
            IDictionary<string, IList<string>> dic3 = new Dictionary<string, IList<string>>();
            dic1.Add("DLVMAN_ID", param.DLVMAN_ID);
            dic1.Add("DIST_DATE", currentDate);

            ldmDists = new DB2Helper<I_DIST>().QueryData(dic1);

            if (ldmDists.Count != 0)
            {
                IList<string> distNums = ldmDists.Select(f => f.DIST_NUM).ToList();

                dic2.Add("DIST_NUM", distNums);

                ldmDistLines = new DB2Helper<I_DIST_LINE>().QueryData(new Dictionary<string, string>(), dic2);
                IList<string> coNums = ldmDistLines.Select(f => f.CO_NUM).ToList();

                dic3.Add("CO_NUM", coNums);
                // ldmDisItems = new List<I_DIST_ITEM>();
                ldmDisItems = new DB2Helper<I_DIST_ITEM>().QueryData(new Dictionary<string, string>(), dic3);
            }
            else
            {
                ldmDistLines = new List<I_DIST_LINE>();
                ldmDisItems = new List<I_DIST_ITEM>();
            }
        }

        #endregion

        #region 下载配送单

        /// <summary>
        /// 从浪潮下载配送单信息
        /// </summary>
        /// <param name="param"></param>
        /// <param name="ldmDists"></param>
        /// <param name="ldmDistLines"></param>
        /// <param name="ldmDisItems"></param>
        public void DownloadDistByDate(DownloadDistByDateParam param, out List<I_DIST> ldmDists, out List<I_DIST_LINE> ldmDistLines, out List<I_DIST_ITEM> ldmDisItems,out List<I_CO_TEMP_RETURN> coTempReturns)
        {
            //取日期字符串集合
            IList<DateTime> realDateTime = param.DistDate.Select(f => DateTime.Parse(f)).ToList();
            IList<string> realDateStr = new List<string>();
            foreach (var item in realDateTime)
            {
                var rs = item.AddDays(-1).ToString("yyyyMMdd");
                realDateStr.Add(rs);
            }

            ldmDists = new List<I_DIST>();
            ldmDistLines = new List<I_DIST_LINE>();
            ldmDisItems = new List<I_DIST_ITEM>();
            coTempReturns = new List<I_CO_TEMP_RETURN>();
            foreach (var item in realDateStr)
            {
                IDictionary<string, string> dic1 = new Dictionary<string, string>();
                IDictionary<string, IList<string>> dic2 = new Dictionary<string, IList<string>>();
                IDictionary<string, IList<string>> dic3 = new Dictionary<string, IList<string>>();

                //查询暂存订单参数
                IDictionary<string, IList<string>> dic4 = new Dictionary<string, IList<string>>();

                dic1.Add("DLVMAN_ID", param.DLVMAN_ID);
                dic1.Add("DIST_DATE", item);
                var currentDist = new DB2Helper<I_DIST>().QueryData(dic1);
                ldmDists.AddRange(currentDist);
                if (currentDist.Count != 0)
                {
                    IList<string> distNums = currentDist.Select(f => f.DIST_NUM).Distinct().ToList();

                    dic2.Add("DIST_NUM", distNums);

                    //正常订单
                   var currentDistLine = new DB2Helper<I_DIST_LINE>().QueryData(new Dictionary<string, string>(), dic2);
                   ldmDistLines.AddRange(currentDistLine);


                   dic4.Add("OUT_DIST_NUM", distNums);
                    //暂存订单
                   var currentCoTempReturn = new DB2Helper<I_CO_TEMP_RETURN>().QueryData(new Dictionary<string, string>(), dic4);
                   coTempReturns.AddRange(currentCoTempReturn);
                   List<string> coTempReturnNums = coTempReturns.Select(f => f.CO_NUM).ToList();
                  

                   List<string> coNums = currentDistLine.Select(f => f.CO_NUM).ToList();
                   coNums.AddRange(coTempReturnNums);

                    dic3.Add("CO_NUM", coNums);
                    var currentDisItems = new DB2Helper<I_DIST_ITEM>().QueryData(new Dictionary<string, string>(), dic3);
                    ldmDisItems.AddRange(currentDisItems);
                }
            }


        }

        #endregion

        #region 备车检查

        /// <summary>
        /// 备车检查
        /// </summary>
        /// <param name="chek"></param>
        /// <param name="dohandle"></param>
        public void CheckCar(List<I_DIST_CAR_CHECK> cheks, I_DIST_RECORD_LOG log, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "管控平台操作失败" };

            CommonDB2 db2Rep = new CommonDB2();
            DB2Helper<I_DIST_CAR_CHECK> chekHelper = new DB2Helper<I_DIST_CAR_CHECK>();
            DB2Helper<I_DIST_RECORD_LOG> recordHelper = new DB2Helper<I_DIST_RECORD_LOG>();


            db2Rep.BeginTrans();

            //向I_DIST_CAR_CHECK写入数据
            chekHelper.Insert(cheks, db2Rep,out dohandle);

            ////向I_DIST_RECORD_LOG写入数据
            recordHelper.Insert(log,db2Rep,out dohandle);


            try
            {
                db2Rep.CommitTrans();
                dohandle.IsSuccessful = true;
                dohandle.OperateMsg = "备车检查成功";
            }
            catch(Exception ex)
            {
                dohandle.IsSuccessful = true;
                dohandle.OperateMsg = ex.Message;
 
            }
        }


        #endregion

        #region 写操作日志

        /// <summary>
        /// 写操作日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="dohanle"></param>
        public void WriteDistRecordLog(I_DIST_RECORD_LOG log, out DoHandle dohanle)
        {
            dohanle = new DoHandle { IsSuccessful = false, OperateMsg = "管控平台操作失败!" };
            DB2Helper<I_DIST_RECORD_LOG> recordHelper = new DB2Helper<I_DIST_RECORD_LOG>();

            ////向I_DIST_RECORD_LOG写入数据
            recordHelper.Insert(log, out dohanle);
            dohanle.IsSuccessful = true;
            dohanle.OperateMsg = "操作成功";
        }

        /// <summary>
        /// 写操作日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="dohanle"></param>
         public void WriteDistRecordLog(IList<I_DIST_RECORD_LOG> logs, out DoHandle dohanle)
        {
            dohanle = new DoHandle { IsSuccessful = false, OperateMsg = "管控平台操作失败!" };
            DB2Helper<I_DIST_RECORD_LOG> recordHelper = new DB2Helper<I_DIST_RECORD_LOG>();

            ////向I_DIST_RECORD_LOG写入数据
            recordHelper.Insert(logs, out dohanle);

        }


        #endregion

        #region 车辆回程登记

        /// <summary>
        /// 车辆回程登记
        /// </summary>
        /// <param name="carRun"></param>
        /// <param name="carLines"></param>
        /// <param name="log"></param>
        /// <param name="dohandle"></param>
        public void BackRegist(I_DIST_CAR_RUN carRun, List<I_DIST_CAR_RUN_LINE> carLines, I_DIST_RECORD_LOG log, out DoHandle dohandle)
        {

            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "管控平台操作失败" };
            CommonDB2 db2Rep = new CommonDB2();
            DB2Helper<I_DIST_CAR_RUN> runHelper = new DB2Helper<I_DIST_CAR_RUN>();
            DB2Helper<I_DIST_CAR_RUN_LINE> lineHelper = new DB2Helper<I_DIST_CAR_RUN_LINE>();
            DB2Helper<I_DIST_RECORD_LOG> recordHelper = new DB2Helper<I_DIST_RECORD_LOG>();
            //db2Rep.BeginTrans();


            //向I_DIST_CAR_RUN写入数据
            runHelper.Insert(carRun, db2Rep, out dohandle);

            var toInsertItems = carLines.OrderBy(f => f.COST_TYPE).ToList();
            foreach (var item in toInsertItems)
            {
                lineHelper.Insert(item,out dohandle);
            }
            /*
            ////向I_DIST_CAR_RUN_LINE写入数据
            lineHelper.Insert(carLines, db2Rep, out dohandle);
             */





            ////向I_DIST_RECORD_LOG写入数据
            recordHelper.Insert(log,db2Rep,out dohandle);

            try
            {
                //db2Rep.CommitTrans();
                dohandle.IsSuccessful = true;
                dohandle.OperateMsg = "操作成功";
            }
            catch (Exception ex)
            {
                dohandle.IsSuccessful = true;
                dohandle.OperateMsg = ex.Message;
            }

        }


        #endregion

        #region 到货确认

        /// <summary>
        /// 到货确认
        /// </summary>
        /// <param name="dist"></param>
        /// <param name="dohandle"></param>
        public void ConfirmDelivery(I_DIST_CUST dist, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "管控平台操作失败" };
            DB2Helper<I_DIST_CUST> custHelper = new DB2Helper<I_DIST_CUST>();
            IDictionary<string, string> dic1 = new Dictionary<string, string>();
            dic1.Add("DIST_NUM", dist.DIST_NUM);
            dic1.Add("CO_NUM", dist.CO_NUM);
            var hasConfirm = custHelper.Any(dic1);
            if (hasConfirm)
            {
                dohandle.IsSuccessful = true;
                dohandle.OperateMsg = string.Format("订单：{0}已经到货确认，请不要重复提交！", dist.CO_NUM);
                return;
            }
            custHelper.Insert(dist, out dohandle);
        }

        /// <summary>
        /// 暂存订单到货确认
        /// </summary>
        /// <param name="dist"></param>
        /// <param name="dohandle"></param>
        public void ConfirmCoTemp(I_DIST_CUST dist, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "管控平台操作失败" };
            DB2Helper<I_DIST_CUST> custHelper = new DB2Helper<I_DIST_CUST>();
            IDictionary<string, string> dic1 = new Dictionary<string, string>();
            dic1.Add("DIST_NUM", dist.DIST_NUM);
            dic1.Add("CO_NUM", dist.CO_NUM);
            var hasConfirm = custHelper.Any(dic1);
            if (hasConfirm)
            {
                //更新 I_DIST_CUST
                IDictionary<string, string> updic = new Dictionary<string, string>();
                updic.Add("IS_RECEIVED", "01");
                updic.Add("UNUSUAL_TYPE", "");
                custHelper.Update(updic, dic1, out dohandle);
            }
            else
            {
                custHelper.Insert(dist, out dohandle);
            }

            //更新 I_CO_TEMP
            IDictionary<string, string> dic2 = new Dictionary<string, string>();
            dic2.Add("DIST_NUM", dist.DIST_NUM);
            dic2.Add("CO_NUM", dist.CO_NUM);
            IDictionary<string, string> updic2 = new Dictionary<string, string>();
            updic2.Add("STATUS", "03");
            updic2.Add("FINISH_TIME", dist.EVALUATE_TIME);
            updic2.Add("FINISH_USER_ID", dist.DLVMAN_ID);

            DB2Helper<I_CO_TEMP> tmpHelper = new DB2Helper<I_CO_TEMP>();
            tmpHelper.Update(updic2, dic2, out dohandle);

        }



        #endregion

        #region 暂存登记

        /// <summary>
        /// 暂存登记
        /// </summary>
        /// <param name="dist"></param>
        /// <param name="dohandle"></param>
        public void CoTmpRegist (I_CO_TEMP tmp, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "管控平台操作失败" };
            DB2Helper<I_CO_TEMP> custHelper = new DB2Helper<I_CO_TEMP>();
            custHelper.Insert(tmp, out dohandle);
        }

        /// <summary>
        /// 暂存订单再次暂存更新
        /// </summary>
        /// <param name="dist"></param>
        /// <param name="dohandle"></param>
        public void CoTmpUpdate(I_CO_TEMP tmp, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "管控平台操作失败" };
            DB2Helper<I_CO_TEMP> custHelper = new DB2Helper<I_CO_TEMP>();
            IDictionary<string, string> dic1 = new Dictionary<string, string>();
            dic1.Add("DIST_NUM", tmp.DIST_NUM);
            dic1.Add("CO_NUM", tmp.CO_NUM);

            IDictionary<string, string> updic = new Dictionary<string, string>();
            updic.Add("RET_TIME", tmp.RET_TIME);
            updic.Add("RET_USER_ID", tmp.RET_USER_ID);
            updic.Add("STATUS", tmp.STATUS);

            custHelper.Update(updic, dic1, out dohandle);
        }



        #endregion

        #region 退货


        /// <summary>
        /// 整单退货
        /// </summary>
        /// <param name="cust"></param>
        /// <param name="dohandle"></param>
        public void ReturnAllOrder(I_DIST_CUST cust, string RETURN_CO_NUM, out DoHandle dohandle)
        {

            DB2Helper<I_DIST_CUST> custHelper = new DB2Helper<I_DIST_CUST>();
            IDictionary<string, string> dic1 = new Dictionary<string, string>();
            dic1.Add("DIST_NUM", cust.DIST_NUM);
            dic1.Add("CO_NUM", cust.CO_NUM);

            var hasDone = custHelper.Any(dic1);
            if (hasDone)
            {
                dohandle = new DoHandle();
                dohandle.IsSuccessful = false;
                dohandle.OperateMsg = string.Format("订单:{0}已经做到货确认，请不要重复操作!",cust.CO_NUM);
                return;
            }

            DB2Helper<I_DIST_LINE> lineRep = new DB2Helper<I_DIST_LINE>();
            DB2Helper<I_DIST_ITEM> itemRep = new DB2Helper<I_DIST_ITEM>();
            IDictionary<string, string> lineDic = new Dictionary<string, string>();
            IDictionary<string, string> itemDic = new Dictionary<string, string>();

            lineDic.Add("DIST_NUM", cust.DIST_NUM);
            lineDic.Add("CO_NUM", cust.CO_NUM);
            I_DIST_LINE order = lineRep.FirstOrDefault(lineDic);

            itemDic.Add("CO_NUM", cust.CO_NUM);

            List<I_DIST_ITEM> orderDetails = itemRep.QueryData(itemDic);
            I_CO_RETURN retunOrder = new I_CO_RETURN 
            {
                RETURN_CO_NUM = RETURN_CO_NUM,
                CUST_ID=order.CUST_ID,
                TYPE="01",
                STATUS="01",
                CRT_DATE=DateTime.Now.ToString("yyyyMMdd"),
                CRT_USER_NAME=string.Empty,//暂不确定
                ORG_CO_NUM=order.CO_NUM,
                AMT_SUM=order.AMT_AR,
                QTY_SUM=order.QTY_BAR
            };

            List<I_CO_RETURN_LINE> lines = new List<I_CO_RETURN_LINE>();
            int i = 1;
            foreach (var t in orderDetails)
            {
                I_CO_RETURN_LINE l = new I_CO_RETURN_LINE
                {
                    RETURN_CO_NUM = order.CO_NUM,//为什么要传订单号？
                    LINE_NUM =i,
                    ITEM_ID = t.ITEM_ID,
                    QTY_ORD = t.QTY,
                    NOTE="正式数据"
                };
                lines.Add(l);
                i++;
            }

            ReturnPartialOrder(cust, retunOrder, lines, out dohandle);

        }

        /// <summary>
        /// 部分退货
        /// </summary>
        /// <param name="cust"></param>
        /// <param name="order"></param>
        /// <param name="lines"></param>
        /// <param name="dohandle"></param>
        public void ReturnPartialOrder(I_DIST_CUST cust, I_CO_RETURN order, List<I_CO_RETURN_LINE> lines, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "管控平台处理失败" };
            DB2Helper<I_DIST_CUST> custHelper = new DB2Helper<I_DIST_CUST>();
            IDictionary<string, string> dic1 = new Dictionary<string, string>();
            dic1.Add("DIST_NUM", cust.DIST_NUM);
            dic1.Add("CO_NUM", cust.CO_NUM);

            var hasDone = custHelper.Any(dic1);
            if (hasDone)
            {
                dohandle = new DoHandle();
                dohandle.IsSuccessful = false;
                dohandle.OperateMsg = string.Format("订单:{0}已经做到货确认，请不要重复操作!", cust.CO_NUM);
                return;
            }

            DB2Helper<I_CO_RETURN> reorderRep = new DB2Helper<I_CO_RETURN>();
            DB2Helper<I_CO_RETURN_LINE> lineRep = new DB2Helper<I_CO_RETURN_LINE>();


            /*  事务处理  */
            //CommonDB2 db2Rep = new CommonDB2();
            //db2Rep.BeginTrans();
            custHelper.Insert(cust,out dohandle);
            reorderRep.Insert(order, out dohandle);

            int i = 1;
            foreach (var item in lines)
            {
                item.LINE_NUM = i;
                lineRep.Insert(item, out dohandle);
                i++;
            }

            //try
            //{
                //db2Rep.CommitTrans();
                dohandle.IsSuccessful = true;
                dohandle.OperateMsg="操作成功";
            //}
            //catch (Exception ex)
            //{ 
            //     dohandle.IsSuccessful = false;
            //     dohandle.OperateMsg = ex.Message;           
            //}
        }


        #endregion

        #region 零售户位置信息采集

        /// <summary>
        /// 零售户位置信息采集
        /// </summary>
        /// <param name="poi"></param>
        /// <param name="dohandle"></param>
        public void CollectRetailerXY(I_GIS_CUST_POI poi, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "管控平台处理失败" };
            DB2Helper<I_GIS_CUST_POI> custHelper = new DB2Helper<I_GIS_CUST_POI>();
            IDictionary<string, string> dic1 = new Dictionary<string, string>();
            dic1.Add("CUST_ID", poi.CUST_ID);
            var hasCust = custHelper.Any(dic1);
            if (!hasCust)
            {
                custHelper.Insert(poi, out dohandle);
            }
            else
            {
                IDictionary<string, string> updic = new Dictionary<string, string>();
                updic.Add("ORIGINAL_LATITUDE", poi.ORIGINAL_LATITUDE.ToString());
                updic.Add("ORIGINAL_LONGITUDE", poi.ORIGINAL_LONGITUDE.ToString());

                custHelper.Update(updic,dic1,out dohandle);
            }
        }

        #endregion

        #region 位置上传

        /// <summary>
        /// 配送员位置上传
        /// </summary>
        /// <param name="poi"></param>
        /// <param name="dohandle"></param>
        public void UploadLocation(I_GIS_LAST_LOCRECORD poi, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "管控平台处理失败" };
            DB2Helper<I_GIS_LAST_LOCRECORD> custHelper = new DB2Helper<I_GIS_LAST_LOCRECORD>();
            custHelper.Insert(poi, out dohandle);
        }

        #endregion

        #region 根据配送员ID获取当日配送单

        /// <summary>
        /// 获取当日配送单
        /// </summary>
        /// <param name="DlvManId"></param>
        /// <returns></returns>
        public I_DIST GetDistByDlvmanId(string DlvManId)
        {
            //当天时间字符串
            string currentDate = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
            IDictionary<string, string> dic1 = new Dictionary<string, string>();
            dic1.Add("DLVMAN_ID", DlvManId);
            var dist = new DB2Helper<I_DIST>().FirstOrDefault(dic1);
            return dist;
        }

        #endregion

        #region 同步线路信息

        /// <summary>
        /// 同步线路信息
        /// </summary>
        /// <param name="COM_ID">公司ID</param>
        /// <returns></returns>
        public List<I_DIST_RUT> GetRutList(string COM_ID)
        {
            IDictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("COM_ID", COM_ID);
            var result = new DB2Helper<I_DIST_RUT>().QueryData(dic);
            return result;

        }

        #endregion


        #region 微信获取订单明细

        /// <summary>
        /// 根据订单号获取订单明细
        /// </summary>
        /// <param name="CO_NUM"></param>
        /// <returns></returns>
        public List<I_DIST_ITEM> GetByCoNum(string CO_NUM)
        {
            IDictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CO_NUM", CO_NUM);
            var result = new DB2Helper<I_DIST_ITEM>().QueryData(dic);
            return result;
        }

        #endregion


        #region 获取零售户位置信息

        /// <summary>
        /// 获取零售户位置信息
        /// </summary>
        /// <returns></returns>
        public IList<I_GIS_CUST_POI> GetCustPois()
        {
            IDictionary<string, string> dic = new Dictionary<string, string>();
            var result = new DB2Helper<I_GIS_CUST_POI>().QueryData(dic);
            return result;

        }
        #endregion 


        #region 批量同步到货确认信息

        /// <summary>
        /// 批量同步到货确认信息
        /// </summary>
        /// <param name="dist"></param>
        /// <param name="dohandle"></param>
        public void BatchSynDistCust(IList<I_DIST_CUST> dists, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "管控平台操作失败" };

            foreach (var dist in dists)
            {
                DB2Helper<I_DIST_CUST> custHelper = new DB2Helper<I_DIST_CUST>();
                IDictionary<string, string> dic1 = new Dictionary<string, string>();
                dic1.Add("DIST_NUM", dist.DIST_NUM);
                dic1.Add("CO_NUM", dist.CO_NUM);
                custHelper.Insert(dist, out dohandle);
            }
        }

        #endregion


    }
}
