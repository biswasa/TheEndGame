using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	private Collider target;
	private const float MAX_HEALTH = 20;
	private float moveTime = 3.0f; // Duration of linear motion
	private float attackTime = 2.0f; // Delay in executing attack
	private float movementSpeed = 5.0f;
	private float attackPower = 3.0f;
	private float currHealth;
	private float motionStartTime;
	private float attackStartTime;
	private float limit; // Map bounds
	public bool hasTarget; // Currently pursing player or tower
	public bool moving; // Movement cycles in process
	public bool attacking; // Attack cycles in process
	public string targetTag; // Identifier for target
	public Vector3 currDirection;

	// Use this for initialization
	void Start() {
		target = null;
		currHealth = MAX_HEALTH;
		motionStartTime = 0.0f;
		attackStartTime = 0.0f;
		limit = GameObject.FindWithTag("GameController").GetComponent<GameManager>().getMapBounds();
		hasTarget = false;
		attacking = false;
		targetTag = "";
		currDirection = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update() {
		chooseAction();
	}
	
	public void trackTarget(Collider temp) {	
		// Identify tags
		string checkTag;
		if (target != null) {	
			targetTag = target.transform.parent.gameObject.tag;
		}
		checkTag = temp.transform.parent.gameObject.tag;
	
		// Check validity of target
		if (targetInRange(temp)) {
			if (targetTag.Equals("")) { // No target presently
				hasTarget = true;	
				target = temp;
			} else { // Target already acquired; give priority to tower
				if (checkTag.Equals("Tower")) {
					target = temp;
				}
			}
		} else {
			if (!targetTag.Equals("")) { // Prior target was present
				// Check whether prior and current target are one and the same; target goes out of range
				if (targetTag.Equals(checkTag)) { 
					hasTarget = false;
					target = null;
				} else { // Prior target is still in range
					hasTarget = true;
				}
			} else { // No target exists
				hasTarget = false;
			}
		}
	}

	// Check whether target is within trigger sphere	
	bool targetInRange(Collider temp) { 
		// Local variable declaration
		GameObject selfTrigger = GameObject.FindWithTag("EnemyTrigger");
		Vector3 self = selfTrigger.transform.position, other = temp.transform.position;
		float radius = 0.0f;
		
		// Check origin of trigger
		if (targetTag.Equals("Player")) {
			SphereCollider otherTrigger = (SphereCollider)temp;			
			radius = selfTrigger.GetComponent<SphereCollider>().radius + otherTrigger.radius;
		} else if (targetTag.Equals("Tower")) {
			CapsuleCollider otherTrigger = (CapsuleCollider)temp;
			radius = selfTrigger.GetComponent<SphereCollider>().radius + otherTrigger.radius;	
		} else {
			reverseDirection();
			return false;
		}
			
		// Compare actual distance to threshold	
		if (Vector3.Distance(self, other) >= radius) {
			return false;
		} else {
			return true;
		}
	}
	
	// AI algorithms
	void chooseAction() {
		if(!hasTarget) { // Random linear movement
			attacking = false;	
			if (!moving) {
				startMotion();
			}
			move();
		} else { // Attack enemy
			moving = false;
			if (!attacking) {
				startAttack();
			}
			attack();	
		}
	}	
	
	void move() {
		if (moving) {
			if ((Time.time - motionStartTime) < moveTime) { // Continue previous motion
				// Resume movement 
				if (currDirection.Equals(Vector3.zero)) {
					randomizeDirection();
				} 
				
				// Get object back on track
				Debug.Log(limit - 1);
				Debug.Log(transform.position.x);
				Debug.Log(transform.position.z);
				if ((Mathf.Abs(transform.position.x) > (limit - 1)) || (Mathf.Abs(transform.position.z) > (limit - 1))) {
					//Debug.Log("Reversed");
					reverseDirection();
				}
				
				transform.position += currDirection * movementSpeed * Time.deltaTime;
			} else { // Stop current phase of movement
				currDirection = Vector3.zero;
				moving = false;
			}
		}
	}
	
	void startMotion() {
		moving = true;
		motionStartTime = Time.time;
	}
	
	void randomizeDirection() {
		float rand = Random.value;
		if (rand <= 0.25f) {
			currDirection = Vector3.left;
		} else if (rand <= 0.50f) {
			currDirection = Vector3.right;
		} else if (rand <= 0.75f) {
			currDirection = Vector3.forward;
		} else {
			currDirection = Vector3.back;
		}	
		startMotion();
	}
	
	// Negates both x and y coordinates; to be called on collision with another object
	// Also call if trigger object cannot be attacked.
	public void reverseDirection() { 
		currDirection = -currDirection;
		startMotion();
	}
	
	void startAttack() {
		attacking = true;
		attackStartTime = Time.time;
	}	
	
	void attack() {
		if (attacking && ((Time.time - attackStartTime) > attackTime)) {
			// Do some attack animation/sound here
			
			Debug.Log("Attacked the player!");
			GameObject victim = GameObject.FindWithTag(targetTag);
			
			if (targetTag == "Player") {
				victim.GetComponent<PlayerManager>().damage(attackPower);
			} else if (targetTag == "Tower") {
				victim.GetComponent<TowerManager>().damage(attackPower);
			}
			
			// Complete attack
			attacking = false;
		}
	}
}
