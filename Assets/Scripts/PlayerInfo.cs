using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {


    public string nameOfPlayer;
    public int xp;
    public int attribute;
    public int headAmor;
    public int bodyAmor;
    public int legAmor;

    public int amor() {
        return (bodyAmor + legAmor + headAmor);
    }

}
