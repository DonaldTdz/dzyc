using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using System.Data.Objects;

namespace DHQR.DataAccess.Implement
{
    //行政区域 
    public class RegionRepository : ProRep<Region>
    {

        public RegionRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<Region> EntityCurrentSet
        {
            get { return ActiveContext.Regions; }
        }

        public Region GetByName(string name)
        {
          return   EntityCurrentSet.FirstOrDefault(p => p.Name.Contains(name));
        }

        public IList<Region> GetByParentId(Guid parentId)
        {
            return EntityCurrentSet.Where(p => p.ParentId == parentId).OrderBy(p=>p.Code).ToList();
        }

        /// <summary>
        /// 根据类型获取区域
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IList<Region> GetByType(RegionType type)
        {
            var tp=(byte)type;
            var result = ActiveContext.Regions.Where(f => f.Level == tp).ToList();
            return result;
        }

        /// <summary>
        /// 根据类型获取区域(多种区域类型)
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public IList<Region> GetByTypes(IList<RegionType> types) {
            IList<byte> tps = types.Select(f => (byte)f).ToList();
            var result = ActiveContext.Regions.Where(f => tps.Contains(f.Level)).ToList();
            return result;
        }
    }
}
