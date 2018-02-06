using System;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Web;
using DHQR.DataAccess.Entities;
using log4net;
using log4net.Appender;

namespace DHQR.Log
{
    /// <summary>
    /// 异常日志处理器
    /// </summary>
    public class ExceptionLogHandler : BaseLogHandler<ServiceExceptionLog>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="exception"></param>
        public ExceptionLogHandler(Exception exception) : this(exception, Guid.NewGuid())
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="id"></param>
        public ExceptionLogHandler(Exception exception, Guid id)
            : this(exception, null, id)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="currentUserLogonName"></param>
        /// <param name="id"></param>
        public ExceptionLogHandler(Exception exception,string currentUserLogonName, Guid id)
            : base("")
        {
            Log = new ServiceExceptionLog
                  {
                      OperateTime = DateTime.Now,
                      Id = id,
                      Message = exception.Message,
                      StackTrace = exception.StackTrace,
                      ExceptionType = exception.GetType().FullName,
                  };
            //logonName
            if (currentUserLogonName == null)
            {
                currentUserLogonName = "";//(PrincipalUser.Current ?? PrincipalUser.AnonymousUser).LogonName;                
            }
            Log.LogonName = currentUserLogonName;
            //host
            string hostName = Dns.GetHostName();
#pragma warning disable 612,618
            var ip = Dns.GetHostByName(hostName).AddressList[0];
#pragma warning restore 612,618
            Log.Host = string.Format("{0}@{1}", hostName, ip);
            //runtime
            /*
            if (PrincipalUser.IsWcfApp)
            {
                Log.Runtime = "WCF";
                Log.RequestUrl = OperationContext.Current.Channel.LocalAddress.Uri.ToString();
            }
            else if (PrincipalUser.IsWebApp)
            {
                Log.Runtime = "WEB";
                Log.RequestUrl = HttpContext.Current.Request.Url.ToString();
                var inputStream = HttpContext.Current.Request.InputStream;
                var streamReader = new StreamReader(inputStream);
                var requestData = HttpUtility.UrlDecode(streamReader.ReadToEnd());
                Log.RequestData = requestData;
            }
             
            else
            {
                Log.Runtime = "Other";
            }
             */
            Log.RequestUrl = OperationContext.Current.Channel.LocalAddress.Uri.ToString();


            //InnerException
            if (exception.InnerException != null)
            {
                Log.InnerException = exception.InnerException.ToString();
            }

        }

        /// <summary>
        /// Writes the log.
        /// </summary>
        public override void WriteLog()
        {
            ILog log = LogManager.GetLogger("logException");
            IAppender[] appenders = log.Logger.Repository.GetAppenders();
            IAppender emailAppender = appenders.FirstOrDefault(f => f.Name == "ExceptionEmailAppender");
            if (emailAppender != null)
            {
                //var email = (SmtpAppender) emailAppender;
                ////给邮件附加器添加要发送的邮件列表 
                //email.To = GetSendToAddress();
            }
            if (log.IsErrorEnabled)
            {
                log.Error(Log);
            }
        }


       
    }
}