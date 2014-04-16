using UnityEngine;
using System.Collections;

public class CampaignGridScript : MonoBehaviour {

    public GameObject blankRoomPrefab;

	// Use this for initialization
	void Start () {
        //Creates the grid of Add Room
        float spacing = 1.0f;
        for (float x = -9.5f; x < 7.18f; x += spacing)
        {
            for (float y = -4.2f; y < 4.5f; y += spacing)
            {
                GameObject go = Instantiate(blankRoomPrefab, new Vector3(x,y,0), Quaternion.identity) as GameObject;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
