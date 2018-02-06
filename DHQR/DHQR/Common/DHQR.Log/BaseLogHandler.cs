#define  debug
using Common.DAL.Entities;
using log4net;
using log4net.Config;

namespace DHQR.Log
{
    

    /// <summary>
    /// 日志处理类的基类
    /// </summary>
    public abstract class BaseLogHandler<TLog> where TLog : IEntityKey, new() // : IBaseLog, new()
    {
        private static bool hasLoad = false;
        static object obj = new object();
        /// <summary>
        /// Loads the config.
        /// </summary>
        static void LoadConfig()
        {
            if (!hasLoad)
            {
                lock (obj)
                {
                    if (!hasLoad)
                    {
                        XmlConfigurator.Configure();
                        hasLoad = true;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the logger config.
        /// </summary>
        /// <value>The logger config.</value>
        private string LoggerConfig { set; get; }

        /// <summary>
        /// 日志对象
        /// </summary>
        public TLog Log { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseLogHandler&lt;TLog&gt;"/> class.
        /// </summary>
        /// <param name="loggerConfig">The logger config.</param>
        protected BaseLogHandler(string loggerConfig)
        {
            //XmlConfigurator.Configure();
            LoadConfig();
            this.LoggerConfig = loggerConfig;
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        public virtual void WriteLog()
        {
            //todo 通过调用log4net写入日志
            if (string.IsNullOrEmpty(this.LoggerConfig))
            {
                return;
            }
            ILog log = LogManager.GetLogger(this.LoggerConfig);
#if debug
            log4net.Appender.IAppender[] appenders = log.Logger.Repository.GetAppenders();
#endif

            if (log.IsInfoEnabled)
            {
                log.Info(this.Log);
            }
        }



    }
}
