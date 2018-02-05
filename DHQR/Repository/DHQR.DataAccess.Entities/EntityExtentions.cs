using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.DAL.Entities;
using System.Runtime.Serialization;

namespace DHQR.DataAccess.Entities
{
    /// <summary>
    /// 用户--扩展实体
    /// </summary>
    public partial class User : IEntityKey
    {
    }
    /// <summary>
    /// 菜单--扩展实体
    /// </summary>
    public partial class Menu : IEntityKey
    {
        public string ParentMenuName { get; set; }
    }

    /// <summary>
    /// 角色对应的菜单--扩展实体
    /// </summary>
    public partial class RoleToAction : IEntityKey
    {
    }


    /// <summary>
    /// 区域--扩展实体
    /// </summary>
    public partial class Region : IEntityKey
    {
    }



    /// <summary>
    ///登录日志--扩展实体
    /// </summary>
    public partial class LoginLog : IEntityKey
    {
    }

    /// <summary>
    ///异常日志--扩展实体
    /// </summary>
    public partial class ExceptionLog : IEntityKey
    {
    }




    /// <summary>
    ///文件上传--扩展实体
    /// </summary>
    public partial class AttachmentInfo : IEntityKey
    {
    }


    /// <summary>
    ///服务调用日志
    /// </summary>
    public partial class ServiceCallLog : IEntityKey
    {
    }

    /// <summary>
    ///全局参数配置
    /// </summary>
    public partial class GlobalConfiguration : IEntityKey
    {
    }



    

    #region 功能权限

    ///功能项--扩展实体
    /// </summary>
    //[NeedCacheAttribute(CacheMode.AllValue)]
    public partial class Module : IEntityKey
    {
    }

    ///功能角色--扩展实体
    /// </summary>
    //[NeedCacheAttribute(CacheMode.AllValue)]
    public partial class ModuleRole : IEntityKey
    {
    }

    ///功能角色与功能项关系--扩展实体
    /// </summary>
    //[NeedCacheAttribute(CacheMode.AllValue)]
    public partial class ModuleRoleToModule : IEntityKey
    {
    }

    ///用户与功能角色关系--扩展实体
    /// </summary>
    //[NeedCacheAttribute(CacheMode.AllValue)]
    public partial class UserModuleRole : IEntityKey
    {
    }

    ///用户对应功能项--扩展实体
    /// </summary>
    //[NeedCacheAttribute(CacheMode.AllValue)]
    public partial class UserFastModule : IEntityKey
    {
    }

    /// <summary>
    /// 用户的功能权限(缓存类)
    /// </summary>
    [DataContract(IsReference = true)]
    [KnownType(typeof(Module))]
    public class UserModuleAuthority
    {
        /// <summary>
        /// 用户登录名
        /// </summary>
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// 用户的功能项
        /// </summary>
        [DataMember]
        public IList<Module> Modules { get; set; }

    }




    #endregion


    #region 业务

    /// <summary>
    ///配送任务单信息
    /// </summary>
    public partial class LdmDist : IEntityKey
    {

    }

    /// <summary>
    ///配送任务单行订单信息
    /// </summary>
    public partial class LdmDistLine : IEntityKey
    {
        /// <summary>
        /// 零售户纬度
        /// </summary>
        [DataMember]
        public decimal CustLongitude { get; set; }

        /// <summary>
        /// 零售户经度
        /// </summary>
        [DataMember]
        public decimal CustLatitude { get; set; }

        /// <summary>
        /// 是否到货确认
        /// </summary>
        [DataMember]
        public bool HasConfirm { get; set; }

        /// <summary>
        /// 收货密码
        /// </summary>
        [DataMember]
        public string PSW { get; set; }

        /// <summary>
        /// 零售户原始纬度
        /// </summary>
        [DataMember]
        public decimal? ORIGINAL_LONGITUDE { get; set; }

        /// <summary>
        /// 零售户原始经度
        /// </summary>
        [DataMember]
        public decimal? ORIGINAL_LATITUDE { get; set; }

        /// <summary>
        /// 预计收货时间
        /// </summary>
        [DataMember]
        public string RecTime { get; set; }

        /// <summary>
        /// 收货状态
        /// </summary>
        [DataMember]
        public string REC_STATE { get; set; }


        /// <summary>
        /// 是否暂存订单
        /// </summary>
        [DataMember]
        public bool IsTemp { get; set; }

        /// <summary>
        /// 刷卡时间
        /// </summary>
        [DataMember]
        public string REC_CIG_TIME { get; set; }

    }

    /// <summary>
    ///配送任务单行订单商品信息
    /// </summary>
    public partial class LdmDisItem : IEntityKey
    {

    }

    /// <summary>
    ///配送车辆信息
    /// </summary>
    public partial class LdmDistCar : IEntityKey
    {
        /// <summary>
        /// 是否登录成功
        /// </summary>
        [DataMember]
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// 反馈信息
        /// </summary>
        [DataMember]
        public string OperateMsg { get; set; }
    }

    /// <summary>
    ///配送操作日志
    /// </summary>
    public partial class DistRecordLog : IEntityKey
    {

    }

    /// <summary>
    ///车辆检查信息
    /// </summary>
    public partial class DistCarCheck : IEntityKey
    {
        /// <summary>
        /// 配送员ID
        /// </summary>
        [DataMember]
        public string USER_ID { get; set; }

    }

    /// <summary>
    ///到货确认信息
    /// </summary>
    public partial class DistCust : IEntityKey
    {
        /// <summary>
        /// 满意度描述
        /// </summary>
        public string DIST_SATIS_DCR
        {
            get
            {
                switch (this.DIST_SATIS)
                {
                    case "10": return "非常满意";
                    case "11": return "满意";
                    case "12": return "一般";
                    case "13": return "不满意";
                    case "14": return "非常不满意";
                    default: return "未知";
                }
            }
        }

    }

    /// <summary>
    ///车辆运行信息头表
    /// </summary>
    public partial class DistCarRun : IEntityKey
    {
        /// <summary>
        /// 车辆名称
        /// </summary>
        [DataMember]
        public string CAR_NAME
        {
            get;
            set;
        }

        /// <summary>
        /// 燃油费
        /// </summary>
        public decimal FUEL_MONEY
        {
            get;
            set;
        }
        /// <summary>
        /// 过路费
        /// </summary>
        public decimal ROAD_MONEY
        {
            get;
            set;
        }
        /// <summary>
        /// 其他费
        /// </summary>
        public decimal OTHER_MONRY
        {
            get;
            set;
        }

        /// <summary>
        /// 车牌号
        /// </summary>
        [DataMember]
        public string LICENSE_CODE
        {
            get;
            set;
        }

        /// <summary>
        /// 送货员
        /// </summary>
        [DataMember]
        public string DLVMAN_NAME
        {
            get;
            set;
        }

        /// <summary>
        /// 驾驶员
        /// </summary>
        [DataMember]
        public string DRIVER_NAME
        {
            get;
            set;
        }

        /// <summary>
        /// 采集时间
        /// </summary>
        [DataMember]
        public string CreateTimeStr
        {
            get { return this.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"); }
        }

    }

    /// <summary>
    ///车辆运行信息行表
    /// </summary>
    public partial class DistCarRunLine : IEntityKey
    {
        /// <summary>
        /// 费用类型描述
        /// </summary>
        public string COST_TYPE_DCR
        {
            get
            {
                switch (this.COST_TYPE)
                {
                    case "1": return "燃油费";
                    case "2": return "过路过桥费";
                    case "9": return "其他";
                    default: return "未知";
                }
            }
        }

        /// <summary>
        /// 燃油类型描述
        /// </summary>
        public string FUEL_TYPE_DCR
        {
            get
            {
                switch (this.FUEL_TYPE)
                {
                    case "1": return "汽油90#";
                    case "2": return "汽油93#";
                    case "3": return "汽油97#";
                    case "4": return "柴油";
                    default: return "其他";
                }
            }
        }

    }

    /// <summary>
    ///照片附件信息
    /// </summary>
    public partial class DistFileLine : IEntityKey
    {

    }

    /// <summary>
    ///退货单抬头
    /// </summary>
    public partial class CoReturn : IEntityKey
    {

    }

    /// <summary>
    ///退货单明细
    /// </summary>
    public partial class CoReturnLine : IEntityKey
    {

    }

    /// <summary>
    ///暂存
    /// </summary>
    public partial class CoTemp : IEntityKey
    {

    }


    /// <summary>
    ///零售户位置信息
    /// </summary>
    public partial class GisCustPois : IEntityKey
    {
        
    }


    /// <summary>
    ///送货员实时位置
    /// </summary>
    public partial class GisLastLocrecord : IEntityKey
    {
        /// <summary>
        /// 配送车名称
        /// </summary>
        [DataMember]
        public string CAR_NAME { get; set; }

    }

    /// <summary>
    /// 零售户基础信息
    /// </summary>
    public partial class Retailer : IEntityKey
    {
        /// <summary>
        /// 线路名称
        /// </summary>
        [DataMember]
        public string RUT_NAME { get; set; }

    }

    /// <summary>
    /// 日志流水号
    /// </summary>
    public partial class LogKey : IEntityKey
    {
    }

    /// <summary>
    /// 回程登记流水号
    /// </summary>
    public partial class CarRunKey : IEntityKey
    {

    }

    /// <summary>
    /// 车辆检查流水号
    /// </summary>
    public partial class CarCheckKey : IEntityKey
    {

    }

    /// <summary>
    /// 退货单流水号
    /// </summary>
    public partial class ReOrderKey : IEntityKey
    {

    }

    /// <summary>
    /// 配送点预警
    /// </summary>
    public partial class DeliveryWarning : IEntityKey
    {
        /// <summary>
        /// 线路ID
        /// </summary>
        [DataMember]
        public string RUT_ID { get; set; }

        /// <summary>
        /// 线路名称
        /// </summary>
        [DataMember]
        public string RUT_NAME { get; set; }

        /// <summary>
        /// 配送员ID
        /// </summary>
        [DataMember]
        public string DLVMAN_ID { get; set; }

        /// <summary>
        /// 配送员名称
        /// </summary>
        [DataMember]
        public string DLVMAN_NAME { get; set; }

        /// <summary>
        /// 配送车ID
        /// </summary>
        [DataMember]
        public string CAR_ID { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        [DataMember]
        public string CAR_LICENSE { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        [DataMember]
        public string CUST_ID { get; set; }

        /// <summary>
        /// 客户代码
        /// </summary>
        [DataMember]
        public string CUST_CODE { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        [DataMember]
        public string CUST_NAME { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        [DataMember]
        public string MANAGER { get; set; }

        /// <summary>
        /// 客户经营地址
        /// </summary>
        [DataMember]
        public string ADDR { get; set; }

        /// <summary>
        /// 客户电话
        /// </summary>
        [DataMember]
        public string TEL { get; set; }


    }

    /// <summary>
    /// 异常日志记录
    /// </summary>
    public partial class ServiceExceptionLog : IEntityKey
    {

    }

    /// <summary>
    /// 到货确认附件
    /// </summary>
    public partial class DistAttachmentInfo : IEntityKey
    {

    }

    /// <summary>
    /// 配送员基本信息
    /// </summary>
    public partial class DistDlvman : IEntityKey
    {
        /// <summary>
        /// 是否登录成功
        /// </summary>
        [DataMember]
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// 反馈信息
        /// </summary>
        [DataMember]
        public string OperateMsg { get; set; }


        /// <summary>
        /// 配送日期
        /// </summary>
        [DataMember]
        public string DistDate { get; set; }
    }


    /// <summary>
    /// APP版本信息
    /// </summary>
    public partial class AppVersion : IEntityKey
    {
        /// <summary>
        /// 是否需要更新
        /// </summary>
        [DataMember]
        public bool NeedUpdate { get; set; }

        /// <summary>
        /// 反馈信息
        /// </summary>
        [DataMember]
        public string OperateMsg { get; set; }


    }

    /// <summary>
    /// 配送线路
    /// </summary>
    public partial class DistRut : IEntityKey
    {

    }

    /// <summary>
    /// 出入门卡信息
    /// </summary>
    public partial class EntranceCard : IEntityKey
    {

    }


    /// <summary>
    /// 暂存订单
    /// </summary>
    public partial class CoTempReturn : IEntityKey
    {

    }




    
    #endregion


    #region 微信

    /// <summary>
    /// 微信配置
    /// </summary>
    public partial class WeiXinApp : IEntityKey
    {
    }

    /// <summary>
    /// 文章管理
    /// </summary>
    public partial class WeiXinArticle : IEntityKey
    {
        /// <summary>
        /// 文章类型名称
        /// </summary>
        public string ArticleTypeName { get; set; }
    }


    /// <summary>
    /// 文章类型
    /// </summary>
    public partial class WeiXinArticlesType : IEntityKey
    {
    }

    /// <summary>
    /// 微信自定义URL
    /// </summary>
    public partial class WeiXinCustomUrl : IEntityKey
    {
    }

    //首次关注回复
    public partial class WeiXinFirstIn : IEntityKey
    {
    }

    /// <summary>
    /// 关键词
    /// </summary>
    public partial class WeiXinKeyWord : IEntityKey
    {
    }

    /// <summary>
    /// 物流信息
    /// </summary>
    public partial class WeiXinLogistic : IEntityKey
    {

    }

    /// <summary>
    /// 微信菜单
    /// </summary>
    public partial class WeiXinMenu : IEntityKey
    {
    }

    /// <summary>
    /// 图文信息抬头
    /// </summary>
    public partial class WeiXinPicMsgMatser : IEntityKey
    {
    }

    /// <summary>
    /// 图文信息明细
    /// </summary>
    public partial class WeiXinPicMsgDetail : IEntityKey
    {
    }

    /// <summary>
    /// 微信系统URL
    /// </summary>
    public partial class WeiXinSysUrl : IEntityKey
    {
    }

    /// <summary>
    /// 微信系统模块类型
    /// </summary>
    public partial class WeiXinSysType : IEntityKey
    {

    }

    /// <summary>
    /// 触发信息设置
    /// </summary>
    public partial class WeiXinTriggerInfo : IEntityKey
    {
    }

    /// <summary>
    /// 微信用户
    /// </summary>
    public partial class WeiXinUser : IEntityKey
    {
        /// <summary>
        /// 货款信息
        /// </summary>
        [DataMember]
        public decimal Money { get; set; }

        /// <summary>
        /// 预计收货时间
        /// </summary>
        [DataMember]
        public string RecTime { get; set; }

        /// <summary>
        /// 零售户订单号
        /// </summary>
        [DataMember]
        public string CO_NUM { get; set; }

        /// <summary>
        /// 线路名称
        /// </summary>
        [DataMember]
        public string RUT_NAME { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        [DataMember]
        public string headimgurl { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [DataMember]
        public string nickname { get; set; }

    }

    /// <summary>
    /// 微信用户类型
    /// </summary>
    public partial class WeiXinUserType : IEntityKey
    {
    }

    /// <summary>
    /// 微信用户类型对应功能项
    /// </summary>
    public partial class WeiXinUserTypeToModule : IEntityKey
    {

    }

    /// <summary>
    /// 微信用户分组
    /// </summary>
    public partial class WeiXinUserGroup : IEntityKey
    {
 
    }

    /// <summary>
    /// 微信用户基本信息
    /// </summary>
    public partial class WeiXinUserInfo : IEntityKey
    {
        
    }

    /// <summary>
    /// 微信群发信息
    /// </summary>
    public partial class WeiXinMassMsg: IEntityKey
    {
        /// <summary>
        /// 预计收货时间
        /// </summary>
        [DataMember]
        public string CreateTimeStr { get { return this.CreateTime.ToString("yyyy-MM-dd"); } }


    }

    /// <summary>
    /// 微信群发媒体信息
    /// </summary>
    public partial class WeiXinMedia : IEntityKey
    {

    }

    /// <summary>
    /// 微信群发历史信息
    /// </summary>
    public partial class WeiXinMassMsgHist : IEntityKey
    {

    }



    #endregion

}
