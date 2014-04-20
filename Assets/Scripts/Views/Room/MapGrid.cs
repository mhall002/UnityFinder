using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;

public class MapGrid : MonoBehaviour {

	public CampaignController CampaignController;
	public GameObject TileGridPrefab;
	TileGrid[,] Map = new TileGrid[Campaign.Width, Campaign.Height];

	Campaign campaign;
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
				campaign.PropertyChanged += Campaign_PropertyChanged;
			UpdateAll();
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
			UpdateRoom (x, y);
			Debug.Log ("MapGrid Got Room Update");
		}
	}

	float spacing = 0.75f;
	Vector3 GetPosition(int x, int y)
	{
		return new Vector3 (-Room.Width / 2 + (Room.Width * spacing) * x - 10.2f,
		                    -Room.Height / 2 + (Room.Height * spacing) * y - 4.6f, 0) + gameObject.transform.position;
	}

	Vector3 GetAbsPosition(int x, int y)
	{
		return new Vector3 (-Room.Width / 2 + (Room.Width * spacing) * x - 10.2f,
		                    -Room.Height / 2 + (Room.Height * spacing) * y - 4.6f, 0);
	}

	public void SetPosition(int x, int y)
	{
		this.transform.position = -GetAbsPosition(x, y);
	}

	void UpdateAll()
	{
		if (Campaign != null)
		{
			for (int x = 0; x < Campaign.Width; x++)
			{
				for (int y = 0; y < Campaign.Height; y++)
				{
					UpdateRoom (x,y);
				}
			}
		}
	}

	void UpdateRoom (int x, int y)
	{
		if (Campaign.GetRoom (x, y) != null) 
		{
			if (Map[x,y] == null)
			{
				Map [x, y] = (Instantiate(TileGridPrefab, GetPosition (x,y), Quaternion.identity) as GameObject).GetComponent("TileGrid") as TileGrid;
				Map [x, y].transform.parent = this.transform;
				Debug.Log ("Created");
			}
			Map [x, y].Room = Campaign.GetRoom (x, y);
		}
		else
		{
			if (Map[x,y] != null)
			{
				Destroy (Map[x,y]);
				Map[x,y] = null;
			}
		}
	}

	void CampaignController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
	{
		if (e.PropertyName.Equals("Campaign"))
		{
			Campaign = CampaignController.Campaign;
			Debug.Log ("MapGridCampaignChange");
		}
	}
	 
	// Use this for initialization
	void Start () {
		Debug.Log ("Started MapGrid");
		Campaign = CampaignController.Campaign;
		CampaignController.PropertyChanged += CampaignController_PropertyChanged;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
