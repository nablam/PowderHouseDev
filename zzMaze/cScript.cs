
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cScript : MonoBehaviour {
	
	public List<Transform> Adjacents;
	public Vector3 Position;
	public int Weight;
	
	public GameObject tresor;
	public GameObject health;
	public GameObject oger;
	private GameObject arak;
	
	
	public Transform here;
	public Transform hereMonster1;
	public Transform hereMonster2;
	public Transform herehealth;
	
	public int AdjacentsOpened = 0;
	// Use this for initialization
	void Start () {
		//transform.localEulerAngles = new Vector3(270,0,0);
		
	int yon;
		yon = Random.Range(1,11);
		if (yon > 7) InstanciateAChest();
		
		
		int mobyon;
		mobyon = Random.Range(1,11);
		if (mobyon > 3) InstanciateAMob();
		
		
		int healthyon;
		healthyon = Random.Range(1,11);
		if (healthyon > 2) InstanciateAHealth();
		
		
		
	}
	
	public void InstanciateAHealth(){}
	
	// Update is called once per frame
	void Update () {
		
		
	
	}
	
	private void InstanciateAMob(){
		
								//	Destroy(  pc.LSpad1Mount.transform.GetChild(0).gameObject);
	//GameObject w = Instantiate(oger, hereMonster1.transform.position  ,Quaternion.identity)as GameObject;
//w.transform.hereMonster1= here.transform;				
		//w.transform.rotation=new Quaternion(0,0,0,0);
		
		
		
		
	}
	private void InstanciateAChest(){
		
								//	Destroy(  pc.LSpad1Mount.transform.GetChild(0).gameObject);
	GameObject w = Instantiate(tresor, here.transform.position  ,Quaternion.identity)as GameObject;
w.transform.parent= here.transform;							w.transform.rotation=new Quaternion(0,0,0,0);
		
		
		
		
	}
}
