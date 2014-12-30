using UnityEngine;
using System.Collections;

public class GamePlayController : MonoBehaviour {

	public Sprite plainSprite;
	public Sprite rockSprite;
	public Sprite scissorSprite;
	public Sprite paperSprite;

	private SpriteButton button;
	private GameObject cardSelection;
	private CutScene cutScene;

	private TextsController texts;
	private BtnsController btns;

	private GameState state;

	private CardBehaviour rock;
	private CardBehaviour scissor;
	private CardBehaviour paper;
	private CardBehaviour computer;

	private GameMessage message;

	private CardType selected;

	private const string GAMEPLAY_SINGLE_SCENE = "GamePlaySingle";
	private const string MENU_SCENE = "Menu";

	void Awake(){
		cutScene = GameObject.FindObjectOfType<CutScene>();
		texts = GameObject.FindObjectOfType<TextsController>();
		btns = GameObject.FindObjectOfType<BtnsController>();

		state = GameState.LOADING_GAMEPLAY;
	}

	void Start(){
		cutScene.FadeIn(AfterCutSceneEvents);
		
		button = GameObject.Find("OkButton").GetComponent<SpriteButton>();	

		cardSelection = GameObject.Find("CardSelection");
		
		rock = GameObject.Find("CardRock").GetComponent<CardBehaviour>();
		scissor = GameObject.Find("CardScissor").GetComponent<CardBehaviour>();
		paper = GameObject.Find("CardPaper").GetComponent<CardBehaviour>();
		
		computer = GameObject.Find("ComputerCard").GetComponent<CardBehaviour>();
		
		message = GameObject.FindObjectOfType<GameMessage>();
		
		button.Disable();
	}

	void Update(){
		if(Input.GetKey(KeyCode.Escape)){
			Application.LoadLevel(MENU_SCENE);
		}

		if(state == GameState.GAMEOVER){
			DoGameOverInputLogics();
		}
	}

	void ChangeSelectedCard(CardType type){
		if(state == GameState.CARD_SELECTION){
			switch (type) {
				case CardType.ROCK:
					scissor.isSelected = false;
					paper.isSelected = false;
					rock.isSelected = true;
					cardSelection.transform.position = rock.transform.position;
					
					selected = CardType.ROCK;
				break;
				case CardType.SCISSOR:
					rock.isSelected = false;
					paper.isSelected = false;
					scissor.isSelected = true;
					cardSelection.transform.position = scissor.transform.position;

					selected = CardType.SCISSOR;
				break;
				case CardType.PAPER:
					scissor.isSelected = false;
					rock.isSelected = false;
					paper.isSelected = true;
					cardSelection.transform.position = paper.transform.position;

					selected = CardType.PAPER;
				break;
			}

			button.Enable();

			cardSelection.GetComponent<SpriteRenderer>().enabled = true;
		}
	}

	void PlayGame(){
		state = GameState.MATCHING_PLAYERS;

		cardSelection.GetComponent<SpriteRenderer>().enabled = false;

		switch (selected) {
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

		computer.ComputerPlayCard();

		StartCoroutine("WaitGameValidation");
	}

	IEnumerator WaitGameValidation(){
		yield return new WaitForSeconds(1.5f);

		state = GameState.VALIDATING;

		yield return new WaitForSeconds(1f);

		ValidateVictoryCard();

		yield return new WaitForSeconds(1f);

		texts.CalculatePoints();

		btns.ShowButtons();
	}

	void ValidateVictoryCard(){
		CardType playerCard = selected;
		CardType computerCard = computer.type;

		computer.FadeOut();

		switch(selected) {
		case CardType.PAPER:
			paper.GetComponent<SpriteRenderer>().enabled = false;
			break;
		case CardType.ROCK:
			rock.GetComponent<SpriteRenderer>().enabled = false;
			break;
		case CardType.SCISSOR:
			scissor.GetComponent<SpriteRenderer>().enabled = false;
			break;
		}

		if(playerCard.Equals(computerCard)){
			texts.DefineResult(GameResult.DRAW);

			message.RunDrawMessage();
		}else if((playerCard.Equals(CardType.ROCK) && computerCard.Equals(CardType.SCISSOR)) ||
		   (playerCard.Equals(CardType.PAPER) && computerCard.Equals(CardType.ROCK)) ||
		   (playerCard.Equals(CardType.SCISSOR) && computerCard.Equals(CardType.PAPER))){

			texts.DefineResult(GameResult.PLAYER1_VICTORY);
			texts.PlayerVictory();

			message.RunWinMessage();
		}else{
			texts.DefineResult(GameResult.PLAYER2_VICTORY);
			texts.ComputerVictory();

			message.RunLoseMessage();
		}
	}

	void DoGameOverInputLogics(){
	}

	void ChangeToCardSelectionState(){
		state = GameState.CARD_SELECTION;
	}

	void AfterCutSceneEvents(){
		texts.Fall(ChangeToCardSelectionState);
	}

	void StartNextGameRound(){
		ChangeToCardSelectionState();

		message.HideMessage();

		btns.HideButtons();

		texts.AdvanceRoundCounter();

		StartCoroutine("WaitAndShow");
	}

	IEnumerator WaitAndShow(){
		computer.ResetAnimations(CardType.PLAIN, plainSprite);

		yield return new WaitForSeconds(1);

		rock.ResetAnimations(selected, rockSprite);
		scissor.ResetAnimations(selected, scissorSprite);
		paper.ResetAnimations(selected, paperSprite);
	}
}