#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpriteButton))]
public class SpriteButtonInspector : Editor {

	public override void OnInspectorGUI(){
		if(!EditorApplication.isPlaying){
			SpriteButtonUtils.UpdateSpriteButtonsOnEditor(false);

			SpriteButton button = (SpriteButton)target;

			button.anchor = (SpriteButtonAnchor) EditorGUILayout.EnumPopup("Anchor", button.anchor);

			if(!button.anchor.Equals(SpriteButtonAnchor.NONE)){
				button.marginTop = EditorGUILayout.IntField("Margin Top", button.marginTop);
				button.marginBottom = EditorGUILayout.IntField("Margin Bottom", button.marginBottom);
				button.marginLeft = EditorGUILayout.IntField("Margin Left", button.marginLeft);
				button.marginRight = EditorGUILayout.IntField("Margin Right", button.marginRight);
			}

			if(GUI.changed){
				EditorUtility.SetDirty(target);
			}
		}
	}
}
#endif