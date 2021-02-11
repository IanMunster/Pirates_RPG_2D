using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidExplosion : MonoBehaviour {

	public static AsteroidExplosion getExplosion;
	[SerializeField] private GameObject poolExpl;
	[SerializeField] private int poolMax;
	[SerializeField] private bool canExpand = true;
	private List<GameObject> pooledExplosions;

	// Use this for initialization
	void Awake () {
		getExplosion = this;
		pooledExplosions = new List<GameObject> ();
		for(int i=0; i<poolMax; i++){
			GameObject ASexplosion = (GameObject)Instantiate (poolExpl);
			ASexplosion.transform.parent = transform;
			ASexplosion.SetActive(false);
			pooledExplosions.Add (ASexplosion);
		}
	}
	
	public GameObject GetPooledExplosion (){
		for(int i=0; i<pooledExplosions.Count; i++){
			if(!pooledExplosions[i].activeInHierarchy){
				return pooledExplosions [i];
			}
		}
		if(canExpand){
			GameObject ASexplosion = (GameObject)Instantiate (poolExpl);
			pooledExplosions.Add (ASexplosion);
			return ASexplosion;
		}
		return null;
	}
}