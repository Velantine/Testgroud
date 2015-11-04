using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {
	public float Dist;
	public int AmmunitionC;


	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		var hit = new RaycastHit();
		if (Physics.Raycast (Camera.main.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0f)),out hit, Dist)) {
			var tag = hit.transform.tag;
			switch(tag){
				case "Collect":
					if (Input.GetButton("Enter")){
						DestroyImmediate(hit.transform.gameObject);
						gameObject.GetComponent<NetworkGun>().Ammunition = gameObject.GetComponent<NetworkGun>().Ammunition+AmmunitionC;
					}
					break;
				case "Vehicle":
					break;
			}
		}
	}
}
