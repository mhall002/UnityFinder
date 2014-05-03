using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour {

    public ViewController Controller;
    public SessionManager SessionManager;
    public CampaignController CampaignController;

	// Use this for initialization
	void Start () {
	    
	}

    public string GetPrefUsername()
    {
        if (PlayerPrefs.HasKey("username"))
        {
            return PlayerPrefs.GetString("username");
        }
        return "Player" + Random.Range(0,200);
    }

    public void SetPrefUsername(string name)
    {
        PlayerPrefs.SetString("username", name);
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
            Username = GetPrefUsername();
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
            GUI.Box(new Rect(330, 10, 220, 120), "Create Campaign");
            
            campaignName = GUI.TextField(new Rect(340,40,200,30), campaignName);
            if (GUI.Button(new Rect(340,100, 200, 20), "Create"))
            {
                CampaignController.CreateCampaign(campaignName);
                Controller.State = ViewController.ViewState.Campaign;
            }
        }

        if (Servers != null)
        {
            GUI.Box(new Rect(120, 10, 200, 120), "");
            GUI.Label(new Rect(130, 30, 50, 20), "Name");
            Username = GUI.TextField(new Rect(180, 30, 120, 20), Username);
            for (int i = 0; i < Servers.Length; i++)
            {
                if (GUI.Button(new Rect(130,70 + i*30,180,20), "Join"))
                {
                    SetPrefUsername(Username);
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
