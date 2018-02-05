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
    ///项目状态
    /// </summary>
    [DataContract]
    public enum ProjectStatus
    {
        /// <summary>
        /// 草稿
        /// </summary>
        [Description("新增")]
        [EnumMember]
        Draft = 0,

        /// <summary>
        /// 进行中
        /// </summary>
        [Description("进行中")]
        [EnumMember]
        Processing = 1,

        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        [EnumMember]
        Complete = 2,

        /// <summary>
        /// 终止
        /// </summary>
        [Description("终止")]
        [EnumMember]
        Absolute = 3,

    }

    /// <summary>
    ///项目节点状态
    /// </summary>
    [DataContract]
    public enum PointStatus
    {
        /// <summary>
        /// 草稿
        /// </summary>
        [Description("新增")]
        [EnumMember]
        NotDo = 0,

        /// <summary>
        /// 进行中
        /// </summary>
        [Description("进行中")]
        [EnumMember]
        Doing = 1,

        /// <summary>
        /// 已完成
        /// </summary>
        [Description("已完成")]
        [EnumMember]
        Done = 2,
    }


    /// <summary>
    ///区域类型
    /// </summary>
    [DataContract]
    public enum RegionType
    {
        /// <summary>
        /// 国家
        /// </summary>
        [Description("国家")]
        [EnumMember]
        Country = 0,

        /// <summary>
        /// 省
        /// </summary>
        [Description("省")]
        [EnumMember]
        Province = 1,

        /// <summary>
        /// 市
        /// </summary>
        [Description("市")]
        [EnumMember]
        City = 2,

        /// <summary>
        /// 县
        /// </summary>
        [Description("县")]
        [EnumMember]
        County = 3,
        
        /// <summary>
        /// 乡镇
        /// </summary>
        [Description("乡镇")]
        [EnumMember]
        Village = 4,



    }


    /// <summary>
    ///用户申请状态
    /// </summary>
    [DataContract]
    public enum UserApplyStatus
    {
        /// <summary>
        /// 未处理
        /// </summary>
        [Description("未处理")]
        [EnumMember]
        Initial = 0,

        /// <summary>
        /// 审批通过
        /// </summary>
        [Description("审批通过")]
        [EnumMember]
        ApproveYes = 1,

        /// <summary>
        /// 审批不通过
        /// </summary>
        [Description("审批不通过")]
        [EnumMember]
        ApproveNo = 2

    }

    /// <summary>
    ///关键字申请状态
    /// </summary>
    [DataContract]
    public enum WeiboKeyWordApplyStatus
    {
        /// <summary>
        /// 未处理
        /// </summary>
        [Description("未处理")]
        [EnumMember]
        Initial = 0,

        /// <summary>
        /// 审批通过
        /// </summary>
        [Description("审批通过")]
        [EnumMember]
        ApproveYes = 1,

        /// <summary>
        /// 审批不通过
        /// </summary>
        [Description("审批不通过")]
        [EnumMember]
        ApproveNo = 2

    }

    /// <summary>
    ///关键字申请状态
    /// </summary>
    [DataContract]
    public enum ResetPswapllyStatus
    {
        /// <summary>
        /// 未处理
        /// </summary>
        [Description("未处理")]
        [EnumMember]
        Initial = 0,

        /// <summary>
        /// 已处理
        /// </summary>
        [Description("已处理")]
        [EnumMember]
        ApproveYes = 1,

        /// <summary>
        /// 拒绝处理
        /// </summary>
        [Description("拒绝处理")]
        [EnumMember]
        ApproveNo = 2

    }


    /// <summary>
    ///角色类型
    /// </summary>
    [DataContract]
    public enum RoleTypeEnum
    {
        /// <summary>
        /// 超级管理员
        /// </summary>
        [Description("超级管理员")]
        [EnumMember]
        SuperAdmin=0,

        /// <summary>
        /// DHQR管理员
        /// </summary>
        [Description("DHQR管理员")]
        [EnumMember]
        Admin=1,

        /// <summary>
        /// 公司领导
        /// </summary>
        [Description("公司领导")]
        [EnumMember]
        Leader=2,

        /// <summary>
        /// 项目经理
        /// </summary>
        [Description("项目经理")]
        [EnumMember]
        XMJL=3,

        /// <summary>
        /// 项目成员
        /// </summary>
        [Description("项目成员")]
        [EnumMember]
        XMCY=4

    }

}
