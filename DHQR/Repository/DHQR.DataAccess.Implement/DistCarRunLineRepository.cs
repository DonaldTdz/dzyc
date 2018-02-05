using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using System.Data.Objects;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 车辆运行明细数据访问层
    /// </summary>
    public class DistCarRunLineRepository : ProRep<DistCarRunLine>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public DistCarRunLineRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<DistCarRunLine> EntityCurrentSet
        {
            get { return ActiveContext.DistCarRunLines; }
        }

        #region 车辆运行统计

        /// <summary>
        /// 查询车辆运行信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<DistCarRunLine> QueryCarRunLine(DistCarRunLineQueryParam queryParam)
        {
            IList<DistCarRunLine> result = new List<DistCarRunLine>();
            var datas = (from l in ActiveContext.DistCarRunLines
                         join d in ActiveContext.DistCarRuns
                         on l.INFO_NUM equals d.INFO_NUM
                         where
                           (string.IsNullOrEmpty(queryParam.CAR_ID) || d.CAR_ID == queryParam.CAR_ID)
                        && d.CreateTime >= queryParam.StartDate
                        && d.CreateTime < queryParam.EndDate
                         select new { l, d }
                     ).ToList();
            foreach (var item in datas)
            {
                DistCarRunLine d = new DistCarRunLine 
                {
                    Id=item.l.Id,
                    INFO_NUM = item.l.INFO_NUM,
                    LINE_ID = item.l.LINE_ID,
                    COST_TYPE = item.l.COST_TYPE,
                    FUEL_TYPE = item.l.FUEL_TYPE,
                    LITRE_SUM = item.l.LITRE_SUM,
                    FUEL_PRI = item.l.FUEL_PRI,
                    AMT = item.l.AMT,
                    INV_NUM = item.l.INV_NUM
                };
                result.Add(d);
            }
            return result;
        }
        #endregion


    }
}
