using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class uiPlayerLifes : MonoBehaviour {

	public static uiPlayerLifes current;
	private Image[] lifeIcons;
	private int Lifes;

	// Use this for initialization
	void Start () {
		current = this;
		lifeIcons = GetComponentsInChildren<Image> ();
	}
		
	public void AddLifes (int newLifesValue) {
		Lifes = newLifesValue;
	}

	// Update is called once per frame
	void Update (){
		//Loop door Array, Zet hartje 4 uit als levens -1 etc
		for(int j=0; j<lifeIcons.Length; j++){
			lifeIcons[j].gameObject.SetActive(false);
		}
		for(int i=0; i<Lifes; i++){
			lifeIcons [i].gameObject.SetActive (true);
		}
	}
}