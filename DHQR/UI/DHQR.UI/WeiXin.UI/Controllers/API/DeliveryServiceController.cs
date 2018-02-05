using Common.Base;
using DHQR.BusinessLogic.Implement;
using DHQR.DataAccess.Entities;
using DHQR.UI.Controllers.API.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DHQR.UI.Controllers.API
{
    public class DeliveryServiceController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        private readonly ServiceCallLogLogic serverlogLogic = new ServiceCallLogLogic();

        #region 下载配送单

        /// <summary>
        /// 下载配送单
        /// </summary>
        public JsonResult DownloadDist(string data)
        {
            var jser = new JavaScriptSerializer();
            var param = jser.Deserialize<DownloadDistParam>(data);
            DoHandle dohandle;
            #region 取浪潮配送单数据

            List<LdmDist> ldmDists = new List<LdmDist>();//配送单
            List<LdmDistLine> ldmDistLines = new List<LdmDistLine>();//配送单明细
            List<LdmDisItem> ldmDisItems = new List<LdmDisItem>();//配送单商品信息



            DZLangchaoLogic lcLogic = new DZLangchaoLogic();
            lcLogic.DownloadDists(param, out ldmDists, out ldmDistLines, out ldmDisItems);

            #endregion

            #region 写本地数据库

            LdmDistLogic ldmLogic = new LdmDistLogic();
            ldmLogic.DownloadDists(ldmDists, ldmDistLines, ldmDisItems, out dohandle);

            #endregion

            if (ldmDists.Count == 0)
            {
                dohandle.IsSuccessful = false;
                dohandle.OperateMsg = "未找到配送单,请与配送调度管理员联系!";
            }


            #region 写服务器日志

            serverlogLogic.InsertLog("DeliveryConfirm", "DownloadDist", data, param.DLVMAN_ID, dohandle.IsSuccessful);

            #endregion




            if (dohandle.IsSuccessful)
            {

                #region 返回配送单到终端

                LdmInfo ldmInfo = new LdmInfo { LdmDists = ldmDists, LdmDistLines = ldmDistLines, LdmDisItems = ldmDisItems };
                var result = jser.Serialize(ldmInfo);

                #endregion

               
                var jr = new JsonResult();
                jr.Data = result;
                return jr;
            }
            else
            {
                var result = jser.Serialize(dohandle);
                var jr = new JsonResult();
                jr.Data = result;
                return jr;
            }
        }

        #endregion
    }
}