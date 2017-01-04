using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Options")]
public class Options : MonoBehaviour {

    void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	[Range(-80, 20)]
    [XmlElement("SoundVolume")]
    public float soundVolume;
	[Range(-80, 20)]
    [XmlElement("MusicVolume")]
    public float musicVolume;
    [XmlElement("Mute")]
    public bool mute;

    [XmlIgnore]
	public AudioMixer master;
    [XmlIgnore]
    public string name;
    [XmlIgnore]
    public int weapon;
    [XmlIgnore]
    public GameObject[] weapons;

	void Start(){
		gameObject.GetComponent<AudioSource> ().enabled = true;
	}

	void Update(){
		if(musicVolume==-20){
			master.SetFloat ("Music", -80f);
		}
		else if(soundVolume==-20){
			master.SetFloat ("Sound", -80f);
		}
		else{
			master.SetFloat ("Music", musicVolume);
			master.SetFloat ("Sound", soundVolume);
		}

		if (mute == true) {
			master.SetFloat ("Master", -80);
		} else {
			master.SetFloat ("Master", 0);
		}

	}

	public void NumberUpdate(){
		mute = GameObject.Find ("ToggleMute").GetComponent<Toggle> ().isOn;
		musicVolume = GameObject.Find ("SliderMusic").GetComponent<Slider> ().value;
		soundVolume = GameObject.Find ("SliderSound").GetComponent<Slider> ().value;
		GameObject.Find ("MusicTextNumber").GetComponent<Text> ().text = (musicVolume/10).ToString("##0.0 %");
		GameObject.Find ("SoundTextNumber").GetComponent<Text> ().text = (soundVolume/10).ToString("##0.0 %");
	}


    //+++++++++++++++++++++++++++++

    public void Save()
    {
        string Path = DataPool.optionPath();
        var serializer = new XmlSerializer(typeof(Options));
        var stream = new FileStream(Path, FileMode.Create);
        serializer.Serialize(stream, this);
        stream.Close();
    }
    public static Options Load()
    {
        string Path = DataPool.optionPath();
        Options container;
        var serializer = new XmlSerializer(typeof(Options));
        if (File.Exists(Path))
        {
            var stream = new FileStream(Path, FileMode.Open);
            container = serializer.Deserialize(stream) as Options;
            stream.Close();
            container.Save();

        }
        else
        {
            container = new Options();
            container.Save();
        }
        return container;

    }

}
