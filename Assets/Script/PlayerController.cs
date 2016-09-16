using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private const string HORIZONTAL = "Horizontal";
	private const string GROUND = "Ground";
	private const string JUMP = "Jump";
	private const string TAG_KILL_PLANE = "KillPlane";
	private const string TAG_CHECKPOINT = "Checkpoint";
	private const string TAG_MOVING_PLATFORM = "MovingPlatform";
	private const int JUMP_COUNT_THRESHOLD = 2;
	private float moveSpeed = 5f;
	private float jumpSpeed = 5f;
	private Rigidbody2D myRigidbody;
	public Transform groundCheck;
	private float groundCheckRadius = 0.25f;  
	private int jumpCount;
	private Animator myAnimator;
	public Vector3 respawnPosition;
	private LevelManager levelManager;
	private AudioSource jumpEffect;
	private Text coinText;

	public Vector3 getRespawnPosition(){
		return this.respawnPosition;
	}

	void Start () {
		respawnPosition = transform.position;
		myRigidbody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
		groundCheck = GameObject.Find("GroundCheck").transform;
		levelManager = FindObjectOfType<LevelManager> ();
		jumpEffect = GameObject.Find ("Jump").GetComponent<AudioSource>();
	}

	void Update() {   
		move();
		jump();
		setJumpCount();
		setAnimatorParams();
	}

	private enum Direction {
		LEFT, RIGHT, NO_DIRECTION
	}

	private bool isGrounded(){
		return Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, LayerMask.GetMask (GROUND));
	}

	private Direction getDirection() {
		if (Input.GetAxisRaw (HORIZONTAL) > 0f) {
			return Direction.RIGHT;
		} else if (Input.GetAxisRaw (HORIZONTAL) < 0f) {
			return Direction.LEFT;
		} else {
			return Direction.NO_DIRECTION;
		}
	}

	private void move() {
		switch (getDirection ()) {
		case Direction.RIGHT:
			movingToRight();
			break;
		case Direction.LEFT:
			movingToLeft();
			break;
		case Direction.NO_DIRECTION:
		default:
			idle ();
			break;
		}

	}

	private void jump() {
		if (canJump()) {
			jumpEffect.Play();
			myRigidbody.velocity = new Vector3 (myRigidbody.velocity.x, jumpSpeed , 0f);
		}
	}

	private bool canJump() {
		incrementJumpCount();
		return Input.GetButtonDown (JUMP) 
			&& (isGrounded() || (!isGrounded() && jumpCount < JUMP_COUNT_THRESHOLD));
	}

	private void incrementJumpCount(){
		if (Input.GetButtonDown (JUMP)) {
			jumpCount++;
		}
	}

	private void setJumpCount(){
		if (isGrounded()) {
			jumpCount = 0; 
		}
	}

	private void setAnimatorParams(){
		myAnimator.SetFloat ("speed", Mathf.Abs (myRigidbody.velocity.x));
		myAnimator.SetBool ("ground", isGrounded());
	}

	private void movingToRight(){
		transform.localScale = new Vector3 (1f, 1f, 1f);
		myRigidbody.velocity = new Vector3 (moveSpeed, myRigidbody.velocity.y, 0f);
	}

	private void movingToLeft(){
		transform.localScale = new Vector3 (-1f, 1f, 1f);
		myRigidbody.velocity = new Vector3 (-moveSpeed, myRigidbody.velocity.y, 0f);
	}

	private void idle(){
		myRigidbody.velocity = new Vector3 (0f, myRigidbody.velocity.y, 0f);
	}


	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == TAG_KILL_PLANE) {
			levelManager.reSpawn();
		}

		if (other.tag == TAG_CHECKPOINT) {
			respawnPosition = other.transform.position;
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == TAG_MOVING_PLATFORM){
			transform.parent = other.transform;
		}
	}

	void OnCollisionExit2D(Collision2D other){
		if(other.gameObject.tag == TAG_MOVING_PLATFORM){
			transform.parent = null;
		}
	}
}


