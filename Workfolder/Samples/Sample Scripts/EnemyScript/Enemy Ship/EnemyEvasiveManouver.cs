using UnityEngine;
using System.Collections;

public class EnemyEvasiveManouver : MonoBehaviour {
	
	[SerializeField] private float dodge = 5;
	[SerializeField] private float smoothing = 7;
	//Just vector 2 to have 2 values
	[SerializeField] private Vector2 startWait = new Vector2(0.5f,1f);
	[SerializeField] private Vector2 mnvrTime = new Vector2(1f,5f);
	[SerializeField] private Vector2 mnvrWait = new Vector2(0.5f,1f);

	private float targetManeuver;
	private bool _isEvading;
	private Rigidbody2D _rigid;
	private float _crrntSpeed;

	public void StartEvading(Rigidbody2D rigid, float crrntSpeed, bool IsEvading){
		_rigid = rigid;
		_crrntSpeed = crrntSpeed;
		_isEvading = IsEvading;
		StartCoroutine (Evade());
	}
	public void StopEvading(bool IsEvading){
		_isEvading = IsEvading;
		StopAllCoroutines ();
	}

	IEnumerator Evade() {
		//WAIT FOR RANDOM RANGE OF TIME
		yield return new WaitForSeconds (Random.Range (startWait.x, startWait.y));
		while (true) {
			//Pick random maneuver range (keep it in the boundary)
			targetManeuver = Random.Range (1, dodge) * -Mathf.Sign(transform.position.x);
			//wait for the maneuver
			yield return new WaitForSeconds (Random.Range(mnvrTime.x,mnvrTime.y));
			//set to 0
			targetManeuver = 0;
			// new maneuver
			yield return new WaitForSeconds (Random.Range(mnvrWait.x,mnvrWait.y));
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if(_isEvading){
			float newManeuver = Mathf.MoveTowards (_rigid.velocity.x, targetManeuver, Time.deltaTime * smoothing);
			_rigid.velocity = new Vector2 (newManeuver, _crrntSpeed);
		}
	}

}
