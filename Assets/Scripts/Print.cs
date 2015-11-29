using UnityEngine;
using System.Collections;

public class Print : MonoBehaviour {
	public string printText;
	// Use this for initialization
	public void PrintOut() {
		Debug.Log(printText);
	}

}
