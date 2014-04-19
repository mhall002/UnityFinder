﻿using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;

public class TileGrid : MonoBehaviour {

    private Room room;
    public Room Room
    {
        get
        {
            return room;
        }
        set
        {
            if (room != null)
                room.PropertyChanged -= room_PropertyChanged;
            room = value;
            if (room != null)
                room.PropertyChanged += room_PropertyChanged;
            LoadLevel();
        }
    }

    
    public RoomController RoomController;
    public GameObject Tile;
    public Tile[,] Tiles;

	// Use this for initialization
	void Start () {
        RoomController.PropertyChanged += RoomController_PropertyChanged;
        Room = RoomController.Room;
        Debug.Log("Started --------------");
        LoadLevel();
	}

    void room_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) // TODO improve efficiency
    {
        LoadLevel();
    }

    void RoomController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        Debug.Log(e.PropertyName);
        if (e.PropertyName == "Room")
        {
            Room = RoomController.Room;
        }
    }

    public void LoadLevel()
    {
        if (Room != null)
        {
            if (Tiles == null)
            {
                Tiles = new Tile[Room.Height, Room.Width];
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
                        go.Terrain = Room.GetTerrain(gridx, gridy);
                        go.X = gridx;
                        go.Y = gridy;
                        Tiles[gridy, gridx] = go;
                    }
                }
            }
            else
            {
                for (int gridx = 0; gridx < Room.Width; gridx += 1)
                {
                    for (int gridy = 0; gridy < Room.Height; gridy += 1)
                    {
                        Tile go = Tiles[gridy, gridx];
                        go.Terrain = Room.GetTerrain(gridx, gridy);
                    }
                }
            }
        }
        else
        {
            Debug.Log("Null Room");
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}