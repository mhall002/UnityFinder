using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;

public class RoomConnector : MonoBehaviour {

    public CampaignController CampaignController;
    public SpriteStorage SpriteStorage;
    public Pair<int, int> Room1;
    public Pair<int, int> Room2;
    private Sprite NotEnabledSprite;
    private Sprite EnabledSprite;
    private bool enabled;
    
    public bool Enabled
    {
        get
        {
            return enabled;
        }
        set
        {
            enabled = value;
            (GetComponent("SpriteRenderer") as SpriteRenderer).sprite = enabled ? EnabledSprite : NotEnabledSprite;
        }
    }
    bool Preview = false;
    bool Vertical = false;

	// Use this for initialization
	void Start () {

        CampaignController = (CampaignController)GameObject.Find("CampaignController").GetComponent("CampaignController");
        
	}

    public void Setup(int x1, int y1, int x2, int y2)
    {
        Room1 = new Pair<int, int>(x1, y1);
        Room2 = new Pair<int, int>(x2, y2);

        Vertical = Room1.Second != Room2.Second;

        if (SpriteStorage == null)
        {
            SpriteStorage = (SpriteStorage)GameObject.Find("SpriteStorage").GetComponent("SpriteStorage");
            if (Vertical)
            {
                NotEnabledSprite = SpriteStorage.GetSprite("BlankConnectorVertical");
                EnabledSprite = SpriteStorage.GetSprite("FilledConnectorVertical");
            }
            else
            {
                NotEnabledSprite = SpriteStorage.GetSprite("BlankConnectorHorizontal");
                EnabledSprite = SpriteStorage.GetSprite("FilledConnectorHorizontal");
            }
        }
    }
	
    void OnMouseEnter()
    {
        if (!Enabled && !Preview)
        {
            Preview = true;
            (GetComponent("SpriteRenderer") as SpriteRenderer).sprite = EnabledSprite;
        }
    }

    void OnMouseExit()
    {
        if (!Enabled && Preview)
        {
            (GetComponent("SpriteRenderer") as SpriteRenderer).sprite = NotEnabledSprite;
        }
        Preview = false;
    }

    void OnMouseDown()
    {
        Debug.Log("Campaign controller " + Room1.First);
        if (Enabled)
            CampaignController.RemoveLink(Room1, Room2);
        else
            CampaignController.CreateLink(Room1, Room2);
    }

	// Update is called once per frame
	void Update () {
	
	}


}
