using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour {

    public ViewController Controller;
    public SessionManager SessionManager;

	// Use this for initialization
	void Start () {
	
	}

    string[] Servers = null;
    bool WaitingForServers = false;
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
            Controller.State = ViewController.ViewState.Campaign;
        }

        if (Servers != null)
        {
            GUI.Box(new Rect(120, 10, 100, 120), "Games");
            for (int i = 0; i < Servers.Length; i++)
            {
                if (GUI.Button(new Rect(130,40 + i*30,80,20), Servers[i]))
                {
                    SessionManager.Connect(i);
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
