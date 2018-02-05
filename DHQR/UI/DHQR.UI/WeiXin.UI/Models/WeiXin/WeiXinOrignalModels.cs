using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHQR.UI.Models
{
    /// <summary>
    /// 微信用户分组（原始返回）
    /// </summary>
    public class WxOriginUserGroup
    {
        /// <summary>
        /// 分组id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 用户数
        /// </summary>
        public int count { get; set; }


    }
    /// <summary>
    /// 微信用户分组列表（原始返回）
    /// </summary>
    public class WxOriginUserGroupList
    {
        public IList<WxOriginUserGroup> groups { get; set; }
    }

    /// <summary>
    /// 微信关注用户列表
    /// </summary>
    public class WxSubUserList
    {
        /// <summary>
        /// 关注用户总数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 当前返回数量
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// OPENID集合
        /// </summary>
        public UserOpenIdLst data { get; set; }

        /// <summary>
        /// 下一个OPENID
        /// </summary>
        public string next_openid { get; set; }
    }

    /// <summary>
    /// OPENID集合
    /// </summary>
    public class UserOpenIdLst
    {
        public IList<string> openid { get; set; }
    }

}