using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;

public class EntityMarker : MonoBehaviour {

    MapGrid MapGrid;

    private Entity entity;
    public Entity Entity
    {
        get
        {
            return entity;
        }
        set
        {
            entity = value;
            entity.PropertyChanged += entity_PropertyChanged;
            SetImage();
        }
    }

    private void entity_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        SetImage();
    }

    public CampaignController CampaignController;
    SpriteRenderer Renderer;
    SpriteStorage SpriteStorage;

    void SetImage()
    {
        if (SpriteStorage == null)
        {
            SpriteStorage = (SpriteStorage)GameObject.Find("SpriteStorage").GetComponent("SpriteStorage");
        }
        if (Renderer == null)
            Renderer = (GetComponent("SpriteRenderer") as SpriteRenderer);
        Renderer.sprite = SpriteStorage.GetSprite(Entity.Image);
        transform.localScale = new Vector3(0.75f / Renderer.sprite.bounds.size.x, 0.75f / Renderer.sprite.bounds.size.y, 1);
        //Debug.Log(Entity.Position.z);
        transform.position = MapGrid.GetPosition(entity.Position);
        Debug.Log(transform.position.x);
    }

    public void Preview(Vector4 position)
    {
        transform.position = MapGrid.GetPosition(position);
        Debug.Log("Previewing");
    }

	// Use this for initialization
	void Start () {
        CampaignController = (CampaignController)GameObject.Find("CampaignController").GetComponent("CampaignController");
        if (Renderer == null)
            Renderer = (GetComponent("SpriteRenderer") as SpriteRenderer);
        MapGrid = (MapGrid)GameObject.Find("MapGrid").GetComponent("MapGrid");
	}

	// Update is called once per frame
	void Update () {
	
	}
}
