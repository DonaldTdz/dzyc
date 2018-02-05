using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 图文信息明细数据防蚊虫
    /// </summary>
    public class WeiXinPicMsgDetailRep : ProRep<WeiXinPicMsgDetail>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public WeiXinPicMsgDetailRep(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<WeiXinPicMsgDetail> EntityCurrentSet
        {
            get { return ActiveContext.WeiXinPicMsgDetails; }
        }


        /// <summary>
        /// 根据抬头获取明细信息
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public IList<WeiXinPicMsgDetail> GetDetails(Guid masterId)
        {
            var result = EntityCurrentSet.Where(f => f.PicMsgMatserId == masterId).ToList();
            return result;
        }

        /// <summary>
        /// 根据抬头集合获取明细信息
        /// </summary>
        /// <param name="masterIds"></param>
        /// <returns></returns>
        public IEnumerable<WeiXinPicMsgDetail> GetDetailbyMsaterIds(IList<Guid> masterIds) 
        {
            var result = EntityCurrentSet.Where(f => masterIds.Any(p => p == f.PicMsgMatserId));
            return result;
        }

    }

}
