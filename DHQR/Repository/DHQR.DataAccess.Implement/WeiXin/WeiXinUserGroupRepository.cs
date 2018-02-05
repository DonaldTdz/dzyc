using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using Common.Base;
using DHQR.DataAccess.Entities;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 微信用户分组数据访问层
    /// </summary>
    public class WeiXinUserGroupRepository : ProRep<WeiXinUserGroup>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public WeiXinUserGroupRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<WeiXinUserGroup> EntityCurrentSet
        {
            get { return ActiveContext.WeiXinUserGroups; }
        }

        /// <summary>
        /// 同步微信用户组
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="dohandle"></param>
        public void SynUserGroup(IList<WeiXinUserGroup> groups, out DoHandle dohandle)
        {
            dohandle = new DoHandle {IsSuccessful=false,OperateMsg="操作失败" };
            var allGroups = ActiveContext.WeiXinUserGroups;
            foreach (var item in allGroups)
            {
                ActiveContext.WeiXinUserGroups.DeleteObject(item);
            }
            foreach (var g in groups)
            {
                ActiveContext.WeiXinUserGroups.AddObject(g);
            }
            try
            {
                ActiveContext.SaveChanges();
                dohandle.IsSuccessful = true;
                dohandle.OperateMsg = "操作成功";
            }
            catch (Exception ex)
            {
                dohandle.IsSuccessful = false;
                dohandle.OperateMsg = ex.Message;
            }
        }

        public WeiXinUserGroup GetByGroupId(int groupId)
        {
            return ActiveContext.WeiXinUserGroups.SingleOrDefault(f => f.groupid == groupId);
            
        }

    }
}
