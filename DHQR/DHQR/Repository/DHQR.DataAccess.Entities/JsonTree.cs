using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DHQR.DataAccess.Entities
{
    /// <summary>
    /// 展示树形结构时输出Json数据的类
    /// </summary>
    public class JsonTree
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public JsonTree()
        {
            showcheck = true;
            isexpand = false;
            complete = true;
        }

        /// <summary>
        /// 节点id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 节点值
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// 是否显示复选框，默认否
        /// </summary>
        public bool showcheck { get; set; }

        /// <summary>
        /// 选中状态 0，1，2
        /// </summary>
        public int checkstate { get; set; }

        /// <summary>
        /// 获取是否有子节点
        /// </summary>
        public bool hasChildren { get { return ChildNodes != null && ChildNodes.Count > 0; } }

        /// <summary>
        /// 是否展开,默认否
        /// </summary>
        public bool isexpand { get; set; }

        /// <summary>
        /// 是否已加载子节点,默认否
        /// </summary>
        public bool complete { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public IList<JsonTree> ChildNodes { get; set; }

        /// <summary>
        /// 根据ITree创建JsonTree
        /// </summary>
        /// <returns></returns>
        public static IList<JsonTree> Create<TNode, TRoot>(IList<ITree<TNode, TRoot>> trees)
            where TNode : ITree<TNode, TRoot>
            where TRoot : IIdentityCode, ISequence
        {
            var data = trees.Select(ConvertITreeToJsonTree).ToArray();
            return data;
        }

        /// <summary>
        /// 将ITree转换成JsonTree
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <typeparam name="TRoot"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        private static JsonTree ConvertITreeToJsonTree<TNode, TRoot>(ITree<TNode, TRoot> node)
            where TNode : ITree<TNode, TRoot>
            where TRoot : IIdentityCode, ISequence
        {
            var jsonTree = new JsonTree
            {
                id = node.Id.ToString(),
                text = node.Name,
                value = node.Code,
                ChildNodes = node.Children == null ? null : node.Children.Select(f => ConvertITreeToJsonTree(f)).ToArray()
            };
            return jsonTree;
        }
    }
}
