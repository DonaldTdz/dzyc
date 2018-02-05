using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.DAL.Implement;
using Common.DAL.Entities;
using DHQR.DataAccess.Entities;
using System.Data.Objects;

namespace DHQR.DataAccess.Implement
{
    /// <summary>
    /// ------------------------------
    /// 项目中继承基础的类进行统一操作
    /// ------------------------------
    /// </summary>
    /// <typeparam name="TEntity">实体类</typeparam>
    public abstract class ProRep<TEntity> : Repository<TEntity> where TEntity : class ,IEntityKey
    {
        /// <summary>
        ///   项目统一私有的实体---BaseDataEntities
        /// </summary>
        protected DHQREntities ActiveContext;

        /// <summary>
        /// 统一重写实体
        /// </summary>
        protected override ObjectContext EntityContext
        {
            get
            {
                return ActiveContext;
            }
        }

        /// <summary>
        /// 用于设置实体集合的项目属性
        /// </summary>
        internal protected abstract ObjectSet<TEntity> EntityCurrentSet { get; }
        /// <summary>
        /// 统一重写实体集合
        /// </summary>
        protected override ObjectSet<TEntity> CurrentSet
        {
            get { return EntityCurrentSet; }
        }
    }
}
