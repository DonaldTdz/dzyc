using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHQR.UI.Models
{
    public class RecordModel : IRecordModel
    {
        public DateTime CreateTime { get; set; }

        public DateTime EditTime { get; set; }

        public string Editor { get; set; }

        public string Creator { get; set; }
    }


}