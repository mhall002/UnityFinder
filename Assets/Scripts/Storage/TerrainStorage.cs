using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;
using System.Collections.Generic;
using AssemblyCSharp;
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

            var terrainSQL = "SELECT * from Terrain;";
            SqliteConnection connection = Database.getDBConnection();
            var terrainReader = Database.runSelectQuery(connection, new SqliteCommand(terrainSQL, connection));
            while (terrainReader.Read())
            {
                Ground currentItem = new Ground()
                {
                    Name = terrainReader.GetValue(4).ToString(),
                    TypeID = int.Parse(terrainReader.GetValue(5).ToString()),
                    TextureFile = terrainReader.GetValue(3).ToString(),
                    CharacterCode = terrainReader.GetValue(2).ToString()[0]
                };
                Terrains.Add(currentItem.Name, currentItem);
                CTerrains.Add(currentItem.CharacterCode, currentItem);
            }

            Loaded = true;
        }
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
