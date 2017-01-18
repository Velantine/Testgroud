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
    public string nameOfPlayer;
    [XmlElement("XP")]
    public int xp;
    [XmlElement("Attribute")]
    public int attribute;
    [XmlElement("Head_Amor")]
    public int headAmor;
    [XmlElement("Body_Amor")]
    public int bodyAmor;
    [XmlElement("Leg_Amor")]
    public int legAmor;



    public PlayerData()
    {
        // Standart  Konstructor für die XML Serialize Funktion (Muss auf jeden Fall vorhanden bleiben!
    }

    public PlayerData(int pid, string pname, int pxp, int pattribute) {
        this.id = pid;
        this.nameOfPlayer = pname;
        this.xp = pxp;
        this.attribute = pattribute;
        this.headAmor = 1;
        this.bodyAmor = 3;
        this.legAmor = 1;
    }

    public void Save()
    {
        string Path = DataPool.pathp() + this.nameOfPlayer + ".pdata";
        var serializer = new XmlSerializer(typeof(PlayerData));
        var stream = new FileStream(Path, FileMode.Create);
        serializer.Serialize(stream, this);
        stream.Close();
    }
    public static PlayerData Load(string pname)
    {
        string Path = DataPool.pathp() + pname + ".pdata";
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
            container.nameOfPlayer = pname;
            container.Save();
        }
        return container;

    }


    public void LoadToPlayer() {
        PlayerInfo pi = GameObject.Find(nameOfPlayer).GetComponent<PlayerInfo>();
        pi.nameOfPlayer = this.nameOfPlayer;
        pi.xp = this.xp;
        pi.attribute = this.attribute;
        pi.headAmor = this.headAmor;
        pi.bodyAmor = this.bodyAmor;
        pi.legAmor = this.legAmor;
    }
}