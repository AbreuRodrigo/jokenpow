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

		DoLogicsInGameBtns(button);
	}

	private void DoLogicsMenuBtns(SpriteButton button){
		if("PlayButton".Equals(button.name)){
			GameObject.FindObjectOfType<GUIBehaviour>().SlideOutLeft();
		}
		if("ReturnMainMenu".Equals(button.name)){
			GameObject.FindObjectOfType<GUIBehaviour>().SlideOutRight();
		}
	}

	private void DoLogicsInGameBtns(SpriteButton button){
		if("OkButton".Equals(button.name)){
			GameObject.FindObjectOfType<GamePlayController>().SendMessage("PlayGame");
			button.Disable();
		}
		if("MenuButton".Equals(button.name)){
			Application.LoadLevel("Menu");
		}
		if("ReturnButton".Equals(button.name)){
			GameObject.FindObjectOfType<GamePlayController>().SendMessage("StartNextGameRound");
		}
	}

	private void LoadGamePlayScene(){
		GameObject.Find("GUI").SetActive(false);
		GameObject.FindObjectOfType<CutScene>().FadeOut(GameMenuController.LoadGamePlay);
	}
}