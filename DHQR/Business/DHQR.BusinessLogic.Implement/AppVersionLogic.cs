using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.BLL.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;
using Common.DAL.Implement;
using Common.Base;

namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// APP版本信息逻辑层
    /// </summary>
    public class AppVersionLogic : BaseLogic<AppVersion>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private AppVersionRepository repository { get { return new AppVersionRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<AppVersion> Repository
        {
            get { return repository; }
        }



         /// <summary>
        /// 检查APP版本
        /// </summary>
        /// <param name="versionParam"></param>
        /// <returns></returns>
        public AppVersion CheckAppVersion(AppVersion versionParam)
        {
            return repository.CheckAppVersion(versionParam);
        }

    }
}
