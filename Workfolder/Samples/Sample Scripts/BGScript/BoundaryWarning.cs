using UnityEngine;
using System.Collections;

/*
 * IF PLAYER GETS TO CLOSE: GET A WARNING;
 * IF PLAYER RETURNS: REMOVE WARNING;
 * IF PLAYER CONTINUES: KILL THE PLAYER
*/

public class BoundaryWarning : MonoBehaviour {

	[SerializeField] private GameObject player;
	private PlayerLifes _lifes;
	private bool playerLeft;

	// Use this for initialization
	void Awake () {
		player = GameObject.FindWithTag ("Player");
		_lifes = player.GetComponent<PlayerLifes> ();
		playerLeft = false;
	}

	void OnTriggerExit2D (Collider2D other){
		if (other.CompareTag ("Player")) {
			uiCenterTxt.current.AddCenterTxt ("WARNING! TURN BACK!", 10f);
			playerLeft = true;
			StartCoroutine (CheckPlayerReturns());
		}
	}

	void OnTriggerEnter2D (Collider2D other){
		if(other.CompareTag("Player")){
			uiCenterTxt.current.AddCenterTxt ("",0f);
			playerLeft = false;
			StopCoroutine (CheckPlayerReturns());
		}
	}

	IEnumerator CheckPlayerReturns(){
		while(playerLeft){
			yield return new WaitForSeconds (8f);
			if (playerLeft) {
				_lifes.LoseLife();
			}
		}
		StopCoroutine (CheckPlayerReturns());
		yield return null;
	}
}
