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
    /// 回程登记流水号逻辑层
    /// </summary>
    public class CarRunKeyLogic : BaseLogic<CarRunKey>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private CarRunKeyRepository repository { get { return new CarRunKeyRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<CarRunKey> Repository
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
