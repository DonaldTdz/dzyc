using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.UI.Model;
using DHQR.DataAccess.Entities;
using Common.BLL.Implement;
using Common.Base;
using Common.UI.CooKies;
using DHQR.BusinessLogic.Implement;
using Common.UI.EncryptTool;
using Common.UI.Util;
using DHQR.UI.DHQRCommon;

namespace DHQR.UI.Models
{
    #region 模型
    public class UserModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id
        {
            set;
            get;
        }
        /// <summary>
        /// 登录名
        /// </summary>
        public string Name
        {
            set;
            get;
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Nickname
        {
            set;
            get;
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord
        {
            set;
            get;
        }
        /// <summary>
        /// 大写拼音
        /// </summary>
        public string UpperName
        {
            set;
            get;
        }
        /// <summary>
        /// 类型
        /// </summary>
        public int Type
        {
            set;
            get;
        }
        /// <summary>
        /// IM需要
        /// </summary>
        public int key
        {
            set;
            get;
        }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel
        {
            set;
            get;
        }
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string EMail
        {
            set;
            get;
        }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginDate
        {
            set;
            get;
        }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegisterTime
        {
            set;
            get;
        }
        /// <summary>
        /// 是否冻结
        /// </summary>
        public bool IsFreeze
        {
            set;
            get;
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set;
            get;
        }
    }
    #endregion



    #region Services
    public class UserModelService : BaseModelService<User, UserModel>
    {
        private readonly UserLogic _userLogic;
        public UserModelService()
        {
            _userLogic = new UserLogic();
        }
        protected override BaseLogic<User> BaseLogic
        {
            get { return _userLogic; }
        }

        public void ValidateUser(UserModel userModel, out DoHandle doHandle)
        {
            doHandle = new DoHandle();
            string word = PassWordMd.HashPassword(userModel.PassWord);
            try
            {
                var logLogic = new LoginLogLogic();
                logLogic.Create(
                    new LoginLog { CreateTime = DateTime.Now, LoginIp = HostHelper.GetIp(), UserName = userModel.Name },
                    out doHandle);
            }
            catch
            {
            }
            var c = _userLogic.SingleOrDefualt(f => f.Name == userModel.Name && f.PassWord == word);
            if (c == null)
            {
                doHandle.IsSuccessful = false;
                doHandle.OperateMsg = "登录失败！";
                CooKies.SetCookies("token", null);
                CooKies.SetCookies("token_Key", null);
                CooKies.SetCookies("token_Name", null);
                CooKies.SetCookies("user_id", null);
                CooKies.SetCookies("wechat_id", null);
                CooKies.SetCookies("wechat_name", null);

            }
            else
            {
                CooKies.SetCookies("token_Key", EncryptTool.Encrypt(c.key.ToString()));
                CooKies.SetCookies("token", EncryptTool.Encrypt(userModel.Name));//登录名
                CooKies.SetCookies("token_Name", EncryptTool.Encrypt(userModel.Nickname));//用户名
                CooKies.SetCookies("user_id", EncryptTool.Encrypt(c.Id.ToString()));
                //获取绑定账号
                var wechat = new WeiXinAppLogic().GetOneByUserId(c.Id);
                if (wechat != null)
                {
                    CooKies.SetCookies("wechat_id", EncryptTool.Encrypt(wechat.Id.ToString()));
                    CooKies.SetCookies("wechat_name", EncryptTool.Encrypt(wechat.Name.ToString()));
                }



            }
        }

        /// <summary>
        /// 根据用户名确实
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserModel GetByName(string userName)
        {
            return ConvertToModel(_userLogic.GetByName(userName));
        }

        //重写增加
        public override void Add(UserModel model, out DoHandle doHandle)
        {
            model.Remark = "";
            model.PassWord = PassWordMd.HashPassword(model.PassWord);
            model.UpperName = string.Empty;
            base.Add(model, out doHandle);
        }

        /// <summary>
        /// 重写数据
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public override PagedResults<UserModel> GetPageData(QueryParam queryParam)
        {
            return _userLogic.GetUserPageData(queryParam).ConvertTo(ConvertToModel);

        }


        public void Freeze(UserModel userModel, out DoHandle doHandle)
        {
            _userLogic.Freeze(userModel.Id, !userModel.IsFreeze, out doHandle);
        }

        public void LoginOut()
        {
            CooKies.SetCookies("token", null);
            CooKies.SetCookies("token_Key", null);
            CooKies.SetCookies("token_Name", null);
            CooKies.SetCookies("user_id", null);
            CooKies.SetCookies("user_name", null);

        }

        public void DoRegister(UserModel userModel, out DoHandle doHandle)
        {
            userModel.UpperName = userModel.Name.ToUpper();
            userModel.LastLoginDate = null;
            userModel.RegisterTime = DateTime.Now;
            userModel.PassWord = PassWordMd.HashPassword(userModel.PassWord).ToUpper();
            _userLogic.DoRegister(ConvertToEntity(userModel), out  doHandle);
        }


        public void ChangePsd(Guid userId, string oldPsd, string newPsd, out DoHandle doHandle)
        {
            oldPsd = PassWordMd.HashPassword(oldPsd).ToUpper();
            newPsd = PassWordMd.HashPassword(newPsd).ToUpper();
            _userLogic.ChangePsd(userId, oldPsd, newPsd, out doHandle);
        }

        /// <summary>
        /// 重置密码(密码：123456)
        /// </summary>
        /// <param name="userLoginName"></param>
        /// <param name="dohandle"></param>
        public void ResetPsd(string userLoginName, out DoHandle dohandle)
        {
            var user = _userLogic.GetByName(userLoginName);
            var newPsd = PassWordMd.HashPassword("123456").ToUpper();
            _userLogic.ChangePsd(user.Id,user.PassWord,newPsd,out dohandle);
        }

        /// <summary>
        /// 修改用户名 同时同步其他表的用户信息
        /// </summary>
        /// <param name="dohandle"></param>
        public void UpdateOtherUserNames(out DoHandle dohandle)
        {
            _userLogic.UpdateOtherUserNames(out dohandle);
        }


        #region 功能权限

        /// <summary>
        /// 根据功能角色ID获取用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<UserModel> GetUserByRoleId(Guid roleId)
        {
            var result= _userLogic.GetUserByRoleId(roleId).Select(ConvertToModel).ToList();
            return result;
        }

        /// <summary>
        /// 根据功能角色ID获取可选择的用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<UserModel> GetUnselUserByRoleId(Guid roleId)
        {
            var allUser = _userLogic.GetAll().Where(f => f.IsFreeze == false).ToList();
            var selUser = _userLogic.GetUserByRoleId(roleId);
            IList<UserModel> result = new List<UserModel>();
            foreach (var item in allUser)
            {
                var currentDt = selUser.SingleOrDefault(f => f.Id == item.Id);
                if (currentDt == null)
                {
                    var t = ConvertToModel(item);
                    result.Add(t);
                }
            }
            return result;
        }



        #endregion

    }

    #endregion


}