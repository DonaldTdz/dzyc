using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DHQR.DataAccess.Entities
{
    /// <summary>
    /// 浪潮参数基础类
    /// </summary>
    public class LangchaoBaseParam
    {

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CAR_LICENSE { set; get; }

        /// <summary>
        /// 车辆ID
        /// </summary>
        public string CAR_ID { set; get; }

        /// <summary>
        /// 驾驶员ID
        /// </summary>
        public string DRIVER_ID { set; get; }

        /// <summary>
        /// 配送员ID
        /// </summary>
        public string DLVMAN_ID { set; get; }


    }

    #region 登录参数

    /// <summary>
    /// 登录参数
    /// </summary>
    public class LoginParam 
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { set; get; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Psw { set; get; }


    }

    #endregion

    #region 修改登录密码参数

    /// <summary>
    /// 修改登录密码参数
    /// </summary>
    public class ChageLoginPswParam
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { set; get; }

        /// <summary>
        /// 原密码
        /// </summary>
        public string OriginalPsw { set; get; }


        /// <summary>
        /// 新密码
        /// </summary>
        public string Psw { set; get; }


    }

    #endregion

    #region 下载配送单参数

    /// <summary>
    /// 下载配送单参数
    /// </summary>
    public class DownloadDistParam: LangchaoBaseParam
    {
      
    }

    #endregion

    #region 下载配送单参数（按日期）

    /// <summary>
    /// 下载配送单参数
    /// </summary>
    public class DownloadDistByDateParam : LangchaoBaseParam
    {
        /// <summary>
        /// 配送单日期
        /// </summary>
        public List<string> DistDate { set; get; }

    }

    #endregion

    #region 下载配送单完成参数

    /// <summary>
    /// 下载配送单完成参数
    /// </summary>
    public class DownDistFinish:DistRecordLog
    {
        /// <summary>
        /// 配送单号集合
        /// </summary>
        public List<string> DistNums { set; get; }

       

    }

    #endregion

    #region 备车检查

    /// <summary>
    /// 备车检查参数
    /// </summary>
    public class CheckCarParam 
    {

        /// <summary>
        /// 备车检查信息
        /// </summary>
        public List<DistCarCheck> CheckDatas { set; get; }

    }

    #endregion

    #region 装车开始

    /// <summary>
    /// 装车开始参数
    /// </summary>
    public class StartLoadParam : DistRecordLog
    {


    }

    #endregion

    #region 装车结束

    /// <summary>
    /// 装车结束参数
    /// </summary>
    public class FinishLoadParam : DistRecordLog
    {
    }

    #endregion

    #region 车辆出库

    /// <summary>
    /// 车辆出库参数
    /// </summary>
    public class CarOutWhseParam : DistRecordLog
    {

    }

    #endregion

    #region 送货任务开始

    /// <summary>
    /// 送货任务开始参数
    /// </summary>
    public class MissionStartParam : DistRecordLog
    {
    }

    #endregion

    #region 到货确认

    /// <summary>
    /// 到货确认参数
    /// </summary>
    public class ConfirmDeliveryParam : DistCust
    {

    }

    #endregion

    #region 退货

    /// <summary>
    /// 整单退货参数
    /// </summary>
    public class ReturnAllOrderParam:DistCust
    {

    }

    /// <summary>
    /// 部分退货参数
    /// </summary>
    public class ReturnPatialOrderParam 
    {
        /// <summary>
        /// 到货确认信息
        /// </summary>
        public DistCust Cust { set; get; }

        /// <summary>
        /// 退货的订单
        /// </summary>
        public CoReturn Order { set; get; }

        /// <summary>
        /// 退货的订单明细
        /// </summary>
        public List<CoReturnLine> OrderDetails { set; get; }
    }

    #endregion

    #region 车辆入库

    /// <summary>
    /// 车辆入库参数
    /// </summary>
    public class CarInWhseParam : DistRecordLog
    {
    }

    #endregion

    #region 回程登记

    /// <summary>
    /// 回程登记参数
    /// </summary>
    public class BackRegistParam
    {

        /// <summary>
        /// 车辆运行信息抬头
        /// </summary>
        public DistCarRun CarRun {set;get; }

        /// <summary>
        /// 车辆运行信息明细
        /// </summary>
        public List<DistCarRunLine> CarRunLine { set; get; }

    }

    #endregion

    #region 批量回传

    /// <summary>
    /// 批量回传参数
    /// </summary>
    public class BatchAddedUploadParam
    {

        /// <summary>
        /// 业务类型（1-客户位置 2-送货员日志 3-到货确认信息 4-退货信息）
        /// </summary>
        public string BatchType { get; set; }

        /// <summary>
        /// 时间点业务状态（装车开始、装车结束、车辆出库、送货任务开始、车辆入库）
        /// </summary>
        public List<DistRecordLog> RecordLog { get; set; }

        /// <summary>
        /// 到货确认信息
        /// </summary>
        public List<DistCust> DistCust { get; set; }

        /// <summary>
        /// 退货信息
        /// </summary>
        public List<ReturnPatialOrderParam> ReturnOrder { get; set; }

        /// <summary>
        /// 客户位置信息
        /// </summary>
        public List<GisCustPois> GisCustPois { get; set; }
    }

    #endregion

    #region 零售户位置采集

    /// <summary>
    /// 零售户位置采集参数
    /// </summary>
    public class CollectRetailerXYParam:GisCustPois
    { 

    }

    /// <summary>
    /// 获取零售户位置参数
    /// </summary>
    public class GetRetailerLocationParam
    {
        /// <summary>
        /// 零售户ID集合（为空取全部）
        /// </summary>
        public List<string> CustIds { get; set; }

    }

    #endregion

    #region 位置上报

    /// <summary>
    /// 位置上报参数
    /// </summary>
    public class XyUploadParam : LangchaoBaseParam
    {

        /// <summary>
        /// 新商盟公司编码
        /// </summary>
        public string xComId { set; get; }

        /// <summary>
        /// 公司编码(移动人员所属的公司)
        /// </summary>
        public string comId { set; get; }

        /// <summary>
        /// 人员类型:61市管员,62稽查员,63客户经理,65送货员
        /// </summary>
        public string organType { set; get; }

        /// <summary>
        /// 人员编号
        /// </summary>
        public string userCode { set; get; }

        /// <summary>
        /// 地理位置信息
        /// </summary>
        public IList<LocationInfo> infoList { set; get; }
    }

    /// <summary>
    /// 位置信息
    /// </summary>
    public class LocationInfo
    {
        /// <summary>
        /// 卫星时间
        /// 如：20120523140057
        /// </summary>
        public string gpsTime { set; get; }

        /// <summary>
        /// 经度
        /// </summary>
        public string x { set; get; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string y { set; get; }

        /// <summary>
        /// 高度
        /// </summary>
        public string h { set; get; }

        /// <summary>
        /// 速度
        /// </summary>
        public string speed { set; get; }

        /// <summary>
        /// 方向
        /// 如：120
        /// </summary>
        public string direction { set; get; }

        /// <summary>
        /// 卫星数
        /// 如：4
        /// </summary>
        public string statlliteNum { set; get; }

    }

    #endregion

    #region 零售户收货密码修改

    public class ChangeDeliveryPswParam
    {
        /// <summary>
        /// 零售户ID
        /// </summary>
        public string CUST_ID { get; set; }

        /// <summary>
        /// 原密码
        /// </summary>
        public string OriginalPsw { set; get; }


        /// <summary>
        /// 新密码
        /// </summary>
        public string Psw { set; get; }
    }

    #endregion

    #region 配送附件信息

    /// <summary>
    /// 上传附件信息参数
    /// </summary>
    public class UploadDistFileParam : LangchaoBaseParam
    {

        /// <summary>
        /// 配送单号
        /// </summary>
        public string distNum { set; get; }


        /// <summary>
        /// 零售户编码
        /// 如：CD45655
        /// </summary>
        public string custId { set; get; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string coNum { set; get; }

        /// <summary>
        /// 附件类型
        /// 0:照片,1:录音,2:视频,3:文档
        /// </summary>
        public string fileType { set; get; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string fileName { set; get; }

        /// <summary>
        /// 已上传附件的编码，用于重复上传时删除旧的附件
        /// </summary>
        public string oldDocId { set; get; }


    }

    #endregion

    #region 配送附件信息

    /// <summary>
    /// 删除附件信息参数
    /// </summary>
    public class DeleteDistFileParam : LangchaoBaseParam
    {
        /// <summary>
        /// 附件的编码
        /// </summary>
        public string docId { set; get; }
    }

    #endregion

    #region 上传附件参数

    /// <summary>
    /// 上传附件参数
    /// </summary>
    public class UpfileByteParam 
    {
        /// <summary>
        /// 配送员ID
        /// </summary>
        public string DLVMAN_ID { get; set; }

        /// <summary>
        /// 配送单号
        /// </summary>
        public string DIST_NUM { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string CO_NUM { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FILE_NAME { get; set; }

        /// <summary>
        /// 文件后缀名
        /// </summary>
        public string FILE_TYPE { get; set; }

        ///// <summary>
        ///// 文件字节流信息
        ///// </summary>
        //public IList<byte> FileByte { get; set; }
    }



    #endregion

    #region 下载附件参数

    /// <summary>
    /// 上传附件参数
    /// </summary>
    public class DownfuleByteParam
    {
        /// <summary>
        /// 附件ID
        /// </summary>
        public Guid Id { get; set; }

    }



    #endregion

    #region  订单明细同步参数

    public class SynOrderParam
    {
        /// <summary>
        /// 配送单号
        /// </summary>
        public string DIST_NUM { get; set; }
    }


    #endregion


    #region 获取车辆上期里程

    /// <summary>
    /// 获取车辆上期里程参数
    /// </summary>
    public class GetPreMileParam 
    {
        /// <summary>
        /// 车辆ID
        /// </summary>
        public string CAR_ID { get; set; }

    }

    #endregion

    #region 微信通知参数

    /// <summary>
    /// 获取车辆上期里程参数
    /// </summary>
    public class WeiXinNotifyParam
    {

        /// <summary>
        /// 配送单号
        /// </summary>
        public string DIST_NUM { get; set; }


        /// <summary>
        /// 订单号
        /// </summary>
        public string CO_NUM { get; set; }

    }

    #endregion

    #region  配送单日志同步参数

    public class SynLdmDistParam
    {
        /// <summary>
        /// 配送单号
        /// </summary>
        public string DIST_NUM { get; set; }
    }


    #endregion


    #region  NFC刷卡客户参数

    public class GetNfcCustomerParam
    {
        /// <summary>
        /// NFC卡号
        /// </summary>
        public string CARD_ID { get; set; }
    }


    #endregion




}
