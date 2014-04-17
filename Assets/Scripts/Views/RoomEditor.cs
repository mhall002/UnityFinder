using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Models;

public class RoomEditor : MonoBehaviour {

    public TerrainStorage TerrainStorage;
    public SpriteStorage SpriteStorage;

    ICollection<Ground> Terrains;


	// Use this for initialization
	void Start () {
        Terrains = TerrainStorage.GetTerrains();
	}
	


    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width - 150, 0, 150, 500));
        GUILayout.BeginVertical("box");
        GUILayout.Button("Set as active room");
        GUILayout.BeginHorizontal("box");
        foreach (Ground terrain in Terrains)
        {
            GUILayout.Button(SpriteStorage.GetTexture(terrain.TextureFile));
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

	// Update is called once per frame
	void Update () {
	
	}
}
