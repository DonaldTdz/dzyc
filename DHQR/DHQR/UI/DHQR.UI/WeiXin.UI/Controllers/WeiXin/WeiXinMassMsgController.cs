using Common.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHQR.DataAccess.Entities;
using DHQR.UI.Models;

using DHQR.UI.DHQRCommon;
namespace DHQR.UI.Controllers
{
    public class WeiXinMassMsgController : WeiXinBaseController
    {
        //
        // GET: /WeiXinMassMsg/
         [IgnoreModule(IgnoreType.IgnoreLogon)]    
        public ActionResult Index()
        {
            return View();
        }

         #region 发送图文消息

         /// <summary>
        /// 群发图文素材
        /// </summary>
        /// <returns></returns>
         [IgnoreModule(IgnoreType.IgnoreLogon)]       
        public ActionResult SourceManage()
        {
            WeiXinUserGroupModelService groupService = new WeiXinUserGroupModelService();
            ViewData["TargetList"] = groupService.GetSendTargetList();
            ViewData["GroupList"] = groupService.GetUserGroupList();
            return View();

        }

         /// <summary>
         /// 查询多图文计划
         /// </summary>
         /// <returns></returns>
         [IgnoreModule(IgnoreType.IgnoreLogon)]       
        public JsonResult QueryMassGroup()
         {
             WeiXinMassMsgModelService modelService = new WeiXinMassMsgModelService();
             var result = modelService.QueryMassGroup();
             return Json(result, JsonRequestBehavior.AllowGet);
         }

         /// <summary>
         /// 
         /// </summary>
         /// <returns></returns>
         [IgnoreModule(IgnoreType.IgnoreLogon)]
         public ActionResult CreateOrEdit(Guid? id, int target, int? groupid)
         {
             WeiXinPicMsgMatserModelService masterModelSer = new WeiXinPicMsgMatserModelService();
             WeiXinMassMsgModelService modelService = new WeiXinMassMsgModelService();

             var urlData = masterModelSer.GetUrlSelectIdList();
             urlData.Insert(0, new SelectListItem() { Text = "请选择", Value = "", Selected = true });
             ViewData["url"] = urlData;
             ViewData["target"] = target;
             ViewData["groupid"] = groupid;
             WeiXinMassGroupModel model = new WeiXinMassGroupModel { MsgHeader = new WeiXinMassMsgModel(), MsgDetails = new List<WeiXinMassMsgModel> () };
             if (id.HasValue)
             {
                  model = modelService.GetByMasterId(id.Value);
             }
             else
             {
                 model.MsgHeader.CreateTimeStr = DateTime.Now.ToString("yyyy-MM-dd");
             }
             return View(model);
         }


         /// <summary>
         /// 保存数据
         /// </summary>
         /// <param name="detailsStr"></param>
         /// <param name="master"></param>
         /// <returns></returns>
         [IgnoreModule(IgnoreType.IgnoreLogon)]
         public JsonResult SaveData(string detailsStr, WeiXinMassMsg master, bool needSend, int target, int? groupid)
         {
             master.CreateTime = DateTime.Now;
             DoHandle dohandle = new DoHandle { };
             WeiXinMassMsgModelService modelService = new WeiXinMassMsgModelService();
             modelService.SaveData(detailsStr, master, wechat_id, needSend,target,groupid,out dohandle);
             return JsonForDoHandle(dohandle);
         }

         /// <summary>
         /// 群发图文消息
         /// </summary>
         /// <param name="detailsStr"></param>
         /// <param name="master"></param>
         /// <returns></returns>
         [IgnoreModule(IgnoreType.IgnoreLogon)]
         public JsonResult SendMassMsg(Guid masterId,int target,int? groupid)
         {
             DoHandle dohandle = new DoHandle { };
            //groupid = 100;
             WeiXinMassMsgModelService modelService = new WeiXinMassMsgModelService();
             modelService.SendMassMsg(masterId,wechat_id,target,groupid,out dohandle);
             return JsonForDoHandle(dohandle);
         }

         /// <summary>
         /// 删除群发素材
         /// </summary>
         /// <param name="detailsStr"></param>
         /// <param name="master"></param>
         /// <returns></returns>
         [IgnoreModule(IgnoreType.IgnoreLogon)]
         public JsonResult DelMassMsg(Guid masterId)
         {
             DoHandle dohandle = new DoHandle { };
             WeiXinMassMsgModelService modelService = new WeiXinMassMsgModelService();
             modelService.DelMassMsg(masterId,  out dohandle);
             return JsonForDoHandle(dohandle);
         }
   

        #endregion 

         #region 发送文字消息

         /// <summary>
         /// 群发文字消息
         /// </summary>
         /// <returns></returns>
         [IgnoreModule(IgnoreType.IgnoreLogon)]
         public ActionResult MassText()
         {
             WeiXinUserGroupModelService groupService = new WeiXinUserGroupModelService();
             ViewData["TargetList"] = groupService.GetSendTargetList();
             ViewData["GroupList"] = groupService.GetUserGroupList();
             return View();
         }

         /// <summary>
         /// 群发文字消息
         /// </summary>
         /// <param name="detailsStr"></param>
         /// <param name="master"></param>
         /// <returns></returns>
         [IgnoreModule(IgnoreType.IgnoreLogon)]
         public JsonResult SendTextMsg(string content, int target, int? groupid)
         {
             DoHandle dohandle = new DoHandle { };
             groupid = 101;
             WeiXinMassMsgModelService modelService = new WeiXinMassMsgModelService();
             modelService.SendTextMsg(content, wechat_id, target, groupid, out dohandle);
             return JsonForDoHandle(dohandle);
         }

        #endregion

    }
}
