using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 100, 120), "Start Menu");

        if (GUI.Button(new Rect(20,40,80,20), "Join"))
        {
            
        }

        if (GUI.Button(new Rect(20, 70, 80, 20), "Load"))
        {

        }

        if (GUI.Button(new Rect(20,100, 80, 20), "Create"))
        {
            Application.LoadLevel(1);
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
