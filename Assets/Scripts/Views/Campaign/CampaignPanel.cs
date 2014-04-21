using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;
using System.Collections.Generic;

public class CampaignPanel : MonoBehaviour {

	Character Character;
	public CharacterController CharacterController;
	public GUIStyle CharacterWindowStyle;

	// Use this for initialization
	void Start () 
	{
	
	}



	string Name = "";
	int Race = 0;
	string[] Races = {"Human", "Dwarf", "Elf"};
	int Class = 0;
	string[] Classes = {"Cleric", "Fighter", "Rogue", "Wizard"};
	string Mod = "";

	void CreateSelectionGrid(ref int index, string[] values, ref int y)
	{
		Rect current = new Rect(10, y, 300, 50);
		index = GUI.SelectionGrid(current, index, values,  5);
		y+=60;
	}

	void CreateNameValue(ref string value, string name, ref int y)
	{
		Rect currentLeft = new Rect(10, y, 80, 20);
		Rect currentRight = new Rect(100, y, 100, 20);
		GUI.Label(currentLeft, name);
		value = GUI.TextField (currentRight, value);
		y+=30;
	}

	void CreateThreeLabel(string value1, string value2, string value3, ref int y)
	{
		Rect currentLeft = new Rect(10, y, 80, 20);
		Rect currentMid = new Rect(100, y, 80, 20);
		Rect currentRight = new Rect(190, y, 80, 20);
		GUI.Label(currentLeft, value1);
		GUI.Label(currentMid, value2);
		GUI.Label(currentRight, value3);
		y+=30;
	}

	void CreateThreeField(string value1, ref string value2, string value3, ref int y)
	{
		Rect currentLeft = new Rect(10, y, 80, 20);
		Rect currentMid = new Rect(100, y, 80, 20);
		Rect currentRight = new Rect(190, y, 80, 20);
		GUI.Label(currentLeft, value1);
		value2 = GUI.TextField(currentMid, value2);
		GUI.Label(currentRight, value3);
		y+=30;
	}



	void CreateCharacterWindow(int windowID)
	{
		Character character = CharacterController.Character;
		GUILayout.Button ("Edit");

		Rect currentLeft = new Rect(10, 50, 80, 20);
		Rect currentRight = new Rect(100, 50, 100, 20);
		Rect current = new Rect(10, 50, 300, 50);

		int y = 50;

		CreateNameValue (ref Name, "Name", ref y);

		CreateSelectionGrid (ref Race, Races, ref y);

		CreateSelectionGrid (ref Class, Classes, ref y);

		CreateThreeLabel("Ability", "Score", "Modifier", ref y);

		foreach (StatName name in CharacterController.GetStatsForType(StatType.AbilityScore))
		{
			int oldValue = character.GetStat(name);
			string newValue = oldValue + "";
			int newInt;
			CreateThreeField(name.ToString(), ref newValue, (oldValue/2-5).ToString(), ref y);
			if (int.TryParse(newValue, out newInt))
			{
				CharacterController.SetStat(name, newInt);
			}
		}


		
	}

	Rect WindowRect = new Rect(130,10,800,400);
	bool CharacterWindowShown = false;

	void OnGUI()
	{
		GUILayout.BeginArea(new Rect(0,0,120,400));

		if (GUILayout.Button ("Create Character"))
		{
			if (!CharacterWindowShown)
			{
				CharacterController.CreateBlankCharacter();
			}
			CharacterWindowShown = !CharacterWindowShown;
		}

		if (CharacterWindowShown)
		{
			WindowRect = GUI.Window (0, WindowRect, CreateCharacterWindow, "", CharacterWindowStyle);
		}

		GUILayout.EndArea ();
	}

	// Update is called once per frame
	void Update () 
	{
	
	}
}
