using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MapMovement : NetworkBehaviour {

	public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//if(isLocalPlayer){
		Vector2 movement = new Vector2 (Input.GetAxis("Horizontal")*speed*Time.deltaTime,Input.GetAxis("Vertical")*speed*Time.deltaTime);
		this.transform.Translate (movement);
		//}
	}
}
