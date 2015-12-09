using UnityEngine;
using System.Collections;

public class rotation : MonoBehaviour {
	public float addX;
	public float addY;
	public float addZ;

	void Update(){
		transform.Rotate (addX,addY,addZ);
	}
}
