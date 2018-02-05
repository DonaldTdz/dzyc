using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using System.Data.Objects;
using DHQR.BasicLib;
using Common.Base;
using DHQR.DataAccess.Langchao;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 配送任务单行订单数据访问层
    /// </summary>
    public class LdmDistLineRepository : ProRep<LdmDistLine>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public LdmDistLineRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<LdmDistLine> EntityCurrentSet
        {
            get { return ActiveContext.LdmDistLines; }
        }


        #region 获取线路明细信息

        /// <summary>
        ///根据条件获取线路明细信息
        /// </summary>
        /// <returns></returns>
        public IList<LdmDistLine> QueryDeliveryLine(LdmDistLineQueryParam queryParam)
        {
            IList<LdmDistLine> result = new List<LdmDistLine>();
            var datas = (from L in ActiveContext.LdmDistLines
                         join c in ActiveContext.Retailers
                         on L.CUST_ID equals c.CUST_ID
                         where (string.IsNullOrEmpty(queryParam.DIST_NUM) || L.DIST_NUM == queryParam.DIST_NUM)
                            && c.LATITUDE != null
                            && c.LONGITUDE != null
                         select new { L, c }
                            ).ToList();
            foreach (var r in datas)
            {
                var L = r.L;
                var c = r.c;
                var hasConfrim = ActiveContext.DistCusts.Any(f => f.CO_NUM == L.CO_NUM);
                var item = new LdmDistLine
                {
                    Id = L.Id,
                    DIST_NUM = L.DIST_NUM,
                    CO_NUM = L.CO_NUM,
                    CUST_ID = L.CUST_ID,
                    CUST_CODE = L.CUST_CODE,
                    CUST_NAME = L.CUST_NAME,
                    MANAGER = L.MANAGER,
                    ADDR = L.ADDR,
                    TEL = L.TEL,
                    QTY_BAR = L.QTY_BAR,
                    AMT_AR = L.AMT_AR,
                    AMT_OR = L.AMT_OR,
                    PMT_STATUS = L.PMT_STATUS,
                    TYPE = L.TYPE,
                    SEQ = L.SEQ,
                    LICENSE_CODE = L.LICENSE_CODE,
                    PAY_TYPE = L.PAY_TYPE,
                    LONGITUDE = L.LONGITUDE,
                    LATITUDE = L.LATITUDE,
                    CUST_CARD_ID = L.CUST_CARD_ID,
                    CUST_CARD_CODE = L.CUST_CARD_CODE,
                    CustLatitude = c.LATITUDE.Value,
                    CustLongitude = c.LONGITUDE.Value,
                    HasConfirm = hasConfrim
                };
                result.Add(item);
            }

            return result;
        }


        #endregion


        #region 同步订单信息

        /// <summary>
        /// 同步订单信息
        /// </summary>
        /// <param name="synParam"></param>
        /// <returns></returns>
        public IList<OrderInfo> SynOrders(SynOrderParam synParam)
        {
            IList<OrderInfo> result = new List<OrderInfo>();
            var orders = ActiveContext.LdmDistLines.Where(f => f.DIST_NUM == synParam.DIST_NUM).ToList();
            var distCusts = ActiveContext.DistCusts.Where(f => f.DIST_NUM == synParam.DIST_NUM).ToList();
            var coReturns = ActiveContext.CoTempReturns.Where(f => f.OUT_DIST_NUM == synParam.DIST_NUM).ToList();//暂存出库订单
            var coReturnCoNums = coReturns.Select(f => f.CO_NUM).Distinct().ToList();
            var coReDistCusts = ActiveContext.DistCusts.Where(f => coReturnCoNums.Contains(f.CO_NUM)).ToList();


            foreach (var item in orders)
            {
                var hasDist = distCusts.Any(f => f.CO_NUM == item.CO_NUM);

                OrderInfo info = new OrderInfo
                {
                    DIST_NUM = item.DIST_NUM,
                    CO_NUM = item.CO_NUM,
                    IS_FINISH = hasDist
                };
                result.Add(info);
            }
            foreach (var item in coReturns)
            {
                var dt = coReDistCusts.FirstOrDefault(f => f.CO_NUM == item.CO_NUM);
                var od = result.SingleOrDefault(f => f.CO_NUM == item.CO_NUM);
                result.Remove(od);
                OrderInfo info = new OrderInfo
                {
                    DIST_NUM = item.OUT_DIST_NUM,
                    CO_NUM = item.CO_NUM,
                    IS_FINISH = (dt.UNUSUAL_TYPE.Trim() == "" || dt.UNUSUAL_TYPE == null) ? true : false
                };
                result.Add(info);

            }
            return result;

        }

        #endregion


        #region 微信订单查询

        /// <summary>
        /// 微信订单查询
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public PagedResults<LdmDistLine> QueryOrders(LdmDistLineQueryParam queryParam)
        {
            //根据微信用户OPENID获取零售户信息

            var wxUser = ActiveContext.WeiXinUsers.SingleOrDefault(f => f.WxUserName == queryParam.WxUserName);
            string custId = string.Empty;
            //判断用户类型
            WeiXinUserType wxUserType = ActiveContext.WeiXinUserTypes.SingleOrDefault(f => f.Id == wxUser.WeiXinUserTypeId);
            if (wxUserType.Code == 0)//零售户
            {
                var retailer = ActiveContext.Retailers.SingleOrDefault(f => f.LICENSE_CODE == wxUser.SysName);
                custId = retailer.CUST_ID;
            }

            var result = (from a in ActiveContext.LdmDistLines
                          join b in ActiveContext.LdmDists
                          on a.DIST_NUM equals b.DIST_NUM
                          where b.DIST_DATE == queryParam.DIST_DATE
                          && a.DIST_NUM == queryParam.DIST_NUM
                          && (string.IsNullOrEmpty(custId) || a.CUST_ID == custId)
                          select a
                        ).ToPagedResults<LdmDistLine>(queryParam);
            return result;
        }

        #endregion

        #region 微信推送

        /// <summary>
        /// 根据配送单号和序号获取下个零售户
        /// </summary>
        /// <param name="DIST_NUM"></param>
        /// <param name="CO_NUM"></param>
        /// <returns></returns>
        public LdmDistLine GetNextData(string DIST_NUM, string CO_NUM)
        {

            var currentData = ActiveContext.LdmDistLines.SingleOrDefault(f => f.DIST_NUM == DIST_NUM && f.CO_NUM == CO_NUM);
            var nextSEQ = currentData.SEQ + 1;
            var result = ActiveContext.LdmDistLines.SingleOrDefault(f => f.DIST_NUM == DIST_NUM && f.SEQ == nextSEQ);
            var firstRetailer = ActiveContext.GisCustPois.SingleOrDefault(f => f.CUST_ID == currentData.CUST_ID);
            var secondRetailer = ActiveContext.GisCustPois.SingleOrDefault(f => f.CUST_ID == result.CUST_ID);
            if (firstRetailer != null && secondRetailer != null)
            {
                double distance = GpsDistanceHelper.Distance((double)firstRetailer.ORIGINAL_LATITUDE, (double)firstRetailer.ORIGINAL_LONGITUDE,
                   (double)secondRetailer.ORIGINAL_LATITUDE, (double)secondRetailer.ORIGINAL_LONGITUDE);
                var time = distance / (double)667 + 10;
                result.RecTime = DateTime.Now.AddMinutes(time).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                result.RecTime = DateTime.Now.AddMinutes(60).ToString("yyyy-MM-dd HH:mm:ss");
            }
            return result;
        }

        /// <summary>
        /// 根据配送单号和序号获取当前零售户微信信息
        /// </summary>
        /// <param name="DIST_NUM"></param>
        /// <param name="CO_NUM"></param>
        /// <returns></returns>
        public LdmDistLine GetCurrentData(string DIST_NUM, string CO_NUM)
        {
            var currentData = ActiveContext.LdmDistLines.SingleOrDefault(f => f.DIST_NUM == DIST_NUM && f.CO_NUM == CO_NUM);
            var preSEQ = currentData.SEQ - 1;
            var result = ActiveContext.LdmDistLines.SingleOrDefault(f => f.DIST_NUM == DIST_NUM && f.SEQ == preSEQ);
            if (result != null)
            {
                var firstRetailer = ActiveContext.GisCustPois.SingleOrDefault(f => f.CUST_ID == result.CUST_ID);
                var secondRetailer = ActiveContext.GisCustPois.SingleOrDefault(f => f.CUST_ID == currentData.CUST_ID);
                if (firstRetailer != null && secondRetailer != null)
                {
                    double distance = GpsDistanceHelper.Distance((double)firstRetailer.ORIGINAL_LATITUDE, (double)firstRetailer.ORIGINAL_LONGITUDE,
                       (double)secondRetailer.ORIGINAL_LATITUDE, (double)secondRetailer.ORIGINAL_LONGITUDE);
                    var time = distance / (double)667 + 10;
                    currentData.RecTime = DateTime.Now.AddMinutes(time).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    currentData.RecTime = DateTime.Now.AddMinutes(60).ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            else
            {
                currentData.RecTime = DateTime.Now.AddMinutes(60).ToString("yyyy-MM-dd HH:mm:ss");
            }
            return currentData;

        }


        /// <summary>
        /// 根据配送单号和序号获取待发微信提醒客户
        /// </summary>
        /// <param name="DIST_NUM"></param>
        /// <param name="CO_NUM"></param>
        /// <returns></returns>
        public IList<WeiXinUser> GetToSendUsers(string DIST_NUM, string CO_NUM)
        {
            IList<LdmDistLine> lines = new List<LdmDistLine>();
            var currentData = ActiveContext.LdmDistLines.SingleOrDefault(f => f.DIST_NUM == DIST_NUM && f.CO_NUM == CO_NUM);
            var sendCountStr = ActiveContext.GlobalConfigurations.SingleOrDefault(f => f.Key == "SendOrderNum").Value;
            var sendCount = int.Parse(sendCountStr);
            if (currentData.SEQ == 1) //当前为第一条订单，则发送微信
            {
                lines = ActiveContext.LdmDistLines.Where(f => f.DIST_NUM == DIST_NUM && f.SEQ >= 1 && f.SEQ <= sendCount).ToList();
            }
            else
            {
                int residule = currentData.SEQ % sendCount;
                if (residule == 0) //余数为0 发送微信
                {
                    lines = ActiveContext.LdmDistLines.Where(f => f.DIST_NUM == DIST_NUM && f.SEQ > currentData.SEQ && f.SEQ <= (currentData.SEQ + sendCount)).ToList();
                }
            }
            IList<WeiXinUser> result = new List<WeiXinUser>();
            if (lines.Count > 0)
            {
                IList<string> licenceCodes = lines.Select(f => f.LICENSE_CODE).ToList();
                result = ActiveContext.WeiXinUsers.Where(f => licenceCodes.Contains(f.SysName)).ToList();
                foreach (var item in result)
                {
                    var cuLine = lines.SingleOrDefault(f => f.LICENSE_CODE == item.SysName);
                    item.CO_NUM = cuLine == null ? " " : cuLine.CO_NUM;
                }
            }
            return result;
        }


        /// <summary>
        /// 获取下一户待发微信用户
        /// </summary>
        /// <param name="DIST_NUM"></param>
        /// <param name="CO_NUM"></param>
        /// <returns></returns>
        public WeiXinUser GetToSendUser(string DIST_NUM, string CO_NUM)
        {
            var currentData = ActiveContext.LdmDistLines.SingleOrDefault(f => f.DIST_NUM == DIST_NUM && f.CO_NUM == CO_NUM);
            int nextSeq = currentData.SEQ + 10;
            var line = ActiveContext.LdmDistLines.FirstOrDefault(f => f.DIST_NUM == DIST_NUM && f.SEQ == nextSeq);
            if (line != null)
            {
                var result = ActiveContext.WeiXinUsers.SingleOrDefault(f => f.SysName == line.LICENSE_CODE);
                if (result == null)
                {
                    return null;
                }
                else
                {
                    result.CO_NUM = line.CO_NUM;
                    return result;
                }
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 获取下一户待发微信用户
        /// </summary>
        /// <param name="DIST_NUM"></param>
        /// <param name="CO_NUM"></param>
        /// <returns></returns>
        public IList<WeiXinUser> GetNextToSendUsers(string DIST_NUM, string CO_NUM)
        {
            IList<WeiXinUser> result = new List<WeiXinUser>();
            var currentData = ActiveContext.LdmDistLines.SingleOrDefault(f => f.DIST_NUM == DIST_NUM && f.CO_NUM == CO_NUM);
            int nextSeq = currentData.SEQ + 10;
            var lines = ActiveContext.LdmDistLines.Where(f => f.DIST_NUM == DIST_NUM && f.SEQ == nextSeq).ToList();
            if (lines.Count > 0)
            {
                foreach (var item in lines)
                {
                    var datas = ActiveContext.WeiXinUsers.Where(f => f.SysName == item.LICENSE_CODE).ToList();
                    foreach (var data in datas)
                    {
                        if (data != null)
                        {
                            data.CO_NUM = item.CO_NUM;
                            result.Add(data);
                        }
                    }
                }
            }
            return result;

        }


        /// <summary>
        /// 获取出门发送微信客户
        /// </summary>
        /// <param name="DIST_NUM"></param>
        /// <returns></returns>
        public IList<WeiXinUser> GetToSendUsersAfterOut(string DIST_NUM)
        {
            IList<LdmDistLine> lines = new List<LdmDistLine>();
            var sendCountStr = ActiveContext.GlobalConfigurations.SingleOrDefault(f => f.Key == "SendOrderNum").Value;
            var sendCount = int.Parse(sendCountStr);
            lines = ActiveContext.LdmDistLines.Where(f => f.DIST_NUM == DIST_NUM && f.SEQ >= 1 && f.SEQ <= sendCount).ToList();

            IList<WeiXinUser> result = new List<WeiXinUser>();
            if (lines.Count > 0)
            {
                IList<string> licenceCodes = lines.Select(f => f.LICENSE_CODE).ToList();
                result = ActiveContext.WeiXinUsers.Where(f => licenceCodes.Contains(f.SysName)).ToList();
                foreach (var item in result)
                {
                    var cuLine = lines.SingleOrDefault(f => f.LICENSE_CODE == item.SysName);
                    item.CO_NUM = cuLine == null ? " " : cuLine.CO_NUM;
                }
            }
            return result;
        }

        #endregion


        #region 订单查询

        /// <summary>
        /// 查询订单信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public PagedResults<LdmDistLine> QueryLdmdistLine(LdmDistLineQueryParam queryParam)
        {
            var result = ActiveContext.LdmDistLines.Where(f => f.DIST_NUM == queryParam.DIST_NUM
                                       && (string.IsNullOrEmpty(queryParam.KeyData) || f.CUST_NAME.Contains(queryParam.KeyData)
                                           || f.CO_NUM.Contains(queryParam.KeyData) || f.CUST_CODE.Contains(queryParam.KeyData))
                                        )
                                    .ToPagedResults<LdmDistLine>(queryParam);
            foreach (var item in result.Data)
            {
                var distCust = ActiveContext.DistCusts.Where(f => f.CO_NUM == item.CO_NUM).OrderByDescending(t => t.RecieveDate).FirstOrDefault();
                //查询是否是暂存订单
                var isCoTemp = ActiveContext.CoTempReturns.Any(f => f.CO_NUM == item.CO_NUM && f.OUT_DIST_NUM == queryParam.DIST_NUM);
                item.IsTemp = isCoTemp;
                if (isCoTemp)
                {//暂存订单
                    var coTmp = ActiveContext.CoTemps.SingleOrDefault(f => f.CO_NUM == item.CO_NUM);
                    if (coTmp.FINISH_TIME != null)
                    {
                        item.REC_STATE = "已签收";
                        var evaTime = DateTimeExtOpt.ConvertToDateTime(distCust.EVALUATE_TIME, "yyyyMMddHHmmss");
                        item.REC_CIG_TIME = evaTime.ToString("yyyy-MM-dd HH:mm:ss");

                    }
                    else if (coTmp.FINISH_TIME == null && distCust.UNUSUAL_TYPE == "01" && coTmp.STATUS == "01")
                    {
                        item.REC_STATE = "已暂存";
                        var evaTime = DateTimeExtOpt.ConvertToDateTime(distCust.EVALUATE_TIME, "yyyyMMddHHmmss");
                        item.REC_CIG_TIME = evaTime.ToString("yyyy-MM-dd HH:mm:ss");

                    }
                    else
                    {
                        item.REC_STATE = "未送达";
                    }

                }
                else
                { //正常订单

                    if (distCust != null)
                    {
                        item.REC_STATE = distCust.UNUSUAL_TYPE == "01" ? "已暂存" : "已签收";
                        var evaTime = DateTimeExtOpt.ConvertToDateTime(distCust.EVALUATE_TIME, "yyyyMMddHHmmss");
                        item.REC_CIG_TIME = evaTime.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        item.REC_STATE = "未送达";
                    }
                }
            }
            return result;
        }


        /// <summary>
        /// 获取订单和配送单信息
        /// </summary>
        /// <param name="CO_NUM"></param>
        /// <returns></returns>
        public LdmDistLine GetByCoNum(string CO_NUM, string DIST_NUM, out LdmDist dist)
        {
            var result = ActiveContext.LdmDistLines.SingleOrDefault(f => f.CO_NUM == CO_NUM && f.DIST_NUM == DIST_NUM);
            dist = ActiveContext.LdmDists.SingleOrDefault(f => f.DIST_NUM == result.DIST_NUM);
            return result;
        }

        #endregion


        /// <summary>
        /// 根据订单号查询订单信息
        /// </summary>
        /// <param name="coNums"></param>
        /// <returns></returns>
        public List<LdmDistLine> GetByCoNums(IList<string> coNums)
        {
            var result = ActiveContext.LdmDistLines.Where(f => coNums.Contains(f.CO_NUM)).ToList();
            return result;
        }

        /// <summary>
        /// 通过暂存订单获取订单详情
        /// </summary>
        /// <param name="tmpReturns"></param>
        /// <param name="distLines"></param>
        /// <param name="distItems"></param>
        public List<LdmDistLine> ConvertTempToLine(List<I_CO_TEMP_RETURN> tmpReturns)
        {
            List<LdmDistLine> result = new List<LdmDistLine>();
            foreach (var item in tmpReturns)
            {
                var co = ActiveContext.LdmDistLines.FirstOrDefault(f => f.CO_NUM == item.CO_NUM);
                co.Id = Guid.NewGuid();
                co.DIST_NUM = item.OUT_DIST_NUM;
                co.IsTemp = true;
                result.Add(co);
            }
            return result;
        }


    }
}
