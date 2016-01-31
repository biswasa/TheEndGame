using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject resourceSpawner;
	private float mapBounds = 15.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		// Make sure spawner is not out of bounds
		resourceSpawner.GetComponent<ResourceSpawner>().reset(mapBounds);
	}	
	
	public float getMapBounds() {
		return mapBounds;
	}

}
