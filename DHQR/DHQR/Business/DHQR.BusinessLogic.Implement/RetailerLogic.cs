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

    public class RetailerLogic : BaseLogic<Retailer>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private RetailerRepository RetailerRep { get { return new RetailerRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<Retailer> Repository
        {
            get { return RetailerRep; }
        }


        #region 零售户收货密码修改


        /// <summary>
        /// 零售户收货密码修改
        /// </summary>
        /// <param name="param"></param>
        /// <param name="?"></param>
        public void ChangeDeliveryPswParam(ChangeDeliveryPswParam param, out  DoHandle dohandle)
        {
            RetailerRep.ChangeDeliveryPswParam(param, out dohandle);
        }


        #endregion


        #region

        /// <summary>
        /// 转换客户信息
        /// </summary>
        /// <param name="COM_ID"></param>
        /// <param name="dohandle"></param>
        public void SysCustomer(string COM_ID, out DoHandle dohandle)
        {
            LangchaoLogic lcLogic=new LangchaoLogic();
            var retailers = lcLogic.GetCustomer(COM_ID);
            RetailerRep.SysnRetailers(retailers, out dohandle);
        }

        #endregion



        /// <summary>
        /// 查询零售户信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public PagedResults<Retailer> GetRetailerPageData(RetailerQueryParam queryParam)
        {
            return RetailerRep.GetRetailerPageData(queryParam);
        }

        /// <summary>
        /// 根据客户代码集合获取零售户集合
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        public IList<Retailer> GetRetailersByCustIds(IList<string> custIds)
        {
            var result = RetailerRep.GetRetailersByCustIds(custIds);
            return result;
        }

        /// <summary>
        /// 根据NFC卡获取客户信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public NfcCustomer GetNfcCustomer(GetNfcCustomerParam param)
        {
            return RetailerRep.GetNfcCustomer(param);
        }

    }
}
 