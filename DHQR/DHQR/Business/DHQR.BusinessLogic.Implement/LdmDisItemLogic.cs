using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.BLL.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;
using Common.DAL.Implement;

namespace DHQR.BusinessLogic.Implement
{

    /// <summary>
    /// 配送任务单行订单商品信息逻辑层
    /// </summary>
    public class LdmDisItemLogic : BaseLogic<LdmDisItem>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private LdmDisItemRepository LdmDisItemRep { get { return new LdmDisItemRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<LdmDisItem> Repository
        {
            get { return LdmDisItemRep; }
        }



        #region 微信获取订单明细

        /// <summary>
        /// 根据订单号获取订单明细
        /// </summary>
        /// <param name="CO_NUM"></param>
        /// <returns></returns>
        public IList<LdmDisItem> GetByCoNum(string CO_NUM)
        {
            LangchaoLogic lcLogic = new LangchaoLogic();
            var result = lcLogic.GetByCoNum(CO_NUM);
            return result;
        }

        #endregion

    }
}
