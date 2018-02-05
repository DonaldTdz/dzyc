using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.BLL.Implement;
using Common.UI.Model;
using DHQR.BusinessLogic.Implement;
using DHQR.DataAccess.Entities;
using System.Web.Mvc;

namespace DHQR.UI.Models
{
    /// <summary>
    /// 系统URL
    /// </summary>
    public class WeiXinSysUrlModel
    {
        #region 基元属性


        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Url { get; set; }
        public Guid WeiXinAppId { get; set; }
        public string WeiXinSysTypeName { get; set; }
        public Guid WeiXinSysTypeId { get; set; }
        /// <summary>
        /// 文章ID
        /// </summary>
        public Guid? WeiXinArticleId
        {
            get;
            set;
        }
        #endregion

    }

    /// <summary>
    /// 系统URL服务
    /// </summary>
    public class WeiXinSysUrlModelService : BaseModelService<WeiXinSysUrl, WeiXinSysUrlModel>
    {
        private readonly WeiXinSysUrlLogic BusinessLogic;
        public WeiXinSysUrlModelService()
        {
            BusinessLogic = new WeiXinSysUrlLogic();
        }

        protected override BaseLogic<WeiXinSysUrl> BaseLogic
        {
            get { return BusinessLogic; }
        }


        /// <summary>
        /// 根据模块ID获取系统URL
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public IList<WeiXinSysUrlModel> GetByTypeId(Guid typeId)
        {
            var result = BusinessLogic.GetByTypeId(typeId).Select(f=>ConvertToModel(f)).ToList();
            return result;
        }

        internal IList<SelectListItem> creatSelectList()
        {
            IList<SelectListItem> result = BusinessLogic.GetAll().
                                                 Select(p => new SelectListItem
                                                 {
                                                     Value = p.Id.ToString(),
                                                     Text = p.Name
                                                 }).ToList();
            return result;
        }



    }
}