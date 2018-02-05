using System;
using System.Reflection;

namespace DHQR.UI.DHQRCommon
{
    /// <summary>
    /// 对象转换辅助类
    /// </summary>
    public static class ConvertHelper
    {
        /// <summary>
        /// 通过反射将传入的对象转换为指定类型的对象
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static TResult Convert<TResult>(object input) where TResult : class
        {
            if (input == null)
                return null;
            //类型相等时直接返回
            if (input.GetType() == typeof(TResult))
                return (TResult)input;
            return (TResult)Convert(input, typeof(TResult));
        }

        /// <summary>
        /// 通过反射将传入的对象转换为指定类型的对象
        /// </summary>
        /// <param name="input"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public static object Convert(object input, Type targetType)
        {
            object result = Activator.CreateInstance(targetType);
            var properties = targetType.GetProperties();
            var inputType = input.GetType();
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (!propertyInfo.CanWrite)
                    continue;
                PropertyInfo inputPropertyInfo = inputType.GetProperty(propertyInfo.Name);
                if (inputPropertyInfo == null)
                    continue;
                object propertyValue = inputPropertyInfo.GetValue(input, null);
                propertyInfo.SetValue(result, propertyValue, null);
            }
            return result;
        }
    }
}