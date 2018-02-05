using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Base;
using Common.BLL.Implement;
using Common.UI.Model;
using DHQR.BusinessLogic.Implement;
using DHQR.DataAccess.Entities;

namespace DHQR.UI.Models
{
    /// <summary>
    /// 微信用户类型对应功能项模型
    /// </summary>
    public class WeiXinUserTypeToModuleModel
    {
        #region 基元属性


        public Guid Id { get; set; }
        public Guid WeiXinUserTypeId { get; set; }
        public Guid ModuleId { get; set; }


        #endregion
    }

    #region Services

    public class WeiXinUserTypeToModuleModelService : BaseModelService<WeiXinUserTypeToModule, WeiXinUserTypeToModuleModel>
    {
        private readonly WeiXinUserTypeToModuleLogic BusinessLogic;
        public WeiXinUserTypeToModuleModelService()
        {
            BusinessLogic = new WeiXinUserTypeToModuleLogic();
        }

        protected override BaseLogic<WeiXinUserTypeToModule> BaseLogic
        {
            get { return BusinessLogic; }
        }

        /// <summary>
        /// 添加功能项到用户类型
        /// </summary>
        /// <param name="moduleIds"></param>
        /// <param name="typeId"></param>
        /// <param name="dohandle"></param>
        public void AddModuleToType(string moduleIds, Guid typeId, out DoHandle dohandle)
        {
            var datas = moduleIds.Split(',').ToList();
            IList<Guid> Ids = new List<Guid>();
            foreach (var item in datas)
            {
                var id = Guid.Parse(item);
                Ids.Add(id);
            }
            BusinessLogic.AddModuleToType(Ids,typeId,out dohandle);
        }

        /// <summary>
        /// 添加功能项到用户类型
        /// </summary>
        /// <param name="moduleIds"></param>
        /// <param name="typeId"></param>
        /// <param name="dohandle"></param>
        public void DelModuleToType(string moduleIds, Guid typeId, out DoHandle dohandle)
        {
            var datas = moduleIds.Split(',').ToList();
            IList<Guid> Ids = new List<Guid>();
            foreach (var item in datas)
            {
                var id = Guid.Parse(item);
                Ids.Add(id);
            }
            BusinessLogic.DelModuleToType(Ids, typeId, out dohandle);

        }

    }

    #endregion

}