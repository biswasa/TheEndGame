﻿using UnityEngine;
using System.Collections;

public class TowerManager : MonoBehaviour {

	private const float MAX_HEALTH = 100.0f;
	private enum RESOURCE_TIERS {
		ZERO = 0,
		ALPHA = 10,
		BETA = 25,
		GAMMA = 50}
	; // Number of resources required to level up
	private float depositDelay = 2.0f; // Time taken to deposit 1 unit resource
	private float depositStartTime;
	private float currHealth;
	public int currResources;
	private bool depositAllowed;
	
	// Use this for initialization
	void Start() {
		depositStartTime = 0.0f;
		currHealth = MAX_HEALTH;
		currResources = 0;
		depositAllowed = false;
	}
	
	// Update is called once per frame
	void Update() {
	
	}
	
	public bool canDeposit() {
		return depositAllowed;	
	}	
	
	// Starts counting time elapsed for single resource deposit
	public void startDeposit() {
		depositStartTime = Time.time;
	}
	
	// Deposits items if enough time has elasped; true -> deposited
	public bool deposit() {
		if ((Time.time - depositStartTime) > depositDelay) {
			depositStartTime = Time.time;
			++currResources;	
			return true;	
		} else {
			return false;
		}
	}	
	
	public void playerInRange(Collider temp) {
		// Local variable declaration
		string tagCheck = temp.transform.parent.gameObject.tag;
		GameObject selfTrigger = GameObject.FindWithTag("TowerTrigger");	
		Vector3 self = selfTrigger.transform.position, other = temp.transform.position;
		float radius = 0.0f;
		
		// Check origin of trigger
		if (tagCheck.Equals("Player")) {
			SphereCollider otherTrigger = (SphereCollider)temp;			
			radius = selfTrigger.GetComponent<SphereCollider>().radius + otherTrigger.radius;
		} else { // Not interested in non-player objects
			return;
		}
		
		// Compare actual distance to threshold	
		if (Vector3.Distance(self, other) >= radius) {
			depositAllowed = false;
		} else {
			depositAllowed = true;
		}
	}
	
	public void takeDamage(float attackPower) {
		if (currHealth > 0) {
			float randSign = (Random.value > 0.5) ? 1.0f : -1.0f;
			float randOffset = (Random.value) / 2.0f;
			float amount = (1 + randSign * randOffset) * attackPower;
			Debug.Log("Tower took " + amount + " damage!");
			currHealth -= amount;	
		}
		
		if (currHealth <= 0) { // Object dies 
			currHealth = 0; // Don't allow negative values
			die();
		}
	}
	
	void die() {
		// Do something here	
	}
}
