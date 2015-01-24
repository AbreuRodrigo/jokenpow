using UnityEngine;
using System.Collections;

public class PlayerStatsMenu : MonoBehaviour {
	public TextMesh score;
	public TextMesh scoreShadow;
	public TextMesh level;
	public TextMesh levelShadow;

	void Awake(){
		RedefinePosition();

		LoadPlayerStats();
	}

	private void RedefinePosition(){
		Vector3 oldPos = gameObject.transform.position;
		
		float newX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
		
		oldPos.x = newX;
		
		gameObject.transform.position = oldPos;
	}

	private void LoadPlayerStats(){
		scoreShadow.text = score.text = "" + GameUtils.Instance.LoadPlayerScore();

		levelShadow.text = level.text = "" + GameUtils.Instance.LoadPlayerLevel();
	}
}