using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CharMovement : MonoBehaviour {

	public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 movement = new Vector3 (Input.GetAxis("Horizontal")*speed*Time.deltaTime,Input.GetAxis("Jump")*speed*Time.deltaTime,Input.GetAxis("Vertical")*speed*Time.deltaTime);
		this.transform.Translate (movement);
	}
}
