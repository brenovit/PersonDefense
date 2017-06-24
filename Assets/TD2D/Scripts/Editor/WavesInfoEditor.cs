using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Waves info inspector extention.
/// </summary>
[CustomEditor(typeof(WavesInfo))]
public class WavesInfoEditor : Editor
{
	/// <summary>
	/// Raises the inspector GU event.
	/// </summary>
	public override void OnInspectorGUI()
	{
		// Show default inspector property editor
		DrawDefaultInspector();
		EditorGUILayout.HelpBox("Waves number automaticaly calculated from Spawn Points", MessageType.Info);
	}
}
