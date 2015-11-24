using UnityEngine;
using System.Collections;

public class rotation : MonoBehaviour {
	public float add;

	void Update(){
		transform.Rotate (0,add,0);
	}
}
