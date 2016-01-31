using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public GameObject tower;	
	
	private TowerManager towerMaster;
	private const float MAX_HEALTH = 50.0f;
	private const int MAX_RESOURCES = 10;
	private float movementSpeed = 10.0f;
	private float currHealth;
	private float attackPower = 5.0f;
	public int currResources;
	
	// Use this for initialization
	void Start() {
		towerMaster = tower.GetComponent<TowerManager>();
		currHealth = MAX_HEALTH;
		currResources = 0;
	}
	
	// Update is called once per frame
	void Update() {
		movePlayer();	
		depositResources();
	}
	
	// Governs degrees of freedom of player movement
	void movePlayer()
	{
		Vector3 moveVector = new Vector3 (0, 0, 0);
		
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
			moveVector.x -= movementSpeed * Time.deltaTime;
		} else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
			moveVector.x += movementSpeed * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {	
			moveVector.z -= movementSpeed * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
			moveVector.z += movementSpeed * Time.deltaTime;
		}
		
		transform.position += moveVector;
	}
	
	void depositResources() {
		if (towerMaster.canDeposit() && currResources > 0) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				towerMaster.startDeposit();
			} else if (Input.GetKey(KeyCode.Space)) {
				if(towerMaster.deposit()) { // True when resource has been added to tower
					--currResources;
				}
			}
		}
	}
	
	public bool getResource() {
		if (currResources < MAX_RESOURCES) {
			++currResources;
			return true;
		} else {
			return false;
		}
	}
	
	public void damage(float attackPower) {
		float randSign = (Random.value > 0.5) ? 1.0f : -1.0f;
		float randOffset = (Random.value)/2;
		currHealth -= randSign * randOffset * attackPower;
	}
}
