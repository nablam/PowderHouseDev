using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MNGR_Waypoints : MonoBehaviour {

//	public Transform WP_Tran;
	public GameObject WP_GO;
	// Use this for initialization

	///public GameObject okfineHome;

	public List<Vector3> wp_V3_List=new List<Vector3>();


	public void Reset_Waypointtile_list(){
		wp_V3_List.Clear();
	}

	public void Reset_Waypointtile_OBJ_list(){

		foreach(GameObject go in WP_GO_LIST){

			Destroy(go);
		}

		WP_GO_LIST.Clear();
	
	}
	public List<GameObject> WP_GO_LIST=new List<GameObject>();
	

	public void GenerateListOfWaypoints(){

		for (int i=0; i<= wp_V3_List.Count-1; i++){
			//JSMB
			GameObject aWP_Go = Instantiate(WP_GO,
			                            // this.transform.position,
			                            wp_V3_List[i],
		                            Quaternion.identity
		                               ) as GameObject;
			//
			WP_GO_LIST.Add (aWP_Go);
			aWP_Go.transform.parent = this.transform;
		}
	
	}


//	public  void RMOVEformdic(int id){mobdik.Remove(id);}


	MNGR_Grid MGrid;
	void Start () {
		MGrid= GameObject.Find("Empty_Grid").GetComponent<MNGR_Grid>();
	}
	

	void Update () {
	
	}
}
