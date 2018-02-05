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
    public class WeiXinMenuLogic : BaseLogic<WeiXinMenu>
    {
        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private WeiXinMenuRep WeiXinMenuRep { get { return new WeiXinMenuRep(_baseDataEntities); } }

        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<WeiXinMenu> Repository
        {
            get { return WeiXinMenuRep; }
        }



        public void DeleteMenuId(Guid id, out DoHandle doHandle)
        {
            WeiXinMenuRep.DeleteMenuId(id, out   doHandle)
            ;
        }

        /// <summary>
        /// 根据微信公众APPId获取自定义菜单
        /// </summary>
        /// <param name="WeiXinAppId"></param>
        /// <returns></returns>
        public IList<WeiXinMenu> GetListByWeiXinAppId(Guid weiXinAppId)
        {
            var result = WeiXinMenuRep.GetListByWeiXinAppId(weiXinAppId);
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
            return WeiXinMenuRep.GetByEventKey(weiXinAppId, enventKey);
        }


    }
}
