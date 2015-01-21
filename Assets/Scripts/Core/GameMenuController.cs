using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GameMenuController : MonoBehaviour {

	bool isAuth = false;

	void Update () {
		if(Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}
	}

	public static void LoadGamePlay(){
		Application.LoadLevel("GamePlaySingle");
	}

	/*void OnGUI(){
		if(GUI.Button(new Rect(10, 10, 100, 50), "Connect")){
			PlayGamesPlatform.Activate();
			PlayGamesPlatform.Instance.Authenticate((bool success) =>{
				isAuth = success;
			});
		}

		GUI.color = Color.black;
		GUI.Label(new Rect(10, 100, 100, 50), PlayGamesPlatform.Instance.IsAuthenticated() + " " + isAuth);

		if(GUI.Button(new Rect(10, 190, 100, 50), "Leaderboard")){
			PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIh7jKwaEKEAIQBg");
		}
	}*/
}
