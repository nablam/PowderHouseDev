using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cell_Script : MonoBehaviour {
	
	public List<Transform> Adjacents;
	public Vector3 Position;
	public int Weight;
	
	public GameObject tresor;
	public GameObject health;
	public GameObject oger;
	public GameObject arak;
	
	
	public Transform here;
	public Transform hereMonster1;
	public Transform hereMonster2;
	public Transform herehealth;
	
	public int AdjacentsOpened = 0;
	// Use this for initialization
	void Start () {
		//transform.localEulerAngles = new Vector3(270,0,0);
		//hereMonster1=transform.GetChild
		
		
	int yon;
		yon = Random.Range(1,11);
		if (yon > 7) InstanciateAChest();
		
		
		int mobyon;
		mobyon = Random.Range(1,11);
		if (mobyon > 6) InstanciateAMob();
		
		
		int healthyon;
		healthyon = Random.Range(1,11);
		if (healthyon > 7) InstanciateAHealth();
		
		
		
	}
	
	public void InstanciateAHealth(){
		
			GameObject w = Instantiate(health, herehealth.transform.position  ,Quaternion.identity)as GameObject;
		w.transform.parent= herehealth.transform;	
	//	w.transform.rotation=new Quaternion(0,0,0,0);
		
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log( transform.GetChild(0).name.ToString());  hoolyy shit <-- this returrned "here" the first thing that is hooked on this 
		
	
	}
	
	private void InstanciateAMob(){
		
								//	Destroy(  pc.LSpad1Mount.transform.GetChild(0).gameObject);
		
		int healthyon;
		healthyon = Random.Range(1,16);
		if (healthyon < 3) {
	GameObject w = Instantiate(oger, hereMonster1.transform.position  ,Quaternion.identity)as GameObject;
		w.transform.parent= hereMonster1.transform;	
	///	w.transform.rotation=new Quaternion(0,0,0,0);
		}
		if (healthyon > 13) {
	GameObject w = Instantiate(arak, hereMonster2.transform.position  ,Quaternion.identity)as GameObject;
		w.transform.parent= hereMonster2.transform;	
	//	w.transform.rotation=new Quaternion(0,0,0,0);
		}
		
		
		
		
		
	}
	private void InstanciateAChest(){
		
								//	Destroy(  pc.LSpad1Mount.transform.GetChild(0).gameObject);
	GameObject w = Instantiate(tresor, here.transform.position  ,Quaternion.identity)as GameObject;
w.transform.parent= here.transform;							w.transform.rotation=new Quaternion(0,0,0,0);
		
		
		
		
	}
}
