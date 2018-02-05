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
using System.Web.Script.Serialization;

namespace DHQR.UI.Models
{
    /// <summary>
    /// 功能角色模型
    /// </summary>
    public class ModuleRoleModel
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
        /// 代码
        /// </summary>
        public string Code
        {
            set;
            get;
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            set;
            get;
        }
		/// <summary>
		/// 角色说明
		/// </summary>
        public string Note
        {
            set;
            get;
        }
    }

    /// <summary>
    /// 功能角色服务模型
    /// </summary>
    public class ModuleRoleModelService : BaseModelService<ModuleRole, ModuleRoleModel>
    {
        #region Base

        private readonly ModuleRoleLogic BusinessLogic;

        public ModuleRoleModelService()
        {
            BusinessLogic = new ModuleRoleLogic();
        }

        protected override BaseLogic<ModuleRole> BaseLogic
        {
            get { return BusinessLogic; }
        }

        #endregion


        #region

        /// <summary>
        /// 向功能角色添加用户
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="roleId"></param>
        /// <param name="dohandle"></param>
        public void AddUserToRole(string userIds, Guid roleId, out DoHandle dohandle)
        {
            IList<Guid> Ids = userIds.Split(',').Select(f => Guid.Parse(f)).ToList();
            BusinessLogic.AddUserToRole(Ids, roleId, out dohandle);
        }

        /// <summary>
        /// 删除功能角色对应用户
        /// </summary>
        /// <param name="userRoleIds"></param>
        /// <param name="roleId"></param>
        /// <param name="dohandle"></param>
        public void DelUserToRole(string userIds, Guid roleId, out DoHandle dohandle)
        {
            IList<Guid> Ids = userIds.Split(',').Select(f => Guid.Parse(f)).ToList();
            BusinessLogic.DelUserToRole(Ids, roleId, out dohandle);

        }


        /// <summary>
        /// 向功能角色添加功能项
        /// </summary>
        /// <param name="moduleIds"></param>
        /// <param name="roleId"></param>
        /// <param name="dohandle"></param>
        public void AddModuleToRole(string moduleIds, Guid roleId, out DoHandle dohandle)
        {
            var datas = moduleIds.Split(',').ToList();
            IList<Guid> Ids = new List<Guid>();
            foreach (var item in datas)
            {
                var id = Guid.Parse(item);
                Ids.Add(id);
            }
            BusinessLogic.AddModuleToRole(Ids, roleId, out dohandle);

        }

        /// <summary>
        /// 删除功能角色对应功能项
        /// </summary>
        /// <param name="userRoleIds"></param>
        /// <param name="roleId"></param>
        /// <param name="dohandle"></param>
        public void DelModuleToRole(string mtomIds, Guid roleId, out DoHandle dohandle)
        {
            IList<Guid> Ids = mtomIds.Split(',').Select(f => Guid.Parse(f)).ToList();
            BusinessLogic.DelModuleToRole(Ids, roleId, out dohandle);

        }

        /// <summary>
        /// 取用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string[] GetRolesByUser(Guid userId) 
        {
            var result = BusinessLogic.GetRolesByUser(userId).Select(f => f.Code).ToArray();
            return result;
        }


        #endregion
    }
}