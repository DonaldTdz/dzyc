using Common.BLL.Implement;
using Common.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHQR.BusinessLogic.Implement;
using DHQR.DataAccess.Entities;

namespace DHQR.UI.Models
{
    /// <summary>
    /// 文章类型
    /// </summary>
    public class WeiXinArticlesTypeModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Code { get; set; }

        public int Sort { get; set; }

        public string Creator { get; set; }

        public DateTime CreateTime { get; set; }

        public string Remark { get; set; }
     }
    /// <summary>
    /// 文章类型
    /// </summary>
    public class WeiXinArticlesTypeModelService : BaseModelService<WeiXinArticlesType, WeiXinArticlesTypeModel>
    {
        private readonly WeiXinArticlesTypeLogic _WeiXinArticlesTypeLogic;
        public WeiXinArticlesTypeModelService()
        {
            _WeiXinArticlesTypeLogic = new WeiXinArticlesTypeLogic();
        }

        /// <summary>
        /// 重写
        /// </summary>
        protected override BaseLogic<WeiXinArticlesType> BaseLogic
        {
            get { return _WeiXinArticlesTypeLogic; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<SelectListItem> GetArticlesType() 
        {
            var data = BaseLogic.GetAll();
            var result = (from item in data
                         select new SelectListItem()
                         {
                             Text = item.Name,
                             Value = item.Id.ToString()
                         }).ToList();
            result.Insert(0, new SelectListItem() { Value = "", Text = "请选择", Selected = true });
            return result;
        }
    }
}