using UnityEngine;
using System.Collections;

public class TowerTracker : MonoBehaviour {

	private bool isActive; // True if player can deposit resources
	
	// Use this for initialization
	void Start() {
	
	}
	
	// Update is called once per frame
	void Update() {
	
	}
	
	void OnTriggerEnter(Collider other) {
		transform.parent.gameObject.GetComponent<TowerManager>().playerInRange(other);
	}
	
	void OnTriggerExit(Collider other) {
		transform.parent.gameObject.GetComponent<TowerManager>().playerInRange(other);
	}
}
