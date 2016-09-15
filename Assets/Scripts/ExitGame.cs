using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour {

	// Use this for initialization
	public void Exit () {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;     
        #else
            Application.Quit();
        #endif
    }

}
