using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SwitchSceen : NetworkBehaviour {
	public int level;
	public void Scene () {
		Application.LoadLevel (level);
	}
}
