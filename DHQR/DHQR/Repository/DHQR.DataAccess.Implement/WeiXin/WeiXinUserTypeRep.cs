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
    /// 微信用户类型
    /// </summary>
    public class WeiXinUserTypeRep : ProRep<WeiXinUserType>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public WeiXinUserTypeRep(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<WeiXinUserType> EntityCurrentSet
        {
            get { return ActiveContext.WeiXinUserTypes; }
        }

        /// <summary>
        /// 根据代码获取用户类型
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public WeiXinUserType GetByCode(int code)
        {
            return ActiveContext.WeiXinUserTypes.SingleOrDefault(f => f.Code == code);
        }
    }
}
