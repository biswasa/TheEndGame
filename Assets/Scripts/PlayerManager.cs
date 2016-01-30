using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	private const float MAX_HEALTH = 50.0f;
	private const int MAX_RESOURCES = 10;
	private float movementSpeed = 10.0f;
	private float currHealth;
	private int currResources;
	
	// Use this for initialization
	void Start() {
		currHealth = MAX_HEALTH;
		currResources = 0;
	}
	
	// Update is called once per frame
	void Update() {
		movePlayer();	
	}
	
	// Governs degrees of freedom of player movement
	void movePlayer()
	{
		Vector3 moveVector = new Vector3 (0, 0, 0);
		
		if (Input.GetKey (KeyCode.LeftArrow)) {
			moveVector.x -= movementSpeed * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			moveVector.x += movementSpeed * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.DownArrow)) {	
			moveVector.z -= movementSpeed * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.UpArrow)) {
			moveVector.z += movementSpeed * Time.deltaTime;
		}
		
		transform.position += moveVector;
	}
	
	public bool getResource() {
		if (currResources < MAX_RESOURCES) {
			++currResources;
			return true;
		} else {
			return false;
		}
	}
}
