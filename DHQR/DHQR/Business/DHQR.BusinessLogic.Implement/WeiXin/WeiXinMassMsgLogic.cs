using System;
using Common.Base;
using Common.BLL.Implement;
using Common.DAL.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;
using System.Collections.Generic;

namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// 微信群发信息逻辑层
    /// </summary>
    public class WeiXinMassMsgLogic : BaseLogic<WeiXinMassMsg>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private WeiXinMassMsgRepository repository { get { return new WeiXinMassMsgRepository(_baseDataEntities); } }
        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<WeiXinMassMsg> Repository
        {
            get { return repository; }
        }


        #region 查询群发多图文集合

        /// <summary>
        /// 查询多图文计划
        /// </summary>
        /// <returns></returns>
        public IList<WeiXinMassGroup> QueryMassGroup()
        {
            return repository.QueryMassGroup();
        }

        #endregion

        /// <summary>
        /// 根据父节点查找明细信息
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IList<WeiXinMassMsg> GetByParentId(Guid parentId)
        {
            return repository.GetByParentId(parentId);
        }

        /// <summary>
        /// 获取单个图文信息
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public WeiXinMassGroup GetByMasterId(Guid masterId)
        {
            return repository.GetByMasterId(masterId);
        }

         /// <summary>
         /// 删除群发素材
         /// </summary>
         /// <param name="masterId"></param>
         /// <param name="dohandle"></param>
        public void DelMassMsg(Guid masterId, out DoHandle dohandle)
        {
            repository.DelMassMsg(masterId, out dohandle);
        }
    }
}
