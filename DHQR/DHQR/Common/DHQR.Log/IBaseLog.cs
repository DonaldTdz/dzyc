using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DHQR.DataAccess.Entities;
using Common.DAL.Entities;

namespace DHQR.Log
{
    /// <summary>
    /// 日志类的基接口
    /// </summary>
    public interface IBaseLog : IEntityKey
    {
        /// <summary>
        /// 操作时间
        /// </summary>
        DateTime OperateTime { get; set; }


        /// <summary>
        /// 当前用户
        /// </summary>
        string LogonName { get; set; }
    }
}
