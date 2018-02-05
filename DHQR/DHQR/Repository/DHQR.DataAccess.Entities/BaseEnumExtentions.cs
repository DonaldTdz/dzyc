using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.DAL.Entities;
using System.Runtime.Serialization;


namespace DHQR.DataAccess.Entities
{
    /// <summary>
    /// 基础枚举表
    /// </summary>
    public partial class BaseEnum : IEntityKey
    {
        /// <summary>
        ///  类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T To<T>() where T : BaseEnum, new()
        {

            T t = new T
            {
                EnumType = this.EnumType,
                TypeNote = this.TypeNote,
                Value = this.Value,
                ValueNote = this.ValueNote,
                Id = this.Id
            };
            return t;
        }
    }


    /// <summary>
    /// 数据状态
    /// </summary>
    public partial class DataStatus : BaseEnum
    {

    }


}
