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
    /// 退货单流水号数据访问层
    /// </summary>
    public class ReOrderKeyRepository : ProRep<ReOrderKey>
    {
         /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public ReOrderKeyRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<ReOrderKey> EntityCurrentSet
        {
            get { return ActiveContext.ReOrderKeys; }
        }

        /// <summary>
        /// 获取流水号
        /// </summary>
        /// <returns></returns>
        public string GetLogkey()
        {
            Guid id = Guid.NewGuid();
            ReOrderKey key = new ReOrderKey
            {
                Id = id
            };
            var sql = "insert into ReOrderKeys values('" + id + "')";
            ActiveContext.ExecuteStoreCommand(sql);
            var result = ActiveContext.ReOrderKeys.SingleOrDefault(f => f.Id == id);
            return result.INFO_NUM;
        }
    }
}
