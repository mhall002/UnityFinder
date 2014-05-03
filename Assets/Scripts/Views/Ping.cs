using UnityEngine;
using System.Collections;

public class Ping : MonoBehaviour {

    float creationTime;
    public float UpperScale = 0.8f;
    public float LowerScale = 0.6f;

	// Use this for initialization
	void Start () {
        creationTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - creationTime > 5)
        {
            DestroyObject(gameObject);
        }

        float scale = UpperScale - (UpperScale - LowerScale) * ((Time.time - creationTime)*2 - (int)((Time.time - creationTime)*2));
        transform.localScale = new Vector3(scale, scale, 1);
	}
}
