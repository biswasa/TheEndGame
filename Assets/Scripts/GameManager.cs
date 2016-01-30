using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject resourceSpawner;

	private float mapBounds;
	private int resources;

	// Use this for initialization
	void Start () {
		mapBounds = 15.0f;	
		resources = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
}
