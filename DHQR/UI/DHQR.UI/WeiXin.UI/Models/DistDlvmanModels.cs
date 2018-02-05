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
    /// 配送员基础信息模型
    /// </summary>
    public class DistDlvmanModel
    {
        #region Model
        private Guid _id;
        private string _user_id;
        private string _user_name;
        private string _organ_id;
        private string _position_code;
        private string _com_id;
        private string _psw;
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 送货员帐号
        /// </summary>
        public string USER_ID
        {
            set { _user_id = value; }
            get { return _user_id; }
        }
        /// <summary>
        /// 送货员名称
        /// </summary>
        public string USER_NAME
        {
            set { _user_name = value; }
            get { return _user_name; }
        }
        /// <summary>
        /// 送货员组织编码
        /// </summary>
        public string ORGAN_ID
        {
            set { _organ_id = value; }
            get { return _organ_id; }
        }
        /// <summary>
        /// 送货员岗位编码
        /// </summary>
        public string POSITION_CODE
        {
            set { _position_code = value; }
            get { return _position_code; }
        }
        /// <summary>
        /// 公司号
        /// </summary>
        public string COM_ID
        {
            set { _com_id = value; }
            get { return _com_id; }
        }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string PSW
        {
            set { _psw = value; }
            get { return _psw; }
        }
        #endregion Model
    }

    /// <summary>
    /// 配送员信息基础信息服务模型
    /// </summary>
    public class DistDlvmanModelService : BaseModelService<DistDlvman, DistDlvmanModel>
    {
        private readonly DistDlvmanLogic BusinessLogic;

        public DistDlvmanModelService()
        {
            BusinessLogic = new DistDlvmanLogic();
        }

        protected override BaseLogic<DistDlvman> BaseLogic
        {
            get { return BusinessLogic; }
        }

        /// <summary>
        /// 获取配送员列表
        /// </summary>
        /// <returns></returns>
        public IList<SelectListItem> GetDlvManList()
        {
            IList<SelectListItem> selectList = new List<SelectListItem>();

            var ldmCars = BusinessLogic.GetAll().ToList();

            foreach (var c in ldmCars)
            {
                SelectListItem item = new SelectListItem
                {
                    Text = c.USER_NAME,
                    Value = c.POSITION_CODE.ToString()
                };
                selectList.Add(item);
            }
            return selectList;

        }



    }
}