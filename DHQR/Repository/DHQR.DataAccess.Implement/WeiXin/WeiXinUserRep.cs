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
    /// 微信用户数据访问层
    /// </summary>
    public class WeiXinUserRep : ProRep<WeiXinUser>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public WeiXinUserRep(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<WeiXinUser> EntityCurrentSet
        {
            get { return ActiveContext.WeiXinUsers; }
        }


        /// <summary>
        /// 检查零售户是否绑定
        /// </summary>
        /// <param name="WxUserName"></param>
        /// <param name="SysName"></param>
        /// <param name="WeiXinAppId"></param>
        /// <returns></returns>
        public bool CheckWxUserHasBind(string WxUserName, Guid WeiXinAppId)
        {
            var result = EntityCurrentSet.Any(f => f.WxUserName == WxUserName && f.WeiXinAppId == WeiXinAppId);
            return result;
        }


        /// <summary>
        /// 检查零售户是否绑定
        /// </summary>
        /// <param name="WxUserName"></param>
        /// <param name="SysName"></param>
        /// <param name="WeiXinAppId"></param>
        /// <returns></returns>
        public bool CheckHasBind(string SysName, Guid WeiXinAppId)
        {
            var result = EntityCurrentSet.Any(f => f.SysName == SysName && f.WeiXinAppId == WeiXinAppId);
            return result;
        }

        /// <summary>
        /// 检查内部员工是否绑定
        /// </summary>
        /// <param name="WxUserName"></param>
        /// <param name="WeiXinAppId"></param>
        /// <returns></returns>
        public bool CheckInnerHasBind(string WxUserName, Guid WeiXinAppId)
        {
            var result = EntityCurrentSet.Any(f => f.WxUserName == WxUserName && f.WeiXinAppId == WeiXinAppId);
            return result;
        }


        /// <summary>
        /// 绑定零售户
        /// </summary>
        /// <param name="model"></param>
        /// <param name="dohandle"></param>
        public void BindInternalUser(WeiXinUser user, out DoHandle dohandle)
        {
            dohandle = new DoHandle {IsSuccessful=false,OperateMsg="绑定失败！" };
            var isBind = CheckHasBind(user.SysName, user.WeiXinAppId);
            if (isBind)
            {
                dohandle.OperateMsg = "您的账号已经完成了绑定，请不要重复操作！";
            }
            else
            {
                string word = PassWordMd.HashPassword(user.SysPsw);
                var data = ActiveContext.Users
                                    .SingleOrDefault(f => 
                                                        f.Name==user.SysName
                                                      && f.PassWord == word   
                                    );
                if (data == null)
                {
                    dohandle.OperateMsg = "绑定失败，请检查您的账号和密码，或与管理员联系！";
                }
                else
                {
                    WeiXinUser newUser = new WeiXinUser 
                    {
                        Id=Guid.NewGuid(),
                        UserType=user.UserType,
                        SysName=data.Name,
                        SysPsw = data.PassWord,
                        Name=data.Nickname,
                        Tel=user.Tel,
                        Address=string.Empty,
                        WxUserName=user.WxUserName,
                       WeiXinUserTypeId=user.WeiXinUserTypeId,
                       WeiXinAppId=user.WeiXinAppId
                    };

                    ActiveContext.WeiXinUsers.AddObject(newUser);
                    ActiveContext.SaveChanges();
                    dohandle.IsSuccessful = true;
                    dohandle.OperateMsg = "恭喜您，账号绑定成功！您可以使用更多的功能！";
                }
            }
        }


        /// <summary>
        /// 绑定内部员工
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dohandle"></param>
        public void BindUser(WeiXinUser user, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "绑定失败！" };
            var isBind = CheckHasBind(user.SysName, user.WeiXinAppId);
            if (isBind)
            {
                dohandle.OperateMsg = "您的账号已经完成了绑定，请不要重复操作！";
            }
            else
            {
                string word = PassWordMd.HashPassword(user.SysPsw);
                var data = ActiveContext.Retailers
                                    .SingleOrDefault(f =>
                                                        f.LICENSE_CODE == user.SysName
                                                      && f.PSW == word
                                                     
                                    );
                if (data == null)
                {
                    dohandle.OperateMsg = "绑定失败，请检查您的账号和密码，或与管理员联系！";
                }
                else
                {
                    if (data.STATUS == "03" || data.STATUS == "04")
                    {
                        dohandle.OperateMsg = "绑定失败，您所绑定的客户为暂停或注销客户！";
                    }
                    else
                    {
                        WeiXinUser newUser = new WeiXinUser
                        {
                            Id = Guid.NewGuid(),
                            UserType = user.UserType,
                            SysName = data.LICENSE_CODE,
                            SysPsw = data.PSW,
                            Name = data.CUST_NAME,
                            Tel = user.Tel,
                            Address = string.Empty,
                            WxUserName = user.WxUserName,
                            WeiXinUserTypeId = user.WeiXinUserTypeId,
                            WeiXinAppId = user.WeiXinAppId
                        };

                        ActiveContext.WeiXinUsers.AddObject(newUser);
                        ActiveContext.SaveChanges();
                        dohandle.IsSuccessful = true;
                        dohandle.OperateMsg = "恭喜您，账号绑定成功！您可以使用更多的功能！";
                    }
                }
            }

        }

        /// <summary>
        /// 根据用户类型ID获取微信用户
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public IList<WeiXinUser> GetUserByTypeId(Guid typeId)
        {
            var result = ActiveContext.WeiXinUsers.Where(f => f.WeiXinUserTypeId == typeId)
                                       .ToList();
            return result;
        }

        /// <summary>
        /// 系统后台绑定微信用户
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dohandle"></param>
        public void BindByFromSys(WeiXinUser user, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "绑定失败！" };
            var isBind = CheckHasBind(user.WxUserName, user.WeiXinAppId);
            if (isBind)
            {
                dohandle.OperateMsg = "该账号已经完成绑定，请不要重复操作！";
            }
            else
            {
                var data = ActiveContext.WeiXinUsers
                                    .SingleOrDefault(f =>f.Id==user.Id
                                    );
                if (data == null)
                {
                    dohandle.OperateMsg = "未找到指定账号！";
                }
                else
                {
                    data.WxUserName = user.WxUserName;
                    ActiveContext.SaveChanges();
                    dohandle.IsSuccessful = true;
                    dohandle.OperateMsg = "账号绑定成功！";
                }
            }
        }



        /// <summary>
        /// 解除绑定
        /// </summary>
        /// <param name="user"></param>
        /// <param name="dohandle"></param>
        public void RemoveBind(WeiXinUser user, out DoHandle dohandle)
        {
            dohandle = new DoHandle {IsSuccessful=false,OperateMsg="解除绑定失败" };
            var wxUser = ActiveContext.WeiXinUsers.SingleOrDefault(f => f.WxUserName == user.WxUserName && f.WeiXinAppId == user.WeiXinAppId);
            if (wxUser != null)
            {
                ActiveContext.WeiXinUsers.DeleteObject(wxUser);
                try
                {
                    ActiveContext.SaveChanges();
                    dohandle.IsSuccessful = true;
                    dohandle.OperateMsg = "解除绑定成功!";
                }
                catch(Exception ex)
                {
                    dohandle.IsSuccessful = false;
                    dohandle.OperateMsg = ex.Message;
                }
            }
            
        }

        /// <summary>
        /// 根据微信用户名和APPID获取微信用户
        /// </summary>
        /// <param name="wxUserName"></param>
        /// <param name="weiXinAppId"></param>
        /// <returns></returns>
        public WeiXinUser GetByWxUserName(string wxUserName,Guid weiXinAppId)
        {
            var result = ActiveContext.WeiXinUsers.SingleOrDefault(f => f.WxUserName == wxUserName && f.WeiXinAppId == weiXinAppId);
            return result;
        }


        /// <summary>
        /// 根据账户名和APPID获取微信用户
        /// </summary>
        /// <param name="wxUserName"></param>
        /// <param name="weiXinAppId"></param>
        /// <returns></returns>
        public WeiXinUser GetBySysName(string sysNmae, Guid weiXinAppId)
        {
            var result = ActiveContext.WeiXinUsers.SingleOrDefault(f => f.SysName == sysNmae && f.WeiXinAppId == weiXinAppId);
            return result;
        }


        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public PagedResults<WeiXinUser> QueryData(WeiXinUserQueryParam queryParam)
        {
            if (queryParam.WeiXinUserTypeId == Guid.Parse("4B51C42E-BE15-436F-97A2-6BF48DEDDA6E"))
            {
                var query = (from a in ActiveContext.WeiXinUsers
                             join b in ActiveContext.Retailers
                             on a.SysName equals b.LICENSE_CODE
                             where (string.IsNullOrEmpty(queryParam.KeyWord) || a.Name.Contains(queryParam.KeyWord))
                             && (string.IsNullOrEmpty(queryParam.RUT_ID) || b.RUT_ID == queryParam.RUT_ID)
                             select a
                             ).ToPagedResults<WeiXinUser>(queryParam);

                var userCodes = query.Data.Select(f => f.SysName).ToList();
                var openIds = query.Data.Select(f => f.WxUserName).ToList();
                var wxUserInfos = ActiveContext.WeiXinUserInfos.Where(f => openIds.Contains(f.openid)).ToList();
                var rets = ActiveContext.Retailers.Where(f => userCodes.Contains(f.LICENSE_CODE)).ToList();
                var retailers = (from a in rets
                                 join b in ActiveContext.DistRuts
                                 on a.RUT_ID equals b.RUT_ID
                                 where userCodes.Contains(a.LICENSE_CODE)
                                 select new Retailer { LICENSE_CODE = a.LICENSE_CODE, RUT_ID = a.RUT_ID, RUT_NAME = b.RUT_NAME, BUSI_ADDR = a.BUSI_ADDR }
                               ).ToList();

                foreach (var item in query.Data)
                {
                    var rut = retailers.SingleOrDefault(f => f.LICENSE_CODE == item.SysName);
                    item.RUT_NAME = rut == null ? "" : rut.RUT_NAME;
                    item.Address = rut == null ? "" : rut.BUSI_ADDR;
                    var wxUserInfo = wxUserInfos.SingleOrDefault(f => f.openid == item.WxUserName);
                    item.headimgurl = wxUserInfo == null ? "" : wxUserInfo.headimgurl;
                    item.nickname = wxUserInfo == null ? "" : wxUserInfo.nickname;

                }
                return query;
            }
            else
            {
                var query = ActiveContext.WeiXinUsers.Where(f=>
                                     f.WeiXinUserTypeId==queryParam.WeiXinUserTypeId
                                  && (string.IsNullOrEmpty(queryParam.KeyWord) || f.Name.Contains(queryParam.KeyWord))
                    ).ToPagedResults<WeiXinUser>(queryParam);
                var openIds = query.Data.Select(f => f.WxUserName).ToList();
                var wxUserInfos = ActiveContext.WeiXinUserInfos.Where(f => openIds.Contains(f.openid)).ToList();
                foreach (var item in query.Data)
                {
                    var wxUserInfo = wxUserInfos.SingleOrDefault(f => f.openid == item.WxUserName);
                    item.headimgurl = wxUserInfo == null ? "" : wxUserInfo.headimgurl;
                    item.nickname = wxUserInfo == null ? "" : wxUserInfo.nickname;

                }
                return query;
            }
        }

        /// <summary>
        /// 根据线路Id获取信息
        /// </summary>
        /// <param name="rutId"></param>
        /// <returns></returns>
        public IList<WeiXinUser> GetByRutId(string rutId)
        {
            var query = (from a in ActiveContext.WeiXinUsers
                         join b in ActiveContext.Retailers
                         on a.SysName equals b.LICENSE_CODE
                         where (string.IsNullOrEmpty(rutId) || b.RUT_ID == rutId)
                         orderby a.Name
                         select a
                        ).ToList();

            var userCodes = query.Select(f => f.SysName).ToList();
            var openIds = query.Select(f => f.WxUserName).ToList();
            var wxUserInfos = ActiveContext.WeiXinUserInfos.Where(f => openIds.Contains(f.openid)).ToList();
            var rets = ActiveContext.Retailers.Where(f => userCodes.Contains(f.LICENSE_CODE)).ToList();
            var retailers = (from a in rets
                             join b in ActiveContext.DistRuts
                             on a.RUT_ID equals b.RUT_ID
                             where userCodes.Contains(a.LICENSE_CODE)
                             select new Retailer { LICENSE_CODE = a.LICENSE_CODE, RUT_ID = a.RUT_ID, RUT_NAME = b.RUT_NAME, BUSI_ADDR = a.BUSI_ADDR }
                           ).ToList();

            foreach (var item in query)
            {
                var rut = retailers.SingleOrDefault(f => f.LICENSE_CODE == item.SysName);
                item.RUT_NAME = rut == null ? "" : rut.RUT_NAME;
                item.Address = rut == null ? "" : rut.BUSI_ADDR;
                var wxUserInfo = wxUserInfos.SingleOrDefault(f => f.openid == item.WxUserName);
                item.headimgurl = wxUserInfo == null ? "" : wxUserInfo.headimgurl;
                item.nickname = wxUserInfo == null ? "" : wxUserInfo.nickname;

            }
            return query;

        }

        #region

        /// <summary>
        /// 通过微信用户名和微信APPId获取功能项清单
        /// </summary>
        /// <param name="WxUserName">微信用户名</param>
        /// <param name="WeiXinappId">APPId</param>
        /// <returns>功能项清单</returns>
        public IList<Module> GetFeaturesByWxUserName(string WxUserName, Guid WeiXinappId)
        {

            //获取用户已分配角色的功能项清单
            WeiXinUser user = ActiveContext.WeiXinUsers.SingleOrDefault(f => f.WxUserName == WxUserName && f.WeiXinAppId == WeiXinappId);

            IList<Module> userFeatures = (from tptoModule in ActiveContext.WeiXinUserTypeToModules
                                          join module in ActiveContext.Modules
                                          on tptoModule.WeiXinUserTypeId equals module.Id
                                          where tptoModule.WeiXinUserTypeId == user.WeiXinUserTypeId
                                          select module).ToList();
            return userFeatures;
        }



        #endregion
    }
}
