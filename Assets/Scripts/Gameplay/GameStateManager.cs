using UnityEngine;
using System.Collections;

namespace VideogamesMoveis{

	public class GameStateManager : MonoBehaviour {
		
		public GameObject greenBallGO, yellowBallGO;
		public SpawnPoint topSpawn, bottomSpawn;
		public MainBall mainBall;
		public float ballVelocity = 0.5f, maxBallVelocity = 3.0f;
		public AudioSource myAudio;
		
		float startBallVelocity;
		int consecutiveBools = 0;
		ColorBall greenBall, yellowBall;
		GameObject greenBallInstance, yellowBallInstance;
		int score = 0;
		bool previousBool;
		
		
		public static GameStateManager sharedInstance;
		
		void Awake(){
			//forcing the game to run decently
			//TODO decouple this from the gamestate manager
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = 60;
		}
		
		void Start(){
			if(sharedInstance==null){
				sharedInstance = this;
			}
			startBallVelocity = ballVelocity;
		}
		
		void SpawnBalls(){
			//clean trailrenderers
			StartCoroutine(greenBall.TrailRenderDestroy());
			StartCoroutine(yellowBall.TrailRenderDestroy());
			
			//randomize balls & set directions
			System.Random rand = new System.Random();
			bool randomBool = rand.NextDouble() >= 0.5;
			if(previousBool==randomBool){ //prevent randomizing the same value too many times
				consecutiveBools++;
			} else {
				consecutiveBools = 0;
			}
			
			if(randomBool && consecutiveBools < 3){
				greenBall.transform.position = topSpawn.transform.position;
				greenBall.isGoingUp = false;
				yellowBall.transform.position = bottomSpawn.transform.position;
				yellowBall.isGoingUp = true;
			} else {
				greenBall.transform.position = bottomSpawn.transform.position;
				greenBall.isGoingUp = true;
				yellowBall.transform.position = topSpawn.transform.position;
				yellowBall.isGoingUp = false;
			}
			previousBool = randomBool;
		}
		
		public void ScoreUp(){
			PlayScoreSound();
			UI.InGame.sharedInstance.ScoreUp(++score);
			SpawnBalls();
			if(ballVelocity<maxBallVelocity) ballVelocity += 0.3f;
		}
		
		void PlayScoreSound(){
			float pitchFluctuation = 0.5f;
			myAudio.pitch = 1.0f + Random.Range(-pitchFluctuation, pitchFluctuation);
			myAudio.Play();
		}
		
		public void GameFail(){		
			UI.InGame.sharedInstance.EndGame(score);
			Destroy(greenBallInstance);
			Destroy(yellowBallInstance);
			ballVelocity = startBallVelocity;
			score = 0;
		}
		
		public void StartGame(){
			Debug.Log("Starting Game");
			UI.InGame.sharedInstance.StartGame();
			greenBallInstance = (GameObject) Instantiate(greenBallGO, bottomSpawn.transform.position, Quaternion.Euler(0,0,0));
			greenBall = greenBallInstance.GetComponent<ColorBall>();
			yellowBallInstance = (GameObject) Instantiate(yellowBallGO, topSpawn.transform.position, Quaternion.Euler(0,0,0));
			yellowBall = yellowBallInstance.GetComponent<ColorBall>();
		}
		
		void Update(){
			//for my beloved Android users, thanks @ishimarumakoto for pointing this out
			if(Input.GetKeyDown(KeyCode.Escape)){
				Application.Quit();
			}
		}

	}
}