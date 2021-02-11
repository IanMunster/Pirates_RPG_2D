using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class uiCenterTxt : MonoBehaviour {

	public static uiCenterTxt current;
	private Text centerTxt;
	private string tekst;
	//private bool canAnimate;
	private float secondsWait;
	//Color.x = R, Color.y= G, Color.z = B;
	private Vector3 txtColor;
	private float alpha = 100;

	// Use this for initialization
	void Start () {
		current = this;
		centerTxt = GetComponent<Text> ();
		centerTxt.text = tekst;
		txtColor = new Vector3 (centerTxt.color.r, centerTxt.color.g, centerTxt.color.b);
	}

	public void AddCenterTxt (string newTxt, float newWait) {
		tekst = newTxt.ToUpper();
		secondsWait = newWait;
		StartCoroutine (AnimateCenterTxt());
	}

	IEnumerator AnimateCenterTxt(){
		centerTxt.text = tekst;
		yield return new WaitForSeconds(secondsWait);
		centerTxt.text = "";
		StopCoroutine (AnimateCenterTxt());
		yield return null;
	}

	void Update(){
		centerTxt.color = new Color (txtColor.x, txtColor.y, txtColor.z, alpha/100f);
	}
}