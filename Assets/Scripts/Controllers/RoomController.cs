using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;
using System.ComponentModel;

public class RoomController : MonoBehaviour, INotifyPropertyChanged {

    public event PropertyChangedEventHandler PropertyChanged;
    public CampaignController CampaignController;

    private Campaign campaign;
    public Campaign Campaign
    {
        get
        {
            return campaign;
        }
        set
        {
            if (campaign != null)
            {
                campaign.PropertyChanged -= Campaign_PropertyChanged;
            }
            campaign = value;
            if (campaign != null)
            {
                campaign.PropertyChanged += Campaign_PropertyChanged;
                Room = campaign.ActiveRoom;
            }
            else
                Room = null;
        }
    }

    private Room room;
    public Room Room 
    {
        get { return room; }
        set
        {
            room = value;
            OnPropertyChanged("Room");
        }
    }


	// Use this for initialization
	void Start () {
        CampaignController.PropertyChanged += CampaignController_PropertyChanged;
        Campaign = CampaignController.Campaign;
	}

    void CampaignController_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "Campaign") // New campaign loaded / created
        {
            Campaign = CampaignController.Campaign;
        }
    }

    void Campaign_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "ActiveRoom") // Room has changed
        {
            Room = Campaign.ActiveRoom;
        }
    }


    public void SetTerrain(int x, int y, Ground terrain)
    {
        Room.SetTerrain(x, y, terrain);
        Room.RoomID = 7;
    }

    public void SetVisible(bool visible)
    {

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
