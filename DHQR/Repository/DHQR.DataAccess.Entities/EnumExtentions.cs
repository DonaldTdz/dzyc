using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Base;
using Common.DAL.Entities;
using System.Runtime.Serialization;
using System.ComponentModel;


namespace DHQR.DataAccess.Entities
{
    /// <summary>
    ///配送单是否下载
    /// </summary>
    [DataContract]
    public enum IsDistDownload
    {
        /// <summary>
        /// 未下载
        /// </summary>
        [Description("未下载")]
        [EnumMember]
        NotDown = 0,

        /// <summary>
        /// 下载完成
        /// </summary>
        [Description("下载完成")]
        [EnumMember]
        Down = 1,

        /// <summary>
        /// 送货完成
        /// </summary>
        [Description("送货完成")]
        [EnumMember]
        DeliveryComplete = 3,

        /// <summary>
        /// 未启动下载
        /// </summary>
        [Description("未启动下载")]
        [EnumMember]
        NotStart = 9,

    }

    /// <summary>
    ///操作模式
    /// </summary>
    [DataContract]
    public enum OperateMode
    {
        /// <summary>
        /// 手工
        /// </summary>
        [Description("手工")]
        [EnumMember]
        Hand = 0,

        /// <summary>
        /// 刷卡
        /// </summary>
        [Description("刷卡")]
        [EnumMember]
        Card = 1,


    }

    /// <summary>
    /// 操作类型
    /// </summary>
    [DataContract]
    public struct OperationType
    {
        /// <summary>
        /// 下载完成
        /// </summary>
        public string downDistFinish { get { return "downDistFinish"; } }

        /// <summary>
        /// 备车
        /// </summary>
        public string prepareCar { get { return "prepareCar"; } }

        /// <summary>
        /// 开始装载
        /// </summary>
        public string startLoad { get { return "startLoad"; } }

        /// <summary>
        /// 完成装载
        /// </summary>
        public string finishLoad { get { return "finishLoad"; } }

        /// <summary>
        /// 车辆出库
        /// </summary>
        public string carOutWhse { get { return "carOutWhse"; } }

        /// <summary>
        /// 车辆入库
        /// </summary>
        public string carInWhse { get { return "carInWhse"; } }

        /// <summary>
        /// 完成送货
        /// </summary>
        public string missionFinish { get { return "missionFinish"; } }

        /// <summary>
        /// 中转下载完成
        /// </summary>
        public string downLnodeDistFinish { get { return "downLnodeDistFinish"; } }

        /// <summary>
        /// 中转送货完成
        /// </summary>
        public string lnodeMissionFinish { get { return "lnodeMissionFinish"; } }

        /// <summary>
        /// 撤销任务
        /// </summary>
        public string delDist { get { return "delDist"; } }

        /// <summary>
        /// 撤销中转任务
        /// </summary>
        public string delLnodeDist { get { return "delLnodeDist"; } }

        /// <summary>
        /// 暂停任务
        /// </summary>
        public string pauseMission { get { return "pauseMission"; } }

        /// <summary>
        /// 启动任务
        /// </summary>
        public string startMission { get { return "startMission"; } }

        /// <summary>
        /// 同步配送单
        /// </summary>
        public string syncDist { get { return "syncDist"; } }

    }
}
