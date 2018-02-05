using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.UI.Model;
using DHQR.BusinessLogic.Implement;
using Common.BLL.Implement;
using Common.Base;
using DHQR.DataAccess.Entities;
using System.Web.Mvc;

namespace DHQR.UI.Models
{

    /// <summary>
    /// 订单明细模型
    /// </summary>
    public class LdmDisItemModel
    {
        #region Model
        private Guid _id;
        private string _co_num;
        private string _item_id;
        private string _item_name;
        private decimal _price;
        private decimal _qty;
        private decimal _amt;
        private string _is_abnormal;
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string CO_NUM
        {
            set { _co_num = value; }
            get { return _co_num; }
        }
        /// <summary>
        /// 商品编码
        /// </summary>
        public string ITEM_ID
        {
            set { _item_id = value; }
            get { return _item_id; }
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ITEM_NAME
        {
            set { _item_name = value; }
            get { return _item_name; }
        }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal PRICE
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal QTY
        {
            set { _qty = value; }
            get { return _qty; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal AMT
        {
            set { _amt = value; }
            get { return _amt; }
        }
        /// <summary>
        /// 是否异性烟
        /// </summary>
        public string IS_ABNORMAL
        {
            set { _is_abnormal = value; }
            get { return _is_abnormal; }
        }
        #endregion Model

    }
    /// <summary>
    /// 订单明细服务
    /// </summary>
    public class LdmDisItemModelService : BaseModelService<LdmDisItem, LdmDisItemModel>
    {
        private readonly LdmDisItemLogic BusinessLogic;

        public LdmDisItemModelService()
        {
            BusinessLogic = new LdmDisItemLogic();
        }

        protected override BaseLogic<LdmDisItem> BaseLogic
        {
            get { return BusinessLogic; }
        }



        #region 微信获取订单明细

        /// <summary>
        /// 根据订单号获取订单明细
        /// </summary>
        /// <param name="CO_NUM"></param>
        /// <returns></returns>
        public IList<LdmDisItemModel> GetByCoNum(string CO_NUM)
        {
            var data = BusinessLogic.GetByCoNum(CO_NUM).Select(f=>ConvertToModel(f)).ToList();
            var result = data.Where(f => f.QTY != 0).ToList();
            return result;
            
        }

        #endregion


    }

}