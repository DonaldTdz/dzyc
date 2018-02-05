using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using System.Data.Objects;
using Common.Base;
using DHQR.BasicLib;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 零售户位置信息数据访问层
    /// </summary>
    public class GisCustPoisRepository : ProRep<GisCustPois>
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public GisCustPoisRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<GisCustPois> EntityCurrentSet
        {
            get { return ActiveContext.GisCustPois; }
        }


        #region 零售户位置信息上传

        /// <summary>
        /// 零售户位置信息上传
        /// </summary>
        /// <param name="poi"></param>
        /// <param name="dohanle"></param>
        public void CollectRetailerXY(GisCustPois poi,out DoHandle dohanle)
        {
            dohanle = new DoHandle { IsSuccessful=false,OperateMsg="上传失败!"};

            var item = ActiveContext.GisCustPois.SingleOrDefault(f => f.CUST_ID == poi.CUST_ID);
            if (item == null)
            {
                poi.Id = Guid.NewGuid();
                ActiveContext.GisCustPois.AddObject(poi);
            }
            else
            {
                item.ORIGINAL_LATITUDE = poi.ORIGINAL_LATITUDE;
                item.ORIGINAL_LONGITUDE = poi.ORIGINAL_LONGITUDE;
            }
            var retailer = ActiveContext.Retailers.SingleOrDefault(f => f.CUST_ID == poi.CUST_ID);
            if (retailer != null)
            {   
                decimal lat = poi.ORIGINAL_LATITUDE.Value;
                decimal lon =  poi.ORIGINAL_LONGITUDE.Value;
                double mgLat, mgLon;
                CoordinateTransform.transform((Double)lat, (Double)lon, out mgLat, out mgLon);
                retailer.LATITUDE =(decimal)mgLat;
                retailer.LONGITUDE =(decimal)mgLon;
               
            }

            ActiveContext.SaveChanges();
            dohanle.IsSuccessful = true;
            dohanle.OperateMsg = "上传成功!";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="poi"></param>
        /// <param name="dohanle"></param>
        public void SynLatLng()
        {
            var datas = ActiveContext.GisCustPois.ToList();
            foreach (var item in datas)
            {
                var retailer = ActiveContext.Retailers.SingleOrDefault(f => f.CUST_ID == item.CUST_ID);
                if (retailer != null)
                {
                    decimal lat = item.ORIGINAL_LATITUDE.Value;
                    decimal lon = item.ORIGINAL_LONGITUDE.Value;
                    double mgLat, mgLon;
                    CoordinateTransform.transform((Double)lat, (Double)lon, out mgLat, out mgLon);
                    retailer.LATITUDE = (decimal)mgLat;
                    retailer.LONGITUDE = (decimal)mgLon;

                }

            }
            ActiveContext.SaveChanges();
        }

        #endregion

        #region 获取零售户位置信息

        /// <summary>
        /// 根据零售户ID集合获取零售户位置信息
        /// </summary>
        /// <param name="custIds"></param>
        /// <returns></returns>
        public List<GisCustPois> GetRetailerLocation(List<string> custIds)
        {
            var result = ActiveContext.GisCustPois
                        .Where(f => 
                             custIds.Count==0 || custIds.Contains(f.CUST_ID)
                            ).ToList();
            return result;
        }

        #endregion

        #region 同步零售户采点信息

        /// <summary>
        /// 同步零售户采点信息
        /// </summary>
        public void SynCustPois(IList<GisCustPois> pois)
        {
            foreach (var poi in pois)
            {
                var item = ActiveContext.GisCustPois.SingleOrDefault(f => f.CUST_ID == poi.CUST_ID);
                if (item == null)
                {
                    poi.Id = Guid.NewGuid();
                    ActiveContext.GisCustPois.AddObject(poi);
                }
                else
                {
                    item.ORIGINAL_LATITUDE = poi.ORIGINAL_LATITUDE;
                    item.ORIGINAL_LONGITUDE = poi.ORIGINAL_LONGITUDE;
                }

            }
            ActiveContext.SaveChanges();
            
        }


        #endregion
    }
}
