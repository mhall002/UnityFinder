using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Models;

public class RoomEditor : MonoBehaviour {

    public EntityStorage EntityStorage;
    public TerrainStorage TerrainStorage;
    public SpriteStorage SpriteStorage;
    public MapGrid MapGrid;
    public CampaignController CampaignController;

    ICollection<Ground> Terrains;
    ICollection<Entity> Entities;
    int EntityCount = 0;

    public Ground SelectedTerrain = null;

	// Use this for initialization
	void Start () {
        Terrains = TerrainStorage.GetTerrains();
        Entities = EntityStorage.GetEntities();
	}
	


    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width - 150, 0, 150, 500));
        GUILayout.BeginVertical("box");
        GUILayout.Button("Set as active room");
        GUILayout.Label("Terrains:");
        GUILayout.BeginHorizontal("box");
        foreach (Ground terrain in Terrains)
        {
            if (GUILayout.Button(SpriteStorage.GetTexture(terrain.TextureFile), GUILayout.Width(30), GUILayout.Height(30)))
            {
                SelectedTerrain = terrain;
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.Label("Entities:");
        GUILayout.BeginHorizontal("box");
        foreach (Entity entity in Entities)
        {
            if (GUILayout.Button(SpriteStorage.GetTexture(entity.Image), GUILayout.Width(30), GUILayout.Height(30)))
            {
                MapGrid.SelectClone(entity);
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButtonDown(1))
        {
            SelectedTerrain = null;
        }
	}
}
