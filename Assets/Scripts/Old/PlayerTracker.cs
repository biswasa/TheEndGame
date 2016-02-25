using UnityEngine;
using System.Collections;

public class PlayerTracker : MonoBehaviour {

	private PlayerManager manager;

	// Use this for initialization
	void Start () {
		manager = transform.parent.gameObject.GetComponent<PlayerManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		manager.targetInRange(other);
	}
	
	void OnTriggerExit(Collider other) {
		manager.targetInRange(other);
	}
}
