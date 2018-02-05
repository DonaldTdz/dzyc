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
    /// 送货员实时位置数据访问层
    /// </summary>
    public class GisLastLocrecordRepository : ProRep<GisLastLocrecord>
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public GisLastLocrecordRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<GisLastLocrecord> EntityCurrentSet
        {
            get { return ActiveContext.GisLastLocrecords; }
        }


        #region 送货员实时位置上传

        /// <summary>
        /// 送货员实时位置上传
        /// </summary>
        /// <param name="locRecord"></param>
        /// <param name="dohandle"></param>
        public void UploadLocation(GisLastLocrecord locRecord, out DoHandle dohandle)
        {
            dohandle = new DoHandle {IsSuccessful=false,OperateMsg="上传失败!" };

            decimal lat = locRecord.ORIGINAL_LATITUDE;
            decimal lon = locRecord.ORIGINAL_LONGITUDE;
            double mgLat, mgLon;
            CoordinateTransform.transform((Double)lat, (Double)lon, out mgLat, out mgLon);
            locRecord.GPS_LATITUDE = lat;
            locRecord.GPS_LONGITUDE = lon;
            locRecord.ORIGINAL_LATITUDE =(decimal)mgLat;
            locRecord.ORIGINAL_LONGITUDE =(decimal)mgLon;

            locRecord.Id = Guid.NewGuid();
            locRecord.CreateTime = DateTime.Now;
            ActiveContext.GisLastLocrecords.AddObject(locRecord);
            ActiveContext.SaveChanges();
            dohandle.IsSuccessful = true;
            dohandle.OperateMsg = "上传成功!";
        }


        #endregion

        #region

        /// <summary>
        /// 查询配送员最近位置
        /// </summary>
        /// <returns></returns>
        public IList<GisLastLocrecord> GetLatestGisInfos()
        {
            IList<GisLastLocrecord> result = new List<GisLastLocrecord>();

            var groupedDatas = ActiveContext.GisLastLocrecords
                                   .GroupBy(t => t.M_CODE);

            foreach (var item in groupedDatas)
            {
                var maxDate = item.Max(f => f.CreateTime);
                var data = ActiveContext.GisLastLocrecords.SingleOrDefault(f => f.M_CODE == item.Key && f.CreateTime == maxDate);
                var uf = ActiveContext.DistDlvmans.FirstOrDefault(f => f.POSITION_CODE == data.M_CODE);
                data.CAR_NAME = uf == null ? "" : uf.USER_NAME;
                result.Add(data);

            }
            return result;
        }

        #endregion



        /// <summary>
        /// 查询配送员线路坐标
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<GisLastLocrecord> GetGisInfosOfDlvman(GisLastLocrecordQueryParam queryParam)
        {
            var result = ActiveContext.GisLastLocrecords.Where(f => f.CreateTime >= queryParam.StartDate

                                                                && f.CreateTime<=queryParam.EndDate
                                                                && f.M_CODE==queryParam.DLVMAN_ID
                ).OrderBy(f=>f.CreateTime).ToList();
            return result;
        }



    }
}
