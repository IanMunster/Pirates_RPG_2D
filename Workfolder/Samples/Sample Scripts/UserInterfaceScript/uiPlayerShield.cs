using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class uiPlayerShield : MonoBehaviour {

	public static uiPlayerShield current;
	private Image shieldBar;
	private float shieldCapacity = 100;

	// Use this for initialization
	void Start () {
		current = this;
		shieldBar = GetComponent<Image> ();
	}

	public void AddShieldValue(float newShieldValue){
		shieldCapacity = newShieldValue;
		UpdateShield ();
	}

	void UpdateShield(){
		if(shieldBar != null){
			shieldBar.fillAmount = shieldCapacity / 100;
		}

	}
}
