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
    /// 微信用户类型对应功能项
    /// </summary>
    public class WeiXinUserTypeToModuleRep : ProRep<WeiXinUserTypeToModule>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public WeiXinUserTypeToModuleRep(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }

        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<WeiXinUserTypeToModule> EntityCurrentSet
        {
            get { return ActiveContext.WeiXinUserTypeToModules; }
        }

        /// <summary>
        /// 添加功能项到用户类型
        /// </summary>
        /// <param name="moduleIds"></param>
        /// <param name="typeId"></param>
        /// <param name="dohandle"></param>
        public void AddModuleToType(IList<Guid> moduleIds, Guid typeId, out DoHandle dohandle)
        {
            dohandle = new DoHandle {IsSuccessful=false,OperateMsg="添加失败！" };
            foreach (var item in moduleIds)
            {
                WeiXinUserTypeToModule model = new WeiXinUserTypeToModule 
                {
                    Id=Guid.NewGuid(),
                    ModuleId=item,
                    WeiXinUserTypeId=typeId
                };
                ActiveContext.WeiXinUserTypeToModules.AddObject(model);
            }
            ActiveContext.SaveChanges();
            dohandle.IsSuccessful = true;
            dohandle.OperateMsg = "添加成功!";
        }

        /// <summary>
        /// 添加功能项到用户类型
        /// </summary>
        /// <param name="moduleIds"></param>
        /// <param name="typeId"></param>
        /// <param name="dohandle"></param>
        public void DelModuleToType(IList<Guid> moduleIds, Guid typeId, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "添加失败！" };
            foreach (var item in moduleIds)
            {
                var d = ActiveContext.WeiXinUserTypeToModules.SingleOrDefault(f => f.WeiXinUserTypeId == typeId && f.ModuleId == item);
                ActiveContext.WeiXinUserTypeToModules.DeleteObject(d);
            }
            ActiveContext.SaveChanges();
            dohandle.IsSuccessful = true;
            dohandle.OperateMsg = "添加成功!";
        }

    }
}
