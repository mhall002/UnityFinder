using UnityEngine;
using System.Collections;
using Assets;
using System.Collections.Generic;
using System.ComponentModel;

public class SessionManager : MonoBehaviour {
   // public List<Player> Players;
	// Use this for initialization

    public event PropertyChangedEventHandler PropertyChanged;

    public RemoteView RemoteView;
    public NetworkController NetworkController;
    public string UserName = "GM";

    private Dictionary<string, string> PlayerToName = new Dictionary<string, string>();
    private Dictionary<string, Color> NameToColour = new Dictionary<string, Color>();

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
        UserName = "GM";
        Network.InitializeServer(32, 25002, !Network.HavePublicAddress());
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
        UserName = userName;
        Network.Connect(hostData[i]);
        NetworkController.gameObject.SetActive(true);
        isClient = true;
        Active = true;
    }

    void OnConnectedToServer()
    {
        Connect(UserName);
    }

    void OnServerInitialized()
    {
        Debug.Log("Connecting with username " + UserName);
        Connect(UserName);
    }

	// Update is called once per frame
	void Update () {
	    
	}

    [RPC]
    void AddPlayer(string userName, NetworkMessageInfo info)
    {
        Debug.Log("Connected " + info.sender.ToString() + " - " + userName);
        PlayerToName[info.sender.ToString()] = userName;
        NameToColour[userName] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        OnPropertyChanged("Players");
    }

    public string GetPlayerName(NetworkPlayer player)
    {
        return PlayerToName[player.ToString()];
    }

    public void Connect(string username)
    {
        networkView.RPC("AddPlayer", RPCMode.AllBuffered, username);
    }

    public void SetColour(Vector3 colour)
    {
        networkView.RPC("ChangeColour", RPCMode.AllBuffered, colour);
    }

    [RPC]
    public void ChangeColour(Vector3 colour, NetworkMessageInfo info)
    {
        string name = PlayerToName[info.sender.ToString()];
        Color color = new Color(colour.x, colour.y, colour.z);
        NameToColour[name] = color;
        OnPropertyChanged("Colours");
    }

    public void OverrideColour(string player)
    {
        NameToColour[player] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        OnPropertyChanged("Colours");
    }

    public Color GetColour(string name)
    {
        if (NameToColour.ContainsKey(name))
            return NameToColour[name];
        return new Color(0, 0, 0);
    }

    public ICollection<string> GetPlayers()
    {
        return PlayerToName.Values;
    }

    void OnPlayerDisconnected(NetworkPlayer sender)
    {
        if (PlayerToName.ContainsKey(sender.ToString()))
        {
            if (NameToColour.ContainsKey(PlayerToName[sender.ToString()]))
            {
                NameToColour.Remove(PlayerToName[sender.ToString()]);
            }
            PlayerToName.Remove(sender.ToString());
            OnPropertyChanged("Players");
        }
    }

    protected void OnPropertyChanged(string name)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(name));
        }
    }
}
