using UnityEngine;
using System.Collections;

public class OkButton : MonoBehaviour {

	private GamePlayController game;
	private SpriteRenderer sr;

	void Awake() {
		game = GameObject.FindObjectOfType<GamePlayController>();
	}

	void OnMouseDown(){
		game.SendMessage("PlayGame");
		Disable();
	}

	public void Enable(){
		gameObject.GetComponent<SpriteRenderer>().enabled = true;
		collider2D.enabled = true;
		RecalculatePosition();
	}

	public void Disable(){
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
		collider2D.enabled = false;
		RecalculatePosition();
	}

	private void RecalculatePosition(){
		float sW;
		float sH;
		
		if(Application.platform.Equals(RuntimePlatform.WindowsEditor)){
			Vector2 s = EditorRes();
			sW = Camera.main.ScreenToWorldPoint(new Vector3(s.x, 0, 0)).x;
			sH = Camera.main.ScreenToWorldPoint(new Vector3(0, s.y,0)).y;
		}else {
			sW = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
			sH = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height,0)).y;
		}
		
		float modifier = collider2D.bounds.size.x;
		
		gameObject.transform.position = new Vector3 (sW - modifier * 0.52f, -sH + modifier * 0.52f, 0);
	}

	private static Vector2 EditorRes(){
		System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
		System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView", 
		                                                                 System.Reflection.BindingFlags.NonPublic | 
		                                                                 System.Reflection.BindingFlags.Static);
		System.Object res = GetSizeOfMainGameView.Invoke(null, null);
		
		return (Vector2)res;
	}
}
