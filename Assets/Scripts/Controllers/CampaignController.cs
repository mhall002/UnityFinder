using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;
using System.ComponentModel;
using System;

public class CampaignController : MonoBehaviour, INotifyPropertyChanged {

    public TerrainStorage TerrainStorage;

    private Campaign campaign;
    public Campaign Campaign
    {
        get
        {
            return campaign;
        }
        set
        {
            campaign = value;
            OnPropertyChanged("Campaign");
        }
    }

    private Ground NormalTerrain;
    private Ground BlockedTerrain;

    public event PropertyChangedEventHandler PropertyChanged;

	// Use this for initialization
	void Start () {
	
	}
	
    public void CreateCampaign (string name)
    {
        Campaign = new Campaign();
        Campaign.Name = name;
    }

    public Room InitializeRoom()
    {
        Room room = new Room();
        if (NormalTerrain == null)
        {
            NormalTerrain = TerrainStorage.GetTerrain("Normal");
        }
        if (BlockedTerrain == null)
        {
            BlockedTerrain = TerrainStorage.GetTerrain("Inaccessible");
        }

        for (int y = 0; y < Room.Height; y++)
        {
            for (int x = 0; x < Room.Width; x++)
            {
                if ((y >= Room.Height - 2 || y <= 1) && Math.Abs(x - Room.Width / 2) > 1)
                {
                    room.SetTerrain(x,y,BlockedTerrain);
                }
                else if ((x >= Room.Width - 2 || x <= 1) && Math.Abs(y - Room.Height / 2) > 1)
                {
                    room.SetTerrain(x,y,BlockedTerrain);
                }
                else
                {
                    room.SetTerrain(x,y,NormalTerrain);
                }
            }
        }
        return room;
    }

    public void CreateRoom (int gridX, int gridY)
    {
        Room room = InitializeRoom();
        Campaign.SetRoom(gridX, gridY, room);
        Campaign.ActiveRoom = room;
        Debug.Log("Creating new room");
    }

    public void SetRoom(int gridX, int gridY)
    {
        Room room = Campaign.GetRoom(gridX, gridY);
        Campaign.ActiveRoom = room;
    }

    public void DeleteRoom (int gridX, int gridY)
    {
        if (Campaign.ActiveRoom == Campaign.GetRoom(gridX, gridY))
        {
            Campaign.ActiveRoom = null;
        }
        Campaign.SetRoom(gridX, gridY, null);
    }

	// Update is called once per frame
	void Update () {
	
	}

    protected void OnPropertyChanged(string name)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(name));
        }
    }

    public void SetVisible(int x, int y, bool visible)
    {
        Campaign.GetRoom(x, y).Visible = visible;
    }

    public void RemoveLink(Pair<int, int> room1, Pair<int, int> room2)
    {
        Campaign.AddLink(room1, room2);
    }

    public void CreateLink(Pair<int,int> room1, Pair<int,int> room2)
    {
        Campaign.AddLink(room1, room2);
    }
}
