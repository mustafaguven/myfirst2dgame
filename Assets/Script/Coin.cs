using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	private LevelManager levelManager;
	private AudioSource taken;
	public int coinValue;

	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();
		taken = GameObject.Find ("Taken").GetComponent<AudioSource> ();
		 
	}
	
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			levelManager.addCoins (coinValue);
			//taken.Play ();
			Destroy (gameObject);
		}
	}
}
