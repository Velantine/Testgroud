using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[NetworkSettings(channel = 1)]
public class NetworkHealth : NetworkBehaviour
{
    [Range(0, 100)]
    public float Health = 100f;


    //TODO: [Server]
    public void GetShot()
    {
        Health -= 20;
        RpcUpdateHealth(Health);
    }

    [ClientRpc(channel = 1)]
    private void RpcUpdateHealth(float health)
    {
        Health = health;
    }
}
