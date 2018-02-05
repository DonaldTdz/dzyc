using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHQR.DataAccess.Entities;
using System.Data.Objects;

namespace DHQR.DataAccess.Implement
{

    public abstract class BaseEnumRepository<T> : ProRep<BaseEnum>
    where T : BaseEnum, new()
    {
        protected internal override ObjectSet<BaseEnum> EntityCurrentSet
        {
            get { return ActiveContext.BaseEnums; }
        }

        /// <summary>
        /// 根据枚举类型获取基本枚举
        /// </summary>
        /// <param name="enumTypes">枚举类型</param>
        /// <returns>基本枚举</returns>
        public IList<T> GetByTypes(IList<string> enumTypes)
        {

            return EntityCurrentSet.Where(f => enumTypes.Contains(f.EnumType)).Select(f => f.To<T>()).ToList();
        }


        /// <summary>
        /// 通过枚举类型名和枚举值获取一个基本枚举
        /// </summary>
        /// <param name="enumType">枚举类</param>
        /// <param name="value">枚举值</param>
        /// <returns>基本枚举</returns>
        public T GetByUnique(string enumType, string value)
        {
            return EntityCurrentSet.SingleOrDefault(f => f.EnumType == enumType && f.Value == value).To<T>();
        }

        /// <summary>
        /// 据枚举类型获取基本枚举
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns>基本枚举</returns>
        public IList<T> GetByType(string enumType)
        {
            return EntityCurrentSet.Where(f => f.EnumType == enumType).Select(f => f.To<T>()).ToList();
        }

        /// <summary>
        /// 通过枚举值获取一个业务对象
        /// </summary>
        /// <param name="value">枚举值</param>
        /// <returns>一个业务对象</returns>
        public T GetByUnique(string value)
        {
            string enumType = typeof(T).Name;
            //如果T是BaseEnum，返回的是null;
            //子类服务中控制此方法的可操作性。
            return EntityCurrentSet.SingleOrDefault(f => f.EnumType == enumType && f.Value == value).To<T>();
        }

    }

    /// <summary>
    /// 基础枚举数据访问层
    /// </summary>
    public class BaseEnumRepository : BaseEnumRepository<BaseEnum>
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="activeContext"></param>
        public BaseEnumRepository(DHQREntities activeContext)
        {
            ActiveContext = activeContext;
        }
        /// <summary>
        /// 重写实体对应的集合
        /// </summary>
        protected internal override ObjectSet<BaseEnum> EntityCurrentSet
        {
            get { return ActiveContext.BaseEnums; }
        }


        /// <summary>
        /// 根据枚举类型获取基本枚举
        /// </summary>
        /// <param name="enumTypes">枚举类型</param>
        /// <returns>基本枚举</returns>
        public new IList<BaseEnum> GetByTypes(IList<string> enumTypes)
        {

            return EntityCurrentSet
                .Where(f => enumTypes.Contains(f.EnumType))
                .ToList();
        }

        /// <summary>
        /// 通过枚举类型名和枚举值获取一个基本枚举
        /// </summary>
        /// <param name="enumType">枚举类</param>
        /// <param name="value">枚举值</param>
        /// <returns>基本枚举</returns>
        public BaseEnum GetByUnique(string enumType, string value)
        {
            return EntityCurrentSet.SingleOrDefault(f => f.EnumType == enumType && f.Value == value);
        }

        /// <summary>
        /// 据枚举类型获取基本枚举
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns>基本枚举</returns>
        public new IList<BaseEnum> GetByType(string enumType)
        {
            return EntityCurrentSet.Where(f => f.EnumType == enumType).ToList();
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public new IList<BaseEnum> GetAll()
        {
            return  EntityCurrentSet.ToList();
        }

    }

   
}
