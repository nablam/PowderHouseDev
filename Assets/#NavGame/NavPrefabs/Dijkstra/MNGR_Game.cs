using UnityEngine;
using System.Collections;

public class MNGR_Game : MonoBehaviour {

	public int totalblocks=100;
	public int blocksAvailable;

	public int TotalNumberofMonbmade=0;

	public int NumberModReachedTheend=0;
	//public static int TotalCOins=100;


	public void UseBlock(){blocksAvailable--;}
	public void UnuseBlock(){blocksAvailable++;}

	//Roundstart
	//make grid
	//totalblock=10
	//letuser set blocks
	//if shortets ossible

	void Start () {
		blocksAvailable=totalblocks;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
