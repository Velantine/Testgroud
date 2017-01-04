using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Options : MonoBehaviour {

    void Awake() {
		DontDestroyOnLoad(transform.gameObject);
        LoadOptions();
	}

	[Range(-80, 20)]
    public float soundVolume;
	[Range(-80, 20)]
    public float musicVolume;
    public bool mute;

	public AudioMixer master;
    public string name;
    public int weapon;
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
    public void Up() {
        //LoadOptions();
        GameObject.Find("ToggleMute").GetComponent<Toggle>().isOn = mute;
        GameObject.Find("SliderMusic").GetComponent<Slider>().value = musicVolume;
        GameObject.Find("SliderSound").GetComponent<Slider>().value = soundVolume;
        GameObject.Find("MusicTextNumber").GetComponent<Text>().text = (musicVolume / 10).ToString("##0.0 %");
        GameObject.Find("SoundTextNumber").GetComponent<Text>().text = (soundVolume / 10).ToString("##0.0 %");
    }

	public void NumberUpdate(){
		mute = GameObject.Find ("ToggleMute").GetComponent<Toggle> ().isOn;
		musicVolume = GameObject.Find ("SliderMusic").GetComponent<Slider> ().value;
		soundVolume = GameObject.Find ("SliderSound").GetComponent<Slider> ().value;
		GameObject.Find ("MusicTextNumber").GetComponent<Text> ().text = (musicVolume/10).ToString("##0.0 %");
		GameObject.Find ("SoundTextNumber").GetComponent<Text> ().text = (soundVolume/10).ToString("##0.0 %");
        SaveOptions();

    }

    public void LoadOptions() {
        DataPool.LoadOptions();
        OptionsClass optC;
        DataPool.OptionList.TryGetValue("Options.xml", out optC);
        this.soundVolume = optC.soundVolume;
        this.musicVolume = optC.musicVolume;
        this.mute = optC.mute;
    }

    public void SaveOptions()
    {
        OptionsClass options = new OptionsClass(this.soundVolume, this.musicVolume, this.mute);
        options.Save();
    }


}
