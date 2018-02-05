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
    /// 微信用户类型逻辑层
    /// </summary>
    public class WeiXinUserTypeLogic : BaseLogic<WeiXinUserType>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private WeiXinUserTypeRep repository { get { return new WeiXinUserTypeRep(_baseDataEntities); } }


        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<WeiXinUserType> Repository
        {
            get { return repository; }
        }
        
        /// <summary>
        /// 根据代码获取用户类型
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public WeiXinUserType GetByCode(int code)
        {
            return repository.GetByCode(code);
        }

    }
}
