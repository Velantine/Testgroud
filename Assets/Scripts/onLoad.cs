using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class onLoad :NetworkBehaviour {
	// Use this for initialization
	void Start(){
		ClientScene.AddPlayer (0);
	}
}
