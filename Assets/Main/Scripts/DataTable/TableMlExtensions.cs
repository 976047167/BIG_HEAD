using System;
using System.Collections.Generic;
using TableML;

    public static class TableMlExtensions
    {
        public static List<int> Get_List_int(this TableFileRow tableRow, string value, string defaultValue)
        {
            return tableRow.GetList<int>(value, defaultValue);
        }

        public static List<T> GetList<T>(this TableFileRow tableRow, string value, string defaultValue)
        {
            var list = new List<T>();
            var str = tableRow.Get_String(value, defaultValue);
            var arr = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in arr)
            {
                var itemValue = (T)Convert.ChangeType(value, typeof(T));
                list.Add(itemValue);
            }
            return list;
        }
    }
