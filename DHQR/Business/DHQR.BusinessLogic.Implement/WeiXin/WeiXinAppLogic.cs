using System;
using Basic.DAl;
using Common.BLL.Implement;
using Common.DAL.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;
using System.Linq;


namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// 微信配置
    /// </summary>
    public class WeiXinAppLogic : BaseLogic<WeiXinApp>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private WeiXinAppRep _repository { get { return new WeiXinAppRep(_baseDataEntities); } }


        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<WeiXinApp> Repository
        {
            get { return _repository; }
        }

        /// <summary>
        /// 根据用户ID获取公众号(1.0版本只返回一个账号)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public WeiXinApp GetOneByUserId(Guid userId)
        {
            var result = _repository.GetOneByUserId(userId);
            return result;
        }


        /// <summary>
        /// 根据微信KEY值获取公众号
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public WeiXinApp GetByWeiXinKey(string key)
        {
            var result = _repository.GetByWeiXinKey(key);
            return result;
        }

    }
}
