using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHQR.UI.Models
{
    /// <summary>
    /// 树模型
    /// </summary>
    public class BaseTreeModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icons { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public IList<BaseTreeModel> ChildNodes { get; set; }

        /// <summary>
        /// 是否有子节点
        /// </summary>
        public bool HasSubMenu { get; set; }

        /// <summary>
        /// 连接地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 是否快捷菜单
        /// </summary>
        public bool IsFavorite { get; set; }
    }
}