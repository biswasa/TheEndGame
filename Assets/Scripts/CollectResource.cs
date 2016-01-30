using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

	public ResourceManager helper;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other) {
		Debug.Log("Caught you");
		if (other.tag == "Player") {
			--helper.resources;	
			Destroy(transform.parent.gameObject);
		}
	}
}
