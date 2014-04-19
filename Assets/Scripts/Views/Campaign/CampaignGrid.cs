using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;

public class CampaignGrid : MonoBehaviour {

    public GameObject blankRoomPrefab;
    public CampaignController CampaignController;

    Campaign campaign;
    Campaign Campaign
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
                campaign.PropertyChanged += Campaign_PropertyChanged;
            ResetGrid();
        }
    }

    public RoomIcon[,] RoomSquares;

	// Use this for initialization
	void Start () {
        //Creates the grid of Add Room
        Debug.Log("Creating Grid");
        RoomSquares = new RoomIcon[17, 9];
        float spacing = 1.0f;
        int xCounter = 0;
        int yCounter = 0;
        for (float x = -9.5f; x < 7.18f; x += spacing)
        {
            yCounter = 0;
            for (float y = -4.2f; y < 4.5f; y += spacing)
            {
                RoomIcon go = (Instantiate(blankRoomPrefab, new Vector3(x,y,0), Quaternion.identity) as GameObject).GetComponent("RoomIcon") as RoomIcon;
                go.transform.parent = transform;
                RoomSquares[xCounter, yCounter] = go;
                go.GridX = xCounter;
                go.GridY = yCounter;
                yCounter++;
            }
            xCounter++;
        }
        Debug.Log(xCounter + " x " + yCounter);

        CampaignController.PropertyChanged += CampaignController_PropertyChanged;
        Campaign = CampaignController.Campaign;
	}

    void ResetGrid()
    {
        if (Campaign != null)
        {
            for (int x = 0; x < 17; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    RoomSquares[x, y].Room = Campaign.GetRoom(x, y);
                }
            }
        }
    }

    void Campaign_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName.StartsWith("Rooms"))
        {
            string property = e.PropertyName;
            string[] indexes = property.Split('[')[1].Split(',');
            Debug.Log(indexes[1].Substring(0, indexes[1].IndexOf(']') - 1));
            int x = int.Parse(indexes[0]);
            int y = int.Parse(indexes[1].Substring(0, indexes[1].IndexOf(']')));
            RoomSquares[x, y].Room = Campaign.GetRoom(x, y);
        }
    }

    void CampaignController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName.Equals("Campaign"))
        {
            Campaign = CampaignController.Campaign;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
