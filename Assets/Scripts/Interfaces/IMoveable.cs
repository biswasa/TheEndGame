using UnityEngine;
using System.Collections;

public interface IMoveable {
	// Methods
	void move(Rigidbody2D body, Vector2 direction);
}
