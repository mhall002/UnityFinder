using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;

public class RemoteView : MonoBehaviour {
    public CampaignController CampaignController;
    public TerrainStorage TerrainStorage;
    private Campaign campaign;
    public Campaign Campaign
    {
        get { return campaign; }
        set
        {
            if (campaign != null)
                campaign.PropertyChanged -= campaign_PropertyChanged;
            campaign = value;
            if (campaign != null)
                campaign.PropertyChanged += campaign_PropertyChanged;
            SendAll();
        }
    }

    void campaign_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        SendAll();
    }

    void SendAll()
    {
        SendTerrains();
        SendCampaign();
    }

    void SendCampaign()
    {
        if (campaign == null)
        {
            networkView.RPC("CreateCampaign", RPCMode.OthersBuffered);
        }
        else
        {
            networkView.RPC("CreateCampaign", RPCMode.OthersBuffered, Campaign.Name);
        }
    }

    void SendTerrains()
    {
        networkView.RPC("ClearTerrain", RPCMode.OthersBuffered);
        foreach (Ground terrain in TerrainStorage.GetTerrains())
        {
            SendTerrain(terrain);
        }
        networkView.RPC("TerrainComplete", RPCMode.OthersBuffered);
    }

    void SendTerrain(Ground terrain)
    {
        networkView.RPC("AddTerrain", RPCMode.OthersBuffered, terrain.Name, terrain.TextureFile, terrain.CharacterCode);
    }

    void SendRooms()
    {
        Room[,] rooms = Campaign.GetRooms();
        for (int x = 0; x < Campaign.Width; x++)
        {
            for (int y = 0; y < Campaign.Height; y++)
            {
                SendRoom(x, y, rooms[x,y]);
            }
        }
    }

    void SendRoom(int x, int y, Room room)
    {
        networkView.RPC("CreateRoom", RPCMode.OthersBuffered, room.GetTerrainString());
    }



	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}

    public void Initiate()
    {
        CampaignController.PropertyChanged += CampaignController_PropertyChanged;
        Campaign = CampaignController.Campaign;
    }

    void CampaignController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        Campaign = CampaignController.Campaign;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

