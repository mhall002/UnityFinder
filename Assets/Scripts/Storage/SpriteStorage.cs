using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteStorage : MonoBehaviour {

    public Dictionary<string, Sprite> Sprites = new Dictionary<string,Sprite>();
    public Dictionary<string, Texture> Textures = new Dictionary<string, Texture>();
    public static string[] CharacterSprites = 
    {
        "Characters/knight",
        "Characters/MaleElfRogue",
    };
    public static string[] NPCSprites = 
    {
        "Creatures/GoblinMage",
        "Creatures/GoblinWarrior",
        "Creatures/GoblinWarrior2",
    };
	// Use this for initialization
	void Start () {
	
	}
	
    public Sprite GetSprite(string path)
    {
        if (!Sprites.ContainsKey(path))
        {
            var obj = Resources.Load<Sprite>("Sprites/" + path);
            Sprites[path] = Resources.Load<Sprite>("Sprites/" + path);
        }
        return Sprites[path];
    }

    public Texture GetTexture(string path)
    {
        if (!Textures.ContainsKey(path))
        {
            var obj = Resources.Load<Texture>("Sprites/" + path);
            Textures[path] = Resources.Load<Texture>("Sprites/" + path);
        }
        return Textures[path];
    }

	// Update is called once per frame
	void Update () {
	
	}
}
