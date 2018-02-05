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
using System.Runtime.Serialization;

namespace DHQR.UI.Models
{
    /// <summary>
    /// 出入门卡模型
    /// </summary>
    public class EntranceCardModel
    {
        #region Model
        private Guid _id;
        private string _cardnum;
        private string _dptId;
        private string _dptname;
        private string _globalcode;
        private bool _isvalid;
        private DateTime _createtime;
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNum
        {
            set { _cardnum = value; }
            get { return _cardnum; }
        }
        /// <summary>
        /// 送货部Id
        /// </summary>
        public string DptId
        {
            set { _dptId = value; }
            get { return _dptId; }
        }

        /// <summary>
        /// 送货部名称
        /// </summary>
        public string DptName
        {
            set { _dptname = value; }
            get { return _dptname; }
        }

        /// <summary>
        /// 全球唯一码
        /// </summary>
        public string GlobalCode
        {
            set { _globalcode = value; }
            get { return _globalcode; }
        }
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsValid
        {
            set { _isvalid = value; }
            get { return _isvalid; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        #endregion Model

    }

    /// <summary>
    /// 出入门卡服务模型
    /// </summary>
    public class EntranceCardModelService : BaseModelService<EntranceCard, EntranceCardModel>
    {
        private readonly EntranceCardLogic BusinessLogic;

        public EntranceCardModelService()
        {
            BusinessLogic = new EntranceCardLogic();
        }

        protected override BaseLogic<EntranceCard> BaseLogic
        {
            get { return BusinessLogic; }
        }

        /// <summary>
        /// 获取送货部列表
        /// </summary>
        /// <returns></returns>
        public IList<SelectListItem> GetDptList()
        {
            IList<SelectListItem> selectList = new List<SelectListItem>();

            var dpts = new BaseEnumLogic().GetByType("Dept");

            foreach (var c in dpts)
            {
                SelectListItem item = new SelectListItem
                {
                    Text = c.ValueNote,
                    Value = c.Value
                };
                selectList.Add(item);
            }
            return selectList;

        }
    }

}