using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;

namespace DHQR.DataAccess.Implement.WeiXin
{
    /// <summary>
    /// 系统URL
    /// </summary>
    public class WeiXinSysUrlRep : ProRep<WeiXinSysUrl>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public WeiXinSysUrlRep(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<WeiXinSysUrl> EntityCurrentSet
        {
            get { return ActiveContext.WeiXinSysUrls; }
        }

        public bool CreateSysUrl(WeiXinSysUrl entity)
        {
            string sqlStr = string.Format(@"insert into WeiXinSysUrls values('{0}','{1}','{2}','{3}','{4}','{5}')",
                                  entity.Id, entity.Name, entity.Url, entity.WeiXinSysTypeId, entity.WeiXinSysTypeName, entity.WeiXinAppId);
            var resultCount = ActiveContext.ExecuteStoreCommand(sqlStr);
            return resultCount == 1;
        }

        /// <summary>
        /// 根据模块ID获取系统URL
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public IList<WeiXinSysUrl> GetByTypeId(Guid typeId)
        {
            var result = EntityCurrentSet.Where(f => f.WeiXinSysTypeId == typeId).ToList();
            return result;
        }
    }
}
