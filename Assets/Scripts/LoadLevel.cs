using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LoadLevel : MonoBehaviour {

    public List<List<string>> Users = new List<List<string>> ();

	List<string> User1 = new List<string> ();
	List<string> User2 = new List<string> ();

	public static string token;

	//public InputField InputUsuario;
	public Text Sorry;
	public Text Loading;
	public GameObject Player;
	public GameObject GC;
	CharacterScript player;
	GameController gc;

	public GUIStyle guiStyle = new GUIStyle();
	//public GameObject afterLoginScreen;

	public GameObject GCLogin;
	AfterLogin gclogin;

	public string inputUsername;
	public string passWord;
	AudioSource menuAudio;
	//AudioSource LostMusic;

    public JSWToken tokenObject = new JSWToken();

    public static UserProfile profile = new UserProfile();

	public GameObject Userhere;
	public GameObject Passhere;

	public Text nolifes;

    float _oldWidth;
    float _oldHeight;
    int _fontSize = 16;
    float Ratio = 20; // public

    // Use this for initialization
    void Start () {

        

		//afterLoginScreen.gameObject.SetActive (false);

		player = Player.GetComponent<CharacterScript> ();
		gc = GC.GetComponent<GameController> ();
		gclogin = GCLogin.GetComponent<AfterLogin> ();
		menuAudio = GameObject.Find ("Soundtrack").GetComponent<AudioSource> ();


		User1.Add ("j_sevamo"); //Username
		User1.Add ("2"); //Vidas
		User1.Add ("nomejoda"); //password

		User2.Add ("felipito");
		User2.Add ("11");
		User2.Add ("pollo");

		Users.Add (User1);
		Users.Add (User2);

		Sorry.gameObject.SetActive (false);
		Loading.gameObject.SetActive (false);
		nolifes.gameObject.SetActive (false);

		//InputUsuario.asteriskChar = "$!£%&*"[0];

		if (menuAudio.isPlaying == false) {
			menuAudio.Play ();
		}

        //inputUsername = "juan9505@gmail.com";
        //passWord = "123456";

    }

	void OnGUI()
	{
		guiStyle.fontSize = _fontSize; //change the font size

		//inputUsername = GUI.TextField(new Rect(710, 400, 300, 20), inputUsername, 50);
		inputUsername = GUI.TextField(new Rect(Userhere.transform.position.x, Userhere.transform.position.y - 6, 300, 20), inputUsername, 50, guiStyle);
		passWord = GUI.PasswordField (new Rect (Passhere.transform.position.x, Passhere.transform.position.y - 4, 300, 20), passWord, "*" [0], 50, guiStyle);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown("space")){
			nolifes.gameObject.SetActive (true);
		}
        if (_oldWidth != Screen.width || _oldHeight != Screen.height)
        {
            _oldWidth = Screen.width;
            _oldHeight = Screen.height;
            _fontSize = Mathf.RoundToInt(Mathf.Min(Screen.width, Screen.height) / Ratio) - 11;
        }

        //Debug.Log(_fontSize);

	}


    IEnumerator postRequest(string url, string json)
    {
        Loading.gameObject.SetActive(true);
        var uwr = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
            token = "error";
            Sorry.gameObject.SetActive(true);
        }
        else
        {
            if(uwr.responseCode == 200)
            {
                tokenObject = JsonUtility.FromJson<JSWToken>(uwr.downloadHandler.text);
                if (tokenObject.idToken == null)
                {
                    Sorry.gameObject.SetActive(true);
                    nolifes.gameObject.SetActive(false);
                    Loading.gameObject.SetActive(false);
                }
                else
                {
                    StartCoroutine(userData(tokenObject.idToken));
                }
            }
            else
            {
                Sorry.gameObject.SetActive(true);
                Loading.gameObject.SetActive(false);
                nolifes.gameObject.SetActive(false);
            }
            
        }
    }

    IEnumerator userData(string idToken)
    {
        token = idToken;
        string json = "{\"idToken\":\"" + idToken + "\"}";
        var uwr = new UnityWebRequest("https://us-central1-mac-center-back-to-school.cloudfunctions.net/userData", "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();
        Loading.gameObject.SetActive(false);
        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
            Sorry.gameObject.SetActive(true);
            nolifes.gameObject.SetActive(false);
        }
        else
        {
            if(uwr.responseCode == 200)
            {
                profile = JsonUtility.FromJson<UserProfile>(uwr.downloadHandler.text);
                Sorry.gameObject.SetActive(false);
                nolifes.gameObject.SetActive(false);
                SceneManager.LoadScene("AfterLogin");
            }
            else if (uwr.responseCode == 403)
            {
                Sorry.gameObject.SetActive(false);
                nolifes.gameObject.SetActive(true);
            }
            else
            {
                nolifes.gameObject.SetActive(false);
                Sorry.gameObject.SetActive(true);
            }
            
        }
    }


    public void loadGame()
	{
        string givenName = inputUsername;
        StartCoroutine(postRequest("https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key=AIzaSyCFxvXvop35U7qwzDt6IfmSsfd2KvDLHTc",
        "{\"email\": \"" + inputUsername+ "\", \"password\": \"" + passWord+ "\", \"returnSecureToken\": true}"));

        /*for (int i = 0; i < Users.Count; i++) {
			if (givenName == Users [i][0] && passWord ==  Users [i][2]) {
				canStart = true;
				index = i;
				break;
			}
		}*/

		/*if (canStart) {
			gc.userName = givenName;
			player.lifes = int.Parse(Users [index] [1]);

			gclogin.username = givenName;
			gclogin.lifes = int.Parse(Users [index] [1]);

			gclogin.token = token;

			SceneManager.LoadScene ("AfterLogin");

		} else {
			Sorry.gameObject.SetActive (true);
		}*/




	}



}
