using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 translate = new Vector3(0, 0, 0);
	    if (Input.GetKey(KeyCode.LeftArrow))
        {
            translate.x -= 1 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            translate.x += 1 * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            translate.y -= 1 * Time.deltaTime; 
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            translate.y += 1 * Time.deltaTime;
        }
        transform.Translate(translate);
        camera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel")*2;
	}
}
