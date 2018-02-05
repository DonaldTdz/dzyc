using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using System.Data.Objects;
using Common.Base;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// APP版本信息数据访问层
    /// </summary>
    public class AppVersionRepository : ProRep<AppVersion>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public AppVersionRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<AppVersion> EntityCurrentSet
        {
            get { return ActiveContext.AppVersions; }
        }


        #region 检查版本

        /// <summary>
        /// 检查APP版本
        /// </summary>
        /// <param name="versionParam"></param>
        /// <returns></returns>
        public AppVersion CheckAppVersion(AppVersion versionParam)
        {
            AppVersion result = new AppVersion();
            var appVersion = ActiveContext.AppVersions.SingleOrDefault(f => f.ApkPacket == versionParam.ApkPacket 
                                                                && f.IsValid == true
                                                                );
            if (appVersion == null)
            {
                result.OperateMsg = "不存在此APK文件,请在后台进行上传";
                result.NeedUpdate = false;
                return result;
            }
            else
            {
                if (appVersion.VersionCode == versionParam.VersionCode)
                {
                    result.OperateMsg = "当前为最新版本";
                    result.NeedUpdate = false;
                    return result;
                }
                else
                {
                    appVersion.OperateMsg = "版本需要更新";
                    appVersion.NeedUpdate = true;
                    return appVersion;
                }
            }
        }

        #endregion

    }
}
