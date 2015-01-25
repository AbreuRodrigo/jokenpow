using UnityEngine;
using System.Collections;

public class GamePlayController : MonoBehaviour {

	public Sprite plainSprite;
	public Sprite rockSprite;
	public Sprite scissorSprite;
	public Sprite paperSprite;

	public TextMesh finalScoreText;
	public TextMesh finalScoreShadow;

	private GameObject cardSelection;
	private CutScene cutScene;

	private TextsController texts;

	private GameState state;

	private CardBehaviour rock;
	private CardBehaviour scissor;
	private CardBehaviour paper;
	private CardBehaviour computer;

	private Animator cardsContainer;
	private RoundCounterBehaviour roundCounter;


	private GameMessage message;

	private CardType selected;

	private const string GAMEPLAY_SINGLE_SCENE = "GamePlaySingle";
	private const string MENU_SCENE = "Menu";

	void Awake(){
		cutScene = GameObject.FindObjectOfType<CutScene>();
		texts = GameObject.FindObjectOfType<TextsController>();

		state = GameState.LOADING_GAMEPLAY;

		ConnectionUtils.Instance.HideBanner();
	}

	void Start(){
		cutScene.FadeIn(AfterCutSceneEvents);

		cardSelection = GameObject.Find("CardSelection");
		
		rock = GameObject.Find("CardRock").GetComponent<CardBehaviour>();
		scissor = GameObject.Find("CardScissor").GetComponent<CardBehaviour>();
		paper = GameObject.Find("CardPaper").GetComponent<CardBehaviour>();
		
		computer = GameObject.Find("ComputerCard").GetComponent<CardBehaviour>();
		
		message = GameObject.FindObjectOfType<GameMessage>();

		cardsContainer = GameObject.Find("CardsContainer").GetComponent<Animator>();

		roundCounter = GameObject.Find("RoundCounter").GetComponent<RoundCounterBehaviour>();
		roundCounter.AddShowEvents(ShowCards);
	}

	void Update(){
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

		if(!IsFinalRound()){
			StartNextGameRound();
		}else {
			finalScoreText.text = finalScoreShadow.text = texts.NextPlayerScore + " - " + texts.NextComputerScore;

			if(texts.NextPlayerScore > texts.NextComputerScore){

				GameUtils.SavePlayerScore(texts.NextPlayerScore - texts.NextComputerScore);

				message.RunWinMessage();
			}else if(texts.NextPlayerScore < texts.NextComputerScore){

				GameUtils.SavePlayerScore(-(texts.NextComputerScore - texts.NextPlayerScore));

				message.RunLoseMessage();
			}else{
				message.RunDrawMessage();
			}

			int chance = Random.Range(0, 100);

			if(chance <= 25){
				ConnectionUtils.Instance.ShowFullScreen();
			}

			yield return new WaitForSeconds(1.5f);

			cutScene.FadeOut(LoadMenuScene);
		}
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
		}else if((playerCard.Equals(CardType.ROCK) && computerCard.Equals(CardType.SCISSOR)) ||
		   (playerCard.Equals(CardType.PAPER) && computerCard.Equals(CardType.ROCK)) ||
		   (playerCard.Equals(CardType.SCISSOR) && computerCard.Equals(CardType.PAPER))){

			texts.DefineResult(GameResult.PLAYER1_VICTORY);
			texts.PlayerVictory();
		}else{
			texts.DefineResult(GameResult.PLAYER2_VICTORY);
			texts.ComputerVictory();
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

		texts.AdvanceRoundCounter();

		roundCounter.PlayShowAnimation();

		cardsContainer.Play("Hidden");

		StartCoroutine("WaitAndShow");
	}

	public bool IsThisState(GameState testingState){
		return state.Equals(testingState);
	}

	public bool IsFinalRound(){
		return texts.CurrentRound == GameUtils.Instance.RoundLimit();
	}

	private void LoadMenuScene(){
		Application.LoadLevel("Menu");
	}

	IEnumerator WaitAndShow(){
		computer.ResetAnimations(CardType.PLAIN, plainSprite);

		yield return new WaitForSeconds(1);

		rock.ResetAnimations(selected, rockSprite);
		scissor.ResetAnimations(selected, scissorSprite);
		paper.ResetAnimations(selected, paperSprite);
	}

	private void ShowCards(){
		cardsContainer.Play("ShowUp");
	}
}