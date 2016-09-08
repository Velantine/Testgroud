using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyChanger : MonoBehaviour {

    string preKey;
    string newKey;
    public Keys keys;
    public bool captureKey;



    void Update()
    {
        ReassignKey();
    }

    void ReassignKey()
    {
        if (!captureKey) return;

        // ignore frames without keys
        if (Input.inputString.Length <= 0) return;

        // Input.inputString stores all keys pressed
        // since last frame but I only want to use a single
        // character.
        string originalKey = newKey;
        newKey = Input.inputString[0].ToString();
        captureKey = false;

        Debug.Log("Reassigned key '" + originalKey + "' to key '" + newKey + "'");

        for (int i = 0; i <= keys.keyName.Length; i++) {
            if (preKey==keys.keyName[i]) {
                keys.keyName[i] = newKey;
                keys.Start();
                break;
            }
        }
    }


    public void ChangeKey() {
        captureKey = true;
        preKey = gameObject.GetComponentInChildren<Text>().text;
    }


 

}
