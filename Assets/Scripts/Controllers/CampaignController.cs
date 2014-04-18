using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;
using System.ComponentModel;
using System;

public class CampaignController : MonoBehaviour, INotifyPropertyChanged {

    public TerrainStorage TerrainStorage;
    public Campaign Campaign;
    private Room activeRoom;
    private Ground NormalTerrain;
    private Ground BlockedTerrain;
    public Room ActiveRoom
    {
        get { return activeRoom; }
        set 
        { 
            activeRoom = value;
            OnPropertyChanged("ActiveRoom");
        }
    }
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
                    room.TerrainGrid[y, x] = BlockedTerrain;
                }
                else if ((x >= Room.Width - 2 || x <= 1) && Math.Abs(y - Room.Height / 2) > 1)
                {
                    room.TerrainGrid[y, x] = BlockedTerrain;
                }
                else
                {
                    room.TerrainGrid[y, x] = NormalTerrain;
                }
            }
        }
        return room;
    }

    public void CreateRoom (int gridX, int gridY)
    {
        Room room = InitializeRoom();
        Campaign.Rooms[gridY, gridX] = room;
        ActiveRoom = room;
        OnPropertyChanged("Rooms[" + gridY + "," + gridX + "]");
        Debug.Log("Creating new room");
    }

    public void SetRoom(int gridX, int gridY)
    {
        Room room = Campaign.Rooms[gridY, gridX];
        ActiveRoom = room;
    }

    public void DeleteRoom (int gridX, int gridY)
    {
        if (ActiveRoom == Campaign.Rooms[gridY, gridX])
        {
            activeRoom = null;
        }
        Campaign.Rooms[gridY, gridX] = null;
        OnPropertyChanged("Rooms[" + gridY + "," + gridX + "]");
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
}
