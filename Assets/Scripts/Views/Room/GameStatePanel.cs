using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;

public class GameStatePanel : MonoBehaviour {

    public SpriteStorage SpriteStorage;
    public CampaignController CampaignController;
    public ViewController ViewController;
    public Entity SelectedEntity = null;

	// Use this for initialization
	void Start () {
	
	}
	
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, 150, 500));
        if (GUILayout.Button("View Campaign"))
        {
            ViewController.State = global::ViewController.ViewState.Campaign;
        }
        GUILayout.Label("Characters:");
        GUILayout.BeginHorizontal("box");
        if (CampaignController.Campaign != null)
        {
            foreach (Entity entity in CampaignController.Campaign.Characters)
            {
                if (GUILayout.Button(SpriteStorage.GetTexture(entity.Image), GUILayout.Width(60), GUILayout.Height(60)))
                {
                    SelectedEntity = entity;
                }
            }
        }
        GUILayout.EndHorizontal();

        GUILayout.EndArea(); 


    }

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            SelectedEntity = null;
        }
	}
}
