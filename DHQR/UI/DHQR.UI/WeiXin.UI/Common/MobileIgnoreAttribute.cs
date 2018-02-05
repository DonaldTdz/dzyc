using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHQR.UI.DHQRCommon
{
    /// <summary>
    /// 手机站点忽略权限特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class MobileIgnoreAttribute : Attribute
    {
        /// <summary>
        /// 忽略类型
        /// </summary>
        public MobileIgnoreType IgnoreType { get; private set; }

        /// <summary>
        /// 和某个action验证方式相同时使用的Controller名称
        /// </summary>
        public string SameControllerName { get; set; }

        /// <summary>
        /// 和某个action验证方式相同时使用的action名称
        /// </summary>
        public string SameActionName { get; set; }



        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ignoreType"></param>
        public MobileIgnoreAttribute(MobileIgnoreType ignoreType)
        {
            IgnoreType = ignoreType;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ignoreType"></param>
        public MobileIgnoreAttribute(MobileIgnoreType ignoreType, string sameActionName)
        {
            IgnoreType = ignoreType;
            SameActionName = sameActionName;
        }

    }


    /// <summary>
    /// 权限检查忽略类型
    /// </summary>
    public enum MobileIgnoreType
    {
        /// <summary>
        /// 忽略绑定验证
        /// </summary>
        IgnoreCheck = 0,


        /// <summary>
        /// 忽略功能验证
        /// </summary>
        IgnoreModule = 1,

        /// <summary>
        /// 和某一个Action的权限一样
        /// </summary>
        SameAs = 2,

    }
}