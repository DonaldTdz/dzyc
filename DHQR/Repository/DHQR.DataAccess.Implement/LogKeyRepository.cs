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
    /// 日志流水号数据访问层
    /// </summary>
    public class LogKeyRepository : ProRep<LogKey>
    {
         /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public LogKeyRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<LogKey> EntityCurrentSet
        {
            get { return ActiveContext.LogKeys; }
        }

        /// <summary>
        /// 获取流水号
        /// </summary>
        /// <returns></returns>
        public string GetLogkey()
        {
            Guid id = Guid.NewGuid();
            LogKey key = new LogKey
            {
                Id = id
            };
            var sql = "insert into LogKeys values('" + id + "')";
            ActiveContext.ExecuteStoreCommand(sql);
            var result = ActiveContext.LogKeys.SingleOrDefault(f => f.Id == id);
            return result.LogKey1;
        }
    }
}
