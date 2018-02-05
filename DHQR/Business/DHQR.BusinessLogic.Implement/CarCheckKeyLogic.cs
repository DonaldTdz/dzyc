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
    /// 车辆检查流水号业务层
    /// </summary>
    public class CarCheckKeyLogic : BaseLogic<CarCheckKey>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private CarCheckKeyRepository repository { get { return new CarCheckKeyRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<CarCheckKey> Repository
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


        /// <summary>
        /// 获取指定量的流水号
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<string> GetLogKeys(int count)
        {

            return repository.GetLogKeys(count);
        }

    }
}
