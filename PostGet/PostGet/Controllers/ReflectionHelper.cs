using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MYHelper;

using System.Web;


namespace MYHelper
{
    /// <summary>
    /// 类反射助手类
    /// </summary>
    public static class ReflectionHelper
    {
        //属性、字段的可访问类型
        private static BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Public |
                                                 BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        /**/
        /// <summary>
        /// 执行某个方法
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <param name="methodName">对象方法名称</param>
        /// <param name="args">参数</param>
        /// <returns>对象</returns>
        public static object InvokeMethod(object obj, string methodName, object[] args)
        {
            object objResult = null;
            Type type = obj.GetType();
            objResult = type.InvokeMember(methodName, bindingFlags | BindingFlags.InvokeMethod, null, obj, args);
            return objResult;
        }

        /**/
        /// <summary>
        /// 设置对象字段的值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="name">字段名</param>
        /// <param name="value">字段值</param>
        public static void SetField(object obj, string name, object value)
        {
            FieldInfo fieldInfo = obj.GetType().GetField(name, bindingFlags);
            object objValue = Convert.ChangeType(value, fieldInfo.FieldType);
            fieldInfo.SetValue(objValue, value);
        }

        /**/
        /// <summary>
        /// 获取对象字段的值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="name">字段值</param>
        public static object GetField(object obj, string name)
        {
            if (obj == null)
                return null;
            FieldInfo fieldInfo = obj.GetType().GetField(name, bindingFlags);
            return fieldInfo.GetValue(obj);
        }

        /**/
        /// <summary>
        /// 设置对象属性的值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="name">属性名</param>
        /// <param name="value">属性值</param>
        public static void SetProperty(object obj, string name, object value)
        {
            Type t = obj.GetType();
            PropertyInfo propertyInfo = obj.GetType().GetProperty(name, bindingFlags);
            //如果获取不到属性，取得基类的属性
            if (propertyInfo == null)
            {
                propertyInfo = t.BaseType.GetProperty(name, bindingFlags);
            }
            object objValue = Convert.ChangeType(value, propertyInfo.PropertyType);
            propertyInfo.SetValue(obj, objValue, null);
        }

        ///**/
        ///// <summary>
        ///// 获取对象属性的值
        ///// </summary>
        ///// <param name="obj">对象</param>
        ///// <param name="name">属性名</param>
        //public static object GetProperty(object obj, string name)
        //{
        //    PropertyInfo propertyInfo = obj.GetType().GetProperty(name, bindingFlags);
        //    //如果属性获得不到，取得基类的属性
        //    if (propertyInfo == null)
        //    {
        //        propertyInfo = obj.GetType().BaseType.GetProperty(name, bindingFlags);
        //    }

        //    return propertyInfo.GetValue(obj, null);
        //}

        public static object GetProperty(object obj, string name)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(name);
            return propertyInfo.GetValue(obj, null);
        }


        /**/
        /// <summary>
        /// 获取对象属性信息（返回属性（PropertyInfo）数组输出）
        /// </summary>
        /// <param name="obj">对象</param>
        public static PropertyInfo[] GetProperties(object obj)
        {

            PropertyInfo[] propertyInfos = obj.GetType().GetProperties(bindingFlags);
            //如果继承自Object的取上级类型的属性
            if (obj.GetType().BaseType.Name.Equals("Object"))
            {

            }
            else
            {

                PropertyInfo[] parentProps = obj.GetType().BaseType.GetProperties(bindingFlags);
                int propertyLength = 0;
                int index = 0;
                switch (propertyInfos.Length)
                {
                    case 0:
                        propertyLength = parentProps.Length;
                        index = 0;
                        break;
                    case 1:
                        propertyLength = parentProps.Length + propertyInfos.Length;
                        index = 1;
                        break;
                    default:
                        propertyLength = propertyInfos.Length + parentProps.Length;
                        index = propertyInfos.Length;
                        break;
                }
                Array.Resize<PropertyInfo>(ref propertyInfos, propertyLength);

                parentProps.CopyTo(propertyInfos, index);
            }

            return propertyInfos;
        }
        /// <summary>
        /// 动态创建对象
        /// </summary>
        /// <param name="T">类型</param>
        /// <returns>实例对象</returns>
        public static T Instance<T>()
        {
            return Activator.CreateInstance<T>();
        }

        public static string PageHtml(
//            Models.Page thePage,
            int currentPage,
            string action,
            string controller,
            HttpRequestBase request
            )
        {
            string url = "";
            foreach (var key in GetRequestAllKeys(request))
            {
                url += "&"+key+"=" + request[key];
            }

            string strAction = "/" + controller + "/" + action;

            return HTMLHelper.fenyeSimpleStr(1000, currentPage, 25, strAction, url); //  (thePage.SumCount, thePage.CurrentPage, thePage.PageSize, strAction, url);
        }


        //更新模型
        public static void UpdateModel(object model , HttpRequestBase request)
        {
            foreach (var key in GetRequestAllKeys(request))
            {
                UpdateModel(model, key, request[key]);
            }
        }

        /// <summary>
        /// 得到请求QueryString.AllKeys 和 Form.AllKeys
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static List<string> GetRequestAllKeys(HttpRequestBase request)
        {
            List<string> Keys = new List<string>();
            Keys.AddRange(request.QueryString.AllKeys);
            Keys.AddRange(request.Form.AllKeys);
            Keys = Keys.Distinct().ToList();
            return Keys;
        }


        /// <summary>
        /// 根据字段 Key和Value为Model赋值
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Key"></param>
        /// <param name="value"></param>
        public static void UpdateModel(object model, string key, object value)
        {
            PropertyInfo prop = model.GetType().GetProperty(key);
            FieldInfo fiel = model.GetType().GetField(key);

            if (prop != null)
            {
                //是否是int? 等类型
                Type columnType = prop.PropertyType;
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    columnType = prop.PropertyType.GetGenericArguments()[0];
                }
                prop.SetValue(model, Convert.ChangeType(value, columnType), null);

            }
            else if (fiel != null)
            {
                Type columnType = fiel.FieldType;
                if (fiel.FieldType.IsGenericType && fiel.FieldType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    columnType = fiel.FieldType.GetGenericArguments()[0];
                }
                fiel.SetValue(model, Convert.ChangeType(value, columnType));
            }
        }
        public static T UpdateModel<T>(object obj)
        {
            T model = Instance<T>();
            PropertyInfo[] proList = model.GetType().GetProperties();
            foreach (var m in proList)
            {
                PropertyInfo valProperty = obj.GetType().GetProperty(m.Name);

                if (valProperty != null)
                {
                    var val = valProperty.GetValue(obj, null);
                    if (val != null)
                    {
                        //判断是否是 int? 等这种 Nullable类型
                        Type columnType = m.PropertyType;
                        if (m.PropertyType.IsGenericType && m.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
                            columnType = m.PropertyType.GetGenericArguments()[0];
                        }
                        m.SetValue(model, Convert.ChangeType(val, columnType), null);
                    }
                }
            }
            return model;
        }

        public static T UpdateModel<T>(string jsonStr)
        {
            return Instance<T>();

           // return JsonConvert.DeserializeObject<T>(jsonStr);

           // T model = Instance<T>();  //获取对象
           //// PropertyInfo[] proList = GetProperties(model);
           // PropertyInfo[] proList = model.GetType().GetProperties();

           // foreach (var m in proList )
           // {
           //     //更新值  循环遍历的方式
           //     foreach (JProperty i in jobj.Children())
           //     {
           //         if (i.Name == m.Name)
           //         {
           //             if (((JValue)i.Value).Value != null)
           //             {
           //                 Type columnType = m.PropertyType;

           //                 //判断是否是 int? 等这种 Nullable类型
           //                 if (m.PropertyType.IsGenericType && m.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
           //                 {
           //                     // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
           //                     columnType = m.PropertyType.GetGenericArguments()[0];
           //                 }

           //                 m.SetValue(model, Convert.ChangeType(((JValue)i.Value).Value, columnType), null);
           //             }
           //             break;
           //         }
           //     }
           // }
            // return model;
        }
    }

}
