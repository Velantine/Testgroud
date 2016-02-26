using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Options : MonoBehaviour {
	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	[Range(-80, 20)]
	public float soundVolume;
	[Range(-80, 20)]
	public float musicVolume;

	public AudioMixer master;

	//public AudioSource music;

	// Use this for initialization
	void Start () {
		NumberUpdate ();
		UpdateOnChange ();
	}
		
	// Update is called once per frame
	public void UpdateOnChange () {
		master.SetFloat ("Music", musicVolume);
		master.SetFloat ("Sound", soundVolume);
	}

	void Update(){
		if(musicVolume==-20){
			master.SetFloat ("Music", -80f);
		}
		if(soundVolume==-20){
			master.SetFloat ("Sound", -80f);
		}
		else{
			master.SetFloat ("Music", musicVolume);
			master.SetFloat ("Sound", soundVolume);
		}
	}

	public void NumberUpdate(){
		
		musicVolume = GameObject.Find ("SliderMusic").GetComponent<Slider> ().value;
		soundVolume = GameObject.Find ("SliderSound").GetComponent<Slider> ().value;
		GameObject.Find ("MusicTextNumber").GetComponent<Text> ().text = (musicVolume/10).ToString("###.0 %");
		GameObject.Find ("SoundTextNumber").GetComponent<Text> ().text = (soundVolume/10).ToString("###.0 %");
	}
}
