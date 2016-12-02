using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public static class DataPool
{
    public static string path()
    {
        string returnpath = "";
        DirectoryInfo ParentDirectory = new DirectoryInfo(Application.dataPath);
        ParentDirectory = ParentDirectory.Parent;
        returnpath = ParentDirectory.ToString();
        returnpath = returnpath + "/Resources/"+"Items/";
        Debug.Log(returnpath);
        return returnpath;
    }


    public static Dictionary<string, ItemData> ItemList = new Dictionary<string, ItemData>();

    public static void Start()
    {
        //LoadItems();

    }

    public static void LoadItem(int ID)
    {
        if (!ItemList.ContainsKey(ID.ToString()))
        {
            ItemData tmp = ItemData.Load(ID);
            ItemList.Add(tmp.id.ToString(), tmp);
        }
    }
}
