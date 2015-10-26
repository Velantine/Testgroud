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
//    [HideInInspector]
//    public NetworkHealth health;

    [HideInInspector]
    public CharacterController CharacterController;

    void Awake()
    {
        movement = GetComponent<NetworkMovement>();
//        health = GetComponent<NetworkHealth>();
    }

    void Start()
    {	

		CharacterController = this.GetComponent<CharacterController> ();
		

        this.GetComponent<FirstPersonController>().enabled = isLocalPlayer;
        Camera.GetComponent<AudioListener>().enabled = isLocalPlayer;
        Camera.GetComponent<Camera>().enabled = isLocalPlayer;

    }



}
