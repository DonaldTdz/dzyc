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
    /// 功能角色数据服务层
    /// </summary>
    public class ModuleRoleRepository : ProRep<ModuleRole>
    {
        #region  基础

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public ModuleRoleRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<ModuleRole> EntityCurrentSet
        {
            get { return ActiveContext.ModuleRoles; }
        }


        #endregion

        /// <summary>
        /// 向功能角色添加用户
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="roleId"></param>
        /// <param name="dohandle"></param>
        public void AddUserToRole(IList<Guid> userIds, Guid roleId, out DoHandle dohandle)
        {
            dohandle=new DoHandle{IsSuccessful=false,OperateMsg="添加失败"};
            foreach (var item in userIds)
            {
                UserModuleRole um = new UserModuleRole 
                {
                    Id=Guid.NewGuid(),
                    ModuleRoleId=roleId,
                    UserId=item
                };
                ActiveContext.UserModuleRoles.AddObject(um);
                /*
                foreach (var m in moduleIds)
                {
                    UserFastModule fastModel = new UserFastModule 
                    {
                        Id=Guid.NewGuid(),
                        ModuleId=m,
                        UserId=item
                    };
                    ActiveContext.UserFastModules.AddObject(fastModel);
                }
                 */
            }
            ActiveContext.SaveChanges();
            dohandle.IsSuccessful = true;
            dohandle.OperateMsg = "添加成功!";
        }

        /// <summary>
        /// 删除功能角色对应用户
        /// </summary>
        /// <param name="userRoleIds"></param>
        /// <param name="roleId"></param>
        /// <param name="dohandle"></param>
        public void DelUserToRole(IList<Guid> userRoleIds, Guid roleId, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "删除失败" };
            IList<UserModuleRole> userModuleRoles=ActiveContext.UserModuleRoles.Where(f=>userRoleIds.Contains(f.UserId) && f.ModuleRoleId==roleId).ToList();
            foreach (var item in userModuleRoles)
            {
                ActiveContext.UserModuleRoles.DeleteObject(item);
            }
            ActiveContext.SaveChanges();
            dohandle.IsSuccessful = true;
            dohandle.OperateMsg = "删除成功!";

        }


        /// <summary>
        /// 向功能角色添加功能项
        /// </summary>
        /// <param name="moduleIds"></param>
        /// <param name="roleId"></param>
        /// <param name="dohandle"></param>
        public void AddModuleToRole(IList<Guid> moduleIds, Guid roleId, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "添加失败" };
            foreach (var item in moduleIds)
            {
                ModuleRoleToModule um = new ModuleRoleToModule
                {
                    Id = Guid.NewGuid(),
                    ModuleRoleId = roleId,
                    ModuleId = item
                };
                ActiveContext.ModuleRoleToModules.AddObject(um);
            }
            ActiveContext.SaveChanges();
            dohandle.IsSuccessful = true;
            dohandle.OperateMsg = "添加成功!";

        }

        /// <summary>
        /// 删除功能角色对应功能项
        /// </summary>
        /// <param name="userRoleIds"></param>
        /// <param name="roleId"></param>
        /// <param name="dohandle"></param>
        public void DelModuleToRole(IList<Guid> mtomIds, Guid roleId, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "删除失败" };
            IList<ModuleRoleToModule> moduleRoleToModules = ActiveContext.ModuleRoleToModules.Where(f => mtomIds.Contains(f.ModuleId) && f.ModuleRoleId==roleId).ToList();
            foreach (var item in moduleRoleToModules)
            {
                ActiveContext.ModuleRoleToModules.DeleteObject(item);
            }
            ActiveContext.SaveChanges();
            dohandle.IsSuccessful = true;
            dohandle.OperateMsg = "删除成功!";

        }

        /// <summary>
        /// 根据用户Id获取功能角色清单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<ModuleRole> GetRolesByUser(Guid userId)
        {
            var roleIds = ActiveContext.UserModuleRoles.Where(f => f.UserId == userId).Select(t=>t.ModuleRoleId);
            var result = ActiveContext.ModuleRoles.Where(f => roleIds.Contains(f.Id)).ToList();
            return result;
        }
    }

}
