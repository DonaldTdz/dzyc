using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.UI.Model;
using DHQR.DataAccess.Entities;
using DHQR.BusinessLogic.Implement;
using Common.BLL.Implement;

namespace DHQR.UI.Models
{
    #region 模型

    /// <summary>
    /// 微信配置
    /// </summary>
    public class WeiXinAppModel
    {
        #region Model
        private Guid _id;
        private string _name;
        private string _weixinkey;
        private string _appid;
        private string _appsecret;
        private string _url;
        private string _token;
        private string _originalid;
        private DateTime _createtime;
        private Guid _userid;
        private int _weixintype;
        private string _picurl;
        private string _access_token;
        private int? _expires_in;
        private DateTime? _next_gettime;
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 微信Key
        /// </summary>
        public string WeiXinKey
        {
            set { _weixinkey = value; }
            get { return _weixinkey; }
        }
        /// <summary>
        /// AppId
        /// </summary>
        public string AppId
        {
            set { _appid = value; }
            get { return _appid; }
        }
        /// <summary>
        /// AppSecret
        /// </summary>
        public string AppSecret
        {
            set { _appsecret = value; }
            get { return _appsecret; }
        }
        /// <summary>
        /// 微信平台配置URL
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 微信平台配置Token
        /// </summary>
        public string Token
        {
            set { _token = value; }
            get { return _token; }
        }
        /// <summary>
        /// 微信平台原始ID
        /// </summary>
        public string OriginalId
        {
            set { _originalid = value; }
            get { return _originalid; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 微信类型
        /// </summary>
        public int WeiXinType
        {
            set { _weixintype = value; }
            get { return _weixintype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PicUrl
        {
            set { _picurl = value; }
            get { return _picurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string access_token
        {
            set { _access_token = value; }
            get { return _access_token; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? expires_in
        {
            set { _expires_in = value; }
            get { return _expires_in; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? next_gettime
        {
            set { _next_gettime = value; }
            get { return _next_gettime; }
        }
        #endregion Model

    }

    #endregion


    #region Services

 
    /// <summary>
    /// 微信配置服务
    /// </summary>
    public class WeiXinAppModelService : BaseModelService<WeiXinApp, WeiXinAppModel>
    {
        private readonly WeiXinAppLogic _WeiXinAppLogic;
         public WeiXinAppModelService()
        {
            _WeiXinAppLogic = new WeiXinAppLogic();
        }

         protected override BaseLogic<WeiXinApp> BaseLogic
        {
            get { return _WeiXinAppLogic; }
        }


        /// <summary>
        /// 根据用户ID获取公众号(1.0版本只返回一个账号)
        /// </summary>
        /// <returns></returns>
         internal WeiXinAppModel GetOneByUserId(Guid userId)
         {
             var result =ConvertToModel( _WeiXinAppLogic.GetOneByUserId(userId));
             return result;
         }

         /// <summary>
         /// 根据微信KEY值获取公众号
         /// </summary>
         /// <param name="key"></param>
         /// <returns></returns>
         public WeiXinAppModel GetByWeiXinKey(string key)
         {
             var result = ConvertToModel(_WeiXinAppLogic.GetByWeiXinKey(key));
             return result;
         }




    }
   
    #endregion
}