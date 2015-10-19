using UnityEngine;
using System.Collections;

public class NewScene : MonoBehaviour {
	public Transform playerPrefab;
	public GameObject NetworkStartPosition;
	// Use this for initialization
	public void Start () {
		NetworkStartPosition = GameObject.Find ("Spawn");
		OnConnectedToServer ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnConnectedToServer() {
		Network.Instantiate(playerPrefab, NetworkStartPosition.transform.position, NetworkStartPosition.transform.rotation, 0);
	}
}
