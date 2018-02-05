using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace DHQR.BasicLib
{
    /// <summary>
    /// 枚举操作
    /// </summary>
    public static class PublicEnum
    {
        /// <summary>
        /// 获取枚举描述信息
        /// </summary>
        /// <param name="en">枚举值</param>
        /// <returns></returns>
        public static string GetEnumDes(Enum en)
        {
            string desc = en.ToString();
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    desc = ((System.ComponentModel.DescriptionAttribute)(attrs[0])).Description;
            }
            return desc;
        }

        /// <summary>
        /// 根据枚举获取所有枚举值对象，并组建成 IEnumerable<SelectListItem>结果集
        /// </summary>
        /// <typeparam name="T">枚举泛型</typeparam>
        /// <typeparam name="bolEmpty">IEnumerable<SelectListItem> 是否包含一个空值，true包含，false不包含</typeparam>
        /// <returns></returns>
        public static Dictionary<string, string> GetSelectListItem<T>(bool bolEmpty)
        {
            Type _type = typeof(T);
            FieldInfo[] fields = _type.GetFields(BindingFlags.Static | BindingFlags.Public);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (bolEmpty)
                dic.Add("　", "　");
            foreach (var fi in fields)
            {
                object value = fi.GetValue(null);
                Enum e = (Enum)Enum.Parse(_type, value.ToString());
                dic.Add(e.GetHashCode().ToString(), GetEnumDes(e));
            }
            return dic;
        }
        /// <summary>
        /// 获取枚举描述信息
        /// </summary>
        /// <typeparam name="T">枚举泛型</typeparam>
        /// <param name="eValue">枚举值(Int)</param>
        /// <returns></returns>
        public static string GetEnumDes<T>(int eValue)
        {
            string str = string.Empty;
            FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (var fi in fields)
            {
                object value = fi.GetValue(null);
                Enum e = (Enum)Enum.Parse(typeof(T), value.ToString());
                if (eValue == e.GetHashCode())
                {
                    str = GetEnumDes(e);
                    break;
                }
            }
            //if (string.IsNullOrEmpty(str))
            //throw new Exception(string.Format("枚举值:{0}在枚举：{1}中不存在", eValue.ToString(), typeof(T).Name));
            return str;
        }
        /// <summary>
        /// 根据枚举值，获取枚举，如果传入的不存在，就返回空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eValue"></param>
        /// <returns></returns>
        public static Enum GetEnum<T>(int eValue)
        {
            FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (var fi in fields)
            {
                object value = fi.GetValue(null);
                Enum e = (Enum)Enum.Parse(typeof(T), value.ToString());
                if (e.GetHashCode() == eValue)
                    return e;
            }
            return null;
        }
    }

}
