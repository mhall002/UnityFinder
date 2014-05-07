using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;

public class RoomIcon : MonoBehaviour {

    private Room room;
    public SpriteStorage SpriteStorage;
    public SessionManager SessionManager;

    public Room Room
    {
        get { return room; }
        set
        {
            if (room != null)
                room.PropertyChanged -= room_PropertyChanged;
            room = value;
            if (room != null)
                room.PropertyChanged += room_PropertyChanged;
            RoomChanged();
        }
    }

    void room_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        RoomChanged();
    }

    public RoomDropdown Menu;
    public int GridX;
    public int GridY;

	// Use this for initialization
	void Start () {
        Menu = (RoomDropdown) GameObject.Find("SquareMenu").GetComponent("RoomDropdown");
        SessionManager = GameObject.Find("SessionManager").GetComponent("SessionManager") as SessionManager;
	}

    void OnMouseDown() {
        Vector2 position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Menu.ShowMenu(GridX, GridY, position);
    }

    void RoomChanged()
    {
        
        if (SpriteStorage == null)
        {
            SpriteStorage = (SpriteStorage)GameObject.Find("SpriteStorage").GetComponent("SpriteStorage");
        }
        if (room != null)
        {
            (GetComponent("SpriteRenderer") as SpriteRenderer).sprite = SpriteStorage.GetSprite("PopulatedRoom");
        }
        else
        {
            (GetComponent("SpriteRenderer") as SpriteRenderer).sprite = SpriteStorage.GetSprite("BlankRoom");
        }
        if (room == null || SessionManager.IsClient)
        {
            transform.Find("InvisibleRoom").renderer.enabled = false;
            transform.Find("VisibleRoom").renderer.enabled = false;
        }
        else if (room.Visible)
        {
            transform.Find("InvisibleRoom").renderer.enabled = false;
            transform.Find("VisibleRoom").renderer.enabled = true;
        }
        else
        {
            transform.Find("InvisibleRoom").renderer.enabled = true;
            transform.Find("VisibleRoom").renderer.enabled = false;
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
