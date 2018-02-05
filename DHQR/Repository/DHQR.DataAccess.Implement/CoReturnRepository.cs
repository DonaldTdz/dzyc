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
    /// 退货单抬头数据访问层
    /// </summary>
    public class CoReturnRepository : ProRep<CoReturn>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public CoReturnRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<CoReturn> EntityCurrentSet
        {
            get { return ActiveContext.CoReturns; }
        }


        #region 退货


        /// <summary>
        /// 整单退货
        /// </summary>
        /// <param name="param"></param>
        /// <param name="dohandle"></param>
        public void ReturnAllOrder(DistCust param, string RETURN_CO_NUM, out DoHandle dohandle)
        {
            dohandle = new DoHandle { IsSuccessful = false, OperateMsg = "操作失败!" };
            var order = ActiveContext.LdmDistLines.FirstOrDefault(f => f.DIST_NUM == param.DIST_NUM && f.CO_NUM == param.CO_NUM);
            var orderDetails = ActiveContext.LdmDisItems.Where(f => f.CO_NUM == order.CO_NUM).ToList();

            CoReturn reOrder = new CoReturn 
            {
                Id=Guid.NewGuid(),
                RETURN_CO_NUM = RETURN_CO_NUM,
                CUST_ID=order.CUST_ID,
                TYPE="01",
                STATUS="01",
                CRT_DATE=DateTime.Now.ToString("yyyyMMdd"),
                CRT_USER_NAME=string.Empty,
                ORG_CO_NUM=order.CO_NUM,
                AMT_SUM=order.AMT_AR,
                QTY_SUM=order.QTY_BAR
            };

            List<CoReturnLine> lines = new List<CoReturnLine>();
            int i = 0;
            foreach (var item in orderDetails)
            {
                CoReturnLine l = new CoReturnLine 
                {
                    Id=Guid.NewGuid(),
                    RETURN_CO_NUM = order.CO_NUM,
                    LINE_NUM=i,
                    ITEM_ID=item.ITEM_ID,
                    QTY_ORD=item.QTY
                };
                lines.Add(l);
                i++;
            }
            ReturnPatialOrderParam reParam = new ReturnPatialOrderParam {Cust=param,Order=reOrder,OrderDetails=lines };
            ReturnPatialOrder(reParam, out dohandle);
        }

        /// <summary>
        /// 退货操作
        /// </summary>
        /// <param name="param"></param>
        /// <param name="dohandle"></param>
        public void ReturnPatialOrder(ReturnPatialOrderParam param, out DoHandle dohandle)
        {
            dohandle = new DoHandle {IsSuccessful=false,OperateMsg="操作失败!" };

            var cust = param.Cust;
            var order = param.Order;
            var orderDetails = param.OrderDetails;

            cust.Id = Guid.NewGuid();
            cust.RecieveDate = DateTime.Now;
            ActiveContext.DistCusts.AddObject(cust);

            order.Id = Guid.NewGuid();
            ActiveContext.CoReturns.AddObject(order);
            int i = 0;
            foreach (var item in orderDetails)
            {
                item.Id = Guid.NewGuid();
                item.LINE_NUM = i;
                ActiveContext.CoReturnLines.AddObject(item);
                i++;
            }
            ActiveContext.SaveChanges();
            dohandle.IsSuccessful = true;
            dohandle.OperateMsg = "操作成功";
        }

        #endregion



    }
}
