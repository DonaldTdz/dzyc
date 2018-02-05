using System;
using Basic.DAl;
using Common.BLL.Implement;
using Common.DAL.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;
using System.Linq;
using System.Collections.Generic;

namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// 微信系统管理模块类型
    /// </summary>
    public class WeiXInSysTypeLogic : BaseLogic<WeiXinSysType>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private WeiXInSysTypeRep _repository { get { return new WeiXInSysTypeRep(_baseDataEntities); } }


        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<WeiXinSysType> Repository
        {
            get { return _repository; }
        }

        /// <summary>
        /// 根据APPID获取模块类型
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public IList<WeiXinSysType> GetByAppId(Guid appId)
        {
            var result = _repository.GetByAppId(appId);
            return result;
        }
    }
}
