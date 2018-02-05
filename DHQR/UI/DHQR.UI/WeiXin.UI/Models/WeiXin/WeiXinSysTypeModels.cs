using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.BLL.Implement;
using Common.UI.Model;
using DHQR.BusinessLogic.Implement;
using DHQR.DataAccess.Entities;

namespace DHQR.UI.Models
{
    #region 模型

    /// <summary>
    /// 微信系统管理模块类型
    /// </summary>
    public class WeiXinSysTypeModel
    {
        #region 基元属性


        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public Guid WeiXinAppId { get; set; }

        #endregion


    }

    #endregion

    #region 服务
    /// <summary>
    /// 微信系统管理模块类型服务
    /// </summary>
    public class WeiXinSysTypeModelService : BaseModelService<WeiXinSysType, WeiXinSysTypeModel>
    {
        private readonly WeiXInSysTypeLogic BusinessLogic;
        public WeiXinSysTypeModelService()
        {
            BusinessLogic = new WeiXInSysTypeLogic();
        }

        protected override BaseLogic<WeiXinSysType> BaseLogic
        {
            get { return BusinessLogic; }
        }



        internal WeiXinSysTypeModel GetByName(string TypeName, Guid weiXinAppId)
        {
            return Query(p => p.Name.Equals(TypeName) && p.WeiXinAppId == weiXinAppId).FirstOrDefault();
        }

        /// <summary>
        /// 根据APPID获取模块类型
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public IList<WeiXinSysTypeModel> GetByAppId(Guid appId)
        {
            var result = BusinessLogic.GetByAppId(appId).Select(f => ConvertToModel(f)).ToList();
            return result;
        }
    }

    #endregion
}