using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("ObjectData")]
public class Object{
    [XmlElement("name")]
    public int name;
    [XmlElement("posX")]
    public float posx;
    [XmlElement("posY")]
    public float posy;
    [XmlElement("posZ")]
    public float posz;
    [XmlElement("rotX")]
    public float rotx;
    [XmlElement("rotY")]
    public float roty;
    [XmlElement("rotZ")]
    public float rotz;
    [XmlElement("rotW")]
    public float rotw;
    [XmlElement("obj")]
    public string obj;

    public Object() {}

    public Object(int pName, Transform trans, string pObj) {
        name = pName;
        posx = trans.position.x;
        posy = trans.position.y;
        posz = trans.position.z;
        rotx = trans.rotation.x;
        roty = trans.rotation.y;
        rotz = trans.rotation.z;
        rotw = trans.rotation.w;
        obj = pObj;
    }

    public void Save()
    {
        string Path = DataPool.patho() + name.ToString() + ".odata";
        var serializer = new XmlSerializer(typeof(Object));
        var stream = new FileStream(Path, FileMode.Create);
        serializer.Serialize(stream, this);
        stream.Close();
    }
    public static Object Load(int id)
    {
        string Path = DataPool.patho() + id.ToString() + ".odata";
        Object container;
        var serializer = new XmlSerializer(typeof(Object));
        if (File.Exists(Path))
        {
            var stream = new FileStream(Path, FileMode.Open);
            container = serializer.Deserialize(stream) as Object;
            stream.Close();
            container.Save();

        }
        else
        {
            container = new Object();
            container.name = id;
            container.Save();
        }
        return container;

    }
}
