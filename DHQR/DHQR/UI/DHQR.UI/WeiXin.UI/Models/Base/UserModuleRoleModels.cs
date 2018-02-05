using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.UI.Model;
using DHQR.BusinessLogic.Implement;
using Common.BLL.Implement;
using Common.Base;
using DHQR.DataAccess.Entities;
using System.Web.Mvc;

namespace DHQR.UI.Models
{
    /// <summary>
    /// 用户对应功能角色模型
    /// </summary>
    public class UserModuleRoleModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id
        {
            set;
            get;
        }
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId
        {
            set;
            get;
        }
        /// <summary>
        /// 功能角色ID
        /// </summary>
        public Guid ModuleRoleId
        {
            set;
            get;
        }

    }

    /// <summary>
    /// 用户对应功能角色服务模型
    /// </summary>
    public class UserModuleRoleModelService : BaseModelService<UserModuleRole, UserModuleRoleModel>
    {
        #region Base

        private readonly UserModuleRoleLogic BusinessLogic;

        public UserModuleRoleModelService()
        {
            BusinessLogic = new UserModuleRoleLogic();
        }

        protected override BaseLogic<UserModuleRole> BaseLogic
        {
            get { return BusinessLogic; }
        }

        #endregion
    }
}