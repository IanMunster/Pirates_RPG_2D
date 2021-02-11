using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyExplosionSupply : MonoBehaviour {

 

    public static EnemyExplosionSupply getExplosion;
	[SerializeField] private GameObject poolExpl;
	[SerializeField] private int pooledMax;
	[SerializeField] private bool canExpand = true;
	private List<GameObject> pooledExplosions;
	// Use this for initialization
	void Awake () {
      
        
        getExplosion = this;
		pooledExplosions = new List<GameObject> ();
		for (int i = 0; i < pooledMax; i++) {
			GameObject explosion = (GameObject)Instantiate (poolExpl);
			//explosion.transform.parent;
			explosion.SetActive(false);
			pooledExplosions.Add (explosion);
		}
	}

	public GameObject GetPooledExplosion (){
		for(int i=0; i<pooledExplosions.Count; i++){
			if(!pooledExplosions[i].activeInHierarchy){
           
                return pooledExplosions [i];
			}
		}
		if(canExpand){
			GameObject explosion = (GameObject)Instantiate (poolExpl);
			pooledExplosions.Add (explosion);
            
			return explosion;
		}
		return null;
	}
}
