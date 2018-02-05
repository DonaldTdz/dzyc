using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.UI.Model;
using DHQR.BusinessLogic.Implement;
using Common.BLL.Implement;
using Common.Base;
using DHQR.DataAccess.Entities;
using System.Web.Mvc;
using System.Runtime.Serialization;

namespace DHQR.UI.Models
{
    /// <summary>
    /// APP版本信息模型
    /// </summary>
    public class AppVersionModel
    {
        #region Model
        private Guid _id;
        private string _apkpacket;
        private string _apkname;
        private int _versioncode;
        private string _versionname;
        private string _url;
        private bool _isvalid;
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// APK包名
        /// </summary>
        public string ApkPacket
        {
            set { _apkpacket = value; }
            get { return _apkpacket; }
        }
        /// <summary>
        /// APK名称
        /// </summary>
        public string ApkName
        {
            set { _apkname = value; }
            get { return _apkname; }
        }
        /// <summary>
        /// 版本号
        /// </summary>
        public int VersionCode
        {
            set { _versioncode = value; }
            get { return _versioncode; }
        }
        /// <summary>
        /// 版本描述
        /// </summary>
        public string VersionName
        {
            set { _versionname = value; }
            get { return _versionname; }
        }
        /// <summary>
        /// APK地址
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid
        {
            set { _isvalid = value; }
            get { return _isvalid; }
        }
        #endregion Model
    }

    /// <summary>
    /// app版本信息服务模型
    /// </summary>
    public class AppVersionModelService : BaseModelService<AppVersion, AppVersionModel>
    {
         private readonly AppVersionLogic BusinessLogic;

         public AppVersionModelService()
        {
            BusinessLogic = new AppVersionLogic();
        }

         protected override BaseLogic<AppVersion> BaseLogic
        {
            get { return BusinessLogic; }
        }
    }
}