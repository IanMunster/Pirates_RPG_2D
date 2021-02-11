using UnityEngine;
using System.Collections;


public class EnemyAI : MonoBehaviour {

	//GET TARGET & DISTANCE
	[SerializeField] private Transform target;
	//Distance, X=stopDist, Y=minDist, Z=maxDist
	[SerializeField] private Vector3 distance = new Vector3(10,20,40);
	[SerializeField] private float enemyLowHealth = 50;

	//Get All the SCRIPTS in this enemy
	private GameObject _enemy;
	private EnemyMove _enemyMove;
	private EnemyHealth _enemyHealth;
	private EnemyWeaponController _enemyShoot;
	private EnemyEvasiveManouver _enemyEvade;
	private EnemyPatrolling _enemyPatrol;
	private EnemyChasing _enemyChasing;

	//VARS for enemy Behaviour
	private float range;
	private float crrntSpeed;
	private Rigidbody2D rigid;
	private bool IsShooting = false;
	private bool IsChasing = false;
	private bool IsEvading = false;
	private bool IsPatrolling = false;

	void Awake (){
		rigid = GetComponent<Rigidbody2D> ();
		GameObject playerObj = GameObject.FindGameObjectWithTag ("Player");
		if (playerObj != null) {
			target = playerObj.transform;
		}
		//Get all the Scripts needed;
		_enemy = GameObject.FindGameObjectWithTag("Enemy");
		if(_enemy != null){
			_enemyMove = _enemy.GetComponent<EnemyMove>();
			_enemyHealth = _enemy.GetComponent<EnemyHealth> ();
			_enemyShoot = _enemy.GetComponent<EnemyWeaponController> ();
			_enemyEvade = _enemy.GetComponent<EnemyEvasiveManouver> ();
			_enemyPatrol = _enemy.GetComponent<EnemyPatrolling> ();
			_enemyChasing = _enemy.GetComponent<EnemyChasing> ();
		} else {
			print("Something went Wrong; Could not find this Enemy "+this+" in this scene");
		}
	}

	void FixedUpdate(){
		crrntSpeed = rigid.velocity.y;
		range = Vector2.Distance(transform.position, target.position);

		//(IF THE PLAYER IS TOO CLOSE: STOP MOVING)
		if (range < distance.x) {
			_enemyMove.enemySpeed = _enemyMove.speed.x;
		} else {
			_enemyMove.enemySpeed = _enemyMove.speed.y;
		}
		//(IF HEALTH LOW: EVADE)
		if (_enemyHealth.enemyHealth < enemyLowHealth) {
			//START EVADING!
			if (!IsEvading) {
				IsEvading = true;
				_enemyEvade.StartEvading (rigid, crrntSpeed, IsEvading);
			}
		} else {
			//NOT EVADING
			IsEvading = false;
			_enemyEvade.StopEvading (IsEvading);
		}
		//(CLOSE ENOUGH:SHOOT)
		if (range < distance.y) {
			if (!IsShooting) {
				IsShooting = true;
				//START SHOOTING
				_enemyShoot.StartFiring ();
			}
		} else {
			//NOT SHOOTING
			IsShooting = false;
			_enemyShoot.StopFiring ();
		}
		//(WITHIN SIGHT: CHASE)
		if (range <= distance.z) {
			if (!IsChasing) {
				//START CHASING
				IsChasing = true;
				_enemyChasing.StartChasing(IsChasing, target);
				_enemyMove.enemySpeed = _enemyMove.speed.z;
			}
		} else {
			//NOT CHASING
			IsChasing = false;
			_enemyMove.enemySpeed = _enemyMove.speed.y;
			_enemyChasing.StopChasing(IsChasing, target);
		}
		//(TOO FAR: PATROL) OR (IF THE PLAYER DIED)
		if (range > distance.z || !target.gameObject.activeInHierarchy) {
			if (!IsPatrolling) {
				//START PATROLLING
				IsPatrolling = true;
				_enemyPatrol.StartPatrolling(IsPatrolling);
			}
		} else {
		//NOT PATROLLING
			IsPatrolling = false;
			_enemyPatrol.StopPatrolling (IsPatrolling);
		}
	}
}