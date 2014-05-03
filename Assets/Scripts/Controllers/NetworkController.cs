using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;
using System;
using System.Collections.Generic;

public class NetworkController : MonoBehaviour {

    public TerrainStorage TerrainStorage;
    public CampaignController CampaignController;
    public RemoteView RemoteView;

    Dictionary<string, Entity> idToEntity = new Dictionary<string, Entity>();

    private Campaign campaign;
    public Campaign Campaign
    {
        get { return campaign; }
        set
        {
            campaign = value;
            CampaignController.Campaign = campaign;
        }
    }

    public void MoveCharacter(Entity entity, Vector4 position)
    {
        Debug.Log("Moving character over network...");
        string uid = entity.Uid.ToString();
        RemoteView.networkView.RPC("MoveCharacter", RPCMode.Server, uid, position.x, position.y, position.z, position.w);
    }

    public void CreateClientCharacter(string name, string image)
    {
        RemoteView.networkView.RPC("CreateClientCharacter", RPCMode.Server, name, image);
    }

    [RPC]
    void DeleteCampaign()
    {
        Debug.Log("Got campaign");
        Campaign = null;
    }


    [RPC]
    void CreateCampaign(string name)
    {
        Debug.Log("Got Campaign " + name);
        Campaign = new Campaign();
        Campaign.Name = name;
    }

    [RPC]
    void CreateCharacter(string uid, string name, string description, string image, string owner, float roomx, float roomy, float tilex, float tiley)
    {
        Debug.Log("Creating character from server");
        bool old = true;
        if (!idToEntity.ContainsKey(uid))
        {
            idToEntity.Add(uid, new Entity(new Guid(uid)));
            old = false;
        }
        Entity entity = idToEntity[uid];
        entity.Name = name;
        entity.Description = description;
        entity.Image = image;
        entity.Owner = owner;
        entity.Type = EntityType.PlayerCharacter;
        entity.Position = new Vector4(roomx, roomy, tilex, tiley);
        if (!old)
        {
            Campaign.Characters.Add(entity);
            Campaign.CharacterChanged(entity.Uid.ToString());
        }
    }

    [RPC]
    void CreateEntity(string uid, string name, string description, string image, int width, int height, string owner, float roomx, float roomy, float tilex, float tiley)
    {
        bool old = true;
        if (!idToEntity.ContainsKey(uid))
        {
            idToEntity.Add(uid, new Entity(new Guid(uid)));
            old = false;
        }
        Entity entity = idToEntity[uid];
        entity.Name = name;
        entity.Description = description;
        entity.Image = image;
        entity.Owner = owner;
        entity.Width = width;
        entity.Height = height;
        entity.Type = EntityType.NonPlayerCharacter;
        entity.Position = new Vector4(roomx, roomy, tilex, tiley);
        if (!old)
        {
            Campaign.Entities.Add(entity);
            Campaign.EntityChanged(entity.Uid.ToString());
        }
    }

    [RPC]
    void DeleteCharacter(string uid)
    {
        Guid guid = new Guid(uid);
        Entity toDelete = null;
        foreach (Entity character in Campaign.Characters)
        {
            if (guid == character.Uid)
            {
                toDelete = character;
                break;
            }
        }
        if (toDelete != null)
        {
            CampaignController.DeleteCharacter(toDelete);
        }
    }

    [RPC]
    void DeleteEntity(string uid)
    {
        Guid guid = new Guid(uid);
        Entity toDelete = null;
        foreach (Entity entity in Campaign.Entities)
        {
            if (guid == entity.Uid)
            {
                toDelete = entity;
                break;
            }
        }
        if (toDelete != null)
        {
            CampaignController.DeleteEntity(toDelete);
        }
    }

    [RPC]
    void MoveEntity(string uid, float roomx, float roomy, float tilex, float tiley)
    {
        Debug.Log("Moving character from server");
        Entity entity = idToEntity[uid];
        entity.Name = name;
        entity.Position = new Vector4(roomx, roomy, tilex, tiley);
    }

    [RPC]
    void CreateRoom(int x, int y, string terrain)
    {
        Room room = new Room();
        room.X = x;
        room.Y = y;
        SetTerrain(room, terrain);
        Campaign.SetRoom(x, y, room);
        Debug.Log("Received Room");
    }

    [RPC]
    void DeleteRoom(int x, int y)
    {
        Campaign.SetRoom(x, y, null);
    }

    [RPC]
    void AddLink(int x1, int y1, int x2, int y2)
    {
        Campaign.AddLink(new Pair<int, int>(x1, y1), new Pair<int, int>(x2, y2));
    }

    [RPC]
    void RemoveLink(int x1, int y1, int x2, int y2)
    {
        Campaign.RemoveLink(new Pair<int, int>(x1, y1), new Pair<int, int>(x2, y2));
    }

    [RPC]
    void SetTerrain(int roomx, int roomy, int x, int y, string terrainCode)
    {
        Campaign.GetRoom(roomx, roomy).SetTerrain(x, y, TerrainStorage.GetTerrain(terrainCode[0]));
    }

    [RPC]
    void SetRoomXPWorth(int x, int y, int xp)
    {
        Campaign.GetRoom(x, y).XPWorth = xp;
    }

    [RPC]
    void ClearTerrain()
    {
        // nope
    }

    [RPC]
    void AddTerrain(string name, string texturePath, string code)
    {
        Ground terrain = new Ground()
        {
            Name = name,
            CharacterCode = code[0],
            TextureFile = texturePath
        };
        TerrainStorage.AddTerrain(terrain);
    }

    [RPC]
    void TerrainComplete()
    {
        // nope
    }
	// Use this for initialization
	void Start () {
        gameObject.SetActive(false);
	}

    public void SetTerrain(Room room, string terrainString)
    {
        for (int x = 0; x < Room.Width; x++)
        {
            for (int y = 0; y < Room.Height; y++)
            {
                room.SetTerrain(x, y, TerrainStorage.GetTerrain(terrainString[x * Room.Height + y]));
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
