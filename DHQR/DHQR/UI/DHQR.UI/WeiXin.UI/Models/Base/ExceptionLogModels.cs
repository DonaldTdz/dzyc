using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.UI.Model;
using DHQR.DataAccess.Entities;
using DHQR.BusinessLogic.Implement;
using Common.BLL.Implement;
using Common.Base;
using Common.UI.Util;
using DHQR.BasicLib;
using DHQR.UI.DHQRCommon;

namespace DHQR.UI.Models
{
    #region 模型
    public class ExceptionLogModel
    {
        #region 基元属性


        public System.Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Message { get; set; }
        public string InnerException { get; set; }
        public string Ip { get; set; }
        public string UserName { get; set; }
        #endregion

    }
    #endregion



    #region Services
    public class ExceptionLogModelService : BaseModelService<ExceptionLog, ExceptionLogModel>
    {
        private readonly ExceptionLogLogic _ExceptionLogLogic;
        public ExceptionLogModelService()
        {
            _ExceptionLogLogic = new ExceptionLogLogic();
        }

        protected override BaseLogic<ExceptionLog> BaseLogic
        {
            get { return _ExceptionLogLogic; }
        }

        /// <summary>
        /// 保存错误日志
        /// </summary>
        /// <param name="model"></param>
        /// <param name="doHandle"></param>
        public override void Add(ExceptionLogModel model, out DoHandle doHandle)
        {
            try
            {
                model.CreateTime = DateTime.Now;
                base.Add(model, out doHandle);
            }
            catch (Exception ex)
            {

                doHandle = new DoHandle() { IsSuccessful = false, OperateMsg = ex.Message };
            }

        }

        /// <summary>
        /// A
        /// </summary>
        /// <param name="ex"></param>
        public static void DoAddLog(Exception ex)
        {
            string Ip = "0.0.0.0";
            string UserName = "系统调用";

            try
            {
                Ip = HostHelper.GetIp();
                UserName = LoginUserInfo.GetUserName();
            }
            catch (Exception)
            {
            }

            try
            {
                DoHandle doHandle;
                new ExceptionLogModelService().Add(new ExceptionLogModel
                {
                    Ip = Ip,
                    Message = ex.Message,
                    InnerException = ex.InnerException == null ? "" : ex.InnerException.Message,
                    UserName = UserName
                }, out doHandle);
            }
            catch (Exception)
            {
            }
        }


        /// <summary>
        /// 手机站点写日志
        /// </summary>
        /// <param name="ex"></param>
        public static void MobileAddLog(Exception ex)
        {
            string Ip = "0.0.0.0";
            string UserName = "系统调用";

            try
            {
                Ip = HostHelper.GetIp();
              //  UserName = MobileWebHelper.GetWxUserName();
            }
            catch (Exception)
            {
            }

            try
            {
                DoHandle doHandle;
                new ExceptionLogModelService().Add(new ExceptionLogModel
                {
                    Ip = Ip,
                    Message = ex.Message,
                    InnerException = ex.InnerException == null ? "" : ex.InnerException.Message,
                    UserName = UserName
                }, out doHandle);
            }
            catch (Exception)
            {
            }
        }
     
    }

    #endregion
}