using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common.UI.Model;
using DHQR.BusinessLogic.Implement;
using Common.BLL.Implement;
using Common.Base;
using DHQR.DataAccess.Entities;
using DHQR.BasicLib;
using System.Text;
using System.IO;
using DHQR.UI.DHQRCommon;
using System.Data;
using System.Web.Script.Serialization;

namespace DHQR.UI.Models
{
    /// <summary>
    /// 公共模型
    /// </summary>
    public class PublicDataServiceModel
    {
        #region web服务器导出数据
        /// <summary>
        ///  将需要导出Excel的泛型集合 转换成DataTable
        /// </summary>
        /// <param name="entitys">泛型集合</param>
        /// <param name="colName">JqGrid英文属性json字符串</param>
        /// <param name="colTitle">JqGrid中文属性标题json字符串</param>
        /// <returns>导出Excel需要的DataTable</returns>
        public static DataTable ListToDataTable<T>(List<T> entitys, string colName, string colTitle)
        {
            var mySerializer = new JavaScriptSerializer();
            var colsName = mySerializer.Deserialize<string[]>(colName);
            var colsTitle = mySerializer.Deserialize<string[]>(colTitle);
            //取出第一个实体的所有Propertie
            var entityType = entitys[0].GetType();
            var entityProperties = entityType.GetProperties();
            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            var dt = new DataTable();
            for (int i = 0; i < colsName.Length; i++)
            {
                //填充列标题到数据集中
                dt.Columns.Add(colsTitle[i]);
            }
            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                object[] entityValues = new object[colsName.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    for (int k = 0; k < colsName.Length; k++)
                    {
                        //判断当前属性名称和传入的属性名称一致
                        if (entityProperties[i].Name == colsName[k])
                        {
                            //填充该属性对应的集合
                            entityValues[k] = entityProperties[i].GetValue(entity, null);
                        }
                    }
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }

        /// <summary>
        ///  将需要导出Excel的泛型集合 转换成DataTable
        /// </summary>
        /// <param name="entitys">泛型集合</param>
        /// <param name="colName">JqGrid英文属性json字符串</param>
        /// <param name="colTitle">JqGrid中文属性标题json字符串</param>
        /// <returns>导出Excel需要的DataTable</returns>
        public static DataTable DictionaryToDataTable(IList<Dictionary<string,string>> entitys, string colName, string colTitle)
        {
            var mySerializer = new JavaScriptSerializer();
            var colsName = mySerializer.Deserialize<string[]>(colName);
            var colsTitle = mySerializer.Deserialize<string[]>(colTitle);
            //取出第一个实体的所有Propertie
            //var entityType = entitys[0].GetType();
            //var entityProperties = entityType.GetProperties();
            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            var dt = new DataTable();
            for (int i = 0; i < colsName.Length; i++)
            {
                //填充列标题到数据集中
                dt.Columns.Add(colsTitle[i]);
            }
            //将所有entity添加到DataTable中
            foreach (var entity in entitys)
            {
                object[] entityValues = new object[colsName.Length];
                foreach(var dic in entity)
                {
                    for (int k = 0; k < colsName.Length; k++)
                    {
                        //判断当前属性名称和传入的属性名称一致
                        if (dic.Key == colsName[k])
                        {
                            //填充该属性对应的集合
                            entityValues[k] = dic.Value;
                        }
                    }
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }

        #endregion
    }
}