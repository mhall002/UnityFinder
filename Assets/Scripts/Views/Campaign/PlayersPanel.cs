using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayersPanel : MonoBehaviour {

    public SessionManager SessionManager;
    public SpriteStorage SpriteStorage;
    public bool ColourChangeable;

	// Use this for initialization
	void Start () {
        SessionManager.PropertyChanged += SessionManager_PropertyChanged;
        Players = new List<string>();
        Colours = new List<Color>();
        Players.AddRange(SessionManager.GetPlayers());
        foreach (string player in Players)
        {
            Colours.Add(SessionManager.GetColour(player));
        }
	}

    void SessionManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        Players = new List<string>();
        Colours = new List<Color>();
        Players.AddRange(SessionManager.GetPlayers());
        foreach (string player in Players)
        {
            Colours.Add(SessionManager.GetColour(player));
        }
        Debug.Log("Changed " + Colours[0].b);
    }

    string red = "0";
    string green = "0";
    string blue = "0";

    string ored = "0";
    string ogreen = "0";
    string oblue = "0";

    List<string> Players;
    List<Color> Colours;
    void OnGUI()
    {
        if (SessionManager.Active)
        {
            Rect position = new Rect(Screen.width - 120, 125, 120, 150);
            GUI.Box(position, "");
            GUILayout.BeginArea(position);
            if (ColourChangeable)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Colour:");
                GUILayout.Label("R");
                GUILayout.TextField(red);
                GUILayout.Label("G");
                GUILayout.TextField(green);
                GUILayout.Label("B");
                GUILayout.TextField(blue);
                GUILayout.EndHorizontal();
            }
            GUILayout.Label("Players:");
            for (int i = 0; i < Players.Count; i++)
            {
                //Debug.Log(Players[i]);
                GUILayout.BeginHorizontal();
                GUILayout.Label(Players[i]);
                Color oldColour = GUI.color;
                GUI.color = Colours[i];
                if (GUILayout.Button(SpriteStorage.GetTexture("BlankRoom"), GUILayout.Width(20), GUILayout.Height(20)))
                {
                    SessionManager.OverrideColour(Players[i]);
                }
                GUI.color = oldColour;
                GUILayout.EndHorizontal();
            }
            GUILayout.EndArea();
        }
    }

	// Update is called once per frame
	void Update () {
	}
}
