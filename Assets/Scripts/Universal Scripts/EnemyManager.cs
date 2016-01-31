using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	private Collider target;
	private const float MAX_HEALTH = 20;
	private float movementSpeed = 5.0f;
	private float attackPower = 3.0f;
	private float fudgeFactor = 0.05f; // Account for edge case spherical trigger detection
	private float currHealth;
	private bool hasTarget; // Currently pursing player or tower

	// Use this for initialization
	void Start() {
		target = null;
		currHealth = MAX_HEALTH;
		hasTarget = false;
	}
	
	// Update is called once per frame
	void Update() {
	}
	
	public void trackTarget(Collider temp) {	
		// Identify tags
		string currTag = "", checkTag;
		if (target != null) {	
			currTag = target.transform.parent.gameObject.tag;
		}
		checkTag = temp.transform.parent.gameObject.tag;
		
		Debug.Log("current = " + currTag);
		Debug.Log("checking = " + checkTag);
		// Check validity of target
		if (targetInRange(temp)) {
			if (currTag.Equals("")) { // No target presently
				hasTarget = true;	
				target = temp;
				Debug.Log("Target acquired");
			} else { // Target already acquired; give priority to tower
				if (checkTag.Equals("Tower")) {
					target = temp;
					Debug.Log("Target switched");
				}
			}
		} else {
			if (!currTag.Equals("")) { // Prior target was present
				// Check whether prior and current target are one and the same; target goes out of range
				if (currTag.Equals(checkTag)) {
					hasTarget = false;
					target = null;
					Debug.Log("Target lost");
				} else { // Prior target is still in range
					hasTarget = true;
				}
			} else { // No target exists
				hasTarget = false;
				Debug.Log("No target");
			}
		}
	}

	// Check whether target is within trigger sphere	
	bool targetInRange(Collider temp) { 
		// Local variable declaration
		string tagCheck = temp.transform.parent.gameObject.tag;
		GameObject selfTrigger = GameObject.FindWithTag("EnemyTrigger");
		Vector3 self = selfTrigger.transform.position, other = temp.transform.position;
		float radius = 0.0f;
		
		// Check origin of trigger
		if (tagCheck.Equals("Player")) {
			SphereCollider otherTrigger = (SphereCollider)temp;			
			radius = selfTrigger.GetComponent<SphereCollider>().radius + otherTrigger.radius;
		} else if (tagCheck.Equals("Tower")) {
			CapsuleCollider otherTrigger = (CapsuleCollider)temp;
			radius = selfTrigger.GetComponent<SphereCollider>().radius + otherTrigger.radius;	
		} else {
			return false;
		}
			
		// Compare actual distance to threshold	
		if (Vector3.Distance(self, other) >= radius) {
			Debug.Log("Out");
			return false;
		} else {
			Debug.Log("In");
			return true;
		}
	}
}
