#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor {
	public override void OnInspectorGUI() {
		base.OnInspectorGUI();

		GUI.enabled = Application.isPlaying;
		if(GUILayout.Button("Raise"))
		{
			var ge = target as GameEvent;
			ge.Raise();
		}
	}
}

#endif