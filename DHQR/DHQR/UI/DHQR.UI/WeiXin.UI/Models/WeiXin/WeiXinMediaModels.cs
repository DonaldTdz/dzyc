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
    /// <summary>
    /// 微信群发媒体模型
    /// </summary>
    public class WeiXinMediaModel
    {
        #region Model
        private Guid _id;
        private string _media_id;
        private string _type;
        private string _created_at;
        private string _local_url;
        private string _fliename;
        private string _extention;
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 媒体文件/图文消息上传后获取的唯一标识
        /// </summary>
        public string media_id
        {
            set { _media_id = value; }
            get { return _media_id; }
        }
        /// <summary>
        /// 媒体文件类型
        /// </summary>
        public string type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string created_at
        {
            set { _created_at = value; }
            get { return _created_at; }
        }
        /// <summary>
        /// 本地服务器地址
        /// </summary>
        public string local_url
        {
            set { _local_url = value; }
            get { return _local_url; }
        }
        /// <summary>
        /// 文件名
        /// </summary>
        public string fliename
        {
            set { _fliename = value; }
            get { return _fliename; }
        }
        /// <summary>
        /// 扩展名
        /// </summary>
        public string extention
        {
            set { _extention = value; }
            get { return _extention; }
        }
        #endregion Model
    }

    #region Services

    /// <summary>
    /// 微信群发媒体服务模型
    /// </summary>
    public class WeiXinMediaModelService : BaseModelService<WeiXinMedia, WeiXinMediaModel>
    {
        private readonly WeiXinMediaLogic BusinessLogic;
        public WeiXinMediaModelService()
        {
            BusinessLogic = new WeiXinMediaLogic();
        }

        protected override BaseLogic<WeiXinMedia> BaseLogic
        {
            get { return BusinessLogic; }
        }
    }

    #endregion
}