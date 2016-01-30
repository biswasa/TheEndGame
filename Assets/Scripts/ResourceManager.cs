﻿using UnityEngine;
using System.Collections;

public class ResourceManager : MonoBehaviour {

	public Transform resourcePrefab;
	
	private const float MIN_RESPAWN_TIME = 10.0f;
	private float resourceSpawnerSpeed = 15.0f;
	private int maxResources = 5;
	public int resources;
	public float lastRespawn;

	// Use this for initialization
	void Start () {
		resources = 0;	
		lastRespawn = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		moveResourceSpawner();
		if ((resources < maxResources) && ((Time.time - lastRespawn) > (1 + Random.value) * MIN_RESPAWN_TIME)) {
			respawn();
		}
	}
	
	// Creates a new resource at the spawner location
	void respawn() {
		Object spawnedResource = Instantiate(resourcePrefab, transform.position, Quaternion.identity);
		
		// Sanity checking
		if(spawnedResource != null) {
			resources++;
			lastRespawn = Time.time;
		} else {
			Debug.Log ("I don't exist");
		}
	}
		
	// Randomly moves resource spawner within player-accessible area
	void moveResourceSpawner() {
		float speed = resourceSpawnerSpeed * Time.deltaTime;
		float randOrientation = Random.value;
		if (randOrientation <= 0.25f) {	
			transform.position += new Vector3((1 + Random.value) * speed, 0, (1 + Random.value) * speed);
		} else if (randOrientation <= 0.5f) {
			transform.position += new Vector3(-(1 + Random.value) * speed, 0, (1 + Random.value) * speed);	
		} else if (randOrientation <= 0.75f) {
			transform.position += new Vector3((1 + Random.value) * speed, 0, -(1 + Random.value) * speed);
		} else {
			transform.position += new Vector3(-(1 + Random.value) * speed, 0, -(1 + Random.value) * speed);
		}
	}
	
}
