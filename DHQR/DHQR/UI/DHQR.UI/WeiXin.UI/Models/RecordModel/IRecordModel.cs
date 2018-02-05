using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHQR.UI.Models
{
    public interface IRecordModel
    {
        /// <summary>
        /// 
        /// </summary>

        DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DateTime EditTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Editor { get; set; }

        /// <summary>
        /// 
        /// </summary>        
        string Creator { get; set; }
    }
}