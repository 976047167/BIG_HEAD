using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//DataTableBase
//DataTableParser     DataTableParser.Parser<DialogTable>();
//DataTableManager  统一加载

public class DialogTable  {

    static DialogTable instance = null;

    private List<Dialog> listDialog = new List<Dialog>();


    public static DialogTable getInstance()
    {
        if (instance == null)
            instance = new DialogTable();
        return instance;
    }
    public int loadTsvFile(string filePath)
    {
        WWW config = new WWW(filePath);
        if (config == null)
        {
            Debug.Log("loadFile failed!");
            return 1;
        }
        //Resources.Load<TextAsset>();
        listDialog.Clear();
        MemoryStream stream = new MemoryStream(config.bytes);
        StreamReader sr = new StreamReader(stream);
        string strLine = "";
        string[] aryLine = null;//每行文本数组
        int columnCount = 0;
        //逐行读取TSV中的数据
        while ((strLine = sr.ReadLine()) != null)
        {
            columnCount++;
            if (columnCount <= 4)
                continue;
            aryLine = strLine.Split('\t');
            int index;
            int.TryParse(aryLine[1], out index);
            int type;
            int.TryParse(aryLine[3], out type);
            List<int>  nextIndices = new List<int>();
            string[] indexArry = aryLine[4].Split(',');
            foreach(string i in indexArry)
            {
                int _index;
                int.TryParse(i, out _index);
                nextIndices.Add(_index);
            }
           
            int imageIndex;
            int.TryParse(aryLine[5], out imageIndex);

            Dialog temp = new Dialog(index, aryLine[2], type, nextIndices, imageIndex);
            listDialog.Add(temp);

        }

        sr.Close();
        return 0;
    }

    public string getDialogString(int index) {
        Dialog i = GetDialog(index);
        if (i == null)
        {
            Debug.Log("getSting fail!");
            return "";
        }
            
        return i.getString();
    }

    public List<int> getDialogNextIndices(int index)
    {
        Dialog i = GetDialog(index);
        if (i == null)
        {
            Debug.Log("getNextIndices fail!");
            return null;
        }

        return i.getNextIndices();
    }
    public int getDialogType(int index)
    {
        Dialog i = GetDialog(index);
        if (i == null)
        {
            Debug.Log("getNextIndex fail!");
            return 0;
        }

        return i.getType();
    }




    ~DialogTable()
    {
        instance = null;
    }




    private Dialog GetDialog(int index)
    {
        foreach(Dialog i in listDialog)
        {
            if (i.getIndex() == index)
                return i;
        }
        Debug.Log("index has no Dialog,it's maybe wrong !");
        return null;
    }


    private class Dialog
    {
        private int index;
        private string text;
        private int type;
        private List<int> nextIndices = new List<int>();
        private int imageIndex;
        public Dialog(int index, string text, int type, List<int> nextIndices, int imageIndex)
        {
            this.index = index;
            this.text = text;
            this.type = type;
            this.nextIndices = nextIndices;
            this.imageIndex = imageIndex;
        }

        public int getIndex()
        {
            return this.index;
        }

        public string getString()
        {
            return this.text;
        }

        public List<int> getNextIndices()
        {
            return nextIndices;
        }
        public int getType()
        {
            return type;
        }
    }
}

