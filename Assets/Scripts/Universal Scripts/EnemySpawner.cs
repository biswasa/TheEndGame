using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public Transform enemyPrefab;
	public int enemies;
	
	private const float MIN_RESPAWN_TIME = 5.0f;
	private int maxEnemies = 5;
	private float lastRespawn;
	
	// Use this for initialization
	void Start () {
		enemies = 0;	
		lastRespawn = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if ((enemies < maxEnemies) && ((Time.time - lastRespawn) > (1 + Random.value) * MIN_RESPAWN_TIME)) {
			respawn();
		}
	}
	
	void respawn() {
		Object spawnedEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
		
		// Sanity checking
		if (spawnedEnemy != null) {
			++enemies;
			lastRespawn = Time.time;
		}	
		
		// Randomize position of spawner corner
		float rand = Random.value;
		if (rand <= 0.25) {
			transform.position = -transform.position;	
		} else if (rand <= 0.5) {
			transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
		} else if (rand <= 0.75) {
			transform.position = new Vector3(transform.position.x, transform.position.y, -transform.position.z);
		}
	}
	
	public void killEnemy() {
		--enemies;
	}
}
