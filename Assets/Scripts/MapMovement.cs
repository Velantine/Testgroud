using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MapMovement : NetworkBehaviour {

	public float speed;
	public float zoomSpeed;
	public GameObject Galaxy;
	public GameObject[] Sun;
	public GameObject[] SunButtons;
	public GameObject GalaxyButtons;
	public int thisSun;

	// Use this for initialization

	// Update is called once per frame
	void Update () {
		//if(isLocalPlayer){
		Vector2 movement = new Vector2 (Input.GetAxis("Horizontal")*speed*Time.deltaTime,Input.GetAxis("Vertical")*speed*Time.deltaTime);
		this.transform.Translate (movement);
		Vector3 zoom = new Vector3 (0, 0, Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime);
		this.transform.Translate (zoom);
		//}
			
		if(this.transform.position.z<(-200)) {
			print("Goto Galaxymap");
			Galaxy.SetActive (true);
			GalaxyButtons.SetActive (true);
			Sun[thisSun].SetActive (false);
			SunButtons[thisSun].SetActive (false);
		}
	}
}
