using UnityEngine;
using System.Collections;

public class CampaignGridScript : MonoBehaviour {

    public GameObject blankRoomPrefab;
    public CampaignController CampaignController;
    public BlankRoomScript[,] RoomSquares;

	// Use this for initialization
	void Start () {
        //Creates the grid of Add Room
        RoomSquares = new BlankRoomScript[9, 17];
        float spacing = 1.0f;
        int xCounter = 0;
        int yCounter = 0;
        for (float x = -9.5f; x < 7.18f; x += spacing)
        {
            yCounter = 0;
            for (float y = -4.2f; y < 4.5f; y += spacing)
            {
                BlankRoomScript go = (Instantiate(blankRoomPrefab, new Vector3(x,y,0), Quaternion.identity) as GameObject).GetComponent("BlankRoomScript") as BlankRoomScript;
                go.transform.parent = transform;
                RoomSquares[yCounter, xCounter] = go;
                go.Room = CampaignController.Campaign.Rooms[yCounter, xCounter];
                go.GridX = xCounter;
                go.GridY = yCounter;
                yCounter++;
            }
            xCounter++;
        }
        Debug.Log(xCounter + " x " + yCounter);

        CampaignController.PropertyChanged += CampaignController_PropertyChanged;
	}

    void CampaignController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName.StartsWith("Rooms"))
        {
            string property = e.PropertyName;
            string[] indexes = property.Split('[')[1].Split(',');
            Debug.Log(indexes[1].Substring(0, indexes[1].IndexOf(']') - 1));
            int y = int.Parse(indexes[0]);
            int x = int.Parse(indexes[1].Substring(0, indexes[1].IndexOf(']')));
            RoomSquares[y, x].Room = CampaignController.Campaign.Rooms[y, x];
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
