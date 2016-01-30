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
		string currTag = target.transform.parent.gameObject.tag;
		string checkTag = temp.transform.parent.gameObject.tag;
		if (targetInRange(temp)) {
			if (target == null) { // No target presently
				hasTarget = true;	
				target = temp;
				Debug.Log("Target acquired")
			} else { // Target already acquired; give priority to tower
				if (checkTag.Equals("Tower")) {
					target = temp;
					Debug.Log("Target switched")
				}
			}
		} else {
			if (target != null)	{ // Prior target was present
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
			}
		}
	}
	
	bool targetInRange(Collider temp) {
		// Check whether target is within trigger sphere
		if (Vector3.Distance(transform.position, temp.gameObject.transform.position)
			< transform.gameObject.GetComponentInChildren<SphereCollider>().radius) {
			return true;
		} else {
			return false;
		}
	}
}
