using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.Base;

namespace DHQR.DataAccess.Entities
{
    public class HouseReportAreaParam : QueryParam
    {
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public Guid CityId { set; get; }
        public string CityName { set; get; }
        public Guid CountyId { set; get; }
        public string HouseType { set; get; }

        public IList<Guid> RegionIds { set; get; }

        public IList<string> RegionNames { set; get; }

        public IList<string> HouseTypeNames { set; get; }
    }




}