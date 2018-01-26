using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterLogin : MonoBehaviour {

	public string username;
	public int lifes;

	public GameObject GC;
	GameController gc;

	public GameObject Player;
	CharacterScript player;

	public GameObject Instructions;
	InstructionsScript instructions;


	AudioSource menuAudio;


	// Use this for initialization
	void Start () {
		gc = GC.GetComponent<GameController> ();
		player = Player.GetComponent<CharacterScript> ();
		instructions = Instructions.GetComponent<InstructionsScript> ();

		menuAudio = GameObject.Find ("Soundtrack").GetComponent<AudioSource> ();

		if (menuAudio.isPlaying == false) {
			menuAudio.Play ();
		}
	}
	
	// Update is called once per frame
	void Update () {


	}

	public void loadFullGame()
	{
		gc.userName = username;
		player.lifes = lifes;
		SceneManager.LoadScene ("Runner");
	}

	public void goBack()
	{
		SceneManager.LoadScene ("MainMenu");
	}

	public void instructionsGoto()
	{
		instructions.username = username;
		instructions.lifes = lifes;
		SceneManager.LoadScene ("Instructions");
	}
}
