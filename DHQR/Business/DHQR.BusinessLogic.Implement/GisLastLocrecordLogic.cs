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
    /// 送货员实时位置逻辑层
    /// </summary>
    public class GisLastLocrecordLogic : BaseLogic<GisLastLocrecord>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private GisLastLocrecordRepository GisLastLocrecordRep { get { return new GisLastLocrecordRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<GisLastLocrecord> Repository
        {
            get { return GisLastLocrecordRep; }
        }

        #region 送货员实时位置上传

        /// <summary>
        /// 送货员实时位置上传
        /// </summary>
        /// <param name="locRecord"></param>
        /// <param name="dohandle"></param>
        public void UploadLocation(GisLastLocrecord locRecord, out DoHandle dohandle)
        {
            LangchaoLogic lcLogic = new LangchaoLogic();
            lcLogic.UploadLocation(locRecord,out dohandle);
            if (dohandle.IsSuccessful)
            {
                GisLastLocrecordRep.UploadLocation(locRecord, out dohandle);
            }
        }

        /// <summary>
        /// 达州送货员实时位置上传
        /// </summary>
        /// <param name="locRecord"></param>
        /// <param name="dohandle"></param>
        public void UploadLocationDZ(GisLastLocrecord locRecord, out DoHandle dohandle)
        {
            //达州先注释浪潮数据
            //LangchaoLogic lcLogic = new LangchaoLogic();
            //lcLogic.UploadLocation(locRecord, out dohandle);
            //if (dohandle.IsSuccessful)
            //{
                GisLastLocrecordRep.UploadLocation(locRecord, out dohandle);
            //}
        }
        #endregion


        #region 位置查询

        /// <summary>
        /// 查询配送员最近位置
        /// </summary>
        /// <returns></returns>
        public IList<GisLastLocrecord> GetLatestGisInfos()
        {
            return GisLastLocrecordRep.GetLatestGisInfos();
        }


        /// <summary>
        /// 查询配送员线路坐标
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<GisLastLocrecord> GetGisInfosOfDlvman(GisLastLocrecordQueryParam queryParam)
        {
            return GisLastLocrecordRep.GetGisInfosOfDlvman(queryParam);
        }


        #endregion

    }
}
