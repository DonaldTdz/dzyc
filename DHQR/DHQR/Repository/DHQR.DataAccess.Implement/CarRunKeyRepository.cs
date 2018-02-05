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
    /// 回程登记流水号数据访问层
    /// </summary>
    public class CarRunKeyRepository : ProRep<CarRunKey>
    {
         /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public CarRunKeyRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<CarRunKey> EntityCurrentSet
        {
            get { return ActiveContext.CarRunKeys; }
        }

        /// <summary>
        /// 获取流水号
        /// </summary>
        /// <returns></returns>
        public string GetLogkey()
        {
            Guid id = Guid.NewGuid();
            CarRunKey key = new CarRunKey
            {
                Id = id
            };
            var sql = "insert into CarRunKeys values('" + id + "')";
            ActiveContext.ExecuteStoreCommand(sql);
            var result = ActiveContext.CarRunKeys.SingleOrDefault(f => f.Id == id);
            return result.INFO_NUM;
        }
    }
}
