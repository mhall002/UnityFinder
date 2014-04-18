using UnityEngine;
using System.Collections;

public class GameStatePanel : MonoBehaviour {

    public ViewController ViewController;

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
        GUILayout.EndArea();
    }

	// Update is called once per frame
	void Update () {
	
	}
}
