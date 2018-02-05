using Common.BLL.Implement;
using Common.DAL.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;

namespace DHQR.BusinessLogic.Implement.WeiXin
{
    /// <summary>
    /// 文章管理
    /// </summary>
    public class WeiXinArticleLogic : BaseLogic<WeiXinArticle>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private WeiXinArticleRep _repository { get { return new WeiXinArticleRep(_baseDataEntities); } }



        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<WeiXinArticle> Repository
        {
            get { return _repository; }
        }

    }
}
