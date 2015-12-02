using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetPlanetInfo : MonoBehaviour {

	public string planetName;
	public string planetInfo;
	public int sceneId;

	Text pName;
	Text pInfo;
	// Use this for initialization
	public void LoadInfo () {
		pName = GameObject.Find("PName").GetComponent<Text>();
		pInfo = GameObject.Find("PInfo").GetComponent<Text>();
		SwitchSceen travelButtonSS = GameObject.Find("Button_Travel").GetComponent<SwitchSceen>();
		pName.text = planetName;
		pInfo.text = planetInfo;
		travelButtonSS.level = sceneId;

	}
}
