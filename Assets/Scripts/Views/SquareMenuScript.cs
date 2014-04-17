using UnityEngine;
using System.Collections;

public class SquareMenuScript : MonoBehaviour {

    public CampaignController CampaignController;
    public ViewController ViewController;
    public bool Displayed = false;
    public Vector2 Position;
    public Rect Rect;
    public int GridX = 0;
    public int GridY = 0;
    int Width = 100;
    int Height = 50;

	// Use this for initialization
	void Start () {
	    
	}
	
    void OnGUI()
    {
        if (Displayed)
        {
            GUI.Box(Rect, "");
            if (!Exists)
            {
                if (GUI.Button(new Rect(Position.x + 10, Position.y + 10, Width - 20, 30), "Create"))
                {
                    CampaignController.CreateRoom(GridX, GridY);
                }
            }
            else
            {
                if (GUI.Button(new Rect(Position.x + 10, Position.y + 10, Width - 20, 30), "View/Edit"))
                {
                    CampaignController.SetRoom(GridX, GridY);
                    ViewController.State = global::ViewController.ViewState.Room;
                }
                if (GUI.Button(new Rect(Position.x + 10, Position.y + 50, Width - 20, 30), "Delete"))
                {
                    CampaignController.DeleteRoom(GridX, GridY);
                }
            }
        }
    }

    bool Exists = false;
    public void ShowMenu(int GridX, int GridY, Vector2 Position)
    {
        Exists = CampaignController.Campaign.Rooms[GridY, GridX] != null;
        if (!Exists)
        {
            Height = 50;
        }
        else
        {
            Height = 80;
        }
        Debug.Log(Position.y);
        Position.y = Screen.height - Position.y;
        Position.x += 20;
        Debug.Log(Position.y);
        this.GridX = GridX;
        this.GridY = GridY;
        this.Position = Position;
        Debug.Log(this.Position.x);
        Displayed = true;
        MouseDown = true;
        Rect = new Rect(this.Position.x, this.Position.y, Width, Height);
        Releases = 0;
    }

    int Releases = 0;
    bool MouseDown = false;
	// Update is called once per frame
	void Update () {
        if (Displayed)
        {
            if (Input.GetMouseButtonUp(0))
            {
                //Debug.Log("Releases: " + Releases);
                Releases++;
                if (Releases >= 2 && !Rect.Contains(Input.mousePosition))
                {
                    Displayed = false;
                }
            }
        }
	}
}
