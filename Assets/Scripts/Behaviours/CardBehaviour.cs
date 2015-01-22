using UnityEngine;
using System.Collections;

public class CardBehaviour : MonoBehaviour {

	public CardType type;

	public bool isSelected = false;
	private GamePlayController game;
	private TextsController texts;

	void Awake(){
		game = GameObject.FindObjectOfType<GamePlayController>();
		texts = GameObject.FindObjectOfType<TextsController>();
	}

	void OnMouseDown(){
		if(game.IsThisState(GameState.CARD_SELECTION) && 
		   gameObject.transform.position.y == 0 && 
		   texts.gameObject.transform.position.y == 0){
			game.SendMessage("ChangeSelectedCard", this.type);
		}
	}

	void OnMouseUp(){
		if(game.IsThisState(GameState.CARD_SELECTION) && 
		   gameObject.transform.position.y == 0 && 
		   texts.gameObject.transform.position.y == 0){
			game.SendMessage("PlayGame");
		}
	}

	public void FadeOut(){
		gameObject.GetComponent<Animator>().SetBool("FadeOut", true);
	}

	public void ScissorMoveLeft(){
		gameObject.GetComponent<Animator>().SetBool("ScissorMoveLeft", true);
	}

	public void PaperMoveLeft(){
		gameObject.GetComponent<Animator>().SetBool("PaperMoveLeft", true);
	}

	public void ComputerPlayCard(){
		gameObject.GetComponent<Animator>().SetBool("ComputerPlayCard", true);
	}

	public void ResetAnimations(CardType selected, Sprite sprite = null){
		isSelected = false;

		if(selected != CardType.PLAIN && this.type == selected){
			gameObject.GetComponent<SpriteRenderer>().enabled = true;
		}

		gameObject.GetComponent<SpriteRenderer>().sprite = sprite;

		gameObject.GetComponent<Animator>().SetBool("ComputerPlayCard", false);
		gameObject.GetComponent<Animator>().SetBool("ScissorMoveLeft", false);
		gameObject.GetComponent<Animator>().SetBool("PaperMoveLeft", false);
		gameObject.GetComponent<Animator>().SetBool("FadeOut", false);

		gameObject.gameObject.GetComponent<Animator>().Play("Idle");
	}

	public void ChangeComputerSpriteByType(){
		//Random.seed = Random.Range(0, Time.frameCount);
		int pcCardNumber = Random.Range(0, 3);

		switch (pcCardNumber) {
			case 0://Rock
				this.type = CardType.ROCK;
				gameObject.GetComponent<SpriteRenderer>().sprite = game.rockSprite;
			break;
			case 1://Scissor
				this.type = CardType.SCISSOR;
				gameObject.GetComponent<SpriteRenderer>().sprite = game.scissorSprite;
			break;
			case 2://Paper
				this.type = CardType.PAPER;
				gameObject.GetComponent<SpriteRenderer>().sprite = game.paperSprite;
			break;
		}
	}
}
