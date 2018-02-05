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
    /// 车辆检查流水号数据访问层
    /// </summary>
    public class CarCheckKeyRepository : ProRep<CarCheckKey>
    {
         /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public CarCheckKeyRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<CarCheckKey> EntityCurrentSet
        {
            get { return ActiveContext.CarCheckKeys; }
        }

        /// <summary>
        /// 获取流水号
        /// </summary>
        /// <returns></returns>
        public string GetLogkey()
        {
            Guid id = Guid.NewGuid();
            CarCheckKey key = new CarCheckKey
            {
                Id = id
            };
            var sql = "insert into CarCheckKeys values('" + id + "')";
            ActiveContext.ExecuteStoreCommand(sql);
            var result = ActiveContext.CarCheckKeys.SingleOrDefault(f => f.Id == id);
            return result.INFO_NUM;
        }

        /// <summary>
        /// 获取指定量的流水号
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<string> GetLogKeys(int count)
        {
            IList<string> result = new List<string>();
            for (int i = 0; i < count; i++)
            {
                var t = GetLogkey();
                result.Add(t);
            }
            return result;
        }
    }
}
