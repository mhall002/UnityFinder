using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;

public class Tile : MonoBehaviour {

    MapGrid MapGrid;
    Ground terrain;
    SpriteStorage SpriteStorage;
    RoomEditor RoomEditor;
    SpriteRenderer Renderer;
	CampaignController CampaignController;
	public int X;
    public int Y;
	private Room room;
	public Room Room
	{
		get { return room; }
		set
		{
			room = value;
			Terrain = Room.GetTerrain(X, Y);
		}
	}

    public Ground Terrain
    {
        get { return terrain;  }
        set 
        { 
            terrain = value;
            TerrainChanged();
        }
    }

	public void Notify()
	{
		Terrain = Room.GetTerrain(X, Y);
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
        GameObject go = GameObject.Find("RoomEditor") as GameObject;
        if (go != null)
            RoomEditor = (RoomEditor)GameObject.Find("RoomEditor").GetComponent("RoomEditor");
		CampaignController = (CampaignController)GameObject.Find("CampaignController").GetComponent("CampaignController");
        if (Renderer == null)
            Renderer = (GetComponent("SpriteRenderer") as SpriteRenderer);
        if (MapGrid == null)
            MapGrid = GameObject.Find("MapGrid").GetComponent("MapGrid") as MapGrid;
	}

    bool UsingMouseOverTexture = false;

    void OnMouseEnter()
    {
        if (RoomEditor != null && RoomEditor.SelectedTerrain != null)
        {
            if (Input.GetMouseButton(0))
            {
                this.terrain = RoomEditor.SelectedTerrain;
                TerrainChanged();
				CampaignController.SetTerrain(Room, X, Y, this.terrain);
            }
            else
            {
                Renderer.sprite = SpriteStorage.GetSprite(RoomEditor.SelectedTerrain.TextureFile);
            }
        }
        MapGrid.GotMouseOver(new Vector4(room.X, room.Y, X, Y));
    }



    void OnMouseExit()
    {
        if (RoomEditor != null)
            Renderer.sprite = SpriteStorage.GetSprite(Terrain.TextureFile);
        MapGrid.LostMouseOver(new Vector4(room.X, room.Y, X, Y));
    }

    void OnMouseDown()
    {
        if (RoomEditor != null && RoomEditor.SelectedTerrain != null)
        {
            this.terrain = RoomEditor.SelectedTerrain;
            TerrainChanged();
			CampaignController.SetTerrain(Room, X, Y, this.terrain);
            Debug.Log("Setting to " + this.terrain.Name);
        }
        MapGrid.GotMouseDown(new Vector4(room.X, room.Y, X, Y));
    }

	// Update is called once per frame
	void Update () {
	    if (RoomEditor != null && Input.GetMouseButtonDown(1))
        {
            Renderer.sprite = SpriteStorage.GetSprite(Terrain.TextureFile);
        }
	}
}
