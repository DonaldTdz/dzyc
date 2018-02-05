using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using DHQR.DataAccess.Entities;
using DHQR.DataAccess.Implement;


namespace Basic.DAl
{
    /// <summary>
    /// 触发信息
    /// </summary>
    public class WeiXinTriggerInfoRep : ProRep<WeiXinTriggerInfo>
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public WeiXinTriggerInfoRep(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<WeiXinTriggerInfo> EntityCurrentSet
        {
            get { return ActiveContext.WeiXinTriggerInfos; }
        }

        public bool UpdateTrigger(WeiXinTriggerInfo entity)
        {
            string sqlStr = string.Format(@"Update WeiXinTriggerInfos Set KeyWord ='{0}', PatternMethod='{1}', MsgTitle='{2}', MsgCoverPath='{3}' Where Id = '{4}'",
                                        entity.KeyWord, entity.PatternMethod, entity.MsgTitle, entity.MsgCoverPath, entity.Id);
            var resultCount = ActiveContext.ExecuteStoreCommand(sqlStr);
            return resultCount == 1;
        }

        //创建trigger info
        public bool CreateTrigger(WeiXinTriggerInfo entity)
        {
            string sqlStr = string.Format(@"insert into WeiXinTriggerInfos values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                                       entity.Id, entity.Code, entity.ActionUrl, entity.KeyWord, entity.PatternMethod, entity.MsgTitle, entity.MsgCoverPath);
            var resultCount = ActiveContext.ExecuteStoreCommand(sqlStr);
            return resultCount == 1;
        }
    }
}
