#pragma strict
public var S1: GameObject;
public var S2: GameObject;
public var S3: GameObject;
public var time: float;

function Start () {
    S1.SetActive(true);
	yield WaitForSeconds(time);
	S1.SetActive(false);
	S2.SetActive(true);
	S3.SetActive(true);
}