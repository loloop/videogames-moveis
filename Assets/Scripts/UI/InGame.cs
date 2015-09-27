using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

namespace VideogamesMoveis.UI{
	public class InGame : MonoBehaviour {
	
		public Text scoreText1, scoreText2, highScoreText;
		public CanvasGroup mainMenuCG, inGameCG, highScoreCG;
		
		bool isScoreText1Visible = true;
		float animationTime = 0.3f;
		float xOffset = 50;
		
		public static InGame sharedInstance; 
		
		void Start(){
			if(sharedInstance==null){
				sharedInstance = this;
			}
			ResetScores();
		}
		
		public void ScoreUp(int newScore){
			if(isScoreText1Visible){
				AnimateDisappearLeft(scoreText1);
				scoreText2.text = newScore.ToString();
				AnimateAppearRight(scoreText2);
				isScoreText1Visible = false;
			} else {
				AnimateDisappearLeft(scoreText2);
				scoreText1.text = newScore.ToString();
				AnimateAppearRight(scoreText1);
				isScoreText1Visible = true;
			}
		}
		
		public void StartGame(){
			mainMenuCG.DOFade(0, animationTime);
			highScoreCG.DOFade(0, animationTime);
			mainMenuCG.interactable = false;
			inGameCG.DOFade(1, animationTime);
		}
		
		public void EndGame(int score){
			mainMenuCG.interactable = true;
			inGameCG.DOFade(0, animationTime);
			if(HighScore.SetScore(score)){
				highScoreText.text = "<color=green>New High Score!\n<size=100>" + score + "</size></color>";
			} else {
				highScoreText.text = "<color=red>You Lost! :(\n<size=50>Your Score: " + score + "</size></color>\n<size=40>High Score: " + HighScore.GetHighScore() + "</size>";
			}
			highScoreCG.DOFade(1, animationTime);
			ResetScores();
		}
		
		void AnimateDisappearLeft(Text text){
			RectTransform rect = text.rectTransform; 
			Vector2 realPosition = rect.anchoredPosition;
			rect.anchoredPosition = new Vector2(realPosition.x - xOffset, realPosition.y);
			rect.DOAnchorPos(realPosition, animationTime + 0.1f, true);
			text.DOFade(0, animationTime);
			DOTween.Play(text);
		}
		
		void AnimateAppearRight(Text text){
			RectTransform rect = text.rectTransform; 
			Vector2 realPosition = rect.anchoredPosition;
			rect.anchoredPosition = new Vector2(rect.anchoredPosition.x + xOffset, rect.anchoredPosition.y);
			rect.DOAnchorPos(realPosition, animationTime + 0.1f, true);
			text.DOFade(1, animationTime);
			DOTween.Play(text);
		}
		
		public void ResetScores(){
			scoreText1.text = "0";
			scoreText2.text =  "0";
			Color newColor = new Color(scoreText2.color.r, scoreText2.color.g, scoreText2.color.b, 0);
			scoreText1.color = newColor;
			scoreText2.color = newColor;
		}
	}
}