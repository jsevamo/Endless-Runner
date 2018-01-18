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
	//string password = "";
	public Font font;

	public string userName;
	public string passWord;

	// Use this for initialization
	void Start () {

		player = Player.GetComponent<CharacterScript> ();
		gc = GC.GetComponent<GameController> ();
		

		User1.Add ("j_sevamo"); //Username
		User1.Add ("1"); //Vidas
		User1.Add ("carne"); //password

		User2.Add ("felipito");
		User2.Add ("11");
		User2.Add ("pollo");

		Users.Add (User1);
		Users.Add (User2);

		canStart = false;

		Sorry.gameObject.SetActive (false);

		InputUsuario.asteriskChar = "$!£%&*"[0];





		
	}

	void OnGUI()
	{
		
		GUI.skin.label.font = GUI.skin.button.font = GUI.skin.box.font = font;
		GUI.skin.label.fontSize = GUI.skin.box.fontSize = GUI.skin.button.fontSize = 50;
		passWord = GUI.PasswordField (new Rect (Screen.width / 2 - 343.74f / 2, Screen.height / 2 + 30, 343.74f, 50), passWord, "*" [0], 25);
	}
	
	// Update is called once per frame
	void Update () {

	}


	public void loadGame()
	{

		string givenName = InputUsuario.text;


		for (int i = 0; i < Users.Count; i++) {
			if (givenName == Users [i][0] && passWord ==  Users [i][2]) {
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
