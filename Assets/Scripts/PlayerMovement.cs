using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

	private float movementSpeed = 10.0f;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		movePlayer ();
	}
	
	// Governs degrees of freedom of player movement
	void movePlayer ()
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
}
