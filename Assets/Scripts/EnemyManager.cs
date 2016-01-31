using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

	private Collider target;
	private const float MAX_HEALTH = 20;
	private float movementSpeed = 5.0f;
	private float attackPower = 3.0f;
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
	
	bool targetInRange(Collider temp) {
		// Check whether target is within trigger sphere
		Vector3 self = transform.position;
		Vector3 other = temp.transform.parent.transform.position;
		GameObject sphereTrigger = GameObject.FindWithTag("EnemyTrigger");
		float radius = sphereTrigger.GetComponent<SphereCollider>().radius;
		Debug.Log("THIS IS IT: " + radius);
		
		if (Vector3.Distance(self, other) < radius) {
			Debug.Log("In");
			return true;
		} else {
			Debug.Log("Out");
			return false;
		}
	}
}
