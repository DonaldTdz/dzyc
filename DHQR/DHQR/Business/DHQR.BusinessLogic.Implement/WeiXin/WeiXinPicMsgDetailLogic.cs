using Common.BLL.Implement;
using Common.DAL.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;


namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// 微信图文消息明细逻辑层
    /// </summary>
    public class WeiXinPicMsgDetailLogic : BaseLogic<WeiXinPicMsgDetail>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private WeiXinPicMsgDetailRep WeiXinPicMsgDetailRep { get { return new WeiXinPicMsgDetailRep(_baseDataEntities); } }
        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<WeiXinPicMsgDetail> Repository
        {
            get { return WeiXinPicMsgDetailRep; }
        }


        /// <summary>
        /// 根据抬头获取明细信息
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public IList<WeiXinPicMsgDetail> GetDetails(Guid masterId)
        {
            var result = WeiXinPicMsgDetailRep.GetDetails(masterId);
            return result;
        }

        /// <summary>
        /// 根据抬头集合获取明细信息
        /// </summary>
        /// <param name="masterIds"></param>
        /// <returns></returns>
        public IEnumerable<WeiXinPicMsgDetail> GetDetailbyMsaterIds(IList<Guid> masterIds)
        {
            var result = WeiXinPicMsgDetailRep.GetDetailbyMsaterIds(masterIds);
            return result;
        }

    }

}
