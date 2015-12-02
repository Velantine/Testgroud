using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class onLoad : NetworkBehaviour {
	public GameObject Line;
	// Use this for initialization
	void Start(){
		ClientScene.AddPlayer(0);
	}
}
