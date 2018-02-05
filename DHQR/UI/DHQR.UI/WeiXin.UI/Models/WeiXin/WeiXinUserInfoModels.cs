using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.UI.Model;
using DHQR.DataAccess.Entities;
using DHQR.BusinessLogic.Implement;
using Common.BLL.Implement;
using Common.Base;

namespace DHQR.UI.Models
{

    #region 模型

    /// <summary>
    /// 微信用户基础信息模型
    /// </summary>
    public class WeiXinUserInfoModel
    {
        #region Model
        private Guid _id;
        private int? _subscribe;
        private string _openid;
        private string _nickname;
        private int? _sex;
        private string _language;
        private string _city;
        private string _province;
        private string _country;
        private string _headimgurl;
        private string _subscribe_time;
        private string _remark;
        private int? _groupid;
        private string _groupname;
        /// <summary>
        /// 
        /// </summary>
        public Guid Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 是否关注
        /// </summary>
        public int? subscribe
        {
            set { _subscribe = value; }
            get { return _subscribe; }
        }
        /// <summary>
        /// openid
        /// </summary>
        public string openid
        {
            set { _openid = value; }
            get { return _openid; }
        }
        /// <summary>
        /// 昵称
        /// </summary>
        public string nickname
        {
            set { _nickname = value; }
            get { return _nickname; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public int? sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 语言
        /// </summary>
        public string language
        {
            set { _language = value; }
            get { return _language; }
        }
        /// <summary>
        /// 城市
        /// </summary>
        public string city
        {
            set { _city = value; }
            get { return _city; }
        }
        /// <summary>
        /// 省份
        /// </summary>
        public string province
        {
            set { _province = value; }
            get { return _province; }
        }
        /// <summary>
        /// 国家
        /// </summary>
        public string country
        {
            set { _country = value; }
            get { return _country; }
        }
        /// <summary>
        /// 头像地址
        /// </summary>
        public string headimgurl
        {
            set { _headimgurl = value; }
            get { return _headimgurl; }
        }
        /// <summary>
        /// 关注时间
        /// </summary>
        public string subscribe_time
        {
            set { _subscribe_time = value; }
            get { return _subscribe_time; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 分组ID
        /// </summary>
        public int? groupid
        {
            set { _groupid = value; }
            get { return _groupid; }
        }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string groupname
        {
            set { _groupname = value; }
            get { return _groupname; }
        }
        #endregion Model
    }

    #endregion

    #region 服务

    /// <summary>
    /// 微信用户基础信息服务
    /// </summary>
    public class WeiXinUserInfoModelService : BaseModelService<WeiXinUserInfo, WeiXinUserInfoModel>
    {
       private readonly WeiXinUserInfoLogic BusinessLogic;
       public WeiXinUserInfoModelService()
        {
            BusinessLogic = new WeiXinUserInfoLogic();
        }

          protected override BaseLogic<WeiXinUserInfo> BaseLogic
        {
            get { return BusinessLogic; }
        }

        /// <summary>
        /// 转换model
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
          public override WeiXinUserInfoModel ConvertToModel(WeiXinUserInfo entity)
          {
              WeiXinUserGroupLogic groupLogic = new WeiXinUserGroupLogic();
              var data= base.ConvertToModel(entity);
              var group = groupLogic.GetByGroupId(data.groupid.Value);
              data.groupname = group.name;
              return data;
          }

        /// <summary>
        /// 同步微信用户基础信息
        /// </summary>
        /// <param name="users"></param>
        /// <param name="dohandle"></param>
          public void SynWeiXinUserInfo(IList<WeiXinUserInfoModel> modles, out DoHandle dohandle)
          {
              var users = modles.Select(f => ConvertToEntity(f)).ToList();
              BusinessLogic.SynWeiXinUserInfo(users, out dohandle);
          }

        /// <summary>
        /// 根据OPENID获取用户
        /// </summary>
        /// <param name="openid"></param>
        /// <returns></returns>
          public WeiXinUserInfoModel GetByOpenId(string openid)
          {
              var data = BusinessLogic.GetByOpenId(openid);
              var result = this.ConvertToModel(data);
              return result;
          }

        /// <summary>
          /// 更新用户分组和用户备注名
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="groupid"></param>
        /// <param name="remark"></param>
        /// <param name="dohandle"></param>
          public void UpdateUser(Guid weixinAppId,string openid, int groupid, string remark, out DoHandle dohandle)
          {
              dohandle = new DoHandle {IsSuccessful=false,OperateMsg="更新失败" };
              WeiXinUserApi userApi = new WeiXinUserApi();
              WeiXinUserGroupApi groupApi = new WeiXinUserGroupApi();
              string msg = string.Empty;
              string operateMsg=string.Empty;
              msg = userApi.SetUserRemark(weixinAppId, openid, remark, out operateMsg);
              if (msg != "0")
              {
                  dohandle.OperateMsg = operateMsg;
                  return;
              }
              msg = groupApi.UpdateUserGroup(weixinAppId, openid, groupid, out operateMsg);
              if (msg != "0")
              {
                  dohandle.OperateMsg = operateMsg;
                  return;
              }
              BusinessLogic.UpdateUser(openid, groupid, remark, out dohandle);

          }

          /// <summary>
          /// 删除该分组用户
          /// </summary>
          /// <param name="openids"></param>
          /// <param name="groupid"></param>
          /// <param name="dohandle"></param>
          public void DelFromGroupByUserIds(Guid weixinAppId, IList<string> openids, out DoHandle dohandle)
          {
              string msg = string.Empty;
              string operateMsg = string.Empty;
              WeiXinUserGroupApi groupApi = new WeiXinUserGroupApi();
              foreach (var item in openids)
              {
                  msg = groupApi.UpdateUserGroup(weixinAppId, item, 0, out operateMsg);
              }

              BusinessLogic.DelFromGroupByUserIds(openids, out dohandle);
              if (dohandle.IsSuccessful)
              {
                  WeiXinUserGroupApi wuserApi = new WeiXinUserGroupApi();
                  WeiXinUserGroupModelService modelService = new WeiXinUserGroupModelService();
                  var data = wuserApi.QueryGroup(weixinAppId, out msg);
                  modelService.SynUserGroup(data, out dohandle);

              }
          }

          /// <summary>
          /// 将指定用户加入目标分组
          /// </summary>
          /// <param name="openids"></param>
          /// <param name="groupid"></param>
          /// <param name="dohandle"></param>
          public void AddToGroupByUserIds(Guid weixinAppId, IList<string> openids, int groupid, out DoHandle dohandle)
          {
              string msg = string.Empty;
              string operateMsg = string.Empty;
              WeiXinUserGroupApi groupApi = new WeiXinUserGroupApi();
              foreach (var item in openids)
              {
                  msg = groupApi.UpdateUserGroup(weixinAppId, item, groupid, out operateMsg);
              }

              BusinessLogic.AddToGroupByUserIds(openids, groupid, out dohandle);
              if (dohandle.IsSuccessful)
              {
                  WeiXinUserGroupApi wuserApi = new WeiXinUserGroupApi();
                  WeiXinUserGroupModelService modelService = new WeiXinUserGroupModelService();
                  var data = wuserApi.QueryGroup(weixinAppId, out msg);
                  modelService.SynUserGroup(data, out dohandle);

              }

          }

          /// <summary>
          /// 根据分组ID获取用户
          /// </summary>
          /// <param name="groupid"></param>
          /// <returns></returns>
          public IList<WeiXinUserInfoModel> GetByGroupId(int groupid)
          {
              var datas= BusinessLogic.GetByGroupId(groupid);
              var result = datas.Select(f => ConvertToModel(f)).ToList();
              return result;
          }


          #region 到货确认年终会

          /// <summary>
          /// 零售户2015年度数据
          /// </summary>
          /// <param name="openId"></param>
          /// <returns></returns>
          public WeiXinRetailerProModel GetWeiXinRetailerPro(string openId)
          {
              WeiXinRetailerProModel result = new WeiXinRetailerProModel();
              var data= BusinessLogic.GetWeiXinRetailerPro(openId);
              result.AddFriendNum = data.AddFriendNum;
              result.EvaluateNum = data.EvaluateNum;
              result.FstFriendImg = data.FstFriendImg;
              result.FstFriendNickName = data.FstFriendNickName;
              result.FstUseWXTime = data.FstUseWXTime;
              result.FstSnsTime = data.FstSnsTime;
              result.RecLike = data.RecLike;
              result.RecRedEnvelope = data.RecRedEnvelope;
              result.SnsNum = data.SnsNum;
              result.WXRankNum = data.WXRankNum;
              return result;
          }

          #endregion




    }

    #endregion
}