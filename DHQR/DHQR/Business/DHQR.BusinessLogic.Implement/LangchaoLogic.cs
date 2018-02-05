using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.BLL.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;
using Common.DAL.Implement;
using Common.Base;
using DHQR.DataAccess.Langchao;

namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// 浪潮数据逻辑层
    /// </summary>
    public class LangchaoLogic
    {

        public LangchaoDB2Repository repository;
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private LdmDistCarRepository LdmDistCarRep { get { return new LdmDistCarRepository(_baseDataEntities); } }
        private LdmDistLineRepository lineRep { get { return new LdmDistLineRepository(_baseDataEntities); } }
        private CoTempReturnRepository coTempRep { get { return new CoTempReturnRepository(_baseDataEntities); } }


        /// <summary>
        /// 构造函数
        /// </summary>
        public LangchaoLogic()
        {
            repository = new LangchaoDB2Repository();
        }

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


        #region 同步车辆信息

        /// <summary>
        /// 同步车辆信息
        /// </summary>
        /// <param name="dohanle"></param>
        public void SysDistCars(out DoHandle dohandle)
        {
            var cars = repository.GetDistCar("300000001");
            var datas = cars.Select(f => ConvertFromLC.ConvertCar(f)).ToList();
            LdmDistCarRep.InsertCar(datas, out dohandle);
        }


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


        #region 同步配送员信息

        /// <summary>
        /// 同步车辆信息
        /// </summary>
        /// <param name="dohanle"></param>
        public IList<DistDlvman> GetDistDlvmans()
        {
            var mans = repository.GetDistDlvmans("300000001");
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
            var ruts = repository.GetRutList("300000001");
            var datas = ruts.Select(f => ConvertFromLC.ConvertDistRut(f)).ToList();
            return datas;
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
            List<I_DIST> idists = new List<I_DIST>();
            List<I_DIST_LINE> idistLine = new List<I_DIST_LINE>();
            List<I_DIST_ITEM> idistItem = new List<I_DIST_ITEM>();
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
            List<I_DIST> idists = new List<I_DIST>();
            List<I_DIST_LINE> idistLine = new List<I_DIST_LINE>();
            List<I_DIST_ITEM> idistItem = new List<I_DIST_ITEM>();
            icoTempReturns = new List<I_CO_TEMP_RETURN>();//暂存订单


            repository.DownloadDistByDate(param, out idists, out idistLine, out idistItem,out icoTempReturns);

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
                item.LATITUDE =retailer==null?null: retailer.LATITUDE;
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
            var coNums=tmpReturns.Select(f=>f.CO_NUM).ToList();
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

        #region 备车检查

        /// <summary>
        /// 备车检查
        /// </summary>
        /// <param name="chek"></param>
        /// <param name="dohandle"></param>
        public void CheckCar(List<DistCarCheck> cheks, DistRecordLog log, out DoHandle dohandle)
        {
            List<I_DIST_CAR_CHECK> carCheck = cheks.Select(f => ConvertToLC.ConvertCarCheck(f)).ToList();
            I_DIST_RECORD_LOG recLog = ConvertToLC.ConvertRecordLog(log);
            repository.CheckCar(carCheck, recLog, out dohandle);
        }


        #endregion


        #region 写操作日志

        /// <summary>
        /// 写操作日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="dohanle"></param>
        public void WriteDistRecordLog(I_DIST_RECORD_LOG log, out DoHandle dohanle)
        {
            repository.WriteDistRecordLog(log, out dohanle);
        }

        /// <summary>
        /// 写操作日志
        /// </summary>
        /// <param name="log"></param>
        /// <param name="dohanle"></param>
        public void WriteDistRecordLog(IList<I_DIST_RECORD_LOG> logs, out DoHandle dohanle)
        {
            repository.WriteDistRecordLog(logs, out dohanle);
        }


        #endregion

        #region 车辆回程登记

        /// <summary>
        /// 车辆回程登记
        /// </summary>
        /// <param name="carRun"></param>
        /// <param name="carLines"></param>
        /// <param name="log"></param>
        /// <param name="dohandle"></param>
        public void BackRegist(DistCarRun carRun, List<DistCarRunLine> carLines, DistRecordLog log, out DoHandle dohandle)
        {

            var lcCarRun = ConvertToLC.ConvertCarRun(carRun);
            var lcCarLines = carLines.Select(f => ConvertToLC.ConvertCarRunLine(f)).ToList();
            var lcLog = ConvertToLC.ConvertRecordLog(log);
            // lcCarRun.ACT_MIL = lcCarRun.THIS_MIL - lcCarRun.PRE_MIL;
            repository.BackRegist(lcCarRun, lcCarLines, lcLog, out dohandle);
        }


        #endregion


        #region 到货确认

        /// <summary>
        /// 到货确认
        /// </summary>
        /// <param name="dist"></param>
        /// <param name="dohandle"></param>
        public void ConfirmDelivery(DistCust dist, out DoHandle dohandle)
        {
            var isCoTemp = coTempRep.IsCoTempReturn(dist.DIST_NUM, dist.CO_NUM);

            if (dist.UNUSUAL_TYPE == "01")//暂存
            {
                //判断是否暂存订单，如果是暂存订单，只更新暂存订单信息
                CoTemp tmp = new CoTemp
                {
                    CO_NUM = dist.CO_NUM,
                    DIST_NUM = dist.DIST_NUM,
                    RET_TIME = dist.EVALUATE_TIME,
                    RET_USER_ID = dist.DLVMAN_ID,
                    STATUS = "01"
                };
                if (!isCoTemp)
                {   //情况1：正常订单暂存
                    //插入I_DIST_CUST，其中IS_RECEIVED=‘04’， UNUSUAL_TYPE=‘01’,
                    //其次要插入I_CO_TEMP，其中STATUS=‘01’返库，且写入RET_TIME，RET_USER_ID
                    CoTmpRegist(tmp, out dohandle);
                    var lcData = ConvertToLC.ConvertDistCust(dist);
                    repository.ConfirmDelivery(lcData, out dohandle);
                }
                else
                {
                     //情况2：暂存订单再次暂存
                    //不操作I_DIST_CUST,更新I_CO_TEMP，更新RET_TIME，RET_USER_ID，并且STATUS=‘01’返库
                    CoTmpUpdate(tmp,out dohandle);
                }
            }
            else//正常卸货
            {
                if (!isCoTemp)
                {
                    //情况3：普通订单正常卸货
                    var lcData = ConvertToLC.ConvertDistCust(dist);
                    repository.ConfirmDelivery(lcData, out dohandle);
                }
                else
                {
                    //情况4：暂存订单正常卸货
                    //更新I_DIST_CUST，其中IS_RECEIVED=‘01’， UNUSUAL_TYPE=‘’；
                    //其次，更新I_CO_TEMP，其中STATUS=‘03’完结，且写入FINISH_TIME，FINISH_USER_ID
                    var lcData = ConvertToLC.ConvertDistCust(dist);
                    repository.ConfirmCoTemp(lcData, out dohandle);
                }
            }
        }

        #endregion


        #region 暂存登记

        /// <summary>
        /// 暂存登记
        /// </summary>
        /// <param name="dist"></param>
        /// <param name="dohandle"></param>
        public void CoTmpRegist(CoTemp tmp, out DoHandle dohandle)
        {
            var lcData = ConvertToLC.ConvertCoTemp(tmp);
            repository.CoTmpRegist(lcData, out dohandle);

        }

        /// <summary>
        /// 暂存订单再次暂存更新
        /// </summary>
        /// <param name="dist"></param>
        /// <param name="dohandle"></param>
        public void CoTmpUpdate(CoTemp tmp, out DoHandle dohandle)
        {
            var lcData = ConvertToLC.ConvertCoTemp(tmp);
            repository.CoTmpUpdate(lcData, out dohandle);

        }




        #endregion


        #region 退货


        /// <summary>
        /// 整单退货
        /// </summary>
        /// <param name="cust"></param>
        /// <param name="dohandle"></param>
        public void ReturnAllOrder(I_DIST_CUST cust, string RETURN_CO_NUM, out DoHandle dohandle)
        {
            repository.ReturnAllOrder(cust, RETURN_CO_NUM, out dohandle);

        }

        /// <summary>
        /// 部分退货
        /// </summary>
        /// <param name="cust"></param>
        /// <param name="order"></param>
        /// <param name="lines"></param>
        /// <param name="dohandle"></param>
        public void ReturnPartialOrder(I_DIST_CUST cust, I_CO_RETURN order, List<I_CO_RETURN_LINE> lines, out DoHandle dohandle)
        {
            repository.ReturnPartialOrder(cust, order, lines, out dohandle);
        }


        #endregion

        #region 零售户位置信息采集

        /// <summary>
        /// 零售户位置信息采集
        /// </summary>
        /// <param name="poi"></param>
        /// <param name="dohandle"></param>
        public void CollectRetailerXY(GisCustPois poi, out DoHandle dohandle)
        {
            var param = ConvertToLC.ConvertCustPoi(poi);
            repository.CollectRetailerXY(param, out dohandle);
        }

        #endregion


        #region 位置上传

        /// <summary>
        /// 配送员位置上传
        /// </summary>
        /// <param name="poi"></param>
        /// <param name="dohandle"></param>
        public void UploadLocation(GisLastLocrecord poi, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "管控平台处理失败" };
            I_GIS_LAST_LOCRECORD record = ConvertToLC.ConvertLastPoi(poi);
            repository.UploadLocation(record, out dohandle);
            dohandle.IsSuccessful = true;
            dohandle.OperateMsg = "处理成功";
        }

        #endregion



        #region 微信获取订单明细

        /// <summary>
        /// 根据订单号获取订单明细
        /// </summary>
        /// <param name="CO_NUM"></param>
        /// <returns></returns>
        public List<LdmDisItem> GetByCoNum(string CO_NUM)
        {

            var result = repository.GetByCoNum(CO_NUM).Select(f => ConvertFromLC.ConvertDistItem(f)).ToList();
            return result;
        }

        #endregion

        #region 获取零售户位置信息

        /// <summary>
        /// 获取零售户位置信息
        /// </summary>
        /// <returns></returns>
        public IList<GisCustPois> GetCustPois()
        {
            var cust = repository.GetCustPois();
            var datas = cust.Select(f => ConvertFromLC.ConvertCustPoi(f)).ToList();
            return datas;

        }

        #endregion

    }
}
