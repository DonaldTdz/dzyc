using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Langchao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DHQR.BusinessLogic.Implement
{
    public class DZLangchaoLogic
    {
        public DZLangchaoDB2Repository repository;

        public DZLangchaoLogic()
        {
            repository = new DZLangchaoDB2Repository();
        }

        #region 访问达州数据库测试

        public List<LdmDist> GetDists(DownloadDistParam param)
        {
            var idists = repository.GetDists(param);
            var ldmDists = idists.Select(f => ConvertFromLC.ConvertDist(f)).ToList();
            return ldmDists;
        }

        #endregion
    }
}
