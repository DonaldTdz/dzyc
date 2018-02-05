using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using Common.Base;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 自定义URL
    /// </summary>
    public class WeiXinCustomUrlRep : ProRep<WeiXinCustomUrl>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public WeiXinCustomUrlRep(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<WeiXinCustomUrl> EntityCurrentSet
        {
            get { return ActiveContext.WeiXinCustomUrls; }
        }

    }
}
