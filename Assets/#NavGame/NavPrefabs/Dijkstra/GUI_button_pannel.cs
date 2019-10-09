using UnityEngine;
using System.Collections;

public class GUI_button_pannel : MonoBehaviour {
	public static bool addTOWERonOff=false;
	MNGR_Grid Mgrid;
	MNGR_Waypoints Mwaypoints;
	MNGR_Game Mgame;
	MNGR_Mob MMob;
	MNGR_Tower Mtower;
	void Start () {
		//ref to the other object of waypoints
		//wm= GameObject.Find("Waypoints_object_keepName").GetComponent<WayPointManager>();

		Mgrid= GameObject.Find("Empty_Grid").GetComponent<MNGR_Grid>();
		Mwaypoints= GameObject.Find("Empty_Waypoints").GetComponent<MNGR_Waypoints>();
		Mgame=GameObject.Find("Empty_GameManager").GetComponent<MNGR_Game>();
		MMob=GameObject.Find("Empty_MobManager").GetComponent<MNGR_Mob>();
			Mtower=GameObject.Find("Empty_TowerManager").GetComponent<MNGR_Tower>();
	}
	
	// Update is called once per frame
	void Update () {

	}
	public bool turnCollideronoff=true;




	private void OnGUI(){
		GUI.Label(new Rect(200, 10, 100, 50),addTOWERonOff.ToString());
		GUI.Label(new Rect(200, 60, 100, 50),"blocks Available" +Mgame.blocksAvailable);

		GUI.Label(new Rect(200, 110, 100, 50),"mobs MAde " +Mgame.TotalNumberofMonbmade);
		
		GUI.Label(new Rect(200, 160, 100, 50),"mobs reached " +Mgame.NumberModReachedTheend);

		if(GUI.Button( new Rect(10,10,100,50) , "Gridd")){
			Debug.Log("griddsetup");

			Mgrid.DO_INTI_SETUP_GRID();


			//otherScript.BuildTowersThere();
			//otherScript.Aalgo();
			//wm.GenerateListOfWaypoints();
			//wm.amob()
		}


		if(GUI.Button( new Rect(10,60,100,50) , "runAlgo and set Waypoints")){
			Debug.Log("alg");
		
		//	Mgrid.Aalgo();
		//	Mwaypoints.GenerateListOfWaypoints();
	
		}

		if(GUI.Button( new Rect(10,110,100,50) , "MOB it")){
			Mgrid.Aalgo();
			Mwaypoints.GenerateListOfWaypoints();
			MMob.makeamob();
			
	


		}






		if(GUI.Button( new Rect(10,160,100,50) , "Towers")){
			Debug.Log("tow");
		//	PathMNGR otherScript = GetComponent<PathMNGR>();
		//	otherScript.randCell_deselect();
		
			//wm.amob()
		

			if(turnCollideronoff==true){
				Mtower.disableALltowers();
				turnCollideronoff=false;
			}
			else
			{
				Mtower.EnableALLtowers();
				turnCollideronoff=true;
			}

		}




		if(GUI.Button( new Rect(10,210,100,50) , "TowerOnoff")){
		if (addTOWERonOff) addTOWERonOff=false;
			else
				addTOWERonOff=true;
		//	Debug.Log("");
		//	WayPointManager otherScript = GetComponent<WayPointManager>();

			//otherScript.
			
			//wm.amob()


		}






	}
}
