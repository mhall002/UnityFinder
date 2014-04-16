using UnityEngine;
using System.Collections;

public class CampaignEditorPanel : MonoBehaviour {

	// Use this for initialization
    void Start()
    {
    }

    int y = 30;
    int x = Screen.width - 120;
    int buttonWidth = 100;
    int buttonHeight = 20;
    int spacing = 10;

    Rect GetButtonRect()
    {
        Rect rect = new Rect(x + 10, y + buttonHeight, buttonWidth, buttonHeight);
        y += buttonHeight + spacing;
        return rect;
    }

    void OnGUI()
    {
        x = Screen.width - 120;
        y = 10;
        GUI.Box(new Rect(x, 0, 120, 200), "Campaign Editor");

        if (GUI.Button(GetButtonRect(), "Host"))
        {

        }
        if (GUI.Button(GetButtonRect(), "Save"))
        {

        }
        if (GUI.Button(GetButtonRect(), "Load"))
        {

        }
        GUI.Label(new Rect(x+ 10, y + 10, buttonWidth - 10, 20), "Players:");
    }

	// Update is called once per frame
	void Update () {
	
	}
}
