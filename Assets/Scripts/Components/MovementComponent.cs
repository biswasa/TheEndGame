using UnityEngine;
using System.Collections;

public class MovementComponent : MonoBehaviour, IMoveable {

	[SerializeField]
	private float movementSpeed;

	public void move(Rigidbody2D body, Vector2 direction) {
		body.velocity = direction * movementSpeed;
	}
}
