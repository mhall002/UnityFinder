using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;

public class NetworkController : MonoBehaviour {

    public TerrainStorage TerrainStorage;
    public CampaignController CampaignController;

    private Campaign campaign;
    public Campaign Campaign
    {
        get { return campaign; }
        set
        {
            campaign = value;
            CampaignController.Campaign = campaign;
        }
    }

    [RPC]
    void CreateCampaign()
    {
        Campaign = null;
    }


    [RPC]
    void CreateCampaign(string name)
    {
        Campaign = new Campaign();
        Campaign.Name = name;
    }

    [RPC]
    void CreateRoom(int x, int y, string terrain)
    {
        Room room = new Room();
        SetTerrain(room, terrain);
        Campaign.SetRoom(x, y, room);
    }

    [RPC]
    void DeleteRoom(int x, int y)
    {
        Campaign.SetRoom(x, y, null);
    }

    [RPC]
    void ClearTerrain()
    {
        // nope
    }

    [RPC]
    void AddTerrain(string name, string texturePath, char code)
    {
        Ground terrain = new Ground()
        {
            Name = name,
            CharacterCode = code,
            TextureFile = texturePath
        };
        TerrainStorage.AddTerrain(terrain);
    }

    [RPC]
    void TerrainComplete()
    {
        // nope
    }
	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}

    public void SetTerrain(Room room, string terrainString)
    {
        for (int x = 0; x < Room.Width; x++)
        {
            for (int y = 0; y < Room.Height; y++)
            {
                room.SetTerrain(x, y, TerrainStorage.GetTerrain(terrainString[x * Room.Height + Room.Height]));
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
