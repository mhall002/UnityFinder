using UnityEngine;
using System.Collections;

public class SetSize : MonoBehaviour {

    public float XSize;
    public float YSize;
    public bool RescaleUpdates;
    public SpriteRenderer SpriteRenderer;

	// Use this for initialization
	void Start () {
        Rescale();
	}

    void Rescale()
    {
        float scale = 1;
        if (XSize < scale * SpriteRenderer.sprite.bounds.size.x)
        {
            scale = XSize / SpriteRenderer.sprite.bounds.size.x;
        }
        if (YSize < scale * SpriteRenderer.sprite.bounds.size.y)
        {
            scale = YSize / SpriteRenderer.sprite.bounds.size.y;
        }
        transform.localScale = new Vector3(scale, scale, 1);
    }

	// Update is called once per frame
	void Update () {
	    if (RescaleUpdates)
        {
            Rescale();
        }
	}
}
