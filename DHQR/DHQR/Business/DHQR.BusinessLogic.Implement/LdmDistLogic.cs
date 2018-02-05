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
    /// 配送任务单逻辑层
    /// </summary>
    public class LdmDistLogic : BaseLogic<LdmDist>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private LdmDistRepository LdmDistRep { get { return new LdmDistRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<LdmDist> Repository
        {
            get { return LdmDistRep; }
        }

        #region 下载配送单信息,写入数据库

        /// <summary>
        /// 下载配送单信息,写入数据库
        /// </summary>
        /// <param name="ldmDists"></param>
        /// <param name="ldmDistLines"></param>
        /// <param name="ldmDisItems"></param>
        /// <param name="dohandle"></param>
        public void DownloadDists(List<LdmDist> ldmDists, List<LdmDistLine> ldmDistLines, List<LdmDisItem> ldmDisItems, out DoHandle dohandle)
        {
            LdmDistRep.DownloadDists(ldmDists, ldmDistLines, ldmDisItems, out dohandle);
        }

        /// <summary>
        /// 下载配送单信息,写入数据库
        /// </summary>
        /// <param name="ldmDists"></param>
        /// <param name="ldmDistLines"></param>
        /// <param name="ldmDisItems"></param>
        /// <param name="dohandle"></param>
        public void DownloadDistsByDate(List<LdmDist> ldmDists, List<LdmDistLine> ldmDistLines, List<LdmDisItem> ldmDisItems, List<I_CO_TEMP_RETURN> coTmpReturns, out DoHandle dohandle)
        {
            var coTmps = coTmpReturns.Select(f => ConvertFromLC.ConvertCoTempReturn(f)).ToList();
            LdmDistRep.DownloadDistsByDate(ldmDists, ldmDistLines, ldmDisItems, coTmps, out dohandle);
        }


        #endregion

        #region 配送单下载到终端完成

        /// <summary>
        /// 配送单下载到终端完成
        /// </summary>
        /// <param name="distNums"></param>
        /// <param name="dohandle"></param>
        public void DownDistFinish(List<string> distNums, out DoHandle dohandle)
        {

            LdmDistRep.DownDistFinish(distNums, out dohandle);
        }

        #endregion


        #region 一键上传

        /// <summary>
        /// 一键上传
        /// </summary>
        /// <param name="param"></param>
        /// <param name="dohandle"></param>
        public void BatchAddedUpload(BatchAddedUploadParam param, out DoHandle dohandle)
        {
            LdmDistRep.BatchAddedUpload(param, out dohandle);
        }

        #endregion


        #region 配送任务完成率

        /// <summary>
        /// 查询配送任务完成率s
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<LdmDistFinishRate> GetLdmDistFinishRate(LdmFinishQueryParam queryParam)
        {
            return LdmDistRep.GetLdmDistFinishRate(queryParam);
        }

        #endregion

        /// <summary>
        /// 根据线路ID和时间获取配送单
        /// </summary>
        /// <param name="rutId"></param>
        /// <param name="distDate"></param>
        /// <returns></returns>
        public LdmDist GetDistByRutAndDate(string rutId, string distDate)
        {
            var result = LdmDistRep.GetDistByRutAndDate(rutId, distDate);
            return result; 
        }

        
        /// <summary>
        /// 查询配送单信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public PagedResults<LdmDist> QueryLdmDist(LdmDistQueryParam queryParam)
        {
            return LdmDistRep.QueryLdmDist(queryParam);
        }

        /// <summary>
        /// 当日配送单信息
        /// </summary>
        /// <param name="distCount"></param>
        /// <param name="finishCount"></param>
        /// <param name="notfinishCount"></param>
        /// <param name="totalMoney"></param>
        public void GetLdmDistInfo(out int distCount, out int finishCount, out int notfinishCount, out decimal totalMoney)
        {
            LdmDistRep.GetLdmDistInfo(out distCount, out finishCount, out notfinishCount, out totalMoney);
        }

    }
}
