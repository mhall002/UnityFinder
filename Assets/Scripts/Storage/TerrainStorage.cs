using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;
using System.Collections.Generic;

public class TerrainStorage : MonoBehaviour {

    Dictionary<string, Ground> Terrains = new Dictionary<string, Ground>();
    bool Loaded = false;

	// Use this for initialization
	void Start () {
	
	}
	
    // Populate from DB, for now just hard coded
    void LoadTerrains()
    {
        Ground NormalTerrain = new Ground()
        {
            Name = "Normal Tile",
            TypeID = 1,
            TextureFile = "Tiles/NormalTile",
            States = new List<State>()
        };
        Terrains.Add("Normal", NormalTerrain);

        Ground BlockedTerrain = new Ground()
        {
            Name = "Inaccessible Tile",
            TypeID = 1,
            TextureFile = "Tiles/BlackTile",
            States = new List<State>()
        };
        BlockedTerrain.States.Add(new State()
        {
            Name = "Inaccessible",
            Description = "You cannot move here"
        });
        Terrains.Add("Inaccessible", BlockedTerrain);

        Loaded = true;
    }

    public ICollection<Ground> GetTerrains()
    {
        if (!Loaded)
        {
            LoadTerrains();
        }
        return Terrains.Values;
    }

    public Ground GetTerrain(string name)
    {
        if (!Loaded)
        {
            LoadTerrains();
        }
        return Terrains[name];
    }

	// Update is called once per frame
	void Update () {
	
	}
}
