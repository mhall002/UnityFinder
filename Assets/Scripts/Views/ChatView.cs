using UnityEngine;
using System.Collections;

public class ChatView : MonoBehaviour {

    public ChatController ChatController;
    public SpriteStorage SpriteStorage;

	// Use this for initialization
	void Start () {
	
	}

    Vector2 ScrollPosition;
    string TextInput = "<Message>";
    bool ChangeFocus = false;
    bool HasFocus = false;
    bool SendText = false;
    int Roll = 0;
    bool SendRoll = false;
    ChatMessage lastMessage = null;

    void OnGUI()
    {
        GUI.Box(new Rect(0, Screen.height - 300, 200, 300), "");
        GUILayout.BeginArea(new Rect(0, Screen.height - 300, 200, 300));
        GUILayout.BeginHorizontal();
        int size = 35;
        if (GUILayout.Button(SpriteStorage.GetTexture("4die3"), GUILayout.Width(size), GUILayout.Height(size)))
        {
            SendRoll = true;
            Roll = 4;
        }
        if(GUILayout.Button(SpriteStorage.GetTexture("6die2"), GUILayout.Width(size), GUILayout.Height(size)))
        {
            SendRoll = true;
            Roll = 6;
        }
        if (GUILayout.Button(SpriteStorage.GetTexture("8die2"), GUILayout.Width(size), GUILayout.Height(size)))
        {
            SendRoll = true;
            Roll = 8;
        }
        if (GUILayout.Button(SpriteStorage.GetTexture("10die2"), GUILayout.Width(size), GUILayout.Height(size)))
        {
            SendRoll = true;
            Roll = 10;
        }
        if (GUILayout.Button(SpriteStorage.GetTexture("20die2"), GUILayout.Width(size), GUILayout.Height(size)))
        {
            SendRoll = true;
            Roll = 20;
        }
        if (ChatController.Messages.Count > 0 && lastMessage != ChatController.Messages[ChatController.Messages.Count-1])
        {
            lastMessage = ChatController.Messages[ChatController.Messages.Count - 1];
            ScrollPosition = new Vector2(0, 500000);
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginScrollView(ScrollPosition);
        foreach(ChatMessage message in ChatController.Messages)
        {
            message.OnGUI();
        }
        GUILayout.EndScrollView();
        GUI.SetNextControlName("TextInput");
        TextInput = GUILayout.TextField(TextInput);
        GUILayout.EndArea();
        if (GUI.GetNameOfFocusedControl() == "TextInput")
        {
            HasFocus = true;
            if (Event.current.isKey && Event.current.keyCode == KeyCode.Return && TextInput.Length > 0 && !ChangeFocus)
            {
                SendText = true;
            }
        }
        if (ChangeFocus)
        {
            GUI.FocusControl("TextInput");
            ChangeFocus = false;
        }
        HasFocus = false;
        GUI.Label(new Rect(Input.mousePosition.x+15, Screen.height-Input.mousePosition.y, 200, 30), GUI.tooltip);
    }

	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown("return"))
        {
            if (!HasFocus)
            {
                ChangeFocus = true;
            }
        }

        if (SendText)
        {
            SendText = false;
            if (TextInput.Length > 0)
            {
                if (TextInput.StartsWith("/r") || TextInput.StartsWith("/roll"))
                {
                    int i = 0;
                    while (TextInput[i] != ' ')
                    {
                        i++;
                    }
                    ChatController.AddRoll(TextInput.Substring(i));
                }
                else
                {
                    ChatController.AddText(TextInput);
                }
                TextInput = "";
            }
        }

        if (SendRoll)
        {
            ChatController.AddRoll("1d" + Roll);
            SendRoll = false;
        }
	}
}
