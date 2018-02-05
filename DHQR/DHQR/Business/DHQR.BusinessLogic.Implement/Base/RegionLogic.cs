using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;
using Common.DAL.Implement;
using Common.BLL.Implement;


namespace DHQR.BusinessLogic.Implement
{
    //行政区域
    public class RegionLogic : BaseLogic<Region>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();

        private RegionRepository regionRep { get { return new RegionRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<Region> Repository
        {
            get { return regionRep; }
        }

        //取一个省所有县级以上区域
        //regionName:省名
        public IList<Region> getCountyLevelRegion(string regionName)
        {
            List<Region> result = new List<Region>();
            Region proviceRegion = regionRep.GetByName(regionName);
            result.Add(proviceRegion);
            IList<Region> cityRegions = regionRep.GetByParentId(proviceRegion.Id);
            result.AddRange(cityRegions);
            foreach (Region item in cityRegions)
            {
                IList<Region> countyRegions = regionRep.GetByParentId(item.Id);
                result.AddRange(countyRegions);
            }
            return result;
        }

        //取一个省所有市级区域
        //regionName:省名
        public IList<Region> getCitysByProvinceName(string regionName)
        {
            List<Region> result = new List<Region>();
            Region proviceRegion = regionRep.GetByName(regionName);
            IList<Region> cityRegions = regionRep.GetByParentId(proviceRegion.Id);
            result.AddRange(cityRegions);
            return result;
        }

        //根据parentId获取区域
        public IList<Region> getByParentId(Guid parentId)
        {
            List<Region> result = new List<Region>();
            IList<Region> cityRegions = regionRep.GetByParentId(parentId);
            result.AddRange(cityRegions);
            return result;
        }

        /// <summary>
        /// 根据类型获取区域
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IList<Region> GetByType(RegionType type)
        {
            return regionRep.GetByType(type);
        }

    }
}
