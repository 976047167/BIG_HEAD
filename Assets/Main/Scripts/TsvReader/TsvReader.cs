using System.Collections;using System.Collections.Generic;using System.IO;using UnityEngine;public class TsvReader : MonoBehaviour{    DataDialog[] dialogs;    public TsvReader(string filePath)    {        WWW config = new WWW(filePath);        MemoryStream stream = new MemoryStream(config.bytes);        StreamReader sr = new StreamReader(stream);        string strLine = "";        string[] aryLine = null;//每行文本数组
        int columnCount = 0;
        //逐行读取TSV中的数据
         List < DataDialog > dialogs = new List<DataDialog>();
        while ((strLine = sr.ReadLine()) != null)        {
            columnCount++;
            if (columnCount <= 4)
                continue;
            aryLine = strLine.Split('\t');
            int index;
            int.TryParse(aryLine[1], out  index);
            int type;
            int.TryParse(aryLine[3], out type);
            int nextIndex;
            int.TryParse(aryLine[4], out nextIndex);
            int imageIndex;
            int.TryParse(aryLine[5], out imageIndex);

            DataDialog temp = new DataDialog( index, aryLine[2], type, nextIndex,imageIndex);            dialogs.Add(temp);                   }
        
        sr.Close();    }}