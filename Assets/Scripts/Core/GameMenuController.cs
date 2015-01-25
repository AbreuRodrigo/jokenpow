using UnityEngine;
using System.Collections;

public class GameMenuController : MonoBehaviour {

	private GameObject leaderBoardBtn;

	void Awake(){
		ConnectionUtils.Instance.ShowBanner();

		leaderBoardBtn = GameObject.Find("LeaderBoardBtn");

		if(!Application.platform.Equals(RuntimePlatform.Android) ||
		   !ConnectionUtils.Instance.IsAuth){
			leaderBoardBtn.SetActive(false);
		}
	}

	void Update () {
		if(Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}
	}

	public static void LoadGamePlay(){
		Application.LoadLevel("GamePlaySingle");
	}
}
