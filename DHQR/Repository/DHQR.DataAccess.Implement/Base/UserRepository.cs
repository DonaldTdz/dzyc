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
    /// 用户实体操作类
    /// </summary>
    public class UserRepository : ProRep<User>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public UserRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<User> EntityCurrentSet
        {
            get { return ActiveContext.Users; }
        }
        public User GetByLoginName(string name)
        {
            return SingleOrDefualt(f => f.Name == name);
        }
        public override void Create(User entity, out DoHandle status)
        {
            entity.RegisterTime = DateTime.Now;
            base.Create(entity, out status);
        }

        public override void Update(User entity, out DoHandle status)
        {
            var data = GetByKey(entity.Id);
            data.Nickname = entity.Nickname;
            base.Update(data, out status);
        }

        public void Freeze(Guid entityid, bool isFreeze, out DoHandle status)
        {
            var data = GetByKey(entityid);

            data.IsFreeze = isFreeze;

            base.Update(data, out status);
        }

        /// <summary>
        /// 根据功能角色ID获取用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<User> GetUserByRoleId(Guid roleId)
        {
            var result = (from m in ActiveContext.UserModuleRoles
                          join u in ActiveContext.Users
                          on m.UserId equals u.Id
                          where m.ModuleRoleId == roleId
                          select u).ToList();
            return result;

        }

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        /// <param name="logonName"></param>
        /// <returns></returns>
        public bool IsSuperAdmin(string logonName)
        {
            var user = ActiveContext.Users.SingleOrDefault(f => f.Name == logonName);
            //var moduleIds = ActiveContext.UserModuleRoles.Where(f => f.UserId == user.Id).Select(t=>t.ModuleRoleId).ToList();
            var isSuperAdmin = ActiveContext.ModuleRoles.Any(f => f.Code == "SuperAdmin");
            return isSuperAdmin;
        }

        /// <summary>
        /// 是否管理员(包含DHQR管理员和超级管理员)
        /// </summary>
        /// <param name="logonName"></param>
        /// <returns></returns>
        public bool IsAdmin(string logonName)
        {
            var user = ActiveContext.Users.SingleOrDefault(f => f.Name == logonName);
            var moduleIds = ActiveContext.UserModuleRoles.Where(f => f.UserId == user.Id).Select(t => t.ModuleRoleId).ToList();
            var isAdmin = ActiveContext.ModuleRoles.Any(f => moduleIds.Contains(f.Id) && (f.Code == "SuperAdmin" || f.Code == "Admin"));
            return isAdmin;

        }

        /// <summary>
        /// 是否领导
        /// </summary>
        /// <param name="logonName"></param>
        /// <returns></returns>
        public bool IsLeader(string logonName)
        {
            var user = ActiveContext.Users.SingleOrDefault(f => f.Name == logonName);
            var moduleIds = ActiveContext.UserModuleRoles.Where(f => f.UserId == user.Id).Select(t => t.ModuleRoleId).ToList();
            var isLeader = ActiveContext.ModuleRoles.Any(f => moduleIds.Contains(f.Id) && f.Code == "Leader");
            return isLeader;

        }


        /// <summary>
        /// 同意用户申请
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="roleId">The role id.</param>
        /// <param name="dohandle">The dohandle.</param>
        public void CreateByAppy(User user, Guid roleId, out DoHandle dohandle)
        {
            dohandle = new DoHandle {OperateMsg="创建失败!",IsSuccessful=false };
            UserModuleRole userModuleRole = new UserModuleRole 
            {
                Id=Guid.NewGuid(),
                ModuleRoleId=roleId,
                UserId=user.Id
            };
            ActiveContext.Users.AddObject(user);
            ActiveContext.UserModuleRoles.AddObject(userModuleRole);
            ActiveContext.SaveChanges();
            dohandle.OperateMsg = "创建成功！";
            dohandle.IsSuccessful = true;
        }

        /// <summary>
        /// 修改用户名 同时同步其他表的用户信息
        /// </summary>
        /// <param name="dohandle"></param>
        public void UpdateOtherUserNames(out DoHandle dohandle)
        {
            dohandle = new DoHandle {IsSuccessful=false,OperateMsg="更新失败！" };
            //var sql = "exec Pro_UpdateOtherUserName";
            //var r= ActiveContext.ExecuteStoreCommand(sql);
            dohandle.IsSuccessful = true;
            dohandle.OperateMsg = "更新成功!";
        }
    }

}
