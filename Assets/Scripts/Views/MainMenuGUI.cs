using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour {

    public ViewController Controller;
    public SessionManager SessionManager;
    public CampaignController CampaignController;

	// Use this for initialization
	void Start () {
	
	}

    string Username = "Player";
    string[] Servers = null;
    bool WaitingForServers = false;
    bool CreatingCampaign = false;
    string campaignName = "Name";
    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 100, 120), "Start Menu");

        if (WaitingForServers)
        {
            GUI.enabled = false;
        }
        if (GUI.Button(new Rect(20,40,80,20), "Join"))
        {
            SessionManager.StartServerLookup();
            WaitingForServers = true;
        }
        GUI.enabled = true;

        if (GUI.Button(new Rect(20, 70, 80, 20), "Load"))
        {

        }

        if (GUI.Button(new Rect(20,100, 80, 20), "Create"))
        {
            CreatingCampaign = true;
        }

        if (CreatingCampaign)
        {
            GUI.Box(new Rect(230, 10, 220, 120), "Create Campaign");
            
            campaignName = GUI.TextField(new Rect(240,40,200,30), campaignName);
            if (GUI.Button(new Rect(240,100, 200, 20), "Create"))
            {
                CampaignController.CreateCampaign(campaignName);
                Controller.State = ViewController.ViewState.Campaign;
            }
        }

        if (Servers != null)
        {
            GUI.Box(new Rect(120, 10, 120, 120), "Games");
            GUI.Label(new Rect(130, 30, 50, 20), "Name");
            Username = GUI.TextField(new Rect(120, 180, 50, 20), Username);
            for (int i = 0; i < Servers.Length; i++)
            {
                if (GUI.Button(new Rect(130,70 + i*30,100,20), "Join"))
                {
                    SessionManager.Connect(i, Username);
                    Controller.State = ViewController.ViewState.Campaign;
                }
            }
        }
    }

	// Update is called once per frame
	void Update () {
	    if (WaitingForServers)
        {
            string[] results = SessionManager.CheckLookupResults();
            if (results != null)
            {
                Servers = results;
                WaitingForServers = false;
            }
        }
	}
}
