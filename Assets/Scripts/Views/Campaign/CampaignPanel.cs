using UnityEngine;
using System.Collections;
using Assets.Scripts.Models;
using System.Collections.Generic;
using System.Text;

public class CampaignPanel : MonoBehaviour {

	Character Character;
	public CharacterController CharacterController;
	public GUIStyle CharacterWindowStyle;
    public GUIStyle CharacterImageStyle;
    public SpriteStorage SpriteStorage;

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
		Rect current = new Rect(10, y, 300, 30);
		index = GUI.SelectionGrid(current, index, values,  5);
		y+=40;
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

    void CreateHealthPair(ref string current, ref string max, ref int y)
    {
        Rect currentLeft = new Rect(310, y, 60, 20);
        Rect currentCurrent = new Rect(400, y, 25, 20);
        Rect currentSlash = new Rect(425, y, 10, 20);
        Rect currentMax = new Rect(435, y, 25, 20);
        GUI.Label(currentLeft, "Hit Points");
        current = GUI.TextField(currentCurrent, current);
        GUI.Label(currentSlash, "/");
        max = GUI.TextField(currentMax, max);
        y += 30;
    }

    void CreateLeftNameValue(ref string value, string name, ref int y)
    {
        Rect currentLeft = new Rect(310, y, 90, 20);
        Rect currentRight = new Rect(400, y, 30, 20);
        GUI.Label(currentLeft, name);
        value = GUI.TextField(currentRight, value);
        y += 20;
    }

    void CreateRightNameValue(ref string value, string name, ref int y)
    {
        Rect currentLeft = new Rect(500, y, 100, 20);
        Rect currentRight = new Rect(600, y, 60, 20);
        GUI.Label(currentLeft, name);
        value = GUI.TextField(currentRight, value);
        y += 20;
    }

    void CreateCombatSection(Character character)
    {
        int y = 20;

        int oldValue = character.GetStat(StatName.HitPoints);
		string newValue = oldValue + "";
		int newInt = oldValue;

        int oldValue2 = character.GetStat(StatName.HitPointTotal);
		string newValue2 = oldValue2 + "";
		int newInt2 = oldValue;

        CreateHealthPair(ref newValue, ref newValue2, ref y);

        if (int.TryParse(newValue, out newInt))
        {
            CharacterController.SetStat(StatName.HitPoints, newInt);
        }
        if (int.TryParse(newValue2, out newInt2))
        {
            CharacterController.SetStat(StatName.HitPointTotal, newInt2);
        }

        string value = character.GetStat(StatName.ArmorClass) + "";

        CreateLeftNameValue(ref value, "AC", ref y);

        if (int.TryParse(value, out newInt))
        {
            CharacterController.SetStat(StatName.ArmorClass, newInt);
        }

        value = character.GetStat(StatName.FortSave) + "";

        CreateLeftNameValue(ref value, "Fortitude Save", ref y);

        if (int.TryParse(value, out newInt))
        {
            CharacterController.SetStat(StatName.FortSave, newInt);
        }

        value = character.GetStat(StatName.ReflexSave) + "";

        CreateLeftNameValue(ref value, "Reflex Save", ref y);

        if (int.TryParse(value, out newInt))
        {
            CharacterController.SetStat(StatName.ReflexSave, newInt);
        }

        value = character.GetStat(StatName.WillSave) + "";

        CreateLeftNameValue(ref value, "Will Save", ref y);

        if (int.TryParse(value, out newInt))
        {
            CharacterController.SetStat(StatName.WillSave, newInt);
        }

        value = character.GetStat(StatName.ClassAttack) + "";

        CreateLeftNameValue(ref value, "Attack Bonus", ref y);

        if (int.TryParse(value, out newInt))
        {
            CharacterController.SetStat(StatName.ClassAttack, newInt);
        }

        value = character.GetStat(StatName.SkillRanks) + "";

        CreateLeftNameValue(ref value, "Skill Ranks", ref y);

        if (int.TryParse(value, out newInt))
        {
            CharacterController.SetStat(StatName.SkillRanks, newInt);
        }

        y += 30;

        value = character.GetStat(StatName.Initiative) + "";

        CreateLeftNameValue(ref value, "Initiative", ref y);

        if (int.TryParse(value, out newInt))
        {
            CharacterController.SetStat(StatName.Initiative, newInt);
        }

        value = character.GetStat(StatName.MeleeAttack) + "";

        CreateLeftNameValue(ref value, "Melee Attack", ref y);

        if (int.TryParse(value, out newInt))
        {
            CharacterController.SetStat(StatName.MeleeAttack, newInt);
        }

        value = character.GetStat(StatName.RangedAttack) + "";

        CreateLeftNameValue(ref value, "Ranged Attack", ref y);

        if (int.TryParse(value, out newInt))
        {
            CharacterController.SetStat(StatName.RangedAttack, newInt);
        }
    }

    void CreateSkillSection(Character character)
    {
        int y = 20;

        foreach(StatName stat in CharacterController.GetStatsForType(StatType.Skill))
        {
            string name = CharacterController.GetNameForStat(stat);
            int newInt = 0;

            string value = character.GetStat(stat) + "";

            CreateRightNameValue(ref value, name, ref y);

            if (int.TryParse(value, out newInt))
            {
                CharacterController.SetStat(stat, newInt);
            }
        }
    }

    Texture[] CharacterTextures = null;
    int selectedImage = -1;

    void CharacterImageWindow(int windowID)
    {
        Character character = CharacterController.Character;

        if (CharacterTextures == null)
        {
            CharacterTextures = new Texture[SpriteStorage.CharacterSprites.Length];
            for (int i = 0; i < SpriteStorage.CharacterSprites.Length; i++)
            {
                CharacterTextures[i] = SpriteStorage.GetTexture(SpriteStorage.CharacterSprites[i]);
            }
        }
        selectedImage = GUILayout.SelectionGrid(selectedImage, CharacterTextures, 10, CharacterImageStyle, GUILayout.Width(400));
    }

    bool choosingImage = false;
	void CreateCharacterWindow(int windowID)
	{
        
            Character character = CharacterController.Character;
        
		Rect currentLeft = new Rect(10, 50, 80, 20);
		Rect currentRight = new Rect(100, 50, 100, 20);
		Rect current = new Rect(10, 50, 300, 50);
        Rect Icon = new Rect(10, 10, 75, 75);

        if (character.Image == null)
        {
            character.Image = "Characters/knight";
        }

        if (GUI.Button(Icon, SpriteStorage.GetTexture(character.Image)))
        {
            choosingImage = !choosingImage;
            selectedImage = -1;
        }
        
        if (choosingImage && selectedImage >= 0)
        {
            choosingImage = false;
            character.Image = SpriteStorage.CharacterSprites[selectedImage];
        }

		int y = 75;

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

        CreateCombatSection(character);

        CreateSkillSection(character);
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

        if (choosingImage)
        {
            GUILayout.Window(1, new Rect(240, 10, 400, 400), CharacterImageWindow, "", CharacterWindowStyle);
        }

		GUILayout.EndArea ();
	}

	// Update is called once per frame
	void Update () 
	{
	
	}
}
