using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Options")]
public class OptionsClass{

    [Range(-80, 20)]
    [XmlElement("SoundVolume")]
    public float soundVolume;
    [Range(-80, 20)]
    [XmlElement("MusicVolume")]
    public float musicVolume;
    [XmlElement("Mute")]
    public bool mute;


    public void Save()
    {
        string Path = DataPool.optionPath();
        var serializer = new XmlSerializer(typeof(OptionsClass));
        var stream = new FileStream(Path, FileMode.Create);
        serializer.Serialize(stream, this);
        stream.Close();
    }
    public static OptionsClass Load()
    {
        string Path = DataPool.optionPath();
        OptionsClass container;
        var serializer = new XmlSerializer(typeof(OptionsClass));
        if (File.Exists(Path))
        {
            var stream = new FileStream(Path, FileMode.Open);
            container = serializer.Deserialize(stream) as OptionsClass;
            stream.Close();
            container.Save();

        }
        else
        {
            container = new OptionsClass();
            container.Save();
        }
        return container;

    }

    public OptionsClass(float sound, float music, bool mu) {
        this.soundVolume = sound;
        this.musicVolume = music;
        this.mute = mu;
    }
    public OptionsClass() { }
}
