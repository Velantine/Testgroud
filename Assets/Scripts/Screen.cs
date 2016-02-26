using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Screen : NetworkBehaviour {

	void Start(){
		screenLock=true;
	}
	public bool escapeM=false;
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
			screenLock=false;
			Application.LoadLevel (1);
		}
//		if(Input.GetButtonDown("Cancel")&&isLocalPlayer){
//			if (escapeM == false) {
//				escapeM = true;
//				screenLock = false;
//				GameObject.Find ("UI").SetActive (false);
//				GameObject.Find ("EscapeM").SetActive (true);
//				GameObject.Find ("Options").GetComponent<Options> ().NumberUpdate ();
//			}
//			if (escapeM == true) {
//				escapeM = false;
//				screenLock = true;
//				GameObject.Find ("UI").SetActive (true);
//				GameObject.Find ("EscapeM").SetActive (false);
//			}
//		}
	}
}
