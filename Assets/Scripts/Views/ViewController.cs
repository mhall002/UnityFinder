using UnityEngine;
using System.Collections;

public class ViewController : MonoBehaviour {

    public GameObject CampaignView;
    public GameObject MenuView;
    public GameObject RoomView;
    public GameObject ClientRoomView;
    public GameObject ClientCampaignView;
    public SessionManager SessionManager;

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
        ClientCampaignView.SetActive(false);
        ClientRoomView.SetActive(false);
        if (SessionManager.IsClient)
        {
            if (State == ViewState.Campaign)
            {
                ClientCampaignView.SetActive(true);
            }
            if (State == ViewState.Room)
            {
                ClientRoomView.SetActive(true);
            }
        }
        else
        {
            if (State == ViewState.Campaign)
            {
                CampaignView.SetActive(true);
            }
            if (State == ViewState.Room)
            {
                RoomView.SetActive(true);
            }
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
