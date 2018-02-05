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
    /// 配送任务单行订单信息逻辑层
    /// </summary>
    public class LdmDistLineLogic : BaseLogic<LdmDistLine>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private LdmDistLineRepository LdmDistLineRep { get { return new LdmDistLineRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<LdmDistLine> Repository
        {
            get { return LdmDistLineRep; }
        }


        #region 获取线路明细信息

         /// <summary>
        /// 根据条件获取线路明细信息
         /// </summary>
         /// <param name="queryParam"></param>
         /// <returns></returns>
        public IList<LdmDistLine> QueryDeliveryLine(LdmDistLineQueryParam queryParam)
        {
            return LdmDistLineRep.QueryDeliveryLine(queryParam);
        }


        #endregion


        #region 同步订单信息

        /// <summary>
        /// 同步订单信息
        /// </summary>
        /// <param name="synParam"></param>
        /// <returns></returns>
        public IList<OrderInfo> SynOrders(SynOrderParam synParam)
        {
            var result = LdmDistLineRep.SynOrders(synParam);
            return result;

        }

        #endregion

        #region 微信订单查询

        /// <summary>
        /// 微信订单查询
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public PagedResults<LdmDistLine> QueryOrders(LdmDistLineQueryParam queryParam)
        {

            var result = LdmDistLineRep.QueryOrders(queryParam);
            return result;
        }

        #endregion

        #region 订单查询

        /// <summary>
        /// 查询订单信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public PagedResults<LdmDistLine> QueryLdmdistLine(LdmDistLineQueryParam queryParam)
        {
            return LdmDistLineRep.QueryLdmdistLine(queryParam);
        }


        /// <summary>
        /// 获取订单和配送单信息
        /// </summary>
        /// <param name="CO_NUM"></param>
        /// <returns></returns>
        public LdmDistLine GetByCoNum(string CO_NUM, string DIST_NUM, out LdmDist dist)
        {
            return LdmDistLineRep.GetByCoNum(CO_NUM,DIST_NUM,out dist);
        }

        #endregion
    }
}
