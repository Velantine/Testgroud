using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Screen : MonoBehaviour {

	/*public bool c;
	void Start(){
		GameObject NetMan = GameObject.Find ("Networkmanager");
		c = NetMan.GetComponent<NetworkManagerHUD>().showGUI;
	}
	*/
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
			Application.LoadLevel (1);
		}
	}
}
