using UnityEngine;
using System.Collections;

public class AsteroidMove : MonoBehaviour {

	[SerializeField] int direction;
	private float AsteroidSpeed;
	private float tumble;
	private float randomize;
	private Rigidbody2D _rigid;
	private bool cannotMove;

	/*Use this for initialization*/ 
	void Start () {
		//Set the Asteroids speed
		AsteroidSpeed = 10;
		//Rotation tumble value
		tumble = 15;
		//Make reference with the Rigidbody2D component

	}
	void Awake (){
		if(NextLevelChecker.current != null){
			cannotMove = NextLevelChecker.current.AsteroidStopper ();
		}

		//Make a random float every time a asteroid is spawned
		randomize = Random.Range(-2.5f,2.5f);
		_rigid = GetComponentInParent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(!cannotMove && NextLevelChecker.current != null){
			//Move the Asteroids
			_rigid.velocity = transform.right * (AsteroidSpeed - randomize);
			//Rotate Asteroids and give angular velocity
			_rigid.angularVelocity = (Random.value * randomize) * (Random.value * tumble);
		}


	}
}
