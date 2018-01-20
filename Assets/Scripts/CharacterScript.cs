using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{

	Rigidbody2D rb;
	public int JumpForce = 1500;
	Vector2 force;
	public bool IsGrounded;
	Animator anim;
	public bool IsDead;
	public bool hasReseted;
	public int lifes = 3;
	GameController GC;

	[HideInInspector] public float totalGameTime;

	// Use this for initialization
	void Start ()
	{

		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		IsGrounded = false;
		IsDead = false;
		hasReseted = false;
		force = new Vector2 (0, JumpForce);
		totalGameTime = 0;

		GC = GameObject.Find ("GameController").GetComponent<GameController> ();
		GC.lifeCountText.text = "x" + lifes.ToString ();
		GC.tryAgainButton.gameObject.SetActive (false);
		
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


		Debug.Log (totalGameTime);

			
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("Ground")) {
			anim.SetBool ("isJumping", false);
			IsGrounded = true;
			//GC.grabbedCollect = false;
			//Debug.Log (IsGrounded);

		} 

		if (other.gameObject.CompareTag ("Obstacle")) {
			//Debug.Log ("You died");

			IsDead = true;
			rb.velocity = Vector3.zero;
			force = Vector2.zero;
			Time.timeScale = 0f;
			lifes--;
			GC.usernameText.gameObject.SetActive (true);
			GC.pointsText.gameObject.SetActive (false);
			GC.lifeCountText.gameObject.SetActive (false);
			GC.timePlayedText.gameObject.SetActive (false);
			GC.timeImage.gameObject.SetActive (false);
			GC.pointsImage.gameObject.SetActive (false);
			GC.heartImage.gameObject.SetActive (false);
			GC.QuitButton.gameObject.SetActive (true);


			if (lifes > 0) {
				if (lifes == 1) {
					//GC.losingText.text = "¡Oh no! Te queda " + lifes.ToString () + " vida. ¿Quieres intentar de nuevo?";
				} else {
					//GC.losingText.text = "¡Oh no! Te quedan " + lifes.ToString () + " vidas. ¿Quieres intentar de nuevo?";
				}
			} else {
				//GC.losingText.fontSize = 65;
				GC.lifeCountText.gameObject.SetActive (false);
				GC.tryAgainButton.gameObject.SetActive (false);
				GC.QuitButton.gameObject.SetActive (false);
				GC.FinalQuitButton.gameObject.SetActive (true);
				//GC.losingText.text = "No te quedan mas vidas. ¡Ingresa mas cupones en la pagina para redimir mas!";
				//Debug.Log (Time.realtimeSinceStartup);
				totalGameTime = Time.deltaTime;
			}



			//GC.losingText.gameObject.SetActive (true);
			GC.tryAgainButton.gameObject.SetActive (true);

			if (lifes == 0) {
				GC.tryAgainButton.gameObject.SetActive (false);
			}
		} 

		if (other.gameObject.CompareTag ("Collectible")) {
			other.gameObject.SetActive (false);
			GC.oneHundred.gameObject.SetActive (true);
			GC.grabbedCollect = true;
			StartCoroutine(stopPoints());
			StartCoroutine(showPointsUI());
		}
			

	}

	IEnumerator stopPoints()
	{
		yield return new WaitForEndOfFrame();
		GC.grabbedCollect = false;
	}

	IEnumerator showPointsUI()
	{	
		yield return new WaitForSeconds (0.5f);
		GC.oneHundred.gameObject.SetActive (false);
	}

	public void resetgame()
	{
		force = new Vector2 (0, JumpForce);
		rb.velocity = Vector3.zero;
		transform.position = new Vector3 (-6.44f, 1.6f, -1f);
		hasReseted = true;
		IsDead = false;
		IsGrounded = false;
		GC.lifeCountText.text = "x" + lifes.ToString ();
		//GC.losingText.gameObject.SetActive (false);
		GC.tryAgainScreen.gameObject.SetActive(false);
		GC.tryAgainButton.gameObject.SetActive (false);
		GC.usernameText.gameObject.SetActive (false);
		GC.pointsText.gameObject.SetActive (true);
		GC.lifeCountText.gameObject.SetActive (true);
		GC.timePlayedText.gameObject.SetActive (true);
		GC.timeImage.gameObject.SetActive (true);
		GC.pointsImage.gameObject.SetActive (true);
		GC.heartImage.gameObject.SetActive (true);
		GC.pointsWhenLost.gameObject.SetActive (false);
		GC.QuitButton.gameObject.SetActive (false);
		GC.FinalQuitButton.gameObject.SetActive (false);
	}
}
