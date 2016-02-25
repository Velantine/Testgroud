using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;
	
public class NetworkSpaceship : NetworkBehaviour{
		
	public GameObject Camera;
		
		
	[HideInInspector]
	public Spaceship spacemovement;
	[HideInInspector]
	public NetworkHealth health;
		
	[HideInInspector]
	public CharacterController CharacterController;
		
	void Awake()
	{
		spacemovement = GetComponent<Spaceship>();
		health = GetComponent<NetworkHealth>();
	}
		
	void Start()
	{	
			
		CharacterController = this.GetComponent<CharacterController> ();
			
			
		this.GetComponent<FirstPersonController>().enabled = isLocalPlayer;
		Camera.GetComponent<AudioListener>().enabled = isLocalPlayer;
		Camera.GetComponent<Camera>().enabled = isLocalPlayer;
			
	}
		
}