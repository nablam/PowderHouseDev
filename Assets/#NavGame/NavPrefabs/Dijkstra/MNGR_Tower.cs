using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MNGR_Tower : MonoBehaviour {



	public GameObject Tow_Ogj;

	public List<GameObject>Towlist=new List<GameObject>();

	void Start () {
	
	}

	public void EnableALLtowers(){foreach(GameObject go in Towlist) go.GetComponent<Collider>().enabled=true;}
	public void disableALltowers(){foreach(GameObject go in Towlist) go.GetComponent<Collider>().enabled=false;}

	public void BuyAndPlaceATowerhere(Transform T){

		GameObject NewTow = Instantiate(Tow_Ogj,
		                                // this.transform.position,
		                                T.position,
		                                Quaternion.identity
		                                ) as GameObject;
		//
		Towlist.Add (NewTow);
		NewTow.transform.parent = this.transform;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
