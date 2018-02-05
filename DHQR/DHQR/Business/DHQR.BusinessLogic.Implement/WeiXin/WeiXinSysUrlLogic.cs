using Common.BLL.Implement;
using Common.DAL.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement.WeiXin;

namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// 
    /// </summary>
    public class WeiXinSysUrlLogic : BaseLogic<WeiXinSysUrl>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private WeiXinSysUrlRep _repository { get { return new WeiXinSysUrlRep(_baseDataEntities); } }


        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<WeiXinSysUrl> Repository
        {
            get { return _repository; }
        }

        /// <summary>
        /// 根据模块ID获取系统URL
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public IList<WeiXinSysUrl> GetByTypeId(Guid typeId)
        {
            var result = _repository.GetByTypeId(typeId);
            return result;
        }

    }
}
