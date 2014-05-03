using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;

public class CampaignGrid : MonoBehaviour {

    public GameObject blankRoomPrefab;
    public GameObject connectorPrefab;
    public CampaignController CampaignController;
    public RoomConnector[,] RoomConnectionsRight;
    public RoomConnector[,] RoomConnectionsUp;

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
        RoomSquares = new RoomIcon[17, 9];
        RoomConnectionsRight = new RoomConnector[16, 9];
        RoomConnectionsUp = new RoomConnector[17, 8];
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
        for (int x = 0; x < Campaign.Width; x++)
        {
            for (int y = 0; y < Campaign.Height; y++)
            {
                if (x < Campaign.Width - 1)
                {
                    RoomConnectionsRight[x, y] = (Instantiate(connectorPrefab, new Vector3(x*spacing - 9.5f + 0.5f, y*spacing - 4.2f, 0), Quaternion.identity) as GameObject).GetComponent("RoomConnector") as RoomConnector;
					RoomConnectionsRight[x, y].transform.parent = this.transform;
                    RoomConnectionsRight[x, y].Setup(x, y, x + 1, y);
                }
                if (y < Campaign.Height - 1)
                {
                    RoomConnectionsUp[x, y] = (Instantiate(connectorPrefab, new Vector3(x * spacing - 9.5f, y * spacing - 4.2f + 0.5f, 0), Quaternion.identity) as GameObject).GetComponent("RoomConnector") as RoomConnector;
					RoomConnectionsUp[x, y].transform.parent = this.transform;
                    RoomConnectionsUp[x, y].Setup(x, y, x, y + 1);
                }
            }
        }

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
                    if (x < Campaign.Width - 1)
                    {
                        if (Campaign.GetRoom(x,y) != null && Campaign.GetRoom(x+1,y) != null)
                        {
                            RoomConnectionsRight[x, y].gameObject.SetActive(true);
                            Debug.Log("Active Right " + x + " " + y);
                            RoomConnectionsRight[x, y].Enabled = Campaign.HasLink(new Pair<int, int>(x, y), new Pair<int, int>(x + 1, y));
                        }
                        else
                        {
                            RoomConnectionsRight[x, y].gameObject.SetActive(false);
                        }
                    }
                    if (y < Campaign.Height - 1)
                    {
                        if (Campaign.GetRoom(x, y) != null && Campaign.GetRoom(x, y + 1) != null)
                        {
                            RoomConnectionsUp[x, y].gameObject.SetActive(true);
                            Debug.Log("Active Up " + x + " " + y);
                            RoomConnectionsUp[x, y].Enabled = Campaign.HasLink(new Pair<int, int>(x, y), new Pair<int, int>(x, y + 1));
                        }
                        else
                        {
                            RoomConnectionsUp[x, y].gameObject.SetActive(false);
                        }
                    }
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
            ResetGrid();
        }
        if (e.PropertyName.StartsWith("AddLink") || e.PropertyName.StartsWith("RemoveLink"))
        {
            ResetGrid();
            Debug.Log("Reset the grid");
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
