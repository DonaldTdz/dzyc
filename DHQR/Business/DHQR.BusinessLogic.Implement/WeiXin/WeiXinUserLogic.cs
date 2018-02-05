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
    /// 微信用户逻辑层
    /// </summary>
    public class WeiXinUserLogic : BaseLogic<WeiXinUser>
    {

        readonly DHQREntities _baseDataEntities = new DHQREntities();
        private WeiXinUserRep repository { get { return new WeiXinUserRep(_baseDataEntities); } }


        /// <summary>
        /// 重写Repository
        /// </summary>
        protected override Repository<WeiXinUser> Repository
        {
            get { return repository; }
        }

        /// <summary>
        /// 检查是否绑定
        /// </summary>
        /// <param name="WxUserName"></param>
        /// <param name="WeiXinAppId"></param>
        /// <returns></returns>
        public bool CheckHasBind(string WxUserName, Guid WeiXinAppId)
        {
            var result = repository.CheckWxUserHasBind(WxUserName, WeiXinAppId);
            return result;
        }

        /// <summary>
        /// 绑定用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dohandle"></param>
        public void BindUser(WeiXinUser user, out DoHandle dohandle)
        {
            if (user.UserType == 0)
            {
                user.WeiXinUserTypeId = Guid.Parse("4B51C42E-BE15-436F-97A2-6BF48DEDDA6E");
                repository.BindUser(user, out dohandle);
            }
            else
            {
                user.WeiXinUserTypeId = Guid.Parse("1F598055-DDBB-498B-9083-8AFAF926334A");
                repository.BindInternalUser(user, out dohandle);
            }
        }

        /// <summary>
        /// 用户类型ID
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public IList<WeiXinUser> GetUserByTypeId(Guid typeId)
        {
            return repository.GetUserByTypeId(typeId);
        }

         /// <summary>
        /// 系统后台绑定微信用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dohandle"></param>
        public void BindByFromSys(WeiXinUser user, out DoHandle dohandle)
        {
            repository.BindByFromSys(user, out dohandle);
        }


        /// <summary>
        /// 根据微信用户名和APPID获取微信用户
        /// </summary>
        /// <param name="wxUserName"></param>
        /// <param name="weiXinAppId"></param>
        /// <returns></returns>
        public WeiXinUser GetByWxUserName(string wxUserName, Guid weiXinAppId)
        {
            var result = repository.GetByWxUserName(wxUserName, weiXinAppId);
            return result;
        }

        
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public PagedResults<WeiXinUser> QueryData(WeiXinUserQueryParam queryParam)
        {
            var result = repository.QueryData(queryParam);
            return result;
        }

        /// <summary>
        /// 解除绑定
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dohandle"></param>
        public void RemoveBind(WeiXinUser user, out DoHandle dohandle)
        {
            repository.RemoveBind(user, out dohandle);
        }

        #region 微信用户功能权限

        /// <summary>
        /// 根据传入的congtroller和action判断用户是拥有相应功能项的操作权限
        /// </summary>
        /// <param name="WxUserName"></param>
        /// <param name="WeiXinappId"></param>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <param name="handle"></param>
        /// <returns></returns>
        public bool HasModuleAuthority(string WxUserName, Guid WeiXinappId, string controllerName,string actionName,
                                        out DoHandle handle)
        {
            handle = new DoHandle { IsSuccessful=false };
            IList<Module> userModules = repository.GetFeaturesByWxUserName(WxUserName, WeiXinappId);
            bool hasAny =
                userModules.Where(
                    f =>
                    f.ControllerName.ToUpper() == controllerName.ToUpper() &&
                    f.ActionName.ToUpper() == actionName.ToUpper()).Any();
            if (hasAny)
            {
                handle.IsSuccessful = true;
            }
            return hasAny;
        }


        #endregion


        /// <summary>
        /// 根据线路Id获取信息
        /// </summary>
        /// <param name="rutId"></param>
        /// <returns></returns>
        public IList<WeiXinUser> GetByRutId(string rutId)
        {
            return repository.GetByRutId(rutId);
        }
    }
}
