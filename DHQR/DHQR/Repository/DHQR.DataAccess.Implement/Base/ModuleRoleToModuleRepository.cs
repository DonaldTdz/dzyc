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
    /// 功能角色对应功能项数据服务层
    /// </summary>
    public class ModuleRoleToModuleRepository : ProRep<ModuleRoleToModule>
    {
        #region  基础

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public ModuleRoleToModuleRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<ModuleRoleToModule> EntityCurrentSet
        {
            get { return ActiveContext.ModuleRoleToModules; }
        }


        #endregion


    }

}
