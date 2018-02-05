using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using System.Data.Objects;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 配送点预警信息数据访问层
    /// </summary>
    public class DeliveryWarningRepository : ProRep<DeliveryWarning>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public DeliveryWarningRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<DeliveryWarning> EntityCurrentSet
        {
            get { return ActiveContext.DeliveryWarnings; }
        }


        /// <summary>
        /// 获取配送点预警信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<DeliveryWarning> GetWarningDatas(DeliveryWarningQueryParam queryParam)
        {
            IList<DeliveryWarning> result = new List<DeliveryWarning>();

            var datas = (from d in ActiveContext.DeliveryWarnings
                        join l in ActiveContext.LdmDists
                        on d.DIST_NUM equals l.DIST_NUM
                        join o in ActiveContext.LdmDistLines
                        on d.CO_NUM equals o.CO_NUM
                        where (string.IsNullOrEmpty(queryParam.DIST_NUM) || d.DIST_NUM == queryParam.DIST_NUM)
                        select new {d,l,o }
                        ).ToList();
            foreach (var item in datas)
            {
                DeliveryWarning dt = new DeliveryWarning 
                {
                    Id=item.d.Id,
                    DIST_NUM=item.d.DIST_NUM,
                    CO_NUM=item.d.CO_NUM,
                    Longitude=item.d.Longitude,
                    Latitude=item.d.Latitude,
                    RealLatitude=item.d.RealLatitude,
                    RealLongitude=item.d.RealLongitude,
                    DeliveryTime=item.d.DeliveryTime,
                    Reason=item.d.Reason,

                    RUT_ID=item.l.RUT_ID,
                    RUT_NAME=item.l.RUT_NAME,
                    DLVMAN_ID=item.l.DLVMAN_ID,
                    DLVMAN_NAME=item.l.DLVMAN_NAME,
                    CAR_ID=item.l.CAR_ID,
                    CAR_LICENSE=item.l.CAR_LICENSE,
                    

                    CUST_ID=item.o.CUST_ID,
                    CUST_CODE=item.o.CUST_CODE,
                    CUST_NAME=item.o.CUST_NAME,
                    MANAGER=item.o.MANAGER,
                    ADDR=item.o.ADDR,
                    TEL=item.o.TEL
                };

                result.Add(dt);
            }
            return result;
        }

    }
}
