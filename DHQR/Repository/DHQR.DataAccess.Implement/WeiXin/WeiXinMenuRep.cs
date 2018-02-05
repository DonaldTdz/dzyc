using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using System.Data.Objects;
using Common.Base;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 用户实体操作类
    /// </summary>
    public class WeiXinMenuRep : ProRep<WeiXinMenu>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public WeiXinMenuRep(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<WeiXinMenu> EntityCurrentSet
        {
            get { return ActiveContext.WeiXinMenus; }
        }
        public WeiXinMenu GetByLoginName(string name)
        {
            return SingleOrDefualt(f => f.Name == name);
        }


        public void DeleteMenuId(Guid id, out DoHandle doHandle)
        {
            doHandle = new DoHandle();
            var count = ActiveContext.ExecuteStoreCommand(string.Format("Delete WeiXinMenus Where Id ='{0}' or ParentId ='{0}'", id));
            if (count < 1)
            {
                doHandle.IsSuccessful = false;
                doHandle.OperateMsg = "处理失败";
            }
        }


        /// <summary>
        /// 根据微信公众APPId获取自定义菜单
        /// </summary>
        /// <param name="WeiXinAppId"></param>
        /// <returns></returns>
        public IList<WeiXinMenu> GetListByWeiXinAppId(Guid weiXinAppId)
        {
            var result = ActiveContext.WeiXinMenus.Where(f => f.WeiXinAppId == weiXinAppId).ToList();
            return result;
        }

        /// <summary>
        /// 根据key和公众账号ID获取自定义菜单
        /// </summary>
        /// <param name="weiXinAppId"></param>
        /// <param name="enventKey"></param>
        /// <returns></returns>
        public WeiXinMenu GetByEventKey(Guid weiXinAppId, string enventKey)
        {
            var result = ActiveContext.WeiXinMenus
                                     .SingleOrDefault(f => f.WeiXinAppId == weiXinAppId
                                                        && f.Key==enventKey
                                     );
            return result;
        }
 
    }
}
