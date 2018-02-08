using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using System.Data.Objects;
using Common.Base;
using System.Transactions;
using DHQR.BasicLib;


namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 配送任务单数据访问层
    /// </summary>
    public class LdmDistRepository : ProRep<LdmDist>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public LdmDistRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<LdmDist> EntityCurrentSet
        {
            get { return ActiveContext.LdmDists; }
        }

        #region 下载配送单信息,写入数据库

        /// <summary>
        /// 下载配送单信息,写入数据库
        /// </summary>
        /// <param name="ldmDists"></param>
        /// <param name="ldmDistLines"></param>
        /// <param name="ldmDisItems"></param>
        /// <param name="dohandle"></param>
        public void DownloadDists(List<LdmDist> ldmDists, List<LdmDistLine> ldmDistLines, List<LdmDisItem> ldmDisItems,out DoHandle dohandle)
        {
               dohandle = new DoHandle {IsSuccessful=false,OperateMsg="下载本地服务器出错!" };
                foreach (var d in ldmDists)
                {
                    var hasDist = ActiveContext.LdmDists.Any(f => f.DIST_NUM == d.DIST_NUM);
                    if (!hasDist)
                    {
                        ActiveContext.LdmDists.AddObject(d);
                        //ActiveContext.SaveChanges();
                    }
                }
                foreach (var l in ldmDistLines)
                {
                    var hasLine = ActiveContext.LdmDistLines.Any(f => f.DIST_NUM == l.DIST_NUM && f.CO_NUM==l.CO_NUM);

                    if (!hasLine)
                    {
                        ActiveContext.LdmDistLines.AddObject(l);
                        //ActiveContext.SaveChanges();
                    }
                }

              
                
                //foreach (var t in ldmDisItems)
                //{
                //    ActiveContext.LdmDisItems.AddObject(t);
                  
                //}
                try
                {
                    ActiveContext.SaveChanges();
                    dohandle.IsSuccessful = true;
                    dohandle.OperateMsg = "下载到本地服务器成功!";
                }
                catch(Exception ex)
                {
                    dohandle.OperateMsg = "保存到本地服务器失败!";
                }
        }



        /// <summary>
        /// 下载配送单信息,写入数据库
        /// </summary>
        /// <param name="ldmDists"></param>
        /// <param name="ldmDistLines"></param>
        /// <param name="ldmDisItems"></param>
        /// <param name="dohandle"></param>
        public void DownloadDistsByDate(List<LdmDist> ldmDists, List<LdmDistLine> ldmDistLines, List<LdmDisItem> ldmDisItems, List<CoTempReturn> coTmpReturns, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "下载本地服务器出错!" };
            foreach (var d in ldmDists)
            {
                var hasDist = ActiveContext.LdmDists.Any(f => f.DIST_NUM == d.DIST_NUM);
                if (!hasDist)
                {
                    ActiveContext.LdmDists.AddObject(d);
                    //ActiveContext.SaveChanges();
                }
            }
            foreach (var l in ldmDistLines)
            {
                var hasLine = ActiveContext.LdmDistLines.Any(f => f.DIST_NUM == l.DIST_NUM && f.CO_NUM == l.CO_NUM);

                if (!hasLine)
                {
                    ActiveContext.LdmDistLines.AddObject(l);
                    //ActiveContext.SaveChanges();
                }
            }

            foreach (var c in coTmpReturns)
            {
                var hasTmp = ActiveContext.CoTempReturns.Any(f => f.DIST_NUM == c.DIST_NUM && f.CO_NUM == c.CO_NUM);
                if (!hasTmp)
                {
                    ActiveContext.CoTempReturns.AddObject(c);
                    //ActiveContext.SaveChanges();
                }
                var coTmp = ActiveContext.CoTemps.FirstOrDefault(f => f.CO_NUM == c.CO_NUM);
                if (coTmp != null)
                {
                    coTmp.STATUS = "02";
                }
            }

            //foreach (var t in ldmDisItems)
            //{
            //    ActiveContext.LdmDisItems.AddObject(t);

            //}
            try
            {
                ActiveContext.SaveChanges();
                dohandle.IsSuccessful = true;
                dohandle.OperateMsg = "下载到本地服务器成功!";
            }
            catch
            {
                dohandle.OperateMsg = "保存到本地服务器失败!";
            }
        }

        #endregion

        #region 下载配送单完成

        /// <summary>
        /// 下载配送单完成
        /// </summary>
        /// <param name="distNums"></param>
        /// <param name="dohandle"></param>
        public void DownDistFinish(List<string> distNums,out DoHandle dohandle)
        {
            dohandle = new DoHandle {IsSuccessful=false,OperateMsg="操作失败!" };
            var ldmDists = EntityCurrentSet.Where(f => distNums.Contains(f.DIST_NUM)).ToList();
            foreach (var item in ldmDists)
            {
                item.IS_DOWNLOAD = "1";//下载完成
            }
            ActiveContext.SaveChanges();
            dohandle.IsSuccessful = true;
            dohandle.OperateMsg = "操作成功!";
        }

        #endregion

        #region 一键上传

        /// <summary>
        /// 一键上传
        /// </summary>
        /// <param name="param"></param>
        /// <param name="dohandle"></param>
        public void BatchAddedUpload(BatchAddedUploadParam param,out DoHandle dohandle)
        {
            dohandle = new DoHandle {IsSuccessful=false,OperateMsg="上传失败！" };

            //上传日志信息
            var logInfo = param.RecordLog.OrderBy(f => f.LOG_TIME).ToList();

            //上传客户位置信息
            foreach (var poi in param.GisCustPois)
            {
                var item = ActiveContext.GisCustPois.SingleOrDefault(f => f.CUST_ID == poi.CUST_ID);
                if (item == null)
                {
                    poi.Id = Guid.NewGuid();
                    ActiveContext.GisCustPois.AddObject(poi);
                }
                else
                {
                    item.ORIGINAL_LATITUDE = poi.ORIGINAL_LATITUDE;
                    item.ORIGINAL_LONGITUDE = poi.ORIGINAL_LONGITUDE;
                }
                var retailer = ActiveContext.Retailers.SingleOrDefault(f => f.CUST_ID == poi.CUST_ID);
                if (retailer != null)
                {
                    decimal lat = poi.ORIGINAL_LATITUDE.Value;
                    decimal lon = poi.ORIGINAL_LONGITUDE.Value;
                    double mgLat, mgLon;
                    CoordinateTransform.transform((Double)lat, (Double)lon, out mgLat, out mgLon);
                    retailer.LATITUDE = (decimal)mgLat;
                    retailer.LONGITUDE = (decimal)mgLon;

                }

                ActiveContext.SaveChanges();
            }

            foreach (var item in logInfo)
            {
                item.Id = Guid.NewGuid();
                ActiveContext.DistRecordLogs.AddObject(item);
            }
            //上传到货确认信息
            foreach (var d in param.DistCust)
            {
                d.Id = Guid.NewGuid();
                ActiveContext.DistCusts.AddObject(d);
            }
            //上传退货信息
            foreach (var r in param.ReturnOrder)
            {
                r.Order.Id = Guid.NewGuid();
                ActiveContext.CoReturns.AddObject(r.Order);
                foreach (var detail in r.OrderDetails)
                {
                    detail.Id = Guid.NewGuid();
                    ActiveContext.CoReturnLines.AddObject(detail);
                }
            }


            ActiveContext.SaveChanges();
            dohandle.IsSuccessful = true;
            dohandle.OperateMsg = "上传成功!";
        }

        #endregion

        #region 配送任务完成率

        /// <summary>
        /// 查询配送任务完成率
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<LdmDistFinishRate> GetLdmDistFinishRate(LdmFinishQueryParam queryParam)
        {
            IList<LdmDistFinishRate> result = new List<LdmDistFinishRate>();
            var queryDate=queryParam.DistDate.AddDays(-1).ToString("yyyyMMdd");
            var ldmDists = ActiveContext.LdmDists.Where(f => f.DIST_DATE == queryDate).ToList();
            var distNums=ldmDists.Select(f=>f.DIST_NUM).ToList();
            var ldmDistLines = ActiveContext.LdmDistLines.Where(f => distNums.Contains(f.DIST_NUM)).ToList();
            var coNums=ldmDistLines.Select(f=>f.CO_NUM).ToList();
            var distCusts = ActiveContext.DistCusts.Where(f => coNums.Contains(f.CO_NUM)).ToList();

            var rutIds = ldmDists.Select(f => f.RUT_ID).ToList();
            var ruts = ActiveContext.DistRuts.Where(f => rutIds.Contains(f.RUT_ID)).ToList();

            foreach (var item in ldmDists)
            {
                decimal orderNum =(decimal)item.DIST_CUST_SUM;
                decimal custNum =(decimal) distCusts.Where(f => f.DIST_NUM == item.DIST_NUM).Count();
                var rt = ruts.SingleOrDefault(f => f.RUT_ID == item.RUT_ID);
                LdmDistFinishRate ft = new LdmDistFinishRate 
                {
                    CAR_NAME = rt.RUT_NAME,
                    DistDate=queryParam.DistDate.ToString("yyyy-MM-dd"),
                    DIST_NUM=item.DIST_NUM,
                    FinishRate = (decimal.Round(( custNum/orderNum ), 4)) * 100
                };
                result.Add(ft);
            }
            return result;                   
        }

        #endregion


        /// <summary>
        /// 根据线路ID和时间获取配送单
        /// </summary>
        /// <param name="rutId"></param>
        /// <param name="distDate"></param>
        /// <returns></returns>
        public LdmDist GetDistByRutAndDate(string rutId, string distDate)
        {
            var result = ActiveContext.LdmDists.SingleOrDefault(f => f.RUT_ID == rutId && f.DIST_DATE == distDate);
            return result;
        }

        /// <summary>
        /// 查询配送单信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public PagedResults<LdmDist> QueryLdmDist(LdmDistQueryParam queryParam)
        {
            //根据微信用户OPENID获取零售户信息
            var wxUser = ActiveContext.WeiXinUsers.SingleOrDefault(f => f.WxUserName == queryParam.WxUserName);

            if (wxUser != null)
            {

                string custId = string.Empty;
                //判断用户类型
                WeiXinUserType wxUserType = ActiveContext.WeiXinUserTypes.SingleOrDefault(f => f.Id == wxUser.WeiXinUserTypeId);
                if (wxUserType.Code == 0)//零售户
                {
                    var retailer = ActiveContext.Retailers.SingleOrDefault(f => f.LICENSE_CODE == wxUser.SysName);
                    custId = retailer.CUST_ID;
                }

                List<string> distNums = (from a in ActiveContext.LdmDistLines
                                         join b in ActiveContext.LdmDists
                                         on a.DIST_NUM equals b.DIST_NUM
                                         where b.DIST_DATE == queryParam.DIST_DATE
                                         && (string.IsNullOrEmpty(custId) || a.CUST_ID == custId)
                                         select a.DIST_NUM
                            ).Distinct().ToList();

                var result = ActiveContext.LdmDists.Where(f => distNums.Contains(f.DIST_NUM)).ToPagedResults<LdmDist>(queryParam);
                return result;
            }
            else
            {
                List<string> distNums = (from a in ActiveContext.LdmDistLines
                                         join b in ActiveContext.LdmDists
                                         on a.DIST_NUM equals b.DIST_NUM
                                         where b.DIST_DATE == queryParam.DIST_DATE
                                         select a.DIST_NUM
                                          ).Distinct().ToList();

                var result = ActiveContext.LdmDists.Where(f => distNums.Contains(f.DIST_NUM)).ToPagedResults<LdmDist>(queryParam);
                return result;
            }
        }

        /// <summary>
        /// 当日配送单信息
        /// </summary>
        /// <param name="distCount"></param>
        /// <param name="finishCount"></param>
        /// <param name="notfinishCount"></param>
        /// <param name="totalMoney"></param>
        public void GetLdmDistInfo(out int distCount,out int finishCount,out int notfinishCount,out decimal totalMoney)
        {
            finishCount = 0;
            OperationType op;
            var preDate = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
            var currentDate=DateTime.Now.ToString("yyyyMMdd");
            var ldmDists = ActiveContext.LdmDists.Where(f => f.DIST_DATE == preDate).ToList();
            distCount = ldmDists.Count;
            totalMoney = ldmDists.Sum(f => f.AMT_SUM);
            var missionFinish = ActiveContext.DistRecordLogs.Where(f => f.LOG_DATE == currentDate && f.OPERATION_TYPE ==op.missionFinish).ToList();
            foreach (var item in missionFinish)
            {
                var currentCount = ldmDists.Where(f => f.DLVMAN_ID == item.USER_ID).Count();
                finishCount = finishCount + currentCount;
            }
            notfinishCount = distCount - finishCount;
        }
    }

   

}
