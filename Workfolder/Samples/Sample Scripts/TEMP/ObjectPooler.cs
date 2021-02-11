using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour {

	/*Make new ref. to ObjectPooler script*/
	public static ObjectPooler current;
	/*public Gameobject of the -to-be-pooled- object*/
	public GameObject pooledObj;
	/*max amount of pooled objects*/
	public int pooledMax = 4;
	/*can the max amount expand?*/
	public bool canExpand = true;
	/*make a list of the pooled objects*/
	List<GameObject> pooledObjects;

	void Awake () {
		current = this;
	}

	// Use this for initialization
	void Start () {
		/*Make a new list, and add all the Gameobjects as Inactive*/
		pooledObjects = new List<GameObject> ();
		for(int i=0; i<pooledMax; i++){
			GameObject obj = (GameObject)Instantiate (pooledObj);
			print (obj.name + i);
			//make the pooled object a child of the Caller
			obj.transform.parent = transform;
			obj.SetActive (false);
			pooledObjects.Add (obj);
		}
	}
	/*Get a Pooled GameObject and give it back (to be activated)*/
	public GameObject GetPooledObject(){
		for(int i=0; i<pooledObjects.Count; i++) {
			if(!pooledObjects[i].activeInHierarchy) {
				return pooledObjects [i];
			}
		}
		/*If the max pooled amount can be expanded, give a new gameobject back*/
		if(canExpand){
			GameObject obj = (GameObject)Instantiate (pooledObj);
			pooledObjects.Add (obj);
			return obj;
		}
		/*but if there are no more and it cant expand, give nothing back*/
		return null;
	}
}
