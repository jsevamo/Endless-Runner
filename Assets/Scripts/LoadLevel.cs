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

	//public InputField InputUsuario;
	public Text Sorry;
	public GameObject Player;
	public GameObject GC;
	CharacterScript player;
	GameController gc;
	int index;
	public GUIStyle guiStyle = new GUIStyle();
	//public GameObject afterLoginScreen;

	public GameObject GCLogin;
	AfterLogin gclogin;

	public string inputUsername;
	public string passWord;

	// Use this for initialization
	void Start () {

		//afterLoginScreen.gameObject.SetActive (false);

		player = Player.GetComponent<CharacterScript> ();
		gc = GC.GetComponent<GameController> ();
		gclogin = GCLogin.GetComponent<AfterLogin> ();
		

		User1.Add ("j_sevamo"); //Username
		User1.Add ("2"); //Vidas
		User1.Add ("nomejoda"); //password

		User2.Add ("felipito");
		User2.Add ("11");
		User2.Add ("pollo");

		Users.Add (User1);
		Users.Add (User2);

		canStart = false;

		Sorry.gameObject.SetActive (false);

		//InputUsuario.asteriskChar = "$!£%&*"[0];





		
	}

	void OnGUI()
	{
		guiStyle.fontSize = 20; //change the font size
		inputUsername = GUI.TextField(new Rect(710, 400, 300, 20), inputUsername, 25, guiStyle);
		passWord = GUI.PasswordField (new Rect (710, 470, 300, 20), passWord, "*" [0], 25, guiStyle);
	}
	
	// Update is called once per frame
	void Update () {

	}


	public void loadGame()
	{

		string givenName = inputUsername;


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

			gclogin.username = givenName;
			gclogin.lifes = int.Parse(Users [index] [1]);

			SceneManager.LoadScene ("AfterLogin");

		} else {
			Sorry.gameObject.SetActive (true);
		}




	}
}
