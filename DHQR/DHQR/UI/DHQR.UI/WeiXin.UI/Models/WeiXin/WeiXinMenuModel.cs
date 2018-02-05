using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.UI.Model;
using DHQR.DataAccess.Entities;
using DHQR.BusinessLogic.Implement;
using Common.BLL.Implement;

namespace DHQR.UI.Models
{
    #region 模型
    public class WeiXinMenuModel
    {



        #region 基元属性


        public System.Guid Id { get; set; }
        public Nullable<System.Guid> ParentId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
        public string WxType { get; set; }
        public string Url { get; set; }
        public string Key { get; set; }
        public string ContenInfo { get; set; }
        public Nullable<System.Guid> WeiXinSourceId { get; set; }
        public Nullable<System.Guid> WeiXinAppId { get; set; }
        public int Sequence { get; set; }
        public Guid? PicMsgOrTirggerInfoId { get; set; }

        #endregion

    }
    #endregion



    #region Services
    public class WeiXinMenuModelService : BaseModelService<WeiXinMenu, WeiXinMenuModel>
    {
        private readonly WeiXinMenuLogic BusinessLogic;
        public WeiXinMenuModelService()
        {
            BusinessLogic = new WeiXinMenuLogic();
        }

        protected override BaseLogic<WeiXinMenu> BaseLogic
        {
            get { return BusinessLogic; }
        }


        internal object GetTreeData()
        {
            var data = BusinessLogic.GetAll();
            return data == null ? null : data.Select(item => new TreeModel
            {
                id = item.Id.ToString(),
                pId = item.ParentId != Guid.Empty
                ? item.ParentId.ToString() : null,
                name = item.Name,
                ename = item.Name,
                open = true,
                info = item.Name,
                isParent = item.ParentId == null
            }).ToList();
        }

        /// <summary>
        /// 根据微信公众APPId获取自定义菜单
        /// </summary>
        /// <param name="WeiXinAppId"></param>
        /// <returns></returns>
        public IList<WeiXinMenuModel> GetListByWeiXinAppId(Guid weiXinAppId)
        {
            var result = BusinessLogic.GetListByWeiXinAppId(weiXinAppId).Select(f => ConvertToModel(f)).OrderBy(f=>f.Sequence).ToList();
            return result;
        }

        /// <summary>
        /// 根据微信公众APPId获取一级菜单
        /// </summary>
        /// <param name="WeiXinAppId"></param>
        /// <returns></returns>
        public IList<WeiXinMenuModel> GetOneLevelByWeiXinAppId(Guid weiXinAppId)
        {
            var result = GetListByWeiXinAppId(weiXinAppId).Where(f => f.ParentId == null).ToList();
            return result;
        }

        /// <summary>
        /// 根据key和公众账号ID获取自定义菜单
        /// </summary>
        /// <param name="weiXinAppId"></param>
        /// <param name="enventKey"></param>
        /// <returns></returns>
        public WeiXinMenuModel GetByEventKey(Guid weiXinAppId, string enventKey)
        {
            var result = ConvertToModel(BusinessLogic.GetByEventKey(weiXinAppId,enventKey));
            return result;
        }


       
       
    }

    #endregion
}