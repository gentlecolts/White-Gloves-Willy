using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerEditor : Editor {
	public override void OnInspectorGUI() {
		serializedObject.Update();
		var controller = target as EnemySpawner;
		EditorGUIUtility.LookLikeInspector();

		/*
		SerializedProperty tps = serializedObject.FindProperty ("enemyTypes");
		EditorGUI.BeginChangeCheck();
		EditorGUILayout.PropertyField(tps, true);

		tps = serializedObject.FindProperty ("spawnRegions");
		EditorGUILayout.PropertyField(tps, true);//*/
		EditorGUI.BeginChangeCheck();
		SerializedProperty tps=serializedObject.GetIterator();
		tps.NextVisible(true);
		do{
			EditorGUILayout.PropertyField(tps, true);
			//tps=tps.Next(true);
		}while(tps.NextVisible(false));

		if(EditorGUI.EndChangeCheck()) {
			serializedObject.ApplyModifiedProperties();
		}
		EditorGUIUtility.LookLikeControls();
	}
}

