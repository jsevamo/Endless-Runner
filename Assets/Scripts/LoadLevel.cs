using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour {

	public List<List<string>> Users = new List<List<string>> ();

	List<string> User1 = new List<string> ();
	List<string> User2 = new List<string> ();

	bool canStart;

	public InputField InputUsuario;
	public Text Sorry;
	public GameObject Player;
	public GameObject GC;
	CharacterScript player;
	GameController gc;
	int index;

	public string userName;

	// Use this for initialization
	void Start () {

		player = Player.GetComponent<CharacterScript> ();
		gc = GC.GetComponent<GameController> ();
		

		User1.Add ("j_sevamo");
		User1.Add ("5");

		User2.Add ("felipito");
		User2.Add ("10");

		Users.Add (User1);
		Users.Add (User2);

		canStart = false;

		Sorry.gameObject.SetActive (false);




		
	}
	
	// Update is called once per frame
	void Update () {

	}


	public void loadGame()
	{

		string givenName = InputUsuario.text;

		for (int i = 0; i < Users.Count; i++) {
			if (givenName == Users [i][0]) {
				canStart = true;
				index = i;
				break;
			}
		}

		if (canStart) {
			gc.userName = givenName;
			player.lifes = int.Parse(Users [index] [1]);
			SceneManager.LoadScene ("Runner");
		} else {
			Sorry.gameObject.SetActive (true);
		}




	}
}
