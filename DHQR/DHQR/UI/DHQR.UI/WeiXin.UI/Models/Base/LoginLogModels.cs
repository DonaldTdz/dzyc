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
    public class LoginLogModel
    {
        #region 基元属性


        public System.Guid Id { get; set; }
        public string UserName { get; set; }
        public string LoginIp { get; set; }
        public System.DateTime CreateTime { get; set; }

        #endregion

    }
    #endregion



    #region Services
    public class LoginLogModelService : BaseModelService<LoginLog, LoginLogModel>
    {
        private readonly LoginLogLogic _LoginLogLogic;
        public LoginLogModelService()
        {
            _LoginLogLogic = new LoginLogLogic();
        }

        protected override BaseLogic<LoginLog> BaseLogic
        {
            get { return _LoginLogLogic; }
        }
    }

    #endregion
}