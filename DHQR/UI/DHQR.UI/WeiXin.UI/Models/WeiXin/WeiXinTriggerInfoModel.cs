using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.UI.Model;
using Common.BLL.Implement;
using DHQR.DataAccess.Entities;
using DHQR.BusinessLogic.Implement;

namespace DHQR.UI.Models
{
    public class WeiXinTriggerInfoModel
    {

        public System.Guid Id { get; set; }
        public string Code { set; get; }
        public string ActionUrl { get; set; }
        public string KeyWord { get; set; }
        public string MsgTitle { get; set; }
        public string MsgCoverPath { get; set; }
        public int PatternMethod { get; set; }
        public string PatternMethodStr
        {
            get
            {
                if (PatternMethod == KeyWordPatternMethod.Fuzzy)
                {
                    return "模糊匹配";
                }
                else
                {
                    return "精确匹配";
                }
            }
        }
    }

    #region Services
    public class WeiXinTriggerInfoModelService : BaseModelService<WeiXinTriggerInfo, WeiXinTriggerInfoModel>
    {
        private readonly WeiXinTriggerInfoLogic _weiXinTriggerInfoLogic;
        public WeiXinTriggerInfoModelService()
        {
            _weiXinTriggerInfoLogic = new WeiXinTriggerInfoLogic();
        }

        protected override BaseLogic<WeiXinTriggerInfo> BaseLogic
        {
            get { return _weiXinTriggerInfoLogic; }
        }

        //获取图片资源下拉列表
        internal IList<SelectListItem> creatSelectList()
        {
            IList<SelectListItem> result = GetAll().Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.KeyWord }).ToList();
            return result;
        }
    }
    #endregion
}