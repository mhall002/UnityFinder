using UnityEngine;
using System.Collections;

public class BlankRoomScript : MonoBehaviour {

    public SquareMenuScript Menu;
    public int GridX;
    public int GridY;

	// Use this for initialization
	void Start () {
        Menu = (SquareMenuScript) GameObject.Find("SquareMenu").GetComponent("SquareMenuScript");
	}

    void OnMouseDown() {
        Vector2 position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Menu.ShowMenu(0, 0, position);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
