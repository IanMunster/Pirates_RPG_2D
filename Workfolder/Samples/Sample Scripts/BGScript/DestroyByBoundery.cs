using UnityEngine;
using System.Collections;

public class DestroyByBoundery : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other){
		/*SET THE OBJECT TO INACTIVE, But not the player*/
		if (other.CompareTag ("Player") || other.CompareTag("PlayerObjects")) {
			return;	
		} else {
			//print ("DESTROY");
			other.gameObject.SetActive (false);
		}
	}
}