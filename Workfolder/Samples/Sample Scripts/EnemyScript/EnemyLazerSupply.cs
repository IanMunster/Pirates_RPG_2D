using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyLazerSupply : MonoBehaviour {

	/*Make new ref. to ObjectPooler script*/
	public static EnemyLazerSupply getSupply;
	/*public Gameobject of the -to-be-pooled- object*/
	[SerializeField] private GameObject pooledLazer;
	/*max amount of pooled objects*/
	[SerializeField] private int lazerMax = 10;
	/*can the max amount expand?*/
	[SerializeField] private bool canExpand = true;
	/*make a list of the pooled objects*/
	private List<GameObject> pooledLazers;

	// Use this for initialization
	void Awake () {
		getSupply = this;
		/*Make a new list, and add all the Gameobjects as Inactive*/
		pooledLazers = new List<GameObject> ();
		for(int i=0; i<lazerMax; i++){
			GameObject lazer = (GameObject)Instantiate (pooledLazer);
			// make Lazer a child ofLazerSupply
			//lazer.transform.parent = this.transform;
			lazer.SetActive (false);
			pooledLazers.Add (lazer);
		}
	}
	/*Get a Pooled GameObject and give it back (to be activated)*/
	public GameObject GetPooledObject(){
		for(int i=0; i<pooledLazers.Count; i++) {
			if(!pooledLazers[i].activeInHierarchy) {
				return pooledLazers[i];
			}
		}
		/*If the max pooled amount can be expanded, give a new gameobject back*/
		if(canExpand){
			GameObject lazer = (GameObject)Instantiate (pooledLazer);
			pooledLazers.Add (lazer);
			return lazer;
		}
		/*but if there are no more and it cant expand, give nothing back*/
		return null;
	}
}
