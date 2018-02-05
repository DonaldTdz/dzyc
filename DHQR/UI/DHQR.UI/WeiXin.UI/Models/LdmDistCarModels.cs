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
    /// 配送车辆模型
    /// </summary>
    public class LdmDistCarModel
    {
        #region Model
        private Guid _id;
        private string _car_id;
        private string _car_name;
        private string _car_license;
        private string _dlvman_id;
        private string _dlvman_name;
        private string _driver_id;
        private string _driver_name;
        private string _is_mrb;
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
        /// 车辆编号
        /// </summary>
        public string CAR_ID
        {
            set { _car_id = value; }
            get { return _car_id; }
        }
        /// <summary>
        /// 车辆名称
        /// </summary>
        public string CAR_NAME
        {
            set { _car_name = value; }
            get { return _car_name; }
        }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string CAR_LICENSE
        {
            set { _car_license = value; }
            get { return _car_license; }
        }
        /// <summary>
        /// 送货员编码
        /// </summary>
        public string DLVMAN_ID
        {
            set { _dlvman_id = value; }
            get { return _dlvman_id; }
        }
        /// <summary>
        /// 送货员名称
        /// </summary>
        public string DLVMAN_NAME
        {
            set { _dlvman_name = value; }
            get { return _dlvman_name; }
        }
        /// <summary>
        /// 驾驶员编码
        /// </summary>
        public string DRIVER_ID
        {
            set { _driver_id = value; }
            get { return _driver_id; }
        }
        /// <summary>
        /// 驾驶员名称
        /// </summary>
        public string DRIVER_NAME
        {
            set { _driver_name = value; }
            get { return _driver_name; }
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
        /// 公司号
        /// </summary>
        public string COM_ID
        {
            set { _com_id = value; }
            get { return _com_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PSW
        {
            set { _psw = value; }
            get { return _psw; }
        }
        #endregion Model
    }


     /// <summary>
    /// 配送车辆服务模型
    /// </summary>
    public class LdmDistCarModelService : BaseModelService<LdmDistCar, LdmDistCarModel>
    {
        private readonly LdmDistCarLogic BusinessLogic;

        public LdmDistCarModelService()
        {
            BusinessLogic = new LdmDistCarLogic();
        }

        protected override BaseLogic<LdmDistCar> BaseLogic
        {
            get { return BusinessLogic; }
        }


        /// <summary>
        /// 获取配送线路列表
        /// </summary>
        /// <returns></returns>
        public IList<SelectListItem> GetLineList(bool includeAll)
        {
            IList<SelectListItem> selectList = new List<SelectListItem>();

            var ldmCars = BusinessLogic.GetAll().Where(f => f.IS_MRB == "1").OrderBy(f=>f.CAR_NAME).ToList();

            foreach (var c in ldmCars)
            {
                SelectListItem item = new SelectListItem 
                {
                    Text=c.CAR_NAME,
                    Value=c.CAR_ID.ToString()
                };
                selectList.Add(item);
            }
            if (includeAll)
            {
                SelectListItem first = new SelectListItem
                {
                    Text ="----全部----",
                    Value = string.Empty
                };
                selectList.Insert(0,first);
            }

            return selectList;

        }

      

    }
}