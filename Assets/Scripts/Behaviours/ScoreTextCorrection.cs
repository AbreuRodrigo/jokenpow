using UnityEngine;
using System.Collections;

public class ScoreTextCorrection : MonoBehaviour {
	public enum TextPosition{
		LEFT,
		RIGHT
	}

	public TextPosition textPosition;

	void Start(){
		DoTextPositionCorrections();
	}

	void DoTextPositionCorrections(){
		Vector3 myPos = gameObject.transform.position;

		switch(textPosition) {
			case TextPosition.LEFT:
				Vector3 leftPos = Camera.main.ScreenToWorldPoint(new Vector3(5, 0, 10));
				myPos.x = leftPos.x; 
				gameObject.transform.position = myPos;
			break;
			case TextPosition.RIGHT:				
				Vector3 rightPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width - 7.86f, 0, 10));
				myPos.x = rightPos.x;
				gameObject.transform.position = myPos;
			break;
		}
	}
}
