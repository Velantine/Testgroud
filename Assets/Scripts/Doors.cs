using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class Doors : MonoBehaviour {

	public Animator animator;
	bool doorOpen;
    public AudioClip doorAudio;
    public AudioMixerGroup amg;

	void Start()
	{
		doorOpen = false;
        animator = GetComponentInChildren<Animator>();
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			doorOpen = true;
			DoorControl ("Open");
        }
	}

	void OnTriggerExit(Collider col)
	{
		if(doorOpen)
		{
			doorOpen = false;
			DoorControl ("Close");
		}
	}



	void DoorControl(string direction)
	{
		animator.SetTrigger(direction);
        DoorSound();
    }


    void DoorSound()
    {
        AudioSource door = gameObject.AddComponent<AudioSource>();
        door.outputAudioMixerGroup = amg;
        door.clip = doorAudio;
        door.Play();
        Destroy(door, 2);
    }
}

