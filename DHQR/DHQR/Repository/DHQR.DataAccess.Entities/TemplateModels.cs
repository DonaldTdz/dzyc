using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHQR.DataAccess.Entities
{

    #region 到货确认提醒

    /// <summary>
    /// 微信业务消息模型
    /// </summary>
    public class TemplateModel
    {

        /// <summary>
        /// 接收用户OPENID
        /// </summary>
        public string touser { get; set; }

        /// <summary>
        /// 模板ID
        /// </summary>
        public string template_id { get; set; }

        /// <summary>
        /// 通知链接地址
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 标题颜色
        /// </summary>
        public string topcolor { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public TemplateData data { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class TemplateData
    {

        public TemplateValue first { get; set; }

        public TemplateValue keyword1 { get; set; }

        public TemplateValue keyword2 { get; set; }

        public TemplateValue keyword3 { get; set; }

        public TemplateValue keyword4 { get; set; }

        public TemplateValue remark { get; set; }
    }

    public class TemplateValue
    {
        public string value { get; set; }

        public string color { get; set; }
    }

   
    #endregion

    #region 发货提醒

    /// <summary>
    /// 微信业务消息模型
    /// </summary>
    public class StartTemplateModel
    {

        /// <summary>
        /// 接收用户OPENID
        /// </summary>
        public string touser { get; set; }

        /// <summary>
        /// 模板ID
        /// </summary>
        public string template_id { get; set; }

        /// <summary>
        /// 通知链接地址
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 标题颜色
        /// </summary>
        public string topcolor { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public StartTemplateData data { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class StartTemplateData
    {

        public StartTemplateValue first { get; set; }

        public StartTemplateValue keyword1 { get; set; }

        public StartTemplateValue keyword2 { get; set; }

        public StartTemplateValue remark { get; set; }
    }

    public class StartTemplateValue
    {
        public string value { get; set; }

        public string color { get; set; }
    }

    /// <summary>
    /// 发货消息推送实体
    /// </summary>
    public class StartMissionModel
    {
        public string TaskName { get; set; }

        public string TaskType { get; set; }

        public string DIST_NUM { get; set; }

        public string CAR_LICENSE { get; set; }

        public string DLVMAN_NAME { get; set; }

        public string DRIVER_NAME { get; set; }

        public string WeiXinAppId { get; set; }

    }



    #endregion

    #region 送货完成提醒

    /// <summary>
    /// 微信业务消息模型
    /// </summary>
    public class EndTemplateModel
    {

        /// <summary>
        /// 接收用户OPENID
        /// </summary>
        public string touser { get; set; }

        /// <summary>
        /// 模板ID
        /// </summary>
        public string template_id { get; set; }

        /// <summary>
        /// 通知链接地址
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 标题颜色
        /// </summary>
        public string topcolor { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public EndTemplateData data { get; set; }


    }

    /// <summary>
    /// 
    /// </summary>
    public class EndTemplateData
    {

        public EndTemplateValue first { get; set; }

        public EndTemplateValue keyword1 { get; set; }

        public EndTemplateValue keyword2 { get; set; }

        public TemplateValue keyword3 { get; set; }

        public EndTemplateValue remark { get; set; }
    }

    public class EndTemplateValue
    {
        public string value { get; set; }

        public string color { get; set; }
    }


    /// <summary>
    /// 送货完成推送实体
    /// </summary>
    public class EndMissionModel
    {
        public string DIST_NUM { get; set; }

        public string CAR_LICENSE { get; set; }

        public string DLVMAN_NAME { get; set; }

        public string DRIVER_NAME { get; set; }

        public string WeiXinAppId { get; set; }

    }


    #endregion

}