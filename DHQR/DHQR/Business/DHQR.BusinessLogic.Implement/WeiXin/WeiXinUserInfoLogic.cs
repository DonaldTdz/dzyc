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
    /// 微信用户基础信息逻辑层
    /// </summary>
    public class WeiXinUserInfoLogic : BaseLogic<WeiXinUserInfo>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private WeiXinUserInfoRepository repository { get { return new WeiXinUserInfoRepository(_baseDataEntities); } }


        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<WeiXinUserInfo> Repository
        {
            get { return repository; }
        }

        /// <summary>
        /// 同步微信用户基础信息
        /// </summary>
        /// <param name="users"></param>
        /// <param name="dohandle"></param>
        public void SynWeiXinUserInfo(IList<WeiXinUserInfo> users, out DoHandle dohandle)
        {
            repository.SynWeiXinUserInfo(users, out dohandle);
        }

        /// <summary>
        /// 根据openid获取用户
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
        public WeiXinUserInfo GetByOpenId(string openid)
        {
            return repository.GetByOpenId(openid);
        }

        
        /// <summary>
        /// 更新用户分组和用户备注名
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="groupid"></param>
        /// <param name="remark"></param>
        /// <param name="dohandle"></param>
        public void UpdateUser(string openid, int groupid, string remark, out DoHandle dohandle)
        {
            repository.UpdateUser(openid, groupid, remark, out dohandle);
        }


        /// <summary>
        /// 删除该分组用户
        /// </summary>
        /// <param name="openids"></param>
        /// <param name="groupid"></param>
        /// <param name="dohandle"></param>
        public void DelFromGroupByUserIds(IList<string> openids, out DoHandle dohandle)
        {
            repository.DelFromGroupByUserIds(openids, out dohandle);
        }

        /// <summary>
        /// 将指定用户加入目标分组
        /// </summary>
        /// <param name="openids"></param>
        /// <param name="groupid"></param>
        /// <param name="dohandle"></param>
        public void AddToGroupByUserIds(IList<string> openids, int groupid, out DoHandle dohandle)
        {
            repository.AddToGroupByUserIds(openids, groupid, out dohandle);
        }

        /// <summary>
        /// 根据分组ID获取用户
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns></returns>
        public IList<WeiXinUserInfo> GetByGroupId(int groupid)
        {
            return repository.GetByGroupId(groupid);
        }


        #region 到货确认年终会

        /// <summary>
        /// 零售户2015年度数据
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public WeiXinRetailerPro GetWeiXinRetailerPro(string openId)
        {

            return repository.GetWeiXinRetailerPro(openId);
        }

        #endregion


    }
}
