using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.UI.Model;
using DHQR.DataAccess.Entities;
using DHQR.BusinessLogic.Implement;
using Common.BLL.Implement;
using Common.Base;
using System.ComponentModel;

namespace DHQR.UI.Models
{
    /// <summary>
    /// 微信用户模型
    /// </summary>
    public class WeiXinUserModel
    {
        #region 基元属性

        public System.Guid Id { get; set; }
        public int UserType { get; set; }

        [Description("专卖证号")]
        public string SysName { get; set; }
        [Description("姓名")]
        public string Name { get; set; }
        [Description("电话")]
        public string Tel { get; set; }
        [Description("微信昵称")]
        public string nickname { get; set; }
        [Description("线路")]
        public string RUT_NAME { get; set; }
        [Description("地址")]
        public string Address { get; set; }


        public string SysPsw { get; set; }

        public string WxUserName { get; set; }
        public string LocationX { get; set; }
        public string LocationY { get; set; }
        public System.Guid WeiXinUserTypeId { get; set; }
        public System.Guid WeiXinAppId { get; set; }
        public string key { get; set; }
        public string headimgurl { get; set; }

        #endregion

        #region 扩展属性

        public string UserTypeDsc
        {
            get
            {
                if (this.UserType == 0)
                {
                    return "零售户";
                }
                else
                {
                    return "内部员工";
                }
            }
        }

        #endregion
    }

    #region Services

    /// <summary>
    /// 微信用户服务
    /// </summary>
    public class WeiXinUserModelService : BaseModelService<WeiXinUser, WeiXinUserModel>
    {
        private readonly WeiXinUserLogic BusinessLogic;
        public WeiXinUserModelService()
        {
            BusinessLogic = new WeiXinUserLogic();
        }

        protected override BaseLogic<WeiXinUser> BaseLogic
        {
            get { return BusinessLogic; }
        }

        /// <summary>
        /// 检查是否绑定
        /// </summary>
        /// <param name="WxUserName"></param>
        /// <param name="WeiXinAppId"></param>
        /// <returns></returns>
        public bool CheckHasBind(string WxUserName, Guid WeiXinAppId)
        {
            var result = BusinessLogic.CheckHasBind(WxUserName, WeiXinAppId);
            return result;
        }

        /// <summary>
        /// 绑定用户
        /// </summary>
        /// <param name="model"></param>
        public void BindUser(WeiXinUserModel model, out DoHandle dohandle)
        {
            WeiXinAppLogic wxAppService = new WeiXinAppLogic();
            Guid appId = wxAppService.GetByWeiXinKey(model.key).Id;
            model.WeiXinAppId = appId;
            var user = ConvertToEntity(model);
            BusinessLogic.BindUser(user, out dohandle);
        }

        /// <summary>
        /// 解除绑定
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dohandle"></param>
        public void RemoveBind(WeiXinUserModel model, out DoHandle dohandle)
        {
            WeiXinAppLogic wxAppService = new WeiXinAppLogic();
            Guid appId = wxAppService.GetByWeiXinKey(model.key).Id;
            model.WeiXinAppId = appId;
            var user = ConvertToEntity(model);
            BusinessLogic.RemoveBind(user, out dohandle);
        }


        /// <summary>
        /// 根据用户类型ID获取微信用户
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public IList<WeiXinUserModel> GetUserByTypeId(Guid typeId)
        {
            var result = BusinessLogic.GetUserByTypeId(typeId).Select(f => ConvertToModel(f)).ToList();
            return result;
        }

        /// <summary>
        /// 根据微信用户名和APPID获取微信用户
        /// </summary>
        /// <param name="wxUserName"></param>
        /// <param name="weiXinAppId"></param>
        /// <returns></returns>
        public WeiXinUserModel GetByWxUserName(string wxUserName, Guid weiXinAppId)
        {
            var result = ConvertToModel(BusinessLogic.GetByWxUserName(wxUserName, weiXinAppId));
            return result;
        }

        /// <summary>
        /// 系统绑定微信用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dohandle"></param>
        public void BindByFromSys(WeiXinUserModel model, out DoHandle dohandle)
        {
            var user = ConvertToEntity(model);
            BusinessLogic.BindByFromSys(user, out dohandle);
        }


        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public PagedResults<WeiXinUserModel> QueryData(WeiXinUserQueryParam queryParam)
        {
            PagedResults<WeiXinUserModel> result = new PagedResults<WeiXinUserModel>();
            var data = BusinessLogic.QueryData(queryParam);
            result.Data = data.Data.Select(f => ConvertToModel(f)).ToList();
            result.PagerInfo = data.PagerInfo;
            return result;
        }

        /// <summary>
        /// 根据线路Id获取信息
        /// </summary>
        /// <param name="rutId"></param>
        /// <returns></returns>
        public IList<WeiXinUserModel> GetByRutId(string rutId)
        {
            var result = BusinessLogic.GetByRutId(rutId).Select(f => ConvertToModel(f)).ToList();
            return result;
        }


    }

    #endregion

}