using UnityEngine;
using System.Collections;

public class SetSize : MonoBehaviour {

    public float XScale;
    public float YScale;
    public bool RescaleUpdates;
    public SpriteRenderer SpriteRenderer;

	// Use this for initialization
	void Start () {
        Rescale();
	}

    void Rescale()
    {
        transform.localScale = new Vector3(XScale / SpriteRenderer.sprite.bounds.size.x, YScale / SpriteRenderer.sprite.bounds.size.y, 1);
    }

	// Update is called once per frame
	void Update () {
	    if (RescaleUpdates)
        {
            Rescale();
        }
	}
}
