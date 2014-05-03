using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;

public class EntityMarker : MonoBehaviour {

    MapGrid MapGrid;
    public float SelectionAlpha;

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

    private Color backColour;
    public Color BackColour
    {
        get
        {
            return backColour;
        }
        set
        {
            backColour = value;
            backColour.a = SelectionAlpha;
            OwnerRenderer.color = backColour;
        }
    }

    private void entity_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        SetImage();
    }

    public CampaignController CampaignController;
    public SpriteRenderer Renderer;
    public SpriteRenderer OwnerRenderer;
    SpriteStorage SpriteStorage;

    void SetImage()
    {
        if (SpriteStorage == null)
        {
            SpriteStorage = (SpriteStorage)GameObject.Find("SpriteStorage").GetComponent("SpriteStorage");
        }
        Renderer.sprite = SpriteStorage.GetSprite(Entity.Image);
        //Debug.Log(Entity.Position.z);
        transform.position = MapGrid.GetPosition(entity.Position);
        Debug.Log(transform.position.x);
    }

    public void Preview(Vector4 position)
    {
        transform.position = MapGrid.GetPosition(position);
    }

	// Use this for initialization
	void Start () {
        CampaignController = (CampaignController)GameObject.Find("CampaignController").GetComponent("CampaignController");
        MapGrid = (MapGrid)GameObject.Find("MapGrid").GetComponent("MapGrid");
	}

	// Update is called once per frame
	void Update () {
	
	}
}
