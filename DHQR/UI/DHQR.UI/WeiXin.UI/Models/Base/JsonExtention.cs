using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;

namespace DHQR.UI.Models
{
     /// <summary>
    /// 提供Json数据格式转换
    /// </summary>
    public static class JsonExtention
    {

        /// <summary>
        /// 从Json字符串转换成具体的类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString">The json格式字符串.</param>
        /// <returns></returns>
        public static T ParseJson<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));

            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

            object result = ser.ReadObject(ms);
            ms.Close();
            T jsonObject = (T)result;
            return jsonObject;

        }

        /// <summary>
        /// 从Json字符串转换成具体的类型集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString">The json格式字符串.</param>
        /// <returns></returns>
        public static IList<T> ParseJsonSet<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IList<T>));

            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

            object result = ser.ReadObject(ms);
            ms.Close();
            IList<T> jsonObject = (IList<T>)result;
            return jsonObject;

        }


        /// <summary>
        /// 从Json字符串转换成具体的类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString">The json格式字符串.</param>
        /// <returns></returns>
        public static T ParseJsonJS<T>(string jsonString)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            T result = serializer.Deserialize<T>(jsonString);

            return result;
        }

        /// <summary>
        /// 转换成json字符串
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="not">不转换的字符串集合</param>
        /// <returns></returns>
        public static string ToJson(object obj, IEnumerable<string> not)
        {
            StringBuilder sb = new StringBuilder();

            //if (obj is IEnumerable)
            //{
            //    sb.Append("[");
            //}
            Type type = obj.GetType();
            IEnumerable<PropertyInfo> properties = type.GetProperties().Where(f => (not == null || !not.Any(g => g == f.Name)));
            sb.Append("{");
            foreach (var info in properties)
            {
                //if(info.PropertyType  is
                sb.AppendFormat("\"{0}\":\"{1}\",", info.Name, info.GetValue(obj, null));
            }
            return sb.ToString().TrimEnd(',');

        }


        /// <summary>
        /// 将对象转成json string
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="not">The not.</param>
        /// <returns></returns>
        public static string ToJson(IEnumerable<object> obj, IEnumerable<string> not)
        {
            StringBuilder sb = new StringBuilder();

            //if (obj is IEnumerable)
            //{
            //    sb.Append("[");
            //}
            Type type = obj.GetType();
            IEnumerable<PropertyInfo> properties = type.GetProperties().Where(f => (not == null || !not.Any(g => g == f.Name)));
            sb.Append("{");
            foreach (var info in properties)
            {
                sb.AppendFormat("\"{0}\":\"{1}\",", info.Name, info.GetValue(obj, null));
            }
            return sb.ToString().TrimEnd(',');

        }

    }

}
