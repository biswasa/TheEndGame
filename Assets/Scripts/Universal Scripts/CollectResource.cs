using UnityEngine;
using System.Collections;

public class CollectResource : MonoBehaviour {

	// Use this for initialization
	void Start() {
	
	}
	
	// Update is called once per frame
	void Update() {
	
	}
	
	// Increment counter; destroy resource
	void OnTriggerEnter(Collider other) {
		float minYPos = transform.parent.gameObject.GetComponent<ResourceManager>().getBaseHeight();
		if (transform.position.y < (2 * minYPos) && other.transform.parent.gameObject.tag == "Player") {
			
			// Check whether resource can be collected; collect if possible
			bool collected = other.gameObject.transform.parent.GetComponent<PlayerManager>().getResource();
			
			// Update overworld resource count
			GameObject helper = GameObject.FindWithTag("ResourceController");	
			helper.GetComponent<ResourceSpawner>().collectResource();
			
			// Despawn resource if collected
			if (collected) {
				Destroy(transform.parent.gameObject);	
			}	
		}
	}
}
