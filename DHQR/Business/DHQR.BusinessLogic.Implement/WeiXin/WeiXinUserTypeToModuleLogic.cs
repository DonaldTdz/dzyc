using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.BLL.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;
using Common.DAL.Implement;
using Common.Base;

namespace DHQR.BusinessLogic.Implement
{

    /// <summary>
    /// 微信用户对应功能项
    /// </summary>
    public class WeiXinUserTypeToModuleLogic : BaseLogic<WeiXinUserTypeToModule>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private WeiXinUserTypeToModuleRep repository { get { return new WeiXinUserTypeToModuleRep(_baseDataEntities); } }


        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<WeiXinUserTypeToModule> Repository
        {
            get { return repository; }
        }

        /// <summary>
        /// 添加功能项到用户类型
        /// </summary>
        /// <param name="moduleIds"></param>
        /// <param name="typeId"></param>
        /// <param name="dohandle"></param>
        public void AddModuleToType(IList<Guid> moduleIds, Guid typeId, out DoHandle dohandle)
        {
            repository.AddModuleToType(moduleIds, typeId, out dohandle);
        }


        /// <summary>
        /// 添加功能项到用户类型
        /// </summary>
        /// <param name="moduleIds"></param>
        /// <param name="typeId"></param>
        /// <param name="dohandle"></param>
        public void DelModuleToType(IList<Guid> moduleIds, Guid typeId, out DoHandle dohandle)
        {
            repository.DelModuleToType(moduleIds, typeId, out dohandle);
        }



    }


}
