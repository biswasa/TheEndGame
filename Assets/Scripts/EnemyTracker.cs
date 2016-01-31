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
		triggerHelper(other);
	}
	
	void OnTriggerExit(Collider other) {
		triggerHelper(other);
	}

	void triggerHelper(Collider temp) {
		string tagCheck = temp.transform.parent.gameObject.tag;
		Debug.Log("Real check = " + tagCheck);
		if (tagCheck.Equals("Player") || tagCheck.Equals("Tower")) {
			transform.parent.gameObject.GetComponent<EnemyManager>().trackTarget(temp);
		}
	}	
}
