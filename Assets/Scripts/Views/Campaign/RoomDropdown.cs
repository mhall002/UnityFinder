using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;

public class RoomDropdown : MonoBehaviour {

    public SessionManager SessionManager;
    public CampaignController CampaignController;
    public ViewController ViewController;
	public MapGrid MapGrid;
    public GameObject BoardCamera;

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
            if (!SessionManager.IsClient)
            {
                Room room = CampaignController.Campaign.GetRoom(GridX, GridY);

                GUILayout.BeginArea(Rect);
                if (!Exists)
                {
                    if (GUILayout.Button("Create"))
                    {
                        CampaignController.CreateRoom(GridX, GridY);
                    }
                }
                else
                {
                    if (GUILayout.Button("View/Edit"))
                    {
                        CampaignController.SetRoom(GridX, GridY);
                        Debug.Log("Set room to " + GridX + " : " + GridY);
                        BoardCamera.transform.position = MapGrid.GetAbsPosition(GridX, GridY) + new Vector3(0,0,BoardCamera.transform.position.z);
                        ViewController.State = global::ViewController.ViewState.Room;
                    }
                    if (GUILayout.Button(room.Visible ? "Hide" : "Show"))
                    {
                        CampaignController.SetVisible(GridX, GridY, !room.Visible);
                    }
                    if (GUILayout.Button("Delete"))
                    {
                        CampaignController.DeleteRoom(GridX, GridY);
                    }
                }
                GUILayout.EndArea();
            }

            else if (Exists)
            {
                GUILayout.BeginArea(Rect);
                if (GUILayout.Button("View"))
                {
					CampaignController.SetRoom(GridX, GridY);
					Debug.Log("Set room to " + GridX + " : " + GridY);
                    BoardCamera.transform.position = MapGrid.GetAbsPosition(GridX, GridY) + new Vector3(0, 0, BoardCamera.transform.position.z);
					ViewController.State = global::ViewController.ViewState.Room;
                }
                GUILayout.EndArea();
            }
        }
    }

    bool Exists = false;
    public void ShowMenu(int GridX, int GridY, Vector2 Position)
    {
        Exists = CampaignController.Campaign.GetRoom(GridX, GridY) != null;
        if (!Exists || SessionManager.IsClient)
        {
            Height = 50;
        }
        else
        {
            Height = 80;
        }
        Position.y = Screen.height - Position.y;
        Position.x += 20;
        Debug.Log(Position.y);
        this.GridX = GridX;
        this.GridY = GridY;
        this.Position = Position;
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
