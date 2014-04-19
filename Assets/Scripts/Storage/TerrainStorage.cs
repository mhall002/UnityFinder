using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using Mono.Data;
using Mono.Data.SqliteClient;

public class TerrainStorage : MonoBehaviour {
    public SessionManager SessionManager;
    public NetworkController NetworkController;
    Dictionary<string, Ground> Terrains = new Dictionary<string, Ground>();
    Dictionary<char, Ground> CTerrains = new Dictionary<char, Ground>();
    bool Loaded = false;

	// Use this for initialization
	void Start () {
	
	}
	
    // Populate from DB, for now just hard coded
    void LoadTerrains()
    {
        if (!SessionManager.IsClient)
        {
            Ground NormalTerrain = new Ground()
            {
                Name = "Normal",
                TypeID = 1,
                TextureFile = "Tiles/NormalTile",
                States = new List<State>(),
                CharacterCode = 'a'
            };
            Terrains.Add("Normal", NormalTerrain);
            CTerrains.Add('a', NormalTerrain);

            Ground BlockedTerrain = new Ground()
            {
                Name = "Inaccessible",
                TypeID = 1,
                TextureFile = "Tiles/BlackTile",
                States = new List<State>(),
                CharacterCode = 'b'
            };
            BlockedTerrain.States.Add(new State()
            {
                Name = "Inaccessible",
                Description = "You cannot move here"
            });
            Terrains.Add("Inaccessible", BlockedTerrain);
            CTerrains.Add('b', NormalTerrain);
        }

        Loaded = true;
    }

    public void AddTerrain(Ground terrain)
    {
        Terrains.Add(terrain.Name, terrain);
        CTerrains.Add(terrain.CharacterCode, terrain);
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

    public Ground GetTerrain(char code)
    {
        if (!Loaded)
        {
            LoadTerrains();
        }
        return CTerrains[code];
    }

	// Update is called once per frame
	void Update () {
	
	}
}
