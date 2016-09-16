using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject target;
	public float followAhead = 5f;
	private Vector3 targetPosition;
	private float smoothing = 0.5f;

	void Start () {
		target = GameObject.Find("Player");
	}
 
	void Update () {
		setFollowingPosition ();
	}

	private void setFollowingPosition(){
		targetPosition = new Vector3 (target.transform.position.x, transform.position.y, transform.position.z);
		// player is moving forward
		if (target.transform.localScale.x > 0f) {
			targetPosition = new Vector3 (targetPosition.x + followAhead, targetPosition.y, targetPosition.z);
		} else {
			targetPosition = new Vector3 (targetPosition.x - followAhead, targetPosition.y, targetPosition.z);
		}

		if (targetPosition.x > 0f) {
			transform.position = Vector3.Lerp (transform.position, targetPosition, smoothing * Time.deltaTime);
		}
	}
}
