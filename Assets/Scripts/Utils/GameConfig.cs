using UnityEngine;
using System.Collections;

public class GameConfig {
	private static GameConfig instance;
	private int roundLimit = 3;

	private GameConfig(){}

	public static GameConfig Instance {
		get{
			if(instance == null){
				instance = new GameConfig();
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
}
