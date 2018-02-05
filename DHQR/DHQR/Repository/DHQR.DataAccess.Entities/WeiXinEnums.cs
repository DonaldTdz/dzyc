using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DHQR.DataAccess.Entities
{
    /// <summary>
    /// 微信菜单类型
    /// </summary>
    [DataContract]
    public enum WeiXinMenuType
    {
        /// <summary>
        /// 文字消息
        /// </summary>
        [EnumMember]
        Word = 0,

        /// <summary>
        /// 图文消息
        /// </summary>
        [EnumMember]
        Pic = 1,

        /// <summary>
        /// 连接网址
        /// </summary>
        [EnumMember]
        View = 2
    }


    /// <summary>
    /// 微信菜单类型
    /// </summary>
    [DataContract]
    public enum ComplaintState
    {
        /// <summary>
        /// 未处理
        /// </summary>
        [EnumMember]
        NoHandled = 0,

        /// <summary>
        /// 已处理
        /// </summary>
        [EnumMember]
        Handled = 1
    }

}
