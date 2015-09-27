using UnityEngine;
using System.Collections;

namespace VideogamesMoveis{
	
	public enum PlayerData{
		HighScore
	}
	
	public class HighScore{
		
		public static bool SetScore(int score){
			if(score > GetHighScore()){
				PlayerPrefs.SetInt(PlayerData.HighScore.ToString(), score);
				return true; //if a new score was set, returns true
			}
			return false; //if no new score was set, returns false
		}
		
		public static int GetHighScore(){
			return PlayerPrefs.GetInt(PlayerData.HighScore.ToString(), 0);
		}
	}
	
}
