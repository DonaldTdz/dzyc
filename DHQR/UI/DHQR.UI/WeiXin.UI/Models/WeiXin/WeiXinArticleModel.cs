using Common.Base;
using Common.BLL.Implement;
using Common.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DHQR.BusinessLogic.Implement;
using DHQR.BusinessLogic.Implement.WeiXin;
using DHQR.DataAccess.Entities;

namespace DHQR.UI.Models
{
    #region 模型
    /// <summary>
    /// 文章管理
    /// </summary>
    public class WeiXinArticleModel
    {
        /// <summary>
        /// ID
        /// </summary>		
        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 名称
        /// </summary>		
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 标题
        /// </summary>		
        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// 内容
        /// </summary>		
        private string _content;
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }
        /// <summary>
        /// 图片地址
        /// </summary>		
        private string _picurl;
        public string PicUrl
        {
            get { return _picurl; }
            set { _picurl = value; }
        }
        /// <summary>
        /// 描述
        /// </summary>		
        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        /// <summary>
        /// URL
        /// </summary>		
        private string _url;
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        /// <summary>
        /// 文章类型
        /// </summary>		
        private Guid _articletype;
        public Guid ArticleType
        {
            get { return _articletype; }
            set { _articletype = value; }
        }
        /// <summary>
        /// 排序号
        /// </summary>		
        private int _articlesort;
        public int ArticleSort
        {
            get { return _articlesort; }
            set { _articlesort = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>		
        private DateTime _createtime;
        public DateTime CreateTime
        {
            get { return _createtime; }
            set { _createtime = value; }
        }
        /// <summary>
        /// 公众账号ID
        /// </summary>		
        private Guid _weixinappid;
        public Guid WeiXinAppId
        {
            get { return _weixinappid; }
            set { _weixinappid = value; }
        }

        /// <summary>
        /// 文章类型名称
        /// </summary>
        public string ArticleTypeName { get; set; }
    }
    #endregion

    #region 服务
    /// <summary>
    /// 文章管理
    /// </summary>
    public class WeiXinArticleModelService  : BaseModelService<WeiXinArticle, WeiXinArticleModel>
    {
        private readonly WeiXinArticleLogic _WeiXinArticleLogic;
        public WeiXinArticleModelService()
        {
            _WeiXinArticleLogic = new WeiXinArticleLogic();
        }

        protected override BaseLogic<WeiXinArticle> BaseLogic
        {
            get { return _WeiXinArticleLogic; }
        }

        /// <summary>
        /// 重写方法
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public override PagedResults<WeiXinArticleModel> GetPageData(QueryParam queryParam)
        {
            var result=base.GetPageData(queryParam);
            WeiXinArticlesTypeLogic  typeLogic=new WeiXinArticlesTypeLogic();
            var typeData=typeLogic.GetAll();
            result.Data=result.Data.Select(f=>{
                var type=typeData.SingleOrDefault(p=>p.Id==f.ArticleType);
                f.ArticleTypeName=type.Name;
               return f;
            }).ToList();
            return result;
        }
    }
    #endregion
}