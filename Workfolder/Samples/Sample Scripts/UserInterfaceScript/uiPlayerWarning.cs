using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class uiPlayerWarning : MonoBehaviour {

	public static uiPlayerWarning current;
	private Text warningTxt;
	private string tekst;
	private float secondsWait;
	private int animCount = 0;
	//Color.x = R, Color.y= G, Color.z = B;
	private Vector3 txtColor;
	private float alpha = 100;
	// Use this for initialization
	void Start () {
		current = this;
		warningTxt = GetComponent<Text> ();
		warningTxt.text = tekst;
		txtColor = new Vector3 (warningTxt.color.r, warningTxt.color.g, warningTxt.color.b);
	}

	public void AddWarningTxt (string newTxt, float newWait, Vector3 newColor) {
		tekst = newTxt;
		secondsWait = newWait;
		txtColor = newColor;
		if (animCount == 0) {
			animCount = 1;
			StartCoroutine (AnimateWarningTxt ());
		} else if (animCount == 1) {
			StopCoroutine (AnimateWarningTxt ());
		} else {
			print ("WENTWRONG");
		}
	}

	IEnumerator AnimateWarningTxt(){
		warningTxt.text = tekst;
		warningTxt.color = new Color(txtColor.x, txtColor.y, txtColor.z);
		yield return new WaitForSeconds(secondsWait);
		warningTxt.text = "";
		StopCoroutine (AnimateWarningTxt());
		animCount = 0;
		yield return null;
	}

	void Update(){
		warningTxt.color = new Color (txtColor.x, txtColor.y, txtColor.z, alpha/100f);
		warningTxt.text.ToUpper ();
		//print (alpha);
	}
}
