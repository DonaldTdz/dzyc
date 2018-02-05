using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using System.Data.Objects;
using Common.Base;
using DHQR.BasicLib;

namespace DHQR.DataAccess.Implement
{
    public class DistCustRepository : ProRep<DistCust>
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public DistCustRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<DistCust> EntityCurrentSet
        {
            get { return ActiveContext.DistCusts; }
        }


        #region 到货确认

        /// <summary>
        /// 到货确认
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dohandle"></param>
        public void ConfirmDelivery(DistCust data, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "操作失败!" };

            var isCoTemp = ActiveContext.CoTemps.SingleOrDefault(f =>  f.CO_NUM == data.CO_NUM);

            if (data.UNUSUAL_TYPE == "01")//暂存
            {
                //判断是否暂存订单，如果是暂存订单，只更新暂存订单信息

                if (isCoTemp == null)
                {
                    //情况1、一般订单暂存
                    //插入I_DIST_CUST，其中IS_RECEIVED=‘04’， UNUSUAL_TYPE=‘01’；
                    //其次要插入I_CO_TEMP，其中STATUS=‘01’返库，且写入RET_TIME，RET_USER_ID
                    CoTmpRegist(data, out dohandle);
                    SetDistCustLog(data, out dohandle);
                }
                else 
                {
                    //情况2、暂存订单暂存
                    //更新I_CO_TEMP，更新RET_TIME，RET_USER_ID，并且STATUS=‘01’返库
                    isCoTemp.RET_TIME = data.EVALUATE_TIME;
                    isCoTemp.RET_USER_ID = data.DLVMAN_ID;
                    isCoTemp.STATUS = "01";
                    ActiveContext.SaveChanges();
                    dohandle.IsSuccessful = true;
                    dohandle.OperateMsg = "操作成功";

                }
            }
            else
            {
               
                if (isCoTemp==null)
                {
                    //情况3、一般订单正常收货
                    SetDistCustLog(data, out dohandle);
                }
                else
                {
                    //情况4、暂存订单正常收货
                    //更新I_DIST_CUST，其中IS_RECEIVED=‘01’， UNUSUAL_TYPE=‘’；
                    //其次，更新I_CO_TEMP，其中STATUS=‘03’完结，且写入FINISH_TIME，FINISH_USER_ID
                    var dt = ActiveContext.DistCusts.SingleOrDefault(f =>  f.CO_NUM == data.CO_NUM);
                    dt.IS_RECEIVED = "01";
                    dt.UNUSUAL_TYPE = "";
                    dt.EVALUATE_TIME = data.EVALUATE_TIME;
                    var coTp = ActiveContext.CoTemps.SingleOrDefault(f => f.CO_NUM == data.CO_NUM);
                    coTp.STATUS = "03";
                    coTp.FINISH_TIME = data.EVALUATE_TIME;
                    coTp.FINISH_USER_ID = data.DLVMAN_ID;

                    ActiveContext.SaveChanges();
                    dohandle.IsSuccessful = true;
                    dohandle.OperateMsg = "操作成功";
                }
            }
        }


        private void SetDistCustLog(DistCust data, out DoHandle dohandle)
        {
            dohandle = new DoHandle {IsSuccessful=false,OperateMsg="操作失败" };
            var hasData = ActiveContext.DistCusts.Any(f => f.DIST_NUM == data.DIST_NUM && f.CO_NUM == data.CO_NUM);
            if (!hasData)
            {
                data.Id = Guid.NewGuid();
                data.RecieveDate = DateFormatExtention.ConvertToDateTime(data.REC_DATE);
                ActiveContext.DistCusts.AddObject(data);
                ActiveContext.SaveChanges();
            }
            dohandle.IsSuccessful = true;
            dohandle.OperateMsg = "操作成功!";
        }

        /// <summary>
        /// 暂存登记
        /// </summary>
        /// <param name="dist"></param>
        /// <param name="dohandle"></param>
        public void CoTmpRegist(DistCust data, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "管控平台操作失败" };
            CoTemp tmp = new CoTemp
            {
                Id = Guid.NewGuid(),
                CO_NUM = data.CO_NUM,
                DIST_NUM = data.DIST_NUM,
                RET_TIME = data.EVALUATE_TIME,
                RET_USER_ID = data.DLVMAN_ID,
                STATUS = "01"
            };
            ActiveContext.CoTemps.AddObject(tmp);
            try
            {
                ActiveContext.SaveChanges();
                dohandle.IsSuccessful = true;
                dohandle.OperateMsg = "暂存成功";
            }
            catch (Exception ex)
            {
                dohandle.IsSuccessful = false;
                dohandle.OperateMsg = "暂存失败";
            }
        }



        #endregion


        #region 满意度分析

        /// <summary>
        /// 获取单个配送员每天满意度信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<DistCust> GetSatisfyByDayData(DistCustByDayQueryParam queryParam)
        {
            var result = ActiveContext.DistCusts.Where(f =>
                                                          (string.IsNullOrEmpty(queryParam.DLVMAN_ID) || (f.DLVMAN_ID == queryParam.DLVMAN_ID))
                                                        && f.RecieveDate == queryParam.DistTime
                                                                ).ToList();
            return result;
        }

        /// <summary>
        /// 获取配送满意度
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<DistCust> GetSatisfyDatas(SatisfyQueryParam queryParam)
        {

            var result = ActiveContext.DistCusts.Where(f => f.RecieveDate >= queryParam.StartDate
                                                        && f.RecieveDate <= queryParam.EndDate
                                                                ).ToList();

            return result;
        }

        /// <summary>
        /// 获取不满意原因信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<NotSatisReasonResult> GetNotSatisReasons(SatisfyQueryParam queryParam)
        {

            var dlvMan = ActiveContext.DistDlvmans.SingleOrDefault(f => f.POSITION_CODE == queryParam.DLVMAN_ID);
            var datas = ActiveContext.DistCusts.Where(f => f.RecieveDate >= queryParam.StartDate
                                                       && f.RecieveDate <= queryParam.EndDate
                                                       && f.DLVMAN_ID == queryParam.DLVMAN_ID
                                                       && (f.DIST_SATIS == "13" || f.DIST_SATIS == "14")
                                                               ).ToList();
            //按不满意原因分组
            var groupedDatas = datas.GroupBy(f => f.NOTSATIS_REASON).ToList();

            IList<BaseEnum> baseEnums = ActiveContext.BaseEnums.Where(f => f.EnumType == "NotsatisReason").ToList();

            IList<NotSatisReasonResult> result = new List<NotSatisReasonResult>();
            foreach (var d in groupedDatas)
            {
                var bm = baseEnums.SingleOrDefault(f => f.Value == d.Key.Trim());

                NotSatisReasonResult item = new NotSatisReasonResult
                {
                    CAR_ID = dlvMan.POSITION_CODE,
                    CAR_NAME = dlvMan.USER_NAME,
                    ReasonCode = d.Key,
                    ReasonName = bm != null ? bm.ValueNote : "未知",
                    Count = d.Count()
                };
                result.Add(item);
            }


            return result;

        }


        /// <summary>
        /// 查询满意度信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<SatisfactionModel> QuerySatisfaction(SatisfyQueryParam queryParam)
        {
            IList<SatisfactionModel> result = new List<SatisfactionModel>();
            var endDate = queryParam.EndDate.AddDays(1);
            var datas = ActiveContext.DistCusts.Where(f =>
                        (string.IsNullOrEmpty(queryParam.DLVMAN_ID) || f.DLVMAN_ID == queryParam.DLVMAN_ID)
                        && f.RecieveDate >= queryParam.StartDate
                        && f.RecieveDate < endDate
                      ).ToList();
            var groupedDaras = datas.GroupBy(f => new { f.DLVMAN_ID });
            //var satisfyStandards = ActiveContext.BaseEnums.Where(f => f.EnumType == "Satisfy");
            foreach (var item in groupedDaras)
            {
                var dataCount = item.Count();
                var verySatisfiedCount = item.Where(f => f.DIST_SATIS == "10").Count();
                var verySatisfiedRate = verySatisfiedCount == 0 ? 0 : (decimal.Round(((decimal)verySatisfiedCount / (decimal)dataCount), 4)) * 100;
                var satisfiedCount = item.Where(f => f.DIST_SATIS == "11").Count();
                var satisfiedRate = satisfiedCount == 0 ? 0 : (decimal.Round(((decimal)satisfiedCount / (decimal)dataCount), 4)) * 100;
                var generalCount = item.Where(f => f.DIST_SATIS == "12").Count();
                var generalRate = generalCount == 0 ? 0 : (decimal.Round(((decimal)generalCount / (decimal)dataCount), 4)) * 100;
                var dissatisfiedCount = item.Where(f => f.DIST_SATIS == "13").Count();
                var dissatisfiedRate = dissatisfiedCount == 0 ? 0 : (decimal.Round(((decimal)dissatisfiedCount / (decimal)dataCount), 4)) * 100;
                var veryDissatisfiedCount = item.Where(f => f.DIST_SATIS == "14").Count();
                var veryDissatisfiedRate = veryDissatisfiedCount == 0 ? 0 : (decimal.Round(((decimal)veryDissatisfiedCount / (decimal)dataCount), 4)) * 100;
                var dlvMan = ActiveContext.DistDlvmans.SingleOrDefault(f => f.POSITION_CODE == item.Key.DLVMAN_ID);
                //var satisfyStandardItem = satisfyStandards.SingleOrDefault(f => f.Value == item.Key.DIST_SATIS);

                SatisfactionModel model = new SatisfactionModel
                {
                    DLVMAN_ID = item.Key.DLVMAN_ID,
                    DLVMAN_NAME = dlvMan.USER_NAME,
                    StartDate = queryParam.StartDate,
                    EndDate = queryParam.EndDate,
                    VerySatisfiedCount = verySatisfiedCount,
                    VerySatisfiedRate = verySatisfiedRate,
                    SatisfiedCount = satisfiedCount,
                    SatisfiedRate = satisfiedRate,
                    GeneralCount = generalCount,
                    GeneralRate = generalRate,
                    DissatisfiedCount = dissatisfiedCount,
                    DissatisfiedRate = dissatisfiedRate,
                    VeryDissatisfiedCount = veryDissatisfiedCount,
                    VeryDissatisfiedRate = veryDissatisfiedRate
                };

                result.Add(model);

            }

            return result;
        }

        #endregion



        public void WriteBug(string msg1, string msg2)
        {
            ExceptionLog log = new ExceptionLog
            {
                Id = Guid.NewGuid(),
                Message = msg1,
                InnerException = msg2,
                Ip = "test",
                CreateTime = DateTime.Now,
                UserName = "SysTem"
            };
            ActiveContext.ExceptionLogs.AddObject(log);
            ActiveContext.SaveChanges();
        }

    }

}
