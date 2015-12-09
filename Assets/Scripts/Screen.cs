using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Screen : NetworkBehaviour {

	void Start(){
		screenLock=true;
	}
	
	public bool screenLock=false;

	void Update () {
		if (screenLock) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		if(!screenLock){
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		if(Input.GetButtonDown ("Chat")&&isLocalPlayer) {
			if(screenLock==false){
				screenLock=true;
			}
			else{
				screenLock=false;
			}

		}
		if (Input.GetButtonDown ("Map")&&isLocalPlayer) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			screenLock=false;
			Application.LoadLevel (1);
		}
	}
}
