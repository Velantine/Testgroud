using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("ItemData")]
public class ItemData
{
    [XmlElement("Id")]
    public int id;   //Name des Items
    [XmlElement("Weight")]
    public float weight; //Gewicht des Items
    [XmlElement("SlotID")]
    public int slotID; //Für welchen Slot des Players das Item geeignet ist
    [XmlElement("Attribute")]
    public int attribute; 


    /*################################################
    [XmlElement("stat_str")]
    public int stat_str = new int(); // stärke
    [XmlElement("stat_int")]
    public int stat_int = new int(); // intelligenz
    [XmlElement("stat_dex")]
    public int stat_dex = new int(); // jagdgeschick
    [XmlElement("stat_vit")]
    public int stat_vit = new int(); // vitalität

    //################################################
    */

    public ItemData()
    {
        // Standart  Konstructor für die XML Serialize Funktion (Muss auf jeden Fall vorhanden bleiben!
    }

    public void Save()
    {
        string Path = DataPool.path() + id.ToString() + ".idata";
        var serializer = new XmlSerializer(typeof(ItemData));
        var stream = new FileStream(Path, FileMode.Create);
        serializer.Serialize(stream, this);
        stream.Close();
    }
    public static ItemData Load(int id)
    {
        string Path = DataPool.path() + id.ToString() + ".idata";
        ItemData container;
        var serializer = new XmlSerializer(typeof(ItemData));
        if (File.Exists(Path))
        {
            var stream = new FileStream(Path, FileMode.Open);
            container = serializer.Deserialize(stream) as ItemData;
            stream.Close();
            container.Save();

        }
        else
        {
            container = new ItemData();
            container.id = id;
            container.Save();
        }
        return container;

    }
}