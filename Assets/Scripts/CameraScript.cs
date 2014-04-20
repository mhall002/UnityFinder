using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public float PanSpeed = 1f;
	public float ScrollSpeed = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 translate = new Vector3(0, 0, 0);
	    if (Input.GetKey(KeyCode.LeftArrow))
        {
            translate.x -= PanSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
			translate.x += PanSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
			translate.y -= PanSpeed * Time.deltaTime; 
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
			translate.y += PanSpeed * Time.deltaTime;
        }
        transform.Translate(translate);
        camera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel")*2*ScrollSpeed;
	}
}
