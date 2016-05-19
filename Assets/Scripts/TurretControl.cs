using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class TurretControl : MonoBehaviour {

    public GameObject lifter;
    public GameObject turret;


    public float mouseSensitivity = 100.0f;

    public float rotY = 0.0f; // rotation around the up/y axis
    public float rotX = 0.0f; // rotation around the right/x axis


    // Update is called once per frame
    public void Update () {
       
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -15,12);

        turret.transform.rotation = Quaternion.Euler(270, rotY,0);
        lifter.transform.localRotation = Quaternion.Euler(90, -rotX, 0);
    }


}
