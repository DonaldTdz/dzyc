using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using System.Data.Objects;
using Common.Base;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// ///车辆检查信息数据访问层
    /// </summary>
    public class DistCarCheckRepository : ProRep<DistCarCheck>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public DistCarCheckRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<DistCarCheck> EntityCurrentSet
        {
            get { return ActiveContext.DistCarChecks; }
        }


        #region 备车检查

        /// <summary>
        /// 备车检查
        /// </summary>
        /// <param name="checkInfo"></param>
        /// <param name="dohandle"></param>
        public void CheckCar(List<DistCarCheck> checkInfo,DistRecordLog log,out DoHandle dohandle)
        {
            dohandle = new DoHandle {IsSuccessful=false,OperateMsg="操作失败!" };
            foreach (var item in checkInfo)
            {
                item.Id = Guid.NewGuid();
                ActiveContext.DistCarChecks.AddObject(item);
            }
         
                log.Id = Guid.NewGuid();
                ActiveContext.DistRecordLogs.AddObject(log);

            ActiveContext.SaveChanges();
            dohandle.IsSuccessful = true;
            dohandle.OperateMsg = "操作成功!";
        }

        #endregion


        #region 查询车辆检查信息
        
        /// <summary>
        /// 查询车辆检查信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<DistCarCheckResult> GetCarCheckResult(DistCarCheckQueryParam queryParam)
        {
            IList<DistCarCheckResult> result = new List<DistCarCheckResult>();
            var queryDate=queryParam.CheckDate.ToString("yyyyMMdd");
            //当天的车辆检查信息
            var datas = ActiveContext.DistCarChecks.Where(f => f.CHECK_TIME.StartsWith(queryDate)).ToList();

            //配送单集合
            var distNums = datas.Select(f => f.REF_ID).Distinct().ToList();
            var ldmDists = ActiveContext.LdmDists.Where(f => distNums.Contains(f.DIST_NUM)).ToList();

            //异常信息枚举
            IList<BaseEnum> baseEnums = ActiveContext.BaseEnums.Where(f => f.EnumType == "CarCheck").ToList();

            foreach (var item in ldmDists)
            {
                #region 备车检查

                var startChecks = datas.Where(f => f.REF_ID == item.DIST_NUM && f.CHECK_TYPE=="1").ToList();
                DistCarCheckResult startCheckResult = new DistCarCheckResult 
                {
                    CHECK_TYPE="1",
                    CHECK_TYPE_DESC="备车检查",
                    CAR_LICENSE=item.CAR_LICENSE,
                    DLVMAN_NAME=item.DLVMAN_NAME,
                    DRIVER_NAME = item.DRIVER_NAME
                };
                if (startChecks.Count == 0)
                {
                    startCheckResult.IsAllOk = true;
                    startCheckResult.ABNORMAL_DETAIL = string.Empty;
                }
                else
                {
                    var isallOk = startChecks.FirstOrDefault(f => f.ABNORMAL_DETAIL == "z00");
                    if (isallOk != null)
                    {
                        startCheckResult.IsAllOk = true;
                        startCheckResult.ABNORMAL_DETAIL = string.Empty;
                    }
                    else
                    {
                        foreach (var sc in startChecks)
                        {
                            var dt = baseEnums.SingleOrDefault(f => f.Value == sc.ABNORMAL_DETAIL);
                            startCheckResult.ABNORMAL_DETAIL = startCheckResult.ABNORMAL_DETAIL + dt.ValueNote + ";";
                        }
                    }
                }
                result.Add(startCheckResult);

                #endregion

                #region 回程检查

                var endChecks = datas.Where(f => f.REF_ID == item.DIST_NUM && f.CHECK_TYPE == "3").ToList();
                DistCarCheckResult endCheckResult = new DistCarCheckResult
                {
                    CHECK_TYPE = "3",
                    CHECK_TYPE_DESC = "回程检查",
                    CAR_LICENSE = item.CAR_LICENSE,
                    DLVMAN_NAME = item.DLVMAN_NAME,
                    DRIVER_NAME = item.DRIVER_NAME
                };
                if (endChecks.Count == 0)
                {
                    continue;
                    //endCheckResult.IsAllOk = true;
                    //endCheckResult.ABNORMAL_DETAIL = string.Empty;
                }
                else
                {
                    var isendallOk = endChecks.FirstOrDefault(f => f.ABNORMAL_DETAIL == "z00");
                    if (isendallOk != null)
                    {
                        endCheckResult.IsAllOk = true;
                        endCheckResult.ABNORMAL_DETAIL = string.Empty;
                    }
                    else
                    {
                        foreach (var sc in endChecks)
                        {
                            var dt = baseEnums.SingleOrDefault(f => f.Value == sc.ABNORMAL_DETAIL);
                            endCheckResult.ABNORMAL_DETAIL = endCheckResult.ABNORMAL_DETAIL + dt.ValueNote + ";";
                        }
                    }
                }
                result.Add(endCheckResult);

                #endregion

            }

            return result;
        }
         
        #endregion
    }
}
