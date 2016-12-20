using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class onLoad :MonoBehaviour {
	// Use this for initialization
	void Start(){
		ClientScene.AddPlayer (0);

        //Create Item
        ItemData i1 = new ItemData(1,20,2,50);
        i1.Save();

        //Create Player
        PlayerData p1 = new PlayerData(1, "ROB", 100, 1);
        p1.Save();

        //Load Item and Player into Dictionary
        DataPool.LoadItem(1);
        DataPool.LoadPlayer(1);

        //Get Player
        PlayerData px = null;
        DataPool.PlayerList.TryGetValue("1", out px);
        //Get Playername
        Debug.Log(px.id+": "+px.name+", "+px.xp+", "+px.attribute);

        //Create Object
        Object o1 = new Object(1, GameObject.Find("Barrel").transform, GameObject.Find("Barrel").gameObject.name);
        o1.Save();
        
        //Load Object into Dict
        DataPool.LoadObject(1);

        //erstelle Object
        Object ox;
        DataPool.ObjectList.TryGetValue("1", out ox);
        Instantiate(Resources.Load("Prefabs/" + ox.obj), new Vector3(ox.posx,ox.posy+100,ox.posz), new Quaternion(ox.rotx,ox.roty,ox.rotz, ox.rotw));



    }
}
