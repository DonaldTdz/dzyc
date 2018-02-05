using System;
using Common.Base;
using Common.BLL.Implement;
using Common.DAL.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;

namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// 用户逻辑层 
    /// </summary>
    public class WeiXinKeyWordLogic : BaseLogic<WeiXinKeyWord>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private WeiXinKeyWordRep weiXinKeyWordRep { get { return new WeiXinKeyWordRep(_baseDataEntities); } }
        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<WeiXinKeyWord> Repository
        {
            get { return weiXinKeyWordRep; }
        }


        /// <summary>
        /// 根据内容和匹配方式获取关键字
        /// 如果多条返回第一条
        /// </summary>
        /// <param name="weiXinAppId">公众号ID</param>
        /// <param name="content">内容</param>
        /// <param name="patternMethod">匹配方式</param>
        /// <returns></returns>
        public WeiXinKeyWord GetByContent(Guid weiXinAppId, string content)
        {
            return weiXinKeyWordRep.GetByContent(weiXinAppId, content);
        }

    }
}
