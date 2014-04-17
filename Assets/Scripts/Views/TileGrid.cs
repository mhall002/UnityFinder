using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;

public class TileGrid : MonoBehaviour {

    public Room Room;
    public RoomController RoomController;
    public GameObject Tile;

	// Use this for initialization
	void Start () {
        RoomController.PropertyChanged += RoomController_PropertyChanged;
        Debug.Log("Started --------------");
        LoadLevel();
	}

    void RoomController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        Debug.Log(e.PropertyName);
        if (e.PropertyName == "Room")
        {
            LoadLevel();
        }
    }

    public void LoadLevel()
    {
        Debug.Log("Loading level");
        Room = RoomController.Room;
        if (Room != null)
        {
            float x = -10.2f;
            float y = -4.6f;
            for (int gridx = 0; gridx < Room.Width; gridx += 1)
            {
                x = -9.5f + gridx * 0.75f;
                for (int gridy = 0; gridy < Room.Height; gridy += 1)
                {
                    y = -4.2f + gridy * 0.75f;
                    Tile go = (Instantiate(Tile, new Vector3(x, y, 0), Quaternion.identity) as GameObject).GetComponent("Tile") as Tile;
                    go.transform.parent = this.transform;
                    go.Terrain = Room.TerrainGrid[gridy, gridx];
                }
            }
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
