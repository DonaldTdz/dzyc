using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.UI.Model;
using DHQR.BusinessLogic.Implement;
using Common.BLL.Implement;
using Common.Base;
using DHQR.DataAccess.Entities;
using System.Web.Mvc;

namespace DHQR.UI.Models
{
    public class WeiXinRetailerProModel
    {
        /// <summary>
        /// 关注时间
        /// </summary>
        public string FstUseWXTime { get; set; }

        /// <summary>
        /// 绑定时间
        /// </summary>
        public string FstSnsTime { get; set; }


        /// <summary>
        /// 第X个关注
        /// </summary>
        public string WXRankNum { get; set; }

        /// <summary>
        /// 第一个送货员
        /// </summary>
        public string FstFriendNickName { get; set; }

        /// <summary>
        /// 第一个送货员头像
        /// </summary>
        public string FstFriendImg { get; set; }

        /// <summary>
        /// 订单数
        /// </summary>
        public string SnsNum { get; set; }


        /// <summary>
        /// 刷卡数
        /// </summary>
        public string RecRedEnvelope { get; set; }


        /// <summary>
        /// 评价数
        /// </summary>
        public string EvaluateNum { get; set; }

        /// <summary>
        /// 新认识送货员数
        /// </summary>
        public string AddFriendNum { get; set; }

        /// <summary>
        /// 非常满意数
        /// </summary>
        public string RecLike { get; set; }

    }
}