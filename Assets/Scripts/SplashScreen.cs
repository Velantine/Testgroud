using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour {

    public GameObject[] splashes;
    public int switchTime;
    public bool b;

    IEnumerator WaitS()
    {
        yield return new WaitForSeconds(switchTime);
    }

    void Start () {
        for (int i = 0; i < splashes.Length - 2; i++)
        {
            splashes[i].SetActive(true);
            StartCoroutine(WaitS());
            splashes[i].SetActive(false);
        }
        splashes[splashes.Length-1].SetActive(true);
        splashes[splashes.Length-2].SetActive(true);
	}
	
}
