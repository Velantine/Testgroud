using UnityEngine;
using System.Collections;

public class openURL : MonoBehaviour {
	public string url;
	public void OpenWeb(){
		Application.OpenURL(url);
	}
}