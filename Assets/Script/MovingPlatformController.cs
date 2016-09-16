using UnityEngine;
using System.Collections;

public class MovingPlatformController : MonoBehaviour {

	public GameObject objectToMove;
	public Transform startPoint;
	public Transform endPoint;
	private Vector3 currentTarget;
	private float moveSpeed = 3f;


	void Start () {
		/*objectToMove = GameObject.Find ("HalfHangingPlatform");
		startPoint = GameObject.Find ("Start").transform;
		endPoint = GameObject.Find ("End").transform;*/
		currentTarget = endPoint.position;
	}
	
	void Update () {
		objectToMove.transform.position = Vector3.MoveTowards (objectToMove.transform.position, currentTarget, moveSpeed * Time.deltaTime);

		if (isTouchedToLeft()) {
			currentTarget = endPoint.position;
		}
		if (isTouchedToRight()) {
			currentTarget = startPoint.position;
		}

	}

	private bool isTouchedToRight(){
		return objectToMove.transform.position == endPoint.position;
	}

	private bool isTouchedToLeft(){
		return objectToMove.transform.position == startPoint.position;
	}


}
