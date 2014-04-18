using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;

public class BlankRoomScript : MonoBehaviour {

    private Room room;
    public SpriteStorage SpriteStorage;

    public Room Room
    {
        get { return room; }
        set
        {
            room = value;
            RoomChanged();
        }
    }
    public SquareMenuScript Menu;
    public int GridX;
    public int GridY;

	// Use this for initialization
	void Start () {
        Menu = (SquareMenuScript) GameObject.Find("SquareMenu").GetComponent("SquareMenuScript");
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
            (GetComponent("SpriteRenderer") as SpriteRenderer).sprite = SpriteStorage.GetSprite("Campaign/PopulatedRoom");
        }
        else
        {
            (GetComponent("SpriteRenderer") as SpriteRenderer).sprite = SpriteStorage.GetSprite("Campaign/BlankRoom");
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
