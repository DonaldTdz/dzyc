using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.DAL.Entities;
using System.Runtime.Serialization;
using Common.Base;



namespace DHQR.DataAccess.Entities
{
    /// <summary>
    /// 配送单完成度
    /// </summary>
     [DataContract]
    public class LdmDistFinishRate
    {

         /// <summary>
         /// 车辆名称
         /// </summary>
         [DataMember]
         public string CAR_NAME { get; set; }

         /// <summary>
         /// 配送日期
         /// </summary>
          [DataMember]
         public string DistDate { get; set; }


         /// <summary>
         /// 配送单号
         /// </summary>
          [DataMember]
          public string DIST_NUM { get; set; }


          /// <summary>
          /// 完成率
          /// </summary>
          [DataMember]
          public decimal FinishRate { get; set; }

    }


}
