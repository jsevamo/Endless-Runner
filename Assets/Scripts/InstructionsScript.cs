using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsScript : MonoBehaviour {

	public string username;
	public int lifes;

	public GameObject GC;
	GameController gc;

	public GameObject Player;
	CharacterScript player;

	// Use this for initialization
	void Start () {
		gc = GC.GetComponent<GameController> ();
		player = Player.GetComponent<CharacterScript> ();
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


}
