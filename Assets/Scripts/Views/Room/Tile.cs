using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;

public class Tile : MonoBehaviour {

    Ground terrain;
    SpriteStorage SpriteStorage;
    RoomEditor RoomEditor;
    SpriteRenderer Renderer;
    RoomController RoomController;
    public int X;
    public int Y;

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
        if (Renderer == null)
            Renderer = (GetComponent("SpriteRenderer") as SpriteRenderer);
        Renderer.sprite = SpriteStorage.GetSprite(Terrain.TextureFile);
    }

	// Use this for initialization
	void Start () {
        RoomEditor = (RoomEditor)GameObject.Find("RoomEditor").GetComponent("RoomEditor");
        RoomController = (RoomController)GameObject.Find("RoomController").GetComponent("RoomController");
        if (Renderer == null)
            Renderer = (GetComponent("SpriteRenderer") as SpriteRenderer);
	}

    bool UsingMouseOverTexture = false;

    void OnMouseEnter()
    {
        if (RoomEditor.SelectedTerrain != null)
        {
            if (Input.GetMouseButton(0))
            {
                this.terrain = RoomEditor.SelectedTerrain;
                TerrainChanged();
                RoomController.SetTerrain(X, Y, this.terrain);
            }
            else
            {
                Renderer.sprite = SpriteStorage.GetSprite(RoomEditor.SelectedTerrain.TextureFile);
            }
        }
    }



    void OnMouseExit()
    {
        Renderer.sprite = SpriteStorage.GetSprite(Terrain.TextureFile);
    }

    void OnMouseDown()
    {
        if (RoomEditor.SelectedTerrain != null)
        {
            this.terrain = RoomEditor.SelectedTerrain;
            TerrainChanged();
            RoomController.SetTerrain(X, Y, this.terrain);
            Debug.Log("Setting to " + this.terrain.Name);
        }
    }

	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButtonDown(1))
        {
            Renderer.sprite = SpriteStorage.GetSprite(Terrain.TextureFile);
        }
	}
}
