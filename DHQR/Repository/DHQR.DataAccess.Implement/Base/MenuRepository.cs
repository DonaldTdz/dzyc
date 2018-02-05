using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using DHQR.DataAccess.Entities;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// 菜单操作类
    /// </summary>
    public class MenuRepository : ProRep<Menu>
    {
        ///// <summary>
        ///// 构造函数
        ///// </summary>
        //public MenuRep()
        //{
        //    ActiveContext = new BaseDataEntities();
        //}

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public MenuRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<Menu> EntityCurrentSet
        {
            get { return ActiveContext.Menus; }
        }

    }
}
