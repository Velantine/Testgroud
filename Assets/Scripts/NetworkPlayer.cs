using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class NetworkPlayer : NetworkBehaviour
{

    public GameObject Camera;
    public GameObject Head;


    [HideInInspector]
    public NetworkMovement movement;
    [HideInInspector]
    public NetworkHealth health;
	public Transform weaponSpot;

    //[HideInInspector]
    //public CharacterController CharacterController;

    void Awake()
    {
        movement = GetComponent<NetworkMovement>();
        health = GetComponent<NetworkHealth>();
		Options opt = GameObject.Find ("Options").GetComponent<Options> ();
		if (!String.Equals(opt.name, "")) {
			gameObject.name = opt.nameOfPlayer;
		} else {
			gameObject.name ="Player";
		}
		GameObject weaponO = Instantiate (opt.weapons [opt.weapon], weaponSpot.position, Quaternion.identity)as GameObject;
		weaponO.transform.SetParent (GetComponentInChildren<Camera>().gameObject.transform);
		weaponO.transform.rotation=Quaternion.Euler(270,0,90);
		gameObject.GetComponent<NetworkGun> ().WeaponUpdateInfo ();
    }

    void Start()
    {	

		//CharacterController = this.GetComponent<CharacterController> ();
		

        this.GetComponent<FirstPersonController>().enabled = isLocalPlayer;
		//this.GetComponent<CharMovement>().enabled=isLocalPlayer;
		//this.GetComponent<RigidbodyFirstPersonController> ().enabled = isLocalPlayer;
		Camera.GetComponent<AudioListener>().enabled = isLocalPlayer;
        Camera.GetComponent<Camera>().enabled = isLocalPlayer;

    }



}
