using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Brick_Clicking : MonoBehaviour {



	public enum State {
		DownState,							//the chest is completely open
		UpState,							//the chest is completely closed
		inbetween						//the chest is being open or closed.
	}

	private State loclaStat;
	

	public delegate void ClickAction(Transform T);
	public static event ClickAction Onclicked;

	#region old




	public bool isclicked=false;

	//public SquareNode SN  ;//= this.transform.GetComponent<SquareNode>();



	public void MoveUpOne(){
		StartCoroutine(MoveToUP());
	}


	public bool isisMovepossible(){

		if (Mgrid.Aalgo())

		return true;
		else
			return false;
	}

	IEnumerator MoveToUP()//(Vector3 position)
	{
		//mutex=false;
		float time=1f;
		Vector3 theend= new Vector3(transform.position.x, 1, transform.position.z);
		Vector3 start = transform.position;
		//Vector3 end = position;
		float t = 0;
		
		while(t < 1)
		{
			yield return null;
			t += Time.deltaTime / time;
			transform.position = Vector3.Lerp(start, theend, t);
		}
		transform.position = theend;
		loclaStat = State.UpState; 
		//mutex=true;
		
	}

	IEnumerator MoveObject(Vector3 source, Vector3 target, float overTime)
	{
		float startTime = Time.time;
		while(Time.time < startTime + overTime)
		{
			transform.position = Vector3.Lerp(source, target, (Time.time - startTime)/overTime);
			yield return null;
		}
		transform.position = target;
	}




	private bool mutex=true;

	IEnumerator MoveToDown()//(Vector3 position)
	{
		//mutex=false;
			float time=1f;
		Vector3 theend= new Vector3(transform.position.x, 0, transform.position.z);
		Vector3 start = transform.position;
		//Vector3 end = position;
		float t = 0;
		
		while(t < 1)
		{
			yield return null;
			t += Time.deltaTime / time;
			transform.position = Vector3.Lerp(start, theend, t);
		}
		transform.position = theend;
		loclaStat = State.DownState;
		//mutex=true;
	}

	
	
	
	public void OnMouseEnter() {
		HighLight( true );
		//StartCoroutine("MoveToUP");
	}
	
	public void OnMouseExit() {
		HighLight( false );
	}



	//*******************************CLIIIIIIIIIIIIIIIIIIIIK
	public void OnMouseDown() {

		if(!GUI_button_pannel.addTOWERonOff)
		{
	//	CLICKSTATE++;

		//if(mutex){		
			switch(loclaStat) {
			case State.UpState:
				mutex =false;
					loclaStat = State.inbetween;
					StartCoroutine(MoveToDown());
					Onclicked(transform);
					transform.GetComponent<Brick>().IsLegitPath=true;
				if(isisMovepossible()){
					transform.GetComponent<Renderer>().material.color= Color.cyan;
					loclaStat = State.DownState;
					transform.GetComponent<Brick>().isDown=true;
					Mgame.UnuseBlock();}

				//mutex =true;

				break;

			case State.DownState:
			//	mutex =false;


				if(Mgame.blocksAvailable<=Mgame.totalblocks && Mgame.blocksAvailable>0 ){
					loclaStat = State.inbetween; //aka was a alegitpath
					Onclicked(transform);
					transform.GetComponent<Brick>().IsLegitPath=false;

					if(!isisMovepossible()){
						transform.GetComponent<Brick>().IsLegitPath=true;
						transform.GetComponent<Renderer>().material.color= Color.red;
						StartCoroutine(MoveToDown());
						isisMovepossible();
						break;
					}
					else{
						transform.GetComponent<Renderer>().material.color= Color.yellow;				
						StartCoroutine(MoveToUP());
						loclaStat = State.UpState;
						transform.GetComponent<Brick>().isUP=true;
						Mgame.UseBlock();}

					//	mutex =true;
				}
			
				break;
			case State.inbetween:
				//mutex =false;
				break;
			}
	}
		else{

			//puttwer on seleced
			if(transform.GetComponent<Brick>().IsLegitPath==false){
				transform.GetComponent<Brick>().hasTower=true;
				transform.GetComponent<Renderer>().material.color= Color.magenta;
				//putTowerhere
				Mtower.BuyAndPlaceATowerhere(transform);

			}
		

		}

	}
	
	public void OnMouseUp() {
	}
	
	private void HighLight( bool glow ) {
	/*
		Color color = Color.red;
		
		if( glow )
			color = Color.yellow;
		
		renderer.material.color = color;
		*/
	}



	#endregion



	public Vector3 downPos;
	public Vector3 upPos;
	public Vector3 currpos;
	public float speed=1;
	void MoveitMoveit(){
	//	if (transform.position == downPos){

			//currpos= upPos;
	//	}

		transform.position= Vector3.MoveTowards(downPos,upPos, speed*Time.deltaTime);



	}


	
	//	public int CLICKSTATE=0;
	
	public void Update() {
		//if( _arrowPressed ){
		//SN= GameObject.Find("
		//	SN.GetComponent<Transform>().GetComponent<SquareNode>();
		//	SN.OnOff=true;
		//MoveTo();
	//	MoveObject(transform.position, Vector3.zero, 1.2f);
		///MoveitMoveit();
		//}
	//	movemeUP();
		
	}


	public void movemeUP(){

		//GameObject nextWaypoint=WPM.WP_GO_LIST[index];
		//	Transform thisWayp= new Transform();
		
		
		float moveY;
		moveY= 2.5f* Time.deltaTime ;
		transform.Translate ( Vector3.up * moveY);
		do{		gameObject.transform.position= new Vector3( transform.position.x,  transform.position.y,  transform.position.z);
		}

		while (transform.position.y<1);

		//GameObject cam =GameObject.FindGameObjectWithTag("MainCamera");


	}



	//************COROUITITNEXAMPLE

	public float smoothing =1f;
	public Transform target;


	//PathMNGR PM;
	// Use this for initialization
	MNGR_Game Mgame;
	MNGR_Grid Mgrid;
	MNGR_Tower Mtower;

	void Start () {
		downPos=new Vector3(transform.position.x,transform.position.y,transform.position.z);
		upPos=new Vector3(transform.position.x,transform.position.y+1,transform.position.z);
		currpos= downPos;
		loclaStat=State.DownState;
		Mgame=GameObject.Find("Empty_GameManager").GetComponent<MNGR_Game>();
		Mgrid=GameObject.Find("Empty_Grid").GetComponent<MNGR_Grid>();

		Mtower=GameObject.Find("Empty_TowerManager").GetComponent<MNGR_Tower>();
	//  PM= transform
	}
	

}
