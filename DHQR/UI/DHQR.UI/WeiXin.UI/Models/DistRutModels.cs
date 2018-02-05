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
    /// 配送线路模型
    /// </summary>
    public class DistRutModel
    {
        #region Model
        private Guid _id;
        private string _rut_id;
        private string _rut_name;
        private string _is_mrb;
        private string _com_id;
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 线路ID
        /// </summary>
        public string RUT_ID
        {
            set { _rut_id = value; }
            get { return _rut_id; }
        }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RUT_NAME
        {
            set { _rut_name = value; }
            get { return _rut_name; }
        }
        /// <summary>
        /// 是否有效
        /// </summary>
        public string IS_MRB
        {
            set { _is_mrb = value; }
            get { return _is_mrb; }
        }
        /// <summary>
        /// 公司代码
        /// </summary>
        public string COM_ID
        {
            set { _com_id = value; }
            get { return _com_id; }
        }
        #endregion Model

    }

    /// <summary>
    /// 配送线路服务模型
    /// </summary>
    public class DistRutModelService : BaseModelService<DistRut, DistRutModel>
    {
         private readonly DistRutLogic BusinessLogic;

         public DistRutModelService()
        {
            BusinessLogic = new DistRutLogic();
        }

         protected override BaseLogic<DistRut> BaseLogic
        {
            get { return BusinessLogic; }
        }



         /// <summary>
         /// 获取线路列表
         /// </summary>
         /// <returns></returns>
         public IList<SelectListItem> GetRutList(bool includeAll)
         {
             IList<SelectListItem> selectList = new List<SelectListItem>();

             var ruts = BusinessLogic.GetAll().Where(f=>f.IS_MRB=="1").OrderBy(f=>f.RUT_NAME).ToList();

             foreach (var c in ruts)
             {
                 SelectListItem item = new SelectListItem
                 {
                     Text = c.RUT_NAME,
                     Value = c.RUT_ID
                 };
                 selectList.Add(item);
             }
             if (includeAll)
             {
                 SelectListItem first = new SelectListItem
                 {
                     Text = "----全部----",
                     Value = string.Empty
                 };
                 selectList.Insert(0, first);
             }
             return selectList;

         }


    }

}