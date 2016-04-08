using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

[NetworkSettings(channel = 0)]
public class NetworkMovement : NetworkBehaviour
{

    [SerializeField] private float positionLerpStep = 15f;
    [SerializeField] private float rotationLerpStep = 15f;

    [SerializeField] private float minPosDist = 1f;
    [SerializeField] private float minBodyRotDist = 1f;
    [SerializeField] private float minHeadRotDist = 1f;

    [SyncVar] private Vector3 syncPosition;
    [SyncVar] private float syncBodyRotation;
    [SyncVar] private float syncHeadRotation;

    private Vector3 lastSentPosition;
    private float lastSentBodyRotation;
    private float lastSentHeadRotation;

    private NetworkPlayer player;

    void Start()
    {
        player = GetComponent<NetworkPlayer>();
    }


	
	void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            Vector3 pos = this.transform.position;
            float bodyRot = this.transform.eulerAngles.y;
            float headRot = player.Camera.transform.rotation.eulerAngles.x;
            if (IsOverThreshold(lastSentPosition, pos, minPosDist) || IsOverThreshold(lastSentBodyRotation, bodyRot, minBodyRotDist) || IsOverThreshold(lastSentHeadRotation, headRot, minHeadRotDist))
            {
                CmdSendData(pos, bodyRot, headRot);
                lastSentPosition = pos;
                lastSentBodyRotation = bodyRot;
                lastSentHeadRotation = headRot;
            }
            
        }
        else
        {
            Interpolate();
        }
    }

    [Client]
    public bool IsOverThreshold(Vector3 old, Vector3 last, float min)
    {
        return Vector3.Distance(old, last) > min;
    }

    [Client]
    public bool IsOverThreshold(float old, float last, float min)
    {
        return Mathf.Abs(old - last) > min;
    }

    [Client]
    private void Interpolate()
    {
        Vector3 newPos = Vector3.Lerp(this.transform.position, syncPosition, Time.deltaTime * positionLerpStep);
        Vector3 movement = newPos - this.transform.position;
        //player.CharacterController.Move(movement);
		player.transform.Translate(movement);

        float newBodyRot = Mathf.LerpAngle(this.transform.eulerAngles.y, syncBodyRotation, Time.deltaTime * rotationLerpStep);
        this.transform.rotation = Quaternion.Euler(0, newBodyRot, 0);

        float newHeadRot = Mathf.LerpAngle(player.Head.transform.eulerAngles.x, syncHeadRotation, Time.deltaTime * rotationLerpStep);
        player.Head.transform.localRotation = Quaternion.Euler(newHeadRot, 0, 0);
    }

    [Command]
    private void CmdSendData(Vector3 pos, float bodyRot, float headRot)
    {
        syncPosition = pos;
        syncBodyRotation = bodyRot;
        syncHeadRotation = headRot;
    }

}
