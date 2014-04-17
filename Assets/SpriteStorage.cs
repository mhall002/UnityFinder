using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteStorage : MonoBehaviour {

    public Dictionary<string, Sprite> Sprites = new Dictionary<string,Sprite>();
    public Dictionary<string, Texture> Textures = new Dictionary<string, Texture>();

	// Use this for initialization
	void Start () {
	
	}
	
    public Sprite GetSprite(string path)
    {
        if (!Sprites.ContainsKey(path))
        {
            var obj = Resources.Load<Sprite>("Sprites/" + path);
            Debug.Log(obj == null);
            Sprites[path] = Resources.Load<Sprite>("Sprites/" + path);
            Debug.Log(path);
            Debug.Log(Sprites[path] == null);
        }
        return Sprites[path];
    }

    public Texture GetTexture(string path)
    {
        if (!Textures.ContainsKey(path))
        {
            var obj = Resources.Load<Texture>("Sprites/" + path);
            Debug.Log(obj == null);
            Textures[path] = Resources.Load<Texture>("Sprites/" + path);
            Debug.Log(path);
            Debug.Log(Sprites[path] == null);
        }
        return Textures[path];
    }

	// Update is called once per frame
	void Update () {
	
	}
}
