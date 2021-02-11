using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class uiPlayerOverheated : MonoBehaviour {

	public static uiPlayerOverheated current;
	private Image heatedBar;
	private float heatLevel;

	// Use this for initialization
	void Start () {
		current = this;
		heatedBar = GetComponent<Image> ();
		//heatLevel = 0f;
	}
		
	public void addHeat (float newHeatValue){
		heatLevel = newHeatValue;
		UpdateHeat ();
	}

	void UpdateHeat () {
		if(heatedBar != null){
			heatedBar.fillAmount = heatLevel / 100f;
		}

	}
}
