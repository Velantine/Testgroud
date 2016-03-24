using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Video : MonoBehaviour {


	public MovieTexture movie;
	public AudioSource audio;
	public bool loop;

	// Use this for initialization
	void Start () {
		GetComponent<RawImage> ().texture = movie as MovieTexture;
		audio.clip = movie.audioClip;
		movie.Play ();
		audio.Play();
		movie.loop = loop;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
