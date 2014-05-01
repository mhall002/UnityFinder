﻿using UnityEngine;
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
                campaign.PropertyChanged += campaign_PropertyChanged;
        }
    }

    Dictionary<Entity, EntityMarker> EntityMarkers = new Dictionary<Entity, EntityMarker>();

    void campaign_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) // TODO improve efficiency
    {
        if (e.PropertyName == "Characters" || e.PropertyName == "Entities")
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
                marker.transform.parent = transform;
                EntityMarkers[entity] = marker;
            }
        }
        foreach (Entity entity in characters)
        {
            if (!EntityMarkers.ContainsKey(entity))
            {
                EntityMarker marker = (Instantiate(EntityMarker, MapGrid.GetPosition(entity.Position), Quaternion.identity) as GameObject).GetComponent("EntityMarker") as EntityMarker;
                marker.transform.parent = transform;
                EntityMarkers[entity] = marker;
            }
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
        if (GameStatePanel.SelectedEntity != null)
        {
            if (MouseOverEntityMarker.Entity != GameStatePanel.SelectedEntity)
                MouseOverEntityMarker.Entity = GameStatePanel.SelectedEntity;
            if (MapGrid.MouseOverPosition != null)
                MouseOverEntityMarker.Entity.Position = MapGrid.MouseOverPosition.Value;
            Debug.Log("Moving selected entity");
        }
    }
	
	// Update is called once per frame
	void Update () {
	    
	}


}
