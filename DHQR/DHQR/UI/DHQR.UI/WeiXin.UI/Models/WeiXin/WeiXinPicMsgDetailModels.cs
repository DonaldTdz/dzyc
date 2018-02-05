using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.BLL.Implement;
using Common.UI.Model;
using DHQR.BusinessLogic.Implement;
using DHQR.DataAccess.Entities;

namespace DHQR.UI.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class WeiXinPicMsgDetailModel
    {

        #region Model
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id
        {
            set;
            get;
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            set;
            get;
        }
        /// <summary>
        /// 图文消息表头ID
        /// </summary>
        public Guid PicMsgMatserId
        {
            set;
            get;
        }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string PicUrl
        {
            set;
            get;
        }

        /// <summary>
        /// 文章地址
        /// </summary>
        public string Url
        {
            set;
            get;
        }

        /// <summary>
        /// 简介
        /// </summary>
        public string Description
        {
            set;
            get;
        }

        /// <summary>
        /// 资源ID
        /// </summary>
        public Guid WeiXinSourceId
        {
            set;
            get;
        }

        #endregion Model
    }

    #region Services

    public class WeiXinPicMsgDetailModelService : BaseModelService<WeiXinPicMsgDetail, WeiXinPicMsgDetailModel>
    {
        private readonly WeiXinPicMsgDetailLogic _weiXinPicMsgDetailLogic;
        public WeiXinPicMsgDetailModelService()
        {
            _weiXinPicMsgDetailLogic = new WeiXinPicMsgDetailLogic();
        }

        protected override BaseLogic<WeiXinPicMsgDetail> BaseLogic
        {
            get { return _weiXinPicMsgDetailLogic; }
        }

        /// <summary>
        /// 根据抬头获取明细信息
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public IList<WeiXinPicMsgDetailModel> GetDetails(Guid masterId)
        {
            var result = _weiXinPicMsgDetailLogic.GetDetails(masterId).Select(f => ConvertToModel(f)).ToList();
            return result;
        }

    }
    #endregion
}