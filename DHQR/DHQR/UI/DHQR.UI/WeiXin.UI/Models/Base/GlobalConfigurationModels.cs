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
    /// 全局配置
    /// </summary>
    public class GlobalConfigurationModel
    {
        #region Model
        private Guid _id;
        private string _name;
        private string _type;
        private string _key;
        private string _value;
        /// <summary>
        /// 
        /// </summary>
        public Guid Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Key
        {
            set { _key = value; }
            get { return _key; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            set { _value = value; }
            get { return _value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        #endregion Model
    }

    
    /// <summary>
    /// 配送点预警服务模型
    /// </summary>
    public class GlobalConfigurationModelService : BaseModelService<GlobalConfiguration, GlobalConfigurationModel>
    {
         private readonly GlobalConfigurationLogic BusinessLogic;

         public GlobalConfigurationModelService()
        {
            BusinessLogic = new GlobalConfigurationLogic();
        }

         protected override BaseLogic<GlobalConfiguration> BaseLogic
        {
            get { return BusinessLogic; }
        }

         /// <summary>
         /// 获取送货部列表
         /// </summary>
         /// <returns></returns>
         public IList<SelectListItem> GetDataTypeList()
         {
             IList<SelectListItem> selectList = new List<SelectListItem>();
             SelectListItem item1 = new SelectListItem {Text="int",Value="int" };
             selectList.Add(item1);
             SelectListItem item2 = new SelectListItem { Text = "String", Value = "String" };
             selectList.Add(item2);
             SelectListItem item3 = new SelectListItem { Text = "Guid", Value = "Guid" };
             selectList.Add(item3);
             SelectListItem item4 = new SelectListItem { Text = "decimal", Value = "decimal" };
             selectList.Add(item4);

             return selectList;

         }

    }
}