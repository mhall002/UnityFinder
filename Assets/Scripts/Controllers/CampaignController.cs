using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;
using System.ComponentModel;

public class CampaignController : MonoBehaviour, INotifyPropertyChanged {

    public Campaign Campaign;

    private Room activeRoom;
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

    public void CreateRoom (int gridX, int gridY)
    {
        Room room = new Room();
        Campaign.Rooms[gridY, gridX] = room;
        ActiveRoom = room;
        OnPropertyChanged("Rooms[" + gridY + "," + gridX + "]");
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
