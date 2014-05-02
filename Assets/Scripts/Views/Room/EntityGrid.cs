using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;
using System.Collections.Generic;

public class EntityGrid : MonoBehaviour {

    public GameObject EntityMarker;
    public CampaignController CampaignController;
    public GameStatePanel GameStatePanel;
    public EntityMarker MouseOverEntityMarker;
    public MapGrid MapGrid;

    Campaign campaign;
    public Campaign Campaign
    {
        get
        {
            return campaign;
        }
        set
        {
            if (campaign != null)
                campaign.PropertyChanged -= campaign_PropertyChanged;
            campaign = value;
            if (campaign != null)
            {
                campaign.PropertyChanged += campaign_PropertyChanged;
                Load();
            }
        }
    }

    Dictionary<Entity, EntityMarker> EntityMarkers = new Dictionary<Entity, EntityMarker>();

    void campaign_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) // TODO improve efficiency
    {
        if (e.PropertyName.StartsWith("Characters") || e.PropertyName.StartsWith("Entities"))
        {
            Load();
        }
    }

    void CampaignController_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName.Equals("Campaign"))
        {
            Campaign = CampaignController.Campaign;
        }
    }

    void Load() // TODO: Remove old entries
    {
        List<Entity> entities = Campaign.Entities;
        List<Entity> characters = Campaign.Characters;
        foreach (Entity entity in entities)
        {
            if (!EntityMarkers.ContainsKey(entity))
            {
                EntityMarker marker = (Instantiate(EntityMarker, MapGrid.GetPosition(entity.Position), Quaternion.identity) as GameObject).GetComponent("EntityMarker") as EntityMarker;
                marker.Entity = entity;
                marker.transform.parent = transform;
                EntityMarkers[entity] = marker;
            }
        }
        foreach (Entity entity in characters)
        {
            if (!EntityMarkers.ContainsKey(entity))
            {
                EntityMarker marker = (Instantiate(EntityMarker, MapGrid.GetPosition(entity.Position), Quaternion.identity) as GameObject).GetComponent("EntityMarker") as EntityMarker;
                marker.Entity = entity;
                marker.transform.parent = transform;
                EntityMarkers[entity] = marker;
            }
        }
        List<Entity> marked = new List<Entity>();
        foreach (Entity entity in EntityMarkers.Keys)
        {
            if (!entities.Contains(entity) && !characters.Contains(entity))
            {
                marked.Add(entity);
            }
        }
        foreach (Entity entity in marked)
        {
            Destroy(EntityMarkers[entity].gameObject);
            EntityMarkers.Remove(entity);
        }
    }

	// Use this for initialization
	void Start () {
        Campaign = CampaignController.Campaign;
        CampaignController.PropertyChanged += CampaignController_PropertyChanged;
        MapGrid.PropertyChanged += MapGrid_PropertyChanged;
	}

    void MapGrid_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (MapGrid.SelectedEntity != null)
        {
            MouseOverEntityMarker = EntityMarkers[MapGrid.SelectedEntity];
            if (MapGrid.MouseOverPosition != null && (!CampaignController.IsClient || CampaignController.Username == MapGrid.SelectedEntity.Owner))
                MouseOverEntityMarker.Preview(MapGrid.MouseOverPosition.Value);
        }
    }
	
	// Update is called once per frame
	void Update () {
	    
	}


}
