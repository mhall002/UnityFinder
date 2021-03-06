﻿using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;
using System.ComponentModel;
using System;
using Assets.Scripts.Storage;

public class CampaignController : MonoBehaviour, INotifyPropertyChanged {

    public TerrainStorage TerrainStorage;
    public SessionManager SessionManager;
    public NetworkController NetworkController;
    public bool IsClient { get { return SessionManager.IsClient; } }
    public string Username { get { return SessionManager.UserName; } }

    private Campaign campaign;
    public Campaign Campaign
    {
        get
        {
            return campaign;
        }
        set
        {
            campaign = value;
            OnPropertyChanged("Campaign");
        }
    }

    private Ground NormalTerrain;
    private Ground BlockedTerrain;

    public event PropertyChangedEventHandler PropertyChanged;

	// Use this for initialization
	void Start () {
	
	}
	
    public void CreateCampaign (string name)
    {
        Campaign = new Campaign();
        Campaign.Name = name;
    }

    public void AddCharacter(Entity character)
    {
        if (!IsClient)
        {
            Debug.Log("Adding character");
            character.Type = EntityType.PlayerCharacter;
            Campaign.Characters.Add(character);
            Campaign.CharacterChanged(character.Uid.ToString());
        }
        else
        {
            NetworkController.CreateClientCharacter(character.Name, character.Image);
        }
    }

    public Entity CreateClone(Entity entity)
    {
        Entity clone = Entity.Clone(entity);
        clone.Type = EntityType.NonPlayerCharacter;
        Campaign.Entities.Add(clone);
        Campaign.EntityChanged(clone.Uid.ToString());
        return clone;
    }

    public void DeleteEntity(Entity entity)
    {
        string uid = entity.Uid.ToString();
        Campaign.Entities.Remove(entity);
        Debug.Log("Deleting " + Campaign.Entities.Count);
        Campaign.EntityChanged(uid);
    }


    public void SaveCampaignToDatabase()
    {
        CampaignStorage storage = new CampaignStorage();
        storage.SaveCampaign(Campaign);
    }

    public void DeleteCharacter(Entity character)
    {
        string uid = character.Uid.ToString();
        Campaign.Characters.Remove(character);
        Campaign.CharacterChanged(uid);
    }

    
    public Entity GetEntity(string uid)
    {
        Guid guid = new Guid(uid);
        foreach (Entity entity in Campaign.Entities)
        {
            if (entity.Uid == guid)
            {
                return entity;
            }
        }
        return null;
    }

    public Entity GetCharacter(string uid)
    {
        Guid guid = new Guid(uid);
        foreach (Entity entity in Campaign.Characters)
        {
            if (entity.Uid == guid)
            {
                return entity;
            }
        }
        return null;
    }

    public Entity GetTempCopy(Entity entity)
    {
        Entity clone = Entity.Clone(entity);
        clone.Uid = entity.Uid;
        return clone;
    }

    public void SaveTempEntity(Entity entity)
    {
        Entity original = GetEntity(entity.Uid.ToString());
        if (original == null)
            original = GetCharacter(entity.Uid.ToString());
        original.SaveAs(entity);
    }

    public Room InitializeRoom()
    {
        Room room = new Room();
        if (NormalTerrain == null)
        {
            NormalTerrain = TerrainStorage.GetTerrain("NormalTile");
        }
        if (BlockedTerrain == null)
        {
            BlockedTerrain = TerrainStorage.GetTerrain("InaccessibleTile");
        }

        for (int y = 0; y < Room.Height; y++)
        {
            for (int x = 0; x < Room.Width; x++)
            {
                if ( true || (y >= Room.Height - 2 || y <= 1) && Math.Abs(x - Room.Width / 2) > 1)
                {
                    room.SetTerrain(x,y,BlockedTerrain);
                }
                else if ((x >= Room.Width - 2 || x <= 1) && Math.Abs(y - Room.Height / 2) > 1)
                {
                    room.SetTerrain(x,y,BlockedTerrain);
                }
                else
                {
                    room.SetTerrain(x,y,NormalTerrain);
                }
            }
        }
        return room;
    }

    public void CreateRoom (int gridX, int gridY)
    {
        Room room = InitializeRoom();
        room.X = gridX;
        room.Y = gridY;
        Campaign.SetRoom(gridX, gridY, room);
        Campaign.ActiveRoom = room;
        Debug.Log("Creating new room");
    }

    public void MoveEntity(Entity entity, Vector4 position)
    {
        if (SessionManager.IsClient)
        {
            NetworkController.MoveCharacter(entity, position);
        }
        else
        {
            entity.Position = position;
        }
    }

	public void SetTerrain(Room room, int x, int y, Ground terrain)
	{
		room.SetTerrain(x, y, terrain);
		room.RoomID = 7;
	}

    public void SetRoom(int gridX, int gridY)
    {
        Room room = Campaign.GetRoom(gridX, gridY);
        Campaign.ActiveRoom = room;
    }

    public void DeleteRoom (int gridX, int gridY)
    {
        if (Campaign.ActiveRoom == Campaign.GetRoom(gridX, gridY))
        {
            Campaign.ActiveRoom = null;
        }
        Campaign.SetRoom(gridX, gridY, null);
    }

	// Update is called once per frame
	void Update () {
	
	}

    protected void OnPropertyChanged(string name)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(name));
        }
    }

    public void SetVisible(int x, int y, bool visible)
    {
        Campaign.GetRoom(x, y).Visible = visible;
    }

    public void RemoveLink(Pair<int, int> room1, Pair<int, int> room2)
    {
        Campaign.AddLink(room1, room2);
    }

    public void CreateLink(Pair<int,int> room1, Pair<int,int> room2)
    {
        Campaign.AddLink(room1, room2);
    }

    float spacing = 0.75f;
    public Vector3 GetPosition(int x, int y)
    {
        return new Vector3(-Room.Width / 2 + (Room.Width * spacing) * x - 10.2f,
                            -Room.Height / 2 + (Room.Height * spacing) * y - 4.6f, 0) + gameObject.transform.position;
    }

    public Vector3 GetAbsPosition(int x, int y)
    {
        return new Vector3(-Room.Width / 2 + (Room.Width * spacing) * x - 10.2f,
                            -Room.Height / 2 + (Room.Height * spacing) * y - 4.6f, 0);
    }
}
