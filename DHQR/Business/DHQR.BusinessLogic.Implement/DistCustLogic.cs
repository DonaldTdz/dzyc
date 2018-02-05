using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.BLL.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;
using Common.DAL.Implement;
using Common.Base;
using System.IO;

namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// 到货确认信息逻辑层
    /// </summary>
    public class DistCustLogic : BaseLogic<DistCust>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private DistCustRepository DistCustRep { get { return new DistCustRepository(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<DistCust> Repository
        {
            get { return DistCustRep; }
        }


        #region 到货确认

        /// <summary>
        /// 到货确认
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dohandle"></param>
        public void ConfirmDelivery(DistCust data, out DoHandle dohandle)
        {

            //浪潮写入数据
            LangchaoLogic lcLogic = new LangchaoLogic();
            lcLogic.ConfirmDelivery(data, out dohandle);
            dohandle = new DoHandle {IsSuccessful =true };
            LdmDistLineRepository lineRep = new LdmDistLineRepository(_baseDataEntities);
            if (dohandle.IsSuccessful)
            {
                //本地服务器写入数据
                DistCustRep.ConfirmDelivery(data, out dohandle);

                if (dohandle.IsSuccessful)
                {
                    try
                    {
                        WeiXinCommonApi wp = new WeiXinCommonApi();
                        WeiXinUserRep wuerRep = new WeiXinUserRep(_baseDataEntities);
                        WeiXinAppRep appRep = new WeiXinAppRep(_baseDataEntities);
                        Guid appId = Guid.Parse("0CD98D1B-C476-4A02-B4B9-0F619FCCD36F");
                        WeiXinApp weiXinApp = appRep.GetByKey(appId);

                        /*
                        IList<WeiXinUser> weiXinUsers = lineRep.GetToSendUsers(data.DIST_NUM, data.CO_NUM);

                        string msg = "";
                        foreach (var item in weiXinUsers)
                        {
                            wp.SendOrderTmp(weiXinApp, item, item.CO_NUM, out msg);
                        }
                        
                        var weiXinUser = lineRep.GetToSendUser(data.DIST_NUM, data.CO_NUM);
                        if (weiXinUser != null)
                        {
                            string msg = "";
                            wp.SendOrderTmp(weiXinApp, weiXinUser, weiXinUser.CO_NUM, out msg);
                        }
                         */
                        var weiXinUsers = lineRep.GetNextToSendUsers(data.DIST_NUM, data.CO_NUM);
                        if (weiXinUsers .Count>0)
                        {
                            foreach (var wx in weiXinUsers)
                            {
                                string msg = "";
                                wp.SendOrderTmp(weiXinApp, wx, wx.CO_NUM, out msg);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        DoHandle exDoHandle;
                        ExceptionLogLogic lc = new ExceptionLogLogic();
                        ExceptionLog log = new ExceptionLog
                        {
                            Id = Guid.NewGuid(),
                            Message = ex.Message,
                            InnerException = ex.InnerException == null ? " " : ex.InnerException.Message,
                            Ip = data.DIST_NUM+","+data.CO_NUM,
                            CreateTime = DateTime.Now,
                            UserName = "System"
                        };
                        lc.Create(log, out exDoHandle);
                    }
                }


            }
        }



        /// <summary>
        /// 手工到货确认
        /// </summary>
        /// <param name="CO_NUM"></param>
        /// <param name="dohandle"></param>
        public void HandleConfirm(string CO_NUM,string DIST_NUM, out DoHandle dohandle)
        {
            LdmDistLineLogic lineLogic = new LdmDistLineLogic();
            LdmDist dist;
            var coData = lineLogic.GetByCoNum(CO_NUM,  DIST_NUM,out dist);
            DistCust distCust = new DistCust 
            {
                Id=Guid.NewGuid(),
                DIST_NUM=dist.DIST_NUM,
                CO_NUM=CO_NUM,
                CUST_ID=coData.CUST_ID,
                IS_RECEIVED="01",//已送达
                DIST_SATIS="10",//非常满意
                UNLOAD_REASON="20",//正常卸货
                REC_DATE = DateTime.Now.ToString("yyyyMMdd"),
                REC_ARRIVE_TIME = DateTime.Now.ToString("yyyyMMddHHmmss"),
                REC_LEAVE_TIME = DateTime.Now.ToString("yyyyMMddHHmmss"),
                HANDOVER_TIME =30,
                EVALUATE_INFO =string.Empty,
                SIGN_TYPE ="3",
                EXP_SIGN_REASON =string.Empty,
                UNLOAD_LON =null,
                UNLOAD_LAT =null,
                UNLOAD_DISTANCE =0,
                EVALUATE_TIME = DateTime.Now.ToString("yyyyMMddHHmmss"),
                DLVMAN_ID =dist.DLVMAN_ID
            };

            //浪潮写入数据
            LangchaoLogic lcLogic = new LangchaoLogic();
            lcLogic.ConfirmDelivery(distCust, out dohandle);
            dohandle = new DoHandle { IsSuccessful=true};
            LdmDistLineRepository lineRep = new LdmDistLineRepository(_baseDataEntities);
            if (dohandle.IsSuccessful)
            {
                //本地服务器写入数据
                DistCustRep.ConfirmDelivery(distCust, out dohandle);
            }
        }


        /// <summary>
        /// 记录bug，以便调试
        /// </summary>
        /// <returns></returns>
        public void Writebug(string msg1, string msg2)
        {
            DistCustRep.WriteBug(msg1, msg2);
        }

        #endregion


        #region 满意度分析


        /// <summary>
        /// 获取单个配送员每天满意度信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<DistCust> GetSatisfyByDayData(DistCustByDayQueryParam queryParam)
        {
            return DistCustRep.GetSatisfyByDayData(queryParam);
        }


        /// <summary>
        /// 获取配送满意度
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<DistCust> GetSatisfyDatas(SatisfyQueryParam queryParam)
        {
            return DistCustRep.GetSatisfyDatas(queryParam);
        }


        /// <summary>
        /// 获取不满意原因信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<NotSatisReasonResult> GetNotSatisReasons(SatisfyQueryParam queryParam)
        {
            return DistCustRep.GetNotSatisReasons(queryParam);
        }

        /// <summary>
        /// 查询满意度信息
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public IList<SatisfactionModel> QuerySatisfaction(SatisfyQueryParam queryParam)
        {
            return DistCustRep.QuerySatisfaction(queryParam);
        }

        #endregion

        #region 微信通知

        /// <summary>
        /// 微信通知客户
        /// </summary>
        /// <param name="data"></param>
        /// <param name="dohandle"></param>
        public void WeiXinNotify(WeiXinNotifyParam data, out DoHandle dohandle)
        {
            LdmDistLineRepository lineRep = new LdmDistLineRepository(_baseDataEntities);
            WeiXinUserRep wuerRep = new WeiXinUserRep(_baseDataEntities);
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "操作失败" };
            try
            {

                //微信通知
                //查找当前零售户
                var currentData = lineRep.GetCurrentData(data.DIST_NUM, data.CO_NUM);
                if (currentData != null)
                {

                    Guid wppId = Guid.Parse("0CD98D1B-C476-4A02-B4B9-0F619FCCD36F");
                    WeiXinUser wuser = wuerRep.GetBySysName(currentData.LICENSE_CODE, wppId);
                    if (wuser != null)
                    {
                        string msg = "";
                        wuser.RecTime = currentData.RecTime;
                        wuser.Address = currentData.ADDR;
                        wuser.Money = currentData.PAY_TYPE == "10" ? (currentData.AMT_AR - currentData.AMT_OR) : 0;
                        WeiXinCommonApi cpi = new WeiXinCommonApi();
                        var ss = cpi.ResponseMsgTemplate(wppId, wuser, out msg);
                    }

                }

                dohandle.IsSuccessful = true;
                dohandle.OperateMsg = "操作成功";
            }
            catch (Exception ex)
            {
                string innerEp = ex.InnerException != null ? ex.InnerException.Message : "";
                Writebug(ex.Message, innerEp);
                dohandle.IsSuccessful = false;
                dohandle.OperateMsg = ex.Message;
            }

        }

        /// <summary>
        /// 送货开始微信通知
        /// </summary>
        /// <param name="distRecord"></param>
        public void SendWeiXinStartMsg(DistRecordLog distRecord)
        {

        }



        /// <summary>
        /// 送货结束微信通知
        /// </summary>
        /// <param name="distRecord"></param>
        public void SendWeiXinEndMsg(DistRecordLog distRecord)
        {

        }
        #endregion

        #region 出门后通知

        /// <summary>
        /// 出门后微信通知客户
        /// </summary>
        /// <param name="DIST_NUM"></param>
        /// <param name="dohandle"></param>
        public void SendWxMsgAfterOut(string DIST_NUM)
        {
            LdmDistLineRepository lineRep = new LdmDistLineRepository(_baseDataEntities);
            try
            {
                WeiXinCommonApi wp = new WeiXinCommonApi();
                WeiXinUserRep wuerRep = new WeiXinUserRep(_baseDataEntities);
                WeiXinAppRep appRep = new WeiXinAppRep(_baseDataEntities);
                Guid appId = Guid.Parse("0CD98D1B-C476-4A02-B4B9-0F619FCCD36F");
                WeiXinApp weiXinApp = appRep.GetByKey(appId);

                IList<WeiXinUser> weiXinUsers = lineRep.GetToSendUsersAfterOut(DIST_NUM);

                string msg = "";
                foreach (var item in weiXinUsers)
                {
                    wp.SendOrderTmp(weiXinApp, item, item.CO_NUM, out msg);
                }

            }
            catch (Exception ex)
            {
                DoHandle exDoHandle;
                ExceptionLogLogic lc = new ExceptionLogLogic();
                ExceptionLog log = new ExceptionLog
                {
                    Id = Guid.NewGuid(),
                    Message = ex.Message,
                    InnerException = ex.InnerException == null ? " " : ex.InnerException.Message,
                    Ip = " ",
                    CreateTime = DateTime.Now,
                    UserName = "System"
                };
                lc.Create(log, out exDoHandle);
            }

        }

        #endregion

    }
}
