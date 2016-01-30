using UnityEngine;
using System.Collections;

public class EnemyTracker : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void onTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "Tower") {
			transform.parent.gameObject.GetComponent<EnemyManager>().trackTarget(other);
		}
	}
	
}
