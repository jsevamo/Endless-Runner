using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameController : MonoBehaviour {

	CharacterScript character;
	//public GameObject levelLoader;
	public Text pointsText;
	public Text hightScoreText;
	public Text lifeCountText;
	public Text oneHundred;
	public Text usernameText;
	public Button tryAgainButton;
	public Button QuitButton;
	public Button FinalQuitButton;
	public Text timePlayedText;

	public GameObject tryAgainScreen;
	public GameObject heartImage;
	public GameObject pointsImage;
	public GameObject timeImage;
	public Text pointsWhenLost;
	public GameObject muteOn;

	[HideInInspector] public float TotalPoints;
	[HideInInspector] public float HighScore;
	int numberOfTries;
	public float pointChangeSpeed = 0.3f;
	public bool deleteData = false;
	bool wantToTryAgain;
	[HideInInspector] public bool grabbedCollect = false;
	int amountOfPointsGained = 100;

	[HideInInspector] public float totalGameTime;

	int timePlayed;
	int timeForRun;

	public GameObject School1BG;
	public GameObject School2BG;
	public GameObject School3BG;

	public GameObject AfterLog;
	AfterLogin afterlog;

	AudioSource menuAudio;
	AudioSource gameAudio;


	public bool canJump;

	public GameObject ExitCorner;

    int preventUpdate = 0;
    bool asyncFinish = false;

	public Text Loading;
	public GameObject macLogo;

	// Use this for initialization
	void Start () {
		
		Time.timeScale = 1f;
		menuAudio = GameObject.Find ("Soundtrack").GetComponent<AudioSource> ();
		gameAudio = GameObject.Find ("gameSountrack").GetComponent<AudioSource> ();

		afterlog = AfterLog.GetComponent<AfterLogin> ();

		menuAudio.Stop ();
		canJump = true;

		timeForRun = 999999999;
		timePlayed = 0;
		StartCoroutine(countOneSecond());
		hightScoreText.gameObject.SetActive (false);
		usernameText.gameObject.SetActive (false);
		tryAgainScreen.SetActive (false);
		pointsWhenLost.gameObject.SetActive (false);
		QuitButton.gameObject.SetActive (false);
		FinalQuitButton.gameObject.SetActive (false);
		Loading.gameObject.SetActive (false);

		usernameText.text = LoadLevel.profile.firstname;

		character = GameObject.Find ("Player").GetComponent<CharacterScript> ();
		TotalPoints = 0;

		if (PlayerPrefs.HasKey("HighScore")) {

			HighScore = PlayerPrefs.GetFloat ("HighScore");
			hightScoreText.text = ("High Score: ") + Mathf.Round (HighScore).ToString ();

		} else if(deleteData == true) {
			HighScore = 0;
		}

		oneHundred.gameObject.SetActive(false);

		pointsText.text = 0.ToString();
		numberOfTries = 0;

		wantToTryAgain = false;





		
	}

	IEnumerator countOneSecond()
	{

		for (int i = 0; i < timeForRun; i++) {
			yield return new WaitForSeconds (1);
			timePlayed++;
		}

	}

    IEnumerator setUserData(string idToken, int lifes, int score, int timeplayed)
    {
        string json = "{\"idToken\":\"" + idToken + "\", \"lifes\": " + lifes + ", \"score\": " + score + ", \"timeplayed\": " + timeplayed + "}";
        var uwr = new UnityWebRequest("https://us-central1-mac-center-back-to-school.cloudfunctions.net/setUserScores", "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            
            timePlayed = 0;
            asyncFinish = true;
            
        }
    }


    void Update () {


		if (character.IsDead) {
            preventUpdate++;

            if (preventUpdate <= 1)
            {
                oneHundred.gameObject.SetActive(false);
				macLogo.gameObject.SetActive (false);
                tryAgainScreen.SetActive(true);
                pointsWhenLost.text = Mathf.Round(TotalPoints).ToString();
                pointsWhenLost.gameObject.SetActive(true);
                StartCoroutine(setUserData(LoadLevel.token, LoadLevel.profile.lifes, (int)Mathf.Round(TotalPoints), timePlayed));
            }
            else
            {
                preventUpdate = 2;
            }
            //COLOCAR EL PUTO CODIGO ACÁ

            if (wantToTryAgain && asyncFinish)
            {
				wantToTryAgain = false;
				Loading.gameObject.SetActive (false);
				macLogo.gameObject.SetActive (true);
                character.resetgame();
                numberOfTries++;
                Time.timeScale = 1f;
                TotalPoints = 0;
                preventUpdate = 0;
                asyncFinish = false;
            }


        } else {
			setUI ();
		}

		timePlayedText.text = (timePlayed).ToString() + "s";
			
		resetSaveData ();

		if (Input.mousePosition.x > 0 && Input.mousePosition.x < 144 && Input.mousePosition.y > 648 && Input.mousePosition.y < 745) {
			canJump = false;
		} else {
			canJump = true;
		}

		//Debug.Log (getUsername() + " " + getlifes() + " " + getPoints() + " " + getHighscore());
	}


	void setUI()
	{

		if (numberOfTries == 0 && PlayerPrefs.HasKey("HighScore")==false && HighScore == 0) {

			if (grabbedCollect) {
				TotalPoints += amountOfPointsGained;
				HighScore = TotalPoints;
			}

			TotalPoints = TotalPoints + pointChangeSpeed;
			HighScore = TotalPoints;
			PlayerPrefs.SetFloat ("HighScore", HighScore);
			pointsText.text = Mathf.Round (TotalPoints).ToString ();
			hightScoreText.text = ("High Score: ") + Mathf.Round (HighScore).ToString ();

		} else {

			if (grabbedCollect) {
				TotalPoints += amountOfPointsGained;
			}
			

			TotalPoints = TotalPoints + pointChangeSpeed;
			pointsText.text = Mathf.Round (TotalPoints).ToString ();

			if (TotalPoints >= HighScore) {
				HighScore = TotalPoints;
				PlayerPrefs.SetFloat ("HighScore", HighScore);
				hightScoreText.text = ("High Score: ") + Mathf.Round (HighScore).ToString ();
			}
		}
			


	}


	void resetSaveData()
	{
		if (deleteData) {
			PlayerPrefs.DeleteAll ();
		}
			
		
	}

	public void tryAgain()
	{
		Loading.gameObject.SetActive (true);
		wantToTryAgain = true;

	}

	public void exitGame()
	{
		if (LoadLevel.profile.lifes > 1) {
            LoadLevel.profile.lifes--;
			SceneManager.LoadScene ("AfterLogin");

		} else {
			SceneManager.LoadScene ("MainMenu");
		}



	}

	public void exitGameWithLifes()
	{
		
		SceneManager.LoadScene ("AfterLogin");
	}

	public void exitGameWhenDead()
	{
		SceneManager.LoadScene ("MainMenu");
	}

	public void MuteGame()
	{
		
		gameAudio.Stop ();
		muteOn.gameObject.SetActive (true);

	}

	public void MuteGameOff()
	{
		
		gameAudio.Play();
		muteOn.gameObject.SetActive (false);

	}

	public string getlifes()
	{
		return LoadLevel.profile.lifes.ToString ();
	}

	public string getUsername()
	{
		return LoadLevel.profile.firstname;
	}

	public string getPoints()
	{
		
		return Mathf.Round(TotalPoints).ToString ();
	}

	public string getHighscore()
	{
		return Mathf.Round(HighScore).ToString ();
	}





}
