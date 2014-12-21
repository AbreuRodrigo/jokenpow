using UnityEngine;
using System.Collections;

public class TextsController : MonoBehaviour {

	private int round;
	private int myScore;
	private int pcScore;
	private int playerVictories;
	private int computerVictories;

	public delegate void TriggerEvents();
	public static event TriggerEvents events;

	private TextMesh myPointsText;
	private TextMesh myPointsShadow;
	private TextMesh pcPointsText;
	private TextMesh pcPointsShadow;
	private TextMesh roundCText;
	private TextMesh roundCShadow;

	void Awake(){
		events = null;

		myPointsText = GameObject.Find("MyPointsText").GetComponent<TextMesh>();
		myPointsShadow = GameObject.Find("MyPointsShadow").GetComponent<TextMesh>();
		pcPointsText = GameObject.Find("PCPointsText").GetComponent<TextMesh>();
		pcPointsShadow = GameObject.Find("PCPointsShadow").GetComponent<TextMesh>();
		roundCText = GameObject.Find("RoundCText").GetComponent<TextMesh>();
		roundCShadow = GameObject.Find("RoundCShadow").GetComponent<TextMesh>();
	}

	void Start(){
		round = 1;
		myScore = 0;
		pcScore = 0;
		playerVictories = 0;
		computerVictories = 0;
		
		myPointsText.text = myScore.ToString();
		myPointsShadow.text = myScore.ToString();
		
		pcPointsText.text = pcScore.ToString();
		pcPointsShadow.text = pcScore.ToString();
		
		roundCText.text = roundCShadow.text = "1-3";
	}

	public void Fall(TriggerEvents e = null){
		if(e != null){
			events += e;
		}

		gameObject.GetComponent<Animator>().SetBool("Falling", true);
	}

	public void FinishEvents(){
		if(events != null){
			events.Invoke();
		}
	}

	public void PlayerVictory(){
		playerVictories++;
	}

	public void ComputerVictory(){
		computerVictories++;
	}

	public void CalculatePoints(){
		StartCoroutine("CalculatePointsRoutine");
	}
	
	public void AdvanceRoundCounter(){
		round++;
		
		roundCText.text = roundCShadow.text = round + "-3";
	}

	IEnumerator CalculatePointsRoutine(){
		int nextPlayerScore = myScore;
		int nextComputerScore = pcScore;

		switch(round){
			case 1:
				if(playerVictories == 1){
					nextPlayerScore += 10;
				}else if(computerVictories == 1){
					nextComputerScore += 10;
				}
			break;
			case 2:
				if(playerVictories == 2){
					nextPlayerScore += 20;
				}else if(computerVictories == 2){
					nextComputerScore += 20;
				}else{
					if(myScore == 0){
						nextPlayerScore += 10;
					}else if(pcScore == 0){
						nextComputerScore += 10;
					}
				}
			break;
			case 3:
				if(playerVictories == 3){
					nextPlayerScore += 15;
				}else if(playerVictories == 2){
					nextPlayerScore += 10;
				}else if(computerVictories == 3){
					nextComputerScore += 15;
				}else if(computerVictories == 2){
					nextComputerScore += 10;
				}
			break;
		}
		
		bool keepCalculating = true;
		
		while(keepCalculating){
			yield return new WaitForSeconds(0.01f);
			
			if(playerVictories == 0 && computerVictories == 0){
				keepCalculating = false;
			}else{
				if(myScore < nextPlayerScore){
					myScore++;
					
					if(myScore == nextPlayerScore){
						keepCalculating = false;
					}
				}else if(pcScore < nextComputerScore){
					pcScore++;
					
					if(pcScore == nextComputerScore){
						keepCalculating = false;
					}
				}
			}
			
			myPointsText.text = myPointsShadow.text = myScore.ToString();
			pcPointsText.text = pcPointsShadow.text = pcScore.ToString();
		}	
	}
}
