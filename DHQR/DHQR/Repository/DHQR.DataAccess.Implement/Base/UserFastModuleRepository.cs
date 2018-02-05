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
    /// 用户对应功能项数据服务层
    /// </summary>
    public class UserFastModuleRepository : ProRep<UserFastModule>
    {
        #region  基础

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public UserFastModuleRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<UserFastModule> EntityCurrentSet
        {
            get { return ActiveContext.UserFastModules; }
        }


        #endregion


    }

}
