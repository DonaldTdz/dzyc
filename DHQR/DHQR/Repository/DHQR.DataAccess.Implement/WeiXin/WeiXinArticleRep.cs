using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 文章管理
    /// </summary>
    public class WeiXinArticleRep: ProRep<WeiXinArticle>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public WeiXinArticleRep(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<WeiXinArticle> EntityCurrentSet
        {
            get { return ActiveContext.WeiXinArticles; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="status"></param>
        public override void Update(WeiXinArticle model, out Common.Base.DoHandle status)
        {
            var entity = ActiveContext.WeiXinArticles.SingleOrDefault(f => f.Id == model.Id);
            entity.Name = model.Name;
            entity.Title = model.Title;
            entity.Content = model.Content;
            entity.PicUrl = model.PicUrl;
            entity.Description = model.Description;
            entity.Url = model.Url;
            entity.ArticleType = model.ArticleType;
            entity.ArticleSort = model.ArticleSort;
            base.Update(entity, out status);
        }
    }
}
