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
    /// 微信用户类型模型
    /// </summary>
    public class WeiXinUserTypeModel
    {
        #region 基元属性


        public Guid Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }


        #endregion
    }


    #region Services
    public class WeiXinUserTypeModelService : BaseModelService<WeiXinUserType, WeiXinUserTypeModel>
    {
        private readonly WeiXinUserTypeLogic BusinessLogic;
        public WeiXinUserTypeModelService()
        {
            BusinessLogic = new WeiXinUserTypeLogic();
        }

        protected override BaseLogic<WeiXinUserType> BaseLogic
        {
            get { return BusinessLogic; }
        }

        
        /// <summary>
        /// 根据代码获取用户类型
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public WeiXinUserTypeModel GetByCode(int code)
        {
            var result =ConvertToModel( BusinessLogic.GetByCode(code));
            return result;
        }

    }

    #endregion
}