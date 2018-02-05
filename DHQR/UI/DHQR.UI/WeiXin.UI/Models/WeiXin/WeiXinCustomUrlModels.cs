using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.UI.Model;
using DHQR.DataAccess.Entities;
using DHQR.BusinessLogic.Implement;
using Common.BLL.Implement;
using DHQR.BusinessLogic.Implement.WeiXin;
using System.Web.Mvc;


namespace DHQR.UI.Models
{
    #region 模型

    public class WeiXinCustomUrlModel
    {
        #region 基元属性
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Url { get; set; }
        public Guid WeiXinAppId { get; set; }
        #endregion

    }

    #endregion

    #region Services

    public class WeiXinCustomUrlModelService : BaseModelService<WeiXinCustomUrl, WeiXinCustomUrlModel>
    {
        private readonly WeiXinCustomUrlLogic _WeiXinCustomUrlLogic;
        public WeiXinCustomUrlModelService()
        {
            _WeiXinCustomUrlLogic = new WeiXinCustomUrlLogic();
        }

        protected override BaseLogic<WeiXinCustomUrl> BaseLogic
        {
            get { return _WeiXinCustomUrlLogic; }
        }

        internal IList<SelectListItem> creatSelectList()
        {
            IList<SelectListItem> result = GetAll().Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }).ToList();
            return result;
        }
    }

    #endregion

}