using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Base;
using Common.BLL.Implement;
using Common.UI.Model;
using System.Web.Mvc;
using DHQR.BusinessLogic.Implement;
using DHQR.DataAccess.Entities;

namespace DHQR.UI.Models
{
    #region 模型
    public class WeiXinKeyWordModel
    {

        #region 基元属性


        public Guid Id { get; set; }
        public int Index { get; set; }
        public string KeyWord { get; set; }
        public int Type { get; set; }
        public string ContenInfo { get; set; }
        public Nullable<Guid> PicMsgOrTirggerInfoId { get; set; }
        public int PatternMethod { get; set; }
        public Guid WeiXinAppId { get; set; }

        public string TypeStr
        {
            get
            {
                if (Type == KeyWordType.Image)
                {
                    return "图文消息";
                }
                else
                {
                    return "文字消息";
                }
            }
        }

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
        #endregion

    }
    #endregion

    #region Services
    public class WeiXinKeyWordModelService : BaseModelService<WeiXinKeyWord, WeiXinKeyWordModel>
    {
        private readonly WeiXinKeyWordLogic BusinessLogic;
        private int index = 1;
        public WeiXinKeyWordModelService()
        {
            BusinessLogic = new WeiXinKeyWordLogic();
        }

        protected override BaseLogic<WeiXinKeyWord> BaseLogic
        {
            get { return BusinessLogic; }
        }

        //public override PagedResults<WeiXinKeyWordModel> GetPageData(QueryParam queryParam)
        //{
        //    PagedResults<WeiXinKeyWordModel> result = base.GetPageData(queryParam);
        //    return result;
        //}

        public override WeiXinKeyWordModel ConvertToModel(WeiXinKeyWord entity)
        {
            if (entity == null) return null;
            WeiXinKeyWordModel result = base.ConvertToModel(entity);
            if (entity.PicMsgOrTirggerInfoId != null)
            {
                WeiXinTriggerInfoLogic triggerInfoLogic = new WeiXinTriggerInfoLogic();
                WeiXinTriggerInfo triggerInfo = triggerInfoLogic.GetByKey(entity.PicMsgOrTirggerInfoId.Value);
                if (triggerInfo != null)
                {
                    result.ContenInfo = triggerInfo.MsgTitle;
                }
                else
                {
                    WeiXinPicMsgMatserLogic picMsgMatserLogic = new WeiXinPicMsgMatserLogic();
                    WeiXinPicMsgMatser picMsgMatser = picMsgMatserLogic.GetByKey(entity.PicMsgOrTirggerInfoId.Value);
                    result.ContenInfo = picMsgMatser.Title;
                }
            }
            result.Index = index;
            index++;
            return result;
            
        }


        internal void EditKeyWord(WeiXinKeyWordModel model, out DoHandle doHandle)
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


        /// <summary>
        /// 根据内容和匹配方式获取关键字
        /// 如果多条返回第一条
        /// </summary>
        /// <param name="weiXinAppId">公众号ID</param>
        /// <param name="content">内容</param>
        /// <param name="patternMethod">匹配方式</param>
        /// <returns></returns>
        internal WeiXinKeyWordModel GetByContent(Guid weiXinAppId, string content)
        {
            var result = ConvertToModel(BusinessLogic.GetByContent(weiXinAppId, content));
            return result;
        }

    }

    #endregion

    

   
}