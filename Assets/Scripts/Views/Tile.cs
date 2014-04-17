using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;

public class Tile : MonoBehaviour {

    Ground terrain;
    SpriteStorage SpriteStorage;

    public Ground Terrain
    {
        get { return terrain;  }
        set 
        { 
            terrain = value;
            TerrainChanged();
        }
    }

    void TerrainChanged()
    {
        if (SpriteStorage == null)
        {
            SpriteStorage = (SpriteStorage)GameObject.Find("SpriteStorage").GetComponent("SpriteStorage");
        }
        (GetComponent("SpriteRenderer") as SpriteRenderer).sprite = SpriteStorage.GetSprite(Terrain.TextureFile);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
