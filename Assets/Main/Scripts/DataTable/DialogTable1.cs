using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KEngine.Table;
using System;

namespace KEngine.Table
{
    public partial class TableRow
    {
        public List<int> Get_List_Int32(string value, string defaultValue)
        {
            return GetList<int>(value, defaultValue);
        }

        public List<T> GetList<T>(string value, string defaultValue)
        {
            var dict = new List<T>();
            var str = Get_String(value, defaultValue);
            var arr = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in arr)
            {
          
                var itemValue = ConvertString<T>(item);
                dict.Add (itemValue);
            }
            return dict;
        }
    }

}




public class DialogTable1 : TableRow
{
    public int index;
    public string text;
    public int type;
    public List<int> nextIndices = new List<int>();
    public int imageIndex;


    public DialogTable1(int rowNumber, Dictionary<string, HeaderInfo> headerInfos) : base(rowNumber, headerInfos)
    {
    }
    public override bool IsAutoParse
    {
        get
        {
            return true;
        }
    }
}
