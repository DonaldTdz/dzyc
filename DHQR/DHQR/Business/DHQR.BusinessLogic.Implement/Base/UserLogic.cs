using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.BLL.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;
using Common.DAL.Implement;
using Common.Base;
using System.Transactions;

namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// 用户逻辑层 
    /// </summary>
    public class UserLogic : BaseLogic<User>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private UserRepository UserRep { get { return new UserRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<User> Repository
        {
            get { return UserRep; }
        }

        public PagedResults<User> GetUserPageData(QueryParam queryParam)
        {
            var userData = Search(queryParam);

            return userData;
        }

        /// <summary>
        /// 冻结
        /// </summary>
        /// <param name="entiryId"></param>
        /// <param name="isFreeze">冻结标识</param>
        /// <param name="doHandle"></param>
        public void Freeze(Guid entiryId, bool isFreeze, out DoHandle doHandle)
        {
            doHandle = new DoHandle();
            try
            {
                UserRep.Freeze(entiryId, isFreeze, out doHandle);
            }
            catch (Exception e)
            {
                doHandle.IsSuccessful = false;
                doHandle.OperateMsg = e.Message;
            }

        }



        public void DoRegister(User user, out DoHandle doHandle)
        {
            doHandle = new DoHandle();
            var c = Any(f => f.Name == user.Name);
            if (c)
            {
                doHandle.IsSuccessful = false;
                doHandle.OperateMsg = "已经存在该用户名";
                return;
            }
            Create(user, out  doHandle);
        }
        /// <summary>
        /// 根据用户名获取
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public User GetByName(string name)
        {
            //  UserRep.Freeze121212();
            var data = SingleOrDefualt(f => f.Name == name);
            return data;
        }

        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oldPsd"></param>
        /// <param name="newPsd"></param>
        /// <param name="doHandle"></param>
        public void ChangePsd(Guid userId, string oldPsd, string newPsd, out DoHandle doHandle)
        {
            doHandle = new DoHandle();
            var data = SingleOrDefualt(f => f.Id == userId);
            if (data == null || data.PassWord.ToUpper() != oldPsd)
            {
                doHandle.IsSuccessful = false;
                doHandle.OperateMsg = "帐号不正确或密码错误！";
                return;
            }
            data.PassWord = newPsd;
            Update(data, out doHandle);
        }

        /// <summary>
        /// 修改用户名 同时同步其他表的用户信息
        /// </summary>
        /// <param name="dohandle"></param>
        public void UpdateOtherUserNames(out DoHandle dohandle)
        {
            UserRep.UpdateOtherUserNames(out dohandle);
        }

        #region 功能权限

        /// <summary>
        /// 根据传入的congtroller和action判断用户是拥有相应功能项的操作权限
        /// </summary>
        /// <param name="logonName"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool HasModuleAuthority(string logonName, string controllerName, string actionName,
                                        out DoHandle handle)
        {
            handle = new DoHandle { IsSuccessful=false };
            UserModuleAuthority userFeatureAuthority =
                new UserModuleAuthorityRepository().GetByLogonName(logonName);
            if (userFeatureAuthority == null || userFeatureAuthority.Modules == null ||
                userFeatureAuthority.Modules.Count == 0)
            {
                return false;
            }
            bool hasAny =
                userFeatureAuthority.Modules.Where(
                    f =>
                    f.ControllerName.ToUpper() == controllerName.ToUpper() &&
                    f.ActionName.ToUpper() == actionName.ToUpper()).Any();
            if (hasAny)
            {
                handle.IsSuccessful = true;
            }
            return hasAny;
        }

        /// <summary>
        /// 根据传入的code判断用户是否拥有相应功能项的操作权限
        /// </summary>
        /// <param name="logonName"></param>
        /// <param name="code"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool HasModuleAuthority(string logonName, string code, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false };
            UserModuleAuthority userFeatureAuthority =
               new UserModuleAuthorityRepository().GetByLogonName(logonName);
            if (userFeatureAuthority == null || userFeatureAuthority.Modules == null ||
                userFeatureAuthority.Modules.Count == 0)
            {
                return false;
            }
            bool hasAny = userFeatureAuthority.Modules.Where(f => f.Code == code).Any();
            if (hasAny)
            {
                dohandle.IsSuccessful = true;
            }
            return hasAny;
        }

        /// <summary>
        /// 根据功能角色ID获取用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<User> GetUserByRoleId(Guid roleId)
        {
            return UserRep.GetUserByRoleId(roleId);
        }

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        /// <param name="logonName"></param>
        /// <returns></returns>
        public bool IsSuperAdmin(string logonName)
        {
            return UserRep.IsSuperAdmin(logonName);
        }

        #endregion


        #region 用户申请

        /// <summary>
        /// 同意用户申请
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="roleId">The role id.</param>
        /// <param name="dohandle">The dohandle.</param>
        public void CreateByAppy(User user,Guid roleId,out DoHandle dohandle)
        {

            UserRep.CreateByAppy(user, roleId, out dohandle);
        }

        #endregion



    }
}
