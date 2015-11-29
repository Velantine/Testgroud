using UnityEngine;
using System.Collections;

public class SwitchSceen : MonoBehaviour {
	public int Level;
	// Use this for initialization
	public void Scene () {
		Application.LoadLevel (Level);
	}
}
