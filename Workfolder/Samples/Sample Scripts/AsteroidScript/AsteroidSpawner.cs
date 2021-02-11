using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidSpawner : MonoBehaviour {

	public static AsteroidSpawner current;
	private GameObject hazard;
	[SerializeField] private List<GameObject> hazards;
	private List<GameObject> hazardPool;
	private Vector2 spawnValues;
	[SerializeField] private int hazardCount;
	[SerializeField] private float spawnWait;
	[SerializeField] private float startWait;
	[SerializeField] private float waveWait;
	private int maxHazPool = 9;
	private int aLilRandom;
	// Use this for initialization
	void Awake () {
		current = this;
		spawnValues = transform.position;
		//SET THE POOLINGLIST, AND ADD INACTIVE GAMEOBJECTS
		hazardPool = new List<GameObject> ();		
		for(int i=0; i<hazards.Count;i++){
			hazard = hazards [i];
			for(int p=0; p<maxHazPool; p++){
				GameObject obj = (GameObject)Instantiate (hazard);
				//make the hazards child of the Spawner
				obj.transform.parent = transform;
				obj.SetActive (false);
				hazardPool.Add (obj);
			}
		}
		StartCoroutine (SpawnWave ());
	}

	public IEnumerator SpawnWave() {
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i < hazardCount; i++) {
				//GIVE THE HAZARD SPAWN POSITION & ROTATION
				Vector2 spawnPosition = new Vector2 (spawnValues.x, Random.Range (spawnValues.y - 20, spawnValues.y + 20));
				Quaternion spawnRotation = this.transform.rotation;
				/*INT P AS IN POOL*/
				aLilRandom = Random.Range (0, hazardPool.Count);
				for (int p = 0; p < hazardPool.Count; p++) {
					if (!hazardPool [aLilRandom].activeInHierarchy) {
						hazardPool [aLilRandom].transform.position = spawnPosition;
						hazardPool [aLilRandom].transform.rotation = spawnRotation;
						hazardPool [aLilRandom].SetActive (true);
						break;
					}
				}
				//WAIT FOR THE SPAWNWAIT SECONDS
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
		}
	}

}
