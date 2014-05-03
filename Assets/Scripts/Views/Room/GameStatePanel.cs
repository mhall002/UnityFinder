using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;

public class GameStatePanel : MonoBehaviour {

    public SpriteStorage SpriteStorage;
    public CampaignController CampaignController;
    public ViewController ViewController;
    public MapGrid MapGrid;
    public int CharacterIconSize;

	// Use this for initialization
	void Start () {
	
	}
	
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 30, 180, 500));
        if (ViewController.State != global::ViewController.ViewState.Campaign)
        {
            if (GUILayout.Button("View Campaign"))
            {
                ViewController.State = global::ViewController.ViewState.Campaign;
            }
        }
        GUILayout.Label("Characters:");
        if (CampaignController.Campaign != null)
        {
            int maxHorizontal = 180 / (CharacterIconSize+5);
            int across = 1;
            GUILayout.BeginHorizontal();
            foreach (Entity entity in CampaignController.Campaign.Characters)
            {
                if (across++ > maxHorizontal)
                {
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    across = 2;
                }
                if (GUILayout.Button(SpriteStorage.GetTexture(entity.Image), GUILayout.Width(CharacterIconSize), GUILayout.Height(CharacterIconSize)))
                {
                    if (ViewController.State != global::ViewController.ViewState.Room)
                        ViewController.State = global::ViewController.ViewState.Room;
                    MapGrid.CharacterFocus(entity);
                }
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndArea(); 
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            MapGrid.SelectedEntity = null;
        }
	}
}
