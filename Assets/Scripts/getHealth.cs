using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class getHealth: MonoBehaviour {
	
	public GameObject ThisO;
	public Slider healthSlider;
	public Text deathCounter;

	/*void Start(){
		ThisO = this.gameObject;
		healthSlider= GameObject.Find("HealthSlider").GetComponent<Slider>();
		deathCounter= GameObject.Find("deathCounter").GetComponent<Text>();
	}*/

	void Update () {
		NetworkHealth HScript = ThisO.GetComponent<NetworkHealth>();
		healthSlider.value = HScript.Health;
		deathCounter.text = "Deaths: " + HScript.deaths;
	}

}