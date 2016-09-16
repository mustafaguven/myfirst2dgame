using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	private LevelManager levelManager;
	public int coinValue;

	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();
		 
	}
	
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			levelManager.addCoins (coinValue);

			Destroy (gameObject);
		}
	}
}
