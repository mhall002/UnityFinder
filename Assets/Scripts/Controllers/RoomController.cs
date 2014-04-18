using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;
using System.ComponentModel;

public class RoomController : MonoBehaviour, INotifyPropertyChanged {

    public event PropertyChangedEventHandler PropertyChanged;
    public CampaignController CampaignController;

    public Room Room 
    {
        get { return CampaignController.ActiveRoom; }
    }


	// Use this for initialization
	void Start () {
        CampaignController.PropertyChanged += CampaignController_PropertyChanged;
	}

    void CampaignController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "ActiveRoom") // Room has changed
        {
            OnPropertyChanged("Room");
            Debug.Log("Sent room");
        }
    }

    public void SetTerrain(int x, int y, Ground terrain)
    {
        Room.TerrainGrid[y, x] = terrain;
        Debug.Log("Terrain at "+ x + "," + y + " = " + Room.TerrainGrid[y, x].Name);
        Room.RoomID = 7;
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
