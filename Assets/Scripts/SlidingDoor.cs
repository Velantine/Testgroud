using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour {

    public Transform door;
    public int switchTime;
    public float speed;
    public bool opened;

    public Transform start;
    public Transform ziel;


    public void open() {
        while (opened == false) {
            float step = speed * Time.deltaTime;
            door.transform.position = Vector3.MoveTowards(door.position, ziel.position, step);
            if (door.position == ziel.position) {
                opened = true;
            }
        }

        StartCoroutine(WaitS());

        
}
        

    IEnumerator WaitS()
    {
        yield return new WaitForSecondsRealtime(switchTime);
        while (opened == true)
        {
            float step2 = speed * Time.deltaTime;
            door.transform.position = Vector3.MoveTowards(door.position, start.position, step2);
            if (door.position == start.position)
            {
                opened = false;
            }
        }
    }

}
