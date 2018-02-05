using DHQR.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DHQR.DataAccess.Langchao
{
    public class DZLangchaoDB2Repository
    {
        #region 访问达州数据库测试

        public List<DZ_I_DIST> GetDists(DownloadDistParam param)
        {
            IDictionary<string, string> dic1 = new Dictionary<string, string>();
            dic1.Add("DLVMAN_ID", param.DLVMAN_ID);
            var ldmDists = new DB2Helper<DZ_I_DIST>().QueryData(dic1);
            return ldmDists;
        }

        #endregion
    }
}
