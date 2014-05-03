using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;
using System.Collections.Generic;
using System;

public class EntityStorage : MonoBehaviour
{
    public SessionManager SessionManager;

    Dictionary<Guid, Entity> Entities = new Dictionary<Guid, Entity>();

    bool Loaded = false;

    void LoadEntities()
    {
        if (!SessionManager.IsClient)
        {
            // Load from db

            Entity entity = new Entity()
            {
                Name = "Knight",
                Description = "Bestiary 1 - Page 67",
                Image = "Characters/knight"
            };
            Entities.Add(entity.Uid, entity);

            entity = new Entity()
            {
                Name = "Goblin Warrior 1",
                Description = "Bestiary 1 - Page 67",
                Image = "Creatures/GoblinWarrior"
            };
            Entities.Add(entity.Uid, entity);

            entity = new Entity()
            {
                Name = "Goblin Warrior 2",
                Description = "Bestiary 1 - Page 67",
                Image = "Creatures/GoblinWarrior2"
            };
            Entities.Add(entity.Uid, entity);

            entity = new Entity()
            {
                Name = "Goblin Mage",
                Description = "Bestiary 1 - Page 67",
                Image = "Creatures/GoblinMage"
            };
            Entities.Add(entity.Uid, entity);
        }

        Loaded = true;
    }

    public void AddEntity(Entity entity) // TODO: save to db
    {
        Entities.Add(entity.Uid, entity);
        Loaded = true;
    }

    public ICollection<Entity> GetEntities()
    {
        if (!Loaded)
        {
            LoadEntities();
        }
        return Entities.Values;
    }
}
