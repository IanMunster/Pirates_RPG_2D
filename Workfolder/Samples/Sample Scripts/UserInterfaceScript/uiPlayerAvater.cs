using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class uiPlayerAvater : MonoBehaviour {

	public static uiPlayerAvater current;
	private Image playerFace;
	[SerializeField] private Sprite[] face;

	// Use this for initialization
	void Start () {
		current = this;
		playerFace = GetComponent<Image>();
	}

	public void UpdateAvatar(string newStateValue){
		if(newStateValue == "Normal"){
			playerFace.sprite = face[0];
		}
		if(newStateValue == "Hit"){
			playerFace.sprite = face[1];
		}
		if(newStateValue == "LowLife"){
			playerFace.sprite = face[2];
		}
		if(newStateValue == "LowShield"){
			playerFace.sprite = face[3];
		}
		if(newStateValue == "Dead"){
			playerFace.sprite = face[4];
		}
		if(newStateValue == null){
			playerFace.sprite = face[0];
		}
	}
}
