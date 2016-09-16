using UnityEngine;
using System.Collections;

public class DestroyOverTime : MonoBehaviour {

	private float particleLifeTime = 1.5f;

	void Start () {
		
	}
	
	void Update () {
		particleLifeTime = particleLifeTime - Time.deltaTime;
		if (particleLifeTime < 0f) {
			Destroy (gameObject);
		}
	}
}
