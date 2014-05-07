using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteStorage : MonoBehaviour {

    public string CharacterSpriteDirectory;
    public string EntitySpriteDirectory;
    public string TileSpriteDirectory;

    public List<string> SpriteDirectories;
    public List<string> TextureDirectories;

    public Dictionary<string, Sprite> Sprites = new Dictionary<string,Sprite>();
    public Dictionary<string, Texture> Textures = new Dictionary<string, Texture>();

    public List<string> CharacterSprites;
    public List<string> NPCSprites;
    public List<string> TileSprites;

	// Use this for initialization
	void Start () {
        Load();
	}

    void Load()
    {
        CharacterSprites = new List<string>();
        Sprite[] sprites = Resources.LoadAll<Sprite>(CharacterSpriteDirectory);
        foreach (Sprite sprite in sprites)
        {
            CharacterSprites.Add(sprite.name);
            Sprites.Add(sprite.name, sprite);
            Debug.Log("Character sprite " + sprite.name);
        }
        sprites = Resources.LoadAll<Sprite>(EntitySpriteDirectory);
        foreach (Sprite sprite in sprites)
        {
            NPCSprites.Add(sprite.name);
            Sprites.Add(sprite.name, sprite);
        }
        sprites = Resources.LoadAll<Sprite>(TileSpriteDirectory);
        foreach (Sprite sprite in sprites)
        {
            TileSprites.Add(sprite.name);
            Sprites.Add(sprite.name, sprite);
        }

        foreach (string directory in SpriteDirectories)
        {
            sprites = Resources.LoadAll<Sprite>(directory);
            foreach (Sprite sprite in sprites)
            {
                Sprites.Add(sprite.name, sprite);
            }
        }

        foreach (string directory in TextureDirectories)
        {
            Texture[] textures = Resources.LoadAll<Texture>(directory);
            foreach (Texture texture in textures)
            {
                Textures.Add(texture.name, texture);
                Debug.Log("Loaded " + texture.name);
            }
        }
    }
	
    public Sprite GetSprite(string name)
    {
        return Sprites[name];
    }

    public Texture GetTexture(string name)
    {
        if (Textures.ContainsKey(name))
            return Textures[name];
        else
            Debug.LogError("Could not find texture " + name);
        return null;
    }

        /*

    public Sprite GetSprite(string path)
    {
        if (!Sprites.ContainsKey(path))
        {
            var obj = Resources.Load<Sprite>("Sprites/" + path);
            Sprites[path] = Resources.Load<Sprite>("Sprites/" + path);
        }
        Debug.Log(Sprites[path].name);
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
         * 
         * */

	// Update is called once per frame
	void Update () {
	
	}
}
