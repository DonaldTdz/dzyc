using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Base;
using System.Runtime.Serialization;

namespace DHQR.DataAccess.Entities
{
    /// <summary>
    ///  项目主表查询类
    /// </summary>
    [DataContract(IsReference=true)]
    public class ProjectMasterQueryParam : QueryParam
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        [DataMember]
        public string Code { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [DataMember]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [DataMember]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [DataMember]
        public string Caption { get; set; }

        /// <summary>
        /// 项目类型ID
        /// </summary>
        [DataMember]
        public Guid? ProjectTypeId { get; set; }

        /// <summary>
        /// 地区
        /// </summary>
        [DataMember]
        public string RegionName { get; set; }

        /// <summary>
        /// 项目经理
        /// </summary>
        [DataMember]
        public string ManagerName { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        [DataMember]
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 是否领导
        /// </summary>
        [DataMember]
        public bool IsLeader { get; set; }

        /// <summary>
        /// 是否储备项目
        /// </summary>
        [DataMember]
        public bool IsReserve { get; set; }

        /// <summary>
        /// 是否重启过
        /// </summary>
        [DataMember]
        public bool? IsRestarted { get; set; }

    }

    /// <summary>
    /// 零售户查询类
    /// </summary>
    [DataContract(IsReference = true)]
    public class RetailerQueryParam : QueryParam
    {
        //查询关键词，模糊匹配
        [DataMember]
        public string KeyWord { set; get; }

       /// <summary>
       /// 零售户状态
       /// </summary>
        [DataMember]
        public string STATUS { set; get; }

        /// <summary>
        /// 收货方式
        /// </summary>
        [DataMember]
        public string RecieveType { set; get; }


        /// <summary>
        /// 是否采点
        /// </summary>
        [DataMember]
        public bool IsCollect { set; get; }

    }


    /// <summary>
    /// 
    /// </summary>
    [DataContract(IsReference = true)]
    public class DistCustByDayQueryParam : QueryParam
    {
        /// <summary>
        /// 送货日期
        /// </summary>
        [DataMember]
        public DateTime DistTime { get; set; }

        /// <summary>
        /// 配送员ID
        /// </summary>
        [DataMember]
        public string DLVMAN_ID { get; set; }

        //查询关键词，模糊匹配
        [DataMember]
        public string KeyWord { set; get; }


    }


    /// <summary>
    /// 满意度分析查询类
    /// </summary>
     [DataContract(IsReference = true)]
    public class SatisfyQueryParam:QueryParam
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        [DataMember]
        public DateTime StartDate
        {
            set;
            get;
        }

        /// <summary>
        /// 结束日期
        /// </summary>
        [DataMember]
        public DateTime EndDate
        {
            set;
            get;
        }

        /// <summary>
        /// 满意度
        /// </summary>
        public string SatisfyValue
        {
            set;
            get;
        }

        /// <summary>
        /// 满意度
        /// </summary>
        public string SatisfyName
        {
            set;
            get;
        }


        /// <summary>
        /// 配送车辆ID
        /// </summary>
        [DataMember]
        public string CAR_ID
        {
            set;
            get;
        }

        /// <summary>
        /// 配送员ID
        /// </summary>
        [DataMember]
        public string DLVMAN_ID
        {
            set;
            get;
        }


    }


     /// <summary>
     /// 订单查询类
     /// </summary>
     [DataContract(IsReference = true)]
     public class LdmDistLineQueryParam : QueryParam
     {
         /// <summary>
         /// 配送单号
         /// </summary>
         public string DIST_NUM
         {
             set;
             get;
         }

         /// <summary>
         /// 关键字
         /// </summary>
         public string KeyData
         {
             set;
             get;
         }


         /// <summary>
         /// 配送员ID
         /// </summary>
         public string DLVMAN_ID
         {
             set;
             get;
         }

         /// <summary>
         /// 线路ID
         /// </summary>
         public string RUT_ID
         {
             set;
             get;
         }


         /// <summary>
         /// 日期
         /// </summary>
         public DateTime DistDate
         {
             set;
             get;
         }

         /// <summary>
         /// 日期
         /// </summary>
         public string DIST_DATE
         {
             set;
             get;
         }

         /// <summary>
         /// 微信OPENID
         /// </summary>
         public string WxUserName
         {
             set;
             get;
         }

     }

     /// <summary>
     /// 配送单查询类
     /// </summary>
     [DataContract(IsReference = true)]
     public class LdmDistQueryParam : QueryParam
     {
         /// <summary>
         /// 日期
         /// </summary>
         public DateTime DistDate
         {
             set;
             get;
         }

         /// <summary>
         /// 日期
         /// </summary>
         public string DIST_DATE
         {
             set;
             get;
         }

         /// <summary>
         /// 微信OPENID
         /// </summary>
         public string WxUserName
         {
             set;
             get;
         }

     }


     /// <summary>
     /// 配送点定位查询类
     /// </summary>
     [DataContract(IsReference = true)]
     public class DeliveryWarningQueryParam : QueryParam
     {
         /// <summary>
         /// 配送单号
         /// </summary>
         public string DIST_NUM
         {
             set;
             get;
         }

         /// <summary>
         /// 配送员ID
         /// </summary>
         public string DLVMAN_ID
         {
             set;
             get;
         }

         /// <summary>
         /// 日期
         /// </summary>
         public DateTime DistDate
         {
             set;
             get;
         }


     }

     #region 车辆运行分析
     

     /// <summary>
     /// 车辆运行查询类
     /// </summary>
     [DataContract(IsReference = true)]
     public class DistCarRunQueryParam : QueryParam
     {
         /// <summary>
         /// 开始日期
         /// </summary>
         [DataMember]
         public DateTime StartDate
         {
             set;
             get;
         }

         /// <summary>
         /// 结束日期
         /// </summary>
         [DataMember]
         public DateTime EndDate
         {
             set;
             get;
         }

         /// <summary>
         /// 配送车辆ID
         /// </summary>
          [DataMember]
         public string CAR_ID
         {
             set;
             get;
         }


     }

     /// <summary>
     /// 车辆运行明细查询类
     /// </summary>
     [DataContract(IsReference = true)]
     public class DistCarRunLineQueryParam : QueryParam
     {
         /// <summary>
         /// 开始日期
         /// </summary>
         [DataMember]
         public DateTime StartDate
         {
             set;
             get;
         }

         /// <summary>
         /// 结束日期
         /// </summary>
         [DataMember]
         public DateTime EndDate
         {
             set;
             get;
         }

         /// <summary>
         /// 配送车辆ID
         /// </summary>
         [DataMember]
         public string CAR_ID
         {
             set;
             get;
         }

     }


     /// <summary>
     /// 车辆运行增长率查询类
     /// </summary>
     [DataContract(IsReference = true)]
     public class DistCarRunRateQueryParam : QueryParam
     {
         /// <summary>
         /// 配送车辆ID
         /// </summary>
         [DataMember]
         public string CAR_ID
         {
             set;
             get;
         }

         /// <summary>
         /// 年度
         /// </summary>
         [DataMember]
         public int Year
         {
             set;
             get;
         }

         /// <summary>
         /// 月份
         /// </summary>
         [DataMember]
         public int Month
         {
             set;
             get;
         }

     }

     #endregion

     /// <summary>
     /// 配送任务完成率查询参数
     /// </summary>
     [DataContract(IsReference = true)]
     public class LdmFinishQueryParam 
     {
         /// <summary>
         /// 配送日期
         /// </summary>
         [DataMember]
         public DateTime DistDate
         {
             set;
             get;
         }
     }

     /// <summary>
     /// 配送任务完成率查询参数
     /// </summary>
     [DataContract(IsReference = true)]
     public class GisLastLocrecordQueryParam
     {
         /// <summary>
         /// 开始日期
         /// </summary>
         [DataMember]
         public DateTime StartDate
         {
             set;
             get;
         }

         /// <summary>
         /// 结束日期
         /// </summary>
         [DataMember]
         public DateTime EndDate
         {
             set;
             get;
         }

         /// <summary>
         /// 配送员ID
         /// </summary>
         [DataMember]
         public string DLVMAN_ID
         {
             set;
             get;
         }



     }

     /// <summary>
     /// 时间节点查询类
     /// </summary>
     [DataContract(IsReference = true)]
     public class TimeRecordQueryParam
     {
         /// <summary>
         /// 日期
         /// </summary>
         [DataMember]
         public DateTime QueryDate
         {
             set;
             get;
         }
  
     }


     /// <summary>
     /// 用户绑定查询类
     /// </summary>
     [DataContract(IsReference = true)]
     public class WeiXinUserQueryParam:QueryParam
     {
         /// <summary>
         /// 查询用户类型
         /// </summary>
         [DataMember]
         public Guid WeiXinUserTypeId
         {
             set;
             get;
         }


         /// <summary>
         /// 线路
         /// </summary>
         [DataMember]
         public string RUT_ID
         {
             set;
             get;
         }

         /// <summary>
         /// 关键字
         /// </summary>
         [DataMember]
         public string KeyWord
         {
             set;
             get;
         }


     }



    
     /// <summary>
     /// 车辆检查查询参数
     /// </summary>
     [DataContract(IsReference = true)]
     public class DistCarCheckQueryParam : QueryParam
     {
         /// <summary>
         /// 检查日期
         /// </summary>
         [DataMember]
         public DateTime CheckDate
         {
             set;
             get;
         }

     }
}
