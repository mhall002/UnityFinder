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
    void DeleteCampaign()
    {
        Debug.Log("Got campaign");
        Campaign = null;
    }


    [RPC]
    void CreateCampaign(string name)
    {
        Debug.Log("Got Campaign " + name);
        Campaign = new Campaign();
        Campaign.Name = name;
    }

    [RPC]
    void CreateRoom(int x, int y, string terrain)
    {
        Room room = new Room();
        SetTerrain(room, terrain);
        Campaign.SetRoom(x, y, room);
        Debug.Log("Received Room");
    }

    [RPC]
    void DeleteRoom(int x, int y)
    {
        Campaign.SetRoom(x, y, null);
    }

    [RPC]
    void AddLink(int x1, int y1, int x2, int y2)
    {
        Campaign.AddLink(new Pair<int, int>(x1, y1), new Pair<int, int>(x2, y2));
    }

    [RPC]
    void RemoveLink(int x1, int y1, int x2, int y2)
    {
        Campaign.RemoveLink(new Pair<int, int>(x1, y1), new Pair<int, int>(x2, y2));
    }

    [RPC]
    void SetTerrain(int roomx, int roomy, int x, int y, string terrainCode)
    {
        Campaign.GetRoom(roomx, roomy).SetTerrain(x, y, TerrainStorage.GetTerrain(terrainCode[0]));
    }

    [RPC]
    void SetRoomXPWorth(int x, int y, int xp)
    {
        Campaign.GetRoom(x, y).XPWorth = xp;
    }

    [RPC]
    void ClearTerrain()
    {
        // nope
    }

    [RPC]
    void AddTerrain(string name, string texturePath, string code)
    {
        Ground terrain = new Ground()
        {
            Name = name,
            CharacterCode = code[0],
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
                room.SetTerrain(x, y, TerrainStorage.GetTerrain(terrainString[x * Room.Height + y]));
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
