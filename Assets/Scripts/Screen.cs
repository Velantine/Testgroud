using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Screen : MonoBehaviour {
	
	void Start(){
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		screenLock=true;
	}
	
	public bool screenLock=false;

	void Update () {
		if (Input.GetButtonDown ("Chat")) {
			if(screenLock==false){
				//c=false;
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				screenLock=true;
			}
			else{
				//c = true;
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				screenLock=false;
			}

		}
		if (Input.GetButtonDown ("Map")) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			screenLock=false;
			Application.LoadLevel (1);
		}
	}
}
