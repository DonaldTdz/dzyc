using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.UI.Model;
using DHQR.DataAccess.Entities;
using DHQR.BusinessLogic.Implement;
using Common.BLL.Implement;
using Common.Base;
using System.Web.Mvc;


namespace DHQR.UI.Models
{

    #region 模型

    /// <summary>
    /// 微信用户将分组模型
    /// </summary>
    public class WeiXinUserGroupModel
    {
        #region Model
        private Guid _id;
        private int? _groupid;
        private string _name;
        private int? _count;
        /// <summary>
        /// 
        /// </summary>
        public Guid Id
        {
            set { _id = value; }
            get { return _id; }
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
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 用户数
        /// </summary>
        public int? count
        {
            set { _count = value; }
            get { return _count; }
        }
        #endregion Model
    }
    #endregion

    #region 服务

     /// <summary>
    /// 微信用户分组服务
    /// </summary>
    public class WeiXinUserGroupModelService : BaseModelService<WeiXinUserGroup, WeiXinUserGroupModel>
    {
         private readonly WeiXinUserGroupLogic BusinessLogic;
         public WeiXinUserGroupModelService()
        {
            BusinessLogic = new WeiXinUserGroupLogic();
        }

         protected override BaseLogic<WeiXinUserGroup> BaseLogic
        {
            get { return BusinessLogic; }
        }

        /// <summary>
        /// 将原始用户组转成微信用户组
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
         public WeiXinUserGroupModel ConvertFromWx(WxOriginUserGroup group)
         {
             WeiXinUserGroupModel result = new WeiXinUserGroupModel 
             {
                 Id=Guid.NewGuid(),
                 groupid=group.id,
                 name=group.name,
                 count=group.count
             };
             return result;
         }

        /// <summary>
        /// 同步微信用户组
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="dohandle"></param>
         public void SynUserGroup(IList<WxOriginUserGroup> groups, out DoHandle dohandle)
         {
             var groupmodels = groups.Select(f => ConvertFromWx(f)).ToList();
             var groupEntities = groupmodels.Select(f => ConvertToEntity(f)).ToList();
             BusinessLogic.SynUserGroup(groupEntities, out  dohandle);
         }

        /// <summary>
        /// 获取用户分组
        /// </summary>
        /// <returns></returns>
         public IList<SelectListItem> GetUserGroupList()
         {
             var groups = BusinessLogic.GetAll();
             IList<SelectListItem> result = new List<SelectListItem>();
             foreach (var item in groups)
             {
                 SelectListItem d = new SelectListItem
                 {
                     Value=item.groupid.ToString(),
                     Text=item.name
                 };
                 result.Add(d);
             }
             return result;
         }

         /// <summary>
         /// 创建分组
         /// </summary>
         /// <param name="model"></param>
         /// <param name="dohandle"></param>
         public void CreateGroup(WeiXinUserGroupModel model, Guid weixinAppId,out DoHandle dohandle)
         {
             string msg = string.Empty;
             string operateMsg = string.Empty;
             dohandle = new DoHandle {IsSuccessful=false,OperateMsg="操作失败" };

             WeiXinUserGroupApi groupApi = new WeiXinUserGroupApi();
             msg=  groupApi.CreateGroup(weixinAppId,  model.name, out operateMsg);
             if (msg != "0")
             {
                 dohandle.OperateMsg = operateMsg;
                 return;
             }
             var gps= groupApi.QueryGroup(weixinAppId, out msg);
             SynUserGroup(gps, out dohandle);
            
         }

         /// <summary>
         /// 修改分组名称
         /// </summary>
         /// <param name="model"></param>
         /// <param name="dohandle"></param>
         public void UpdateGroup(WeiXinUserGroupModel model, Guid weixinAppId, out DoHandle dohandle)
         {
             string msg = string.Empty;
             string operateMsg = string.Empty;
             dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "操作失败" };

             WeiXinUserGroupApi groupApi = new WeiXinUserGroupApi();
             msg = groupApi.UpdateUserGroup(weixinAppId, model.groupid.Value, model.name, out operateMsg);
             if (msg != "0")
             {
                 dohandle.OperateMsg = operateMsg;
                 return;
             }
             var gp = ConvertToEntity(model);
             BusinessLogic.Update(gp, out dohandle);
         }




         /// <summary>
         /// 获取群发对象列表
         /// </summary>
         /// <returns></returns>
         public IList<SelectListItem> GetSendTargetList()
         {
             IList<SelectListItem> selectList = new List<SelectListItem>();
             SelectListItem item1 = new SelectListItem
                 {
                     Text = "全部用户",
                     Value = "0"
                 };
             SelectListItem item2 = new SelectListItem
             {
                 Text = "按群组",
                 Value = "1"
             };
             selectList.Add(item1);
             selectList.Add(item2);   
             return selectList;

         }

         /// <summary>
         /// 获取微信用户分组列表
         /// </summary>
         /// <returns></returns>
         public IList<SelectListItem> GetWeiXinUserGroupList()
         {
             IList<SelectListItem> selectList = new List<SelectListItem>();

             var groups = BusinessLogic.GetAll().ToList();

             foreach (var g in groups)
             {
                 SelectListItem item = new SelectListItem
                 {
                     Text = g.name,
                     Value = g.groupid.ToString()
                 };
                 selectList.Add(item);
             }
             return selectList;

         }

    }

    #endregion

}