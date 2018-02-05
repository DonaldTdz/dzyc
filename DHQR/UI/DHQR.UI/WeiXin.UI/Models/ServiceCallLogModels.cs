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

namespace DHQR.UI.Models
{
    /// <summary>
    /// 服务器调用日志模型
    /// </summary>
    public class ServiceCallLogModel
    {
        #region Model
        private Guid _id;
        private string _servicename;
        private string _methodname;
        private string _requestparam;
        private DateTime _requesttime;
        private bool _issucessful;
        private string _username;
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName
        {
            set { _servicename = value; }
            get { return _servicename; }
        }
        /// <summary>
        /// 方法名称
        /// </summary>
        public string MethodName
        {
            set { _methodname = value; }
            get { return _methodname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RequestParam
        {
            set { _requestparam = value; }
            get { return _requestparam; }
        }
        /// <summary>
        /// 调用时间
        /// </summary>
        public DateTime RequestTime
        {
            set { _requesttime = value; }
            get { return _requesttime; }
        }
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSucessful
        {
            set { _issucessful = value; }
            get { return _issucessful; }
        }
        /// <summary>
        /// 调用用户名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        #endregion Model
    }


    /// <summary>
    /// 服务器调用日志服务模型
    /// </summary>
    public class ServiceCallLogModelService : BaseModelService<ServiceCallLog, ServiceCallLogModel>
    {
        private readonly ServiceCallLogLogic BusinessLogic;

         public ServiceCallLogModelService()
        {
            BusinessLogic = new ServiceCallLogLogic();
        }

         protected override BaseLogic<ServiceCallLog> BaseLogic
        {
            get { return BusinessLogic; }
        }

    }

}