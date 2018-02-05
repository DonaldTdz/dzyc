using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DHQR.DataAccess.Entities;
using DHQR.BusinessLogic.Implement;
using Common.BLL.Implement;
using Common.UI.Model;
using System.Web.Mvc;

namespace DHQR.UI.Models
{
    public class RegionModel
    {
        public Guid Id { set; get; }

        public string Name { set; get; }

        public Guid ParentId { set; get; }

        public int Level { set; get; }
    }

    public class RegionModelService : BaseModelService<Region, RegionModel>
    {
        private readonly RegionLogic logic;
        public RegionModelService()
        {
            logic = new RegionLogic();
        }

        protected override BaseLogic<Region> BaseLogic
        {
            get { return logic; }
        }



        //根据省名称获取所有的市区域
        internal IList<SelectListItem> getCitysByProvinceName(string provinceName)
        {
            IList<SelectListItem> result = logic.getCitysByProvinceName(provinceName).Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name }).ToList();
            result.Insert(0, new SelectListItem { Value = Guid.Empty.ToString(), Text = "请选择", Selected = true });
            result.First().Selected = true;
            return result;
        }

        //根据parentId获取区域
        internal IList<SelectListItem> getByParentId(Guid parentId)
        {
            IList<SelectListItem> result = logic.getByParentId(parentId).Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name }).ToList();
            result.Insert(0, new SelectListItem { Value = Guid.Empty.ToString(), Text = "全部", Selected = true });
            return result;
        }

        /// <summary>
        /// 根据类型获取区域
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IList<RegionModel> GetByType(RegionType type)
        {
            var result = logic.GetByType(type).Select(ConvertToModel).ToList();
            return result;
        }

        /// <summary>
        /// 取省份
        /// </summary>
        /// <returns></returns>
        public IList<SelectListItem> getProvinceSelList() {
            var data = GetByType(RegionType.Province);
            IList<SelectListItem> result = data.Select(f => new SelectListItem() { Value = f.Id.ToString(), Text = f.Name }).ToList();
            return result;
        }
    }
}