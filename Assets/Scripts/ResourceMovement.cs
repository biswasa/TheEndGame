using UnityEngine;
using System.Collections;

public class ResourceMovement : MonoBehaviour {

	private float fallSpeed = 5.0f;
	private const float BASE_HEIGHT = 0.5f;

	// Use this for initialization
	void Start() {
	
	}
	
	// Update is called once per frame
	void Update() {
		if (transform.position.y > BASE_HEIGHT) {
			fall();
		}
	}
	
	void fall() {
		transform.position += Vector3.down * fallSpeed * Time.deltaTime;
		transform.Rotate(Vector3.right * Time.deltaTime);
	}
	
	void OnTriggerEnter(Collider other) {
		Debug.Log("Caught you");
		if (other.tag == "Player") {
			--(this.GetComponentInParent<ResourceManager>().resources);
			Destroy(gameObject);
		}
	}
}
