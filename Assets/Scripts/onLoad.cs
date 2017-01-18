using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class onLoad :MonoBehaviour {
	// Use this for initialization
	void Start(){
		//ClientScene.AddPlayer (0);

        //Create Item
        ItemData i1 = new ItemData(1,20,2,50);
        i1.Save();

        //Create Player
        

        //Load Item and Player into Dictionary
        DataPool.LoadItem(1);

        PlayerData px = null;
        if (DataPool.PlayerList.TryGetValue("1", out px))
        {
            DataPool.LoadPlayer(GameObject.Find("Options").GetComponent<Options>().nameOfPlayer);
            Debug.Log("Load: "+px.id + ": " + px.nameOfPlayer + ", " + px.xp + ", " + px.attribute + ", " + (px.headAmor + px.bodyAmor + px.legAmor));
            px.LoadToPlayer();
            //GameObject.Find(GameObject.Find("Options").GetComponent<Options>().name).GetComponent<>();
        }
        else {
            PlayerData p1 = new PlayerData(1, GameObject.Find("Options").GetComponent<Options>().nameOfPlayer, 0, 0);
            p1.Save();
            Debug.Log("Create: "+p1.id + ": " + p1.nameOfPlayer + ", " + p1.xp + ", " + p1.attribute + ", " + (p1.headAmor + p1.bodyAmor + p1.legAmor)+"%");
            p1.LoadToPlayer();
        }

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
