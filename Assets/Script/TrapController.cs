using UnityEngine;
using System.Collections;

public class TrapController : MonoBehaviour {

	LevelManager levelManager;
	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();
	}
	
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			levelManager.reSpawn ();
		}
	}
}
