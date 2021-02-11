using UnityEngine;
using System.Collections;

public class ZoomCamera : MonoBehaviour {
	
	[SerializeField] private float zoomSpeed;
	private float cameraOrthographSize;
	private float zoomSmoothing;
	private float zoomMin;
	private float zoomNrml;
	private float zoomMax;

	// Use this for initialization
	void Start () {
		zoomSmoothing = 5.0f;
		zoomMin = 10.0f;
		zoomNrml = 30.0f;
		zoomMax = 40.0f;
		cameraOrthographSize = Camera.main.orthographicSize;
		cameraOrthographSize = zoomNrml;
	}

	void Awake(){
		cameraOrthographSize = zoomNrml;
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown ("Fire3")){
			cameraOrthographSize = zoomNrml;
		}
		float mouseScroll = Input.GetAxis ("Mouse ScrollWheel");
		if(mouseScroll != 0.0f){
			cameraOrthographSize -= mouseScroll * zoomSpeed;
			cameraOrthographSize = Mathf.Clamp (cameraOrthographSize, zoomMin, zoomMax);
		}
		Camera.main.orthographicSize = Mathf.MoveTowards (Camera.main.orthographicSize, cameraOrthographSize, zoomSmoothing * Time.deltaTime);
	}
}
