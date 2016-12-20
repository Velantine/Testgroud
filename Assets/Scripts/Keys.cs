using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keys : MonoBehaviour {

    public string[] inputName;
    public string[] keyName;

    public Text[] inputs;
    public Text[] keys;

    public void Start() {
        for (int i=0;i<inputs.Length;i++) {
            inputs[i].text = inputName[i];
            keys[i].text = keyName[i];
        }
    }

    
}
