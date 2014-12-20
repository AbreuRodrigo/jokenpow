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
		if("PlayButton".Equals(button.name)){
			GameObject.Find("GUI").SetActive(false);

			GameObject.FindObjectOfType<CutScene>().FadeOut(GameMenuController.LoadGamePlay);
		}
	}
}
