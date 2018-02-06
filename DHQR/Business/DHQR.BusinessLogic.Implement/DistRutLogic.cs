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
    /// 配送线路逻辑层
    /// </summary>
    public class DistRutLogic : BaseLogic<DistRut>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private DistRutRepository repository { get { return new DistRutRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<DistRut> Repository
        {
            get { return repository; }
        }


        /// <summary>
        /// 同步线路信息
        /// </summary>
        /// <param name="dlvmans"></param>
        /// <param name="dohandle"></param>
        public void SynDistRuts(out DoHandle dohandle)
        {
            LangchaoLogic lcLogic = new LangchaoLogic();
            var ruts = lcLogic.GetRutList();
            repository.SynDistRuts(ruts, out dohandle);
        }
        /// <summary>
        /// 日期：2018-2-6
        /// 作者：Donald
        /// 说明：同步达州线路信息
        /// </summary>
        /// <param name="dlvmans"></param>
        /// <param name="dohandle"></param>
        public void SynDZDistRuts(out DoHandle dohandle)
        {
            DZLangchaoLogic lcLogic = new DZLangchaoLogic();
            var ruts = lcLogic.GetRutList();
            repository.SynDistRuts(ruts, out dohandle);
        }

    }
}
