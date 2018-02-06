using DHQR.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DHQR.DataAccess.Langchao
{
    public class DZLangchaoDB2Repository
    {
        #region 访问达州数据库测试

        public List<DZ_I_DIST> GetDists(DownloadDistParam param)
        {
            IDictionary<string, string> dic1 = new Dictionary<string, string>();
            dic1.Add("DLVMAN_ID", param.DLVMAN_ID);
            var ldmDists = new DB2Helper<DZ_I_DIST>().QueryData(dic1);
            return ldmDists;
        }

        #endregion

        #region 同步零售户

        /// <summary>
        /// 获取零售户信息
        /// </summary>
        /// <param name="COM_ID">公司ID</param>
        /// <returns></returns>
        public List<DZ_I_CUST> GetCustomer(string COM_ID)
        {
            IDictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("COM_ID", COM_ID);
            //dic.Add("STATUS", "01");
            //dic.Add("STATUS", "02");
            var result = new DB2Helper<DZ_I_CUST>().QueryData(dic);
            return result;
        }


        #endregion

        #region 同步配送员信息

        /// <summary>
        /// 同步配送员信息
        /// </summary>
        /// <param name="USER_ID">送货员账号ID</param>
        /// <returns></returns>
        public List<DZ_I_DIST_DLVMAN> GetDistDlvmans(string USER_ID)
        {
            //IDictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("COM_ID", COM_ID);
            //dic.Add("USER_ID", USER_ID);
            var result = new DB2Helper<DZ_I_DIST_DLVMAN>().GetAll();
            return result;

        }

        #endregion

        #region 同步线路信息

        /// <summary>
        /// 同步线路信息
        /// </summary>
        /// <param name="COM_ID">公司ID</param>
        /// <returns></returns>
        public List<DZ_I_DIST_RUT> GetRutList(string COM_ID)
        {
            //IDictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("COM_ID", COM_ID);
            var result = new DB2Helper<DZ_I_DIST_RUT>().GetAll();
            return result;

        }

        #endregion

        #region 同步车辆和配送员信息

        /// <summary>
        /// 获取所有配送车辆
        /// </summary>
        /// <param name="COM_ID">公司ID</param>
        /// <returns></returns>
        public List<DZ_I_DIST_CAR> GetDistCar(string COM_ID)
        {
            //IDictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("COM_ID", COM_ID);
            var result = new DB2Helper<DZ_I_DIST_CAR>().GetAll();
            return result;

        }

        #endregion

        #region 下载配送单

        /// <summary>
        /// 从浪潮下载配送单信息
        /// </summary>
        /// <param name="param"></param>
        /// <param name="ldmDists"></param>
        /// <param name="ldmDistLines"></param>
        /// <param name="ldmDisItems"></param>
        public void DownloadDists(DownloadDistParam param, int distDate, out List<DZ_I_DIST> ldmDists, out List<DZ_I_DIST_LINE> ldmDistLines, out List<DZ_I_DIST_ITME> ldmDisItems)
        {
            //当天时间字符串
            string currentDate = DateTime.Now.AddDays(-1).AddDays(-distDate).ToString("yyyyMMdd");


            IDictionary<string, string> dic1 = new Dictionary<string, string>();
            IDictionary<string, IList<string>> dic2 = new Dictionary<string, IList<string>>();
            IDictionary<string, IList<string>> dic3 = new Dictionary<string, IList<string>>();
            dic1.Add("DLVMAN_ID", param.DLVMAN_ID);
            dic1.Add("DIST_DATE", currentDate);

            ldmDists = new DB2Helper<DZ_I_DIST>().QueryData(dic1);

            if (ldmDists.Count != 0)
            {
                IList<string> distNums = ldmDists.Select(f => f.DIST_NUM).ToList();

                dic2.Add("DIST_NUM", distNums);

                ldmDistLines = new DB2Helper<DZ_I_DIST_LINE>().QueryData(new Dictionary<string, string>(), dic2);
                IList<string> coNums = ldmDistLines.Select(f => f.CO_NUM).ToList();

                dic3.Add("CO_NUM", coNums);
                // ldmDisItems = new List<I_DIST_ITEM>();
                ldmDisItems = new DB2Helper<DZ_I_DIST_ITME>().QueryData(new Dictionary<string, string>(), dic3);
            }
            else
            {
                ldmDistLines = new List<DZ_I_DIST_LINE>();
                ldmDisItems = new List<DZ_I_DIST_ITME>();
            }
        }

        #endregion
    }
}
