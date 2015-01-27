using UnityEngine;
using System.Collections;

public class GameMenuController : MonoBehaviour {

	private GameObject leaderBoardBtn;

	private Animator guiMainMenu;
	private Animator guiCredits;

	private bool isShowingCredits = false;

	private bool waitingAuthentication = false;

	void Awake(){
		ConnectionUtils.Instance.ShowBanner();

		leaderBoardBtn = GameObject.Find("LeaderBoardBtn");

		guiMainMenu = GameObject.Find("GUIMainMenu").GetComponent<Animator>();
		guiCredits = GameObject.Find("GUICredits").GetComponent<Animator>();

		if(!IsAndroid() || Social.Active.localUser == null){
			leaderBoardBtn.SetActive(false);
		}
	}

	void Update () {
		if(Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}

		if(isShowingCredits){
			if(IsAndroid()){
				if(Input.touches.Length > 0){
					if(Input.touches[0].phase.Equals(TouchPhase.Began) ||
					   Input.touches[0].phase.Equals(TouchPhase.Moved) ||
					   Input.touches[0].phase.Equals(TouchPhase.Stationary) ||
					   Input.touches[0].phase.Equals(TouchPhase.Canceled) ||
					   Input.touches[0].phase.Equals(TouchPhase.Ended)){
						HideCredits();
					}
				}
			}else {
				if(Input.GetMouseButtonDown(0) || 
				   Input.GetMouseButtonDown(1) || 
				   Input.GetMouseButtonDown(2) ||
				   Input.anyKeyDown){
					HideCredits();
				}
			}
		}
	}

	public static void LoadGamePlay(){
		Application.LoadLevel("GamePlaySingle");
	}

	public void ShowCredits(){
		StartCoroutine("ShowCreditsRoutine");
	}

	public void HideCredits(){
		StartCoroutine("HideCreditsRoutine");
	}

	IEnumerator ShowCreditsRoutine(){
		guiMainMenu.Play("Hide");

		yield return new WaitForSeconds (0.5f);

		guiCredits.Play("Show");
		
		isShowingCredits = true;
	}

	IEnumerator HideCreditsRoutine(){
		guiCredits.Play("Hide");

		yield return new WaitForSeconds (0.5f);

		guiMainMenu.Play("Show");
		
		isShowingCredits = false;
	}

	public bool IsAndroid(){
		return Application.platform.Equals(RuntimePlatform.Android);
	}
}
