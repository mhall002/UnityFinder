﻿using UnityEngine;
using System.Collections;

public class ViewController : MonoBehaviour {

    public GameObject CampaignView;
    public GameObject MenuView;
    public GameObject RoomView;
    public GameObject ServerRoomView;
    public GameObject ServerCampaignView;
    public SessionManager SessionManager;
    public GameObject CampaignCamera;
    public GameObject BoardCamera;

    public enum ViewState
    {
        Menu,
        Campaign,
        Room
    }

    public ViewState state;
    public ViewState State
    {
        get
        {
            return state;
        }
        set
        {
            state = value;
            ChangeState();
        }
    }

    void ChangeState()
    {
        CampaignView.SetActive(false);
        MenuView.SetActive(false);
        RoomView.SetActive(false);
        
	    if (State == ViewState.Campaign)
	    {
	        CampaignView.SetActive(true);
            CampaignCamera.SetActive(true);
            BoardCamera.SetActive(false);
	    }
	    if (State == ViewState.Room)
	    {
	        RoomView.SetActive(true);
            CampaignCamera.SetActive(false);
            BoardCamera.SetActive(true);
	    }
        
		if (SessionManager.IsClient)
		{
			ServerRoomView.SetActive(false);
			ServerCampaignView.SetActive(false);
		}

        if (State == ViewState.Menu)
        {
            MenuView.SetActive(true);
        }

    }

	// Use this for initialization
	void Start () {
        ChangeState();
	}
}
