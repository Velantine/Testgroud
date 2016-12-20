using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("PlayerData")]
public class PlayerData
{
    [XmlElement("Id")]
    public int id;
    [XmlElement("Name")]
    public string name;
    [XmlElement("XP")]
    public int xp;
    [XmlElement("Attribute")]
    public int attribute; 



    public PlayerData()
    {
        // Standart  Konstructor für die XML Serialize Funktion (Muss auf jeden Fall vorhanden bleiben!
    }

    public PlayerData(int pid, string pname, int pxp, int pattribute) {
        this.id = pid;
        this.name = pname;
        this.xp = pxp;
        this.attribute = pattribute;
    }

    public void Save()
    {
        string Path = DataPool.pathp() + id.ToString() + ".pdata";
        var serializer = new XmlSerializer(typeof(PlayerData));
        var stream = new FileStream(Path, FileMode.Create);
        serializer.Serialize(stream, this);
        stream.Close();
    }
    public static PlayerData Load(int id)
    {
        string Path = DataPool.pathp() + id.ToString() + ".pdata";
        PlayerData container;
        var serializer = new XmlSerializer(typeof(PlayerData));
        if (File.Exists(Path))
        {
            var stream = new FileStream(Path, FileMode.Open);
            container = serializer.Deserialize(stream) as PlayerData;
            stream.Close();
            container.Save();

        }
        else
        {
            container = new PlayerData();
            container.id = id;
            container.Save();
        }
        return container;

    }
}