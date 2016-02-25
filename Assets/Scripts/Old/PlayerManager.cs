using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public GameObject tower;
	private TowerManager towerMaster;
	private Collider target;
	private const float MAX_HEALTH = 50.0f;
	private const int MAX_RESOURCES = 10;
	private float movementSpeed = 10.0f;
	private float attackPower = 5.0f;
	private float currHealth;
	private bool inRange; // True if an enemy is within striking range
	public int currResources;
	
	// Use this for initialization
	void Start() {
		towerMaster = tower.GetComponent<TowerManager>();
		currHealth = MAX_HEALTH;
		inRange = false;
		currResources = 0;
	}
	
	// Update is called once per frame
	void Update() {
		movePlayer();	
		depositResources();
		attack();
	}
	
	// Governs degrees of freedom of player movement
	void movePlayer() {
		Vector3 moveVector = new Vector3(0, 0, 0);
		
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
			moveVector.x += movementSpeed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
			moveVector.x -= movementSpeed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {	
			moveVector.z += movementSpeed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
			moveVector.z -= movementSpeed * Time.deltaTime;
		}
		
		transform.position += moveVector;
	}
	
	void depositResources() {
		if (towerMaster.canDeposit() && currResources > 0) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				towerMaster.startDeposit();
			} else if (Input.GetKey(KeyCode.Space)) {
				if (towerMaster.deposit()) { // True when resource has been added to tower
					--currResources;
				}
			}
		}
	}
	
	void attack() {
		if (inRange && Input.GetKeyDown(KeyCode.K)) {
			target.transform.parent.gameObject.GetComponent<EnemyManager>().takeDamage(attackPower);
		}
	}
	
	public bool getResource() {
		if (currResources < MAX_RESOURCES) {
			++currResources;
			return true;
		} else {
			return false;
		}
	}	
	
	// Check whether target is within trigger sphere	
	public void targetInRange(Collider temp) { 
		// Local variable declaration
		GameObject selfTrigger = GameObject.FindWithTag("PlayerTrigger");
		Vector3 self = selfTrigger.transform.position, other = temp.transform.position;
		float distance = 0.0f;
		string checkTag = temp.transform.parent.gameObject.tag;	
		
		// Check origin of trigger
		if (checkTag == "Enemy") {
			SphereCollider otherTrigger = (SphereCollider)temp;			
			distance = selfTrigger.GetComponent<SphereCollider>().radius + otherTrigger.radius;
		} else {
			inRange = false;
			target = null;
			return;
		}
		
		// Compare actual distance to threshold	
		if (Mathf.Sqrt(Mathf.Pow(self.x - other.x, 2) + Mathf.Pow(self.z - other.z, 2)) >= distance) {
			inRange = false;
			target = null;
		} else {
			inRange = true;
			target = temp;
		}
	}
	
	public void takeDamage(float attackPower) {
		if (currHealth > 0) {
			float randSign = (Random.value > 0.5) ? 1.0f : -1.0f;
			float randOffset = (Random.value) / 2.0f;
			float amount = (1 + randSign * randOffset) * attackPower;
			Debug.Log("Player took " + amount + " damage!");
			currHealth -= amount;	
		}
		
		if (currHealth < 0) { // Don't allow negative values
			currHealth = 0;
			die();
		}
	}
	
	void die() {
		// Do something here	
	}
	
	public float getPercentageHealth() {
		return currHealth/MAX_HEALTH;
	}
}
