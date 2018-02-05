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
    /// 退货单检查流水号业务层
    /// </summary>
    public class ReOrderKeyLogic : BaseLogic<ReOrderKey>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private ReOrderKeyRepository repository { get { return new ReOrderKeyRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<ReOrderKey> Repository
        {
            get { return repository; }
        }


        /// <summary>
        /// 获取流水号
        /// </summary>
        /// <returns></returns>
        public string GetLogkey()
        {
           return  repository.GetLogkey();
        }

    }
}
