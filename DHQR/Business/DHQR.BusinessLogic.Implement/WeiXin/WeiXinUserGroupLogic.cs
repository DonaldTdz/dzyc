using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.BLL.Implement;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;
using Common.DAL.Implement;
using Common.Base;


namespace DHQR.BusinessLogic.Implement
{
    /// <summary>
    /// 微信用户分组逻辑层
    /// </summary>
    public class WeiXinUserGroupLogic : BaseLogic<WeiXinUserGroup>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private WeiXinUserGroupRepository repository { get { return new WeiXinUserGroupRepository(_baseDataEntities); } }


        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<WeiXinUserGroup> Repository
        {
            get { return repository; }
        }

        /// <summary>
        /// 同步微信用户组
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="dohandle"></param>
        public void SynUserGroup(IList<WeiXinUserGroup> groups, out DoHandle dohandle)
        {
            repository.SynUserGroup(groups,out dohandle);
        }

        public WeiXinUserGroup GetByGroupId(int groupId)
        {
            return repository.GetByGroupId(groupId);

        }

    }
}
