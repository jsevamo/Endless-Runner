using System.Collections;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{

	Rigidbody2D rb;
	public int JumpForce = 700;
	Vector2 force;
	public bool IsGrounded;
	Animator anim;
	public bool IsDead;
	public bool hasReseted;
	public int lifes = LoadLevel.profile.lifes;
	GameController GC;
	AudioSource grabbedCoin;
	AudioSource lostMusic;

	[HideInInspector] public float totalGameTime;



	// Use this for initialization
	void Start ()
	{

		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		grabbedCoin = GetComponent<AudioSource> ();
		lostMusic = GameObject.Find ("MusicDeath").GetComponent<AudioSource> ();
		IsGrounded = false;
		IsDead = false;
		hasReseted = false;
		force = new Vector2 (0, JumpForce);
		totalGameTime = 0;

		GC = GameObject.Find ("GameController").GetComponent<GameController> ();
		GC.lifeCountText.text = "x" + LoadLevel.profile.lifes.ToString ();
		GC.tryAgainButton.gameObject.SetActive (false);
		
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (IsGrounded == true) {

			if (Input.GetKey("up") && GC.canJump)  {
				IsGrounded = false;
				rb.AddForce (force, ForceMode2D.Impulse);
			}

		} else {
			
			anim.SetBool ("isJumping", true);
		}


		if (transform.position.y < -1.70f) {
			IsGrounded = true;
			anim.SetBool ("isJumping", false);
		} else {
			IsGrounded = false;
		}

		if (rb.velocity.y > 24f) {
			rb.velocity = new Vector3(0, 24f, 0);
		}

			
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

			lostMusic.Play();
			IsDead = true;
			rb.velocity = Vector3.zero;
			force = Vector2.zero;
			Time.timeScale = 0f;
            LoadLevel.profile.lifes--;
			GC.usernameText.gameObject.SetActive (true);
			GC.pointsText.gameObject.SetActive (false);
			GC.lifeCountText.gameObject.SetActive (false);
			GC.timePlayedText.gameObject.SetActive (false);
			GC.timeImage.gameObject.SetActive (false);
			GC.pointsImage.gameObject.SetActive (false);
			GC.heartImage.gameObject.SetActive (false);
			GC.QuitButton.gameObject.SetActive (true);
			GC.ExitCorner.gameObject.SetActive (false);



			if (LoadLevel.profile.lifes > 0) {
				if (LoadLevel.profile.lifes == 1) {
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
				//totalGameTime = Time.deltaTime;
			}



			//GC.losingText.gameObject.SetActive (true);
			GC.tryAgainButton.gameObject.SetActive (true);

			if (LoadLevel.profile.lifes == 0) {
				GC.tryAgainButton.gameObject.SetActive (false);
			}
		} 

		if (other.gameObject.CompareTag ("Collectible")) {
			grabbedCoin.Play ();
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
		transform.position = new Vector3 (-4.54f, 1.94f, -1f);
		hasReseted = true;
		IsDead = false;
		IsGrounded = false;
		GC.lifeCountText.text = "x" + LoadLevel.profile.lifes.ToString ();
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
		GC.ExitCorner.gameObject.SetActive (true);
	}
}
