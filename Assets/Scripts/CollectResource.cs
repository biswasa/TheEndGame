using UnityEngine;
using System.Collections;

public class CollectResource : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Increment counter; destroy resource
	void OnTriggerEnter(Collider other) {
		Debug.Log(other.tag);
		if (other.tag == "Player") {
			Debug.Log("Caught you");
			GameObject helper = GameObject.FindWithTag("ResourceController");	
			--(helper.GetComponent<ResourceManager>().resources); // Consider making public function, hide variable
			// Increment player's resources here
			Destroy(transform.parent.gameObject);
		}
	}
}
