using UnityEngine;
using System.Collections;

public class PlayerLifes : MonoBehaviour {

	private GameObject _player;
	private int lifes;
	private int maxLifes = 4;
	private int minLifes = 0;
	private int lostCount = 0;

    // Use this for initialization
    void Start () {
		_player = GameObject.FindGameObjectWithTag ("Player");
		_player.SetActive (true);
		//LOAD DATA:
		lifes = GameController.current.LoadData("Lifes");
		if(lifes == minLifes || lifes > maxLifes){
			lifes += maxLifes;
		}
	}

	void Awake(){
		lostCount = 0;
    }

	void Update(){
		uiPlayerLifes.current.AddLifes (lifes);
		if(lifes == 1){
			uiPlayerAvater.current.UpdateAvatar ("LowLife");
		}
	}

	public void LoseLife () {
		if(lostCount == 0){
			lostCount = 1;
			lifes = lifes - 1;
			GameController.current.SaveData ("Lifes", lifes);
			uiPlayerLifes.current.AddLifes (lifes);
			_player.SetActive (false);

			if (lifes != minLifes) {
				GameController.current.Retry();
			} else if (lifes == minLifes) {
				GameController.current.GameOver();
			} else {
				print ("WentWrong");
			}
		}
	}
}
