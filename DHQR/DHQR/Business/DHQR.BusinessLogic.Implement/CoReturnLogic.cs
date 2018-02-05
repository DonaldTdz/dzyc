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
    /// 退货单抬头逻辑层
    /// </summary>
    public class CoReturnLogic : BaseLogic<CoReturn>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private CoReturnRepository CoReturnRep { get { return new CoReturnRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<CoReturn> Repository
        {
            get { return CoReturnRep; }
        }

        #region 退货


        /// <summary>
        /// 整单退货
        /// </summary>
        /// <param name="param"></param>
        /// <param name="dohandle"></param>
        public void ReturnAllOrder(DistCust param, out DoHandle dohandle)
        {

            string RETURN_CO_NUM = new ReOrderKeyLogic().GetLogkey();
            var lcParam = ConvertToLC.ConvertDistCust(param);
            LangchaoLogic lcLogic = new LangchaoLogic();
            lcLogic.ReturnAllOrder(lcParam,  RETURN_CO_NUM,out dohandle);
            if (dohandle.IsSuccessful)
            {
                CoReturnRep.ReturnAllOrder(param, RETURN_CO_NUM,out dohandle);
            }
        }

        /// <summary>
        /// 部分退货操作
        /// </summary>
        /// <param name="param"></param>
        /// <param name="dohandle"></param>
        public void ReturnPatialOrder(ReturnPatialOrderParam param, out DoHandle dohandle)
        {
            param.Order.ORG_CO_NUM = param.Cust.CO_NUM; 
            var lcCust = ConvertToLC.ConvertDistCust(param.Cust);
            var lcCoReturn = ConvertToLC.ConvertReturnOrder(param.Order);
            var lcCoLines = param.OrderDetails.Select(f => ConvertToLC.ConvertReturnLine(f)).ToList();
            if (string.IsNullOrEmpty(lcCoReturn.RETURN_CO_NUM))
            {
                string RETURN_CO_NUM = new ReOrderKeyLogic().GetLogkey();
                lcCoReturn.RETURN_CO_NUM = RETURN_CO_NUM;
                param.Order.RETURN_CO_NUM = RETURN_CO_NUM;
            }
            LangchaoLogic lcLogic = new LangchaoLogic();
            lcLogic.ReturnPartialOrder(lcCust,lcCoReturn,lcCoLines, out dohandle);
            if (dohandle.IsSuccessful)
            {
                CoReturnRep.ReturnPatialOrder(param, out dohandle);
            }
        }

        #endregion
    }
}
