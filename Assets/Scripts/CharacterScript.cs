using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{

	Rigidbody2D rb;
	public int JumpForce = 1500;
	Vector2 force;
	public bool IsGrounded;
	int i;
	Animator anim;
	public bool IsDead;
	public bool hasReseted;

	// Use this for initialization
	void Start ()
	{

		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		IsGrounded = false;
		IsDead = false;
		i = 0;
		hasReseted = false;
		force = new Vector2 (0, JumpForce);
		
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (IsGrounded == true) {

			if (Input.GetButtonDown ("Fire1")) {
				rb.AddForce (force);
				IsGrounded = false;
			}

		} else {
			anim.SetBool ("isJumping", true);
		}

		Debug.Log (rb.velocity.magnitude);

			
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Ground")) {
			anim.SetBool ("isJumping", false);
			IsGrounded = true;
			//Debug.Log (IsGrounded);

		} 

		if (other.gameObject.CompareTag ("Obstacle")) {
			//Debug.Log ("You died");
			IsDead = true;
			rb.velocity = Vector3.zero;
			force = Vector2.zero;
			Time.timeScale = 0f;
		} 

	}

	public void resetgame()
	{
		force = new Vector2 (0, JumpForce);
		rb.velocity = Vector3.zero;
		transform.position = new Vector3 (-6.44f, 1.6f, -1f);
		hasReseted = true;
		IsDead = false;
		IsGrounded = false;

	}
}
