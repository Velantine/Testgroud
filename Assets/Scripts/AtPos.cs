using UnityEngine;
using System.Collections;

public class AtPos : MonoBehaviour {

	public GameObject targetObject;
	public GameObject thisTarget;

	// Use this for initialization
	void Start () {
		thisTarget = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, targetObject.transform.position.z);
	}
}
