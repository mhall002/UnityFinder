using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;
using System.ComponentModel;

public class MapGrid : MonoBehaviour {

    public event PropertyChangedEventHandler PropertyChanged;
    public SessionManager SessionManager;
	public CampaignController CampaignController;
	public GameObject TileGridPrefab;
    public GameObject PingPrefab;
    public Camera RoomCamera;

    private Vector4? mouseOverPosition;
    public Vector4? MouseOverPosition
    {
        get
        {
            return mouseOverPosition;
        }
        private set
        {
            mouseOverPosition = value;
            Entity entity = null;
            if (value != null)
                entity = GetEntity(value.Value);
            if (entity != MouseOverEntity)
                MouseOverEntity = entity;
            OnPropertyChanged("MouseOverPosition");
        }
    }

    private Entity mouseOverEntity;
    public Entity MouseOverEntity
    {
        get
        {
            return mouseOverEntity;
        }
        private set
        {
            mouseOverEntity = value;
            OnPropertyChanged("MouseOverEntity");
        }
    }

    private Entity selectedEntity;
    public Entity SelectedEntity
    {
        get
        {
            return selectedEntity;
        }
        set
        {
            if (selectedEntity != null)
            {
                if (CloneSelected)
                    CampaignController.DeleteEntity(selectedEntity);
                else
                    selectedEntity.Position = selectedEntity.Position;
            }
            selectedEntity = value;
        }
    }

    public 
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

    [RPC]
    public void CreatePing(Vector3 position, NetworkMessageInfo info)
    {
        GameObject go = Instantiate(PingPrefab, position, Quaternion.identity) as GameObject;
        go.transform.parent = transform;
        (go.GetComponent("SpriteRenderer") as SpriteRenderer).color = SessionManager.GetColour(SessionManager.GetPlayerName(info.sender));
    }

    public void SendPing(Vector3 position)
    {
        position.z = 2;
        if (SessionManager.Active)
            networkView.RPC("CreatePing", RPCMode.All, position);
        else
        {
            GameObject go = Instantiate(PingPrefab, position, Quaternion.identity) as GameObject;
            go.transform.parent = transform;
        }
    }

    public Entity GetEntity(Vector4 position)
    {
        foreach (Entity character in campaign.Characters)
        {
            if (character.Position == position)
            {
                return character;
            }
        }

        foreach (Entity entity in campaign.Entities)
        {
            if (entity.Position == position)
            {
                return entity;
            }
        }
        return null;
    }

    public void GotMouseDown(Vector4 position)
    {
        if (SelectedEntity != null)
        {
            if (!CampaignController.IsClient || CampaignController.Username == SelectedEntity.Owner)
            {
                CampaignController.MoveEntity(selectedEntity, position);
            }
            selectedEntity = null;
            CloneSelected = false;
        }
        else
        {
            foreach (Entity character in campaign.Characters)
            {
                if (character.Position == position)
                {
                    SelectedEntity = character;
                }
            }

            foreach (Entity entity in campaign.Entities)
            {
                if (entity.Position == position)
                {
                    SelectedEntity = entity;
                }
            }
        }
        
    }

    public void GotMouseOver(Vector4 position)
    {
        MouseOverPosition = position;
    }

    public void LostMouseOver(Vector4 position)
    {
        if (MouseOverPosition != null && position.Equals(MouseOverPosition.Value))
        {
            MouseOverPosition = null;
        }
    }

    bool CloneSelected = false;
    public void SelectClone(Entity clone)
    {
        Entity entity = CampaignController.CreateClone(clone);
        selectedEntity = entity;
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

	static float spacing = 0.75f;
	public static Vector3 GetPosition(int x, int y)
	{
		return new Vector3 (-Room.Width / 2 + (Room.Width * spacing) * x - 10.2f/2,
		                    -Room.Height / 2 + (Room.Height * spacing) * y - 4.6f, 0);
	}

    public static Vector3 GetPosition(Vector4 position)
    {
        return new Vector3(-Room.Width / 2 + (Room.Width * spacing) * position.x - 10.2f/2 - 9.5f,
                            -Room.Height / 2 + (Room.Height * spacing) * position.y - 4.6f - 4.2f, 0) + new Vector3(spacing * position.z, spacing * position.w);
    }

	public Vector3 GetAbsPosition(int x, int y)
	{
		return new Vector3 (-Room.Width / 2 + (Room.Width * spacing) * x - 10.2f,
		                    -Room.Height / 2 + (Room.Height * spacing) * y - 4.6f, 0);
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

    public void CharacterFocus(Entity entity)
    {
        if (entity.Position.x < 0) // Hasnt been placed yet
        {
            SelectedEntity = entity;
        }
        else
        {
            RoomCamera.transform.position = GetPosition(entity.Position) + new Vector3(0,0,-10);
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
	    if (Input.GetKeyDown("x"))
        {
            if (selectedEntity != null)
            {
                if (Campaign.Characters.Contains(selectedEntity) && !CampaignController.IsClient)
                {
                    CampaignController.DeleteCharacter(selectedEntity);
                }
                if (Campaign.Entities.Contains(selectedEntity))
                {
                    CampaignController.DeleteEntity(selectedEntity);
                }
                SelectedEntity = null;
            }
        }

        if (Input.GetKeyDown("q"))
        {
            SendPing(RoomCamera.ScreenToWorldPoint(Input.mousePosition));
        }
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
