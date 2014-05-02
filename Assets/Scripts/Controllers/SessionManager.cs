﻿using UnityEngine;
using System.Collections;
using Assets;
using System.Collections.Generic;

public class SessionManager : MonoBehaviour {
   // public List<Player> Players;
	// Use this for initialization

    public RemoteView RemoteView;
    public NetworkController NetworkController;
    public string UserName;

    bool isClient = false;
    public bool IsClient
    {
        get { return isClient; }
    }

    public bool Active
    {
        get;
        private set;
    }

	void Start () {
        DontDestroyOnLoad(this);
        Active = false;
	}
	
    public void StartServer()
    {
        Network.InitializeServer(32, 25002, !Network.HavePublicAddress());
        UserName = "GM";
        MasterServer.RegisterHost("mhall002UnityFinder", "Game 1");
        RemoteView.gameObject.SetActive(true);
        RemoteView.Initiate();
        Active = true;
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

    public void Connect(int i, string userName)
    {
        Network.Connect(hostData[i]);
        NetworkController.gameObject.SetActive(true);
        UserName = userName;
        isClient = true;
        Active = true;
    }

    void OnConnectedToServer()
    {
        NetworkController.Connect(UserName);
    }

	// Update is called once per frame
	void Update () {
	    
	}
}
