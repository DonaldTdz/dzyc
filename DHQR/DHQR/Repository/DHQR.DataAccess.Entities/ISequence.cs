using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DHQR.DataAccess.Entities
{
    ///<summary>
    /// 排序接口
    ///</summary>
    public interface ISequence
    {
        ///<summary>
        /// 排序号
        ///</summary>
        int Sequence
        { get; set; }
    }
}
