using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour {

	public float lifeTime; // = 2

    void OnEnable () {
        Invoke ("Destroy", lifeTime);	/*DESTROY AFTER LIFETIME*/
	}

	void Destroy () {
		gameObject.SetActive (false); /*SET THE OBJECT TO INACTIVE*/
    }

	void OnDisable () {
		CancelInvoke (); 	/*CHECK SO IT DOESNT DESTROY TWICE*/
	}
}
