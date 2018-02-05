using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Base;
using Common.BLL.Implement;
using Common.UI.Model;
using DHQR.BusinessLogic.Implement;
using DHQR.DataAccess.Entities;

namespace DHQR.UI.Models
{
    #region 模型
    public class WeiXinFirstInModel
    {

        #region 基元属性


        public Guid Id { get; set; }
        public int Type { get; set; }
        public string ContenInfo { get; set; }
        public bool IsOpen { get; set; }
        public Nullable<Guid> PicMsgOrTirggerInfoId { get; set; }
        public Guid WeiXinAppId { get; set; }
        #endregion
      
    }
    #endregion



    #region Services
    public class WeiXinFirstInModelService : BaseModelService<WeiXinFirstIn, WeiXinFirstInModel>
    {
        private readonly WeiXinFirstInLogic _WeiXinFirstInLogic;
        public WeiXinFirstInModelService()
        {
            _WeiXinFirstInLogic = new WeiXinFirstInLogic();
        }

        protected override BaseLogic<WeiXinFirstIn> BaseLogic
        {
            get { return _WeiXinFirstInLogic; }
        }


        internal WeiXinFirstInModel GetByWeiXinAppId(Guid weiXinAppId)
        {
            WeiXinFirstIn entity = _WeiXinFirstInLogic.GetByWeiXinAppId(weiXinAppId);
            return ConvertToModel(entity);
        }

        //编辑首次关注消息
        internal void EditFirstInMsg(WeiXinFirstInModel model, out DoHandle doHandle)
        {
            model.ContenInfo = model.ContenInfo == null ? "" : model.ContenInfo;
            if (model.Type == KeyWordType.Text)
            {
                model.PicMsgOrTirggerInfoId = null;
            }
            else
            {
                model.ContenInfo = "";
            }
            Update(model, out doHandle);
        }


    }

    #endregion
}