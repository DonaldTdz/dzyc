using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using System.Data.Objects;
using Common.Base;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 零售户基础信息数据访问层
    /// </summary>
    public class RetailerRepository : ProRep<Retailer>
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public RetailerRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<Retailer> EntityCurrentSet
        {
            get { return ActiveContext.Retailers; }
        }


        #region 零售户收货密码修改

       
        /// <summary>
        /// 零售户收货密码修改
        /// </summary>
        /// <param name="param"></param>
        /// <param name="?"></param>
        public void ChangeDeliveryPswParam(ChangeDeliveryPswParam param,out  DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "修改密码失败！" };
            var retailer = ActiveContext.Retailers.SingleOrDefault(f => f.CUST_ID == param.CUST_ID.Trim());
            string originalPsw = PassWordMd.HashPassword(param.OriginalPsw);
            string currentPsw = PassWordMd.HashPassword(param.Psw);
            if (retailer == null)
            {
                dohandle.OperateMsg = "指定零售户不存在,请检查后再修改！";
                return;
            }
            else
            {
                if (retailer.PSW == originalPsw)
                {
                    retailer.PSW = currentPsw;
                    ActiveContext.SaveChanges();
                    dohandle.IsSuccessful = true;
                    dohandle.OperateMsg = "密码修改成功!";
                }
                else
                {
                    dohandle.OperateMsg = "原始密码不正确!";
                }
            }
        }



        /// <summary>
        /// 同步零售户
        /// </summary>
        /// <param name="retailers"></param>
        /// <param name="dohandle"></param>
        public void SysnRetailers(List<Retailer> retailers,out DoHandle dohandle)
        {
            dohandle=new DoHandle{IsSuccessful=false,OperateMsg="操作失败"};
            foreach (var item in retailers)
            {
              //  var hasRetailer = ActiveContext.Retailers.Any(f => f.CUST_ID == item.CUST_ID);
                //var rt = ActiveContext.Retailers.SingleOrDefault(f => f.LICENSE_CODE == item.LICENSE_CODE);
                var rt = ActiveContext.Retailers.SingleOrDefault(f => f.CUST_ID == item.CUST_ID);
                if (rt==null)
                {
                    item.PSW = "c33367701511b4f6020ec61ded352059";//初始密码
                    item.RecieveType = "1";
                    item.RecieveTypeName = "自主";
                    item.OR_LICENSE_CODE = item.LICENSE_CODE;
                    ActiveContext.Retailers.AddObject(item);
                }
                else
                {
                    rt.STATUS = item.STATUS;
                    rt.CARD_ID = item.CARD_ID;
                    rt.CARD_CODE = item.CARD_CODE;
                    rt.ORDER_TEL = item.ORDER_TEL;
                    rt.BUSI_ADDR = item.BUSI_ADDR;
                    //rt.CUST_ID = item.CUST_ID;
                    rt.LICENSE_CODE = item.LICENSE_CODE;
                    rt.CUST_NAME = item.CUST_NAME;
                    rt.RUT_ID = item.RUT_ID;
                }
            }
            ActiveContext.SaveChanges();
            dohandle.IsSuccessful = true;
            dohandle.OperateMsg = "操作成功";
        }

        #endregion


        /// <summary>
        /// 查询零售户信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public PagedResults<Retailer> GetRetailerPageData(RetailerQueryParam queryParam)
        {
            var result = ActiveContext.Retailers
                .Where(f => (string.IsNullOrEmpty(queryParam.KeyWord) 
                    ||  f.CUST_ID.Contains(queryParam.KeyWord)
                    || f.CUST_NAME.Contains(queryParam.KeyWord)
                    || f.LICENSE_CODE.Contains(queryParam.KeyWord)
                    )
                    && (string.IsNullOrEmpty(queryParam.STATUS) || f.STATUS==queryParam.STATUS)
                    && (string.IsNullOrEmpty(queryParam.RecieveType) || f.RecieveType == queryParam.RecieveType)
                    && ((queryParam.IsCollect && f.LONGITUDE != null) || !queryParam.IsCollect)
                ).ToPagedResults<Retailer>(queryParam);          
            return result;
        }


        /// <summary>
        /// 根据客户代码集合获取零售户集合
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        public IList<Retailer> GetRetailersByCustIds(IList<string> custIds)
        {
            var result = ActiveContext.Retailers.Where(f => custIds.Contains(f.CUST_ID)).ToList();
            return result;
        }


        /// <summary>
        /// 根据NFC卡获取客户信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public NfcCustomer GetNfcCustomer(GetNfcCustomerParam param)
        {
            var retailer = ActiveContext.Retailers.SingleOrDefault(f => f.CARD_ID == param.CARD_ID && f.STATUS!="03" && f.STATUS!="04");
            if (retailer == null)
            {
                NfcCustomer rt = new NfcCustomer { IsSuccessful = false, OperateMsg = "该卡未绑定客户！" };
                return rt;
            }
            else
            {
                NfcCustomer result = new NfcCustomer 
                {
                    Id=retailer.Id,
                    CUST_ID=retailer.CUST_ID,
                    CUST_NAME=retailer.CUST_NAME,
                    LICENSE_CODE=retailer.LICENSE_CODE,
                    CARD_ID=retailer.CARD_ID,
                    CARD_CODE=retailer.CARD_CODE,
                    ORDER_TEL=retailer.ORDER_TEL,
                    BUSI_ADDR=retailer.BUSI_ADDR,
                    IsSuccessful=true,
                    OperateMsg="获取成功"
                };
                return result;
            }
        }
    }
}
