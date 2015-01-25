using UnityEngine;

public class SpriteButtonEvents {

	private static SpriteButtonEvents instance;

	private SpriteButtonEvents(){}

	public static SpriteButtonEvents Instance {
		get{
			if(instance == null){
				instance = new SpriteButtonEvents();
			}

			return instance;
		}
	}	

	public void OnPress(SpriteButton button){

	}

	public void OnRelease(SpriteButton button){
		DoLogicsMenuBtns(button);
	}

	private void DoLogicsMenuBtns(SpriteButton button){
		if("PlayButton".Equals(button.name)){
			GameObject.FindObjectOfType<GUIBehaviour>().SlideOutLeft();
		}
		if("ReturnMainMenu".Equals(button.name)){
			GameObject.FindObjectOfType<GUIBehaviour>().SlideOutRight();
		}
		if ("LeaderBoardBtn".Equals (button.name)){
			Debug.Log("Leader");
			ConnectionUtils.Instance.ShowLeaderBoard();
		}
		if ("3Rounds".Equals (button.name)) {
			GameUtils.Instance.DefineRoundLimit(GameRounds.ROUND_LIMIT_3);
			LoadGamePlayScene();
		}
		if ("5Rounds".Equals (button.name)) {
			GameUtils.Instance.DefineRoundLimit(GameRounds.ROUND_LIMIT_5);
			LoadGamePlayScene();
		}
		if ("7Rounds".Equals (button.name)) {
			GameUtils.Instance.DefineRoundLimit(GameRounds.ROUND_LIMIT_7);
			LoadGamePlayScene();
		}
	}

	private void LoadGamePlayScene(){
		GameObject.Find("GUI").SetActive(false);
		GameObject.FindObjectOfType<CutScene>().FadeOut(GameMenuController.LoadGamePlay);
	}
}