using UnityEngine;
using System.Collections;

public class GameUtils {
	private const string PLAYER_SCORE = "PlayerScore";
	private const int LEVEL_LIMIT = 750;

	private static GameUtils instance;
	private int roundLimit = 3;

	private GameUtils(){}

	public static GameUtils Instance {
		get{
			if(instance == null){
				instance = new GameUtils();
			}

			return instance;
		}
	}

	public void DefineRoundLimit(GameRounds limit){
		roundLimit = limit.Equals (GameRounds.ROUND_LIMIT_3) ? 3 : (
					 limit.Equals (GameRounds.ROUND_LIMIT_5) ? 5 : 7);
	}

	public int RoundLimit(){
		return roundLimit;
	}

	public static void SavePlayerScore(int score){
		if(PlayerPrefs.GetInt(PLAYER_SCORE) == null){
			PlayerPrefs.SetInt(PLAYER_SCORE, 0);
		}

		int totalScore = PlayerPrefs.GetInt(PLAYER_SCORE) + score;

		totalScore = totalScore < 0 ? 0 : totalScore;

		PlayerPrefs.SetInt(PLAYER_SCORE, totalScore);

		ConnectionUtils.Instance.ShareScoreToLeaderBoard(totalScore);
	}

	public int LoadPlayerScore(){
		ValidatePlayerStats();
		
		return PlayerPrefs.GetInt(PLAYER_SCORE);
	}
	
	public int LoadPlayerLevel(){
		ValidatePlayerStats();
		
		int score = PlayerPrefs.GetInt(PLAYER_SCORE);
		
		return (score / LEVEL_LIMIT) + 1;
	}
	
	public void ValidatePlayerStats(){
		if(PlayerPrefs.GetInt(PLAYER_SCORE) == null){
			PlayerPrefs.SetInt(PLAYER_SCORE, 0);
		}
	}
}