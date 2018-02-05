using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Base;
using DHQR.DataAccess.Entities;

namespace DHQR.DataAccess.Langchao
{
    public class LangchaoRepository
    {
        /*
        public TestEntities activeContext;

        /// <summary>
        /// 构造函数
        /// </summary>
        public LangchaoRepository()
        {
            activeContext = new TestEntities();
        }
         */

        #region 同步零售户

        /// <summary>
        /// 获取零售户信息
        /// </summary>
        /// <param name="COM_ID">公司ID</param>
        /// <returns></returns>
        public List<I_CUST> GetCustomer(string COM_ID)
        {
            //var result = activeContext.I_CUST.Where(f => f.COM_ID == COM_ID).ToList();
            //return result;
            return null;
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
            //var result = activeContext.I_DIST_CAR.Where(f => f.COM_ID ==COM_ID).ToList();
            //return result;
            return null;

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
        public void DownloadDists(DownloadDistParam param,out List<I_DIST> ldmDists,out List<I_DIST_LINE> ldmDistLines, out List<I_DIST_ITEM> ldmDisItems)
        {
            //当天时间字符串
            //string currentDate = "20141216";//DateTime.Now.ToString("yyyyMMdd");
            //ldmDists = activeContext.I_DIST.Where(f => f.CAR_ID == param.CAR_ID
            //                                //&& f.DLVMAN_ID == param.DLVMAN_ID
            //                                //&& f.DRIVER_ID == param.DRIVER_ID
            //                                && f.DIST_DATE==currentDate
            //                                ).ToList();
            //IList<string> distNums = ldmDists.Select(f => f.DIST_NUM).ToList();
            //ldmDistLines = activeContext.I_DIST_LINE.Where(f => distNums.Contains(f.DIST_NUM)).ToList();
            //IList<string> coNums = ldmDistLines.Select(f => f.CO_NUM).ToList();
            //ldmDisItems = activeContext.I_DIST_ITEM.Where(f => coNums.Contains(f.CO_NUM)).ToList();   
            ldmDists = new List<I_DIST>();
            ldmDistLines = new List<I_DIST_LINE>();
            ldmDisItems = new List<I_DIST_ITEM>();
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

            //向I_DIST_CAR_CHECK写入数据
            //foreach (var chek in cheks)
            //{
            //    activeContext.I_DIST_CAR_CHECK.AddObject(chek);
            //}

            ////向I_DIST_RECORD_LOG写入数据
            //activeContext.I_DIST_RECORD_LOG.AddObject(log);

            //activeContext.SaveChanges();
            dohandle.IsSuccessful = true;
            dohandle.OperateMsg = "备车检查成功";
        }


        #endregion

        #region 写操作日志

        /// <summary>
        /// 写操作日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="dohanle"></param>
        public void WriteDistRecordLog(I_DIST_RECORD_LOG log,out DoHandle dohanle)
        {
            dohanle = new DoHandle { IsSuccessful = false, OperateMsg = "管控平台操作失败!" };
            //activeContext.I_DIST_RECORD_LOG.AddObject(log);
            //activeContext.SaveChanges();
            dohanle.IsSuccessful = true;
            dohanle.OperateMsg = "操作成功";
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
        public void BackRegist(I_DIST_CAR_RUN carRun,List<I_DIST_CAR_RUN_LINE> carLines ,I_DIST_RECORD_LOG log,out DoHandle dohandle)
        {

            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "管控平台操作失败" };
            //activeContext.I_DIST_CAR_RUN.AddObject(carRun);
            //foreach(var item in carLines)
            //{
            //    activeContext.I_DIST_CAR_RUN_LINE.AddObject(item);
            //}
            //activeContext.I_DIST_RECORD_LOG.AddObject(log);
            //activeContext.SaveChanges();
            dohandle.IsSuccessful = true;
            dohandle.OperateMsg = "操作成功";

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
            //var hasConfirm = activeContext.I_DIST_CUST.Any(f => f.DIST_NUM == dist.DIST_NUM && f.CO_NUM == dist.CO_NUM);
            //if (hasConfirm)
            //{
            //    dohandle.IsSuccessful = false;
            //    dohandle.OperateMsg = string.Format("订单：{0}已经到货确认，请不要重复提交！",dist.CO_NUM);
            //    return;
            //}
            //activeContext.I_DIST_CUST.AddObject(dist);
            //activeContext.SaveChanges();
            dohandle.IsSuccessful = true;
            dohandle.OperateMsg = "操作成功";

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
            /*
            var hasDone = activeContext.I_DIST_CUST.Any(f => f.DIST_NUM == cust.DIST_NUM && f.CO_NUM == cust.CO_NUM);
            if (hasDone)
            {
                dohandle = new DoHandle();
                dohandle.IsSuccessful = false;
                dohandle.OperateMsg = string.Format("订单:{0}已经做到货确认，请不要重复操作!",cust.CO_NUM);
                return;
            }

            I_DIST_LINE order = activeContext.I_DIST_LINE.SingleOrDefault(f => f.DIST_NUM == cust.DIST_NUM && f.CO_NUM == cust.CO_NUM);
            List<I_DIST_ITEM> orderDetails = activeContext.I_DIST_ITEM.Where(f => f.CO_NUM == order.CO_NUM).ToList();
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
                    NOTE="测试"
                };
                lines.Add(l);
                i++;
            }

            ReturnPartialOrder(cust, retunOrder, lines, out dohandle);
             */
            dohandle = new DoHandle();

        }
        
        /// <summary>
        /// 部分退货
        /// </summary>
        /// <param name="cust"></param>
        /// <param name="order"></param>
        /// <param name="lines"></param>
        /// <param name="dohandle"></param>
        public void ReturnPartialOrder(I_DIST_CUST cust,I_CO_RETURN order,List<I_CO_RETURN_LINE> lines,out DoHandle dohandle)
        {
            dohandle = new DoHandle {IsSuccessful=false,OperateMsg="管控平台处理失败" };
            //var hasDone = activeContext.I_DIST_CUST.Any(f => f.DIST_NUM == cust.DIST_NUM && f.CO_NUM == cust.CO_NUM);
            //if (hasDone)
            //{
            //    dohandle = new DoHandle();
            //    dohandle.IsSuccessful = false;
            //    dohandle.OperateMsg = string.Format("订单:{0}已经做到货确认，请不要重复操作!",cust.CO_NUM);
            //    return;
            //}

            //activeContext.I_DIST_CUST.AddObject(cust);
            //activeContext.I_CO_RETURN.AddObject(order);
            //int i = 1;
            //foreach (var item in lines)
            //{
            //    item.LINE_NUM = i;
            //    activeContext.I_CO_RETURN_LINE.AddObject(item);
            //    i++;
            //}
            //activeContext.SaveChanges();
            //dohandle.IsSuccessful = true;
            //dohandle.OperateMsg = "操作成功";
        }


        #endregion

        #region 零售户位置信息采集

        /// <summary>
        /// 零售户位置信息采集
        /// </summary>
        /// <param name="poi"></param>
        /// <param name="dohandle"></param>
        public void CollectRetailerXY(I_GIS_CUST_POI poi ,out DoHandle dohandle)
        {
            dohandle = new DoHandle {IsSuccessful=false,OperateMsg="管控平台处理失败" };
            //var cust = activeContext.I_GIS_CUST_POI.SingleOrDefault(f => f.CUST_ID == poi.CUST_ID);
            //if (cust == null)
            //{
            //    activeContext.I_GIS_CUST_POI.AddObject(poi);
            //}
            //else
            //{
            //    cust.ORIGINAL_LATITUDE = poi.ORIGINAL_LATITUDE;
            //    cust.ORIGINAL_LONGITUDE = poi.ORIGINAL_LONGITUDE;
            //}
            //activeContext.SaveChanges();
            //dohandle.IsSuccessful = true;
            //dohandle.OperateMsg = "处理成功";
        }

        #endregion

        #region 位置上传

        /// <summary>
        /// 配送员位置上传
        /// </summary>
        /// <param name="poi"></param>
        /// <param name="dohandle"></param>
        public void UploadLocation(I_GIS_LAST_LOCRECORD poi,out DoHandle dohandle)
        {
            dohandle = new DoHandle {IsSuccessful=false,OperateMsg="管控平台处理失败" };
            //activeContext.I_GIS_LAST_LOCRECORD.AddObject(poi);
            //activeContext.SaveChanges();
            //dohandle.IsSuccessful = true;
            //dohandle.OperateMsg = "处理成功";
        }

        #endregion
    }
}
