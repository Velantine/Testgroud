﻿using UnityEngine;
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
        return returnpath;
    }

    public static string pathp()
    {
        string returnpath = "";
        DirectoryInfo ParentDirectory = new DirectoryInfo(Application.dataPath);
        ParentDirectory = ParentDirectory.Parent;
        returnpath = ParentDirectory.ToString();
        returnpath = returnpath + "/Resources/" + "Players/";
        return returnpath;
    }

    public static string patho()
    {
        string returnpath = "";
        DirectoryInfo ParentDirectory = new DirectoryInfo(Application.dataPath);
        ParentDirectory = ParentDirectory.Parent;
        returnpath = ParentDirectory.ToString();
        returnpath = returnpath + "/Resources/" + "Objects/";
        return returnpath;
    }

    public static string optionPath()
    {
        string returnpath = "";
        DirectoryInfo ParentDirectory = new DirectoryInfo(Application.dataPath);
        ParentDirectory = ParentDirectory.Parent;
        returnpath = ParentDirectory.ToString();
        returnpath = returnpath + "/Resources/Options.xml";
        return returnpath;
    }



    public static Dictionary<string, ItemData> ItemList = new Dictionary<string, ItemData>();
    public static Dictionary<string, PlayerData> PlayerList = new Dictionary<string, PlayerData>();
    public static Dictionary<string, Object> ObjectList = new Dictionary<string, Object>();
    public static Dictionary<string, Options> OptionList = new Dictionary<string, Options>();

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

    public static void LoadPlayer(int ID)
    {
        if (!PlayerList.ContainsKey(ID.ToString()))
        {
            PlayerData tmp = PlayerData.Load(ID);
            PlayerList.Add(tmp.id.ToString(), tmp);
        }
    }

    public static void LoadObject(int ID)
    {
        if (!ObjectList.ContainsKey(ID.ToString()))
        {
            Object tmp = Object.Load(ID);
            ObjectList.Add(tmp.name.ToString(), tmp);
        }
    }
}
