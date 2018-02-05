using System;
using System.Data.Objects;
using DHQR.DataAccess.Entities;
using System.Linq;
using System.Collections.Generic;
using Common.Base;

namespace DHQR.DataAccess.Implement
{


    /// <summary>
    /// 微信群发信息数据访问层
    /// </summary>
     public class WeiXinMassMsgRepository:ProRep<WeiXinMassMsg>
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
         public WeiXinMassMsgRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
         protected internal override ObjectSet<WeiXinMassMsg> EntityCurrentSet
        {
            get { return ActiveContext.WeiXinMassMsgs; }
        }




        #region 查询群发多图文集合

         /// <summary>
         /// 查询多图文计划
         /// </summary>
         /// <returns></returns>
         public IList<WeiXinMassGroup> QueryMassGroup()
         {
             IList<WeiXinMassGroup> result = new List<WeiXinMassGroup>();
             var heads = ActiveContext.WeiXinMassMsgs.Where(f => f.parentId == null).ToList();
             foreach (var hd in heads)
             {
                 var details = ActiveContext.WeiXinMassMsgs.Where(f => f.parentId == hd.Id).ToList();
                 WeiXinMassGroup gp = new WeiXinMassGroup 
                 {
                     MsgHeader=hd,
                     MsgDetails=details
                 };
                 result.Add(gp);
             }
             return result;
         }

        #endregion


         /// <summary>
         /// 根据父节点查找明细信息
         /// </summary>
         /// <param name="parentId"></param>
         /// <returns></returns>
         public IList<WeiXinMassMsg> GetByParentId(Guid parentId)
         {
             var result = ActiveContext.WeiXinMassMsgs.Where(f => f.parentId == parentId).ToList();
             return result;
         }

         /// <summary>
         /// 获取单个图文信息
         /// </summary>
         /// <param name="masterId"></param>
         /// <returns></returns>
         public WeiXinMassGroup GetByMasterId(Guid masterId)
         {
             var head = ActiveContext.WeiXinMassMsgs.SingleOrDefault(f => f.Id == masterId);
             var details = ActiveContext.WeiXinMassMsgs.Where(f => f.parentId == masterId).ToList();
             WeiXinMassGroup result = new WeiXinMassGroup
             {
                 MsgHeader = head,
                 MsgDetails = details
             };
             return result;
         }

         /// <summary>
         /// 删除群发素材
         /// </summary>
         /// <param name="masterId"></param>
         /// <param name="dohandle"></param>
         public void DelMassMsg(Guid masterId,out DoHandle dohandle)
         {
             dohandle = new DoHandle {IsSuccessful=false,OperateMsg="操作失败!" };
             var master = ActiveContext.WeiXinMassMsgs.SingleOrDefault(f => f.Id == masterId);
             var details = ActiveContext.WeiXinMassMsgs.Where(f => f.parentId == masterId).ToList();
             ActiveContext.WeiXinMassMsgs.DeleteObject(master);
             foreach (var item in details)
             {
                 ActiveContext.WeiXinMassMsgs.DeleteObject(item);
             }

             try
             {
                 ActiveContext.SaveChanges();
                 dohandle.IsSuccessful = true;
                 dohandle.OperateMsg = "操作成功";
             }
             catch (Exception ex)
             {
                 dohandle.IsSuccessful = false;
                 dohandle.OperateMsg = ex.Message;
             }
         }
    }
}
