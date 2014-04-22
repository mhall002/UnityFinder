using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;
using System.Collections.Generic;

public class RemoteView : MonoBehaviour {
    public CampaignController CampaignController;
    public TerrainStorage TerrainStorage;
    public NetworkController NetworkController;
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
            SendCampaign();
        }
    }

    private Dictionary<Room, Pair<int, int>> RoomToLocation = new Dictionary<Room, Pair<int, int>>();

    void campaign_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName.StartsWith("Rooms"))
        {
            string property = e.PropertyName;
            string[] indexes = property.Split('[')[1].Split(',');
            Debug.Log(indexes[1].Substring(0, indexes[1].IndexOf(']') - 1));
            int x = int.Parse(indexes[0]);
            int y = int.Parse(indexes[1].Substring(0, indexes[1].IndexOf(']')));
            Room room = Campaign.GetRoom(x, y);
            if (room != null && room.Visible)
                SendRoom(x, y, room);
            room.PropertyChanged += room_PropertyChanged;
            RoomToLocation[room] = new Pair<int, int>(x, y);
            Debug.Log(sender.ToString());
        }
        if (e.PropertyName.StartsWith("AddLink"))
        {
            string property = e.PropertyName;
            string[] indexes = property.Split('[')[1].Split(',');
            int x1 = int.Parse(indexes[0]);
            int y1 = int.Parse(indexes[1]);
            int x2 = int.Parse(indexes[2]);
            int y2 = int.Parse(indexes[3].Substring(0, indexes[1].IndexOf(']')));
            AddLink(x1, y1, x2, y2);
        }
        if (e.PropertyName.StartsWith("RemoveLink"))
        {
            string property = e.PropertyName;
            string[] indexes = property.Split('[')[1].Split(',');
            int x1 = int.Parse(indexes[0]);
            int y1 = int.Parse(indexes[1]);
            int x2 = int.Parse(indexes[3]);
            int y2 = int.Parse(indexes[4].Substring(0, indexes[1].IndexOf(']')));
            RemoveLink(x1, y1, x2, y2);
        }

    }

    void room_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        Room room = (Room)sender;
        Pair<int, int> location = RoomToLocation[(Room)sender];
        int x = location.First;
        int y = location.Second;
        if (room.Visible)
        {
            if (e.PropertyName.StartsWith("Terrain["))
            {
                string property = e.PropertyName;
                string[] indexes = property.Split('[')[1].Split(',');
                int tx = int.Parse(indexes[0]);
                int ty = int.Parse(indexes[1].Substring(0, indexes[1].IndexOf(']')));
                SetTerrain(x, y, tx, ty, room.GetTerrain(tx, ty));
            }
            if (e.PropertyName == "XPWorth")
            {
                SetRoomXPWorth(x, y, room.XPWorth);
            }
        }
        if (e.PropertyName == "Visible")
        {
            if (room.Visible)
            {
                SendRoom(x, y, room);
            }
            else
            {
                DeleteRoom(x, y);
            }
        }

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
            NetworkController.networkView.RPC("DeleteCampaign", RPCMode.OthersBuffered);
        }
        else
        {
            NetworkController.networkView.RPC("CreateCampaign", RPCMode.OthersBuffered, Campaign.Name);
            SendRooms();
            SendLinks();
            Debug.Log("Sent Campaign!");
        }
    }

    void SendTerrains()
    {
        NetworkController.networkView.RPC("ClearTerrain", RPCMode.OthersBuffered);
        foreach (Ground terrain in TerrainStorage.GetTerrains())
        {
            SendTerrain(terrain);
        }
        NetworkController.networkView.RPC("TerrainComplete", RPCMode.OthersBuffered);
    }

    void SendTerrain(Ground terrain)
    {
        NetworkController.networkView.RPC("AddTerrain", RPCMode.OthersBuffered, terrain.Name, terrain.TextureFile, terrain.CharacterCode+"");
    }

    void SendRooms()
    {
        Room[,] rooms = Campaign.GetRooms();
        for (int x = 0; x < Campaign.Width; x++)
        {
            for (int y = 0; y < Campaign.Height; y++)
            {
                if (rooms[x, y] != null)
                {
                    rooms[x, y].PropertyChanged += room_PropertyChanged;
                    RoomToLocation.Add(rooms[x, y], new Pair<int, int>(x, y));
                    if (rooms[x,y].Visible)
                        SendRoom(x, y, rooms[x, y]);
                }
            }
        }
    }

    void SendRoom(int x, int y, Room room)
    {
        if (room != null)
        {
            NetworkController.networkView.RPC("CreateRoom", RPCMode.OthersBuffered, x, y, room.GetTerrainString());
            SetRoomXPWorth(x, y, room.XPWorth);
            Debug.Log("Sending Room");
        }
        else
        {
            DeleteRoom(x, y);
        }
    }

    void SendLinks()
    {
        foreach (Pair<Pair<int, int>, Pair<int, int>> pair in Campaign.GetLinks())
        {
            AddLink(pair.First.First, pair.First.Second, pair.Second.First, pair.Second.Second);
        }
    }

    void AddLink(int x1, int y1, int x2, int y2)
    {
        NetworkController.networkView.RPC("AddLink", RPCMode.OthersBuffered, x1, y1, x2, y2);
    }

    void RemoveLink(int x1, int y1, int x2, int y2)
    {
        NetworkController.networkView.RPC("RemoveLink", RPCMode.OthersBuffered, x1, y1, x2, y2);
    }

    void DeleteRoom(int x, int y)
    {
        NetworkController.networkView.RPC("DeleteRoom", RPCMode.OthersBuffered, x, y);
    }

    void SetTerrain(int roomx, int roomy, int x, int y, Ground terrain)
    {
        NetworkController.networkView.RPC("SetTerrain", RPCMode.OthersBuffered, roomx, roomy, x, y, terrain.CharacterCode.ToString());
    }

    void SetRoomXPWorth(int x, int y, int xp)
    {
        NetworkController.networkView.RPC("SetRoomXPWorth", RPCMode.OthersBuffered, x, y, xp);
    }

	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}

    public void Initiate()
    {
        CampaignController.PropertyChanged += CampaignController_PropertyChanged;
        SendTerrains();
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

