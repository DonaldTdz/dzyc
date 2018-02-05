using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 文章类型
    /// </summary>
    public class WeiXinArticlesTypeRep : ProRep<WeiXinArticlesType>
    {
         /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public WeiXinArticlesTypeRep(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<WeiXinArticlesType> EntityCurrentSet
        {
            get { return ActiveContext.WeiXinArticlesTypes; }
        }
    }
}
