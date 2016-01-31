using UnityEngine;
using System.Collections;

public class TowerManager : MonoBehaviour {

	public int currResources;
	public bool levelUp;
	public bool victory;
	
	private const float MAX_HEALTH = 100.0f;
	private enum RESOURCE_TIERS {
		ZERO = 0,
		ALPHA = 10,
		BETA = 25,
		GAMMA = 50}
	; // Number of resources required to level up
	private float depositDelay = 2.0f; // Time taken to deposit 1 unit resource
	private float deathTime = 3.0f; // Time required for death animation/sound
	private float winTime = 3.0f; // Time required for victory animation/sound
	private float depositStartTime;
	private float currHealth;
	private bool depositAllowed;
	
	// Use this for initialization
	void Start() {
		depositStartTime = 0.0f;
		currHealth = MAX_HEALTH;
		currResources = 0;
		depositAllowed = false;
		levelUp = false;
		victory = false;
	}
	
	// Update is called once per frame
	void Update() {
	
	}
	
	public bool canDeposit() {
		return depositAllowed;	
	}	
	
	// Starts counting time elapsed for single resource deposit
	public void startDeposit() {
		depositStartTime = Time.time;
	}
	
	// Deposits items if enough time has elasped; true -> deposited
	public bool deposit() {
		if ((Time.time - depositStartTime) > depositDelay) {
			depositStartTime = Time.time;
			++currResources;	
			if (currResources == (int)RESOURCE_TIERS.GAMMA) {
				win();			
			} else if ((currResources == (int)RESOURCE_TIERS.BETA) || (currResources == (int)RESOURCE_TIERS.ALPHA)) {
				levelUp = true;
			}
			return true;	
		} else {
			return false;
		}
	}	
	
	public void playerInRange(Collider temp) {
		// Local variable declaration
		string tagCheck = temp.transform.parent.gameObject.tag;
		GameObject selfTrigger = GameObject.FindWithTag("TowerTrigger");	
		Vector3 self = selfTrigger.transform.position, other = temp.transform.position;
		float radius = 0.0f;
		
		// Check origin of trigger
		if (tagCheck.Equals("Player")) {
			SphereCollider otherTrigger = (SphereCollider)temp;			
			radius = selfTrigger.GetComponent<SphereCollider>().radius + otherTrigger.radius;
		} else { // Not interested in non-player objects
			return;
		}
		
		// Compare actual distance to threshold	
		if (Vector3.Distance(self, other) >= radius) {
			depositAllowed = false;
		} else {
			depositAllowed = true;
		}
	}
	
	public void takeDamage(float attackPower) {
		if (currHealth > 0) {
			float randSign = (Random.value > 0.5) ? 1.0f : -1.0f;
			float randOffset = (Random.value) / 2.0f;
			float amount = (1 + randSign * randOffset) * attackPower;
			Debug.Log("Tower took " + amount + " damage!");
			currHealth -= amount;	
		}
		
		if (currHealth <= 0) { // Object dies 
			currHealth = 0; // Don't allow negative values
			die();
		}
	}
	
	void die() {
		// Death sound/animation here
		float startDeath = Time.time;
		while (Time.time - startDeath < deathTime) {
			// Do nothing/loop effects
		}	
		Application.LoadLevel("GameOver");
	}
	
	void win() {
		// Victory sound/animation here
		float startWin = Time.time;
		while (Time.time - startWin < deathTime) {
			// Do nothing/loop effects
		}	
		Application.LoadLevel("Victory");
	}

	public float getPercentageHealth() {
		return currHealth/MAX_HEALTH;
	}
	
	public float getPercentageResources() {
		int max = 0;
		if (currResources < (int)RESOURCE_TIERS.ALPHA) {
			max = (int)RESOURCE_TIERS.ALPHA;
		} else if (currResources < (int)RESOURCE_TIERS.BETA) {
			max = (int)RESOURCE_TIERS.BETA;
		} else {
			max = (int)RESOURCE_TIERS.GAMMA;
		}
		
		return currResources/(float)max;
	}
}
