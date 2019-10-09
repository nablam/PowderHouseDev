using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MNGR_Mob : MonoBehaviour {

	public GameObject amobGO;
	public Dictionary<int, GameObject> mobdik= new Dictionary<int, GameObject>();
	public  void RMOVEformdic(int id){mobdik.Remove(id);}
	MNGR_Grid Mgrid;
	MNGR_Waypoints MWay;

	int mobNumbermade=0;

	public void makeamob(){

		mobNumbermade++;
		Mgame.TotalNumberofMonbmade=mobNumbermade;
		transform.position=Mgrid.Grid_end_ptr.position;

		GameObject aWP_Go = Instantiate(amobGO,
		                                // this.transform.position,
		                                MWay.wp_V3_List[0],
		                                Quaternion.identity
		                                ) as GameObject;
		//
		aWP_Go.transform.GetComponent<MobScript>().MOB_ID=mobNumbermade;
		mobdik.Add (mobNumbermade, aWP_Go);
		aWP_Go.transform.parent = this.transform;
	

	}
	MNGR_Game Mgame;

	void Start () {
		Mgrid= GameObject.Find ("Empty_Grid").GetComponent<MNGR_Grid>();
		MWay = GameObject.Find ("Empty_Waypoints").GetComponent<MNGR_Waypoints>();

		Mgame =GameObject.Find("Empty_GameManager").GetComponent<MNGR_Game>();
		//transform.position=Mgrid.Grid_end_ptr.position;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
