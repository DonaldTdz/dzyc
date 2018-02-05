using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DHQR.DataAccess.Entities
{
    ///<summary>
    /// 树形结构接口
    ///</summary>
    ///<typeparam name="TTree">树形结构类</typeparam>
    ///<typeparam name="TRoot">树形结构的树根类</typeparam>
    public interface ITree<TTree, TRoot> : IIdentityCode, ISequence
        where TTree : ITree<TTree, TRoot>
        where TRoot : IIdentityCode, ISequence
    {
        ///<summary>
        /// 层级
        ///</summary>
        byte Level { get; set; }

        ///<summary>
        /// 树根信息
        ///</summary>
        TRoot TreeRoot { get; set; }

        /// <summary>
        /// 父节点Id
        /// </summary>
        Guid? ParentId { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        TTree Parent { get; set; }

        ///<summary>
        /// 子节点
        ///</summary>
        IList<TTree> Children { get; set; }

        ///<summary>
        /// 是可以使用的
        ///</summary>
        bool IsEnabled { get; set; }

        ///<summary>
        /// 节点的路径
        ///</summary>
        string Path { get; set; }
    }

}
