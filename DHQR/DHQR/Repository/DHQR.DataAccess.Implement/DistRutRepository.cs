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
    /// 配送线路数据访问层
    /// </summary>
    public class DistRutRepository : ProRep<DistRut>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public DistRutRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<DistRut> EntityCurrentSet
        {
            get { return ActiveContext.DistRuts; }
        }


        /// <summary>
        /// 同步线路信息
        /// </summary>
        /// <param name="dlvmans"></param>
        /// <param name="dohandle"></param>
        public void SynDistRuts(List<DistRut> ruts, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "同步失败" };
            List<string> rutIds = ruts.Select(f => f.RUT_ID).ToList();
            IList<DistRut> existRuts = ActiveContext.DistRuts.Where(f => rutIds.Contains(f.RUT_ID)).ToList();
            foreach (var item in ruts)
            {
                var currentRut = existRuts.SingleOrDefault(f => f.RUT_ID == item.RUT_ID);
                if (currentRut == null)
                {
                    item.Id = Guid.NewGuid();
                    ActiveContext.DistRuts.AddObject(item);
                }
                else
                {
                    currentRut.IS_MRB = item.IS_MRB;
                }
            }
            ActiveContext.SaveChanges();
            dohandle.IsSuccessful = true;
            dohandle.OperateMsg = "同步成功!";
        }



    }
}
