using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;
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
        readonly DHQREntities _baseDataEntities = new DHQREntities();

        private LdmDistLineRepository lineRep { get { return new LdmDistLineRepository(_baseDataEntities); } }

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

        #region 同步配送员信息

        /// <summary>
        /// 同步车辆信息
        /// </summary>
        /// <param name="dohanle"></param>
        public IList<DistDlvman> GetDistDlvmans(string userId)
        {
            var mans = repository.GetDistDlvmans(userId);
            var datas = mans.Select(f => ConvertFromLC.ConvertDistDlvman(f)).ToList();
            return datas;
        }

        #endregion

        #region 同步线路信息

        /// <summary>
        /// 同步线路信息
        /// </summary>
        /// <param name="COM_ID">公司ID</param>
        /// <returns></returns>
        public List<DistRut> GetRutList()
        {
            var ruts = repository.GetRutList("");
            var datas = ruts.Select(f => ConvertFromLC.ConvertDistRut(f)).ToList();
            return datas;
        }

        #endregion

        #region 同步车辆信息

        /// <summary>
        /// 同步车辆信息
        /// </summary>
        /// <param name="dohanle"></param>
        //public void SysDistCars(out DoHandle dohandle)
        //{
        //    var cars = repository.GetDistCar("300000001");
        //    var datas = cars.Select(f => ConvertFromLC.ConvertCar(f)).ToList();
        //    LdmDistCarRep.InsertCar(datas, out dohandle);
        //}


        /// <summary>
        /// 获取车辆列表
        /// </summary>
        /// <returns></returns>
        public List<LdmDistCar> GetCarList()
        {
            var cars = repository.GetDistCar("300000001");
            var datas = cars.Select(f => ConvertFromLC.ConvertCar(f)).ToList();
            return datas;
        }

        #endregion

        #region 获取零售户信息

        /// <summary>
        /// 获取零售户信息
        /// </summary>
        /// <param name="COM_ID">公司ID</param>
        /// <returns></returns>
        public List<Retailer> GetCustomer(string COM_ID)
        {
            var result = repository.GetCustomer(COM_ID).Select(f => ConvertFromLC.ConvertRetailer(f)).ToList();
            return result;
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

        #region 下载配送单(按日期)

        /// <summary>
        /// 下载配送单
        /// </summary>
        /// <param name="param"></param>
        /// <param name="ldmDists"></param>
        /// <param name="ldmDistLines"></param>
        /// <param name="ldmDisItems"></param>
        public void DownloadDistByDate(DownloadDistByDateParam param, out List<LdmDist> ldmDists, out List<LdmDistLine> ldmDistLines, out List<LdmDisItem> ldmDisItems, out List<I_CO_TEMP_RETURN> icoTempReturns)
        {
            List<DZ_I_DIST> idists = new List<DZ_I_DIST>();
            List<DZ_I_DIST_LINE> idistLine = new List<DZ_I_DIST_LINE>();
            List<DZ_I_DIST_ITME> idistItem = new List<DZ_I_DIST_ITME>();
            icoTempReturns = new List<I_CO_TEMP_RETURN>();//暂存订单


            repository.DownloadDistByDate(param, out idists, out idistLine, out idistItem, out icoTempReturns);

            var tmpReturnLines = lineRep.ConvertTempToLine(icoTempReturns);
            ldmDists = idists.Select(f => ConvertFromLC.ConvertDist(f)).ToList();
            ldmDistLines = idistLine.Select(f => ConvertFromLC.ConvertDistLine(f)).ToList();
            ldmDisItems = idistItem.Select(f => ConvertFromLC.ConvertDistItem(f)).ToList();
            ldmDistLines.AddRange(tmpReturnLines);


            RetailerLogic retailerLogic = new RetailerLogic();
            var custIds = ldmDistLines.Select(f => f.CUST_ID).ToList();

            //附加零售户收货密码和坐标
            IList<Retailer> retailers = retailerLogic.GetRetailersByCustIds(custIds);

            GisCustPoisLogic custPoiLogic = new GisCustPoisLogic();
            IList<GisCustPois> custPois = custPoiLogic.GetRetailerLocation(custIds);

            foreach (var item in ldmDistLines)
            {
                var retailer = retailers.SingleOrDefault(f => f.CUST_ID == item.CUST_ID);
                item.PSW = retailer == null ? "c33367701511b4f6020ec61ded352059" : retailer.PSW;
                item.LATITUDE = retailer == null ? null : retailer.LATITUDE;
                item.LONGITUDE = retailer == null ? null : retailer.LONGITUDE;

                var gisCust = custPois.FirstOrDefault(f => f.CUST_ID == item.CUST_ID);
                item.ORIGINAL_LATITUDE = gisCust == null ? null : gisCust.ORIGINAL_LATITUDE;
                item.ORIGINAL_LONGITUDE = gisCust == null ? null : gisCust.ORIGINAL_LONGITUDE;
            }

        }

        /// <summary>
        /// 通过暂存订单获取订单详情
        /// </summary>
        /// <param name="tmpReturns"></param>
        /// <param name="distLines"></param>
        /// <param name="distItems"></param>
        private List<LdmDistLine> ConvertTempToLine(List<I_CO_TEMP_RETURN> tmpReturns)
        {
            var coNums = tmpReturns.Select(f => f.CO_NUM).ToList();
            var lines = lineRep.GetByCoNums(coNums).ToList();
            foreach (var item in lines)
            {
                var tp = tmpReturns.FirstOrDefault(f => f.CO_NUM == item.CO_NUM);
                item.Id = Guid.NewGuid();
                item.DIST_NUM = tp.OUT_DIST_NUM;
                item.IsTemp = true;
            }
            return lines;
        }

        #endregion


    }
}
