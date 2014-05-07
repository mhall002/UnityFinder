using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;
using System.Collections.Generic;
using System;

public class EntityStorage : MonoBehaviour
{
    public SessionManager SessionManager;
    public SpriteStorage SpriteStorage;

    Dictionary<Guid, Entity> Entities = new Dictionary<Guid, Entity>();
    Dictionary<string, Entity> EntityByImage = new Dictionary<string, Entity>();

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
                Image = "knight"
            };
            AddEntity(entity);

            entity = new Entity()
            {
                Name = "Goblin Warrior 1",
                Description = "Bestiary 1 - Page 67",
                Image = "GoblinWarrior"
            };
            AddEntity(entity);

            entity = new Entity()
            {
                Name = "Goblin Warrior 2",
                Description = "Bestiary 1 - Page 67",
                Image = "GoblinWarrior2"
            };
            AddEntity(entity);

            entity = new Entity()
            {
                Name = "Goblin Mage",
                Description = "Bestiary 1 - Page 67",
                Image = "GoblinMage"
            };
            AddEntity(entity);

            foreach (string npcSpriteName in SpriteStorage.NPCSprites)
            {
                if (!EntityByImage.ContainsKey(npcSpriteName))
                {
                    AddEntity(new Entity()
                        {
                            Name = npcSpriteName,
                            Description = "",
                            Image = npcSpriteName
                        });
                }
            }
        }

        Loaded = true;
    }



    public void AddEntity(Entity entity) // TODO: save to db
    {
        Entities.Add(entity.Uid, entity);
        EntityByImage.Add(entity.Image, entity);
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
