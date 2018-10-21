using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	
	// public variables
	public float moveSpeed = 3.0f;
	public float jumpSpeed = 10.0f;
	public float gravity = 9.81f;

	private CharacterController myController;
	private Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Start () {

		myController = gameObject.GetComponent<CharacterController>();
	}

	void Update() {

		if (myController.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= moveSpeed;
			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;

		}
		moveDirection.y -= gravity * Time.deltaTime;	

		myController.Move(moveDirection * Time.deltaTime);
	}
}
