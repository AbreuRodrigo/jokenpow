using UnityEngine;
using System.Collections;

public class GamePlayController : MonoBehaviour {

	public Sprite rockSprite;
	public Sprite scissorSprite;
	public Sprite paperSprite;

	private OkButton button;
	private GameObject cardSelection;
	private GameState state;

	private CardBehaviour rock;
	private CardBehaviour scissor;
	private CardBehaviour paper;
	private CardBehaviour computer;

	private GameMessage message;

	private CardBehaviour selected;

	void Awake(){
		state = GameState.CARD_SELECTION;

		Screen.fullScreen = true;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		LoadObjects();
	}

	void Update () {
		if(Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}

		if(state.Equals(GameState.GAMEOVER)){
			DoGameOverInputLogics();
		}
	}

	void ChangeSelectedCard(CardType type){
		if(state.Equals(GameState.CARD_SELECTION)){
			switch (type) {
				case CardType.ROCK:
					scissor.isSelected = false;
					paper.isSelected = false;
					rock.isSelected = true;
					cardSelection.transform.position = rock.transform.position;
					
					selected = rock;
				break;
				case CardType.SCISSOR:
					rock.isSelected = false;
					paper.isSelected = false;
					scissor.isSelected = true;
					cardSelection.transform.position = scissor.transform.position;

					selected = scissor;
				break;
				case CardType.PAPER:
					scissor.isSelected = false;
					rock.isSelected = false;
					paper.isSelected = true;
					cardSelection.transform.position = paper.transform.position;

					selected = paper;
				break;
			}

			button.Enable();

			cardSelection.GetComponent<SpriteRenderer>().enabled = true;
		}
	}

	void PlayGame(){
		state = GameState.MATCHING_PLAYERS;

		//Sumir o cardSelection
		cardSelection.GetComponent<SpriteRenderer>().enabled = false;
		cardSelection.SetActive(false);

		//Sumir os cards nao selecionados
		//Mover o card selecionado para a posiçao a esquerda
		switch (selected.type) {
			case CardType.ROCK:
				scissor.FadeOut();
				paper.FadeOut();
			break;
			case CardType.SCISSOR:
				rock.FadeOut();
				paper.FadeOut();
				scissor.ScissorMoveLeft();
			break;
			case CardType.PAPER:
				scissor.FadeOut();
				rock.FadeOut();
				paper.PaperMoveLeft();
			break;
		}

		//Descer de cima o card do computador
		computer.ComputerPlayCard();

		StartCoroutine("WaitGameValidation");
	}

	IEnumerator WaitGameValidation(){
		yield return new WaitForSeconds(1.5f);

		state = GameState.VALIDATING;

		yield return new WaitForSeconds(1f);

		ValidateVictoryCard();

		yield return new WaitForSeconds(1);

		state = GameState.GAMEOVER;
	}

	private void ValidateVictoryCard(){
		CardType playerCard = selected.type;
		CardType computerCard = computer.type;

		if(playerCard.Equals(computerCard)){
			selected.GetComponent<SpriteRenderer> ().enabled = false;
			computer.GetComponent<SpriteRenderer> ().enabled = false;

			message.RunDrawMessage();
		}else if((playerCard.Equals(CardType.ROCK) && computerCard.Equals(CardType.SCISSOR)) ||
		   (playerCard.Equals(CardType.PAPER) && computerCard.Equals(CardType.ROCK)) ||
		   (playerCard.Equals(CardType.SCISSOR) && computerCard.Equals(CardType.PAPER))){

			computer.GetComponent<SpriteRenderer> ().enabled = false;

			selected.GetComponent<SpriteRenderer> ().enabled = false;

			message.RunWinMessage();
		}else{
			selected.GetComponent<SpriteRenderer> ().enabled = false;

			computer.GetComponent<SpriteRenderer> ().enabled = false;

			message.RunLoseMessage();
		}
	}

	private void LoadObjects(){			
		button = GameObject.FindObjectOfType<OkButton>();
		
		cardSelection = GameObject.Find("CardSelection");
		
		rock = GameObject.Find("CardRock").GetComponent<CardBehaviour>();
		scissor = GameObject.Find("CardScissor").GetComponent<CardBehaviour>();
		paper = GameObject.Find("CardPaper").GetComponent<CardBehaviour>();
		
		computer = GameObject.Find("ComputerCard").GetComponent<CardBehaviour>();

		message = GameObject.FindObjectOfType<GameMessage>();
		
		button.Disable();
	}

	private void DoGameOverInputLogics(){
		if(Application.platform == RuntimePlatform.Android){
			if(Input.touches.Length > 0){
				Touch touch = Input.touches[0];
				
				if(touch.phase == TouchPhase.Began){
					Application.LoadLevel("GamePlay");
				}
			}
		}else{
			if(Input.GetMouseButton(0)){
				Application.LoadLevel("GamePlay");
			}
		}
	}
}