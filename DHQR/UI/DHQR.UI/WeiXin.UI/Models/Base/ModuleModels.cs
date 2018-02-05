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
    /// 功能项模型
    /// </summary>
    public class ModuleModel
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
        /// 控制器类名
        /// </summary>
        public string ControllerName
        {
            set;
            get;
        }
        /// <summary>
        /// 控制器类的ACTION
        /// </summary>
        public string ActionName
        {
            set;
            get;
        }
        /// <summary>
        /// 是否菜单项
        /// </summary>
        public bool IsMenu
        {
            set;
            get;
        }
        /// <summary>
        /// 功能项地址
        /// </summary>
        public string Url
        {
            set;
            get;
        }
    }

    /// <summary>
    /// 功能项服务模型
    /// </summary>
    public class ModuleModelService : BaseModelService<Module, ModuleModel>
    {
        #region Base

        private readonly ModuleLogic BusinessLogic;

        public ModuleModelService()
        {
            BusinessLogic = new ModuleLogic();
        }

        protected override BaseLogic<Module> BaseLogic
        {
            get { return BusinessLogic; }
        }

        #endregion

        /// <summary>
        /// 根据功能角色ID获取功能项
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<ModuleModel> GetModuleByRoleId(Guid roleId)
        {
            var result = BusinessLogic.GetModuleByRoleId(roleId).Select(ConvertToModel).ToList();
            return result;
        }

        /// <summary>
        /// 根据功能角色ID获取可选择功能项
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<ModuleModel> GetUnSelModuleByRoleId(Guid roleId)
        {
            var allModules = BusinessLogic.GetAll();
            var selModules = BusinessLogic.GetModuleByRoleId(roleId);
            IList<ModuleModel> result = new List<ModuleModel>();
            foreach (var item in allModules)
            {
                var currentDt = selModules.SingleOrDefault(f => f.Id == item.Id);
                if (currentDt == null)
                {
                    var t = ConvertToModel(item);
                    result.Add(t);
                }
            }
            return result;
        }

        /// <summary>
        /// 根据用户类型获取可选择功能项
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public IList<ModuleModel> GetUnSelModuleByTypeId(Guid typeId)
        {
            var allModules = BusinessLogic.GetAll();
            var selModules = BusinessLogic.GetModuleByTypeId(typeId);
            IList<ModuleModel> result = new List<ModuleModel>();
            foreach (var item in allModules)
            {
                var currentDt = selModules.SingleOrDefault(f => f.Id == item.Id);
                if (currentDt == null)
                {
                    var t = ConvertToModel(item);
                    result.Add(t);
                }
            }
            return result;
        }


        /// <summary>
        /// 根据用户类型ID获取功能项
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public IList<ModuleModel> GetModuleByTypeId(Guid typeId)
        {
            var result = BusinessLogic.GetModuleByTypeId(typeId).Select(ConvertToModel).ToList();
            return result;

        }

    }
}