using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class onLoad :MonoBehaviour {
	// Use this for initialization
	void Start(){
		ClientScene.AddPlayer (0);
        DataPool.LoadItem(1);
	}
}
