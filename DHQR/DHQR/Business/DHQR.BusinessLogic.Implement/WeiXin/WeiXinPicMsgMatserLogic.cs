using Common.Base;
using Common.BLL.Implement;
using Common.DAL.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;

namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// 图文信息抬头逻辑层 
    /// </summary>
    public class WeiXinPicMsgMatserLogic : BaseLogic<WeiXinPicMsgMatser>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private WeiXinPicMsgMatserRep WeiXinPicMsgMatserRep { get { return new WeiXinPicMsgMatserRep(_baseDataEntities); } }
        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<WeiXinPicMsgMatser> Repository
        {
            get { return WeiXinPicMsgMatserRep; }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="master"></param>
        /// <param name="detatils"></param>
        /// <returns></returns>
        public DoHandle SaveData(WeiXinPicMsgMatser master, IList<WeiXinPicMsgDetail> detatils) 
        {
            return WeiXinPicMsgMatserRep.SaveData(master, detatils);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public DoHandle DeleteData(Guid masterId) 
        {
            return WeiXinPicMsgMatserRep.DeleteData(masterId);
        }
    }
}
