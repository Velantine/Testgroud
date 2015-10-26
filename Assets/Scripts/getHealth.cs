using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class getHealth: MonoBehaviour {

	public Slider Slider;
	public GameObject ThisO;


	void Update () {
		NetworkHealth HScript = ThisO.GetComponent<NetworkHealth>();
		Slider.value = HScript.Health;
	}

}