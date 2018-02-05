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
    /// 零售户模型
    /// </summary>
    public class RetailerModel
    {
        #region Model
        private Guid _id;
        private string _cust_id;
        private string _cust_name;
        private string _license_code;
        private string _status;
        private string _com_id;
        private string _psw;
        private decimal? _longitude;
        private decimal? _latitude;
        private string _address;
        private string _provinceAndCity;
        private string _areacode;
        private string _recievetype;
        private string _recievetypename;
        private string _rut_id;

        /// <summary>
        /// ID
        /// </summary>
        public Guid Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 客户内码
        /// </summary>
        public string CUST_ID
        {
            set { _cust_id = value; }
            get { return _cust_id; }
        }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CUST_NAME
        {
            set { _cust_name = value; }
            get { return _cust_name; }
        }
        /// <summary>
        /// 专卖证号
        /// </summary>
        public string LICENSE_CODE
        {
            set { _license_code = value; }
            get { return _license_code; }
        }
        /// <summary>
        /// 客户状态
        /// </summary>
        public string STATUS
        {
            set { _status = value; }
            get { return _status; }
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
        /// 收货密码
        /// </summary>
        public string PSW
        {
            set { _psw = value; }
            get { return _psw; }
        }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal? LONGITUDE
        {
            set { _longitude = value; }
            get { return _longitude; }
        }
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal? LATITUDE
        {
            set { _latitude = value; }
            get { return _latitude; }
        }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }

        /// <summary>
        /// 省市
        /// </summary>
        public string ProvinceAndCity
        {
            set { _provinceAndCity = value; }
            get { return _provinceAndCity; }
        }

        /// <summary>
        /// 区号
        /// </summary>
        public string AreaCode
        {
            set { _areacode = value; }
            get { return _areacode; }
        }
        /// <summary>
        /// 接收方式
        /// </summary>
        public string RecieveType
        {
            set { _recievetype = value; }
            get { return _recievetype; }
        }
        /// <summary>
        /// 接收方式描述
        /// </summary>
        public string RecieveTypeName
        {
            set { _recievetypename = value; }
            get { return _recievetypename; }
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
        /// 状态描述
        /// </summary>
        public string StatusStr 
        {
            get
            {
                switch (this.STATUS)
                {
                    case "01": return "新增";
                    case "02": return "有效";
                    case "03": return "暂停";
                    case "04": return "无效";
                    default: return "新增";
                }
            }
        }


        /// <summary>
        /// 是否采点
        /// </summary>
        public bool HasPoint
        {
            get
            {
                return (this.LONGITUDE.HasValue && this.LATITUDE.HasValue) ? true : false;
            }
        }

        #endregion Model

    }


    /// <summary>
    /// 零售户服务模型
    /// </summary>
    public class RetailerModelService:BaseModelService<Retailer,RetailerModel>
    {
        private readonly RetailerLogic BusinessLogic;

        public RetailerModelService()
        {
            BusinessLogic = new RetailerLogic();
        }

        protected override BaseLogic<Retailer> BaseLogic
        {
            get { return BusinessLogic; }
        }


        /// <summary>
        /// 查询零售户信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public PagedResults<RetailerModel> GetRetailerPageData(RetailerQueryParam queryParam)
        {
            var datas = BusinessLogic.GetRetailerPageData(queryParam);
            var pagedInfo = datas.PagerInfo;
            var dt = datas.Data.Select(f => ConvertToModel(f)).ToList();
            var result = new PagedResults<RetailerModel> {Data=dt,PagerInfo=pagedInfo };
            return result;
        }

        /// <summary>
        /// 获取接收方式选择列表
        /// </summary>
        /// <returns></returns>
        public IList<SelectListItem> GetRecieveTypeList(string selectValue)
        {
            IList<SelectListItem> selectList = new List<SelectListItem>();
            BaseEnumLogic enumLogic = new BaseEnumLogic();
            var datas = enumLogic.GetByType("ReciveType");
            foreach (var item in datas)
            {

                if (item.Value == selectValue)
                {
                    selectList.Add(new SelectListItem
                    {
                        Text = item.ValueNote,
                        Value = item.Value,
                        Selected = true
                    });
                }
                else
                {
                    selectList.Add(new SelectListItem
                    {
                        Text = item.ValueNote,
                        Value = item.Value
                    });
                   
                }
            }
            if (string.IsNullOrEmpty(selectValue))
            {
                SelectListItem defalutData = new SelectListItem { Text = "——全部——", Value =string.Empty };
                selectList.Insert(0, defalutData);
            }
            return selectList;

        }

        /// <summary>
        /// 获取零售户状态列表
        /// </summary>
        /// <returns></returns>
        public IList<SelectListItem> GetRetailerStatusList()
        {
            IList<SelectListItem> selectList = new List<SelectListItem>();
            BaseEnumLogic enumLogic = new BaseEnumLogic();
            var datas = enumLogic.GetByType("RetailerStatus");
            foreach (var item in datas)
            {
                    selectList.Add(new SelectListItem
                    {
                        Text = item.ValueNote,
                        Value = item.Value
                    });
            }

            SelectListItem defalutData = new SelectListItem { Text = "——全部——", Value =string.Empty};
            selectList.Insert(0, defalutData);
            return selectList;

        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="dohandle"></param>
        public void ResetPsw(Guid Id, out DoHandle dohandle)
        {
            var retailer = BusinessLogic.GetByKey(Id);
            retailer.PSW = PassWordMd.HashPassword("654321");
            BusinessLogic.Update(retailer,out dohandle);
        }
    }

}