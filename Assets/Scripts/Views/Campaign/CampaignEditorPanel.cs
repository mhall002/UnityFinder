using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class CampaignEditorPanel : MonoBehaviour {

    public ViewController ViewController;
    public SessionManager SessionManager;

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
            GUI.Box(new Rect(x, 0, 120, 120), "Campaign Editor");

            if (GUI.Button(GetButtonRect(), "Host"))
            {
                SessionManager.StartServer();
            }
            if (GUI.Button(GetButtonRect(), "Save"))
            {
                Database database = new Database();
            }
            if (GUI.Button(GetButtonRect(), "Load"))
            {

            }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
