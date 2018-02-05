using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.DAL.Entities;
using System.Runtime.Serialization;
using Common.Base;

namespace DHQR.DataAccess.Entities
{


    /// <summary>
    /// 消息反馈
    /// </summary>
    public class OperateResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 操作反馈消息
        /// </summary>
        public string OperateMsg { get; set; }


    }



    /// <summary>
    /// 配送单信息
    /// </summary>
    public class LdmInfo
    {
        /// <summary>
        /// 配送单抬头
        /// </summary>
        public List<LdmDist> LdmDists { get; set; }

        /// <summary>
        /// 配送单明细（订单信息）
        /// </summary>
        public List<LdmDistLine> LdmDistLines { get; set; }

        /// <summary>
        /// 配送单商品信息（订单明细）
        /// </summary>
        public List<LdmDisItem> LdmDisItems { get; set; }


    }

    /// <summary>
    /// 同步配送单信息返回
    /// </summary>
    public class SynLdmInfoData
    {
        /// <summary>
        /// 日志信息
        /// </summary>
        public List<LdmLog> Logs { get; set; }

        /// <summary>
        /// 订单信息
        /// </summary>
        public List<OrderInfo> OrderInfo { get; set; }

    }







    /// <summary>
    /// 上次文件返回信息
    /// </summary>
    [DataContract]
    public class FileupByteReturn : DoHandle
    {
        /// <summary>
        /// 文件ID
        /// </summary>
        [DataMember]
        public string FileId { get; set; }

    }



    /// <summary>
    /// 订单信息
    /// </summary>
    [DataContract]
    public class OrderInfo
    {
        /// <summary>
        /// 配送单号
        /// </summary>
        [DataMember]
        public string DIST_NUM { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        [DataMember]
        public string CO_NUM { get; set; }


        /// <summary>
        /// 是否处理
        /// </summary>
        [DataMember]
        public bool IS_FINISH { get; set; }

    }


    /// <summary>
    /// 操作日志
    /// </summary>
    [DataContract]
    public class LdmLog
    {
        /// <summary>
        /// 配送单号
        /// </summary>
        [DataMember]
        public string DIST_NUM { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        [DataMember]
        public string OPERATION_TYPE { get; set; }

    }

    /// <summary>
    /// 车辆运行同比增长率
    /// </summary>
    [DataContract]
    public class DistCarRunYearRate
    {
        /// <summary>
        /// 车辆ID
        /// </summary>
        public string CAR_ID { get; set; }

        /// <summary>
        /// 车辆名称
        /// </summary>
        public string CAR_NAME { get; set; }


        /// <summary>
        /// 年度
        /// </summary>
        public int YEAR { get; set; }


        /// <summary>
        /// 月份
        /// </summary>
        public int MONTH { get; set; }

        /// <summary>
        /// 同比增长率
        /// </summary>
        public decimal RATE { get; set; }
    }

    /// <summary>
    /// 车辆运行环比增长率
    /// </summary>
    [DataContract]
    public class DistCarRunLinkRate
    {
        /// <summary>
        /// 车辆ID
        /// </summary>
        public string CAR_ID { get; set; }

        /// <summary>
        /// 车辆名称
        /// </summary>
        public string CAR_NAME { get; set; }


        /// <summary>
        /// 年度
        /// </summary>
        public int YEAR { get; set; }


        /// <summary>
        /// 月份
        /// </summary>
        public int MONTH { get; set; }

        /// <summary>
        /// 环比增长率
        /// </summary>
        public decimal RATE { get; set; }

    }

    /// <summary>
    /// 不满意原因分析
    /// </summary>
    [DataContract]
    public class NotSatisReasonResult
    {
        /// <summary>
        /// 车辆ID
        /// </summary>
        public string CAR_ID { get; set; }

        /// <summary>
        /// 车辆名称
        /// </summary>
        public string CAR_NAME { get; set; }

        /// <summary>
        /// 不满意原因代码
        /// </summary>
        public string ReasonCode { get; set; }

        /// <summary>
        /// 不满意原因描述
        /// </summary>
        public string ReasonName { get; set; }

        /// <summary>
        /// 不满意原因数量
        /// </summary>
        public int Count { get; set; }

    }

    /// <summary>
    /// 上期里程
    /// </summary>
    [DataContract]
    public class PreMilData
    {
        /// <summary>
        /// 上期里程
        /// </summary>
        public decimal? PRE_MIL { get; set; }

    }

    /// <summary>
    /// NFC卡客户信息
    /// </summary>
    [DataContract]
    public class NfcCustomer
    {
        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        public Guid Id { get; set; }

        /// <summary>
        /// 客户编码
        /// </summary>
        [DataMember]
        public string CUST_ID { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        [DataMember]
        public string CUST_NAME { get; set; }

        /// <summary>
        /// 专卖证号
        /// </summary>
        [DataMember]
        public string LICENSE_CODE { get; set; }

        /// <summary>
        /// NFC卡ID
        /// </summary>
        [DataMember]
        public string CARD_ID { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        [DataMember]
        public string CARD_CODE { get; set; }



        /// <summary>
        /// 订货电话
        /// </summary>
        [DataMember]
        public string ORDER_TEL { get; set; }

        /// <summary>
        /// 经营地址
        /// </summary>
        [DataMember]
        public string BUSI_ADDR { get; set; }


        /// <summary>
        /// 是否成功
        /// </summary>
        [DataMember]
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// 操作反馈消息
        /// </summary>
        [DataMember]
        public string OperateMsg { get; set; }

    }

    /// <summary>
    /// 送货时间点记录
    /// </summary>
    [DataContract]
    public class TimeRecord
    {
        /// <summary>
        /// 车辆ID
        /// </summary>
        [DataMember]
        public string CAR_ID { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [DataMember]
        public string CAR_LICENSE { get; set; }

        /// <summary>
        /// 送货员ID
        /// </summary>
        [DataMember]
        public string DLVMAN_ID { get; set; }

        /// <summary>
        /// 送货员名称
        /// </summary>
        [DataMember]
        public string DLVMAN_NAME { get; set; }

        /// <summary>
        /// 驾驶员名称
        /// </summary>
        [DataMember]
        public string DRIVER_NAME { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        [DataMember]
        public string LOG_DATE { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [DataMember]
        public string LOG_TIME { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        [DataMember]
        public string OPERATION_TYPE { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [DataMember]
        public DateTime ExactTime { get; set; }



    }

    /// <summary>
    /// 送货时间点记录明细
    /// </summary>
    [DataContract]
    public class TimeRecordDetail
    {
        /// <summary>
        /// 车辆ID
        /// </summary>
        [DataMember]
        public string CAR_ID { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [DataMember]
        public string CAR_LICENSE { get; set; }

        /// <summary>
        /// 送货员ID
        /// </summary>
        [DataMember]
        public string DLVMAN_ID { get; set; }

        /// <summary>
        /// 送货员名称
        /// </summary>
        [DataMember]
        public string DLVMAN_NAME { get; set; }

        /// <summary>
        /// 驾驶员名称
        /// </summary>
        [DataMember]
        public string DRIVER_NAME { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        [DataMember]
        public string LOG_DATE { get; set; }


        /// <summary>
        /// 备车检查时间
        /// </summary>
        [DataMember]
        public string prepareCar { get; set; }

        /// <summary>
        /// 开始装车时间
        /// </summary>
        [DataMember]
        public string startLoad { get; set; }

        /// <summary>
        /// 装车结束时间
        /// </summary>
        [DataMember]
        public string finishLoad { get; set; }

        /// <summary>
        /// 车辆出库时间
        /// </summary>
        [DataMember]
        public string carOutWhse { get; set; }

        /// <summary>
        /// 车辆入库时间
        /// </summary>
        [DataMember]
        public string carInWhse { get; set; }

    }


    /// <summary>
    /// 满意度模型
    /// </summary>
    [DataContract]
    public class SatisfactionModel
    {
        /// <summary>
        /// 送货员ID
        /// </summary>
        [DataMember]
        public string DLVMAN_ID { get; set; }

        /// <summary>
        /// 送货员名称
        /// </summary>
        [DataMember]
        public string DLVMAN_NAME { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [DataMember]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [DataMember]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 非常满意数
        /// </summary>
        [DataMember]
        public int VerySatisfiedCount { get; set; }

        /// <summary>
        /// 非常满意比例
        /// </summary>
        [DataMember]
        public decimal VerySatisfiedRate { get; set; }

        /// <summary>
        /// 满意数
        /// </summary>
        [DataMember]
        public int SatisfiedCount { get; set; }

        /// <summary>
        /// 满意比例
        /// </summary>
        [DataMember]
        public decimal SatisfiedRate { get; set; }

        /// <summary>
        /// 一般数
        /// </summary>
        [DataMember]
        public int GeneralCount { get; set; }

        /// <summary>
        /// 一般比例
        /// </summary>
        [DataMember]
        public decimal GeneralRate { get; set; }

        /// <summary>
        /// 不满意数
        /// </summary>
        [DataMember]
        public int DissatisfiedCount { get; set; }

        /// <summary>
        /// 不满意比例
        /// </summary>
        [DataMember]
        public decimal DissatisfiedRate { get; set; }

        /// <summary>
        /// 非常不满意数
        /// </summary>
        [DataMember]
        public int VeryDissatisfiedCount { get; set; }

        /// <summary>
        /// 非常不满意比例
        /// </summary>
        [DataMember]
        public decimal VeryDissatisfiedRate { get; set; }

    }


    /// <summary>
    /// 微信群发图文集合
    /// </summary>
    public class WeiXinMassGroup
    {
        /// <summary>
        /// 多图文头
        /// </summary>
        public WeiXinMassMsg MsgHeader { get; set; }

        /// <summary>
        /// 订单信息
        /// </summary>
        public List<WeiXinMassMsg> MsgDetails { get; set; }

    }


    /// <summary>
    /// 车辆检查结果
    /// </summary>
    public class DistCarCheckResult
    {

        /// <summary>
        /// 检查类型
        /// </summary>
        [DataMember]
        public string CHECK_TYPE { get; set; }

        /// <summary>
        /// 检查类型描述
        /// </summary>
        [DataMember]
        public string CHECK_TYPE_DESC { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [DataMember]
        public string CAR_LICENSE { get; set; }

        /// <summary>
        /// 送货员名称
        /// </summary>
        [DataMember]
        public string DLVMAN_NAME { get; set; }

        /// <summary>
        /// 驾驶员名称
        /// </summary>
        [DataMember]
        public string DRIVER_NAME { get; set; }


        /// <summary>
        /// 是否一切正常
        /// </summary>
        [DataMember]
        public bool IsAllOk { get; set; }


        /// <summary>
        /// 异常明细
        /// </summary>
        [DataMember]
        public string ABNORMAL_DETAIL { get; set; }

    }


    /// <summary>
    /// 微信零售户年度总结
    /// </summary>
    public class WeiXinRetailerPro
    {
        /// <summary>
        /// 关注时间
        /// </summary>
        [DataMember]
        public string FstUseWXTime { get; set; }

        /// <summary>
        /// 绑定时间
        /// </summary>
        [DataMember]
        public string FstSnsTime { get; set; }


        /// <summary>
        /// 第X个关注
        /// </summary>
        [DataMember]
        public string WXRankNum { get; set; }

        /// <summary>
        /// 第一个送货员
        /// </summary>
        [DataMember]
        public string FstFriendNickName { get; set; }

        /// <summary>
        /// 第一个送货员头像
        /// </summary>
        [DataMember]
        public string FstFriendImg { get; set; }

        /// <summary>
        /// 订单数
        /// </summary>
        [DataMember]
        public string SnsNum { get; set; }


        /// <summary>
        /// 刷卡数
        /// </summary>
        [DataMember]
        public string RecRedEnvelope { get; set; }


        /// <summary>
        /// 评价数
        /// </summary>
        [DataMember]
        public string EvaluateNum { get; set; }

        /// <summary>
        /// 新认识送货员数
        /// </summary>
        [DataMember]
        public string AddFriendNum { get; set; }

        /// <summary>
        /// 非常满意数
        /// </summary>
        [DataMember]
        public string RecLike { get; set; }

    }


}
