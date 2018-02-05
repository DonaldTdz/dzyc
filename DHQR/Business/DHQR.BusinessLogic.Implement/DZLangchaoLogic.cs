using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Langchao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DHQR.BusinessLogic.Implement
{
    public class DZLangchaoLogic
    {
        public DZLangchaoDB2Repository repository;

        public DZLangchaoLogic()
        {
            repository = new DZLangchaoDB2Repository();
        }

        #region 访问达州数据库测试

        public List<LdmDist> GetDists(DownloadDistParam param)
        {
            var idists = repository.GetDists(param);
            var ldmDists = idists.Select(f => ConvertFromLC.ConvertDist(f)).ToList();
            return ldmDists;
        }

        #endregion

        #region 下载配送单

        /// <summary>
        /// 下载配送单
        /// </summary>
        /// <param name="param"></param>
        /// <param name="ldmDists"></param>
        /// <param name="ldmDistLines"></param>
        /// <param name="ldmDisItems"></param>
        public void DownloadDists(DownloadDistParam param, out List<LdmDist> ldmDists, out List<LdmDistLine> ldmDistLines, out List<LdmDisItem> ldmDisItems)
        {
            List<DZ_I_DIST> idists = new List<DZ_I_DIST>();
            List<DZ_I_DIST_LINE> idistLine = new List<DZ_I_DIST_LINE>();
            List<DZ_I_DIST_ITME> idistItem = new List<DZ_I_DIST_ITME>();
            BaseEnumLogic enumLogic = new BaseEnumLogic();
            int configDate = int.Parse(enumLogic.GetByType("DistDate").FirstOrDefault().Value);

            repository.DownloadDists(param, configDate, out idists, out idistLine, out idistItem);
            ldmDists = idists.Select(f => ConvertFromLC.ConvertDist(f)).ToList();
            ldmDistLines = idistLine.Select(f => ConvertFromLC.ConvertDistLine(f)).ToList();
            ldmDisItems = idistItem.Select(f => ConvertFromLC.ConvertDistItem(f)).ToList();


            RetailerLogic retailerLogic = new RetailerLogic();
            var custIds = ldmDistLines.Select(f => f.CUST_ID).ToList();

            //附加零售户收货密码和坐标
            IList<Retailer> retailers = retailerLogic.GetRetailersByCustIds(custIds);

            GisCustPoisLogic custPoiLogic = new GisCustPoisLogic();
            IList<GisCustPois> custPois = custPoiLogic.GetRetailerLocation(custIds);

            foreach (var item in ldmDistLines)
            {
                var retailer = retailers.SingleOrDefault(f => f.CUST_ID == item.CUST_ID);
                item.PSW = retailer == null ? string.Empty : retailer.PSW;
                item.LATITUDE = retailer.LATITUDE;
                item.LONGITUDE = retailer.LONGITUDE;

                var gisCust = custPois.FirstOrDefault(f => f.CUST_ID == item.CUST_ID);
                item.ORIGINAL_LATITUDE = gisCust == null ? null : gisCust.ORIGINAL_LATITUDE;
                item.ORIGINAL_LONGITUDE = gisCust == null ? null : gisCust.ORIGINAL_LONGITUDE;
            }

        }
        #endregion

       
    }
}
