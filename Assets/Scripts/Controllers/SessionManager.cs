using UnityEngine;
using System.Collections;
using Assets;
using System.Collections.Generic;

public class SessionManager : MonoBehaviour {
   // public List<Player> Players;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}
	
    public void StartServer()
    {
        Network.InitializeServer(32, 25001, !Network.HavePublicAddress());
        MasterServer.RegisterHost("mhall002UnityFinder", "Game 1");
    }

    public void StartServerLookup()
    {
        MasterServer.ClearHostList();
        MasterServer.RequestHostList("mhall002UnityFinder");
        hostData = null;
        Debug.Log("Server lookup started");
    }

    HostData[] hostData = null;
    public string[] CheckLookupResults()
    {
        if (MasterServer.PollHostList().Length != 0)
        {
            hostData = MasterServer.PollHostList();
        }

        if (hostData != null)
        {
            string[] hostNames = new string[hostData.Length];
            for (int i = 0; i < hostData.Length; i++)
            {
                hostNames[i] = hostData[i].gameName;
            }
            return hostNames;
        }
        
        return null;
    }

    public void Connect(int i)
    {

    }

	// Update is called once per frame
	void Update () {
	    
	}
}
