using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	// make game manager public static so can access this from other scripts
	public static GameManager gm;

	public GameObject player;

	// public variables
	public int score=0;

	public bool canBeatLevel = false;
	public int beatLevelScore=0;

	public float startTime=5.0f;
	
	public Text mainScoreDisplay;
	public Text mainTimerDisplay;
	public Text restartMessageDisplay;

	public GameObject gameOverScoreOutline;

	public AudioSource musicAudioSource;

	public bool gameIsOver = false;

	public GameObject playAgainButtons;
	public string playAgainLevelToLoad;

	
    
	public GameObject gameStartCanvas;
	public GameObject mainCanvas;

	private float currentTime;
	public bool hasGameStarted { 
		get;
		private set;
	}

	// setup the game
	void Start () {
		hasGameStarted = false;

		// disable movement of player when game over
		player.GetComponent<CharacterController> ().enabled = false;

		gameStartCanvas.SetActive (true);
		mainCanvas.SetActive (false);

		musicAudioSource.Stop ();

		// set the current time to the startTime specified
		currentTime = startTime;

		// get a reference to the GameManager component for use by other scripts
		if (gm == null) 
			gm = this.gameObject.GetComponent<GameManager>();

		// init scoreboard to 0
		mainScoreDisplay.text = "0";

		// inactivate the gameOverScoreOutline gameObject, if it is set
		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (false);

		// inactivate the playAgainButtons gameObject, if it is set
		if (playAgainButtons)
			playAgainButtons.SetActive (false);

		if (restartMessageDisplay)
			restartMessageDisplay.text = "";

		
	}

	// this is the main game event loop
	void Update () {
		if (!hasGameStarted)
			return;
		
		if (!gameIsOver) {
			// check if player has not collided with the death zone
			if (player.transform.position.y < -1)
				EndGame ();
			if (canBeatLevel && (score >= beatLevelScore)) {  // check to see if beat game
				BeatLevel ();
			} else if (currentTime < 0) { // check to see if timer has run out
				EndGame ();
			} else { // game playing state, so update the timer
				currentTime -= Time.deltaTime;
				mainTimerDisplay.text = currentTime.ToString ("0.00");				
			}
            if (Input.GetKey(KeyCode.Q))
                EndGame();
        }

		// otherwise, listen for "R" or "P" key presses
		else{
			if (Input.GetKey (KeyCode.R))
				RestartGame ();
			else if (Input.GetKey (KeyCode.Q)) 
				EndGame();
		}
	}

	void EndGame() {
		// game is over
		gameIsOver = true;

		// repurpose the timer to display a message to the player
		mainTimerDisplay.text = "GAME OVER";

		// activate the gameOverScoreOutline gameObject, if it is set 
		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (true);

		if (restartMessageDisplay)
			restartMessageDisplay.text = "\"R\" TO TRY AGAIN \n\"Q\" To Quit";
	
		// activate the playAgainButtons gameObject, if it is set 
		if (playAgainButtons)
			playAgainButtons.SetActive (true);

		// reduce the pitch of the background music, if it is set 
		if (musicAudioSource)
			musicAudioSource.pitch = 0.5f; // slow down the music

		// disable movement of player when game over
		player.GetComponent<CharacterController> ().enabled = false;
	}
	
	void BeatLevel() {
		// game is over
		gameIsOver = true;

		// repurpose the timer to display a message to the player
		mainTimerDisplay.text = "LEVEL COMPLETE";

		// activate the gameOverScoreOutline gameObject, if it is set 
		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (true);

		if (restartMessageDisplay)
			restartMessageDisplay.text = "\"R\" TO TRY AGAIN \n\"Q\" to Exit";

		
		
		// reduce the pitch of the background music, if it is set 
		if (musicAudioSource)
			musicAudioSource.pitch = 0.5f; // slow down the music
	}

	// public function that can be called to update the score or time
	public void targetHit (int scoreAmount, float timeAmount)
	{
		// increase the score by the scoreAmount and update the text UI
		score += scoreAmount;
		mainScoreDisplay.text = score.ToString ();
		
		// increase the time by the timeAmount
		currentTime += timeAmount;
		
		// don't let it go negative
		if (currentTime < 0)
			currentTime = 0.0f;

		// update the text UI
		mainTimerDisplay.text = currentTime.ToString ("0.00");
	}

	// public function that can be called to restart the game
	public void RestartGame ()
	{
		// we are just loading a scene (or reloading this scene)
		// which is an easy way to restart the level
		Application.LoadLevel (playAgainLevelToLoad);
	}

	

	public void StartGame()
	{
		// disable movement of player when game over
		player.GetComponent<CharacterController> ().enabled = true;

		gameStartCanvas.SetActive (false);
		mainCanvas.SetActive (true);

		hasGameStarted = true;

		musicAudioSource.Play ();
	}

	

}
