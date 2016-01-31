using UnityEngine;
using System.Collections;

public class EnemyTracker : MonoBehaviour {

	// Use this for initialization
	void Start() {
	
	}
	
	// Update is called once per frame
	void Update() {
	
	}
	
	void OnTriggerEnter(Collider other) {
		transform.parent.gameObject.GetComponent<EnemyManager>().trackTarget(other);
	}
	
	void OnTriggerExit(Collider other) {
		transform.parent.gameObject.GetComponent<EnemyManager>().trackTarget(other);
	}
	
	void OnCollisionEnter(Collision collision) {
		Debug.Log("Pika");
		transform.parent.gameObject.GetComponent<EnemyManager>().reverseDirection();
	}
}
