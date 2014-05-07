using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpriteStorage))]
public class TextureSettings : Editor {

	// Use this for initialization
	void Start () {
	
	}
	
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SpriteStorage SpriteStorage = (SpriteStorage)target;
        //Debug.Log("I exist");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("OHI");
        EditorGUILayout.TextField("TOBOGGAN FACE");
        EditorGUILayout.EndHorizontal();
        GameObject GameObject;
    }

	// Update is called once per frame
	void Update () {
	
	}
}
