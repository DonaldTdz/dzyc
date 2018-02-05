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
    /// 功能项数据服务层
    /// </summary>
    public class ModuleRepository : ProRep<Module>
    {
        #region  基础

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public ModuleRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<Module> EntityCurrentSet
        {
            get { return ActiveContext.Modules; }
        }


        #endregion

        /// <summary>
        /// 根据功能角色ID获取功能项
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<Module> GetModuleByRoleId(Guid roleId)
        {
            var result = (from r in ActiveContext.ModuleRoleToModules
                         join m in ActiveContext.Modules
                         on r.ModuleId equals m.Id
                         where r.ModuleRoleId == roleId
                         select m).ToList();
            return result;
        }

        /// <summary>
        /// 根据功能角色ID获取功能项
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<Module> GetModuleByTypeId(Guid typeId)
        {
            var result = (from r in ActiveContext.WeiXinUserTypeToModules
                          join m in ActiveContext.Modules
                          on r.ModuleId equals m.Id
                          where r.WeiXinUserTypeId == typeId
                          select m).ToList();
            return result;
        }

    }

}
