using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.BLL.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;
using Common.DAL.Implement;
using Common.Base;

namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// 车辆检查信息逻辑层
    /// </summary>
    public class DistCarCheckLogic : BaseLogic<DistCarCheck>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private DistCarCheckRepository DistCarCheckRep { get { return new DistCarCheckRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<DistCarCheck> Repository
        {
            get { return DistCarCheckRep; }
        }

        /// <summary>
        /// 备车检查
        /// </summary>
        /// <param name="checkInfo"></param>
        /// <param name="dohandle"></param>
        public void CheckCar(List<DistCarCheck> checkInfo, out DoHandle dohandle)
        {
            OperationType operateType;
            var logKey = new LogKeyLogic().GetLogkey();
            var firstChek = checkInfo.FirstOrDefault();
            DistRecordLog log = new DistRecordLog
            {
                LOG_SEQ = logKey,
                REF_TYPE = firstChek.REF_TYPE,
                REF_ID = firstChek.REF_ID,
                OPERATION_TYPE = operateType.prepareCar,
                LOG_DATE = DateTime.Now.ToString("yyyyMMdd"),
                LOG_TIME = DateTime.Now.ToString("HHmmss"),
                USER_ID = firstChek.USER_ID,
                LONGITUDE = firstChek.LONGITUDE,
                LATITUDE = firstChek.LATITUDE,
                NOTE = operateType.prepareCar,
                OPERATE_MODE = "0"
            };
            CarCheckKeyLogic carKeyLogic = new CarCheckKeyLogic();
            foreach (var item in checkInfo)
            {
                item.CHECK_ID = carKeyLogic.GetLogkey();
                item.CHECK_TIME = DateTime.Now.ToString("yyyyMMddHHmmss");
            }

            //写浪潮数据库
            LangchaoLogic lcLogic = new LangchaoLogic();
            lcLogic.CheckCar(checkInfo, log, out dohandle);

            if (dohandle.IsSuccessful)
            {
                DistCarCheckRep.CheckCar(checkInfo, log, out dohandle);
            }
        }

        #region 查询车辆检查信息

        /// <summary>
        /// 查询车辆检查信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<DistCarCheckResult> GetCarCheckResult(DistCarCheckQueryParam queryParam)
        {
            return DistCarCheckRep.GetCarCheckResult(queryParam);
        }

        #endregion
    }
}
