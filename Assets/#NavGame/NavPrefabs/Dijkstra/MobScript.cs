using UnityEngine;
using System.Collections;

public class MobScript : MonoBehaviour {
	public delegate void DeathAction(int id);
	public static event DeathAction OnDided_talktoTower;



	private Transform _myTransform;
	// Use this for initialization
	
	public int MOB_ID;
	public float MOB_health= 100f;

	//l max= 2.2
	public void accesshealth_bar(float HealthInput){


		float Hlength= (HealthInput /100)*2.2f;
		//foreach (Transform child in transform)
		//{
			//Debug.Log(child);
			//
			Transform thisguy=   transform.Find("HealthBarCube");
		thisguy.GetComponent<Renderer>().material.color= Color.red;

		thisguy.localScale = new Vector3(Hlength, 0.20f, 0.20f);
			// do whatever you want with child transform object here
		//}
		
		

	}





	public Transform HB_trans;
	MNGR_Waypoints MWay;
	MNGR_Game MGame;
	MNGR_Mob MMob;
	MNGR_Tower Mtower;

	void Start () {

		_myTransform=transform;

		MWay= GameObject.Find("Empty_Waypoints").GetComponent<MNGR_Waypoints>();
		HB_trans=   transform.Find("HealthBarCube");
		MGame =GameObject.Find("Empty_GameManager").GetComponent<MNGR_Game>();
		MMob=GameObject.Find("Empty_MobManager").GetComponent<MNGR_Mob>();
		Mtower=GameObject.Find("Empty_TowerManager").GetComponent<MNGR_Tower>();
		//MOB_ID=_myTransform.GetComponent<mobs
	}
	
	int index=1;
	bool WPReacked=false;
	//public GameObject emptyWayp;
	
	
	bool waypointReached(){
		
		return WPReacked;
	}
	
	bool movetogoal(int ind){
		//if( waypointReached())
		//index++;
		
		GameObject nextWaypoint=MWay.WP_GO_LIST[index];
		//	Transform thisWayp= new Transform();
				
		float moveZ;
//fast speeed moveZ= -2.5f* Time.deltaTime ;
		moveZ= -1f* Time.deltaTime ;

		transform.Translate ( Vector3.forward * moveZ);
		gameObject.transform.position= new Vector3( transform.position.x,  transform.position.y,  transform.position.z);
		//GameObject cam =GameObject.FindGameObjectWithTag("MainCamera");
		_myTransform.LookAt(MWay.WP_GO_LIST[ind].transform.position);		
		transform.Rotate(Vector3.up * 180);
		
		
		//if( transform.position.x 
		
		return true;
	}
	
	
	bool movetogoalSlerp(int ind){
		if(index< MWay.WP_GO_LIST.Count )
		//if( true)
		{	

			GameObject nextWaypoint=MWay.WP_GO_LIST[index];
			//	Transform thisWayp= new Transform();
			
			
			float moveZ;
			moveZ= -2.5f* Time.deltaTime ;
			transform.Translate ( Vector3.forward * moveZ);
			gameObject.transform.position= new Vector3( transform.position.x,  transform.position.y,  transform.position.z);
			//GameObject cam =GameObject.FindGameObjectWithTag("MainCamera");
			
			
			Quaternion angleNeeded;
			angleNeeded=Quaternion.LookRotation(transform.position- MWay.wp_V3_List[ind]);
			float damp=9f;
			
			transform.rotation= Quaternion.Slerp(transform.rotation, angleNeeded, Time.deltaTime*damp);
			//_myTransform.LookAt(WPM.WP_GO_LIST[ind].transform.position);		
			//transform.Rotate(Vector3.up * 180);
			
			
			//if( transform.position.x 
			
			return true;
		}


		//MGame.NumberModReachedTheend++;
	//	Deathroutine();
		return false;
	
	}
	// Update is called once per frame

	public void mob_decreaseHealthBy(int x){

		MOB_health=MOB_health-x;
	}

	public void TDeathroutine(){
		
		OnDided_talktoTower(MOB_ID);
		MMob.RMOVEformdic(MOB_ID); //mobID is set from WPM makeamob() 
		if(index>= MWay.WP_GO_LIST.Count)
			MGame.NumberModReachedTheend++;
		Destroy(this.gameObject);
	}


	public void Deathroutine(){
		if (Mtower.Towlist.Count>0)
		OnDided_talktoTower(MOB_ID);
		MMob.RMOVEformdic(MOB_ID); //mobID is set from WPM makeamob() 
		if(index>= MWay.WP_GO_LIST.Count)
		 MGame.NumberModReachedTheend++;
		Destroy(this.gameObject);
		//}
	}
	void Update () {
		
		movetogoalSlerp(index);
		accesshealth_bar(MOB_health);

		if(MOB_health<1){


			TDeathroutine();
		}


		if(index>= MWay.WP_GO_LIST.Count){
			
			//MGame.NumberModReachedTheend++;
		//	Deathroutine();
		}

	//	HB_trans.LookAt( Camera.main.transform.position);	
		//HB_trans.Rotate(Vector3.back * 180);
	}
	
	
	void OnTriggerEnter(Collider other) {
		//Debug.Log("enetere");
		if(other.CompareTag("t2")){
			//Debug.Log("index"+index.ToString());
			if(mutex)
				index++;
			if(index>= MWay.WP_GO_LIST.Count){

				Deathroutine();
			}
			
			mutex=false;
			
		};
	}
	bool mutex=true;
	void OnTriggerExit(Collider other) {
		//Debug.Log("enetere");
		if(other.CompareTag("t2")){
			mutex=true;
		};
	}
	
}
