using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Assets.Scripts.Models;

public class CharacterController : MonoBehaviour {

	public enum StatName2
	{
		Strength,
		Dexterity,
		Constitution,
		Intelligence,
		Wisdom,
		Charisma,
		Acrobatics,
		Bluff,
		Climb,
		Diplomacy,
		DisableDevice,
		Heal,
		Arcana,
		Dungeoneering,
		Geography,
		History,
		Local,
		Nature,
		Religion,
		Perception,
		Ride,
		SenseMotive,
		SpellCraft,
		Stealth,
		Swim,
		FortSave,
		ReflexSave,
		WillSave,
		ClassAttack,
		SkillRanks,
		Initiative,
		MeleeAttack,
		RangedAttack,
		WeaponAttack1,
		WeaponAttack2,
		ArmorClass
	}
	
	public enum StatType2
	{
		AbilityScore,
		AbilityMod,
		Skill,
		Saves,
		ClassFeature,
		Combat
	}

	public Character Character; 

	private static Dictionary<StatName, StatType> statNameToType = null;
	public static Dictionary<StatName, StatType> StatNameToType
	{
		get
		{
			if (statNameToType == null)
				InitStatNameToType();
			return statNameToType;
		}
	}

	private static Dictionary<StatType, List<StatName>> statTypeToNames = null;
	public static Dictionary<StatType, List<StatName>> StatTypeToNames
	{
		get
		{
			if (statTypeToNames == null)
				InitStatNameToType();
			return statTypeToNames;
		}
	}

    private static Dictionary<StatName, string> statNameToString = null;
    public static Dictionary<StatName, string> StatNameToString
    {
        get
        {
            if (statTypeToNames == null)
                InitStatNameToType();
            return statNameToString;
        }
    }

	private static void InitStatNameToType()
	{
        statNameToString = new Dictionary<StatName,string>();
		statNameToType = new Dictionary<StatName, StatType>();
		statTypeToNames = new Dictionary<StatType, List<StatName>>();

        foreach (StatName stat in Enum.GetValues(typeof(StatName)))
        {
            StringBuilder builder = new StringBuilder(stat.ToString());
            for(int i = 1; i < builder.Length; i++)
            {
                if (char.IsUpper(builder[i]))
                {
                    builder.Insert(i, ' ');
                    i++;
                }
            }
            statNameToString[stat] = builder.ToString();
        }

		statTypeToNames[StatType.AbilityScore] = new List<StatName>();
		for(int i = 0; i < 6; i++)
		{
			statNameToType[(StatName)i] = StatType.AbilityScore;
			statTypeToNames[StatType.AbilityScore].Add((StatName)i);
		}

		statTypeToNames[StatType.AbilityMod] = new List<StatName>();
		for(int i = 6; i < 12; i++)
		{
			statNameToType[(StatName)i] = StatType.AbilityMod;
			statTypeToNames[StatType.AbilityMod].Add((StatName)i);
		}

		statTypeToNames[StatType.Skill] = new List<StatName>();
		for(int i = 12; i < 31; i++)
		{
			statNameToType[(StatName)i] = StatType.Skill;
			statTypeToNames[StatType.Skill].Add((StatName)i);
		}

		statTypeToNames[StatType.Saves] = new List<StatName>();
		for(int i = 31; i < 34; i++)
		{
			statNameToType[(StatName)i] = StatType.Saves;
			statTypeToNames[StatType.Saves].Add((StatName)i);
		}

		statTypeToNames[StatType.ClassFeature] = new List<StatName>();
		for(int i = 34; i < 36; i++)
		{
			statNameToType[(StatName)i] = StatType.ClassFeature;
			statTypeToNames[StatType.ClassFeature].Add((StatName)i);
		}

		statTypeToNames[StatType.Combat] = new List<StatName>();
		for(int i = 36; i < 42; i++)
		{
			statNameToType[(StatName)i] = StatType.Combat;
			statTypeToNames[StatType.Combat].Add((StatName)i);
		}
	}

	public IEnumerable<StatName> GetStatsForType(StatType type)
	{
		return StatTypeToNames[type];
	}

    public string GetNameForStat(StatName name)
    {
        return StatNameToString[name];
    }

	// Use this for initialization
	void Start () {
	
	}

	public void CreateBlankCharacter()
	{
		Character character = new Character();
		character.Name = "Name";
		character.Level = 0;
		character.Height = 1;
		character.Width = 1;
		character.XPWorth = 0;
		foreach (StatName statName in Enum.GetValues(typeof(StatName)))
		{
			character.SetStat(statName, 0);
		}
		this.Character = character;
	}

	public void SetStat(StatName stat, int value)
	{
		Character.SetStat(stat, value);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
