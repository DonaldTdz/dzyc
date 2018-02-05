using Common.Base;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 图文信息抬头数据访问层
    /// </summary>
    public class WeiXinPicMsgMatserRep : ProRep<WeiXinPicMsgMatser>
    {
        ///// <summary>
        ///// 构造函数
        ///// </summary>
        //public WeiXinKeyWordRep()
        //{
        //    ActiveContext = new BaseDataEntities();
        //}

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public WeiXinPicMsgMatserRep(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<WeiXinPicMsgMatser> EntityCurrentSet
        {
            get { return ActiveContext.WeiXinPicMsgMatsers; }
        }

        

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="master"></param>
        /// <param name="detatils"></param>
        /// <returns></returns>
        public DoHandle SaveData(WeiXinPicMsgMatser master, IList<WeiXinPicMsgDetail> detatils)
        {

            var entityMaster = ActiveContext.WeiXinPicMsgMatsers.SingleOrDefault(f => f.Id == master.Id);
            WeiXinSource entitySource = new WeiXinSource();
            if (entityMaster != null)
            {
                entitySource = ActiveContext.WeiXinSources.SingleOrDefault(f => f.Id == entityMaster.WeiXinSourceId);
                entityMaster.Url = master.Url;
                entityMaster.Title = master.Title;
                entityMaster.Description = master.Description;
                entityMaster.PicUrl = master.PicUrl;
                entitySource.Url = master.Url;
                entitySource.Name = master.Title;
            }
            else
            {
                entitySource.Id = Guid.NewGuid();
                entitySource.Name = master.Title;
                entitySource.Url = master.Url;
                
                entitySource.WeiXinAppId = master.WeiXinAppId;
                master.Id = Guid.NewGuid();
                master.WeiXinSourceId = entitySource.Id;
                
                ActiveContext.WeiXinPicMsgMatsers.AddObject(master);
                ActiveContext.WeiXinSources.AddObject(entitySource);
            }
            foreach (var item in detatils)
            {
                var entityItem = ActiveContext.WeiXinPicMsgDetails.SingleOrDefault(f => f.Id == item.Id);
                if (entityItem != null)
                {
                    entityItem.Url = item.Url;
                    entityItem.Description = item.Description;
                    entityItem.PicUrl = item.PicUrl;
                    entityItem.Title = item.Title;
                }
                else
                {
                    item.Id = Guid.NewGuid();
                    item.PicMsgMatserId = master.Id;
                    item.WeiXinSourceId = entitySource.Id;
                    ActiveContext.WeiXinPicMsgDetails.AddObject(item);
                }
            }
            ActiveContext.SaveChanges();
            return new DoHandle() { OperateMsg = "保存成功", IsSuccessful = true };
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public DoHandle DeleteData(Guid masterId)
        {
            var master = ActiveContext.WeiXinPicMsgMatsers.SingleOrDefault(f => f.Id == masterId);
            if (master == null)
            {
                return new DoHandle() { OperateMsg = "没有数据可删除!", IsSuccessful = false };
            }
            var details = ActiveContext.WeiXinPicMsgDetails.Where(f => f.PicMsgMatserId == masterId);
            var source=ActiveContext.WeiXinSources.SingleOrDefault(f=>f.Id==master.WeiXinSourceId);
            ActiveContext.WeiXinPicMsgMatsers.DeleteObject(master);
            ActiveContext.WeiXinSources.DeleteObject(source);
            foreach (var item in details)
            {
                ActiveContext.WeiXinPicMsgDetails.DeleteObject(item);
            }
            ActiveContext.SaveChanges();
            return new DoHandle() { OperateMsg = "删除成功！", IsSuccessful = true };
        }
    }
}
