using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class OnStartUp : MonoBehaviour {
    public GameObject optionsOb;
	// Use this for initialization
	void Awake() {
        if (!Directory.Exists(DataPool.path())) {
            Directory.CreateDirectory(DataPool.path());
        }
        if (!Directory.Exists(DataPool.patho()))
        {
            Directory.CreateDirectory(DataPool.patho());
        }
        if (!Directory.Exists(DataPool.pathp()))
        {
            Directory.CreateDirectory(DataPool.pathp());
        }
        if (!GameObject.Find("Options"))
        {
            Instantiate(optionsOb);
            GameObject.Find("Options(Clone)").name = "Options";
            /*if (Application.isEditor)
            {
                GameObject.Find("Options").GetComponent<AudioSource>().enabled = false;
            }*/
        }
        GameObject.Find("Options").GetComponent<Options>().LoadOptions();
        if (!File.Exists(DataPool.optionPath()))
        {
            Options optionsObj = GameObject.Find("Options").GetComponent<Options>();
            OptionsClass options = new OptionsClass(optionsObj.soundVolume,optionsObj.musicVolume,optionsObj.mute);
            options.Save();
        }

    }
	
}
